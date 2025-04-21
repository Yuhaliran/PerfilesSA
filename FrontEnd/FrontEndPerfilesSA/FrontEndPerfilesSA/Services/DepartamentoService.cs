using FrontEndPerfilesSA.Helpers;
using FrontEndPerfilesSA.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace FrontEndPerfilesSA.Services
{
    public class DepartamentoService
    {
        public List<Departamento> ObtenerDepartamentos()
        {
            HttpResponseMessage respuesta = ApiHelper.Get("api/departamento/GetDepartamentos");
            if (respuesta.IsSuccessStatusCode)
            {
                string contenido = respuesta.Content.ReadAsStringAsync().Result;
                var departamentos = JsonConvert.DeserializeObject<List<Departamento>>(contenido);
                return departamentos;
            }
            return new List<Departamento>();
        }

        public bool InsertarDepartamento(Departamento departamento)
        {
            HttpResponseMessage respuesta = ApiHelper.Post("api/departamento/insertar", departamento);
            if (respuesta.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("Departamento insertado correctamente.");
                return true;
            }
            return false;
        }

        public bool ActualizarDepartamento(Departamento departamento)
        {
            HttpResponseMessage respuesta = ApiHelper.Put("api/departamento/actualizar", departamento);
            if (respuesta.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("Departamento actualizado correctamente.");
                return true;
            }
            return false;
        }

        public bool ValidarExistenciaDepartamento(string nombre)
        {
            HttpResponseMessage respuesta = ApiHelper.Get($"api/departamento/validar?nombre={nombre}");
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = respuesta.Content.ReadAsStringAsync().Result;
                var existencia = JsonConvert.DeserializeObject<dynamic>(contenido);
                return existencia.existe;
            }
            return false;
        }
    }
}
