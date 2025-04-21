using FrontEndPerfilesSA.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using EmpleadoModel = FrontEndPerfilesSA.Models.Empleado;
using System.Linq;

namespace FrontEndPerfilesSA.Pages.Reportes
{
    public partial class ReporteEmpleados : System.Web.UI.Page
    {
        private EmpleadoService empleadoService = new EmpleadoService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEmpleados();
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            var empleados = empleadoService.ReportarEmpleados();
            gvReporte.DataSource = empleados;
            gvReporte.DataBind();
        }

        private void CargarEmpleadosFiltrados()
        {
            var empleados = empleadoService.ReportarEmpleados();
            if (DateTime.TryParse(txtFechaInicio.Text, out DateTime fechaInicio) &&
                DateTime.TryParse(txtFechaFin.Text, out DateTime fechaFin))
            {
                empleados = empleados
                    .Where(e => e.FechaIngreso.Date >= fechaInicio.Date && e.FechaIngreso.Date <= fechaFin.Date)
                    .ToList();
            }
            gvReporte.DataSource = empleados;
            gvReporte.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarEmpleadosFiltrados();
        }

    }
}
