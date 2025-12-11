using EscolaAPI.DTO;
using EscolaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EscolaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly GeminiServices _gemini;

        public ChatController(GeminiServices gemini)
        {
            _gemini = gemini;
        }

        [HttpPost]
        public async Task<IActionResult> Perguntar([FromBody] GeminiRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Pergunta))
                return BadRequest("Pergunta não pode ser vazia.");

            var resposta = await _gemini.Perguntar(dto.Pergunta);

            // Substitui \n por quebras de linha reais
            resposta = resposta.Replace("\\n", Environment.NewLine);

            return Ok(new GeminiResponseDTO
            {
                Resposta = resposta
            });
        }
    }
}
