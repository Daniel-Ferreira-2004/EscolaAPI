using EscolaAPI.Model;

namespace EscolaAPI.Services
{
    public class ViaCepServices
    {
        private readonly HttpClient _Client;
        public ViaCepServices(HttpClient client)
        {
            _Client = client;
        }

        public async Task<Endereco> GetEndereco(string cep)
        {
            var response = await _Client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (!response.IsSuccessStatusCode)
                return null;
            var endereco = await response.Content.ReadFromJsonAsync<Endereco>();
            return endereco;
        }
    }
}
