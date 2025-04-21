using ServiciosWebPerfilesSA.Interfaces;
using ServiciosWebPerfilesSA.Models;
using ServiciosWebPerfilesSA.Repositories;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net;
using System.Web.Http;

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
        [Route("api/empleado/insertar")]
        public IHttpActionResult InsertarEmpleado(Empleado emp)
        {
            try
            {
                if (_repo.InsertarEmpleado(emp))
                    return Ok("Empleado insertado correctamente.");

                return BadRequest("No se pudo insertar el empleado.");
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpPut]
        [Route("api/empleado/actualizar")]
        public IHttpActionResult ActualizarEmpleado(Empleado emp)
        {
            try
            {
                if (_repo.ActualizarEmpleado(emp))
                    return Ok("Empleado actualizado correctamente.");

                return InternalServerError();
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/empleado/buscar")]
        public IHttpActionResult BuscarEmpleado([FromUri] string dato)
        {
            try
            {
                var resultado = _repo.BuscarEmpleado(dato);

                if (resultado.Count == 0)
                    return NotFound();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
            
        }


        [HttpGet]
        [Route("api/empleado/GetEmpleados")]
        public IHttpActionResult ObtenerTodos()
        {
            try
            {
                var resultado = _repo.BuscarEmpleado("");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/empleado/ReportarEmpleados")]
        public IHttpActionResult ReportarEmpleados()
        {
            try
            {
                var resultado = _repo.ReportarEmpleados();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }



        [HttpGet]
        [Route("api/empleado/validar")]
        public HttpResponseMessage ValidarExistenciaEmpleado(string nit, string dpi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.empleado WHERE nit = @nit OR dpi = @dpi", conn);
                    cmd.Parameters.AddWithValue("@nit", nit);
                    cmd.Parameters.AddWithValue("@dpi", dpi);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return Request.CreateResponse(HttpStatusCode.OK, new { existe = count > 0 });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        [Route("api/empleado/buscarPorId/{id}")]
        public IHttpActionResult BuscarEmpleado(int id)
        {
            try
            {
                var resultado = _repo.BuscarEmpleadoPorId(id);

                if (resultado.Count == 0)
                    return NotFound();

                return Ok(resultado);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
            
        }
    }
}
