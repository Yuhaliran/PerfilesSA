using System.Configuration;

namespace FrontEndPerfilesSA.Helpers
{
    public static class ApiRoutes
    {

        public static string BaseUrl => ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static string EmpleadoBase => ConfigurationManager.AppSettings["EmpleadoEndpoint"];
        public static string DepartamentoBase => ConfigurationManager.AppSettings["DepartamentoEndpoint"];

        public static class Departamento
        {
            public static string ObtenerTodos => $"{BaseUrl}/{DepartamentoBase}/GetDepartamentos";
            public static string Insertar => $"{BaseUrl}/{DepartamentoBase}/insertar";
            public static string Actualizar => $"{BaseUrl}/{DepartamentoBase}/actualizar";
            public static string Buscar => $"{BaseUrl}/{DepartamentoBase}/buscar";
            public static string BuscarId(int id) => $"{BaseUrl}/{DepartamentoBase}/buscar/{id}";
        }

        public static class Empleado
        {
            public static string ObtenerTodos => $"{BaseUrl}/{EmpleadoBase}/GetEmpleados";
            public static string Insertar => $"{BaseUrl}/{EmpleadoBase}/insertar";
            public static string Actualizar => $"{BaseUrl}/{EmpleadoBase}/actualizar";
            public static string Buscar => $"{BaseUrl}/{EmpleadoBase}/buscar";
            public static string BuscarId(int id) => $"{BaseUrl}/{EmpleadoBase}/buscarPorId/{id}";
            public static string Reportar => $"{BaseUrl}/{EmpleadoBase}/ReportarEmpleados";
        }
    }
}
