using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBack.Domain.DTO.Disciplina;
using TesteBack.Service.Service.Disciplina;

namespace TesteBack.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {

        [HttpGet("ListarDisciplinas")]
        public List<DisciplinaDTO> ListarDisciplinas([FromServices] DisciplinaService _service)
        {
            return _service.ListarDisciplinas();
        }


        [HttpPost("InserirDisciplina")]
        public DisciplinaDTO InserirDisciplina([FromServices] DisciplinaService _service, DisciplinaDTO disciplinaDTO)
        {
            return _service.InserirDisciplina(disciplinaDTO);
        }
    }
}
