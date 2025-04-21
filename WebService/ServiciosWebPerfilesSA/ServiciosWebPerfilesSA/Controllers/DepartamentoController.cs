using ServiciosWebPerfilesSA.Interfaces;
using ServiciosWebPerfilesSA.Models;
using ServiciosWebPerfilesSA.Repositories;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net;
using System;
using System.Web.Http;

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
        [Route("api/departamento/test")]
        public IHttpActionResult Test()
        {
            return Ok("probando API dafasdf");
        }


        [HttpGet]
        [Route("api/departamento/GetDepartamentos")]
        public IHttpActionResult ObtenerTodos()
        {
            var resultado = _repo.BuscarDepartamento("");
            return Ok(resultado);
        }


        [HttpPost]
        [Route("api/departamento/insertar")]
        public IHttpActionResult InsertarDepartamento(Departamento depto)
        {
            try
            {
                if (_repo.InsertarDepartamento(depto))
                    return Ok("Departamento insertado correctamente.");

                return BadRequest("No se pudo insertar el departamento.");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/departamento/actualizar")]
        public IHttpActionResult ActualizarDepartamento(Departamento depto)
        {
            try
            {
                if (_repo.ActualizarDepartamento(depto))
                    return Ok("Departamento actualizado correctamente.");

                return BadRequest("No se pudo actualizar el departamento.");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("api/departamento/buscar")]
        public IHttpActionResult BuscarDepartamento([FromUri] string dato)
        {
            var resultado = _repo.BuscarDepartamento(dato);

            if (resultado.Count == 0)
                return NotFound();

            return Ok(resultado);
        }


        [HttpGet]
        [Route("api/departamento/buscar/{id}")]
        public IHttpActionResult BuscarDepartamentoPorId(int id)
        {
            var resultado = _repo.BuscarDepartamentoPorId(id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/departamento/validar")]
        public HttpResponseMessage ValidarExistenciaDepartamento(string nombre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.departamento WHERE Nombre = @Nombre", conn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);

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


    }
}
