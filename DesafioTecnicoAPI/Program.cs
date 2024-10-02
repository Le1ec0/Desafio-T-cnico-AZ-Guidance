using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar conexão com o banco de dados (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o serviço PermissaoClienteService
builder.Services.AddScoped<PermissaoClienteService>();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
