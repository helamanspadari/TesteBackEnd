using System.Collections.Generic;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Domain.Entities.Disciplina;
using TesteBack.Repository.Repository.Disciplina;

namespace TesteBack.Service.Service.Disciplina
{
    public class DisciplinaService : BaseService<DisciplinaEntities>
    {
        #region ' Variaveis '
        private readonly DisciplinaRepository _repository;

        public DisciplinaService(DisciplinaRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion

        public List<DisciplinaDTO> ListarDisciplinas()
        {
            return _repository.ListarDisciplinas();
        }

        public DisciplinaDTO InserirDisciplina(DisciplinaDTO disciplinaDTO)
        {
            if (disciplinaDTO.CursoId.Equals(0))
                disciplinaDTO.CursoId = null;

            return _repository.InserirDisciplina(disciplinaDTO);
        }
    }
}
