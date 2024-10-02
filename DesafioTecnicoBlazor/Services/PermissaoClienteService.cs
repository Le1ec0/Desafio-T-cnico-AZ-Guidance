using System.Net.Http.Json;
using DesafioTecnicoBlazor.Models;

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
            // Obtém o cliente a partir da API
            var cliente = await _httpClient.GetFromJsonAsync<PermissaoCliente>($"api/permissao_cliente/{clienteId}");

            // Se o cliente foi encontrado, busca as descrições correspondentes
            if (cliente != null)
            {
                cliente.TipoEmailDescricao = await GetTipoEmailDescricao(cliente.TipoEmailID);
                cliente.EnviarParaDescricao = await GetEnviarParaDescricao(cliente.EnviarParaID);
                cliente.FormaEnvioDescricao = await GetFormaEnvioDescricao(cliente.FormaEnvioRmID);
            }

            return cliente;
        }

        public async Task UpdateCliente(PermissaoCliente cliente)
        {
            // Atualiza as informações do cliente na API
            await _httpClient.PutAsJsonAsync($"api/permissao_cliente/{cliente.ClienteID}", cliente);
        }

        // Métodos auxiliares para obter as descrições
        private async Task<string> GetTipoEmailDescricao(int tipoEmailId)
        {
            // Chamada para a API ou lógica para obter a descrição do tipo de e-mail
            var tipoEmail = await _httpClient.GetStringAsync($"api/tipo_email/{tipoEmailId}");
            return tipoEmail; // Certifique-se de que isso retorna a descrição correta
        }

        private async Task<string> GetEnviarParaDescricao(int enviarParaId)
        {
            // Chamada para a API ou lógica para obter a descrição de "Enviar Para"
            var enviarPara = await _httpClient.GetStringAsync($"api/enviar_para/{enviarParaId}");
            return enviarPara; // Certifique-se de que isso retorna a descrição correta
        }

        private async Task<string> GetFormaEnvioDescricao(int formaEnvioId)
        {
            // Chamada para a API ou lógica para obter a descrição da forma de envio
            var formaEnvio = await _httpClient.GetStringAsync($"api/forma_envio/{formaEnvioId}");
            return formaEnvio; // Certifique-se de que isso retorna a descrição correta
        }
    }
}
