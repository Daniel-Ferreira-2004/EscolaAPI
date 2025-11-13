using EscolaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiServices _geminiService;

        public GeminiController(GeminiServices geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("perguntar")]
        public async Task<IActionResult> Perguntar([FromBody] string pergunta)
        {
            var resposta = await _geminiService.PerguntarAsync(pergunta);

            // Remove caracteres de formatação Markdown e quebras de linha
            resposta = resposta
                .Replace("\\n", "\n")                // quebra real
                .Replace("\n", " ")                  // tudo em uma linha
                .Replace("**", "")                   // negrito markdown
                .Replace("###", "")                  // título markdown
                .Replace("*", "");                   // bullet simples

            return Ok(new { resposta });
        }
    }
}
