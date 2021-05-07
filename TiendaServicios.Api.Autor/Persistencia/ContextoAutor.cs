using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Persistencia
{
    /// <summary>
    /// Clase de contexto
    /// </summary>
    public class ContextoAutor : DbContext
    {
        #region Propiedades referentes a las tablas de la base de datos (El tipo es el nombre de la talba en la BD y el nombre de la propiedad es como se nombra a nivel c#
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; } 
        #endregion

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="options">opciones entre otras cosas de conexión (eso se inserta en el Starup.cs)</param>
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options) { }
    }
}
