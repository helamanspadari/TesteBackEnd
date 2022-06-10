using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TesteBack.Domain.Entities.Aluno;
using TesteBack.Domain.Entities.Disciplina;

namespace TesteBack.Domain.Entities.Curso
{
    public class CursoEntities : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<DisciplinaEntities>? Disciplinas { get; set; }
        [JsonIgnore]
        public List<AlunoEntities>? Alunos { get; set; }
    }
}
