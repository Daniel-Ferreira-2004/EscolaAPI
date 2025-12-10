using EscolaAPI.Data;
using EscolaAPI.Model;
using EscolaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.DTO;

namespace EscolaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ViaCepServices _viaCepServices;

        public AlunosController(AppDbContext context, ViaCepServices viaCepServices)
        {
            _context = context;
            _viaCepServices = viaCepServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlunos()
        {

            var alunos = _context.Alunos.Include(a => a.Endereco).ToListAsync();

            if (alunos == null)
            {
                return NotFound("Nenhum aluno encontrado.");
            }
            return Ok(await alunos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAluno(int id)
        {
            var aluno = _context.Alunos.Include(a => a.Endereco).FirstOrDefault(a => a.Id == id);
            if (aluno is null)
            {
                return NotFound("Nenhum aluno com essa ID encontrada");
            }
            return Ok(aluno);
        }

        // Preenche os dados vindos da API
        [HttpPost]
        public async Task<IActionResult> CreateAluno([FromBody] EnderecoDTO Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var endereco = await _viaCepServices.GetEndereco(Dto.Cep!);
            if (endereco is null)
                return BadRequest("CEP inválido ou não encontrado.");
            var aluno = new Aluno
            {
                Nome = Dto.Nome,
                Sobrenome = Dto.Sobrenome,
                NomeResponsavel = Dto.NomeResponsavel,
                SobrenomeResponsavel = Dto.SobrenomeResponsavel,
                Telefone = Dto.Telefone,
                Email = Dto.Email,
                Idade = Dto.Idade,
                Turma = Dto.Turma,
                Cep = Dto.Cep,
                Endereco = endereco
            };

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAluno(int id, [FromBody] EnderecoDTO aluno)
        {
            var alunoUpdate = await _context.Alunos
                .Include(a => a.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alunoUpdate is null)
                return NotFound("Aluno não encontrado.");

            alunoUpdate.Nome = aluno.Nome;
            alunoUpdate.Sobrenome = aluno.Sobrenome;
            alunoUpdate.NomeResponsavel = aluno.NomeResponsavel;
            alunoUpdate.SobrenomeResponsavel = aluno.SobrenomeResponsavel;
            alunoUpdate.Telefone = aluno.Telefone;
            alunoUpdate.Email = aluno.Email;
            alunoUpdate.Idade = aluno.Idade;
            alunoUpdate.Turma = aluno.Turma;

            var novoCep = new string(aluno.Cep.Where(char.IsDigit).ToArray());

            if (novoCep != alunoUpdate.Cep)
            {
                var enderecoNovo = await _viaCepServices.GetEndereco(novoCep);

                if (enderecoNovo == null)
                    return BadRequest("CEP inválido.");

                // Remove endereço antigo
                if (alunoUpdate.Endereco != null)
                    _context.Enderecos.Remove(alunoUpdate.Endereco);

                alunoUpdate.Cep = novoCep;
                alunoUpdate.Endereco = enderecoNovo;
            }

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