using FluentValidation;
using SM.WebApi.Contracts;
using SM.WebApi.Domain;
using SM.WebApi.Infrastructure;

namespace SM.WebApi.Endpoints;

public static class SubstationEndpoints
{
    public static void MapSubstationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/substations")
                          .WithTags("Substations");

        // GET all
        group.MapGet("/", async (IRepository<Substation> repo) =>
        {
            var list = await repo.GetAllAsync();
            var result = list.Select(s => new SubstationDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                CustomerId = s.CustomerId
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:int}", async (int id, IRepository<Substation> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();

            var dto = new SubstationDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CustomerId = entity.CustomerId
            };
            return Results.Ok(dto);
        });

        // POST (create)
        group.MapPost("/", async (
            SubstationCreateDto dto,
            IRepository<Substation> repo,
            IValidator<SubstationCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var entity = new Substation
            {
                Name = dto.Name,
                Description =  dto.Description ?? string.Empty,
                CustomerId = dto.CustomerId
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new SubstationDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CustomerId = entity.CustomerId
            };

            return Results.Created($"/substations/{entity.Id}", result);
        });

        // PUT (update)
        group.MapPut("/{id:int}", async (
            int id,
            SubstationCreateDto dto,
            IRepository<Substation> repo,
            IValidator<SubstationCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description ?? string.Empty;
            existing.CustomerId = dto.CustomerId;

            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new SubstationDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Description = existing.Description,
                CustomerId = existing.CustomerId
            };

            return Results.Ok(result);
        });

        // DELETE
        group.MapDelete("/{id:int}", async (int id, IRepository<Substation> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            repo.Remove(existing);
            await repo.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
