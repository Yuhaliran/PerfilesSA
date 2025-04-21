using ServiciosWebPerfilesSA.Models;
using System.Collections.Generic;

namespace ServiciosWebPerfilesSA.Interfaces
{
    public interface IEmpleadoRepository
    {
        bool InsertarEmpleado(Empleado emp);
        List<Empleado> BuscarEmpleado(string dato);
        bool ActualizarEmpleado(Empleado emp);
        List<Empleado> BuscarEmpleadoPorId(int dato);
        List<Empleado> ReportarEmpleados();
    }
}
