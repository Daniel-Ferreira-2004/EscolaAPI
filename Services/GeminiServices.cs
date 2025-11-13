using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EscolaAPI.Services
{
    public class GeminiServices
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public GeminiServices(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _httpClient = new HttpClient();
        }

        public async Task<string> PerguntarAsync(string pergunta)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = pergunta }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro Gemini ({response.StatusCode}): {result}");

            using var doc = JsonDocument.Parse(result);
            return doc.RootElement
                      .GetProperty("candidates")[0]
                      .GetProperty("content")
                      .GetProperty("parts")[0]
                      .GetProperty("text")
                      .GetString();
        }
    }
}
