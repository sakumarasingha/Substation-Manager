using FluentValidation;
using SM.Shared;
using SM.WebApi.Domain;
using SM.WebApi.Infrastructure;

namespace SM.WebApi.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/customers")
                          .WithTags("Customers");

        // GET all
        group.MapGet("/", async (IRepository<Customer> repo) =>
        {
            var list = await repo.GetAllAsync();
            var result = list.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                PrimaryEmail = c.PrimaryEmail,
                PrimaryPhone = c.PrimaryPhone
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:guid}", async (Guid id, IRepository<Customer> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();
            var dto = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                PrimaryEmail = entity.PrimaryEmail,
                PrimaryPhone = entity.PrimaryPhone,
                Alias = entity.Alias,
                Status = (Shared.CustomerStatus)entity.Status,
                BillingAddress = entity.BillingAddress is null ? null : new AddressDto
                {
                    Line1 = entity.BillingAddress.Line1,
                    Line2 = entity.BillingAddress.Line2,
                    City = entity.BillingAddress.City,
                    StateOrProvince = entity.BillingAddress.StateOrProvince,
                    PostalCode = entity.BillingAddress.PostalCode,
                    Country = entity.BillingAddress.Country
                },
                SlaTier = entity.SlaTier,
                Notes = entity.Notes,
                AssetCount = entity.AssetCount,
                ActiveWorkOrders = entity.ActiveWorkOrders,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };

            return Results.Ok(dto);
        });

        // POST (create)
        group.MapPost("/", async (
            CustomerDto dto,
            IRepository<Customer> repo,
            IValidator<CustomerDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var entity = new Customer
            {
                Name = dto.Name,
                Code = dto.Code,
                Alias = dto.Alias,
                PrimaryEmail = dto.PrimaryEmail,
                PrimaryPhone = dto.PrimaryPhone,
                Website = dto.Website,
                Status = (Domain.CustomerStatus)dto.Status,
                SlaTier = dto.SlaTier,
                Notes = dto.Notes,
                TenantId = "00000000-0000-0000-0000-000000000000",

                BillingAddress = dto.BillingAddress is null ? null : new Address
                {
                    Line1 = dto.BillingAddress.Line1,
                    Line2 = dto.BillingAddress.Line2,
                    City = dto.BillingAddress.City,
                    StateOrProvince = dto.BillingAddress.StateOrProvince,
                    PostalCode = dto.BillingAddress.PostalCode,
                    Country = dto.BillingAddress.Country
                },

                // Audit fields (usually set by service layer)
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "system"
            };


            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                PrimaryEmail = entity.PrimaryEmail,
                PrimaryPhone = entity.PrimaryPhone,
                Alias = entity.Alias,
                Status = (Shared.CustomerStatus)entity.Status,
                BillingAddress = entity.BillingAddress is null ? null : new AddressDto
                {
                    Line1 = entity.BillingAddress.Line1,
                    Line2 = entity.BillingAddress.Line2,
                    City = entity.BillingAddress.City,
                    StateOrProvince = entity.BillingAddress.StateOrProvince,
                    PostalCode = entity.BillingAddress.PostalCode,
                    Country = entity.BillingAddress.Country
                },
                SlaTier = entity.SlaTier,
                Notes = entity.Notes,
                AssetCount = entity.AssetCount,
                ActiveWorkOrders = entity.ActiveWorkOrders,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };

            return Results.Created($"/customers/{entity.Id}", result);
        });

        // PUT (update)
        group.MapPut("/{id:guid}", async (
            Guid id,
            CustomerDto dto,
            IRepository<Customer> repo,
            IValidator<CustomerDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();


            // Assuming `existing` is the tracked entity from DbContext
            existing.Name = dto.Name;
            existing.PrimaryEmail = dto.PrimaryEmail;
            existing.PrimaryPhone = dto.PrimaryPhone;
            existing.Website = dto.Website;
            existing.Alias = dto.Alias;
            existing.Status = (Domain.CustomerStatus)dto.Status;
            existing.SlaTier = dto.SlaTier;
            existing.Notes = dto.Notes;

            // Update nested addresses if provided
            if (dto.BillingAddress != null)
            {
                existing.BillingAddress ??= new Address();
                existing.BillingAddress.Line1 = dto.BillingAddress.Line1;
                existing.BillingAddress.Line2 = dto.BillingAddress.Line2;
                existing.BillingAddress.City = dto.BillingAddress.City;
                existing.BillingAddress.StateOrProvince = dto.BillingAddress.StateOrProvince;
                existing.BillingAddress.PostalCode = dto.BillingAddress.PostalCode;
                existing.BillingAddress.Country = dto.BillingAddress.Country;
            }


            // Audit fields
            existing.UpdatedAt = DateTimeOffset.UtcNow;


            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new CustomerDto
            {
                Id = existing.Id,
                Name = existing.Name,
                PrimaryEmail = existing.PrimaryEmail,
                PrimaryPhone = existing.PrimaryPhone,
                Alias = existing.Alias,
                Status = (Shared.CustomerStatus)existing.Status,
                BillingAddress = existing.BillingAddress is null ? null : new AddressDto
                {
                    Line1 = existing.BillingAddress.Line1,
                    Line2 = existing.BillingAddress.Line2,
                    City = existing.BillingAddress.City,
                    StateOrProvince = existing.BillingAddress.StateOrProvince,
                    PostalCode = existing.BillingAddress.PostalCode,
                    Country = existing.BillingAddress.Country
                },
                SlaTier = existing.SlaTier,
                Notes = existing.Notes,
                AssetCount = existing.AssetCount,
                ActiveWorkOrders = existing.ActiveWorkOrders,
                CreatedAt = existing.CreatedAt,
                CreatedBy = existing.CreatedBy,
                UpdatedAt = existing.UpdatedAt,
                UpdatedBy = existing.UpdatedBy
            };


            return Results.Ok(result);
        });

        // DELETE
        group.MapDelete("/{id:guid}", async (Guid id, IRepository<Customer> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            repo.Remove(existing);
            await repo.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
