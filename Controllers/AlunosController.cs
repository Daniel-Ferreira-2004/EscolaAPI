using EscolaAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;


        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlunos()
        {
            var alunos = _context.Alunos.ToList();
            if (alunos == null)
            {
                return NotFound("Nenhum aluno encontrado.");
            }
            return Ok(alunos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAluno(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno is null)
            {
                return NotFound("Nenhum aluno com essa ID encontrada");
            }
            return Ok(aluno);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> CreateAluno([FromBody] Model.Aluno aluno)
        {
            if (aluno is null)
            {
                return BadRequest("Dados inválidos.");
            }
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAluno(int id, [FromBody] Model.Aluno aluno)
        {
            var alunoUpdate = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (alunoUpdate is null)
            {
                return NotFound("Aluno não encontrado.");
            }
            _context.Entry(alunoUpdate).State = EntityState.Modified;
            _context.Entry(alunoUpdate).CurrentValues.SetValues(aluno);
            await _context.SaveChangesAsync();
            return Ok(alunoUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var alunoDelete = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (alunoDelete is null)
            {
                return NotFound("Aluno não encontrado.");
            }
            _context.Entry(alunoDelete).State = EntityState.Modified;
            _context.Alunos.Remove(alunoDelete);
            await _context.SaveChangesAsync();
            return Ok(alunoDelete);
        }
    }
}