using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            
            return lista;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new MappingTest());
            });
            var mockMapper = mapConfig.CreateMapper();
            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mockMapper);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            
            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros()
        {
            //System.Diagnostics.Debugger.Launch(); //Para poder debuguear el metodo de testing

            var moqContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new MappingTest());
            });
            var moqMapper = mapConfig.CreateMapper();

            var manejador = new Consulta.Manejador(moqContexto.Object, moqMapper);

            var request = new Consulta.Ejecuta();
            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());

        }

        [Fact]
        public async void GuardarLibro()
        {
            //System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                                .Options;
            var contexto = new ContextoLibreria(options);
            var request = new Nuevo.Ejecuta();
            
            request.Titulo = "Libro de Micorservicios";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null);
        }
    }
}
