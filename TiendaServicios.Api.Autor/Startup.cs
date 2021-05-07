using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor
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
            //Agregar el FluentValidation para validación de datos de entradad en los controllers
            //services.AddControllers();
            //services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>()); //del curso
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));


            //-- Se inserta el servicio que crea/configura el contexto de entityframework para postgresql --\\
            services.AddDbContext<ContextoAutor>(options => 
            {
                options.UseNpgsql(Configuration.GetConnectionString("ConexionDatabase"));
            });


            #region Inserción de servicios de MediatR
            //-- Inserta Nuevo.Manejador al MediatR --\\
            // Supuestamente esto solo se debe hacer con una clase cualquiera que extienda de las clases de MediatR y que posteriormente el propio MediatR se encarga de buscar todas las clases que lo contengan
            //services.AddMediatR(typeof(Nuevo.Manejador).Assembly); //Del curso
            services.AddMediatR(Assembly.GetExecutingAssembly()); //MediatR necesita accesa al assembly para que cargue todas las referencias a el
            #endregion

            //services.AddAutoMapper(typeof(Consulta.Manejador).Assembly); //curso
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
