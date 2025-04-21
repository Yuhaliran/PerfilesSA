<%@ Page Title="Modificar Departamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarDepartamento.aspx.cs" Inherits="FrontEndPerfilesSA.Pages.ModificarDepartamento.ModificarDepartamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Modificar Departamentos</h2>

    <div class="form-group">
        <label for="txtBuscar">Buscar por Nombre:</label>
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" />
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn" />
    </div>

    <asp:GridView ID="gvDepartamentos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdDepartamento" 
        AllowPaging="true" PageSize="5" 
        OnPageIndexChanging="gvDepartamentos_PageIndexChanging"
        OnRowEditing="gvDepartamentos_RowEditing" 
        OnRowCancelingEdit="gvDepartamentos_RowCancelingEdit"
        OnRowUpdating="gvDepartamentos_RowUpdating"
        CssClass="grid">
        <Columns>
            <asp:BoundField DataField="IdDepartamento" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre del Departamento" />
            <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
            <asp:CommandField ShowEditButton="True" EditText="Editar" UpdateText="Guardar" CancelText="Cancelar" />
        </Columns>
    </asp:GridView>

    <style>
        .form-group {
            margin-bottom: 15px;
        }

        .grid {
            width: 100%;
            margin: auto;
            font-family: Arial;
            border-collapse: collapse;
        }

        .grid th, .grid td {
            padding: 8px;
            border: 1px solid #ccc;
        }

        .grid th {
            background-color: #007bff;
            color: white;
        }

        .btn {
            margin-top: 5px;
            padding: 6px 15px;
            background-color: #28a745;
            color: white;
            border: none;
            border-radius: 4px;
        }
    </style>
</asp:Content>
