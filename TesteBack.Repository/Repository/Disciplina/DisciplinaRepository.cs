using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Domain.Entities.Disciplina;
using TesteBack.Repository.Context;

namespace TesteBack.Repository.Repository.Disciplina
{
    public class DisciplinaRepository : BaseRepository<DisciplinaEntities>
    {
        #region ' Variaveis '
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public DisciplinaRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        #endregion

        public List<DisciplinaDTO> ListarDisciplinas()
        {
            try
            {
                List<DisciplinaEntities> lista = _context.Disciplinas.ToList();

                List<DisciplinaDTO> listaDTO = _mapper.Map<List<DisciplinaEntities>, List<DisciplinaDTO>>(lista);

                return listaDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DisciplinaDTO InserirDisciplina(DisciplinaDTO disciplinaDTO)
        {
            try
            {
                _context.Disciplinas.Add(disciplinaDTO);
                _context.SaveChanges();

                return disciplinaDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
