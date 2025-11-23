<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionesConductor.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesConductor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card {
            border: none;
            border-radius: 18px;
            background: #ffffff;
            padding: 0;
            box-shadow: 0 6px 22px rgba(0, 0, 0, 0.12);
        }

        .card-header {
            padding: 18px 25px;
            background: #ffffff !important;
            border-bottom: 1px solid #e5e5e5;
        }

        .card-title {
            font-size: 1.3rem;
            font-weight: 700;
            margin: 0;
            color: #2E7D32;
        }

        .container-fluid {
            animation: fadeIn 0.3s ease-in-out;
        }

        .table {
            margin-bottom: 0;
            background: #ffffff;
        }

            .table th {
                background: #f8f9fa !important;
                font-weight: 700;
                color: #2E7D32;
                padding: 14px 16px !important;
                font-size: 0.97rem;
            }

            .table td {
                padding: 13px 15px !important;
                font-size: 0.95rem;
            }

        .table-hover tbody tr:hover {
            background-color: rgba(46, 125, 50, 0.12) !important;
        }

        .btn,
        .btn-state,
        .btn-edit,
        .btn-delete,
        .btn-save {
            border-radius: 10px;
            padding: 9px 16px !important;
            font-weight: 600;
            transition: 0.2s ease-in-out;
            border: none;
        }

        .btn-success,
        .btn-outline-success,
        .btn-state,
        .btn-save {
            background-color: #2E7D32 !important;
            color: #fff !important;
            border: none !important;
        }

            .btn-success:hover,
            .btn-outline-success:hover,
            .btn-state:hover,
            .btn-save:hover {
                background-color: #256628 !important;
            }

        .btn-outline-success {
            background: transparent !important;
            border: 2px solid #2E7D32 !important;
            color: #2E7D32 !important;
        }

            .btn-outline-success:hover {
                background: #2E7D32 !important;
                color: white !important;
            }

        .badge-estado {
            font-size: 0.80rem;
            padding: 6px 14px;
            border-radius: 25px;
            font-weight: 700;
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

        .content-card {
            background: #ffffff;
            border-radius: 15px;
            padding: 20px;
            box-shadow: 0 4px 18px rgba(0, 0, 0, 0.12);
            margin-top: 40px;
        }

        .card-header-custom {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 15px;
        }

            .card-header-custom h3 {
                margin: 0;
                font-size: 1.2rem;
                font-weight: 700;
                color: #2E7D32;
            }

        .brand-icon {
            font-size: 1.4rem;
            color: #2E7D32;
        }

        .form-label {
            font-weight: 600;
            color: #2E7D32;
        }

        .form-control {
            border-radius: 12px;
            border: 1px solid #c9d8c9;
            padding: 10px 14px;
            font-size: 0.95rem;
        }

            .form-control:focus {
                border-color: #2E7D32;
                box-shadow: 0 0 0 0.15rem rgba(46, 125, 50, 0.25);
            }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(4px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-4">


        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-success">
                    <i class="bi bi-truck me-2"></i>Panel del Conductor
                </h2>

                <p class="text-muted">
                    Bienvenido,
                    <asp:Label ID="lblNombreConductor" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </div>
        </div>


        <asp:Panel ID="pnlTablaViajes" runat="server" Visible="true">
            <div class="card">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-list-check me-2"></i>Mis Viajes Asignados
                    </h5>

                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar"
                        CssClass="btn btn-outline-success btn-sm" OnClick="btnActualizar_Click" />
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvViajes" runat="server"
                            CssClass="table table-hover table-striped"
                            EmptyDataText="No hay viajes asignados"
                            AutoGenerateColumns="false"
                            OnRowCommand="gvViajes_RowCommand"
                            DataKeyNames="idViaje">

                            <Columns>

                                <asp:BoundField DataField="idViaje" HeaderText="ID" />
                                <asp:BoundField DataField="puntoPartida" HeaderText="Origen" />
                                <asp:BoundField DataField="destino" HeaderText="Destino" />
                                <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Salida" />
                                <asp:BoundField DataField="fechaFin" HeaderText="Fecha llegada" />
                                <asp:BoundField DataField="costo" HeaderText="Costo del viaje" />
                                <asp:BoundField DataField="placa" HeaderText="Vehículo" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="capacidad" HeaderText="Capacidad" />
                                <asp:BoundField DataField="distancia" HeaderText="Distancia" />
                                <asp:BoundField DataField="tipoCarga" HeaderText="Tipo de carga" />
                                <asp:BoundField DataField="motivo" HeaderText="Motivo" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                <asp:BoundField DataField="idCliente" HeaderText="Cliente" />


                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <span class='badge-estado badge-<%# Eval("estadoViaje").ToString().ToLower().Replace(" ", "") %>'>
                                            <%# Eval("estadoViaje") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEstado" runat="server"
                                            CssClass="btn-state"
                                            ToolTip="Editar estado del viaje"
                                            CommandName="cambiarEstado"
                                            CommandArgument="<%# Container.DataItemIndex %>">
                                            <i class="bi bi-pencil-square"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </asp:Panel>




        <asp:Panel ID="pnlEditar" runat="server" Visible="false" CssClass="col-lg-4 mb-4">
            <div class="content-card">

                <div class="card-header-custom">
                    <i class="bi bi-pencil brand-icon"></i>
                    <h3>Editar estado del viaje</h3>
                </div>

                <div class="card-body">

                    <asp:TextBox ID="txtEstado" runat="server" Visible="false"></asp:TextBox>

                    <div class="mb-3">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Completado</asp:ListItem>
                            <asp:ListItem Value="2">En curso</asp:ListItem>
                            <asp:ListItem Value="3">Pendiente</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                        CssClass="btn-save"
                        OnClick="btnGuardar_Click" />

                    <asp:Label ID="Label1" runat="server" CssClass="alert-message"></asp:Label>
                </div>
            </div>
        </asp:Panel>



        <div class="mt-3">
            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="alert alert-dismissible fade show"
                Visible="false"></asp:Label>
        </div>

    </div>
</asp:Content>
