using Microsoft.EntityFrameworkCore;
using TesteBack.Domain.Entities.Aluno;
using TesteBack.Domain.Entities.Curso;
using TesteBack.Domain.Entities.Disciplina;

namespace TesteBack.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BoletimEntities> Boletims { get; set; }
        public DbSet<AlunoEntities> Alunos { get; set; }
        public DbSet<CursoEntities> Cursos { get; set; }
        public DbSet<DisciplinaEntities> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
