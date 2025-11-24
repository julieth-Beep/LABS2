<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PruebaLABS.Vista.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>LABS - Iniciar Sesión</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background: #f4f6f5;
            font-family: 'Segoe UI';
            height: 100vh;
            margin: 0;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .login-container {
            width: 100%;
            max-width: 1050px;
            background: #ffffff;
            border-radius: 25px;
            box-shadow: 0 25px 60px rgba(0,0,0,0.18);
            display: flex;
            overflow: hidden;
        }

        .login-left {
            width: 50%;
            padding: 60px 50px 50px 50px;
        }

        .title-welcome {
            font-size: 34px;
            font-weight: 800;
            color: #2E7D32;
        }

        .title-login {
            font-size: 22px;
            font-weight: 600;
            color: #444;
            margin-bottom: 30px;
        }

        .form-label {
            color: #2E7D32;
            font-weight: 600;
            margin-top: 15px;
            font-size: 15px;
        }

        .input-group-text {
            background: #eef3ef;
            border: 1px solid #d8d8d8;
            border-right: none;
            border-radius: 12px 0 0 12px;
        }

        .form-control {
            border: 1px solid #d8d8d8;
            border-left: none;
            border-radius: 0 12px 12px 0;
            height: 50px;
            font-size: 15px;
        }

            .form-control:focus {
                border-color: #2E7D32 !important;
                box-shadow: 0 0 0 0.12rem rgba(46,125,50,0.25);
            }

        .btn-login {
            background: #2E7D32;
            border: none;
            color: white;
            border-radius: 10px;
            padding: 14px;
            font-weight: 600;
            width: 100%;
            margin-top: 25px;
            transition: 0.2s;
        }

            .btn-login:hover {
                background: #27662C;
            }

        .register-link {
            color: #2E7D32;
            font-weight: 600;
        }

        .error-text {
            margin-top: 10px;
            color: #d91c1c;
            font-weight: 600;
            text-align: center;
        }

        .login-right {
            width: 50%;
            background: #e8f3ea;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 0;
        }

            .login-right img {
                width: 100%;
                height: 100%;
                object-fit: cover; 
                object-position: center; 
                border-radius: 0 25px 25px 0; 
                box-shadow: 0 10px 25px rgba(0,0,0,0.22);
            }


        @media(max-width: 950px) {
            .login-container {
                flex-direction: column;
                max-width: 480px;
            }

            .login-right {
                display: none;
            }

            .login-left {
                width: 100%;
                padding: 40px;
            }
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">

        <div class="login-container">

            <!-- LEFT SIDE -->
            <div class="login-left">

                <div class="mb-3">
                    <div class="title-welcome">Bienvenido a LABS</div>
                    <div class="title-login">Inicia sesión para continuar</div>
                </div>

                <asp:Label ID="lblUser" runat="server" Text="Usuario" CssClass="form-label"></asp:Label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-envelope"></i></span>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Digite email" TextMode="Email"></asp:TextBox>
                </div>

                <asp:Label ID="lblpass" runat="server" Text="Password" CssClass="form-label"></asp:Label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-lock"></i></span>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" placeholder="Digite password" TextMode="Password"></asp:TextBox>
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-login" OnClick="bntIngresar_Click" />

                <p class="mt-3 text-center">
                    ¿No tienes cuenta?
                    <a href="Registro.aspx" class="register-link">Regístrate aquí</a>
                </p>

                <asp:Label ID="lnlMensaje" runat="server" CssClass="error-text"></asp:Label>

            </div>

            <div class="login-right">
                <img src="https://media.istockphoto.com/id/1703227595/es/foto/fila-de-los-grandes-camiones-semirremolques-tractores-con-semirremolques-de-furgoneta-seca.jpg?s=612x612&w=0&k=20&c=IRsQ6SPVoHv3dfCasuHD5r4w-4pLoadHmKskgpNyLK8="
                     />
            </div>

        </div>

    </form>

</body>
</html>
