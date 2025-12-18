using FluentValidation;
using SM.Shared;
using SM.WebApi.Domain;
using SM.WebApi.Infrastructure;

namespace SM.WebApi.Endpoints;

public static class AuditReportEndpoints
{
    public static void MapAuditReportEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/auditreports")
                          .WithTags("AuditReports");

        // GET all
        group.MapGet("/", async (IRepository<TransformerAuditReport> repo) =>
        {
            var list = await repo.GetAllAsync();
            var result = list.Select(r => new AuditReportDto
            {
                Id = r.Id,
                ReportNumber = r.ReportNumber,
                TransformerId = r.TransformerId,
                CustomerId = r.CustomerId,
                SubstationId = r.SubstationId,
                DateServiced = r.DateServiced,
                Submitted = r.Submitted,
                SubmittedDateTime = r.SubmittedDateTime,
                WindingTemperature = r.WindingTemperature,
                TransformerOilLevelPercent = r.TransformerOilLevelPercent,
                SilicaGelBreatherOk = r.SilicaGelBreatherOk,
                BuchholzRelayOk = r.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = r.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = r.RequiredBdvLevel,
                OilMoistureContentPpm = r.OilMoistureContentPpm
            });
            return Results.Ok(result);
        });

        // GET by id
        group.MapGet("/{id:guid}", async (Guid id, IRepository<TransformerAuditReport> repo) =>
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity is null) return Results.NotFound();

