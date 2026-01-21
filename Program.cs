using GetechnologiesMx.Application.Services;
using GetechnologiesMx.Application.UseCases;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;
using GetechnologiesMx.Infrastructure.Persistence;
using GetechnologiesMx.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Repositories (Port Out -> Adapter)
builder.Services.AddScoped<PersonaRepositoryPort, PersonaRepositoryAdapter>();
builder.Services.AddScoped<FacturaRepositoryPort, FacturaRepositoryAdapter>();

// Persona Use Cases (Port In -> Implementation)
builder.Services.AddScoped<StorePersonaUseCase, StorePersonaUseCaseImpl>();
builder.Services.AddScoped<RetrievePersonaUseCase, RetrievePersonaUseCaseImpl>();
builder.Services.AddScoped<DeletePersonaUseCase, DeletePersonaUseCaseImpl>();

// Factura Use Cases (Port In -> Implementation)
builder.Services.AddScoped<StoreFacturaUseCase, StoreFacturaUseCaseImpl>();
builder.Services.AddScoped<RetrieveFacturaUseCase, RetrieveFacturaUseCaseImpl>();
builder.Services.AddScoped<DeleteFacturaUseCase, DeleteFacturaUseCaseImpl>();

// Services (Application Layer)
builder.Services.AddScoped<DirectorioService>();
builder.Services.AddScoped<VentaService>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crear la base de datos y las tablas si no existen
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Eliminar y recrear la base de datos (solo para desarrollo)
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
