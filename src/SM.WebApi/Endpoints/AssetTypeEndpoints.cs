using FluentValidation;
using SM.WebApi.Contracts;
using SM.WebApi.Domain;
using SM.WebApi.Infrastructure;

namespace SM.WebApi.Endpoints;

public static class AssetTypeEndpoints
{
    public static void MapAssetTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/AssetTypes")
                          .WithTags("AssetTypes");

        // GET all
        group.MapGet("/", async (IRepository<AssetType> repo) =>
        {
            var list = await repo.GetAllAsync();
            var result = list.Select(ec => new AssetTypeDto
            {
                Id = ec.Id,
                Name = ec.Name
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:int}", async (int id, IRepository<AssetType> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();

            var dto = new AssetTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return Results.Ok(dto);
        });

        // POST (create)
        group.MapPost("/", async (
            AssetTypeCreateDto dto,
            IRepository<AssetType> repo,
            IValidator<AssetTypeCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var entity = new AssetType
            {
                Name = dto.Name
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new AssetTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return Results.Created($"/equipmentcategories/{entity.Id}", result);
        });
    }
}
