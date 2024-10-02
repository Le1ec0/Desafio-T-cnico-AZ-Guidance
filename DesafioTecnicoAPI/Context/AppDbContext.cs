using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PermissaoCliente> PermissoesClientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mapeia a entidade PermissaoCliente para a tabela Permissao_Cliente
        modelBuilder.Entity<PermissaoCliente>()
            .ToTable("Permissao_Cliente"); // Nome da tabela no banco de dados
    }
}

