using FluentValidation;
using MediatR;
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
    /// ESCRITURA
    /// Clase principal de aplicación con patrón CQRS usando la librería MediatR
    /// </summary>
    public class Nuevo
    {
        /// <summary>
        /// Clase de los parametros recibidos por el controller
        /// </summary>
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }


        /// <summary>
        /// Clase proveniente del servicio FluentValidation que sirve para validar los datos de entrada
        /// </summary>
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }


        /// <summary>
        /// Lógica de insérción
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Asignar el request al modelo
                var autorLibro = new AutorLibro 
                { 
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellido = request.Apellido,
                    AutorLibroGuid = string.Format("{0}", Guid.NewGuid())
                };

                _contexto.AutorLibro.Add(autorLibro); //Agregar la instancia creada (en memoria)
                var valor = await _contexto.SaveChangesAsync(); //Se guardan todos los cambios en memoria

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el autor del libro");
            }
        }
    }
}
