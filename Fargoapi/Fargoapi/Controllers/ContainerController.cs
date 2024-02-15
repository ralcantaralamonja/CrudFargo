using Fargoapi.Datos;
using Fargoapi.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace Fargoapi.Controllers
{
    [ApiController]
    [Route("/api/contenedores")]
    public class ContainerController
    {
        [HttpGet]
        public async Task<ActionResult<List<Mcontenedor>>> getObtenerTodos() {
            var funcion = new Dcontainers();
            var lista = await funcion.ListarContenedores();
            return lista;
        }
    }
}
