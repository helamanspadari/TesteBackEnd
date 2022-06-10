using System;
using System.Collections.Generic;
using System.Linq;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Domain.DTO.Curso;
using TesteBack.Domain.Entities.Aluno;
using TesteBack.Repository.Repository.Aluno;
using TesteBack.Service.Service.Curso;
using TesteBack.Service.Service.Disciplina;

namespace TesteBack.Service.Service.Aluno
{
    public class AlunoService : BaseService<AlunoEntities>
    {
        #region ' Variaveis '
        private readonly AlunoRepository _repository;
        private readonly CursoService _cursoService;
        private readonly DisciplinaService _disciplinaService;

        public AlunoService(AlunoRepository repository,
                            CursoService cursoService,
                            DisciplinaService disciplinaService) : base(repository)
        {
            _repository = repository;
            _cursoService = cursoService;
            _disciplinaService = disciplinaService;
        }
        #endregion

        public List<AlunoDTO> ListarAlunos()
        {
            return _repository.ListarAlunos();
        }

        public List<AlunoDTO> ListaFiltrada(FiltroDTO filtroDTO)
        {
            if (filtroDTO.Nome == "string")
                filtroDTO.Nome = string.Empty;

            if (filtroDTO.Status == "string")
                filtroDTO.Status = string.Empty;
            else if (filtroDTO.Status.ToLower() != "reprovado" || filtroDTO.Status.ToLower() != "aprovado")
                throw new Exception("Status invalido");

            return _repository.ListaFiltrada(filtroDTO);
        }

        public AlunoDTO ObterAlunoRA(int ra)
        {
            return _repository.ObterAlunoRA(ra);
        }

        public AlunoDTO InserirAluno(AlunoDTO alunoDTO)
        {
            if (alunoDTO.CursoId.Equals(0))
            {
                alunoDTO.CursoId = null;
                alunoDTO.Curso = null;
            }

            if (alunoDTO.Boletim != null && alunoDTO.Boletim.Count > 0)
                alunoDTO.Boletim = null;

            return _repository.InserirAluno(alunoDTO);
        }

        public AlunoDTO AlterarAluno(AlunoDTO alunoDTO)
        {
            AlunoDTO dbAluno = ObterAlunoRA(alunoDTO.RA);

            if(dbAluno.CursoId > 0)
            {
                alunoDTO.CursoId = dbAluno.CursoId;
                alunoDTO.Curso = dbAluno.Curso;
                alunoDTO.Boletim = dbAluno.Boletim;
            }

            if (alunoDTO.CursoId.Equals(0))
            {
                alunoDTO.CursoId = null;
                alunoDTO.Curso = null;

                if (alunoDTO.Boletim != null && alunoDTO.Boletim.Count > 0)
                    alunoDTO.Boletim = null;
            }

            return _repository.AlterarAluno(alunoDTO);
        }

        public bool RemoverAluno(int ra)
        {
            AlunoDTO dbAluno = ObterAlunoRA(ra);

            if (dbAluno.Boletim.Count > 0)
            {
                dbAluno.Boletim = null;
            }

            if (dbAluno.Curso != null && dbAluno.Curso.Id > 0)
            {
                dbAluno.Curso = null;
            }

            return _repository.RemoverAluno(dbAluno);
        }

        public AlunoDTO CadastrarCurso(AlunoDTO alunoDTO)
        {
            if (alunoDTO.CursoId.Equals(0))
                throw new Exception("Erro");

            return _repository.CadastrarCurso(alunoDTO);
        }

        public AlunoDTO CadastrarNotaDisciplina(BoletimDTO boletimDTO)
        {
            return _repository.CadastrarNotaDisciplina(boletimDTO);
        }
    }
}
