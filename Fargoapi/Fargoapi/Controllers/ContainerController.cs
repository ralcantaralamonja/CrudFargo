using Fargoapi.Datos;
using Fargoapi.Modelo;
using Microsoft.AspNetCore.Mvc;
 

namespace Fargoapi.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ContainerController
    {
        [HttpGet("contenedores")]
        public async Task<ActionResult<List<Mcontenedor>>> ObtenerTodos()
        {
            var funcion = new Dcontainers();
            var lista = await funcion.ListarContenedores();
            return lista;
        }

        [HttpGet("contenedores/{id}")]
        public async Task<ActionResult<Mcontenedor>> ObtenerPorId(int id)
        {
            var funcion = new Dcontainers();
            var contenedor = await funcion.ObtenerContenedorPorId(id);
            if (contenedor == null)
                return new StatusCodeResult(404);

            return contenedor;
        }
    }
}
