<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionesCliente.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LABS - Opciones Cliente</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f4f6f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .options-container {
            min-height: 100vh;
            padding: 30px 20px;
        }

        .content-card {
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            border: none;
            margin-bottom: 30px;
        }

        .card-header-custom {
            background: white;
            color: #333;
            text-align: center;
            padding: 25px 30px 15px 30px;
            border-radius: 15px 15px 0 0 !important;
            border: none;
            border-bottom: 1px solid #e9ecef;
        }

        .brand-icon {
            font-size: 40px;
            margin-bottom: 10px;
            display: block;
            color: #2E7D32;
        }

        .card-header-custom h3 {
            font-weight: 600;
            font-size: 24px;
            margin: 0;
            color: #333;
        }

        .card-body-custom {
            padding: 25px;
        }

        .table-custom {
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
        }

        .table-custom thead th {
            background-color: #2E7D32;
            color: white;
            border: none;
            padding: 15px;
            font-weight: 600;
            text-align: center;
        }

        .table-custom tbody td {
            padding: 12px 15px;
            border-bottom: 1px solid #e9ecef;
            text-align: center;
        }

        .table-custom tbody tr:hover {
            background-color: rgba(46, 125, 50, 0.05);
        }

        .form-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
        }

        .form-group {
            margin-bottom: 15px;
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

        .full-width {
            grid-column: 1 / -1;
        }

        @media (max-width: 768px) {
            .form-grid {
                grid-template-columns: 1fr;
            }

            .card-body-custom {
                padding: 20px 15px;
            }

            .card-header-custom {
                padding: 20px 15px 10px 15px;
            }

                .card-header-custom h3 {
                    font-size: 22px;
                }
        }

        @media (max-width: 576px) {
            .options-container {
                padding: 15px;
            }

            .card-body-custom {
                padding: 15px 10px;
            }
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
    <div class="options-container">
        <div class="container">
            <div class="row">
                <%--Flota de Vehículos con Caracteristicas--%>
                <div class="col-lg-7 mb-4">
                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-truck brand-icon"></i>
                            <h3>Flota de Vehículos</h3>
                            <p class="text-muted mb-0">Vehículos disponibles para tus envíos</p>
                        </div>
                        <div class="card-body-custom">
                            <div class="table-responsive">
                                <asp:GridView
                                    ID="gvFlota"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-custom"
                                    HeaderStyle-CssClass="table-success text-center"
                                    RowStyle-CssClass="text-center"
                                    BorderStyle="None">

                                    <Columns>
                                        <asp:BoundField DataField="placa" HeaderText="Placa" />
                                        <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="capacidad" HeaderText="Capacidad" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <%--Registrar Viaje--%>
                <div class="col-lg-5 mb-4">
                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-plus-circle brand-icon"></i>
                            <h3>Solicitar Viaje</h3>
                            <p class="text-muted mb-0">Solicita un servicio de transporte</p>
                        </div>
                        <div class="card-body-custom">
                            <div class="form-grid">
                                
                                <div class="form-group full-width">
                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-building"></i></span>
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">Registre su empresa</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group full-width">
                                    <asp:Label ID="lblMotivo" runat="server" Text="Motivo del Viaje" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-chat-text"></i></span>
                                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" placeholder="Motivo del transporte"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblOrigen" runat="server" Text="Origen" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-geo-alt"></i></span>
                                        <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control" placeholder="Ciudad origen"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblDestino" runat="server" Text="Destino" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-geo-alt-fill"></i></span>
                                        <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" placeholder="Ciudad destino"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblFechaSalida" runat="server" Text="Fecha Salida" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-calendar"></i></span>
                                        <asp:TextBox ID="txtFechaSalida" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblFechaLlegada" runat="server" Text="Fecha Llegada" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-calendar-check"></i></span>
                                        <asp:TextBox ID="txtFechaLlegada" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group full-width">
                                    <asp:Label ID="lblTipoCarga" runat="server" Text="Tipo de Carga" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-box-seam"></i></span>
                                        <asp:DropDownList ID="ddlTipoCarga" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">Seleccione tipo de carga</asp:ListItem>
                                            <asp:ListItem Value="1">Mercancía General</asp:ListItem>
                                            <asp:ListItem Value="2">Perecederos</asp:ListItem>
                                            <asp:ListItem Value="3">Material Peligroso</asp:ListItem>
                                            <asp:ListItem Value="4">Maquinaria Pesada</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group full-width">
                                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones" CssClass="form-label"></asp:Label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-chat-left-text"></i></span>
                                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Observaciones adicionales..."></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="full-width">
                                <asp:Button ID="btnSolicitarViaje" runat="server" Text="Solicitar Viaje"
                                    CssClass="btn-register" />
                            </div>

                            <div class="full-width">
                                <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="alert-message"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>