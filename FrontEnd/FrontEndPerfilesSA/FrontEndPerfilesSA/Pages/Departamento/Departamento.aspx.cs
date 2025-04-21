using FrontEndPerfilesSA.Services;
using System;
using System.Web.UI;
using DepartamentoModel = FrontEndPerfilesSA.Models.Departamento;

namespace FrontEndPerfilesSA.Pages.Departamento
{
    public partial class Departamento : Page
    {
        private readonly DepartamentoService _departamentoService = new DepartamentoService();

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var depto = new DepartamentoModel
            {
                Nombre = txtNombre.Text.Trim(),
                Activo = chkActivo.Checked
            };

            var resultado = _departamentoService.InsertarDepartamento(depto);

            if (resultado)
            {
                lblResultado.Text = "Departamento guardado correctamente.";
                lblResultado.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblResultado.Text = "Error al guardar el departamento.";
                lblResultado.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
