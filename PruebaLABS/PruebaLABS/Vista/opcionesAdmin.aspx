<%@ Page Title="Panel Administrador" Language="C#"
    MasterPageFile="~/Vista/Site1.Master"
    AutoEventWireup="true"
    CodeBehind="OpcionesAdmin.aspx.cs"
    Inherits="PruebaLABS.Vista.OpcionesAdmin" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f4f6f5;
            font-family: 'Segoe UI';
        }

        .options-container {
            min-height: 100vh;
            padding: 30px 20px;
        }

        .admin-layout {
            display: flex;
            gap: 25px;
            align-items: flex-start;
        }

        .sidebar {
            min-width: 230px;
            max-width: 260px;
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.1);
            padding-bottom: 20px;
            height: fit-content;
            position: sticky;
            top: 20px;
        }

        .sidebar-header {
            text-align: center;
            padding: 25px 20px 15px 20px;
            border-bottom: 1px solid #e9ecef;
        }

            .sidebar-header h4 {
                margin-top: 10px;
                font-weight: 600;
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
            font-size: 15px;
            border-left: 4px solid transparent;
            color: #333;
            transition: 0.2s;
        }

            .sidebar-item i {
                color: #2E7D32;
                margin-right: 8px;
            }

            .sidebar-item:hover {
                background: rgba(46,125,50,0.1);
                color: #2E7D32;
                border-left: 4px solid #2E7D32;
            }

            .sidebar-item.active {
                background: rgba(46,125,50,0.15);
                color: #2E7D32;
                border-left: 4px solid #2E7D32;
                font-weight: 600;
            }

        .content-zone {
            flex: 1;
        }

        .content-card {
            background: white;
            border-radius: 15px;
            padding: 25px;
            margin-bottom: 35px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.1);
        }

        .card-header-custom {
            text-align: center;
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 1px solid #e9ecef;
        }

        .brand-icon {
            font-size: 40px;
            margin-bottom: 10px;
            color: #2E7D32;
        }

        .btn-edit,
        .btn-state {
            background: #E8F5E9;
            color: #2E7D32;
            padding: 6px 10px;
            border-radius: 8px;
            border: 1px solid #C8E6C9;
            transition: 0.2s;
        }

        .btn-delete {
            background: #FFEBEE;
            color: #B71C1C;
            padding: 6px 10px;
            border-radius: 8px;
            border: 1px solid #FFCDD2;
            transition: 0.2s;
        }

        .btn-save,
        .btn-add,
        .btn-register {
            background-color: #2E7D32;
            color: white;
            padding: 12px;
            width: 100%;
            border-radius: 8px;
            border: none;
            margin-top: 10px;
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

        .alert-message {
            border-radius: 8px;
            padding: 10px 12px;
            margin-top: 12px;
            font-weight: 500;
            font-size: 14px;
            display: block;
        }

        .table-container {
            max-height: 600px;
            overflow: auto;
            border: 1px solid #e9ecef;
            border-radius: 8px;
        }

        .table-custom {
            margin: 0;
            min-width: 1200px;
        }

            .table-custom th {
                background: #2E7D32 !important;
                color: white;
                font-size: 12px;
                padding: 8px 6px !important;
                position: sticky;
                top: 0;
                z-index: 10;
                border: none;
                font-weight: 600;
            }

            .table-custom td {
                font-size: 11px;
                padding: 6px 4px !important;
                vertical-align: middle;
                border-bottom: 1px solid #f1f1f1;
            }

            .table-custom .form-control-sm {
                font-size: 11px;
                padding: 4px 6px;
                height: 28px;
            }

        .badge-warning {
            background-color: #ffc107;
            color: #212529;
        }

        .badge-info {
            background-color: #0dcaf0;
            color: #000;
        }

        .badge-primary {
            background-color: #0d6efd;
            color: #fff;
        }

        .badge-success {
            background-color: #198754;
            color: #fff;
        }

        .badge-danger {
            background-color: #dc3545;
            color: #fff;
        }

        .badge-secondary {
            background-color: #6c757d;
            color: #fff;
        }

        .badge {
            padding: 3px 6px;
            border-radius: 10px;
            font-size: 10px;
            font-weight: 600;
            white-space: nowrap;
        }

        .grid-responsive {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 15px;
            margin-bottom: 20px;
        }

        .search-section {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 15px;
        }

        .submenu-reportes {
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.08);
            overflow: hidden;
        }

        .submenu-header {
            background: linear-gradient(135deg, #2E7D32 0%, #4CAF50 100%);
            color: white;
            padding: 20px;
            text-align: center;
        }

        .submenu-item {
            padding: 12px 20px;
            border: none;
            background: none;
            width: 100%;
            text-align: left;
            font-size: 15px;
            border-left: 4px solid transparent;
            color: #333;
            transition: all 0.3s ease;
            margin: 2px 0;
        }

            .submenu-item:hover {
                background: rgba(46,125,50,0.08);
                color: #2E7D32;
                border-left: 4px solid #2E7D32;
            }

            .submenu-item.active {
                background: rgba(46,125,50,0.15);
                color: #2E7D32;
                border-left: 4px solid #2E7D32;
                font-weight: 600;
            }

        /* Mejoras para la tabla */
        .table-custom th {
            background-color: #2E7D32 !important;
            color: white;
        }

        /* Tarjetas de estadísticas */
        .stats-card {
            border-radius: 12px;
            padding: 20px;
            transition: transform 0.3s ease;
        }

            .stats-card:hover {
                transform: translateY(-5px);
            }

        /* ====== ESTILOS ESPECÍFICOS PARA EL PANEL DE REPORTES ====== */
        /* Estos estilos solo afectan al panel de reportes */

        /* CORRECCIÓN DE BADGES DE ESTADO SEGÚN LA IMAGEN */
        .badge-estado {
            font-size: 0.80rem;
            padding: 6px 14px;
            border-radius: 25px;
            font-weight: 700;
        }

        .badge-pendiente {
            background-color: #fff3cd !important;
            color: #856404 !important;
        }

        .badge-encurso {
            background-color: #d1ecf1 !important;
            color: #0c5460 !important;
        }

        .badge-completado {
            background-color: #d4edda !important;
            color: #155724 !important;
        }

        /* Estadísticas en panel de reportes */
        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #ffc107"] {
            background-color: #2E7D32 !important;
        }

        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #dc3545"] {
            background-color: #E8F5E9 !important;
        }

        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #0d6efd"] {
            background-color: #0d6efd !important;
        }

        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #0dcaf0"] {
            background-color: #0dcaf0 !important;
        }

        /* Iconos dentro de estadísticas */
        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #2E7D32"] i {
            color: white !important;
        }

        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #E8F5E9"] i {
            color: #2E7D32 !important;
        }

        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #0d6efd"] i,
        #pnlReportes .rounded-circle.p-3.me-3[style*="background-color: #0dcaf0"] i {
            color: white !important;
        }

        /* Badges de estado específicos para panel de reportes según la imagen */
        /* Badge de ID en reportes */
        #pnlReportes .badge.bg-dark {
            background-color: #495057 !important;
        }

        /* Fondo para los iconos de conductor, cliente, vehículo */
        #pnlReportes .bg-primary-subtle {
            background-color: #e3f2fd !important;
        }

        #pnlReportes .bg-info-subtle {
            background-color: #e0f7fa !important;
        }

        #pnlReportes .bg-secondary-subtle {
            background-color: #f5f5f5 !important;
        }

            /* Color para iconos dentro de estos fondos */
            #pnlReportes .bg-primary-subtle i,
            #pnlReportes .bg-info-subtle i,
            #pnlReportes .bg-secondary-subtle i {
                color: #2E7D32 !important;
            }

        /* Encabezado de sección de viajes */
        #pnlReportes .card-header-custom[style*="border-bottom: 2px solid #2E7D32"] {
            background-color: #f8fff9 !important;
        }

        /* Encabezado de sección de gastos */
        #pnlReportes .card-header-custom[style*="border-bottom: 2px solid #6f42c1"] {
            background-color: #f9f8ff !important;
        }

        /* Alerta informativa */
        #pnlReportes .alert-light {
            background-color: #f8fff9 !important;
            border-color: #C8E6C9 !important;
        }

        /* Botón exportar */
        #pnlReportes .btn-outline-success {
            border-color: #2E7D32;
            color: #2E7D32;
        }

            #pnlReportes .btn-outline-success:hover {
                background-color: #2E7D32;
                color: white;
            }

        /* Submenú de reportes */
        #pnlReportes .sidebar-header[style*="border-bottom: 1px solid #e9ecef"] {
            background-color: #2E7D32 !important;
        }

            #pnlReportes .sidebar-header[style*="border-bottom: 1px solid #e9ecef"] i {
                color: white !important;
            }

            #pnlReportes .sidebar-header[style*="border-bottom: 1px solid #e9ecef"] h5 {
                color: white !important;
            }

        /* Tabla en panel de reportes */
        #pnlReportes .table-custom tbody tr:hover {
            background-color: #E8F5E9 !important;
        }

        /* Badge de fecha en reportes */
        #pnlReportes .badge[style*="background-color: #f8f9fa"] {
            background-color: #f8f9fa !important;
            color: #212529 !important;
            border: 1px solid #dee2e6 !important;
        }

        /* ESTILOS NUEVOS PARA LA BARRA DE NAVEGACIÓN HORIZONTAL */
        .nav-reportes {
            background: white;
            border-radius: 0 0 10px 10px;
            padding: 0;
            border: 1px solid #e9ecef;
            border-top: none;
        }

        .nav-reporte-item {
            flex: 1;
            padding: 15px 20px;
            border: none;
            background: none;
            text-align: center;
            font-size: 16px;
            font-weight: 500;
            color: #495057;
            border-bottom: 3px solid transparent;
            transition: all 0.3s ease;
            position: relative;
            margin: 0;
        }

            .nav-reporte-item:hover {
                background-color: #f8f9fa;
                color: #2E7D32;
                border-bottom: 3px solid rgba(46, 125, 50, 0.3);
            }

            .nav-reporte-item.active {
                background-color: #E8F5E9;
                color: #2E7D32;
                border-bottom: 3px solid #2E7D32;
                font-weight: 600;
            }

            .nav-reporte-item i {
                font-size: 18px;
                vertical-align: middle;
            }

        /* ESTILOS ESPECÍFICOS PARA PANEL DE GASTOS */
        .badge-combustible {
            background-color: #fff3cd !important;
            color: #856404 !important;
            border: 1px solid #ffeaa7;
        }

        .badge-mantenimiento {
            background-color: #d1ecf1 !important;
            color: #0c5460 !important;
            border: 1px solid #bee5eb;
        }

        .badge-otros {
            background-color: #e2e3e5 !important;
            color: #383d41 !important;
            border: 1px solid #d6d8db;
        }

        /* Modal de imagen */
        #modalImagen .modal-dialog {
            max-width: 90%;
        }

        #imgEvidencia {
            max-width: 100%;
            height: auto;
            border: 1px solid #dee2e6;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        /* Agrega esto en la sección de estilos */
        #pnlReportesGastos .search-section {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
        }

        #pnlReportesGastos .input-group .btn-success {
            border-radius: 0 8px 8px 0;
        }

        #pnlReportesGastos .btn-outline-secondary {
            border-color: #6c757d;
            color: #6c757d;
        }

            #pnlReportesGastos .btn-outline-secondary:hover {
                background-color: #6c757d;
                color: white;
            }
    </style>

