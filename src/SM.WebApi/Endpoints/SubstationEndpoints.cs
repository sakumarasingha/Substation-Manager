using FluentValidation;
using SM.Shared;
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
            var list = await repo.GetAllAsync(t => t.Customer);
            var result = list.Select(s => new SubstationDto
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                CustomerId = s.CustomerId,
                Customer = new CustomerDto
                {
                    Name = s.Customer?.Name?? string.Empty,
                    Code = s.Customer?.Code?? string.Empty
                }
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:guid}", async (Guid id, IRepository<Substation> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();

            var dto = new SubstationDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                CustomerId = entity.CustomerId
            };
            return Results.Ok(dto);
        });

        // GET by id
        group.MapGet("/bycustid/{id:guid}", async (Guid id, IRepository<Substation> repo) =>
        {
            var list = await repo.GetManyAsync(s => s.CustomerId == id, t => t.Customer);
            var result = list.Select(s => new SubstationDto
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                CustomerId = s.CustomerId,
                Customer = new CustomerDto
                {
                    Name = s.Customer?.Name ?? string.Empty,
                    Code = s.Customer?.Code ?? string.Empty
                }
            });
            return Results.Ok(result);
        });

        // POST (create)
        group.MapPost("/", async (
            SubstationDto dto,
            IRepository<Substation> repo,
            IValidator<SubstationDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var entity = new Substation
            {
                Name = dto.Name,
                Code = dto.Code ?? string.Empty,
                CustomerId = dto.CustomerId,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "system"
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new SubstationDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                CustomerId = entity.CustomerId
            };

            return Results.Created($"/substations/{entity.Id}", result);
        });

        // PUT (update)
        group.MapPut("/{id:guid}", async (
            Guid id,
            SubstationDto dto,
            IRepository<Substation> repo,
            IValidator<SubstationDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            existing.Name = dto.Name;
            existing.Code = dto.Code ?? string.Empty;
            existing.CustomerId = dto.CustomerId;

            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new SubstationDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Code = existing.Code,
                CustomerId = existing.CustomerId
            };

            return Results.Ok(result);
        });

        // DELETE
        group.MapDelete("/{id:guid}", async (Guid id, IRepository<Substation> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            repo.Remove(existing);
            await repo.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
