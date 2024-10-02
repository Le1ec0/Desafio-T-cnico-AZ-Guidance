using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PermissaoCliente API",
        Description = "API to manage client permissions"
    });
});

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

// Configurar a conexão com o banco de dados (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o PermissaoClienteService
builder.Services.AddScoped<PermissaoClienteService>();

var app = builder.Build();

// Usar a política CORS
app.UseCors("AllowAllOrigins"); // Adicione esta linha

// Habilitar Swagger middleware apenas no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PermissaoCliente API V1");
        c.RoutePrefix = string.Empty; // Swagger estará na URL raiz
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Endpoint para obter informações de um cliente pelo ID.
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

// Endpoint para atualizar informações de um cliente.
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
