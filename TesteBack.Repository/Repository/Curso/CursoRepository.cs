using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Domain.DTO.Curso;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Domain.Entities.Curso;
using TesteBack.Domain.Entities.Disciplina;
using TesteBack.Repository.Context;

namespace TesteBack.Repository.Repository.Curso
{
    public class CursoRepository : BaseRepository<CursoEntities>
    {
        #region ' Variaveis '
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CursoRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        #endregion




        public List<CursoDTO> ListarCursos()
        {
            try
            {
                List<CursoEntities> lista = _context.Cursos
                    .Include(c => c.Disciplinas)
                    .Include(c => c.Alunos)
                    .ToList();

                List<CursoDTO> listaDTO = _mapper.Map<List<CursoEntities>, List<CursoDTO>>(lista);

                return listaDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }

        public CursoDTO ObterCurso(int id)
        {
            try
            {
                CursoEntities curso = (CursoEntities)_context.Cursos
                    .Include(c => c.Disciplinas)
                    .Include(c => c.Alunos)
                    .Where(c => c.Id.Equals(id))
                    .FirstOrDefault();

                CursoDTO cursoDTO = _mapper.Map<CursoEntities, CursoDTO>(curso);

                return cursoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CursoDTO InserirCurso(CursoDTO cursoDTO)
        {
            try
            {
                _context.Cursos.Add(cursoDTO);
                _context.SaveChanges();

                return cursoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }

        public CursoDTO CadastrarDisciplinas(List<DisciplinaDTO> listaDisciplinas)
        {
            try
            {
                int idCurso = (int)listaDisciplinas.First().CursoId;

                CursoEntities curso = _context.Cursos.Find(idCurso);

                if (curso != null && curso.Id > 0)
                {
                    curso.Disciplinas = new List<DisciplinaEntities>();
                    curso.Disciplinas.AddRange(listaDisciplinas);

                    _context.SaveChanges();
                }

                CursoDTO cursoDTO = _mapper.Map<CursoEntities, CursoDTO>(curso);

                return cursoDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
