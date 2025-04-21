using FrontEndPerfilesSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using EmpleadoModel = FrontEndPerfilesSA.Models.Empleado;
using DepartamentoModel = FrontEndPerfilesSA.Models.Departamento;

namespace FrontEndPerfilesSA.Pages.ModificarEmpleado
{
    public partial class ModificarEmpleado : System.Web.UI.Page
    {
        private readonly EmpleadoService empleadoService = new EmpleadoService();
        private readonly DepartamentoService departamentoService = new DepartamentoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEmpleados(null);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEmpleados(txtBuscar.Text.Trim());
        }

        private void CargarEmpleados(string filtro)
        {
            List<EmpleadoModel> empleados = string.IsNullOrWhiteSpace(filtro)
                ? empleadoService.ObtenerEmpleados()
                : empleadoService.BuscarEmpleados(filtro);

            List<DepartamentoModel> departamentos = departamentoService.ObtenerDepartamentos();

            AsignarNombreDepartamento(empleados, departamentos);

            gvEmpleados.DataSource = empleados;
            gvEmpleados.DataBind();
        }



        protected void gvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmpleados.PageIndex = e.NewPageIndex;
            CargarEmpleados(txtBuscar.Text.Trim());
        }

        private void AsignarNombreDepartamento(List<EmpleadoModel> empleados, List<DepartamentoModel> departamentos)
        {
            foreach (var empleado in empleados)
            {
                var depto = departamentos.FirstOrDefault(d => d.IdDepartamento == empleado.IdDepartamento);
                empleado.NombreDepartamento = depto?.Nombre?? "Sin asignar";
            }
        }

        private void CargarDropDownDepartamentos(int? idSeleccionado = null)
        {
            List<DepartamentoModel> departamentos = departamentoService.ObtenerDepartamentos();
            ddlDepartamento.Items.Clear();
            ddlDepartamento.DataSource = departamentos;
            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "IdDepartamento";
            ddlDepartamento.DataBind();

            if (idSeleccionado.HasValue)
            {
                ddlDepartamento.SelectedValue = idSeleccionado.Value.ToString();
            }
        }


        protected void gvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int idEmpleado = Convert.ToInt32(e.CommandArgument);
                var empleado = empleadoService.BuscarEmpleadoPorId(idEmpleado).Find(x => x.IdEmpleado == idEmpleado);

                if (empleado != null)
                {
                    hfIdEmpleado.Value = empleado.IdEmpleado.ToString();
                    txtNombres.Text = empleado.Nombres;
                    txtApellidos.Text = empleado.Apellidos;
                    txtDPI.Text = empleado.DPI;
                    txtNIT.Text = empleado.NIT;
                    txtDireccion.Text = empleado.Direccion;
                    txtFechaNacimiento.Text = empleado.FechaNacimiento.ToString("yyyy-MM-dd");
                    txtFechaIngreso.Text = empleado.FechaIngreso.ToString("yyyy-MM-dd");
                    CargarDropDownDepartamentos(empleado.IdDepartamento);
                    ddlSexo.SelectedValue = empleado.Sexo.ToString().ToLower();
                    chkActivo.Checked = empleado.Activo;
                }

            }
        }

        private void LimpiarFormulario()
        {
            hfIdEmpleado.Value = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDPI.Text = "";
            txtNIT.Text = "";
            txtDireccion.Text = "";
            txtFechaNacimiento.Text = "";
            txtFechaIngreso.Text = "";
            ddlDepartamento.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;
            chkActivo.Checked = false;
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                EmpleadoModel empleado = new EmpleadoModel
                {
                    IdEmpleado = int.Parse(hfIdEmpleado.Value),
                    Nombres = txtNombres.Text.Trim(),
                    Apellidos = txtApellidos.Text.Trim(),
                    DPI = txtDPI.Text.Trim(),
                    NIT = txtNIT.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                    FechaIngreso = DateTime.Parse(txtFechaIngreso.Text),
                    IdDepartamento = int.Parse(ddlDepartamento.SelectedValue),
                    Sexo = bool.Parse(ddlSexo.SelectedValue),
                    Activo = chkActivo.Checked
                };

                bool exito = empleadoService.ActualizarEmpleado(empleado);

                if (exito)
                {
                    lblMensaje.Text = "Empleado actualizado correctamente.";
                    lblMensaje.CssClass = "success-message";
                    LimpiarFormulario(); // Limpia solo si fue exitoso
                }
                else
                {
                    lblMensaje.Text = "No se pudo actualizar el empleado.";
                    lblMensaje.CssClass = "error-message";
                }

                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar el empleado: " + ex.Message;
                lblMensaje.CssClass = "error-message";
                lblMensaje.Visible = true;
            }
            finally
            {
                CargarEmpleados(txtBuscar.Text.Trim()); // Siempre actualiza la tabla
            }
        }

    }
}
