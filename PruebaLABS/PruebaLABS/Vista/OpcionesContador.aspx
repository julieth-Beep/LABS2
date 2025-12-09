<%@ Page Title="Panel Contador" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionesContador.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesContador" %>

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
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.1);
            padding-bottom: 20px;
        }

        @media (min-width: 768px) {
            .sidebar {
                position: sticky;
                top: 20px;
            }
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
            border: 1px solid #eef1f2;
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

        .btn-edit {
            background: #0d6efd;
            color: white;
            padding: 6px 10px;
            border-radius: 6px;
            border: none;
        }

        .btn-state {
            background: #198754;
            color: white;
            padding: 6px 10px;
            border-radius: 6px;
            border: none;
        }

        .btn-delete {
            background: #dc3545;
            color: white;
            padding: 6px 10px;
            border-radius: 6px;
            border: none;
        }

        .btn-save,
        .btn-add {
            background-color: #2E7D32;
            color: white;
            padding: 12px;
            width: 100%;
            border-radius: 8px;
            border: none;
            margin-top: 10px;
        }

        /* ===== REPORTES ===== */
        .reportes-header {
            text-align: center;
            padding: 10px 10px 6px 10px;
        }

        .reportes-header .icon {
            font-size: 28px;
            color: #2E7D32;
            display: block;
            margin-bottom: 6px;
        }

        .reportes-header h2 {
            margin: 0;
            font-weight: 800;
            color: #1f2937;
        }

        .reportes-header p {
            margin-top: 4px;
            color: #6c757d;
            font-size: 13px;
        }

        .reportes-tabs-card {
            border-radius: 14px;
            overflow: hidden;
            box-shadow: 0 10px 25px rgba(0,0,0,0.08);
            background: #fff;
            margin-bottom: 18px;
            border: 1px solid #eef1f2;
        }

        .reportes-tabs-top {
            background: #2E7D32;
            color: #fff;
            padding: 12px 16px;
            font-weight: 700;
            display: flex;
            gap: 10px;
            align-items: center;
        }

        .reportes-tabs {
            display: flex;
            background: #fff;
        }

        .nav-reporte-item {
            width: 50%;
            border: 0;
            padding: 14px 12px;
            background: #fff;
            font-weight: 800;
            color: #2b2b2b;
            border-bottom: 2px solid transparent;
        }

        .nav-reporte-item.active {
            background: #e9f5ea;
            color: #2E7D32;
            border-bottom: 3px solid #2E7D32;
        }

        .reporte-titulo {
            text-align: center;
            margin: 14px 0 8px 0;
        }

        .reporte-titulo i {
            font-size: 34px;
            color: #2E7D32;
            display: block;
            margin-bottom: 4px;
        }

        .reporte-titulo h3 {
            color: #2E7D32;
            font-weight: 900;
            margin: 0;
        }

        .reporte-titulo small {
            color: #6c757d;
        }

        .divider-green {
            border-top: 2px solid #2E7D32;
            opacity: .25;
            margin: 14px 0 16px 0;
        }

        .stat-card {
            background: #f8fafb;
            border-radius: 14px;
            padding: 14px 16px;
            display: flex;
            gap: 12px;
            align-items: center;
            border: 1px solid #eef1f2;
        }

        .stat-icon {
            width: 42px;
            height: 42px;
            border-radius: 50%;
            background: #e9f5ea;
            display: grid;
            place-items: center;
            color: #2E7D32;
            font-size: 18px;
            flex: 0 0 auto;
        }

        .stat-text b {
            display: block;
            font-size: 18px;
            color: #111827;
            line-height: 1.05;
            font-weight: 900;
        }

        .stat-text span {
            font-size: 12px;
            color: #6c757d;
            font-weight: 700;
        }

        .tabla-verde {
            background: #fff;
            border-radius: 12px;
            overflow: hidden;
            border: 1px solid #e9ecef;
        }

        .tabla-verde thead th {
            background: #2E7D32 !important;
            color: #fff !important;
            border-color: #2E7D32 !important;
            font-weight: 800;
            font-size: 13px;
            vertical-align: middle;
            white-space: nowrap;
        }

        .tabla-verde td {
            vertical-align: middle;
            font-size: 14px;
        }

        .tabla-verde tbody tr:nth-child(even) {
            background: #fbfcfd;
        }

        .tabla-verde tbody tr:hover {
            background: #f1f8f2;
        }

        .badge-estado {
            padding: 6px 10px;
            border-radius: 999px;
            font-weight: 800;
            font-size: 12px;
            display: inline-flex;
            align-items: center;
            gap: 6px;
            white-space: nowrap;
        }

        .badge-estado.success { background: #d1e7dd; color: #0f5132; }
        .badge-estado.primary { background: #cfe2ff; color: #084298; }
        .badge-estado.warning { background: #fff3cd; color: #664d03; }
        .badge-estado.secondary { background: #e2e3e5; color: #41464b; }

        .badge-tipo {
            padding: 4px 10px;
            border-radius: 999px;
            font-weight: 800;
            font-size: 12px;
            display: inline-flex;
            align-items: center;
            gap: 6px;
            white-space: nowrap;
        }

        .badge-combustible { background:#fff3cd; color:#664d03; }
        .badge-mantenimiento { background:#e2e3e5; color:#41464b; }
        .badge-otros { background:#d1e7dd; color:#0f5132; }

        .grid-footer-box{
            border: 1px solid #e9ecef;
            border-radius: 12px;
            padding: 12px 14px;
            display:flex;
            align-items:center;
            justify-content:space-between;
            gap: 10px;
            background:#fff;
            margin-top: 12px;
        }

        .btn-export {
            border: 1px solid #198754;
            color: #198754;
            background: #fff;
            padding: 8px 12px;
            border-radius: 10px;
            font-weight: 900;
            font-size: 12px;
        }

        .btn-export:hover { background:#e9f5ea; }

        /* Scroll bonito */
        .table-responsive::-webkit-scrollbar { width: 10px; height: 10px; }
        .table-responsive::-webkit-scrollbar-thumb { background: rgba(46,125,50,0.35); border-radius: 999px; }
        .table-responsive::-webkit-scrollbar-track { background: rgba(0,0,0,0.04); border-radius: 999px; }

        /* ===== Ajustes de columnas en Gastos (para que quede como la captura) ===== */
        .gastos-grid td:nth-child(1), .gastos-grid th:nth-child(1) { width: 70px; }
        .gastos-grid td:nth-child(2), .gastos-grid th:nth-child(2) { width: 160px; }
        .gastos-grid td:nth-child(3), .gastos-grid th:nth-child(3) { min-width: 260px; }
        .gastos-grid td:nth-child(4), .gastos-grid th:nth-child(4) { width: 140px; white-space: nowrap; }
        .gastos-grid td:nth-child(5), .gastos-grid th:nth-child(5) { width: 140px; white-space: nowrap; }
        .gastos-grid td:nth-child(6), .gastos-grid th:nth-child(6) { width: 160px; }
        .gastos-grid td:nth-child(7), .gastos-grid th:nth-child(7) { width: 140px; }
        .gastos-grid td:nth-child(8), .gastos-grid th:nth-child(8) { width: 150px; white-space: nowrap; }

        /* La columna Viaje NO se muestra (la quitamos) */
        /* ===== Iconos tipo "píldora" como captura ===== */
        .pill {
            display:inline-flex;
            align-items:center;
            gap:8px;
            padding: 4px 10px;
            border-radius: 999px;
            font-weight: 800;
            font-size: 12px;
            line-height: 1;
            white-space: nowrap;
        }
        .pill .mini {
            width: 18px;
            height: 18px;
            border-radius: 6px;
            display:inline-grid;
            place-items:center;
            background: rgba(0,0,0,0.08);
        }

        /* Celdas con avatar circular como captura */
        .cell-avatar {
            display:flex;
            align-items:center;
            gap:10px;
            white-space: nowrap;
        }
        .cell-avatar .av {
            width:34px;
            height:34px;
            border-radius:50%;
            display:inline-grid;
            place-items:center;
            flex:0 0 auto;
        }
        .av.blue { background:#cfe2ff; color:#084298; }
        .av.gray { background:#e2e3e5; color:#41464b; }

        .money-green { color:#198754; font-weight:900; }
        .desc-strong { font-weight:900; color:#111827; }
        .desc-sub { font-size:12px; color:#6c757d; margin-top:2px; }
        .date-pill {
            display:inline-flex;
            align-items:center;
            gap:8px;
            padding: 4px 10px;
            border-radius: 999px;
            background:#f8f9fa;
            border:1px solid #e9ecef;
            font-weight:900;
            white-space: nowrap;
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid py-4">
        <div class="row">

            <div class="col-12 col-md-3 mb-4">
                <div class="sidebar p-3 bg-white shadow rounded-3">

                    <div class="sidebar-header text-center mb-3 pb-3 border-bottom">
                        <i class="bi bi-shield-lock brand-icon"></i>
                        <h4>Contador</h4>
                    </div>

                    <div class="sidebar-menu">
                        <asp:Button ID="btnContraEmp" runat="server" Text="Contratos de Empleados" CssClass="sidebar-item w-100 mb-2" OnClick="btnContraEmp_Click" />
                        <asp:Button ID="btnContraViaj" runat="server" Text="Contratos de Viajes" CssClass="sidebar-item w-100 mb-2" OnClick="btnContraViaj_Click" />
                        <asp:Button ID="btnGastos" runat="server" Text="Gastos Viaje" CssClass="sidebar-item w-100 mb-2" OnClick="btnGastos_Click" />
                        <asp:Button ID="btnBonos" runat="server" Text="Bonos" CssClass="sidebar-item w-100 mb-2" OnClick="btnBonos_Click" />
                        <asp:Button ID="btnEstadistica" runat="server" Text="Estadistica" CssClass="sidebar-item w-100 mb-2" OnClick="btnEstadistica_Click" />
                        <asp:Button ID="btnReportes" runat="server" Text="Reportes" CssClass="sidebar-item w-100 mb-2" OnClick="btnReportes_Click" />
                    </div>

                </div>
            </div>

            <div class="col-12 col-md-9">

                <asp:Panel ID="pnlContraEmp" runat="server" Visible="true">
                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-truck brand-icon"></i>
                            <h3>Contratos de Empleados</h3>
                            <p class="text-muted">Listado de contratos</p>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvContraEmp" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered"
                                OnRowCommand="gvContraEmp_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="idContrato" HeaderText="ID" />
                                    <asp:BoundField DataField="nombre" HeaderText="Cargo" />
                                    <asp:BoundField DataField="documento" HeaderText="Documento" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="salario" HeaderText="Salario" />
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo de Contrato" />
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn-delete"
                                                CommandName="eliminar"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este Contrato?');">
                                                <i class="bi bi-trash3"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Button ID="Button4" runat="server" Text="Exportar Contratos" CssClass="btn btn-success mt-3" OnClick="Button4_Click" />
                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control mt-3" />
                        <asp:Button ID="Button5" runat="server" Text="Importar Contratos" CssClass="btn btn-primary mt-2" OnClick="Button5_Click" />
                        <asp:Label ID="lblMensaje" runat="server" CssClass="text-success fw-bold mt-3"></asp:Label>
                    </div>

                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-pencil brand-icon"></i>
                            <h3>Editar Contrato</h3>
                        </div>

                        <asp:TextBox ID="txtIdContrato" runat="server" Visible="false"></asp:TextBox>

                        <label>Fecha</label>
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Salario</label>
                        <asp:TextBox ID="txtSalario" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Bono</label>
                        <asp:TextBox ID="txtBono" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Tipo</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control mb-3">
                            <asp:ListItem>Fijo</asp:ListItem>
                            <asp:ListItem>Indefinido</asp:ListItem>
                            <asp:ListItem>Contrato por Viaje</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn-save w-100 mt-2" OnClick="btnGuardar_Click" />
                    </div>

                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-plus-circle brand-icon"></i>
                            <h3>Agregar Nuevo Contrato</h3>
                        </div>

                        <label>Documento</label>
                        <asp:TextBox ID="txtAddDocumento" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Fecha</label>
                        <asp:TextBox ID="txtAddFecha" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Salario</label>
                        <asp:TextBox ID="txtAddSalario" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Bono</label>
                        <asp:TextBox ID="txtAddBono" runat="server" CssClass="form-control mb-3"></asp:TextBox>

                        <label>Tipo</label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control mb-3">
                            <asp:ListItem>Fijo</asp:ListItem>
                            <asp:ListItem>Indefinido</asp:ListItem>
                            <asp:ListItem>Contrato por Viaje</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button ID="Button1" runat="server" Text="Guardar Cambios" CssClass="btn-save w-100 mt-2" OnClick="btnRegistrar_Click" />
                        <asp:Label ID="lblAddMensaje" runat="server" CssClass="text-success fw-bold mt-3"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlContraViaj" runat="server" Visible="false">
                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-file-earmark-text brand-icon"></i>
                            <h3>Contratos de Viajes</h3>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvContraViaj" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="idCliente" HeaderText="ID Cliente" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Cliente" />
                                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="idViaje" HeaderText="ID Viaje" />
                                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Salida" />
                                    <asp:BoundField DataField="fechaFin" HeaderText="Fecha Llegada" />
                                    <asp:BoundField DataField="puntoPartida" HeaderText="Punto de Partida" />
                                    <asp:BoundField DataField="destino" HeaderText="Destino" />
                                    <asp:BoundField DataField="costo" HeaderText="Valor Viaje" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Button ID="Button2" runat="server" Text="Exportar Contratos" CssClass="btn btn-success mt-3" OnClick="Button2_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlGastos" runat="server" Visible="false">
                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-receipt brand-icon"></i>
                            <h3>Gastos del Viaje</h3>
                        </div>

                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtIdViajeBuscar" runat="server" CssClass="form-control" placeholder="Ingrese ID del Viaje"></asp:TextBox>
                            <asp:Button ID="btnBuscarGastos" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscarGastos_Click" />
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped"
                                ShowFooter="true"
                                OnRowDataBound="gvGastos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="placa" HeaderText="Placa" />
                                    <asp:BoundField DataField="nombre" HeaderText="Conductor" />
                                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Cargue" />
                                    <asp:BoundField DataField="fechaFin" HeaderText="FechaDescargue" />
                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                    <asp:BoundField DataField="tipoCarga" HeaderText="tipoCarga" />
                                    <asp:BoundField DataField="puntoPartida" HeaderText="Ruta Origen" />
                                    <asp:BoundField DataField="destino" HeaderText="Destino" />
                                    <asp:BoundField DataField="costo" HeaderText="Total Viaje" />
                                    <asp:BoundField DataField="idGasto" HeaderText="ID" />
                                    <asp:BoundField DataField="tipoGasto" HeaderText="Tipo" />
                                    <asp:BoundField DataField="monto" HeaderText="Monto" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Button ID="btnExportar" runat="server" Text="Exportar Gastos" CssClass="btn btn-success mt-3" OnClick="btnExportar_Click" />
                        <asp:FileUpload ID="fileExcel" runat="server" CssClass="form-control mt-3" />
                        <asp:Button ID="btnImportar" runat="server" Text="Importar Gastos" CssClass="btn btn-primary mt-2" OnClick="btnImportar_Click" />
                        <asp:Label ID="lblGastosMensaje" runat="server" CssClass="text-danger fw-bold mt-3"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlBonos" runat="server" Visible="false">
                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-cash-coin brand-icon"></i>
                            <h3>Bonos de Empleados</h3>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvBonos" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered"
                                DataKeyNames="idUsuario"
                                OnRowEditing="gvBonos_RowEditing"
                                OnRowCancelingEdit="gvBonos_RowCancelingEdit"
                                OnRowUpdating="gvBonos_RowUpdating">
                                <Columns>
                                    <asp:BoundField DataField="idUsuario" HeaderText="ID Usuario" ReadOnly="true" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="true" />
                                    <asp:BoundField DataField="apellido" HeaderText="Apellido" ReadOnly="true" />
                                    <asp:BoundField DataField="nombre1" HeaderText="Rol" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Bono Asignado">
                                        <ItemTemplate><%# Eval("bono") %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBonoEdit" runat="server" CssClass="form-control" Text='<%# Bind("bono") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="true" EditText="Editar" UpdateText="Guardar" CancelText="Cancelar" ControlStyle-CssClass="btn btn-primary btn-sm" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlContabilidad" runat="server" Visible="false">
                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">
                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-graph-up brand-icon"></i>
                            <h3>Movimientos Contables</h3>
                        </div>
                        <canvas id="graficoContable" style="width: 100%; height: 500px;"></canvas>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlReportes" runat="server" Visible="false">

                    <div class="content-card bg-white rounded-3 p-4 mb-4">

                        <div class="reportes-header">
                            <i class="bi bi-bar-chart-line-fill icon"></i>
                            <h2>Reportes</h2>
                            <p>Seleccione el tipo de reporte que desea ver</p>
                        </div>

                        <div class="reportes-tabs-card">
                            <div class="reportes-tabs-top">
                                <i class="bi bi-clipboard-data"></i>
                                Tipos de Reportes
                            </div>

                            <div class="reportes-tabs">
                                <asp:Button ID="btnReportesViajes" runat="server" Text="Viajes" CssClass="nav-reporte-item active" OnClick="btnReportesViajes_Click" />
                                <asp:Button ID="btnReportesGastos" runat="server" Text="Gastos" CssClass="nav-reporte-item" OnClick="btnReportesGastos_Click" />
                            </div>
                        </div>

                        <asp:Panel ID="pnlReportesViajes" runat="server" Visible="true">

                            <div class="reporte-titulo">
                                <i class="bi bi-truck"></i>
                                <h3>Reporte de Viajes</h3>
                                <small>Listado completo de todos los viajes registrados</small>
                            </div>

                            <hr class="divider-green" />

                            <div class="row g-3 mb-3">
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-truck"></i></div>
                                        <div class="stat-text"><b id="totalViajes">0</b><span>Total Viajes</span></div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-clock"></i></div>
                                        <div class="stat-text"><b id="viajesPendientes">0</b><span>Pendientes</span></div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-arrow-right-circle"></i></div>
                                        <div class="stat-text"><b id="viajesEnCurso">0</b><span>En Curso</span></div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-check-circle"></i></div>
                                        <div class="stat-text"><b id="viajesCompletados">0</b><span>Completados</span></div>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="gvReportesViajes" runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-hover tabla-verde"
                                    GridLines="None">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Origen">
                                            <ItemTemplate>
                                                <i class="bi bi-geo-alt-fill" style="color:#0d6efd; margin-right:6px;"></i>
                                                <%# Eval("puntoPartida") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Destino">
                                            <ItemTemplate>
                                                <i class="bi bi-geo-fill" style="color:#198754; margin-right:6px;"></i>
                                                <%# Eval("destino") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="F. Inicio">
                                            <ItemTemplate>
                                                <i class="bi bi-calendar3" style="color:#6c757d; margin-right:6px;"></i>
                                                <%# Eval("fechaInicio") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <span class='badge-estado <%# Eval("estadoViaje").ToString().ToLower().Contains("complet") ? "success" : (Eval("estadoViaje").ToString().ToLower().Contains("curso") ? "primary" : "warning") %>'>
                                                    <i class="<%# GetEstadoIcon(Eval("estadoViaje").ToString()) %>"></i>
                                                    <%# Eval("estadoViaje") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Costo">
                                            <ItemTemplate>
                                                <span style="font-weight:700;">$ <%# Eval("costo") %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Conductor">
                                            <ItemTemplate>
                                                <span style="display:inline-flex; align-items:center; gap:10px;">
                                                    <span style="width:34px; height:34px; border-radius:50%; background:#dbeafe; display:grid; place-items:center;">
                                                        <i class="bi bi-person" style="color:#1d4ed8;"></i>
                                                    </span>
                                                    <span style="line-height:1.1;">
                                                        <b><%# Eval("Conductor") %></b><br />
                                                        <span style="font-size:12px; color:#6c757d;"><%# Eval("telefono") %></span>
                                                    </span>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cliente">
                                            <ItemTemplate>
                                                <span style="display:inline-flex; align-items:center; gap:10px;">
                                                    <span style="width:34px; height:34px; border-radius:50%; background:#cffafe; display:grid; place-items:center;">
                                                        <i class="bi bi-building" style="color:#0e7490;"></i>
                                                    </span>
                                                    <span style="line-height:1.1;">
                                                        <b><%# Eval("cliente") %></b><br />
                                                        <span style="font-size:12px; color:#6c757d;"><%# Eval("empresa") %></span>
                                                    </span>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vehículo">
                                            <ItemTemplate>
                                                <span style="display:inline-flex; align-items:center; gap:10px;">
                                                    <span style="width:34px; height:34px; border-radius:50%; background:#e5e7eb; display:grid; place-items:center;">
                                                        <i class="bi bi-truck" style="color:#111827;"></i>
                                                    </span>
                                                    <span style="line-height:1.1;">
                                                        <b><%# Eval("placa") %></b><br />
                                                        <span style="font-size:12px; color:#111827;"><%# Eval("modelo") %></span>
                                                    </span>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="grid-footer-box">
                                <div class="text-muted small">
                                    <i class="bi bi-info-circle"></i>
                                    Total de registros mostrados:
                                    <b><asp:Label ID="lblTotalViajesMostrados" runat="server" Text="0"></asp:Label></b>
                                </div>
                                <asp:Button ID="btnExportarReportesViajes" runat="server" Text="Exportar" CssClass="btn-export" OnClick="btnExportarReportesViajes_Click" />
                            </div>

                            <asp:Label ID="lblMensajeReportesViajes" runat="server" CssClass="mt-2 d-block"></asp:Label>
                        </asp:Panel>

                        <asp:Panel ID="pnlReportesGastos" runat="server" Visible="false">

                            <div class="reporte-titulo">
                                <i class="bi bi-cash-coin"></i>
                                <h3>Reporte de Gastos</h3>
                                <small>Listado completo de todos los gastos registrados</small>
                            </div>

                            <hr class="divider-green" />

                            <div class="mb-2">
                                <label class="small text-muted">Filtrar por Placa del Vehículo</label>
                                <div class="d-flex gap-2 flex-wrap">
                                    <asp:TextBox ID="txtBuscarPlacaGastos" runat="server" CssClass="form-control" placeholder="Ingrese la placa del vehículo" Style="max-width:420px;"></asp:TextBox>
                                    <asp:Button ID="btnBuscarPlacaGastos" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscarPlacaGastos_Click" />
                                    <asp:Button ID="btnLimpiarFiltroPlaca" runat="server" Text="Mostrar Todos" CssClass="btn btn-outline-secondary" OnClick="btnLimpiarFiltroPlaca_Click" />
                                </div>
                            </div>

                            <div class="row g-3 mb-3 mt-2">
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-currency-dollar"></i></div>
                                        <div class="stat-text"><b id="totalGastos">0</b><span>Total Gastos</span></div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-fuel-pump"></i></div>
                                        <div class="stat-text"><b id="gastosCombustible">0</b><span>Combustible</span></div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-tools"></i></div>
                                        <div class="stat-text"><b id="gastosMantenimiento">0</b><span>Mantenimiento</span></div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-3">
                                    <div class="stat-card">
                                        <div class="stat-icon"><i class="bi bi-cash-stack"></i></div>
                                        <div class="stat-text"><b id="gastosOtros">0</b><span>Otros Gastos</span></div>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive" style="max-height: 520px; overflow:auto;">
                                <asp:GridView ID="gvReportesGastos" runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-hover tabla-verde gastos-grid"
                                    GridLines="None"
                                    OnRowCommand="gvReportesGastos_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="idGasto" HeaderText="ID" />

                                        <asp:TemplateField HeaderText="Tipo">
                                            <ItemTemplate>
                                                <span class='badge-tipo <%# GetClaseTipoGasto(Eval("tipoGasto").ToString()) %>'>
                                                    <span class="mini"><i class="<%# GetIconoTipoGasto(Eval("tipoGasto").ToString()) %>"></i></span>
                                                    <%# Eval("tipoGasto") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <div class="desc-strong"><%# Eval("descripcionGasto") %></div>
                                                <div class="desc-sub">ID Viaje: <%# Eval("idViaje") %></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monto">
                                            <ItemTemplate>
                                                <span class="money-green">$ <%# string.Format("{0:N2}", Eval("monto")) %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha">
                                            <ItemTemplate>
                                                <span class="date-pill">
                                                    <i class="bi bi-calendar3"></i>
                                                    <%# Eval("fechaGasto", "{0:dd/MM/yyyy}") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Conductor">
                                            <ItemTemplate>
                                                <div class="cell-avatar">
                                                    <span class="av blue"><i class="bi bi-person"></i></span>
                                                    <span style="font-weight:900;"><%# Eval("nombreUsuario") %></span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Placa">
                                            <ItemTemplate>
                                                <div class="cell-avatar">
                                                    <span class="av gray"><i class="bi bi-truck"></i></span>
                                                    <span style="font-weight:900;"><%# Eval("placa") %></span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Evidencia">
                                            <ItemTemplate>
                                                <%# MostrarBotonEvidencia(Eval("imagenRecibo").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="grid-footer-box">
                                <div class="text-muted small">
                                    <i class="bi bi-info-circle"></i>
                                    Total de gastos mostrados:
                                    <b><asp:Label ID="lblTotalGastosMostrados" runat="server" Text="0"></asp:Label></b>
                                    &nbsp;&nbsp;|&nbsp;&nbsp;
                                    Monto total:
                                    <b><span id="montoTotalGastos">$0.00</span></b>
                                </div>
                                <asp:Button ID="btnExportarReportesGastos" runat="server" Text="Exportar" CssClass="btn-export" OnClick="btnExportarReportesGastos_Click" />
                            </div>

                            <asp:Label ID="lblMensajeReportesGastos" runat="server" CssClass="mt-2 d-block"></asp:Label>

                            <div class="modal fade" id="modalImagen" tabindex="-1" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Evidencia</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body text-center">
                                            <img id="imgEvidencia" src="" style="max-width:100%; border-radius:12px;" alt="Evidencia" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>

                    </div>
                </asp:Panel>

                <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

                <script>
                    function CargarGrafico(labels, datos) {
                        const ctx = document.getElementById('graficoContable').getContext('2d');
                        new Chart(ctx, {
                            type: 'bar',
                            data: {
                                labels: labels,
                                datasets: [{
                                    label: 'Monto del Movimiento',
                                    data: datos,
                                    backgroundColor: 'rgba(54, 162, 235, 0.6)',
                                    borderColor: 'rgba(54, 162, 235, 1)',
                                    borderWidth: 2
                                }]
                            },
                            options: {
                                responsive: true,
                                scales: { y: { beginAtZero: true } }
                            }
                        });
                    }

                    function mostrarImagen(ruta) {
                        document.getElementById('imgEvidencia').src = ruta;
                    }
                </script>

                <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

            </div>
        </div>
    </div>

</asp:Content>
