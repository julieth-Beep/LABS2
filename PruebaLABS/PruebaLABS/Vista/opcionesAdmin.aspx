<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true"
    CodeBehind="OpcionesAdmin.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f4f6f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .options-container {
            min-height: 100vh;
            padding: 35px 20px;
        }

        .content-card {
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.08);
            border: none;
            margin-bottom: 30px;
        }

        .card-header-custom {
            background: #ffffff;
            color: #333;
            padding: 25px 30px 20px 30px;
            text-align: center;
            border-bottom: 1px solid #e9ecef;
            border-radius: 15px 15px 0 0;
        }

        .brand-icon {
            font-size: 42px;
            margin-bottom: 8px;
            color: #2E7D32;
        }

        .table-custom thead th {
            background-color: #2E7D32;
            color: white;
            text-align: center;
            padding: 13px;
            font-size: 15px;
        }

        .table-custom tbody td {
            padding: 11px;
            text-align: center;
            font-size: 14px;
        }

        .btn-edit, .btn-state, .btn-delete {
            padding: 6px 10px;
            border-radius: 7px;
            border: none;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 3px;
            font-size: 14px;
        }

        .btn-edit {
            background-color: #0d6efd;
            color: white;
        }

        .btn-state {
            background-color: #198754;
            color: white;
        }

        .btn-delete {
            background-color: #dc3545;
            color: white;
        }

        .form-label {
            font-weight: 600;
            margin-bottom: 4px;
        }

        .btn-save, .btn-add {
            background-color: #2E7D32;
            color: white;
            padding: 12px;
            border-radius: 8px;
            border: none;
            width: 100%;
            font-size: 15px;
            font-weight: 500;
        }

        .btn-save:hover, .btn-add:hover {
            background-color: #27662C;
        }

        .alert-message {
            padding: 10px;
            font-weight: 600;
            color: #2E7D32;
            margin-top: 10px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="options-container">
        <div class="container">
            <div class="row">

                <!-- ================= TABLA DE VEHICULOS ================= -->
                <div class="col-lg-8 mb-4">
                    <div class="content-card">

                        <div class="card-header-custom">
                            <i class="bi bi-truck brand-icon"></i>
                            <h3>Gestión de Vehículos</h3>
                            <p class="text-muted mb-0">Administra toda la flota disponible</p>
                        </div>

                        <div class="card-body">
                            <asp:GridView ID="gvVehiculos" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-custom"
                                OnRowCommand="gvVehiculos_RowCommand">

                                <Columns>
                                    <asp:BoundField DataField="idVehiculo" HeaderText="ID" />
                                    <asp:BoundField DataField="placa" HeaderText="Placa" />
                                    <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                    <asp:BoundField DataField="capacidad" HeaderText="Capacidad" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />

                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server"
                                                CssClass="btn-edit"
                                                CommandName="editar"
                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEstado" runat="server"
                                                CssClass="btn-state"
                                                CommandName="cambiarEstado"
                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                <i class="bi bi-arrow-repeat"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEliminar" runat="server"
                                                CssClass="btn-delete"
                                                CommandName="eliminar"
                                                CommandArgument="<%# Container.DataItemIndex %>"
                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este vehículo?');">
                                                <i class="bi bi-trash3"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 mb-4">
                    <div class="content-card">

                        <div class="card-header-custom">
                            <i class="bi bi-pencil brand-icon"></i>
                            <h3>Editar Vehículo</h3>
                        </div>

                        <div class="card-body">

                            <asp:TextBox ID="txtIdVehiculo" runat="server" Visible="false"></asp:TextBox>

                            <div class="mb-3">
                                <label class="form-label">Placa</label>
                                <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Modelo</label>
                                <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Capacidad</label>
                                <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Estado</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1">Disponible</asp:ListItem>
                                    <asp:ListItem Value="2">En mantenimiento</asp:ListItem>
                                    <asp:ListItem Value="3">Fuera de servicio</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                                CssClass="btn-save"
                                OnClick="btnGuardar_Click" />

                            <asp:Label ID="lblMensaje" runat="server" CssClass="alert-message"></asp:Label>

                        </div>

                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="content-card">

                        <div class="card-header-custom">
                            <i class="bi bi-plus-circle brand-icon"></i>
                            <h3>Agregar Nuevo Vehículo</h3>
                        </div>

                        <div class="card-body row">

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Placa</label>
                                <asp:TextBox ID="txtAddPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Modelo</label>
                                <asp:TextBox ID="txtAddModelo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Capacidad</label>
                                <asp:TextBox ID="txtAddCapacidad" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Estado</label>
                                <asp:DropDownList ID="ddlAddEstado" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1">Disponible</asp:ListItem>
                                    <asp:ListItem Value="2">En mantenimiento</asp:ListItem>
                                    <asp:ListItem Value="3">Fuera de servicio</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-12 mt-2">
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Vehículo"
                                    CssClass="btn-add"
                                    OnClick="btnAgregar_Click" />

                                <asp:Label ID="lblAddMensaje" runat="server" CssClass="alert-message"></asp:Label>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
