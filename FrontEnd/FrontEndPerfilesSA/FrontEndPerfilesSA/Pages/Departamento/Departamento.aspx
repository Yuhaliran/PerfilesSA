<%@ Page Title="Departamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departamento.aspx.cs" Inherits="FrontEndPerfilesSA.Pages.Departamento.Departamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Registrar Departamento</h2>

        <div class="form-group">
            <label for="txtNombre">Nombre del Departamento</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
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
            width: 400px;
            margin: 30px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-family: Arial;
            background-color: #f9f9f9;
        }

        .form-container h2 {
            text-align: center;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            display: block;
            font-weight: bold;
        }

        input[type="text"] {
            width: 100%;
            padding: 8px;
        }

        .checkbox {
            display: flex;
            align-items: center;
        }

        .checkbox input {
            margin-right: 8px;
        }

        .btn {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: white;
            font-weight: bold;
            border: none;
            border-radius: 4px;
        }

        .resultado {
            margin-top: 15px;
            text-align: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
