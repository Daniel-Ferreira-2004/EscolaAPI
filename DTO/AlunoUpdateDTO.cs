using System.ComponentModel.DataAnnotations;

namespace EscolaAPI.DTO
{
    public class AlunoUpdateDTO
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? NomeResponsavel { get; set; }
        public string? SobrenomeResponsavel { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public int? Idade { get; set; }
        public string? Turma { get; set; }
        [Required]
        public string ? Cep { get; set; }
    }
}