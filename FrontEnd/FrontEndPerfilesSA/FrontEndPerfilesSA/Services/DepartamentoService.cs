using FrontEndPerfilesSA.Helpers;
using FrontEndPerfilesSA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FrontEndPerfilesSA.Services
{
    public class DepartamentoService
    {
        public List<Departamento> ObtenerDepartamentos()
        {
            try
            {
                var respuesta = ApiHelper.Get(ApiRoutes.Departamento.ObtenerTodos);
                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().Result;
                    var departamentos = JsonConvert.DeserializeObject<List<Departamento>>(contenido);
                    Logger.Log("ObtenerDepartamentos correcto.");
                    return departamentos;
                }
                else
                {
                    Logger.Log("ObtenerDepartamentos incorrecto.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return new List<Departamento>();
        }

        public bool InsertarDepartamento(Departamento depto)
        {
            try
            {
                var respuesta = ApiHelper.Post(ApiRoutes.Departamento.Insertar, depto);
                if (respuesta.IsSuccessStatusCode)
                {
                    Logger.Log("InsertarDepartamento correcto.");
                    return true;
                }
                else
                {
                    Logger.Log("InsertarDepartamento incorrecto.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return false;
        }

        public bool ActualizarDepartamento(Departamento depto)
        {
            try
            {
                var respuesta = ApiHelper.Put(ApiRoutes.Departamento.Actualizar, depto);
                if (respuesta.IsSuccessStatusCode)
                {
                    Logger.Log("ActualizarDepartamento correcto.");
                    return true;
                }
                else
                {
                    Logger.Log("ActualizarDepartamento incorrecto.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return false;
        }

        public List<Departamento> BuscarDepartamento(string dato)
        {
            try
            {
                var respuesta = ApiHelper.Get($"{ApiRoutes.Departamento.Buscar}?dato={dato}");
                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().Result;
                    var resultados = JsonConvert.DeserializeObject<List<Departamento>>(contenido);
                    Logger.Log("BuscarDepartamento correcto.");
                    return resultados;
                }
                else
                {
                    Logger.Log("BuscarDepartamento incorrecto.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return new List<Departamento>();
        }

        public Departamento BuscarDepartamentoPorId(int id)
        {
            try
            {
                var respuesta = ApiHelper.Get(ApiRoutes.Departamento.BuscarId(id));
                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<Departamento>(contenido);
                    Logger.Log("BuscarDepartamentoPorId correcto.");
                    return resultado;
                }
                else
                {
                    Logger.Log("BuscarDepartamentoPorId incorrecto.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return null;
        }
    }
}
