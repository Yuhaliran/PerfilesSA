using ServiciosWebPerfilesSA.Repositories;
using ServiciosWebPerfilesSA.Interfaces;
using ServiciosWebPerfilesSA.Helpers;
using ServiciosWebPerfilesSA.Models;
using System.Web.Http;
using System;

namespace ServiciosWebPerfilesSA.Controllers
{
    public class EmpleadoController : ApiController
    {
        private readonly IEmpleadoRepository _repo;

        public EmpleadoController()
        {
            _repo = new EmpleadoRepository();
        }

        [HttpPost]
        [Route(ApiRoutes.Empleado.Insertar)]
        public IHttpActionResult InsertarEmpleado(Empleado emp)
        {
            Logger.Log($"Intentando insertar empleado: {emp.Nombres}");

            try
            {
                if (_repo.InsertarEmpleado(emp))
                {
                    Logger.Log($"Empleado insertado: {emp.Nombres}");
                    return Ok("Empleado insertado");
                }

                Logger.Log($"Fallo al insertar empleado: {emp.Nombres}");
                return BadRequest("No se pudo insertar empleado");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "InsertarEmpleado");
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route(ApiRoutes.Empleado.Actualizar)]
        public IHttpActionResult ActualizarEmpleado(Empleado emp)
        {
            Logger.Log($"Intentando actualizar empleado: {emp.IdEmpleado}");

            try
            {
                if (_repo.ActualizarEmpleado(emp))
                {
                    Logger.Log($"Empleado actualizado: {emp.IdEmpleado}");
                    return Ok("Empleado actualizado");
                }

                Logger.Log($"Fallo al actualizar el empleado: {emp.IdEmpleado}");
                return BadRequest("No se pudo actualizar el empleado");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ActualizarEmpleado");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route(ApiRoutes.Empleado.Buscar)]
        public IHttpActionResult BuscarEmpleado([FromUri] string dato)
        {
            Logger.Log($"Buscando empleado: {dato}");

            try
            {
                var resultado = _repo.BuscarEmpleado(dato);

                if (resultado.Count == 0)
                {
                    Logger.Log("No se encontraron empleados");
                    return NotFound();
                }

                Logger.Log($"Se encontraron {resultado.Count} empleados.");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarEmpleado");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route(ApiRoutes.Empleado.ObtenerTodos)]
        public IHttpActionResult ObtenerTodos()
        {
            Logger.Log("Consultando todos los empleados");

            try
            {
                var resultado = _repo.BuscarEmpleado("");
                Logger.Log("Consultados todos los empleados");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ObtenerTodos");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route(ApiRoutes.Empleado.Reportar)]
        public IHttpActionResult ReportarEmpleados()
        {
            Logger.Log("Reportando empleados");

            try
            {
                var resultado = _repo.ReportarEmpleados();
                Logger.Log("Empleados Reportados");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ReportarEmpleados");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route(ApiRoutes.Empleado.BuscarId)]
        public IHttpActionResult BuscarEmpleado(int id)
        {
            Logger.Log($"Buscando empleado: {id}");

            try
            {
                var resultado = _repo.BuscarEmpleadoPorId(id);

                if (resultado.Count == 0)
                {
                    Logger.Log("Empleado no encontrado");
                    return NotFound();
                }

                Logger.Log("Empleado encontradoaa");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarEmpleadoPorId");
                return InternalServerError();
            }
        }
    }
}
