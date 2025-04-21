<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarEmpleado.aspx.cs" Inherits="FrontEndPerfilesSA.Pages.ModificarEmpleado.ModificarEmpleado" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Modificar Empleado</h2>

    <div class="form-message">
        <asp:Label ID="lblMensaje" runat="server" Visible="false" />
    </div>

    <h4>Búsqueda de Empleado</h4>
    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Nombre, DPI o NIT" />
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Empleado" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />

    <hr />

    <asp:GridView ID="gvEmpleados" runat="server" EnableViewState="true" AutoGenerateColumns="False"
                  CssClass="grid" AllowPaging="True" PageSize="5"
                  OnRowCommand="gvEmpleados_RowCommand" OnPageIndexChanging="gvEmpleados_PageIndexChanging">

        <Columns>
            <asp:BoundField DataField="IdEmpleado" HeaderText="ID" />
            <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
            <asp:BoundField DataField="DPI" HeaderText="DPI" />
            <asp:BoundField DataField="NIT" HeaderText="NIT" />

            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("Activo") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="NombreDepartamento" HeaderText="Departamento" />

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar"
                        CommandName="Modificar" CommandArgument='<%# Eval("IdEmpleado") %>'
                        CssClass="btn btn-warning btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


    <hr />
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <ContentTemplate>
            <div class="form-container">
                <h4 class="form-title">Formulario de Edición</h4>

                <asp:HiddenField ID="hfIdEmpleado" runat="server" />

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Nombres:" runat="server" AssociatedControlID="txtNombres" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtNombres" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Apellidos:" runat="server" AssociatedControlID="txtApellidos" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtApellidos" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="DPI:" runat="server" AssociatedControlID="txtDPI" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtDPI" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="NIT:" runat="server" AssociatedControlID="txtNIT" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtNIT" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Dirección:" runat="server" AssociatedControlID="txtDireccion" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtDireccion" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Fecha Nacimiento:" runat="server" AssociatedControlID="txtFechaNacimiento" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Fecha Ingreso:" runat="server" AssociatedControlID="txtFechaIngreso" />
                    </div>
                    <div class="form-input">
                        <asp:TextBox ID="txtFechaIngreso" runat="server" TextMode="Date" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Departamento:" runat="server" AssociatedControlID="ddlDepartamento" />
                    </div>
                    <div class="form-input">
                        <asp:DropDownList ID="ddlDepartamento" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione un departamento" Value="" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Sexo:" runat="server" AssociatedControlID="ddlSexo" />
                    </div>
                    <div class="form-input">
                        <asp:DropDownList ID="ddlSexo" runat="server">
                            <asp:ListItem Text="Masculino" Value="true" />
                            <asp:ListItem Text="Femenino" Value="false" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">
                        <asp:Label Text="Activo:" runat="server" AssociatedControlID="chkActivo" />
                    </div>
                    <div class="form-input">
                        <asp:CheckBox ID="chkActivo" runat="server" />
                    </div>
                </div>

                <div class="form-actions">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </ContentTemplate>

    
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

        .form-container {
            max-width: 800px;
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
        }

        .form-row {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 15px;
        }

        .form-label {
            width: 30%;
            font-weight: bold;
            padding-top: 6px;
        }

        .form-input {
            width: 70%;
        }

        .form-input input,
        .form-input select {
            width: 100%;
            padding: 6px;
            border-radius: 4px;
            border: 1px solid #ccc;
        }

        .form-actions {
            text-align: right;
            margin-top: 20px;
        }

        .form-title {
            margin-bottom: 20px;
            font-size: 1.5em;
            color: #343a40;
        }

        .form-message {
            margin-top: 15px;
        }

        .success-message {
            color: #28a745;
            font-weight: bold;
        }

        .error-message {
            color: #dc3545;
            font-weight: bold;
        }
    </style>
</asp:Content>
