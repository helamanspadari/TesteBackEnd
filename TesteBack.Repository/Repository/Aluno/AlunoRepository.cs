using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Domain.Entities.Aluno;
using TesteBack.Domain.Entities.Disciplina;
using TesteBack.Repository.Context;

namespace TesteBack.Repository.Repository.Aluno
{
    public class AlunoRepository : BaseRepository<AlunoEntities>
    {
        #region ' Variaveis '
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        #endregion

        public List<AlunoDTO> ListarAlunos()
        {
            try
            {
                List<AlunoEntities> lista = _context.Alunos
                    .Include(a => a.Curso)
                    .Include(a => a.Curso.Disciplinas)
                    .Include(a => a.Boletim)
                    .ToList();

                lista.ForEach(l => {
                    List<BoletimEntities> listaAD = _context.Boletims.Where(x => x.AlunoId.Equals(l.Id)).ToList();
                    l.Boletim = listaAD;
                });

                List<AlunoDTO> listaDTO = _mapper.Map<List<AlunoEntities>, List<AlunoDTO>>(lista);

                return listaDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AlunoDTO> ListaFiltrada(FiltroDTO filtroDTO)
        {
            Type tipo = filtroDTO.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(tipo.GetProperties());

            var query = new List<AlunoEntities>();
            bool popula = true;

            foreach(PropertyInfo prop in props)
            {
                switch (prop.Name)
                {
                    case "Nome":
                        if (!string.IsNullOrEmpty(filtroDTO.Nome))
                        {
                            if(query.Count > 0)
                            {
                                popula = false;
                                query = query.Where(x => x.Nome.Contains(filtroDTO.Nome)).ToList();
                            }
                            else
                            {
                                query = _context.Alunos.Where(x => x.Nome.Contains(filtroDTO.Nome))
                                    .Include(x => x.Curso)
                                    .Include(x => x.Curso.Disciplinas)
                                    .Include(x => x.Boletim)
                                    .ToList();
                            }
                        }
                        break;

                    case "RA":
                        if (filtroDTO.RA > 0)
                        {
                            if(query.Count > 0)
                            {
                                popula = false;
                                query = query.Where(x => x.RA.Equals(filtroDTO.RA)).ToList();
                            }
                            else
                            {
                                query = _context.Alunos.Where(x => x.RA == filtroDTO.RA)
                                    .Include(x => x.Curso)
                                    .Include(x => x.Curso.Disciplinas)
                                    .Include(x => x.Boletim)
                                    .ToList();
                            }
                        }
                        break;

                    case "IdCurso":
                        if (filtroDTO.IdCurso > 0)
                        {
                            if(query.Count > 0)
                            {
                                popula = false;
                                query = query.Where(x => x.CursoId.Equals(filtroDTO.IdCurso)).ToList();
                            }
                            else
                            {
                                query = _context.Alunos.Where(x => x.CursoId == filtroDTO.IdCurso)
                                    .Include(x => x.Curso)
                                    .Include(x => x.Curso.Disciplinas)
                                    .Include(x => x.Boletim)
                                    .ToList();
                            }
                        }
                        break;

                    case "Status":
                        if (!string.IsNullOrEmpty(filtroDTO.Status))
                        {
                            if (query.Count > 0)
                            {
                                popula = false;
                                query = query.Where(x => x.Boletim.Any(b => b.StatusAlunoDisciplina.Equals(filtroDTO.Status))).ToList();
                            }
                            else
                            {
                                query = _context.Alunos
                                    .Where(x => x.Boletim.Any(b => b.StatusAlunoDisciplina.Equals(filtroDTO.Status)))
                                    .Include(x => x.Curso)
                                    .Include(x => x.Curso.Disciplinas)
                                    .Include(x => x.Boletim)                        
                                    .AsEnumerable()
                                    .ToList();
                            }
                        }
                        break;

                }
            }

            if(popula)
                query = _mapper.Map<List<AlunoDTO>, List<AlunoEntities>>(ListarAlunos());
            

            query.ForEach(i => {
                i.Boletim = _context.Boletims.Where(x => x.AlunoId == i.Id).ToList();
            });

            List<AlunoDTO> listaDTO = _mapper.Map<List<AlunoEntities>, List<AlunoDTO>>(query);

            return listaDTO;
        }

        public AlunoDTO InserirAluno(AlunoDTO alunoDTO)
        {
            try
            {
                AlunoEntities dbAluno = ObterAlunoRA(alunoDTO.RA);

                if(dbAluno == null)
                {
                    _context.Alunos.Add(alunoDTO);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("RA já existente!");
                }

                return alunoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AlunoDTO AlterarAluno(AlunoDTO alunoDTO)
        {
            try
            {
                _context.Entry(alunoDTO).State = EntityState.Modified;
                _context.SaveChanges();

                return alunoDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoverAluno(AlunoDTO alunoDTO)
        {
            try
            {
                int sucesso = 0;

                if (alunoDTO != null && (alunoDTO.Id > 0 || alunoDTO.RA > 0))
                {
                    _context.Alunos.Remove(alunoDTO);
                    sucesso = _context.SaveChanges();
                }

                return Convert.ToBoolean(sucesso);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AlunoDTO ObterAlunoId(int id)
        {
            try
            {
                AlunoEntities aluno = _context.Alunos
                    .Include(a => a.Curso)
                    .Include(a => a.Curso.Disciplinas)
                    .Include(a => a.Boletim)
                    .Where(a => a.Id == id)
                    .AsNoTracking()
                    .FirstOrDefault();

                AlunoDTO alunoDTO = _mapper.Map<AlunoEntities, AlunoDTO>(aluno);

                return alunoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AlunoDTO ObterAlunoRA(int ra)
        {
            try
            {
                AlunoEntities aluno = _context.Alunos
                    .Include(a => a.Curso)
                    .Include(a => a.Curso.Disciplinas)
                    .Include(a => a.Boletim)
                    .Where(a => a.RA == ra)
                    .AsNoTracking()
                    .FirstOrDefault();
                
                AlunoDTO alunoDTO = _mapper.Map<AlunoEntities, AlunoDTO>(aluno);

                return alunoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AlunoDTO CadastrarCurso(AlunoDTO alunoDTO)
        {
            try
            {
                AlunoDTO dbAluno = ObterAlunoId(alunoDTO.Id);

                dbAluno.CursoId = alunoDTO.CursoId;

                dbAluno.Curso = _context.Cursos
                    .Include(c => c.Disciplinas)
                    .Where(c => c.Id == dbAluno.CursoId)
                    .FirstOrDefault();

                _context.Entry(dbAluno).State = EntityState.Modified;
                _context.SaveChanges();

                return dbAluno;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AlunoDTO CadastrarNotaDisciplina(BoletimDTO boletimDTO)
        {
            try
            {
                AlunoDTO dbAluno = ObterAlunoId(boletimDTO.AlunoId);

                DisciplinaEntities disciplina = dbAluno.Curso.Disciplinas
                    .Where(x => x.Id == boletimDTO.DisciplinaId)
                    .FirstOrDefault();

                BoletimDTO disciplinaBoletim = new BoletimDTO()
                {
                    AlunoId = dbAluno.Id,
                    DisciplinaId = disciplina.Id,
                    NomeDisciplina = disciplina.Nome,
                    NotaAlunoDisciplina = boletimDTO.NotaAlunoDisciplina,
                    StatusAlunoDisciplina = boletimDTO.NotaAlunoDisciplina >= disciplina.NotaMinimaAprovacao ? "Aprovado" : "Reprovado"
                };

                _context.Boletims.Add(disciplinaBoletim);
                _context.SaveChanges();

                dbAluno.Boletim.Add(disciplinaBoletim);

                return dbAluno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
