using Microsoft.AspNetCore.Mvc;

namespace EscolaAPI.Model
{
    public class Endereco 
    {
        public int Id { get; set; }
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Localidade { get; set; }
        public string? Uf { get; set; }

    }
}
