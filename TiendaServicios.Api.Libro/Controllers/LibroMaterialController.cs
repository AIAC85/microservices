using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroMaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDto>>> Obtener()
        {
            var libro = new Consulta.Ejecuta();
            return await _mediator.Send(libro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDto>> ObtenerLibroUnico(Guid Id)
        {
            var libro = new ConsultaFiltro.LibroUnico { LibroId  = Id };
            return await _mediator.Send(libro);
        }
    }
}
