<%@ Page Title="Registro de nuevos Usuarios" Language="C#" 
    MasterPageFile="~/Vista/vistas.Master" AutoEventWireup="true" 
    CodeBehind="Registro.aspx.cs" Inherits="PruebaLABS.Vista.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>LABS - Registro</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

<style>
    body {
        background-color: #f4f6f5;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .register-container {
        min-height: 100vh;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 20px;
    }

    .register-card {
        background: white;
        border-radius: 15px;
        box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
        width: 100%;
        max-width: 800px;
    }

    .card-header {
        background: white;
        color: #333;
        text-align: center;
        padding: 30px 30px 10px 30px;
        border-bottom: 1px solid #e9ecef;
    }

    .brand-icon {
        font-size: 55px;
        margin-bottom: 10px;
        color: #2E7D32;
    }

    .card-body {
        padding: 30px;
    }

    .form-grid {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 18px;
    }

    .input-group-text {
        background-color: #f8f9fa;
        border-radius: 8px 0 0 8px;
    }

    .form-control {
        border-radius: 0 8px 8px 0;
        height: 45px;
    }

    .btn-register {
        background: #2E7D32;
        color: white;
        font-size: 15px;
        width: 100%;
        height: 45px;
        border-radius: 8px;
    }

    .btn-register:hover {
        background: #27662C;
    }

    .login-link {
        text-align: center;
        margin-top: 25px;
        padding-top: 20px;
        border-top: 1px solid #e9ecef;
    }

    .login-link a {
        color: #2E7D32;
    }
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

<div class="register-container">
    <div class="register-card">

        <div class="card-header">
            <i class="bi bi-person-plus-fill brand-icon"></i>
            <h2>Registrarse</h2>
            <p>Crea tu cuenta en LABS</p>
        </div>

        <div class="card-body">

            <div class="form-grid">

                <div>
                    <asp:Label Text="Documento" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-card-text"></i></span>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Número de documento"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label Text="Teléfono" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-telephone"></i></span>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Número de teléfono"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label Text="Nombres" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-person"></i></span>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Tus nombres"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label Text="Apellidos" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-person"></i></span>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Tus apellidos"></asp:TextBox>
                    </div>
                </div>

                <div class="full-width">
                    <asp:Label Text="Correo Electrónico" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-envelope"></i></span>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@ejemplo.com"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label Text="Contraseña" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-lock"></i></span>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Mínimo 6 caracteres"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label Text="Confirmar Contraseña" runat="server" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-lock-fill"></i></span>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Repetir contraseña"></asp:TextBox>
                    </div>
                </div>

            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse"
                CssClass="btn-register mt-3" OnClick="btnRegistrar_Click" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert-message d-block mt-3"></asp:Label>

            <div class="login-link">
                <span>¿Ya tienes cuenta? </span>
                <a href="Login.aspx">Inicia sesión aquí</a>
            </div>

        </div>

    </div>
</div>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
