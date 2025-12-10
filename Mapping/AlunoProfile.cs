using EscolaAPI.DTO;
using EscolaAPI.Model;

namespace EscolaAPI.Mapping
{
    public class AlunoProfile : AutoMapper.Profile
    {
        public AlunoProfile()
        {
            //POST
            CreateMap<EnderecoDTO, Aluno>();

            //PUT
            CreateMap<AlunoUpdateDTO, Aluno>();
        }
    }
}
