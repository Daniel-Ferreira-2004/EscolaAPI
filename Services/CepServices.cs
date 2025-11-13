using System.Text.Json;

namespace EscolaAPI.Services
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object?> BuscarEnderecoPorCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return null;

            cep = cep.Replace("-", "").Trim();

            // chamamos ViaCEP (ex: https://viacep.com.br/ws/01001000/json/)
            var res = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (!res.IsSuccessStatusCode) return null;

            var json = await res.Content.ReadAsStringAsync();

            // Se ViaCEP retornar {"erro": true} interpretamos como não encontrado
            if (json.Contains("\"erro\": true")) return null;

            var endereco = JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return endereco;
        }
    }
}
