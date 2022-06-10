using Microsoft.OpenApi.Models;
using TesteBack.Domain.Interfaces;
using TesteBack.Repository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TesteBack.Service.Service;
using TesteBack.Repository.Repository;
using TesteBack.Service.Service.Disciplina;
using TesteBack.Repository.Repository.Disciplina;
using TesteBack.Repository.Repository.Curso;
using TesteBack.Service.Service.Curso;
using TesteBack.Service.Service.Aluno;
using TesteBack.Repository.Repository.Aluno;
using AutoMapper;
using TesteBack.Service.AutoMapper;
using System.Text.Json.Serialization;

namespace TesteBack.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            // String conexao
            services.AddDbContext<AppDbContext>(conn => conn.UseSqlite(Configuration.GetConnectionString("ServerConnection")));

            // Injecao de dependencia das services
            StartDependencyInjectionAddTransient(services);


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteBack.Application", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteBack.Application v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public void StartDependencyInjectionAddTransient(IServiceCollection services)
        {
            services.AddTransient<AlunoService>();
            services.AddTransient<AlunoRepository>();
            services.AddTransient<CursoService>();
            services.AddTransient<CursoRepository>();
            services.AddTransient<DisciplinaService>();
            services.AddTransient<DisciplinaRepository>();
            services.AddScoped(typeof(IService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        }

    }
}
