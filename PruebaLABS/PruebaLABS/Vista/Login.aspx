<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PruebaLABS.Vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LABS</title>

    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f4f6f5;
        }

        .card {
            border: none;
            border-radius: 15px;
        }

        .brand-icon {
            font-size: 65px;
            color: #2E7D32;
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
            text-decoration: none;
        }

        a:hover {
            text-decoration: underline;
            color: #1B4A20;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container d-flex align-items-center justify-content-center vh-100">
            <div class="card shadow-lg p-4" style="max-width: 410px; width: 100%;">
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
                </div>

                <div class="d-grid mt-3">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="bntIngresar_Click" CssClass="btn btn-custom" />
                </div>

                <p class="mt-3 text-center">
                    ¿No tienes cuenta?
                    <a href="Registro.aspx">Regístrate aquí</a>
                </p>

                <asp:Label ID="lnlMensaje" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>