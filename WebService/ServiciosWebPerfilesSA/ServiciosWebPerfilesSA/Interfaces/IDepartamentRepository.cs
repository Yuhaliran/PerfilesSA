using ServiciosWebPerfilesSA.Models;
using System.Collections.Generic;

namespace ServiciosWebPerfilesSA.Interfaces
{
    public interface IDepartamentoRepository
    {
        bool InsertarDepartamento(Departamento depto);
        bool ActualizarDepartamento(Departamento depto);
        List<Departamento> BuscarDepartamento(string dato);        
        List<Departamento> BuscarDepartamentoPorId(int dato);
    }
}