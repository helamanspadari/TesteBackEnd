using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TesteBack.Domain.DTO.Aluno;
using TesteBack.Domain.DTO.Curso;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Service.Service.Curso;

namespace TesteBack.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        [HttpGet("ListarCursos")]
        public List<CursoDTO> ListarCursos([FromServices] CursoService _service)
        {
            return _service.ListarCursos();
        }

        [HttpPost("ObterCurso")]
        public CursoDTO ObterCurso([FromServices] CursoService _service, int id)
        {
            return _service.ObterCurso(id);
        }

        [HttpPost("InserirCurso")]
        public CursoDTO InserirCurso([FromServices] CursoService _service, CursoDTO cursoDTO)
        {
            return _service.InserirCurso(cursoDTO);
        }

        [HttpPost("CadastrarDisciplina")]
        public CursoDTO CadastrarDisciplinas([FromServices] CursoService _service, List<DisciplinaDTO> listaDisciplinas)
        {
            return _service.CadastrarDisciplinas(listaDisciplinas);
        }
    }
}
