using System;
namespace ServiciosWebPerfilesSA.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Direccion { get; set; }
        public string Nit { get; set; }
        public long Dpi { get; set; }
        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public int Edad{ get; set; }

    }
}