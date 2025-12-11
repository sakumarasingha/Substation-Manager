using FluentValidation;
using SM.Shared;
using SM.WebApi.Domain;
using SM.WebApi.Infrastructure;

namespace SM.WebApi.Endpoints;

public static class TransformerEndpoints
{
    public static void MapTransformerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/transformers")
                          .WithTags("Transformers");

        // GET all
        group.MapGet("/", async (IRepository<Transformer> repo) =>
        {
            var list = await repo.GetAllAsync();

            var result = list.Select(t => new TransformerDto
            {
                Id = t.Id,
                SerialNumber = t.SerialNumber,
                ManufacturerName = t.ManufacturerName,
                YearOfManufacture = t.YearOfManufacture,
                RatedCapacity = t.RatedCapacity,
                PrimaryVoltage = t.PrimaryVoltage,
                SecondaryVoltage = t.SecondaryVoltage,
                TransformerType = t.TransformerType,
                VectorGroup = t.VectorGroup
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:guid}", async (Guid id, IRepository<Transformer> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();

            var dto = new TransformerDto
            {
                Id = entity.Id,
                SerialNumber = entity.SerialNumber,
                ManufacturerName = entity.ManufacturerName,
                YearOfManufacture = entity.YearOfManufacture,
                RatedCapacity = entity.RatedCapacity,
                PrimaryVoltage = entity.PrimaryVoltage,
                SecondaryVoltage = entity.SecondaryVoltage,
                TransformerType = entity.TransformerType,
                VectorGroup = entity.VectorGroup
            };
            return Results.Ok(dto);
        });

        // POST (create)
        group.MapPost("/", async (
            TransformerDto dto,
            IRepository<Transformer> repo,
            IValidator<TransformerDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var asset = new Asset
            {
                AssetTypeId = dto.Asset.AssetTypeId,
                InstallationDate = dto.Asset.InstallationDate,
                SubstationId = dto.Asset.SubstationId,
                Status = dto.Asset.Status
            };

            var entity = new Transformer
            {
                Id = asset.Id,
                Asset = asset,
                SerialNumber = dto.SerialNumber,
                ManufacturerName = dto.ManufacturerName,
                YearOfManufacture = dto.YearOfManufacture,
                RatedCapacity = dto.RatedCapacity,
                PrimaryVoltage = dto.PrimaryVoltage,
                SecondaryVoltage = dto.SecondaryVoltage,
                TransformerType = dto.TransformerType,
                VectorGroup = dto.VectorGroup
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new TransformerDto
            {
                Id = entity.Id,
               
                SerialNumber = entity.SerialNumber,
                ManufacturerName = entity.ManufacturerName,
                YearOfManufacture = entity.YearOfManufacture,
                RatedCapacity = entity.RatedCapacity,
                PrimaryVoltage = entity.PrimaryVoltage,
                SecondaryVoltage = entity.SecondaryVoltage,
                TransformerType = entity.TransformerType,
                VectorGroup = entity.VectorGroup
            };

            return Results.Created($"/transformers/{entity.Id}", result);
        });

        // PUT (update)
        group.MapPut("/{id:guid}", async (
            Guid id,
            TransformerDto dto,
            IRepository<Transformer> repo,
            IValidator<TransformerDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            existing.SerialNumber = dto.SerialNumber;
            existing.ManufacturerName = dto.ManufacturerName;
            existing.YearOfManufacture = dto.YearOfManufacture;
            existing.RatedCapacity = dto.RatedCapacity;
            existing.PrimaryVoltage = dto.PrimaryVoltage;
            existing.SecondaryVoltage = dto.SecondaryVoltage;
            existing.TransformerType = dto.TransformerType;
            existing.VectorGroup = dto.VectorGroup;

            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new TransformerDto
            {
                Id = existing.Id,
                SerialNumber = existing.SerialNumber,
                ManufacturerName = existing.ManufacturerName,
                YearOfManufacture = existing.YearOfManufacture,
                RatedCapacity = existing.RatedCapacity,
                PrimaryVoltage = existing.PrimaryVoltage,
                SecondaryVoltage = existing.SecondaryVoltage,
                TransformerType = existing.TransformerType,
                VectorGroup = existing.VectorGroup
            };

            return Results.Ok(result);
        });

        // DELETE
        group.MapDelete("/{id:guid}", async (Guid id, IRepository<Transformer> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            repo.Remove(existing);
            await repo.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
