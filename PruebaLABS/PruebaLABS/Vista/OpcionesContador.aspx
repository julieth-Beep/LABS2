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
    </style>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid py-4">

        <div class="row">

            <!-- SIDEBAR RESPONSIVE -->
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

                    </div>

                </div>

            </div>

            <!-- CONTENIDO PRINCIPAL -->
            <div class="col-12 col-md-9">

                <!-- PANEL CONTRATOS EMPLEADOS -->
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

                        <asp:Button ID="Button4" runat="server" Text="Exportar Contratos"
                            CssClass="btn btn-success mt-3" OnClick="Button4_Click" />

                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control mt-3" />
                        <asp:Button ID="Button5" runat="server" Text="Importar Contratos"
                            CssClass="btn btn-primary mt-2" OnClick="Button5_Click" />

                        <asp:Label ID="lblMensaje" runat="server" CssClass="text-success fw-bold mt-3"></asp:Label>

                    </div>

                    <!-- EDITAR CONTRATO -->
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

                        <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar Cambios"
                            CssClass="btn-save w-100 mt-2"
                            OnClick="btnGuardar_Click" />

                    </div>

                    <!-- AGREGAR CONTRATO -->
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

                        <asp:Button ID="Button1" runat="server"
                            Text="Guardar Cambios"
                            CssClass="btn-save w-100 mt-2"
                            OnClick="btnGuardar_Click" />

                        <asp:Label ID="lblAddMensaje" runat="server" CssClass="text-success fw-bold mt-3"></asp:Label>

                    </div>

                </asp:Panel>


                <!-- ======================== PANEL CONTRATOS VIAJE ======================= -->

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

                        <asp:Button ID="Button2" runat="server" Text="Exportar Contratos"
                            CssClass="btn btn-success mt-3" OnClick="Button2_Click" />

                    </div>

                </asp:Panel>


                <!-- ======================== PANEL GASTOS ======================= -->

                <asp:Panel ID="pnlGastos" runat="server" Visible="false">

                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">

                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-receipt brand-icon"></i>
                            <h3>Gastos del Viaje</h3>
                        </div>

                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtIdViajeBuscar" runat="server" CssClass="form-control" placeholder="Ingrese ID del Viaje"></asp:TextBox>
                            <asp:Button ID="btnBuscarGastos" runat="server" Text="Buscar"
                                CssClass="btn btn-success" OnClick="btnBuscarGastos_Click" />
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped"
                                ShowFooter="true"
                                OnRowDataBound="gvGastos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="idGasto" HeaderText="ID" />
                                    <asp:BoundField DataField="tipoGasto" HeaderText="Tipo" />
                                    <asp:BoundField DataField="monto" HeaderText="Monto" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Button ID="btnExportar" runat="server" Text="Exportar Gastos"
                            CssClass="btn btn-success mt-3" OnClick="btnExportar_Click" />

                        <asp:FileUpload ID="fileExcel" runat="server" CssClass="form-control mt-3" />
                        <asp:Button ID="btnImportar" runat="server" Text="Importar Gastos"
                            CssClass="btn btn-primary mt-2" OnClick="btnImportar_Click" />

                        <asp:Label ID="lblGastosMensaje" runat="server" CssClass="text-danger fw-bold mt-3"></asp:Label>

                    </div>

                </asp:Panel>


                <!-- ======================== PANEL BONOS ======================= -->

                <asp:Panel ID="pnlBonos" runat="server" Visible="false">

                    <div class="content-card bg-white shadow rounded-3 p-4 mb-4">

                        <div class="card-header-custom text-center mb-3 pb-3 border-bottom">
                            <i class="bi bi-cash-coin brand-icon"></i>
                            <h3>Bonos de Empleados</h3>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvBonos" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="idUsuario" HeaderText="ID Usuario" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="nombre1" HeaderText="Rol" />
                                    <asp:BoundField DataField="bono" HeaderText="Bono Asignado" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Button ID="Export" runat="server" Text="Exportar Bonos"
                            CssClass="btn btn-success mt-3" OnClick="Export_Click" />

                    </div>

                </asp:Panel>

            </div>

        </div>

    </div>

</asp:Content>
