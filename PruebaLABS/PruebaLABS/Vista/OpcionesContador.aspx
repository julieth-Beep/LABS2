<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionesContador.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesContador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f4f6f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .container-top {
            margin-top: 24px;
        }

        .card-rounded {
            border-radius: 12px;
        }

        .table-sm td, .table-sm th {
            padding: .4rem .6rem;
        }

        .nav-link {
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- NAVBAR (EN LA MISMA PÁGINA) -->
    <nav class="navbar navbar-expand-lg navbar-dark" style="background-color: #2E7D32;">
        <div class="container-fluid">
            <a class="navbar-brand fw-bold"><i class="bi bi-speedometer2"></i>Panel Contador</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navContador">
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navContador">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item"><a class="nav-link" id="lnkContratos" runat="server" onserverclick="ShowContratos">Contratos</a></li>
                    <li class="nav-item"><a class="nav-link" id="lnkGastos" runat="server" onserverclick="ShowGastos">Gastos Realizados</a></li>
                    <li class="nav-item"><a class="nav-link" id="lnkBonos" runat="server" onserverclick="ShowBonos">Bonos</a></li>
                    <li class="nav-item"><a class="nav-link" id="lnkTotales" runat="server" onserverclick="ShowTotales">Total a Pagar</a></li>
                    <li class="nav-item"><a class="nav-link" id="lnkContratoUsuario" runat="server" onserverclick="ShowContratoUsuario">Contrato por Usuario</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container container-top">

        <asp:Label ID="lblFeedback" runat="server" CssClass="fw-bold"></asp:Label>

        <!-- ====== CONTRATOS (LISTA GENERAL) ====== -->
        <asp:GridView ID="gvContratos" runat="server"
            CssClass="table table-striped table-bordered table-sm"
            AutoGenerateColumns="false"
            OnRowCommand="gvContratos_RowCommand"
            DataKeyNames="idContrato">

            <Columns>
                <asp:BoundField DataField="documento" HeaderText="Documento" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="salario" HeaderText="Salario" DataFormatString="{0:C0}" />
                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                <asp:BoundField DataField="idContrato" HeaderText="ID" Visible="false" />

                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Editar" CssClass="btn btn-primary btn-sm"
                            CommandName="Editar" CommandArgument='<%# Eval("idContrato") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="panelEditarContrato" runat="server" CssClass="card p-3 mt-3" Visible="false">

            <asp:HiddenField ID="hfIdContratoEditar" runat="server" />

            <h5>Editar Contrato</h5>

            <div class="row mt-3">

                <div class="col-md-3">
                    <label>Documento</label>
                    <asp:TextBox ID="txtEditDocumento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Salario</label>
                    <asp:TextBox ID="txtEditSalario" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Tipo</label>
                    <asp:TextBox ID="txtEditTipo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Bono</label>
                    <asp:TextBox ID="txtEditBono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

            </div>

            <div class="mt-3">
                <asp:Button ID="btnGuardarEdicion" runat="server" Text="Guardar Cambios"
                    CssClass="btn btn-success" OnClick="btnGuardarEdicion_Click" />
                <asp:Button ID="btnCancelarEdicion" runat="server" Text="Cancelar"
                    CssClass="btn btn-secondary" OnClick="btnCancelarEdicion_Click" />
            </div>

        </asp:Panel>



        <!-- ====== GASTOS ====== -->
        <div id="panelGastos" runat="server" visible="false" class="mb-4">
            <div class="card card-rounded">
                <div class="card-header bg-success text-white text-center">
                    <h5 class="mb-0"><i class="bi bi-cash-stack"></i>Gastos por Conductor</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvGastos" runat="server"
                        CssClass="table table-striped table-bordered table-sm text-center"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="nombreConductor" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellidoConductor" HeaderText="Apellido" />
                            <asp:BoundField DataField="tipoGasto" HeaderText="Tipo de Gasto" />
                            <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="fechaGasto" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- ====== BONOS ====== -->
        <div id="panelBonos" runat="server" visible="false" class="mb-4">
            <div class="card card-rounded">
                <div class="card-header bg-primary text-white text-center">
                    <h5 class="mb-0"><i class="bi bi-award"></i>Bonos por Conductor</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvBonos" runat="server"
                        CssClass="table table-striped table-bordered table-sm text-center"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="totalViajes" HeaderText="Viajes" />
                            <asp:BoundField DataField="bonoTotal" HeaderText="Bono Total" DataFormatString="{0:N0}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- ====== TOTALES ====== -->
        <div id="panelTotales" runat="server" visible="false" class="mb-4">
            <div class="card card-rounded">
                <div class="card-header bg-dark text-white text-center">
                    <h5 class="mb-0"><i class="bi bi-calculator"></i>Total a Pagar</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvTotal" runat="server"
                        CssClass="table table-striped table-bordered table-sm text-center"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="documento" HeaderText="Documento" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="salario" HeaderText="Salario" DataFormatString="{0:C0}" />
                            <asp:BoundField DataField="bono" HeaderText="Bono" DataFormatString="{0:C0}" />
                            <asp:BoundField DataField="totalPagar" HeaderText="Total a Pagar" DataFormatString="{0:C0}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- ====== CONTRATO POR USUARIO (BUSCAR + REGISTRAR) ====== -->
        <div id="panelContratoUsuario" runat="server" visible="false" class="mb-4">
            <div class="card card-rounded">
                <div class="card-header bg-warning text-dark text-center">
                    <h5 class="mb-0"><i class="bi bi-person-lines-fill"></i>Contratos por Usuario</h5>
                </div>
                <div class="card-body">
                    <div class="row g-2 mb-3">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtBuscarDocumento" runat="server" CssClass="form-control" placeholder="Documento"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary w-100" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>

                    <asp:GridView ID="gvUsuario" runat="server" CssClass="table table-striped table-bordered table-sm"
                        AutoGenerateColumns="false" DataKeyNames="idUusuario,documento">
                        <Columns>
                            <asp:BoundField DataField="documento" HeaderText="Documento" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="salario" HeaderText="Salario" DataFormatString="{0:C0}" />
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                        </Columns>
                    </asp:GridView>

                    <hr />

                    <h6>Registrar Contrato</h6>
                    <div class="row g-2">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRegDocumento" runat="server" CssClass="form-control" placeholder="Documento"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRegSalario" runat="server" CssClass="form-control" placeholder="Salario"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlRegTipo" runat="server" CssClass="form-control">
                                <asp:ListItem>Fijo</asp:ListItem>
                                <asp:ListItem>Temporal</asp:ListItem>
                                <asp:ListItem>Contrato por viaje</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 mt-2">
                            <asp:Button ID="btnRegistrarContrato" runat="server" CssClass="btn btn-success w-100" Text="Registrar" OnClick="btnRegistrarContrato_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>


    <!-- Bootstrap JS (necesario para el modal) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
