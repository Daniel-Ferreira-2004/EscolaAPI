using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace EscolaAPI.Model
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(80)]
        public string? Sobrenome { get; set; }
        [Required]
        [StringLength(80)]
        public string? NomeResponsavel { get; set; }
        [Required]
        [StringLength(80)]
        public string? SobrenomeResponsavel { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Telefone { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int? Idade { get; set; }
        [Required]
        public string? Turma { get; set; }
        [Required]
        public string? Cep { get; set; }
    }
}
