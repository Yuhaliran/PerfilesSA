﻿using ServiciosWebPerfilesSA.Interfaces;
using ServiciosWebPerfilesSA.Models;
using ServiciosWebPerfilesSA.Helpers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;

namespace ServiciosWebPerfilesSA.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public bool InsertarDepartamento(Departamento depto)
        {
            try
            {
                Logger.Log("Insertando departamento...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_InsertarDepartamento", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", depto.Nombre);
                    cmd.Parameters.AddWithValue("@Activo", depto.Activo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "InsertarDepartamento");
                return false;
            }
        }

        public bool ActualizarDepartamento(Departamento depto)
        {
            try
            {
                Logger.Log("Actualizando departamento...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarDepartamento", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDepartamento", depto.IdDepartamento);
                    cmd.Parameters.AddWithValue("@Nombre", depto.Nombre);
                    cmd.Parameters.AddWithValue("@Activo", depto.Activo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ActualizarDepartamento");
                return false;
            }
        }

        public List<Departamento> BuscarDepartamento(string dato)
        {
            var departamentos = new List<Departamento>();

            try
            {
                Logger.Log("Buscando departamentos...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.fn_BuscarDepartamento(@dato)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@dato", dato);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                departamentos.Add(new Departamento
                                {
                                    IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Activo = Convert.ToBoolean(reader["Activo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarDepartamento");
            }

            return departamentos;
        }

        public List<Departamento> BuscarDepartamentoPorId(int idDepartamento)
        {
            var departamentos = new List<Departamento>();

            try
            {
                Logger.Log($"Buscando departamento {idDepartamento}...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.fn_BuscarDepartamentoPorId(@idDepartamento)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idDepartamento", idDepartamento);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                departamentos.Add(new Departamento
                                {
                                    IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Activo = Convert.ToBoolean(reader["Activo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "BuscarDepartamentoPorId");
            }
            return departamentos;
        }
    }
}
