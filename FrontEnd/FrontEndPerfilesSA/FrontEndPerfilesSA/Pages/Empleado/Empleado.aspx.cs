using FrontEndPerfilesSA.Helpers;
using FrontEndPerfilesSA.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Services.Description;
using DepartamentoModel = FrontEndPerfilesSA.Models.Departamento;
using EmpleadoModel = FrontEndPerfilesSA.Models.Empleado;

namespace FrontEndPerfilesSA.Pages.Empleado
{
    public partial class Empleado : System.Web.UI.Page
    {
        private DepartamentoService departamentoService = new DepartamentoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropDownDepartamentos();
            }
        }


        private void CargarDropDownDepartamentos()
        {
            List<DepartamentoModel> departamentos = departamentoService.ObtenerDepartamentos();
            ddlDepartamento.Items.Clear();
            ddlDepartamento.DataSource = departamentos;
            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "IdDepartamento";
            ddlDepartamento.DataBind();

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevoEmpleado = new EmpleadoModel
                {
                    Nombres = txtNombres.Text.Trim(),
                    Apellidos = txtApellidos.Text.Trim(),
                    FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                    Sexo = rbtnMasculino.Checked,
                    Direccion = txtDireccion.Text.Trim(),
                    NIT = txtNIT.Text.Trim(),
                    DPI = txtDPI.Text.Trim(),
                    FechaIngreso = DateTime.Parse(txtFechaIngreso.Text),
                    IdDepartamento = int.Parse(ddlDepartamento.SelectedValue),
                    Activo = chkActivo.Checked
                };

                HttpResponseMessage respuesta = ApiHelper.Post("api/empleado/insertar", nuevoEmpleado);

                if (respuesta.IsSuccessStatusCode)
                {
                    lblResultado.ForeColor = System.Drawing.Color.Green;
                    lblResultado.Text = "Empleado registrado exitosamente.";
                    LimpiarFormulario();
                }
                else
                {
                    lblResultado.ForeColor = System.Drawing.Color.Red;
                    lblResultado.Text = "Error al insertar";
                }
            }
            catch (Exception ex)
            {
                lblResultado.ForeColor = System.Drawing.Color.Red;
                lblResultado.Text = "Error al insertar";
            }
        }

        private void LimpiarFormulario()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtNIT.Text = "";
            txtDPI.Text = "";
            txtFechaNacimiento.Text = "";
            txtFechaIngreso.Text = "";
            rbtnMasculino.Checked = false;
            rbtnFemenino.Checked = false;
            chkActivo.Checked = false;
            ddlDepartamento.SelectedIndex = 0;
        }
    }
}
