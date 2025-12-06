using FluentValidation;
using FluentValidation.AspNetCore;
using SM.WebApi.Contracts;
using SM.WebApi.Infrastructure;
using SM.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using SM.WebApi.Validators;
using SM.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7109") // your Blazor WASM origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<CustomerCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AssetTypeCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubstationCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransformerCreateValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register endpoint groups
app.MapCustomerEndpoints();
app.MapSubstationEndpoints();
app.MapAssetTypeEndpoints();
app.MapTransformerEndpoints();
app.MapAuditReportEndpoints();

app.Run();