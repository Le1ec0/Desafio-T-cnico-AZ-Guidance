public class PermissaoClienteService
{
    private readonly AppDbContext _context;

    public PermissaoClienteService(AppDbContext context)
    {
        _context = context;
    }

    // Obtém um cliente pelo ID
    public async Task<PermissaoCliente> GetClienteById(int clienteId)
    {
        return await _context.PermissoesClientes.FindAsync(clienteId);
    }

    // Atualiza as informações de um cliente
    public async Task<bool> UpdateCliente(PermissaoCliente cliente)
    {
        var existingCliente = await _context.PermissoesClientes.FindAsync(cliente.ClienteID);
        if (existingCliente == null)
        {
            return false;
        }

        // Atualiza os campos conforme os dados recebidos
        existingCliente.Permitido = cliente.Permitido;
        existingCliente.TipoEmailID = cliente.TipoEmailID;
        existingCliente.EnviarParaID = cliente.EnviarParaID;
        existingCliente.FormaEnvioRmID = cliente.FormaEnvioRmID;

        await _context.SaveChangesAsync();
        return true;
    }
}
