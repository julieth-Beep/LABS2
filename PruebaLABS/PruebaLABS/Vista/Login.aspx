<%@ Page Title="Incio secion" Language="C#" MasterPageFile="~/Vista/Vistas.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PruebaLABS.Vista.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body, html {
            height: 100%;
            background-color: #f4f6f5;
            margin: 0;
            padding: 0;
        }

        .login-wrapper {
            min-height: calc(100vh - 120px); 
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 25px;
        }

        .card {
            width: 100%;
            max-width: 410px;
            border: none;
            border-radius: 15px;
        }

        .brand-icon {
            font-size: 65px;
            color: #2E7D32;
            animation: moveTruck 2s ease-in-out infinite;
        }

        @keyframes moveTruck {
            0%, 100% { transform: translateX(0); }
            50% { transform: translateX(6px); }
        }

        .btn-custom {
            background-color: #2E7D32;
            color: white;
            border-radius: 8px;
            transition: 0.3s ease;
            font-weight: 500;
        }

        .btn-custom:hover {
            background-color: #27662C;
        }

        a {
            color: #2E7D32;
            font-weight: 500;
        }

        .forgot-password {
            text-align: right;
            margin-top: 8px;
        }

      
        @media (max-width: 480px) {
            .brand-icon { font-size: 50px; }
            .card { padding: 20px !important; }
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentBodys" runat="server">

    <div class="login-wrapper">
        <div class="card shadow-lg p-4">
            
            <div class="text-center mb-4">
                <i class="bi bi-truck brand-icon"></i>
                <h3 class="mt-3 fw-bold text-dark">Iniciar Sesión</h3>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblUser" runat="server" Text="Usuario" CssClass="form-label fw-semibold"></asp:Label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-person"></i></span>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Digite email" TextMode="Email"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblpass" runat="server" Text="Password" CssClass="form-label fw-semibold"></asp:Label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-lock"></i></span>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" placeholder="Digite password" TextMode="Password"></asp:TextBox>
                </div>

                <div class="forgot-password">
                    <asp:HyperLink ID="lnkRecuperar" runat="server"
                                   NavigateUrl="RecuperarContraseña.aspx"
                                   Text="¿Olvidó su contraseña?" />
                </div>
            </div>

            <div class="d-grid mt-3">
                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" 
                            OnClick="bntIngresar_Click" CssClass="btn btn-custom" />
            </div>

            <p class="mt-3 text-center">
                ¿No tienes cuenta?
                <a href="Registro.aspx">Regístrate aquí</a>
            </p>

            <asp:Label ID="lnlMensaje" runat="server" Text=""></asp:Label>

        </div>
    </div>

</asp:Content>
