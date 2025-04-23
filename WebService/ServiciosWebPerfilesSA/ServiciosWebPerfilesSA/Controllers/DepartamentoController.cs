using ServiciosWebPerfilesSA.Repositories;
using ServiciosWebPerfilesSA.Interfaces;
using ServiciosWebPerfilesSA.Helpers;
using ServiciosWebPerfilesSA.Models;
using System.Web.Http;
using System;

namespace ServiciosWebPerfilesSA.Controllers
{
    public class DepartamentoController : ApiController
    {
        private readonly IDepartamentoRepository _repo;

        public DepartamentoController()
        {
            _repo = new DepartamentoRepository();
        }

        [HttpGet]
        [Route(ApiRoutes.Departamento.ObtenerTodos)]
        public IHttpActionResult ObtenerTodos()
        {
            Logger.Log("Intentando consultar todos los departamentos");
            try
            {
                var resultado = _repo.BuscarDepartamento("");
                Logger.Log("Consulta de departamentos correcta");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Fallo al consultar todos los departamentos");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route(ApiRoutes.Departamento.Insertar)]
        public IHttpActionResult InsertarDepartamento(Departamento depto)
        {
            Logger.Log($"Intentando insertar departamento: {depto.Nombre}");

            try
            {
                if (_repo.InsertarDepartamento(depto))
                {
                    Logger.Log($"Departamento insertado correctamente: {depto.Nombre}");
                    return Ok("Departamento insertado correctamente.");
                }

                Logger.Log($"Fallo al insertar el departamento: {depto.Nombre}");
                return BadRequest("Fallo al insertar el departamento");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "InsertarDepartamento");
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route(ApiRoutes.Departamento.Actualizar)]
        public IHttpActionResult ActualizarDepartamento(Departamento depto)
        {
            Logger.Log($"Intentando actualizar departamento id: {depto.IdDepartamento} nombre: {depto.Nombre}");

            try
            {
                if (_repo.ActualizarDepartamento(depto))
                {
                    Logger.Log($"Departamento actualizado correctamente: {depto.Nombre}");
                    return Ok("Departamento actualizado correctamente.");
                }

                Logger.Log($"No se pudo actualizar el departamento: {depto.Nombre}");
                return BadRequest("No se pudo actualizar el departamento");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ActualizarDepartamento");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route(ApiRoutes.Departamento.Buscar)]
        public IHttpActionResult BuscarDepartamento([FromUri] string dato)
        {
            Logger.Log($"Buscando departamento: {dato}");

            try
            {
                var resultado = _repo.BuscarDepartamento(dato);

                if (resultado.Count == 0)
                {
                    Logger.Log($"No se encontraron departamentos: {dato}");
                    return NotFound();
                }

                Logger.Log($"Departamentos encontrados con {dato} total: {resultado.Count}");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarDepartamento");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route(ApiRoutes.Departamento.BuscarId)]
        public IHttpActionResult BuscarDepartamentoPorId(int id)
        {
            Logger.Log($"Buscando departamento por id: {id}");

            try
            {
                var resultado = _repo.BuscarDepartamentoPorId(id);

                if (resultado == null)
                {
                    Logger.Log($"No se encontro departamento id: {id}");
                    return NotFound();
                }

                Logger.Log($"Departamento encontrado con id: {id}");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarDepartamentoPorId");
                return InternalServerError();
            }
        }
    }
}
