using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TesteBack.Domain.Entities.Disciplina;

namespace TesteBack.Domain.Entities.Aluno
{
    public class BoletimEntities
    {
        [Key]
        public int Id { get; set; }

        public int AlunoId { get; set; }
        [ForeignKey("AlunoId"), JsonIgnore]
        public AlunoEntities Aluno { get; set; }

        public int DisciplinaId { get; set; }
        public string NomeDisciplina { get; set; }
        [ForeignKey("DisciplinaId"), JsonIgnore]
        public DisciplinaEntities Disciplina { get; set; }

        public decimal NotaAlunoDisciplina { get; set; }
        public string StatusAlunoDisciplina { get; set; }
    }
}
