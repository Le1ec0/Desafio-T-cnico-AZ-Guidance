using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PermissaoCliente API",
        Description = "API to manage client permissions"
    });
});

// Configure the database connection (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the PermissaoClienteService
builder.Services.AddScoped<PermissaoClienteService>();

var app = builder.Build();

// Enable Swagger middleware for development only
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PermissaoCliente API V1");
        c.RoutePrefix = string.Empty; // Swagger will be at the root URL
    });
}

app.UseHttpsRedirection();

// Endpoint to get client permission by ID
app.MapGet("/api/permissao_cliente/{id}", async (int id, PermissaoClienteService service) =>
{
    var cliente = await service.GetClienteById(id);
    if (cliente == null)
    {
        return Results.NotFound($"Cliente com ID {id} não encontrado.");
    }
    return Results.Ok(cliente);
})
.WithName("GetPermissaoCliente")
.WithOpenApi();

// Endpoint to update client permission
app.MapPut("/api/permissao_cliente/{id}", async (int id, PermissaoCliente permissaoCliente, PermissaoClienteService service) =>
{
    if (id != permissaoCliente.ClienteID)
    {
        return Results.BadRequest("ID do cliente não corresponde ao enviado.");
    }

    var updated = await service.UpdateCliente(permissaoCliente);
    if (!updated)
    {
        return Results.NotFound($"Cliente com ID {id} não encontrado.");
    }

    return Results.NoContent();
})
.WithName("UpdatePermissaoCliente")
.WithOpenApi();

app.Run();
