using FluentValidation;
using SM.WebApi.Contracts;
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
                Email = c.Email
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:int}", async (int id, IRepository<Customer> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();

            var dto = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
            return Results.Ok(dto);
        });

        // POST (create)
        group.MapPost("/", async (
            CustomerCreateDto dto,
            IRepository<Customer> repo,
            IValidator<CustomerCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var entity = new Customer
            {
                Name = dto.Name,
                Email = dto.Email
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };

            return Results.Created($"/customers/{entity.Id}", result);
        });

        // PUT (update)
        group.MapPut("/{id:int}", async (
            int id,
            CustomerCreateDto dto,
            IRepository<Customer> repo,
            IValidator<CustomerCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            existing.Name = dto.Name;
            existing.Email = dto.Email;

            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new CustomerDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Email = existing.Email
            };

            return Results.Ok(result);
        });

        // DELETE
        group.MapDelete("/{id:int}", async (int id, IRepository<Customer> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            repo.Remove(existing);
            await repo.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
