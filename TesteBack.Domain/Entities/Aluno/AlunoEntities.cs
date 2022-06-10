using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TesteBack.Domain.Entities.Curso;

namespace TesteBack.Domain.Entities.Aluno
{
    public class AlunoEntities : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int RA { get; set; }
        public string Nome { get; set; }
        public string Periodo { get; set; }
        public string Foto { get; set; }

        public int? CursoId { get; set; }
        //[JsonIgnore]
        public CursoEntities? Curso { get; set; }
        //[JsonIgnore]
        public List<BoletimEntities>? Boletim { get; set; }
    }
}
