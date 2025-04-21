using FrontEndPerfilesSA.Models;
using FrontEndPerfilesSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DepartamentoModel = FrontEndPerfilesSA.Models.Departamento;

namespace FrontEndPerfilesSA.Pages.ModificarDepartamento
{
    public partial class ModificarDepartamento : System.Web.UI.Page
    {
        private DepartamentoService _service = new DepartamentoService();
        private static List<DepartamentoModel> _departamentosFiltrados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDepartamentos();
            }
        }

        private void CargarDepartamentos(string filtro = "")
        {
            var lista = _service.ObtenerDepartamentos();

            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista.Where(d => d.Nombre.ToLower().Contains(filtro.ToLower())).ToList();
            }

            _departamentosFiltrados = lista; 
            gvDepartamentos.DataSource = _departamentosFiltrados;
            gvDepartamentos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            CargarDepartamentos(filtro);
        }

        protected void gvDepartamentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepartamentos.PageIndex = e.NewPageIndex;
            gvDepartamentos.DataSource = _departamentosFiltrados;
            gvDepartamentos.DataBind();
        }

        protected void gvDepartamentos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDepartamentos.EditIndex = e.NewEditIndex;
            gvDepartamentos.DataSource = _departamentosFiltrados;
            gvDepartamentos.DataBind();
        }

        protected void gvDepartamentos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDepartamentos.EditIndex = -1;
            gvDepartamentos.DataSource = _departamentosFiltrados;
            gvDepartamentos.DataBind();
        }

        protected void gvDepartamentos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvDepartamentos.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvDepartamentos.Rows[e.RowIndex];

            string nuevoNombre = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            bool activo = ((CheckBox)row.Cells[2].Controls[0]).Checked;

            var depto = new DepartamentoModel
            {
                IdDepartamento = id,
                Nombre = nuevoNombre,
                Activo = activo
            };

            bool actualizado = _service.ActualizarDepartamento(depto);

            gvDepartamentos.EditIndex = -1;
            CargarDepartamentos(txtBuscar.Text.Trim());
        }
    }
}
