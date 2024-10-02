using System.Net.Http.Json;

namespace DesafioTecnicoBlazor.Services
{
    public class PermissaoClienteService
    {
        private readonly HttpClient _httpClient;

        public PermissaoClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PermissaoCliente> GetClienteById(int clienteId)
        {
            return await _httpClient.GetFromJsonAsync<PermissaoCliente>($"api/permissao_cliente/{clienteId}");
        }

        public async Task UpdateCliente(PermissaoCliente cliente)
        {
            await _httpClient.PutAsJsonAsync($"api/permissao_cliente/{cliente.ClienteID}", cliente);
        }
    }

}
