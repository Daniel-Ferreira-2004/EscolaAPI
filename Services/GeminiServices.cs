using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace EscolaAPI.Services
{
    public class GeminiServices
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public GeminiServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeminiApiKey"]; // Corrigido
        }

        public async Task<string> Perguntar(string prompt)
        {
            int tentativas = 0;
            while (tentativas < 3)
            {
                try
                {
                    var url = $"v1/models/gemini-2.5-flash:generateContent?key={_apiKey}";
                    var requestBody = new
                    {
                        contents = new[]
                        {
                    new { parts = new[] { new { text = prompt } } }
                }
                    };
                    var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    var respostaJson = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(respostaJson);

                    return doc.RootElement.GetProperty("candidates")[0]
                                  .GetProperty("content")
                                  .GetProperty("parts")[0]
                                  .GetProperty("text")
                                  .GetString();
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("503"))
                {
                    tentativas++;
                    await Task.Delay(2000); // espera 2 segundos antes de tentar de novo
                }
                catch (Exception ex)
                {
                    return $"Erro inesperado: {ex.Message}";
                }
            }

            return "Erro: serviço temporariamente indisponível. Tente novamente mais tarde.";
        }
    }
}
