using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    /// <summary>
    /// LECTURA
    /// Clase principal de aplicación con patrón CQRS usando la librería MediatR
    /// </summary>
    public class Consulta
    {
        /// <summary>
        /// 
        /// </summary>
        public class ListaAutor : IRequest<List<AutorDto>> { }


        /// <summary>
        /// 
        /// </summary>
        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibro.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorDto>>(autores);
                return autoresDto;
            }
        }
    }
}
