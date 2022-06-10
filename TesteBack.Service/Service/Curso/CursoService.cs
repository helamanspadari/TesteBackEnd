using System.Collections.Generic;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Domain.DTO.Curso;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Domain.Entities.Curso;
using TesteBack.Repository.Repository.Curso;
using TesteBack.Service.Service.Disciplina;

namespace TesteBack.Service.Service.Curso
{
    public class CursoService : BaseService<CursoEntities>
    {
        #region ' Variaveis '
        private readonly CursoRepository _repository;
        private readonly DisciplinaService _disciplinaService;

        public CursoService(CursoRepository repository,
                            DisciplinaService disciplinaService) : base(repository)
        {
            _repository = repository;
            _disciplinaService = disciplinaService;
        }
        #endregion

        public List<CursoDTO> ListarCursos()
        {
            return _repository.ListarCursos();
        }

        public CursoDTO ObterCurso(int id)
        {
            return _repository.ObterCurso(id);
        }

        public CursoDTO InserirCurso(CursoDTO cursoDTO)
        {
            if (cursoDTO.Disciplinas.Count > 0)
                cursoDTO.Disciplinas = null;

            return _repository.InserirCurso(cursoDTO);
        }

        public CursoDTO CadastrarDisciplinas(List<DisciplinaDTO> listaDTO)
        {
            return _repository.CadastrarDisciplinas(listaDTO);
        }
    }
}
