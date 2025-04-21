using System;

namespace FrontEndPerfilesSA.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Sexo { get; set; }
        public string Direccion { get; set; }
        public string NIT { get; set; }
        public string DPI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int IdDepartamento { get; set; }
        public bool Activo { get; set; }
        public string NombreDepartamento { get; set;}
        public string Edad { get; set;}
    }
}
