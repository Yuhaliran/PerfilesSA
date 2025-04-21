<%@ Page Title="Reporte de Empleados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteEmpleados.aspx.cs" Inherits="FrontEndPerfilesSA.Pages.Reportes.ReporteEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .table-custom {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
            font-size: 14px;
        }

        .table-custom th, .table-custom td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        .table-custom th {
            background-color: #4CAF50;
            color: white;
        }

        .table-custom tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-right: 10px;
            font-size: 14px;
            font-weight: 600;
            text-align: center;
            cursor: pointer;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 4px;
            text-decoration: none;
        }

        .btn:hover {
            background-color: #0056b3;
        }

    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <h2 class="titulo">Reporte de Empleados</h2>

    <asp:Label ID="lblFechaInicio" runat="server" Text="Desde:"></asp:Label>
    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

    <asp:Label ID="lblFechaFin" runat="server" Text="Hasta:"></asp:Label>
    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-secondary" OnClick="btnFiltrar_Click" />


    <ContentTemplate>
        <asp:GridView ID="gvReporte" runat="server" CssClass="table-custom"
            AutoGenerateColumns="False" GridLines="None">
                <Columns>
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="Edad" HeaderText="Edad" />
        
                <asp:TemplateField HeaderText="Activo">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActivo" runat="server" Enabled="false" Checked='<%# Eval("Activo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="NombreDepartamento" HeaderText="Departamento" />
            </Columns>
        </asp:GridView>
        <br>
        <br>
        <asp:Button ID="btnCargar" runat="server" CssClass="btn" Text="Cargar Empleados" OnClick="btnCargar_Click" />

    </ContentTemplate>

</asp:Content>
