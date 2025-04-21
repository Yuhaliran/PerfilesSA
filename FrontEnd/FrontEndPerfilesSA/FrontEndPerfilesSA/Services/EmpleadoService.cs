using FrontEndPerfilesSA.Helpers;
using FrontEndPerfilesSA.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace FrontEndPerfilesSA.Services
{
    public class EmpleadoService
    {

        public List<Empleado> BuscarEmpleados(string dato)
        {
            var respuesta = ApiHelper.Get($"api/empleado/buscar?dato={dato}");
            if (respuesta.IsSuccessStatusCode)
            {
                string json = respuesta.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Empleado>>(json);
            }
            return new List<Empleado>();
        }


        public bool ActualizarEmpleado(Empleado empleado)
        {
            string endpoint = $"api/empleado//actualizar";
            var respuesta = ApiHelper.Put(endpoint, empleado);
            return respuesta.IsSuccessStatusCode;
        }


        public List<Empleado> BuscarEmpleadoPorId(int dato)
        {
            var respuesta = ApiHelper.Get($"api/empleado/buscarPorId/{dato}");
            if (respuesta.IsSuccessStatusCode)
            {
                string json = respuesta.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Empleado>>(json);
            }

            return new List<Empleado>();
        }


        public List<Empleado> ObtenerEmpleados()
        {
            HttpResponseMessage response = ApiHelper.Get("api/empleado/GetEmpleados");

            if (response.IsSuccessStatusCode)
            {
                string contenido = response.Content.ReadAsStringAsync().Result;
                var departamentos = JsonConvert.DeserializeObject<List<Empleado>>(contenido);return departamentos;
            }
            return new List<Empleado>();
        }



        public List<Empleado> ReportarEmpleados()
        {
            HttpResponseMessage response = ApiHelper.Get("api/empleado/ReportarEmpleados");

            if (response.IsSuccessStatusCode)
            {
                string contenido = response.Content.ReadAsStringAsync().Result;
                var departamentos = JsonConvert.DeserializeObject<List<Empleado>>(contenido);
                return departamentos;
            }
            return new List<Empleado>();
        }
    }
}
