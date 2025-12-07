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
            position: relative;
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

        .badge-notificacion {
            position: absolute;
            top: -8px;
            right: -8px;
            background: #dc3545;
            color: white;
            border-radius: 50%;
            width: 22px;
            height: 22px;
            font-size: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
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
            background-color: rgba(46, 125, 50, 0.1);
            border-color: #2E7D32;
            color: #2E7D32;
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
            background-color: rgba(46, 125, 50, 0.1);
            color: #2E7D32;
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

        .btn-detalles {
            background-color: #2E7D32 !important;
            border-color: #2E7D32 !important;
            color: white !important;
        }
        
        .btn-detalles:hover {
            background-color: #27662C !important;
            border-color: #27662C !important;
        }

        .nav-tabs {
            border-bottom: 2px solid #dee2e6;
        }

        .nav-tabs .nav-link {
            color: #495057;
            border: 1px solid transparent;
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
            padding: 12px 20px;
            font-weight: 500;
            transition: all 0.3s ease;
        }

        .nav-tabs .nav-link:hover {
            border-color: #e9ecef #e9ecef #dee2e6;
            color: #2E7D32;
        }

        .nav-tabs .nav-link.active {
            color: #2E7D32;
            background-color: #fff;
            border-color: #dee2e6 #dee2e6 #fff;
            border-bottom: 3px solid #2E7D32;
            font-weight: 600;
        }

        .tab-content {
            padding: 20px 0;
            border: 1px solid #dee2e6;
            border-top: none;
            border-radius: 0 0 8px 8px;
            background: white;
        }

        .badge-estado-ticket {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
        }

        .badge-abierto {
            background-color: rgba(46, 125, 50, 0.1);
            color: #2E7D32;
        }

        .badge-proceso {
            background-color: #fff3cd;
            color: #856404;
        }

        .badge-resuelto {
            background-color: #d4edda;
            color: #155724;
        }

        .badge-cerrado {
            background-color: #f8d7da;
            color: #721c24;
        }

        .badge-prioridad {
            padding: 4px 8px;
            border-radius: 15px;
            font-size: 11px;
            font-weight: 600;
        }

        .baja {
            background-color: #6c757d;
            color: white;
        }

        .media {
            background-color: rgba(46, 125, 50, 0.8);
            color: white;
        }

        .alta {
            background-color: #ffc107;
            color: #212529;
        }

        .urgente {
            background-color: #dc3545;
            color: white;
        }

        .modal-ticket .modal-header {
            background-color: #2E7D32;
            color: white;
        }

        .modal-ticket .respuesta-soporte {
            background-color: rgba(46, 125, 50, 0.05);
            border-left: 4px solid #2E7D32;
            padding: 15px;
            margin-top: 15px;
            border-radius: 5px;
        }

        .detalles-viaje h6,
        .detalles-ticket h6 {
            color: #2E7D32;
            font-weight: 600;
            margin-bottom: 5px;
        }
        
        .detalles-viaje p,
        .detalles-ticket p {
            background-color: rgba(46, 125, 50, 0.05);
            padding: 10px;
            border-radius: 5px;
            border-left: 3px solid #2E7D32;
        }

        .btn-success-custom {
            background-color: #2E7D32;
            border-color: #2E7D32;
        }
        
        .btn-success-custom:hover {
            background-color: #27662C;
            border-color: #27662C;
        }
        
        .btn-outline-success-custom {
            color: #2E7D32;
            border-color: #2E7D32;
        }
        
        .btn-outline-success-custom:hover {
            background-color: #2E7D32;
            border-color: #2E7D32;
            color: white;
        }

        .btn-warning-custom {
            background-color: #ffc107;
            border-color: #ffc107;
        }
        
        .btn-outline-warning-custom {
            color: #ffc107;
            border-color: #ffc107;
        }

        .btn-danger-custom {
            background-color: #dc3545;
            border-color: #dc3545;
        }
        
        .btn-outline-danger-custom {
            color: #dc3545;
            border-color: #dc3545;
        }

        .table-success-custom {
            background-color: #2E7D32 !important;
            color: white;
        }

        .bg-success-custom {
            background-color: #2E7D32 !important;
        }

        .text-success-custom {
            color: #2E7D32 !important;
        }

        .border-success-custom {
            border-color: #2E7D32 !important;
        }

        #toast-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 9999;
            width: 350px;
        }

        .notification-toast {
            animation: slideInRight 0.3s ease;
            margin-bottom: 10px;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.15);
            border: 1px solid #2E7D32;
            background: white;
        }

        .notification-toast .toast-header {
            background-color: #2E7D32;
            color: white;
            border-radius: 8px 8px 0 0;
            padding: 8px 12px;
        }

        .notification-toast .toast-body {
            padding: 12px;
        }

        @keyframes slideInRight {
            from { transform: translateX(100%); opacity: 0; }
            to { transform: translateX(0); opacity: 1; }
        }

        @keyframes slideOutRight {
            from { transform: translateX(0); opacity: 1; }
            to { transform: translateX(100%); opacity: 0; }
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
                            <asp:Button ID="btnNotificaciones" runat="server" Text="Notificaciones"
                                CssClass="sidebar-item btn-notificacion-indicator" OnClick="btnNotificaciones_Click" />
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

                            <div class="card mb-4">
                                <div class="card-header bg-success-custom text-white">
                                    <h5 class="mb-0">Historial de Viajes</h5>
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <label>Estado:</label>
                                            <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Todos" Value="" />
                                                <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                                <asp:ListItem Text="Aprobado" Value="Aprobado" />
                                                <asp:ListItem Text="En curso" Value="En curso" />
                                                <asp:ListItem Text="Completado" Value="Completado" />
                                                <asp:ListItem Text="Cancelado" Value="Cancelado" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Desde:</label>
                                            <asp:TextBox ID="txtFiltroDesde" runat="server" CssClass="form-control" TextMode="Date" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>Hasta:</label>
                                            <asp:TextBox ID="txtFiltroHasta" runat="server" CssClass="form-control" TextMode="Date" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>Destino:</label>
                                            <asp:TextBox ID="txtFiltroDestino" runat="server" CssClass="form-control" placeholder="Ej: Medellín" />
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                                                CssClass="btn btn-success-custom me-2" OnClick="btnFiltrar_Click" />
                                            <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar a Excel" 
                                                CssClass="btn btn-outline-success-custom" OnClick="btnExportarExcel_Click" />
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <asp:GridView ID="gvHistorial" runat="server" 
                                            CssClass="table table-striped table-hover table-bordered" 
                                            AutoGenerateColumns="false"
                                            DataKeyNames="idViaje" 
                                            OnRowCommand="gvHistorial_RowCommand"
                                            EmptyDataText="No tienes viajes solicitados">
                                            <HeaderStyle CssClass="table-success-custom" />
                                            <Columns>
                                                <asp:BoundField DataField="idViaje" HeaderText="ID" ItemStyle-Width="80px" />
                                                <asp:BoundField DataField="puntoPartida" HeaderText="Origen" />
                                                <asp:BoundField DataField="destino" HeaderText="Destino" />
                                                <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Salida" 
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="estadoViaje" HeaderText="Estado" />
                                                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnVerDetalles" runat="server" 
                                                            Text="Ver Detalles" 
                                                            CommandName="verDetalles" 
                                                            CommandArgument='<%# Container.DataItemIndex %>'
                                                            CssClass="btn btn-detalles btn-sm" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- Notificaciones --%>
                    <asp:Panel ID="pnlNotificaciones" runat="server" Visible="false">
                        <div class="content-card">
                            <div class="card-header-custom">
                                <i class="bi bi-bell brand-icon"></i>
                                <h3>Mis Notificaciones</h3>
                                <p class="text-muted mb-0">Historial de todas las notificaciones recibidas</p>
                            </div>
                            <div class="card-body-custom">
                                <div class="mb-4">
                                    <div class="row g-3">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnMarcarTodasLeidas" runat="server" 
                                                Text="Marcar Todas Leídas" 
                                                CssClass="btn btn-outline-success-custom w-100"
                                                OnClick="btnMarcarTodasLeidas_Click" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnEliminarLeidas" runat="server" 
                                                Text="Eliminar Leídas" 
                                                CssClass="btn btn-outline-warning-custom w-100"
                                                OnClick="btnEliminarLeidas_Click" 
                                                OnClientClick="return confirm('¿Eliminar todas las notificaciones leídas?');" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnEliminarTodas" runat="server" 
                                                Text="Eliminar Todas" 
                                                CssClass="btn btn-outline-danger-custom w-100"
                                                OnClick="btnEliminarTodas_Click" 
                                                OnClientClick="return confirm('¿Eliminar TODAS las notificaciones?');" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnActualizarNotificaciones" runat="server" 
                                                Text="Actualizar" 
                                                CssClass="btn btn-success w-100"
                                                OnClick="btnActualizarNotificaciones_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvNotificaciones" runat="server" 
                                        CssClass="table table-striped table-hover"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="idNotificacion"
                                        OnRowCommand="gvNotificaciones_RowCommand"
                                        EmptyDataText="🎉 ¡No tienes notificaciones!"
                                        GridLines="None">
                                        <HeaderStyle CssClass="table-success-custom" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <span class='badge <%# Eval("leido").ToString() == "True" ? "bg-secondary" : "bg-primary" %> p-2'>
                                                        <i class='bi <%# Eval("leido").ToString() == "True" ? "bi-envelope-open" : "bi-envelope-fill" %>'></i>
                                                        <%# Eval("leido").ToString() == "True" ? "Leída" : "Nueva" %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="mensaje" HeaderText="Mensaje" 
                                                ItemStyle-Width="60%" />
                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" 
                                                DataFormatString="{0:dd/MM/yyyy HH:mm}" 
                                                ItemStyle-Width="200px" />
                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-sm" role="group">
                                                       
                                                        <asp:Button ID="btnEliminarNotificacion" runat="server" 
                                                            Text="Eliminar" 
                                                            CommandName="eliminar"
                                                            CommandArgument='<%# Container.DataItemIndex %>'
                                                            CssClass="btn btn-outline-danger-custom w-100"
                                                            OnClientClick="return confirm('¿Eliminar esta notificación?');" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="align-middle" />
                                    </asp:GridView>
                                </div>
                                
                                <div class="mt-4 p-3" style="background-color: rgba(46, 125, 50, 0.05); border-radius: 8px; border: 1px solid rgba(46, 125, 50, 0.1);">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h5 style="color: #2E7D32;">Resumen:</h5>
                                            <asp:Label ID="lblContadorNotificaciones" runat="server" 
                                                CssClass="fs-4 fw-bold text-success-custom"></asp:Label>
                                        </div>
                                        <div class="col-md-6 text-end">
                                            <asp:Label ID="lblUltimaActualizacion" runat="server" 
                                                CssClass="text-muted"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- Cajón de Preguntas y Soporte Técnico --%>
                    <asp:Panel ID="pnlCajonPreguntas" runat="server" Visible="false">
                        <div class="content-card">
                            <div class="card-header-custom">
                                <i class="bi bi-chat-dots brand-icon"></i>
                                <h3>Cajón de Preguntas y Soporte Técnico</h3>
                                <p class="text-muted mb-0">Envía tus consultas, reporta problemas o solicita soporte</p>
                            </div>
                            <div class="card-body-custom">
                                <ul class="nav nav-tabs mb-4" id="soporteTabs" role="tablist">
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link active" id="consulta-tab" data-bs-toggle="tab" 
                                            data-bs-target="#consulta" type="button" role="tab">
                                            <i class="bi bi-chat-text me-2"></i>Consulta General
                                        </button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="ticket-tab" data-bs-toggle="tab" 
                                            data-bs-target="#ticket" type="button" role="tab">
                                            <i class="bi bi-headset me-2"></i>Soporte Técnico
                                        </button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="historial-tab" data-bs-toggle="tab" 
                                            data-bs-target="#historial" type="button" role="tab">
                                            <i class="bi bi-clock-history me-2"></i>Mis Tickets
                                        </button>
                                    </li>
                                </ul>

                                <div class="tab-content" id="soporteTabsContent">
                                    <div class="tab-pane fade show active" id="consulta" role="tabpanel">
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

                                    <div class="tab-pane fade" id="ticket" role="tabpanel">
                                        <div class="form-grid">
                                            <div class="form-group full-width">
                                                <asp:Label runat="server" Text="Asunto del Ticket" CssClass="form-label"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="bi bi-ticket"></i></span>
                                                    <asp:TextBox ID="txtAsuntoTicket" runat="server" CssClass="form-control" 
                                                        placeholder="Ej: Problema con mi viaje, error en el sistema, etc."></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group full-width">
                                                <asp:Label runat="server" Text="Descripción del Problema" CssClass="form-label"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="bi bi-chat-left-text"></i></span>
                                                    <asp:TextBox ID="txtMensajeTicket" runat="server" CssClass="form-control" 
                                                        TextMode="MultiLine" Rows="6" 
                                                        placeholder="Describe detalladamente el problema o solicitud..."></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group full-width">
                                                <asp:Label runat="server" Text="Prioridad" CssClass="form-label"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="bi bi-exclamation-triangle"></i></span>
                                                    <asp:DropDownList ID="ddlPrioridadTicket" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="Baja">Baja</asp:ListItem>
                                                        <asp:ListItem Value="Media" Selected="True">Media</asp:ListItem>
                                                        <asp:ListItem Value="Alta">Alta</asp:ListItem>
                                                        <asp:ListItem Value="Urgente">Urgente</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group full-width">
                                                <asp:Label runat="server" Text="Categoría" CssClass="form-label"></asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="bi bi-tags"></i></span>
                                                    <asp:DropDownList ID="ddlCategoriaTicket" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="Problema Técnico">Problema Técnico</asp:ListItem>
                                                        <asp:ListItem Value="Consulta Facturación">Consulta Facturación</asp:ListItem>
                                                        <asp:ListItem Value="Solicitud de Información">Solicitud de Información</asp:ListItem>
                                                        <asp:ListItem Value="Reporte de Error">Reporte de Error</asp:ListItem>
                                                        <asp:ListItem Value="Otro">Otro</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="full-width">
                                            <asp:Button ID="btnEnviarTicket" runat="server" Text="Enviar Ticket de Soporte"
                                                CssClass="btn-register" OnClick="btnEnviarTicket_Click" />
                                        </div>

                                        <div class="full-width">
                                            <asp:Label ID="lblMensajeTicket" runat="server" Text="" CssClass="alert-message"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="tab-pane fade" id="historial" role="tabpanel">
                                        <div class="mb-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label>Filtrar por Estado:</label>
                                                    <asp:DropDownList ID="ddlFiltroEstadoTicket" runat="server" CssClass="form-control" 
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstadoTicket_SelectedIndexChanged">
                                                        <asp:ListItem Text="Todos los estados" Value="" />
                                                        <asp:ListItem Text="Abierto" Value="Abierto" />
                                                        <asp:ListItem Text="En Proceso" Value="En Proceso" />
                                                        <asp:ListItem Text="Resuelto" Value="Resuelto" />
                                                        <asp:ListItem Text="Cerrado" Value="Cerrado" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-8 d-flex align-items-end">
                                                    <asp:Button ID="btnRefrescarTickets" runat="server" Text="Refrescar" 
                                                        CssClass="btn btn-outline-success-custom ms-2" OnClick="btnRefrescarTickets_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTickets" runat="server" 
                                                CssClass="table table-striped table-hover table-bordered" 
                                                AutoGenerateColumns="false"
                                                DataKeyNames="idTicket"
                                                OnRowCommand="gvTickets_RowCommand"
                                                EmptyDataText="No tienes tickets de soporte">
                                                <HeaderStyle CssClass="table-success-custom" />
                                                <Columns>
                                                    <asp:BoundField DataField="idTicket" HeaderText="ID" ItemStyle-Width="80px" />
                                                    <asp:BoundField DataField="asunto" HeaderText="Asunto" />
                                                    <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                                                    <asp:BoundField DataField="prioridad" HeaderText="Prioridad" />
                                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Fecha Creación" 
                                                        DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnVerTicket" runat="server" 
                                                                Text="Ver" 
                                                                CommandName="verTicket" 
                                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                                CssClass="btn btn-info btn-sm" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--Flota Vehículos--%>
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
                                        HeaderStyle-CssClass="table-success-custom text-center"
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

    <%-- confirmacion solicitud --%>
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="modalConfirmacionLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success-custom text-white">
                    <h5 class="modal-title" id="modalConfirmacionLabel">
                        <i class="bi bi-check-circle-fill me-2"></i>Solicitud Registrada
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <div class="mb-3">
                        <i class="bi bi-check-circle text-success-custom" style="font-size: 4rem;"></i>
                    </div>
                    <h4 class="text-success-custom mb-3">¡Solicitud de Viaje Registrada Exitosamente!</h4>
                    <p class="lead">Hemos recibido tu solicitud de viaje. Nos contactaremos contigo a la brevedad para informarte el costo y las indicaciones específicas.</p>
                    <div class="alert alert-info mt-3">
                        <i class="bi bi-info-circle me-2"></i>
                        <strong>Próximos pasos:</strong> Un asesor se comunicará contigo dentro de las próximas 24 horas.
                    </div>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-success-custom btn-lg px-4" data-bs-dismiss="modal">
                        <i class="bi bi-check-lg me-2"></i>Entendido
                    </button>
                </div>
            </div>
        </div>
    </div>

    <%-- detalles --%>
    <div class="modal fade" id="modalDetalles" tabindex="-1" aria-labelledby="modalDetallesLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success-custom text-white">
                    <h5 class="modal-title" id="modalDetallesLabel">
                        <i class="bi bi-info-circle me-2"></i>Detalles del Viaje
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="max-height: 70vh; overflow-y: auto;">
                    <asp:Literal ID="litDetalles" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <%-- caja preguntas --%>
    <div class="modal fade modal-ticket" id="modalTicketDetalle" tabindex="-1" aria-labelledby="modalTicketDetalleLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success-custom text-white">
                    <h5 class="modal-title" id="modalTicketDetalleLabel">
                        <i class="bi bi-ticket-detailed me-2"></i>Detalles del Ticket
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="max-height: 70vh; overflow-y: auto;">
                    <asp:Literal ID="litDetallesTicket" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
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

        function mostrarModalDetalles() {
            var modal = new bootstrap.Modal(document.getElementById('modalDetalles'));
            modal.show();
        }

        function mostrarModalTicket() {
            var modal = new bootstrap.Modal(document.getElementById('modalTicketDetalle'));
            modal.show();
        }
    </script>

    <script>
        let notificacionesMostradas = new Set();
        let intervaloNotificaciones;

        function verificarNotificacionesNuevas() {
            const idCliente = '<%= Session["idCliente"] %>';
            
            if (!idCliente || idCliente === '') return;

            fetch(`NotificacionesCliente.ashx?idCliente=${idCliente}&soloNuevas=true`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Error en la respuesta del servidor');
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.success && data.notificaciones > 0) {
                        data.data.forEach(function (notificacion) {
                            if (!notificacionesMostradas.has(notificacion.idNotificacion)) {
                                mostrarNotificacionToast(notificacion.mensaje);
                                notificacionesMostradas.add(notificacion.idNotificacion);
                            }
                        });
                        
                        actualizarContadorNotificaciones();
                    }
                })
                .catch(error => {
                    console.error('Error al obtener notificaciones:', error);
                });
        }

        function mostrarNotificacionToast(mensaje) {
            let toastContainer = document.getElementById('toast-container');
            if (!toastContainer) {
                toastContainer = document.createElement('div');
                toastContainer.id = 'toast-container';
                toastContainer.style.cssText = 'position: fixed; top: 20px; right: 20px; z-index: 9999; width: 350px;';
                document.body.appendChild(toastContainer);
            }

            const toastId = 'toast-' + Date.now();
            const toast = document.createElement('div');
            toast.id = toastId;
            toast.className = 'notification-toast';
            toast.innerHTML = `
                <div class="toast-header bg-success-custom text-white">
                    <strong class="me-auto">
                        <i class="bi bi-bell-fill me-2"></i>Nueva Notificación
                    </strong>
                    <button type="button" class="btn-close btn-close-white" onclick="document.getElementById('${toastId}').remove()"></button>
                </div>
                <div class="toast-body">
                    ${mensaje}
                    <div class="mt-2 text-end">
                        <small class="text-muted">Hace un momento</small>
                    </div>
                </div>
            `;

            toast.style.cssText = `
                animation: slideInRight 0.3s ease;
                margin-bottom: 10px;
                border-radius: 8px;
                box-shadow: 0 4px 12px rgba(0,0,0,0.15);
                border: 1px solid #2E7D32;
            `;

            toastContainer.appendChild(toast);

            setTimeout(() => {
                const toastElement = document.getElementById(toastId);
                if (toastElement) {
                    toastElement.style.animation = 'slideOutRight 0.3s ease';
                    setTimeout(() => toastElement.remove(), 300);
                }
            }, 8000);
        }

        function actualizarContadorNotificaciones() {
            const btnNotificaciones = document.getElementById('<%= btnNotificaciones.ClientID %>');
            if (btnNotificaciones) {
                btnNotificaciones.click();
            }
        }

        document.addEventListener('DOMContentLoaded', function () {
            setTimeout(verificarNotificacionesNuevas, 2000);

            intervaloNotificaciones = setInterval(verificarNotificacionesNuevas, 30000);

            document.addEventListener('visibilitychange', function () {
                if (!document.hidden) {
                    verificarNotificacionesNuevas();
                }
            });
        });

        window.addEventListener('beforeunload', function () {
            if (intervaloNotificaciones) {
                clearInterval(intervaloNotificaciones);
            }
        });
    </script>
</asp:Content>