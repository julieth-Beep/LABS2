<%@ Page Title="Panel Administrador" Language="C#"
    MasterPageFile="~/Vista/Site1.Master"
    AutoEventWireup="true"
    CodeBehind="OpcionesAdmin.aspx.cs"
    Inherits="PruebaLABS.Vista.OpcionesAdmin" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body { background-color: #f4f6f5; font-family: 'Segoe UI'; }
        .options-container { min-height: 100vh; padding: 30px 20px; }
        .admin-layout { display: flex; gap: 25px; align-items: flex-start; }

        .sidebar {
            min-width: 230px; max-width: 260px; background: white; border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.1); padding-bottom: 20px;
            height: fit-content; position: sticky; top: 20px;
        }
        .sidebar-header { text-align: center; padding: 25px 20px 15px 20px; border-bottom: 1px solid #e9ecef; }
        .sidebar-header h4 { margin-top: 10px; font-weight: 600; }
        .sidebar-menu { padding: 15px 0; }

        .sidebar-item {
            padding: 12px 25px; border: none; background: none; width: 100%;
            text-align: left; font-size: 15px; border-left: 4px solid transparent;
            color: #333; transition: 0.2s;
        }
        .sidebar-item i { color: #2E7D32; margin-right: 8px; }
        .sidebar-item:hover { background: rgba(46,125,50,0.1); color: #2E7D32; border-left: 4px solid #2E7D32; }
        .sidebar-item.active { background: rgba(46,125,50,0.15); color: #2E7D32; border-left: 4px solid #2E7D32; font-weight: 600; }

        .content-zone { flex: 1; }
        .content-card {
            background: white; border-radius: 15px; padding: 25px;
            margin-bottom: 35px; box-shadow: 0 10px 25px rgba(0,0,0,0.1);
        }
        .card-header-custom { text-align: center; margin-bottom: 20px; padding-bottom: 10px; border-bottom: 1px solid #e9ecef; }
        .brand-icon { font-size: 40px; margin-bottom: 10px; color: #2E7D32; }

        .btn-edit,.btn-state {
            background: #E8F5E9; color: #2E7D32; padding: 6px 10px;
            border-radius: 8px; border: 1px solid #C8E6C9; transition: 0.2s;
        }
        .btn-delete {
            background: #FFEBEE; color: #B71C1C; padding: 6px 10px;
            border-radius: 8px; border: 1px solid #FFCDD2; transition: 0.2s;
        }
        .btn-save,.btn-add,.btn-register {
            background-color: #2E7D32; color: white; padding: 12px; width: 100%;
            border-radius: 8px; border: none; margin-top: 10px;
        }

        .form-group { margin-bottom: 18px; }
        .form-label { font-weight: 500; color: #333; margin-bottom: 6px; font-size: 13px; display: block; }
        .input-group { position: relative; }
        .input-group-text {
            background-color: #f8f9fa; border: 1px solid #e9ecef; border-right: none;
            border-radius: 8px 0 0 8px;
        }
        .form-control {
            border: 1px solid #e9ecef; border-left: none; border-radius: 0 8px 8px 0;
            padding: 10px 12px; font-size: 14px; transition: all 0.3s ease; height: 45px;
        }
        .form-control:focus { border-color: #2E7D32; box-shadow: 0 0 0 0.2rem rgba(46, 125, 50, 0.15); }
        .input-group:focus-within .input-group-text { border-color: #2E7D32; }

        .alert-message { border-radius: 8px; padding: 10px 12px; margin-top: 12px; font-weight: 500; font-size: 14px; display: block; }

        .table-container { max-height: 600px; overflow: auto; border: 1px solid #e9ecef; border-radius: 8px; }
        .table-custom { margin: 0; min-width: 1200px; }
        .table-custom th {
            background: #2E7D32 !important; color: white; font-size: 12px;
            padding: 8px 6px !important; position: sticky; top: 0; z-index: 10; border: none; font-weight: 600;
        }
        .table-custom td { font-size: 11px; padding: 6px 4px !important; vertical-align: middle; border-bottom: 1px solid #f1f1f1; }

        .badge { padding: 3px 6px; border-radius: 10px; font-size: 10px; font-weight: 600; white-space: nowrap; }
        .badge-warning { background-color: #ffc107; color: #212529; }
        .badge-info { background-color: #0dcaf0; color: #000; }
        .badge-primary { background-color: #0d6efd; color: #fff; }
        .badge-success { background-color: #198754; color: #fff; }
        .badge-danger { background-color: #dc3545; color: #fff; }
        .badge-secondary { background-color: #6c757d; color: #fff; }

        .grid-responsive { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 15px; margin-bottom: 20px; }
        .search-section { background: #f8f9fa; padding: 15px; border-radius: 8px; margin-bottom: 15px; }

        .nav-reportes { background: white; border-radius: 0 0 10px 10px; padding: 0; border: 1px solid #e9ecef; border-top: none; }
        .nav-reporte-item {
            flex: 1; padding: 15px 20px; border: none; background: none; text-align: center;
            font-size: 16px; font-weight: 500; color: #495057; border-bottom: 3px solid transparent;
            transition: all 0.3s ease; position: relative; margin: 0;
        }
        .nav-reporte-item:hover { background-color: #f8f9fa; color: #2E7D32; border-bottom: 3px solid rgba(46, 125, 50, 0.3); }
        .nav-reporte-item.active { background-color: #E8F5E9; color: #2E7D32; border-bottom: 3px solid #2E7D32; font-weight: 600; }

        .badge-estado { font-size: 0.80rem; padding: 6px 14px; border-radius: 25px; font-weight: 700; }
        .badge-pendiente { background-color: #fff3cd !important; color: #856404 !important; }
        .badge-encurso { background-color: #d1ecf1 !important; color: #0c5460 !important; }
        .badge-completado { background-color: #d4edda !important; color: #155724 !important; }
        .badge-cancelado { background-color: #f8d7da !important; color: #842029 !important; }
        .badge-aprobado { background-color: #cfe2ff !important; color: #084298 !important; }

        .btn-edit,.btn-state,.btn-delete,
        a.btn-edit,a.btn-state,a.btn-delete{
            width:36px;
            height:36px;
            padding:0 !important;
            display:inline-flex !important;
            align-items:center;
            justify-content:center;
            border-radius:999px !important;
            text-decoration:none !important;
            box-shadow:0 6px 14px rgba(0,0,0,0.08);
            font-size:0;
        }
        .btn-edit i,.btn-state i,.btn-delete i{
            font-size:16px;
            line-height:1;
        }
        .btn-edit{
            background:rgba(13,110,253,.12) !important;
            border:1px solid rgba(13,110,253,.20) !important;
            color:#0d6efd !important;
        }
        .btn-state{
            background:rgba(46,125,50,.12) !important;
            border:1px solid rgba(46,125,50,.22) !important;
            color:#2E7D32 !important;
        }
        .btn-delete{
            background:rgba(220,53,69,.12) !important;
            border:1px solid rgba(220,53,69,.22) !important;
            color:#dc3545 !important;
        }
        .btn-edit:hover{ background:rgba(13,110,253,.18) !important; transform:translateY(-1px); }
        .btn-state:hover{ background:rgba(46,125,50,.18) !important; transform:translateY(-1px); }
        .btn-delete:hover{ background:rgba(220,53,69,.18) !important; transform:translateY(-1px); }
        .btn-edit:focus,.btn-state:focus,.btn-delete:focus{
            outline:none !important;
            box-shadow:0 0 0 .2rem rgba(46,125,50,.15), 0 6px 14px rgba(0,0,0,0.08) !important;
        }
        .table td,.table th{ vertical-align:middle !important; }
        .table .d-flex.gap-1{ gap:.5rem !important; }
        .table.table-bordered > :not(caption) > * > *{ border-color:#e9ecef !important; }
        .table.table-bordered{ border-radius:10px; overflow:hidden; }

        /* ====== EDITAR VIAJES (NUEVO DISEÑO) ====== */
        .table-edit-container{
          max-height: 520px;
          overflow:auto;
          border:1px solid #e9ecef;
          border-radius:12px;
          box-shadow: 0 10px 25px rgba(0,0,0,0.06);
        }

        .table-edit{
          margin:0;
          min-width: 1400px;
        }
        .table-edit th{
          background:#2E7D32 !important;
          color:#fff !important;
          font-size:12px;
          padding:10px 8px !important;
          position: sticky;
          top: 0;
          z-index: 5;
          border: none !important;
          font-weight: 700;
        }
        .table-edit td{
          font-size:12px;
          padding:8px 8px !important;
          vertical-align: middle !important;
          border-bottom:1px solid #f1f1f1 !important;
          background:#fff;
        }
        .table-edit tr:hover td{
          background: rgba(46,125,50,0.05);
        }

        .table-edit .form-control,
        .table-edit select{
          height: 34px !important;
          font-size: 12px !important;
          border-radius: 10px !important;
          border: 1px solid #e9ecef !important;
        }
        .table-edit .form-control:focus,
        .table-edit select:focus{
          border-color:#2E7D32 !important;
          box-shadow:0 0 0 .15rem rgba(46,125,50,.15) !important;
        }

        .col-actions{
          width:110px;
          text-align:center;
          white-space:nowrap;
        }

        .gv-action a{
          width:36px;
          height:36px;
          display:inline-flex;
          align-items:center;
          justify-content:center;
          border-radius:999px;
          text-decoration:none;
          margin:0 4px;
          box-shadow:0 6px 14px rgba(0,0,0,0.08);
          border:1px solid transparent;
          font-size:0;
        }
        .gv-action a:hover{ transform: translateY(-1px); }

        .gv-action a[href*="Edit"]{
          background: rgba(13,110,253,.12);
          border-color: rgba(13,110,253,.20);
        }
        .gv-action a[href*="Update"]{
          background: rgba(46,125,50,.12);
          border-color: rgba(46,125,50,.22);
        }
        .gv-action a[href*="Cancel"]{
          background: rgba(220,53,69,.12);
          border-color: rgba(220,53,69,.22);
        }

        .gv-action a[href*="Edit"]::after{
          content:"\F4CB";
          font-family:"bootstrap-icons";
          font-size:16px;
          color:#0d6efd;
        }
        .gv-action a[href*="Update"]::after{
          content:"\F633";
          font-family:"bootstrap-icons";
          font-size:16px;
          color:#2E7D32;
        }
        .gv-action a[href*="Cancel"]::after{
          content:"\F62A";
          font-family:"bootstrap-icons";
          font-size:16px;
          color:#dc3545;
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
                        Text="     Viajes"
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
                            <i class="bi bi-truck brand-icon"></i>
                            <h3>Viajes</h3>
                            <p class="text-muted">Listado completo de todos los viajes registrados</p>
                        </div>

                        <div class="row mb-4">
                            <div class="col-12">
                                <div class="card border-0 shadow-sm">
                                    <div class="card-body p-0">
                                        <div class="d-flex align-items-center" style="background: linear-gradient(135deg, #2E7D32 0%, #4CAF50 100%); border-radius: 10px 10px 0 0; padding: 15px 20px;">
                                            <i class="bi bi-clipboard-data text-white me-2" style="font-size: 1.5rem;"></i>
                                            <h5 class="text-white mb-0">Viajes</h5>
                                        </div>
                                        <div class="d-flex nav-reportes">
                                            <asp:Button ID="btnReportesViajes" runat="server"
                                                Text=" Viajes"
                                                CssClass="nav-reporte-item active"
                                                OnClick="btnReportesViajes_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnlReportesViajes" runat="server" Visible="true">

                            <div class="content-card" style="border: 1px solid #e9ecef;">
                                <div class="card-header-custom" style="border-bottom: 2px solid #2E7D32;">
                                    <i class="bi bi-truck brand-icon"></i>
                                    <h4 style="color: #2E7D32;">Reporte de Viajes</h4>
                                    <p class="text-muted">Listado completo de todos los viajes registrados</p>
                                </div>

                                <div class="table-container">
                                    <asp:GridView ID="gvReportesViajes" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-custom table-hover"
                                        EmptyDataText="No se encontraron viajes registrados"
                                        ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="50px">
                                                <ItemTemplate><span><%# Eval("idViaje") %></span></ItemTemplate>
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

                                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <span class='badge-estado badge-<%# GetEstadoBadgeClass(Eval("estadoViaje").ToString()) %>'>
                                                        <i class='<%# GetEstadoIcon(Eval("estadoViaje").ToString()) %> me-1'></i>
                                                        <%# Eval("estadoViaje") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Costo" ItemStyle-Width="110px">
                                                <ItemTemplate>
                                                    <div class="fw-bold text-end">
                                                        <i class="bi bi-currency-dollar"></i>
                                                        <%# !string.IsNullOrEmpty(Eval("costo").ToString()) ? string.Format("{0:N0}", Convert.ToDecimal(Eval("costo"))) : "0" %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Conductor" ItemStyle-Width="160px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-primary-subtle rounded-circle p-2 me-2"><i class="bi bi-person"></i></div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("Conductor") %></div>
                                                            <small class="text-muted"><%# Eval("telefono") %></small>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="160px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-info-subtle rounded-circle p-2 me-2"><i class="bi bi-building"></i></div>
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("cliente") %></div>
                                                            <small class="text-muted"><%# Eval("empresa") %></small>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vehículo" ItemStyle-Width="130px">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="bg-secondary-subtle rounded-circle p-2 me-2"><i class="bi bi-truck"></i></div>
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

                                <div class="mt-3">
                                    <asp:Label ID="lblMensajeReportesViajes" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                            <div class="content-card">
                                <div class="card-header-custom">
                                    <i class="bi bi-plus-square brand-icon"></i>
                                    <h3>Crear Viaje</h3>
                                    <p class="text-muted mb-0">Registra un nuevo viaje y opcionalmente asígnalo</p>
                                </div>

                                <div class="grid-responsive">
                                    <div class="form-group">
                                        <label class="form-label">Origen</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-geo-alt"></i></span>
                                            <asp:TextBox ID="txtOrigenCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Destino</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-geo"></i></span>
                                            <asp:TextBox ID="txtDestinoCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Distancia (Km)</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-signpost-2"></i></span>
                                            <asp:TextBox ID="txtDistanciaCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Costo</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-currency-dollar"></i></span>
                                            <asp:TextBox ID="txtCostoCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Tipo de Carga</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-box-seam"></i></span>
                                            <asp:TextBox ID="txtTipoCargaCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Motivo</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-chat-left-text"></i></span>
                                            <asp:TextBox ID="txtMotivoCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Observaciones</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-pencil"></i></span>
                                            <asp:TextBox ID="txtObservacionesCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Cliente (ID) *</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-person-badge"></i></span>
                                            <asp:TextBox ID="txtIdClienteCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <hr />

                                <div class="grid-responsive">
                                    <div class="form-group">
                                        <label class="form-label">Conductor (opcional)</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-person"></i></span>
                                            <asp:DropDownList ID="ddlConductorCrear" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlConductorCrear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <small class="text-muted">Tel:</small>
                                        <asp:Label ID="lblTelConductorCrear" runat="server" CssClass="text-muted fw-bold"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Vehículo (opcional)</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-truck"></i></span>
                                            <asp:DropDownList ID="ddlVehiculoCrear" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlVehiculoCrear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="d-flex gap-3">
                                            <small class="text-muted">Modelo:</small>
                                            <asp:Label ID="lblModeloVehCrear" runat="server" CssClass="text-muted fw-bold"></asp:Label>
                                            <small class="text-muted ms-3">Capacidad:</small>
                                            <asp:Label ID="lblCapacidadVehCrear" runat="server" CssClass="text-muted fw-bold"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="form-label">Anticipo (opcional)</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-cash"></i></span>
                                            <asp:TextBox ID="txtAnticipoCrear" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="text-center">
                                    <asp:Button ID="btnCrearViaje" runat="server"
                                        Text="Crear Viaje"
                                        CssClass="btn btn-success px-4"
                                        OnClick="btnCrearViaje_Click" />
                                </div>

                                <asp:Label ID="lblMensajeCrearViaje" runat="server" CssClass="alert-message"></asp:Label>
                            </div>

                            <!-- ====== EDITAR VIAJES: AQUI SE APLICA EL DISEÑO NUEVO (SIN TOCAR FUNCIONALIDAD) ====== -->
                            <div class="content-card" style="border: 1px solid #e9ecef;">
                                <div class="card-header-custom" style="border-bottom: 2px solid #2E7D32;">
                                    <i class="bi bi-pencil-square brand-icon"></i>
                                    <h4 style="color: #2E7D32;">Editar Viajes</h4>
                                    <p class="text-muted">Edita datos del viaje y su asignación</p>
                                </div>

                                <div class="table-edit-container">
                                    <asp:GridView ID="gvViajesAdminEditar" runat="server"
                                        AutoGenerateColumns="false"
                                        CssClass="table table-edit table-hover"
                                        DataKeyNames="idViaje"
                                        OnRowEditing="gvViajesAdminEditar_RowEditing"
                                        OnRowUpdating="gvViajesAdminEditar_RowUpdating"
                                        OnRowCancelingEdit="gvViajesAdminEditar_RowCancelingEdit"
                                        OnRowDataBound="gvViajesAdminEditar_RowDataBound"
                                        EmptyDataText="No hay viajes para editar"
                                        ShowHeaderWhenEmpty="true">

                                        <Columns>
                                            <asp:BoundField DataField="idViaje" HeaderText="ID" ReadOnly="true" />

                                            <asp:TemplateField HeaderText="Origen">
                                                <ItemTemplate><%# Eval("origen") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtOrigenEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("origen") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Destino">
                                                <ItemTemplate><%# Eval("destino") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDestinoEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("destino") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Distancia">
                                                <ItemTemplate><%# Eval("distancia") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDistanciaEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("distancia") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Costo">
                                                <ItemTemplate><%# Eval("costo") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCostoEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("costo") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Carga">
                                                <ItemTemplate><%# Eval("tipoCarga") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTipoCargaEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("tipoCarga") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Motivo">
                                                <ItemTemplate><%# Eval("motivo") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMotivoEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("motivo") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Observaciones">
                                                <ItemTemplate><%# Eval("observaciones") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtObservacionesEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("observaciones") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Conductor">
                                                <ItemTemplate><%# Eval("conductorNombre") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hidConductorCargo" runat="server" Value='<%# Bind("idConductorCargo") %>' />
                                                    <asp:DropDownList ID="ddlConductorEdit" runat="server" CssClass="form-control form-control-sm" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vehículo">
                                                <ItemTemplate><%# Eval("vehiculoTexto") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hidVehiculo" runat="server" Value='<%# Bind("idVehiculo") %>' />
                                                    <asp:DropDownList ID="ddlVehiculoEdit" runat="server" CssClass="form-control form-control-sm" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Anticipo">
                                                <ItemTemplate><%# Eval("anticipo") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAnticipoEdit" runat="server" CssClass="form-control form-control-sm"
                                                        Text='<%# Bind("anticipo") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField ShowEditButton="true"
                                                ItemStyle-CssClass="col-actions gv-action"
                                                HeaderStyle-CssClass="col-actions" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                </asp:Panel>

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
                                        <ItemTemplate><asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("documento") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cliente" HeaderStyle-Width="150px">
                                        <ItemTemplate><asp:Label ID="lblCliente" runat="server" Text='<%# Eval("Cliente") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Empresa" HeaderStyle-Width="120px">
                                        <ItemTemplate><asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("empresa") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Origen" HeaderStyle-Width="120px">
                                        <ItemTemplate><asp:Label ID="lblOrigen" runat="server" Text='<%# Eval("puntoPartida") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Destino" HeaderStyle-Width="120px">
                                        <ItemTemplate><asp:Label ID="lblDestino" runat="server" Text='<%# Eval("destino") %>'></asp:Label></ItemTemplate>
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
                                            <span class='badge badge-<%# GetEstadoBadgeClass(Eval("estadoViaje").ToString()) %>'><%# Eval("estadoViaje") %></span>
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
                                        <ItemTemplate><asp:Label ID="lblTipoCarga" runat="server" Text='<%# Eval("tipoCarga") %>'></asp:Label></ItemTemplate>
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

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
        function mostrarModalConfirmacionUsuario() {
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmacionUsuario'));
            modal.show();
        }
    </script>

</asp:Content>
