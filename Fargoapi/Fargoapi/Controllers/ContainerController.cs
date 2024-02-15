﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fargoapi.Datos;
using Fargoapi.Modelo;


namespace Fargoapi.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ContainerController : ControllerBase
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
            var contenedor = await funcion.ListarContenedor(id);
            if (contenedor == null)
                return new StatusCodeResult(404);
            return contenedor;
        }

 [HttpPost("contenedores")]
        public async Task<ActionResult> InsertarContenedor(Mcontenedor nuevoContenedor)
        {
            try
            {
                var funcion = new Dcontainers();
                var exito = await funcion.InsertarContenedor(nuevoContenedor);

                if (exito)
                {
                    return Ok("Contenedor insertado exitosamente");
                }
                else
                {
                    return StatusCode(500, "Error al insertar el contenedor");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al insertar el contenedor: {ex.Message}");
            }
        }

    }
}
