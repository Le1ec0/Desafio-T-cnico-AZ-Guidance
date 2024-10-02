using DesafioTecnicoAZGuidance.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PermissaoCliente> PermissoesClientes { get; set; }
    public DbSet<PermissaoTipo> PermissaoTipos { get; set; }
    public DbSet<PermissaoEnviarPara> PermissaoEnviarPara { get; set; }
    public DbSet<PermissaoFormaEnvio> PermissaoFormaEnvio { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mapeia a entidade PermissaoCliente para a tabela Permissao_Cliente
        modelBuilder.Entity<PermissaoCliente>()
            .ToTable("Permissao_Cliente");

        // Mapeia a entidade PermissaoTipo para a tabela Permissão_Tipo
        modelBuilder.Entity<PermissaoTipo>()
            .ToTable("Permissao_Tipo");

        // Mapeia a entidade PermissaoEnviarPara para a tabela Permissão_Enviar_Para
        modelBuilder.Entity<PermissaoEnviarPara>()
            .ToTable("Permissao_Enviar_Para");

        // Mapeia a entidade PermissaoFormaEnvio para a tabela Permissão_Forma_Envio
        modelBuilder.Entity<PermissaoFormaEnvio>()
            .ToTable("Permissao_Forma_Envio");
    }
}
