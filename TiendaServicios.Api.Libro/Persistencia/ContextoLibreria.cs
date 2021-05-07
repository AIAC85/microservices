using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria() { }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }

        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }
    }
}
