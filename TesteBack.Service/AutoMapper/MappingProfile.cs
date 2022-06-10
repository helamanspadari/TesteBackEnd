using AutoMapper;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Domain.DTO.Curso;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Domain.Entities.Aluno;
using TesteBack.Domain.Entities.Curso;
using TesteBack.Domain.Entities.Disciplina;

namespace TesteBack.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AlunoEntities, AlunoDTO>();
            CreateMap<CursoEntities, CursoDTO>();
            CreateMap<DisciplinaEntities, DisciplinaDTO>();
            CreateMap<BoletimEntities, BoletimDTO>();
        }

    }
}
