using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Service.Service.Aluno;

namespace TesteBack.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        [HttpGet("ListarAlunos")]
        public List<AlunoDTO> ListarAlunos([FromServices] AlunoService _service)
        {
            return _service.ListarAlunos();
        }

        [HttpPost("ListaFiltrada")]
        public List<AlunoDTO> ListaFiltrada([FromServices] AlunoService _service, FiltroDTO filtroDTO)
        {
            return _service.ListaFiltrada(filtroDTO);
        }

        [HttpPost("ObterAlunoRA")]
        public AlunoDTO ObterAlunoRA([FromServices] AlunoService _service, int ra)
        {
            return _service.ObterAlunoRA(ra);
        }

        [HttpPost("InserirAluno")]
        public AlunoDTO InserirAluno([FromServices] AlunoService _service, AlunoDTO alunoDTO)
        {
            return _service.InserirAluno(alunoDTO);
        }

        [HttpPut("AlterarAluno")]
        public AlunoDTO AlterarAluno([FromServices] AlunoService _service, AlunoDTO alunoDTO)
        {
            return _service.AlterarAluno(alunoDTO);
        }

        [HttpDelete("RemoverAluno")]
        public bool RemoverAluno([FromServices] AlunoService _service, int ra)
        {
            return _service.RemoverAluno(ra);
        }

        [HttpPost("CadastrarCurso")]
        public AlunoDTO CadastrarCurso([FromServices] AlunoService _service, AlunoDTO alunoDTO)
        {
            return _service.CadastrarCurso(alunoDTO);
        }

        [HttpPost("CadastrarNotaDisciplina")]
        public AlunoDTO CadastrarNotaDisciplina([FromServices] AlunoService _service, BoletimDTO boletimDTO)
        {
            return _service.CadastrarNotaDisciplina(boletimDTO);
        }
    }
}
