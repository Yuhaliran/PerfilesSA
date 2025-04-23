using ServiciosWebPerfilesSA.Interfaces;
using ServiciosWebPerfilesSA.Models;
using ServiciosWebPerfilesSA.Helpers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;

namespace ServiciosWebPerfilesSA.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public bool InsertarEmpleado(Empleado emp)
        {
            try
            {
                Logger.Log("Insertando empleado...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_InsertarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombres", emp.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", emp.Apellidos);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", emp.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@sexo", emp.Sexo);
                    cmd.Parameters.AddWithValue("@Activo", emp.Activo);
                    cmd.Parameters.AddWithValue("@fechaIngreso", emp.FechaIngreso);
                    cmd.Parameters.AddWithValue("@direccion", emp.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@nit", emp.Nit);
                    cmd.Parameters.AddWithValue("@dpi", emp.Dpi);
                    cmd.Parameters.AddWithValue("@IdDepartamento", emp.IdDepartamento);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "InsertarEmpleado");
                return false;
            }
        }

        public bool ActualizarEmpleado(Empleado emp)
        {
            try
            {
                Logger.Log("Actualizando empleado...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idEmpleado", emp.IdEmpleado);
                    cmd.Parameters.AddWithValue("@nombres", emp.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", emp.Apellidos);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", emp.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@sexo", emp.Sexo);
                    cmd.Parameters.AddWithValue("@Activo", emp.Activo);
                    cmd.Parameters.AddWithValue("@fechaIngreso", emp.FechaIngreso);
                    cmd.Parameters.AddWithValue("@direccion", emp.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@nit", emp.Nit);
                    cmd.Parameters.AddWithValue("@dpi", emp.Dpi);
                    cmd.Parameters.AddWithValue("@IdDepartamento", emp.IdDepartamento);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ActualizarEmpleado");
                return false;
            }
        }

        public List<Empleado> BuscarEmpleado(string dato)
        {
            var empleados = new List<Empleado>();
            try
            {
                Logger.Log("Buscando empleados...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.fn_BuscarEmpleado(@dato)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@dato", dato);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                empleados.Add(new Empleado
                                {
                                    IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                    Nombres = reader["nombres"].ToString(),
                                    Apellidos = reader["apellidos"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                                    Sexo = Convert.ToBoolean(reader["sexo"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]),
                                    Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : null,
                                    Nit = reader["nit"].ToString(),
                                    Dpi = Convert.ToInt64(reader["dpi"]),
                                    IdDepartamento = Convert.ToInt32(reader["IdDepartamento"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarEmpleado");
            }

            return empleados;
        }

        public List<Empleado> BuscarEmpleadoPorId(int idEmpleado)
        {
            var empleados = new List<Empleado>();
            try
            {
                Logger.Log($"Buscando empleado {idEmpleado}...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.fn_BuscarEmpleadoPorId(@idEmpleado)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                empleados.Add(new Empleado
                                {
                                    IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                    Nombres = reader["nombres"].ToString(),
                                    Apellidos = reader["apellidos"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                                    Sexo = Convert.ToBoolean(reader["sexo"]),
                                    Activo = Convert.ToBoolean(reader["activo"]),
                                    FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]),
                                    Direccion = reader["direccion"].ToString(),
                                    Nit = reader["nit"].ToString(),
                                    Dpi = Convert.ToInt64(reader["dpi"]),
                                    IdDepartamento = Convert.ToInt32(reader["idDepartamento"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarEmpleadoPorId");
            }

            return empleados;
        }

        public List<Empleado> ReportarEmpleados()
        {
            var empleados = new List<Empleado>();
            try
            {
                Logger.Log("Generando reporte de empleados...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.fn_ReportarEmpleados()";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                empleados.Add(new Empleado
                                {
                                    IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                    Nombres = reader["nombres"].ToString(),
                                    Apellidos = reader["apellidos"].ToString(),
                                    Edad = Convert.ToInt32(reader["edad"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]),
                                    NombreDepartamento = reader["departamento"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ReportarEmpleados");
            }
            return empleados;
        }
    }
}
