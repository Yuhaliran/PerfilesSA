using FrontEndPerfilesSA.Helpers;
using FrontEndPerfilesSA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FrontEndPerfilesSA.Services
{
    public class EmpleadoService
    {
        public List<Empleado> BuscarEmpleados(string dato)
        {
            try
            {
                var respuesta = ApiHelper.Get($"{ApiRoutes.Empleado.Buscar}?dato={dato}");
                if (respuesta.IsSuccessStatusCode)
                {
                    string json = respuesta.Content.ReadAsStringAsync().Result;
                    Logger.Log("Buscar empleados correcto");
                    return JsonConvert.DeserializeObject<List<Empleado>>(json);
                }
                else
                {
                    Logger.Log("Buscar Empleados incorrecto");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return new List<Empleado>();
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                var respuesta = ApiHelper.Put(ApiRoutes.Empleado.Actualizar, empleado);
                if (respuesta.IsSuccessStatusCode)
                {
                    Logger.Log("Actualizar Empleado Correcto");
                    return true;
                }
                else
                {
                    Logger.Log("Actualizar empleado incorrecto");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return false;
        }

        public List<Empleado> BuscarEmpleadoPorId(int dato)
        {
            try
            {
                var respuesta = ApiHelper.Get(ApiRoutes.Empleado.BuscarId(dato));
                if (respuesta.IsSuccessStatusCode)
                {
                    string json = respuesta.Content.ReadAsStringAsync().Result;
                    Logger.Log("Buscar Empleado por id correcto");
                    return JsonConvert.DeserializeObject<List<Empleado>>(json);
                }
                else
                {
                    Logger.Log("Buscar Empleado por id incorrecto");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return new List<Empleado>();
        }

        public List<Empleado> ObtenerEmpleados()
        {
            try
            {
                HttpResponseMessage response = ApiHelper.Get(ApiRoutes.Empleado.ObtenerTodos);
                if (response.IsSuccessStatusCode)
                {
                    string contenido = response.Content.ReadAsStringAsync().Result;
                    var empleados = JsonConvert.DeserializeObject<List<Empleado>>(contenido);
                    Logger.Log("ObtenerEmpleados correcto.");
                    return empleados;
                }
                else
                {
                    Logger.Log("ObtenerEmpleados incorrecto");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return new List<Empleado>();
        }

        public List<Empleado> ReportarEmpleados()
        {
            try
            {
                HttpResponseMessage response = ApiHelper.Get(ApiRoutes.Empleado.Reportar);
                if (response.IsSuccessStatusCode)
                {
                    string contenido = response.Content.ReadAsStringAsync().Result;
                    var empleados = JsonConvert.DeserializeObject<List<Empleado>>(contenido);
                    Logger.Log("ReportarEmpleados correcto");
                    return empleados;
                }
                else
                {
                    Logger.Log("ReportarEmpleados incorrecto");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return new List<Empleado>();
        }
    }
}
