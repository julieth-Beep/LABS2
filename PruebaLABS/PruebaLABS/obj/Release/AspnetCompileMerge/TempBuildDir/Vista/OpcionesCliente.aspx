<%@ Page Title="Panel Cliente" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionesCliente.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesCliente" %>

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

        .sidebar {
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            padding: 0;
            margin-bottom: 30px;
            position: sticky;
            top: 20px;
            height: fit-content;
        }

        .sidebar-header {
            padding: 20px 20px 15px 20px;
            text-align: center;
            border-bottom: 1px solid #e9ecef;
        }

        .sidebar-menu {
            padding: 15px 0;
        }

        .sidebar-item {
            padding: 12px 25px;
            border: none;
            background: none;
            width: 100%;
            text-align: left;
            font-weight: 500;
            color: #333;
            transition: all 0.3s ease;
            border-left: 4px solid transparent;
            font-size: 14px;
        }

            .sidebar-item:hover {
                background-color: rgba(46, 125, 50, 0.1);
                color: #2E7D32;
                border-left: 4px solid #2E7D32;
            }

            .sidebar-item.active {
                background-color: rgba(46, 125, 50, 0.15);
                color: #2E7D32;
                border-left: 4px solid #2E7D32;
                font-weight: 600;
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
            width: 100%;
        }

            .table-custom thead th {
                background-color: #2E7D32;
                color: white;
                border: none;
                padding: 15px;
                font-weight: 600;
                text-align: center;
                font-size: 14px;
            }

            .table-custom tbody td {
                padding: 12px 15px;
                border-bottom: 1px solid #e9ecef;
                text-align: center;
                font-size: 13px;
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

        .modal-content {
            border-radius: 15px;
            border: none;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
        }

        .modal-header {
            border-radius: 15px 15px 0 0;
            border-bottom: 1px solid #dee2e6;
        }

        .modal-footer {
            border-radius: 0 0 15px 15px;
            border-top: 1px solid #dee2e6;
        }

        .btn-close:focus {
            box-shadow: none;
        }

        .modal.fade .modal-dialog {
            transform: scale(0.8);
            transition: transform 0.3s ease-out;
        }

        .modal.show .modal-dialog {
            transform: scale(1);
        }

        .bi-check-circle {
            animation: pulse 1.5s infinite;
        }

        @keyframes pulse {
            0% {
                transform: scale(1);
            }

            50% {
                transform: scale(1.1);
            }

            100% {
                transform: scale(1);
            }
        }

        .alert-info {
            background-color: #d1ecf1;
            border-color: #bee5eb;
            color: #0c5460;
        }

        .badge-estado {
            padding: 6px 12px;
            border-radius: 20px;
            font-weight: 600;
            font-size: 12px;
        }

        .badge-pendiente {
            background-color: #fff3cd;
            color: #856404;
        }

        .badge-encurso {
            background-color: #d1ecf1;
            color: #0c5460;
        }

        .badge-completado {
            background-color: #d4edda;
            color: #155724;
        }

        .table-pedidos {
            font-size: 13px;
        }

            .table-pedidos th {
                background-color: #2E7D32 !important;
                color: white;
                position: sticky;
                top: 0;
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

            .sidebar {
                position: relative;
                top: 0;
            }
        }

        @media (max-width: 576px) {
            .options-container {
                padding: 15px;
            }

            .card-body-custom {
                padding: 15px 10px;
            }

            .table-custom {
                font-size: 12px;
            }

                .table-custom thead th,
                .table-custom tbody td {
                    padding: 8px 10px;
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
        <div class="container-fluid">
            <div class="row">

                <div class="col-lg-3 mb-4">
                    <div class="sidebar">
                        <div class="sidebar-header">
                            <i class="bi bi-person-circle brand-icon"></i>
                            <h4>Menú</h4>
                        </div>
                        <div class="sidebar-menu">
                            <asp:Button ID="btnSolicitarPedido" runat="server" Text="Solicitar Pedido"
                                CssClass="sidebar-item active" OnClick="btnSolicitarPedido_Click" />
                            <asp:Button ID="btnVisualizarPedidos" runat="server" Text="Visualizar Pedidos"
                                CssClass="sidebar-item" OnClick="btnVisualizarPedidos_Click" />
                            <asp:Button ID="btnCajonPreguntas" runat="server" Text="Cajón de Preguntas"
                                CssClass="sidebar-item" OnClick="btnCajonPreguntas_Click" />
                            <asp:Button ID="btnFlotaVehiculos" runat="server" Text="Flota de Vehículos"
                                CssClass="sidebar-item" OnClick="btnFlotaVehiculos_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-lg-9">
                    <%--Solicitar Pedido--%>
                    <asp:Panel ID="pnlSolicitarPedido" runat="server" Visible="true">
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
                                                <asp:ListItem Value="">Seleccione su empresa</asp:ListItem>
                                                <asp:ListItem Value="TransporteAndes">TransporteAndes</asp:ListItem>
                                                <asp:ListItem Value="LogiCar S.A.">LogiCar S.A.</asp:ListItem>
                                                <asp:ListItem Value="CargaExpress">CargaExpress</asp:ListItem>
                                                <asp:ListItem Value="Otra">Otra empresa</asp:ListItem>
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
                                                <asp:ListItem Value="Mercancía General">Mercancía General</asp:ListItem>
                                                <asp:ListItem Value="Perecederos">Perecederos</asp:ListItem>
                                                <asp:ListItem Value="Material Peligroso">Material Peligroso</asp:ListItem>
                                                <asp:ListItem Value="Maquinaria Pesada">Maquinaria Pesada</asp:ListItem>
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
                                        CssClass="btn-register" OnClick="btnSolicitarViaje_Click" />
                                </div>

                                <div class="full-width">
                                    <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="alert-message"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--Visualizar Pedidos Realizados--%>
                    <asp:Panel ID="pnlVisualizarPedidos" runat="server" Visible="false">
                        <div class="content-card">
                            <div class="card-header-custom">
                                <i class="bi bi-list-check brand-icon"></i>
                                <h3>Mis Pedidos Solicitados</h3>
                                <p class="text-muted mb-0">Historial de todos tus viajes solicitados</p>
                            </div>
                            <div class="card-body-custom">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvMisPedidos" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-custom table-pedidos"
                                        EmptyDataText="No has solicitado ningún viaje aún"
                                        DataKeyNames="idViaje">
                                        <Columns>
                                            <asp:BoundField DataField="idViaje" HeaderText="ID Viaje" />
                                            <asp:BoundField DataField="puntoPartida" HeaderText="Origen" />
                                            <asp:BoundField DataField="destino" HeaderText="Destino" />
                                            <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Salida"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="fechaFin" HeaderText="Fecha Llegada"
                                                DataFormatString="{0:dd/MM/yyyy}"
                                                NullDisplayText="Pendiente" />
                                            <asp:BoundField DataField="costo" HeaderText="Costo"
                                                DataFormatString="{0:C0}"
                                                NullDisplayText="Por confirmar" />
                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <span class='badge-estado badge-<%# Eval("estadoViaje").ToString().ToLower().Replace(" ", "") %>'>
                                                        <%# Eval("estadoViaje") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="motivo" HeaderText="Motivo"
                                                NullDisplayText="No especificado" />
                                            <asp:BoundField DataField="tipoCarga" HeaderText="Tipo Carga"
                                                NullDisplayText="No especificado" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

       
                    <asp:Panel ID="pnlCajonPreguntas" runat="server" Visible="false">
                        <div class="content-card">
                            <div class="card-header-custom">
                                <i class="bi bi-chat-dots brand-icon"></i>
                                <h3>Cajón de Preguntas y Opiniones</h3>
                                <p class="text-muted mb-0">Envía tus consultas y comentarios</p>
                            </div>
                            <div class="card-body-custom">
                                <div class="form-grid">
                                    <div class="form-group full-width">
                                        <asp:Label ID="lblTipoConsulta" runat="server" Text="Tipo de Consulta" CssClass="form-label"></asp:Label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-question-circle"></i></span>
                                            <asp:DropDownList ID="ddlTipoConsulta" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="">Seleccione tipo de consulta</asp:ListItem>
                                                <asp:ListItem Value="Pregunta">Pregunta</asp:ListItem>
                                                <asp:ListItem Value="Sugerencia">Sugerencia</asp:ListItem>
                                                <asp:ListItem Value="Queja">Queja</asp:ListItem>
                                                <asp:ListItem Value="Felicitacion">Felicitación</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group full-width">
                                        <asp:Label ID="lblAsunto" runat="server" Text="Asunto" CssClass="form-label"></asp:Label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-pen"></i></span>
                                            <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control" placeholder="Asunto de tu consulta"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group full-width">
                                        <asp:Label ID="lblMensajeConsulta" runat="server" Text="Mensaje" CssClass="form-label"></asp:Label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-chat-text"></i></span>
                                            <asp:TextBox ID="txtMensajeConsulta" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                Rows="5" placeholder="Escribe tu pregunta, sugerencia o comentario aquí..."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="full-width">
                                    <asp:Button ID="btnEnviarConsulta" runat="server" Text="Enviar Consulta"
                                        CssClass="btn-register" OnClick="btnEnviarConsulta_Click" />
                                </div>

                                <div class="full-width">
                                    <asp:Label ID="lblMensajeConsultaResult" runat="server" Text="" CssClass="alert-message"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

        
                    <asp:Panel ID="pnlFlotaVehiculos" runat="server" Visible="false">
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
                                        BorderStyle="None"
                                        EmptyDataText="No hay vehículos disponibles">

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
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

 
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="modalConfirmacionLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="modalConfirmacionLabel">
                        <i class="bi bi-check-circle-fill me-2"></i>Solicitud Registrada
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <div class="mb-3">
                        <i class="bi bi-check-circle text-success" style="font-size: 4rem;"></i>
                    </div>
                    <h4 class="text-success mb-3">¡Solicitud de Viaje Registrada Exitosamente!</h4>
                    <p class="lead">Hemos recibido tu solicitud de viaje. Nos contactaremos contigo a la brevedad para informarte el costo y las indicaciones específicas.</p>
                    <div class="alert alert-info mt-3">
                        <i class="bi bi-info-circle me-2"></i>
                        <strong>Próximos pasos:</strong> Un asesor se comunicará contigo dentro de las próximas 24 horas.
                    </div>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-success btn-lg px-4" data-bs-dismiss="modal">
                        <i class="bi bi-check-lg me-2"></i>Entendido
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
        function mostrarModalConfirmacion() {
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmacion'));
            modal.show();
        }
    </script>
</asp:Content>
