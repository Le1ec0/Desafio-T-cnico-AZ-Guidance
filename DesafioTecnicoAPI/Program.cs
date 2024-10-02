using DesafioTecnicoAZGuidance.Models;
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

// Adicionar serviço de autorização
builder.Services.AddAuthorization(); // Adicione esta linha

var app = builder.Build();

// Usar a política CORS
app.UseCors("AllowAllOrigins");

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

//app.UseHttpsRedirection();
app.UseRouting();

// Adicionar middleware de autorização
app.UseAuthorization();


// Endpoint para obter informações de um cliente pelo ID.
app.MapGet("/api/permissao_cliente/{id}", async (int id, PermissaoClienteService service) =>
{
    var clienteDto = await service.GetClienteById(id);

    if (clienteDto == null)
    {
        return Results.NotFound($"Cliente com ID {id} não encontrado.");
    }

    return Results.Ok(clienteDto); // Retorna o DTO, não o modelo original
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

    // Atualize as informações do cliente aqui, se necessário
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
