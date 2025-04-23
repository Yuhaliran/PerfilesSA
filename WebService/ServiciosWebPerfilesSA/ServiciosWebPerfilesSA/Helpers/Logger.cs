using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System;

namespace ServiciosWebPerfilesSA.Helpers
{
    public static class Logger
    {
        private static readonly bool bitacoraActiva =
            ConfigurationManager.AppSettings["activebitacora"]?.ToLower() == "true";

        private static string GetLogFilePath()
        {
            string rutaLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (!Directory.Exists(rutaLog))
            {
                Directory.CreateDirectory(rutaLog);
            }

            string nombreArchivo = $"log_{DateTime.Now:yyyyMMdd}.txt";

            return Path.Combine(rutaLog, nombreArchivo);
        }

        public static void Log(string message)
        {
            if (!bitacoraActiva)
            {
                return;
            }                

            string log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Info | {message}";
            File.AppendAllText(GetLogFilePath(), log + Environment.NewLine);
        }

        public static void LogError(Exception ex, string procedimiento)
        {
            if (!bitacoraActiva)
            {
                return;
            }

            int lineaError = ex.StackTrace?.Split('\n').Length ?? 0;
            string mensaje = ex.Message;

            string logError = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Error | {mensaje} | Línea: {lineaError} | Procedimiento: {procedimiento}";
            File.AppendAllText(GetLogFilePath(), logError + Environment.NewLine);

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarError", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mensajeError", mensaje);
                    cmd.Parameters.AddWithValue("@lineaError", lineaError);
                    cmd.Parameters.AddWithValue("@procedimiento", procedimiento);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception innerEx)
            {
                string fallback = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Error | No se pudo registrar en BD: {innerEx.Message} | Msg: {mensaje} | Línea: {lineaError} | Procedimiento: {procedimiento}";
                File.AppendAllText(GetLogFilePath(), fallback + Environment.NewLine);
            }
        }
    }
}
