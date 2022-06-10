using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TesteBack.Domain.Entities.Aluno;
using TesteBack.Domain.Entities.Curso;

namespace TesteBack.Domain.Entities.Disciplina
{
    public class DisciplinaEntities : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal NotaMinimaAprovacao { get; set; }

        public int? CursoId { get; set; }
        [JsonIgnore]
        public CursoEntities? Curso { get; set; }

        [JsonIgnore]
        public List<BoletimEntities>? Boletim { get; set; }
    }
}