</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="options-container">
        <div class="admin-layout">


            <div class="sidebar">
                <div class="sidebar-header">
                    <i class="bi bi-shield-lock brand-icon"></i>
                    <h4>Administrador</h4>
                </div>

                <div class="sidebar-menu">

                    <asp:Button ID="btnVehiculos" runat="server"
                        Text="     Información de Vehículo"
                        CssClass="sidebar-item active"
                        OnClick="btnVehiculos_Click" />

                    <asp:Button ID="btnUsuarios" runat="server"
                        Text="     Usuarios"
                        CssClass="sidebar-item"
                        OnClick="btnUsuarios_Click" />

                    <asp:Button ID="btnRegistro" runat="server"
                        Text="     Registro de Personal"
                        CssClass="sidebar-item"
                        OnClick="btnRegistro_Click" />

                    <asp:Button ID="btnReportes" runat="server"
                        Text="     Reportes"
                        CssClass="sidebar-item"
                        OnClick="btnReportes_Click" />

                    <asp:Button ID="btnClientes" runat="server"
                        Text="     Clientes"
                        CssClass="sidebar-item"
                        OnClick="btnClientes_Click" />

                </div>
            </div>


            <div class="content-zone">

                <asp:Panel ID="pnlVehiculos" runat="server" Visible="true">

                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-truck brand-icon"></i>
                            <h3>Información de Vehículo</h3>
                            <p class="text-muted">Listado y gestión de la flota registrada</p>
                        </div>

                        <asp:GridView ID="gvVehiculos" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered"
                            OnRowCommand="gvVehiculos_RowCommand">

                            <Columns>
                                <asp:BoundField DataField="idVehiculo" HeaderText="ID" />
                                <asp:BoundField DataField="placa" HeaderText="Placa" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="capacidad" HeaderText="Capacidad" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />

                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn-edit"
                                            CommandName="editar"
                                            CommandArgument='<%# Container.DataItemIndex %>'>
                                            <i class="bi bi-pencil-square"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn-state"
                                            CommandName="cambiarEstado"
                                            CommandArgument='<%# Container.DataItemIndex %>'>
                                            <i class="bi bi-arrow-repeat"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn-delete"
                                            CommandName="eliminar"
                                            CommandArgument='<%# Container.DataItemIndex %>'
                                            OnClientClick="return confirm('¿Seguro que deseas eliminar este vehículo?');">
                                            <i class="bi bi-trash3"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                        <asp:Label ID="lblMensaje" runat="server" CssClass="text-success fw-bold mt-3"></asp:Label>
                    </div>

                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-pencil brand-icon"></i>
                            <h3>Editar Vehículo</h3>
                        </div>

                        <asp:TextBox ID="txtIdVehiculo" runat="server" Visible="false"></asp:TextBox>

                        <label>Placa</label>
                        <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Modelo</label>
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Capacidad</label>
                        <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control mb-3">
                            <asp:ListItem>Disponible</asp:ListItem>
                            <asp:ListItem>En mantenimiento</asp:ListItem>
                            <asp:ListItem>Fuera de servicio</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar Cambios"
                            CssClass="btn-save"
                            OnClick="btnGuardar_Click" />

                    </div>

                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-plus-circle brand-icon"></i>
                            <h3>Agregar Nuevo Vehículo</h3>
                        </div>

                        <label>Placa</label>
                        <asp:TextBox ID="txtAddPlaca" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Modelo</label>
                        <asp:TextBox ID="txtAddModelo" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Capacidad</label>
                        <asp:TextBox ID="txtAddCapacidad" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Estado</label>
                        <asp:DropDownList ID="ddlAddEstado" runat="server" CssClass="form-control mb-3">
                            <asp:ListItem>Disponible</asp:ListItem>
                            <asp:ListItem>En mantenimiento</asp:ListItem>
                            <asp:ListItem>Fuera de servicio</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button ID="btnAgregar" runat="server"
                            Text="Agregar Vehículo"
                            CssClass="btn-add"
                            OnClick="btnAgregar_Click" />

                        <asp:Label ID="lblAddMensaje" runat="server" CssClass="text-success fw-bold mt-3"></asp:Label>
                    </div>

                </asp:Panel>


                <asp:Panel ID="pnlUsuarios" runat="server" Visible="false">

                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-people brand-icon"></i>
                            <h3>Gestión de Usuarios</h3>
                            <p class="text-muted">Listado y administración de usuarios registrados</p>
                        </div>

                        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered">

                            <Columns>

                                <asp:BoundField DataField="idUsuario" HeaderText="ID" />
                                <asp:BoundField DataField="documento" HeaderText="Documento" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombres" />
                                <asp:BoundField DataField="apellido" HeaderText="Apellidos" />
                                <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="correo" HeaderText="Correo" />
                                <asp:BoundField DataField="nombreRol" HeaderText="Rol" />

                            </Columns>

                        </asp:GridView>

                        <asp:Label ID="lblMensajeUsuario" runat="server" CssClass="alert-message"></asp:Label>
                    </div>

                </asp:Panel>




                <asp:Panel ID="pnlRegistro" runat="server" Visible="false">
                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-person-plus brand-icon"></i>
                            <h3>Registro de Personal</h3>
                            <p class="text-muted mb-0">Registra un nuevo empleado del sistema</p>
                        </div>

                        <div class="card-body">

                            <div class="form-group">
                                <asp:Label ID="lblDocumento" runat="server" Text="Documento *" CssClass="form-label"></asp:Label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="bi bi-card-text"></i></span>
                                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Número de documento"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombres *" CssClass="form-label"></asp:Label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="bi bi-person"></i></span>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombres"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblApellido" runat="server" Text="Apellidos *" CssClass="form-label"></asp:Label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="bi bi-person"></i></span>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellidos"></asp:TextBox>
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
                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@gmail.com"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblRol" runat="server" Text="Tipo de Usuario *" CssClass="form-label"></asp:Label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="bi bi-person-badge"></i></span>
                                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Seleccione un rol</asp:ListItem>
                                        <asp:ListItem Value="1">Conductor</asp:ListItem>
                                        <asp:ListItem Value="2">Administrador</asp:ListItem>
                                        <asp:ListItem Value="3">Contador</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblPassword" runat="server" Text="Contraseña *" CssClass="form-label"></asp:Label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="bi bi-lock"></i></span>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Mínimo 6 caracteres"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirmar Contraseña *" CssClass="form-label"></asp:Label>
                                <div class="input-group">
                                    <span class="input-group-text"><i class="bi bi-lock-fill"></i></span>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Repetir contraseña"></asp:TextBox>
                                </div>
                            </div>

                            <asp:Button ID="btnRegistrarr" runat="server"
                                Text="Registrar Usuario"
                                CssClass="btn-register"
                                OnClick="btnRegistrarr_Click" />

                            <asp:Label ID="lblMensajeRegistro" runat="server" Text="" CssClass="alert-message"></asp:Label>

                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlReportes" runat="server" Visible="false">
                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-bar-chart-line brand-icon"></i>
                            <h3>Reportes</h3>
                            <p class="text-muted">Seleccione el tipo de reporte que desea ver</p>
                        </div>

                        <!-- BARRA DE NAVEGACIÓN HORIZONTAL (REEMPLAZO DEL SUBMENÚ) -->
                        <div class="row mb-4">
                            <div class="col-12">
                                <div class="card border-0 shadow-sm">
                                    <div class="card-body p-0">
                                        <div class="d-flex align-items-center" style="background: linear-gradient(135deg, #2E7D32 0%, #4CAF50 100%); border-radius: 10px 10px 0 0; padding: 15px 20px;">
                                            <i class="bi bi-clipboard-data text-white me-2" style="font-size: 1.5rem;"></i>
                                            <h5 class="text-white mb-0">Tipos de Reportes</h5>
                                        </div>
                                        <div class="d-flex nav-reportes">
                                            <asp:Button ID="btnReportesViajes" runat="server"
                                                Text=" Viajes"
                                                CssClass="nav-reporte-item active"
                                                OnClick="btnReportesViajes_Click" />

                                            <asp:Button ID="btnReportesGastos" runat="server"
                                                Text=" Gastos"
                                                CssClass="nav-reporte-item"
                                                OnClick="btnReportesGastos_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Panel para Viajes (SIN CAMBIOS) -->
                        <asp:Panel ID="pnlReportesViajes" runat="server" Visible="true">
                            <div class="content-card" style="border: 1px solid #e9ecef;">
                                <div class="card-header-custom" style="border-bottom: 2px solid #2E7D32;">
                                    <i class="bi bi-truck brand-icon"></i>
                                    <h4 style="color: #2E7D32;">Reporte de Viajes</h4>
                                    <p class="text-muted">Listado completo de todos los viajes registrados</p>
                                </div>

                                <!-- Encabezado con estadísticas -->
                                <div class="row mb-4">
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-truck " style="font-size: 20px; color: #2E7D32"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="totalViajes">0</h5>
                                                    <small class="text-muted">Total Viajes</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-clock" style="font-size: 20px; color: #2E7D32;"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="viajesPendientes">0</h5>
                                                    <small class="text-muted">Pendientes</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-arrow-right-circle " style="font-size: 20px; color: #2E7D32;"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="viajesEnCurso">0</h5>
                                                    <small class="text-muted">En Curso</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-check-circle" style="font-size: 20px; color: #2E7D32;"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="viajesCompletados">0</h5>
                                                    <small class="text-muted">Completados</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="table-container">
                                    <asp:GridView ID="gvReportesViajes" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-custom table-hover"
                                        EmptyDataText="No se encontraron viajes registrados"
                                        ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <span><%# Eval("idViaje") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Origen" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <i class="bi bi-geo-alt-fill text-primary me-2"></i>
                                                        <span><%# Eval("puntoPartida") %></span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Destino" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <i class="bi bi-geo-fill text-success me-2"></i>
                                                        <span><%# Eval("destino") %></span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="F. Inicio" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <span class="badge bg-light text-dark">
                                                            <i class="bi bi-calendar me-1"></i>
                                                            <%# Eval("fechaInicio") %>
                                                        </span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <span class='badge-estado badge-<%# GetEstadoBadgeClass(Eval("estadoViaje").ToString()) %>'>
                                                        <i class='<%# GetEstadoIcon(Eval("estadoViaje").ToString()) %> me-1'></i>
                                                        <%# Eval("estadoViaje") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Costo" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="fw-bold text-end">
                                                        <i class="bi bi-currency-dollar"></i>
                                                        <%# !string.IsNullOrEmpty(Eval("costo").ToString()) ? string.Format("{0:N0}", Convert.ToDecimal(Eval("costo"))) : "0" %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Conductor" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-primary-subtle rounded-circle p-2 me-2">
                                                            <i class="bi bi-person"></i>
                                                        </div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("Conductor") %></div>
                                                            <small class="text-muted"><%# Eval("telefono") %></small>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-info-subtle rounded-circle p-2 me-2">
                                                            <i class="bi bi-building"></i>
                                                        </div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("cliente") %></div>
                                                            <small class="text-muted"><%# Eval("empresa") %></small>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vehículo" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-secondary-subtle rounded-circle p-2 me-2">
                                                            <i class="bi bi-truck"></i>
                                                        </div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("placa") %></div>
                                                            <small><%# Eval("modelo") %></small>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="mt-4">
                                    <div class="alert alert-light border" role="alert">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <div>
                                                <i class="bi bi-info-circle text-primary me-2"></i>
                                                <span>Total de registros mostrados: <strong><%# gvReportesViajes.Rows.Count %></strong></span>
                                            </div>
                                            <div>
                                                <button class="btn btn-sm btn-outline-success">
                                                    <i class="bi bi-download me-1"></i>Exportar
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="mt-3">
                                    <asp:Label ID="lblMensajeReportesViajes" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>

                        <!-- Panel para Gastos - NUEVO -->
                        <asp:Panel ID="pnlReportesGastos" runat="server" Visible="false">
                            <div class="content-card" style="border: 1px solid #e9ecef;">
                                <div class="card-header-custom" style="border-bottom: 2px solid #2E7D32;">
                                    <i class="bi bi-cash-coin brand-icon" style="color: #2E7D32;"></i>
                                    <h4 style="color: #2E7D32;">Reporte de Gastos</h4>
                                    <p class="text-muted">Listado completo de todos los gastos registrados</p>
                                </div>
                                <div class="search-section mb-4">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label class="form-label">Filtrar por Placa del Vehículo</label>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="bi bi-search"></i></span>
                                                    <asp:TextBox ID="txtBuscarPlacaGastos" runat="server"
                                                        CssClass="form-control"
                                                        placeholder="Ingrese la placa del vehículo"></asp:TextBox>
                                                    <asp:Button ID="btnBuscarPlacaGastos" runat="server"
                                                        Text="Buscar"
                                                        CssClass="btn btn-success"
                                                        OnClick="btnBuscarPlacaGastos_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="form-label">&nbsp;</label>
                                                <asp:Button ID="btnLimpiarFiltroPlaca" runat="server"
                                                    Text="Mostrar Todos"
                                                    CssClass="btn btn-outline-secondary w-100"
                                                    OnClick="btnLimpiarFiltroPlaca_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Encabezado con estadísticas -->
                                <div class="row mb-4">
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-currency-dollar" style="font-size: 20px; color: #2E7D32"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="totalGastos">0</h5>
                                                    <small class="text-muted">Total Gastos</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-fuel-pump" style="font-size: 20px; color: #2E7D32;"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="gastosCombustible">0</h5>
                                                    <small class="text-muted">Combustible</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-tools" style="font-size: 20px; color: #2E7D32;"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="gastosMantenimiento">0</h5>
                                                    <small class="text-muted">Mantenimiento</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card bg-light border-0" style="border-radius: 10px; padding: 15px;">
                                            <div class="d-flex align-items-center">
                                                <div class="rounded-circle p-3 me-3" style="background-color: #E8F5E9;">
                                                    <i class="bi bi-cash-stack" style="font-size: 20px; color: #2E7D32;"></i>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0" id="gastosOtros">0</h5>
                                                    <small class="text-muted">Otros Gastos</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="table-container">
                                    <asp:GridView ID="gvReportesGastos" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-custom table-hover"
                                        EmptyDataText="No se encontraron gastos registrados"
                                        ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <span><%# Eval("idGasto") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <span class='badge <%# GetClaseTipoGasto(Eval("tipoGasto").ToString()) %>'>
                                                        <i class='<%# GetIconoTipoGasto(Eval("tipoGasto").ToString()) %> me-1'></i>
                                                        <%# Eval("tipoGasto") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <div>
                                                        <div class="fw-bold"><%# Eval("descripcionGasto") %></div>
                                                        <small class="text-muted">ID Viaje: <%# Eval("idViaje") %></small>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Monto" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="fw-bold text-success text-end">
                                                        <i class="bi bi-currency-dollar"></i>
                                                        <%# string.Format("{0:N2}", Eval("monto")) %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fecha" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <span class="badge bg-light text-dark">
                                                            <i class="bi bi-calendar me-1"></i>
                                                            <%# Convert.ToDateTime(Eval("fechaGasto")).ToString("dd/MM/yyyy") %>
                                                        </span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Conductor" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-primary-subtle rounded-circle p-2 me-2">
                                                            <i class="bi bi-person"></i>
                                                        </div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("nombreUsuario") %></div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Placa" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-secondary-subtle rounded-circle p-2 me-2">
                                                            <i class="bi bi-truck"></i>
                                                        </div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("placa") %></div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Evidencia" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <%# MostrarBotonEvidencia(Eval("imagenRecibo").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="mt-4">
                                    <div class="alert alert-light border" role="alert">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <div>
                                                <i class="bi bi-info-circle text-primary me-2"></i>
                                                <span>Total de gastos mostrados: <strong><%# gvReportesGastos.Rows.Count %></strong></span>
                                                <span class="ms-3">Monto total: <strong id="montoTotalGastos">$0.00</strong></span>
                                            </div>
                                            <div>
                                                <button class="btn btn-sm btn-outline-success">
                                                    <i class="bi bi-download me-1"></i>Exportar
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="mt-3">
                                    <asp:Label ID="lblMensajeReportesGastos" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>




                <%--cliente--%>

                <asp:Panel ID="pnlClientes" runat="server" Visible="false">

                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-people brand-icon"></i>
                            <h3>Gestión de Solicitudes - Clientes</h3>
                            <p class="text-muted mb-0">Buscar y gestionar solicitudes de viaje</p>
                        </div>

                        <div class="search-section">
                            <div class="grid-responsive">
                                <div class="form-group">
                                    <label class="form-label">Buscar por Documento</label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-person-badge"></i></span>
                                        <asp:TextBox ID="txtBuscarDocumento" runat="server" CssClass="form-control"
                                            placeholder="Documento del cliente"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="form-label">Fecha Desde</label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-calendar"></i></span>
                                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control"
                                            TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="form-label">Fecha Hasta</label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="bi bi-calendar-check"></i></span>
                                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control"
                                            TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="form-label">Filtrar por Estado</label>
                                    <asp:DropDownList ID="ddlFiltrarEstado" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Todos los estados</asp:ListItem>
                                        <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                        <asp:ListItem Value="Aprobado">Aprobado</asp:ListItem>
                                        <asp:ListItem Value="En curso">En curso</asp:ListItem>
                                        <asp:ListItem Value="Completado">Completado</asp:ListItem>
                                        <asp:ListItem Value="Cancelado">Cancelado</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="text-center mt-3">
                                <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar Solicitudes"
                                    CssClass="btn btn-success me-2" OnClick="btnBuscarCliente_Click" />
                                <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar Todos"
                                    CssClass="btn btn-outline-secondary" OnClick="btnMostrarTodos_Click" />
                            </div>
                        </div>

                        <asp:Label ID="lblMensajeSolicitudes" runat="server" Text="" CssClass="alert-message"></asp:Label>
                    </div>

                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-list-check brand-icon"></i>
                            <h3>Solicitudes de Viaje</h3>
                            <p class="text-muted mb-0">Tabla responsiva con filtros avanzados</p>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvSolicitudesClientes" runat="server"
                                AutoGenerateColumns="false"
                                CssClass="table table-hover table-striped"
                                OnRowEditing="gvSolicitudesClientes_RowEditing"
                                OnRowUpdating="gvSolicitudesClientes_RowUpdating"
                                OnRowCancelingEdit="gvSolicitudesClientes_RowCancelingEdit"
                                OnRowDeleting="gvSolicitudesClientes_RowDeleting"
                                DataKeyNames="idViaje"
                                EmptyDataText="No se encontraron solicitudes de viaje"
                                ShowHeaderWhenEmpty="true">

                                <Columns>
                                    <asp:TemplateField HeaderText="Documento" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("documento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cliente" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("Cliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Empresa" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Origen" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrigen" runat="server" Text='<%# Eval("puntoPartida") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Destino" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDestino" runat="server" Text='<%# Eval("destino") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="F. Salida" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaSalida" runat="server"
                                                Text='<%# Convert.ToDateTime(Eval("fechaInicio")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="F. Llegada" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaLlegada" runat="server"
                                                Text='<%# Eval("fechaFin") != DBNull.Value ? Convert.ToDateTime(Eval("fechaFin")).ToString("dd/MM/yyyy") : "Pendiente" %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFechaLlegada" runat="server" TextMode="Date" CssClass="form-control form-control-sm"
                                                Text='<%# Eval("fechaFin") != DBNull.Value ? Convert.ToDateTime(Eval("fechaFin")).ToString("yyyy-MM-dd") : "" %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <span class='badge badge-<%# GetEstadoBadgeClass(Eval("estadoViaje").ToString()) %>'>
                                                <%# Eval("estadoViaje") %>
                                            </span>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control form-control-sm">
                                                <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                                <asp:ListItem Value="Aprobado">Aprobado</asp:ListItem>
                                                <asp:ListItem Value="En curso">En curso</asp:ListItem>
                                                <asp:ListItem Value="Completado">Completado</asp:ListItem>
                                                <asp:ListItem Value="Cancelado">Cancelado</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Costo" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCosto" runat="server"
                                                Text='<%# Eval("costo") != DBNull.Value && !string.IsNullOrEmpty(Eval("costo").ToString()) ? string.Format("{0:C0}", Convert.ToDecimal(Eval("costo"))) : "Por confirmar" %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control form-control-sm" TextMode="Number" step="0.01"
                                                Text='<%# Eval("costo") != DBNull.Value && !string.IsNullOrEmpty(Eval("costo").ToString()) ? Eval("costo") : "" %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo Carga" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoCarga" runat="server" Text='<%# Eval("tipoCarga") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <div class="d-flex gap-1">
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-edit" CommandName="Edit" ToolTip="Editar">
                                    <i class="bi bi-pencil-square"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn-delete" CommandName="Delete"
                                                    OnClientClick="return confirm('¿Está seguro de eliminar esta solicitud?');" ToolTip="Eliminar">
                                    <i class="bi bi-trash3"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div class="d-flex gap-1">
                                                <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn-state" CommandName="Update" ToolTip="Guardar">
                                    <i class="bi bi-check-lg"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn-delete" CommandName="Cancel" ToolTip="Cancelar">
                                    <i class="bi bi-x-lg"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="table-success" />
                            </asp:GridView>
                        </div>

                        <div class="mt-3 text-center">
                            <small class="text-muted">
                                <i class="bi bi-info-circle me-1"></i>
                                Total de registros: 
                <asp:Label ID="lblTotalRegistros" runat="server" Text="0"></asp:Label>
                            </small>
                        </div>
                    </div>

                </asp:Panel>

                <%--cliente--%>
            </div>
        </div>
    </div>



    <div class="modal fade" id="modalConfirmacionUsuario" tabindex="-1" aria-labelledby="modalConfirmacionUsuarioLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="modalConfirmacionUsuarioLabel">
                        <i class="bi bi-check-circle-fill me-2"></i>Usuario Registrado
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center py-4">
                    <div class="mb-3">
                        <i class="bi bi-check-circle text-success" style="font-size: 4rem;"></i>
                    </div>
                    <h4 class="text-success mb-3">¡Usuario Registrado Exitosamente!</h4>
                    <p class="lead">El usuario ha sido registrado correctamente en el sistema con el rol asignado.</p>
                    <div class="alert alert-info mt-3">
                        <i class="bi bi-info-circle me-2"></i>
                        <strong>Información:</strong> El usuario ya puede iniciar sesión con sus credenciales.
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

    <div class="modal fade" id="modalImagen" tabindex="-1" aria-labelledby="modalImagenLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="modalImagenLabel">
                        <i class="bi bi-receipt me-2"></i>Evidencia del Gasto
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center">
                    <img id="imgEvidencia" src="k" alt="Evidencia" class="img-fluid rounded" style="max-height: 500px;" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <a id="descargarImagen" href="#" class="btn btn-primary">
                        <i class="bi bi-download me-1"></i>Descargar
                    </a>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
        function mostrarModalConfirmacionUsuario() {
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmacionUsuario'));
            modal.show();
        }
        function mostrarImagen(rutaImagen) {
            var imgElement = document.getElementById('imgEvidencia');
            var descargarLink = document.getElementById('descargarImagen');

            // Usar la ruta directamente como viene del servidor
            imgElement.src = rutaImagen;
            descargarLink.href = rutaImagen;

            // Extraer nombre del archivo para la descarga
            var nombreArchivo = rutaImagen.split('/').pop();
            descargarLink.download = nombreArchivo;
        }
    </script>

</asp:Content>
