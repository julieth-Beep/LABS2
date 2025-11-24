<%@ Page Title="Panel Administrador" Language="C#" MasterPageFile="~/Vista/Site1.Master"
    AutoEventWireup="true" CodeBehind="OpcionesAdmin.aspx.cs"
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
                            <h3>Usuarios</h3>
                        </div>

                        <p>Contenido pendiente...</p>
                    </div>
                </asp:Panel>




                <asp:Panel ID="pnlRegistro" runat="server" Visible="false">
                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-person-plus brand-icon"></i>
                            <h3>Registro de Personal</h3>
                        </div>

                        <a href="RegistroUsuario.aspx" class="btn btn-success">Ir al formulario</a>
                    </div>
                </asp:Panel>




                <asp:Panel ID="pnlReportes" runat="server" Visible="false">
                    <div class="content-card">
                        <div class="card-header-custom">
                            <i class="bi bi-bar-chart-line brand-icon"></i>
                            <h3>Reportes</h3>
                        </div>

                        <p>Contenido pendiente...</p>
                    </div>
                </asp:Panel>


            </div>
        </div>
    </div>

</asp:Content>
