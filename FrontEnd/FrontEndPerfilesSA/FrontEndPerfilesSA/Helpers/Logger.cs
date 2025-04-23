using System;
using System.IO;

namespace FrontEndPerfilesSA.Helpers
{
    public static class Logger
    {
        private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "log_frontend.txt");

        public static void Log(string mensaje)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                string mensajeFormateado = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {mensaje}";
                File.AppendAllText(logFilePath, mensajeFormateado + Environment.NewLine);
            }
            catch { }
        }

        public static void LogError(Exception ex)
        {
            Log($"ERROR: {ex.Message} | StackTrace: {ex.StackTrace}");
        }
    }
}