            var dto = new AuditReportDto
            {
                Id = entity.Id,
                ReportNumber = entity.ReportNumber,
                TransformerId = entity.TransformerId,
                CustomerId = entity.CustomerId,
                SubstationId = entity.SubstationId,
                DateServiced = entity.DateServiced,
                Submitted = entity.Submitted,
                SubmittedDateTime = entity.SubmittedDateTime,
                WindingTemperature = entity.WindingTemperature,
                TransformerOilLevelPercent = entity.TransformerOilLevelPercent,
                SilicaGelBreatherOk = entity.SilicaGelBreatherOk,
                BuchholzRelayOk = entity.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = entity.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = entity.RequiredBdvLevel,
                OilMoistureContentPpm = entity.OilMoistureContentPpm
            };
            return Results.Ok(dto);
        });

        // GET by id
        group.MapGet("/byequid/{id:guid}", async (Guid id, IRepository<TransformerAuditReport> repo) =>
        {
            var list = await repo.GetManyAsync(t => t.TransformerId == id);
            var result = list.Select(entity => new AuditReportDto
            {
                Id = entity.Id,
                ReportNumber = entity.ReportNumber,
                TransformerId = entity.TransformerId,
                CustomerId = entity.CustomerId,
                SubstationId = entity.SubstationId,
                DateServiced = entity.DateServiced,
                Submitted = entity.Submitted,
                SubmittedDateTime = entity.SubmittedDateTime,
                WindingTemperature = entity.WindingTemperature,
                TransformerOilLevelPercent = entity.TransformerOilLevelPercent,
                SilicaGelBreatherOk = entity.SilicaGelBreatherOk,
                BuchholzRelayOk = entity.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = entity.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = entity.RequiredBdvLevel,
                OilMoistureContentPpm = entity.OilMoistureContentPpm
            });
            return Results.Ok(result);
        });

        // POST (create)
        group.MapPost("/", async (
            AuditReportDto dto,
            IRepository<TransformerAuditReport> repo,
            IValidator<AuditReportDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var entity = new TransformerAuditReport
            {
                ReportNumber = dto.ReportNumber,
                TransformerId = dto.TransformerId,
                CustomerId = dto.CustomerId,
                SubstationId = dto.SubstationId,
                DateServiced = dto.DateServiced,
                Submitted = dto.Submitted,
                SubmittedDateTime = dto.SubmittedDateTime,
                WindingTemperature = dto.WindingTemperature,
                TransformerOilLevelPercent = dto.TransformerOilLevelPercent,
                SilicaGelBreatherOk = dto.SilicaGelBreatherOk,
                BuchholzRelayOk = dto.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = dto.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = dto.RequiredBdvLevel,
                OilMoistureContentPpm = dto.OilMoistureContentPpm
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            var result = new AuditReportDto
            {
                Id = entity.Id,
                ReportNumber = entity.ReportNumber,
                TransformerId = entity.TransformerId,
                CustomerId = entity.CustomerId,
                SubstationId = entity.SubstationId,
                DateServiced = entity.DateServiced,
                Submitted = entity.Submitted,
                SubmittedDateTime = entity.SubmittedDateTime,
                WindingTemperature = entity.WindingTemperature,
                TransformerOilLevelPercent = entity.TransformerOilLevelPercent,
                SilicaGelBreatherOk = entity.SilicaGelBreatherOk,
                BuchholzRelayOk = entity.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = entity.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = entity.RequiredBdvLevel,
                OilMoistureContentPpm = entity.OilMoistureContentPpm
            };

            return Results.Created($"/auditreports/{entity.Id}", result);
        });

        // PUT - Subvmit
        group.MapPut("/submit/{id:guid}", async (
            Guid id,
            AuditReportDto dto,
            IRepository<TransformerAuditReport> repo,
            IValidator<AuditReportDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(dto.Id);
            if (existing is null) return Results.NotFound();
            existing.Submitted = true;
            existing.SubmittedDateTime = DateTime.UtcNow;
            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new AuditReportDto
            {
                Id = existing.Id,
                ReportNumber = existing.ReportNumber,
                TransformerId = existing.TransformerId,
                CustomerId = existing.CustomerId,
                SubstationId = existing.SubstationId,
                DateServiced = existing.DateServiced,
                Submitted = existing.Submitted,
                SubmittedDateTime = existing.SubmittedDateTime,
                WindingTemperature = existing.WindingTemperature,
                TransformerOilLevelPercent = existing.TransformerOilLevelPercent,
                SilicaGelBreatherOk = existing.SilicaGelBreatherOk,
                BuchholzRelayOk = existing.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = existing.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = existing.RequiredBdvLevel,
                OilMoistureContentPpm = existing.OilMoistureContentPpm
            };

            return Results.Ok(result);
        });

        // PUT (update)
        group.MapPut("/{id:guid}", async (
            Guid id,
            AuditReportDto dto,
            IRepository<TransformerAuditReport> repo,
            IValidator<AuditReportDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            existing.ReportNumber = dto.ReportNumber;
            existing.TransformerId = dto.TransformerId;
            existing.CustomerId = dto.CustomerId;
            existing.SubstationId = dto.SubstationId;
            existing.DateServiced = dto.DateServiced;
            existing.WindingTemperature = dto.WindingTemperature;
            existing.TransformerOilLevelPercent = dto.TransformerOilLevelPercent;
            existing.SilicaGelBreatherOk = dto.SilicaGelBreatherOk;
            existing.BuchholzRelayOk = dto.BuchholzRelayOk;
            existing.OilDielectricBreakdownVoltage = dto.OilDielectricBreakdownVoltage;
            existing.RequiredBdvLevel = dto.RequiredBdvLevel;
            existing.OilMoistureContentPpm = dto.OilMoistureContentPpm;

            repo.Update(existing);
            await repo.SaveChangesAsync();

            var result = new AuditReportDto
            {
                Id = existing.Id,
                ReportNumber = existing.ReportNumber,
                TransformerId = existing.TransformerId,
                CustomerId = existing.CustomerId,
                SubstationId = existing.SubstationId,
                DateServiced = existing.DateServiced,
                Submitted = existing.Submitted,
                SubmittedDateTime = existing.SubmittedDateTime,
                WindingTemperature = existing.WindingTemperature,
                TransformerOilLevelPercent = existing.TransformerOilLevelPercent,
                SilicaGelBreatherOk = existing.SilicaGelBreatherOk,
                BuchholzRelayOk = existing.BuchholzRelayOk,
                OilDielectricBreakdownVoltage = existing.OilDielectricBreakdownVoltage,
                RequiredBdvLevel = existing.RequiredBdvLevel,
                OilMoistureContentPpm = existing.OilMoistureContentPpm
            };

            return Results.Ok(result);
        });

        // DELETE
        group.MapDelete("/{id:guid}", async (Guid id, IRepository<TransformerAuditReport> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();

            repo.Remove(existing);
            await repo.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
