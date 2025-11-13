using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Data;
using EscolaAPI.Model;
using EscolaAPI.Services;

namespace EscolaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly CepService _cepService;

        public AlunosController(AppDbContext context, CepService cepService)
        {
            _context = context;
            _cepService = cepService;
        }

        // POST: api/Alunos
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno([FromBody] Aluno aluno)
        {
            if (aluno == null) return BadRequest(new { mensagem = "Dados inválidos" });

            // Não envie 'Id' no JSON; DB deverá gerar o Id
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }

        // GET: api/Alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAlunos()
        {
            var alunos = await _context.Alunos.AsNoTracking().ToListAsync();
            if (alunos == null || alunos.Count == 0) return NotFound("Nenhum aluno encontrado");

            var resultado = new List<object>();

            foreach (var aluno in alunos)
            {
                object? endereco = null;
                if (!string.IsNullOrWhiteSpace(aluno.Cep))
                {
                    endereco = await _cepService.BuscarEnderecoPorCepAsync(aluno.Cep);
                }

                resultado.Add(new
                {
                    aluno.Id,
                    aluno.Nome,
                    aluno.Sobrenome,
                    aluno.NomeResponsavel,
                    aluno.SobrenomeResponsavel,
                    aluno.Telefone,
                    aluno.Email,
                    aluno.Idade,
                    aluno.Turma,
                    aluno.Cep,
                    Endereco = endereco // null se não encontrado
                });
            }

            return Ok(resultado);
        }

        // GET: api/Alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (aluno == null) return NotFound(new { mensagem = "Aluno não encontrado" });

            object? endereco = null;
            if (!string.IsNullOrWhiteSpace(aluno.Cep))
                endereco = await _cepService.BuscarEnderecoPorCepAsync(aluno.Cep);

            return Ok(new
            {
                aluno.Id,
                aluno.Nome,
                aluno.Sobrenome,
                aluno.NomeResponsavel,
                aluno.SobrenomeResponsavel,
                aluno.Telefone,
                aluno.Email,
                aluno.Idade,
                aluno.Turma,
                aluno.Cep,
                Endereco = endereco
            });
        }

        // PUT and DELETE 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id) return BadRequest(new { mensagem = "ID do aluno não corresponde" });

            var existeAluno = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (existeAluno == null) return NotFound(new { mensagem = "Aluno não encontrado" });

            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { mensagem = "Aluno atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound(new { mensagem = "Aluno não encontrado" });

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Aluno excluído com sucesso", aluno });
        }
    }
}
