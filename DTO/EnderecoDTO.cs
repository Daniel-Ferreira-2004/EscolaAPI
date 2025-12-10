using System.ComponentModel.DataAnnotations;

namespace EscolaAPI.DTO
{
    public class EnderecoDTO
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
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter somente 8 números.")]
        public string? Cep { get; set; } = null!;
    }
}
