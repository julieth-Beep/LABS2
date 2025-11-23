<%@ Page Title="Registro de Cliente" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="PruebaLABS.Vista.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LABS - Registro de Cliente</title>

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
            border: none;
            width: 100%;
            max-width: 600px;
        }

        .card-header {
            background: white;
            color: #333;
            text-align: center;
            padding: 30px 30px 10px 30px;
            border-radius: 15px 15px 0 0 !important;
            border: none;
            border-bottom: 1px solid #e9ecef;
        }

        .brand-icon {
            font-size: 55px;
            margin-bottom: 10px;
            display: block;
            color: #2E7D32;
        }

        .card-header h2 {
            font-weight: 600;
            font-size: 26px;
            margin: 0;
            color: #333;
        }

        .card-header p {
            font-size: 15px;
            color: #666;
            margin: 8px 0 0 0;
        }

        .card-body {
            padding: 30px;
        }

        .form-group {
            margin-bottom: 18px;
        }

        .form-label {
            font-weight: 500;
            color: #333;
            margin-bottom: 6px;
            font-size: 13px;
            display: block;
        }

        .input-group {
            position: relative;
        }

        .input-group-text {
            background-color: #f8f9fa;
            border: 1px solid #e9ecef;
            border-right: none;
            border-radius: 8px 0 0 8px;
        }

        .form-control {
            border: 1px solid #e9ecef;
            border-left: none;
            border-radius: 0 8px 8px 0;
            padding: 10px 12px;
            font-size: 14px;
            transition: all 0.3s ease;
            height: 45px;
        }

            .form-control:focus {
                border-color: #2E7D32;
                box-shadow: 0 0 0 0.2rem rgba(46, 125, 50, 0.15);
            }

        .input-group:focus-within .input-group-text {
            border-color: #2E7D32;
        }

        .btn-register {
            background: #2E7D32;
            border: none;
            color: white;
            padding: 12px 25px;
            border-radius: 8px;
            font-weight: 500;
            font-size: 15px;
            transition: all 0.3s ease;
            width: 100%;
            margin-top: 10px;
            height: 45px;
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
                font-weight: 500;
                text-decoration: none;
                transition: color 0.3s ease;
                font-size: 14px;
            }

                .login-link a:hover {
                    color: #1B5E20;
                    text-decoration: underline;
                }

        .alert-message {
            border-radius: 8px;
            padding: 10px 12px;
            margin-top: 12px;
            font-weight: 500;
            font-size: 14px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="register-container">
        <div class="register-card">

            <div class="card-header">
                <i class="bi bi-person-plus-fill brand-icon"></i>
                <h2>Registro de Cliente</h2>
                <p>Registra tu información para solicitar servicios de transporte</p>
            </div>

            <div class="card-body">
                <div class="form-group">
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento *" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-card-text"></i></span>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Número de documento" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombres *" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-person"></i></span>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Tus nombres" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellidos *" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-person"></i></span>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Tus apellidos" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa *" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-building"></i></span>
                        <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control" placeholder="Nombre de tu empresa" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-telephone"></i></span>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Número de teléfono"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCorreo" runat="server" Text="Correo Electrónico *" CssClass="form-label"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-envelope"></i></span>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@gmail.com" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse como Cliente"
                        CssClass="btn-register" OnClick="btnRegistrar_Click" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="alert-message"></asp:Label>
                </div>

                <div class="login-link">
                    <span>¿Ya tienes cuenta? </span>
                    <a href="Inicio.aspx">Volver al inicio</a>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>