namespace ServiciosWebPerfilesSA.Helpers
{
    public static class ApiRoutes
    {
        public const string DepartamentoBase = "api/departamento";
        public const string EmpleadoBase = "api/empleado";

        public static class Departamento
        {
            public const string ObtenerTodos = DepartamentoBase + "/GetDepartamentos";
            public const string Insertar = DepartamentoBase + "/insertar";
            public const string Actualizar = DepartamentoBase + "/actualizar";
            public const string Buscar = DepartamentoBase + "/buscar";
            public const string BuscarId = DepartamentoBase + "/buscar/{id}";
        }

        public static class Empleado
        {
            public const string ObtenerTodos = EmpleadoBase + "/GetEmpleados";
            public const string Insertar = EmpleadoBase + "/insertar";
            public const string Actualizar = EmpleadoBase + "/actualizar";
            public const string Buscar = EmpleadoBase + "/buscar";
            public const string BuscarId = EmpleadoBase + "/buscarPorId/{id}";
            public const string Reportar = EmpleadoBase + "/ReportarEmpleados";
        }
    }
}
