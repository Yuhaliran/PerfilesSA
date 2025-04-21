<%@ Page Title="Empleado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="FrontEndPerfilesSA.Pages.Empleado.Empleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Registrar Empleado</h2>

        <div class="form-group">
            <label for="txtNombres">Nombres</label>
            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtApellidos">Apellidos</label>
            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
            <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Sexo</label><br />
            <asp:RadioButton ID="rbtnMasculino" runat="server" GroupName="Sexo" Text="Masculino" />
            <asp:RadioButton ID="rbtnFemenino" runat="server" GroupName="Sexo" Text="Femenino" />
        </div>

        <div class="form-group">
            <label for="txtDireccion">Dirección</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtNIT">NIT</label>
            <asp:TextBox ID="txtNIT" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtDPI">DPI</label>
            <asp:TextBox ID="txtDPI" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="ddlDepartamento">Departamento</label>
            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtFechaIngreso">Fecha de Ingreso</label>
            <asp:TextBox ID="txtFechaIngreso" runat="server" TextMode="Date" CssClass="form-control" />
        </div>

        <div class="form-group checkbox">
            <asp:CheckBox ID="chkActivo" runat="server" />
            <label for="chkActivo">Activo</label>
        </div>

        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" CssClass="btn" />

        <div class="resultado">
            <asp:Label ID="lblResultado" runat="server" ForeColor="Green" />
        </div>
    </div>

    <style>
        .form-container {
            width: 500px;
            margin: 30px auto;
            padding: 25px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-family: Arial;
            background-color: #f9f9f9;
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            font-weight: bold;
            display: block;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            font-size: 14px;
        }

        .checkbox {
            display: flex;
            align-items: center;
        }

        .checkbox label {
            margin-left: 5px;
        }

        .btn {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: white;
            font-weight: bold;
            border: none;
            border-radius: 4px;
            margin-top: 10px;
        }

        .resultado {
            margin-top: 15px;
            text-align: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
