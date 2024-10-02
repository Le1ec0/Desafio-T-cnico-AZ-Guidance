using DesafioTecnicoAZGuidance.Models;
using Microsoft.EntityFrameworkCore;

public class PermissaoClienteService
{
    private readonly AppDbContext _context;

    public PermissaoClienteService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ClienteDto> GetClienteById(int clienteId)
    {
        var cliente = await _context.PermissoesClientes
            .Include(c => c.TipoEmail) // Inclui a tabela PermissaoTipo
            .Include(c => c.EnviarPara) // Inclui a tabela PermissaoEnviarPara
            .Include(c => c.FormaEnvio) // Inclui a tabela PermissaoFormaEnvio
            .FirstOrDefaultAsync(c => c.ClienteID == clienteId);

        if (cliente == null) return null;

        return new ClienteDto
        {
            ClienteID = cliente.ClienteID,
            Permitido = cliente.Permitido ? "Sim" : "Não",
            TipoEmailDescricao = cliente.TipoEmail?.Descricao, // Aqui é onde pegamos a descrição
            EnviarParaDescricao = cliente.EnviarPara?.Descricao, // Aqui pegamos a descrição
            FormaEnvioDescricao = cliente.FormaEnvio?.Descricao // Aqui pegamos a descrição
        };
    }

    // Atualiza as informações de um cliente
    public async Task<bool> UpdateCliente(ClienteDto clienteDto)
    {
        var existingCliente = await _context.PermissoesClientes.FindAsync(clienteDto.ClienteID);
        if (existingCliente == null)
        {
            return false;
        }

        // Atualiza os campos conforme os dados recebidos
        existingCliente.Permitido = clienteDto.Permitido == "Sim"; // Converte de string para bool
        existingCliente.TipoEmailID = existingCliente.TipoEmailID; // Preserva o ID, pois não se está mudando
        existingCliente.EnviarParaID = existingCliente.EnviarParaID; // Preserva o ID, pois não se está mudando
        existingCliente.FormaEnvioRmID = existingCliente.FormaEnvioRmID; // Preserva o ID, pois não se está mudando

        await _context.SaveChangesAsync();
        return true;
    }

    internal async Task<bool> UpdateCliente(PermissaoCliente permissaoCliente)
    {
        throw new NotImplementedException();
    }
}

// DTO para representar o cliente com descrições
public class ClienteDto
{
    public int ClienteID { get; set; }
    public string Permitido { get; set; }
    public string TipoEmailDescricao { get; set; }
    public string EnviarParaDescricao { get; set; }
    public string FormaEnvioDescricao { get; set; }
}
