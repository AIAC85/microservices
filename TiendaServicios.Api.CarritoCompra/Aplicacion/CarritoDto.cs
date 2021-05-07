using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDto
    {
        public int CarridoId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public List<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
