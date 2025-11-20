<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionesContador.aspx.cs" Inherits="PruebaLABS.Vista.OpcionesContador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
 <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" rel="stylesheet" />

 <div class="container my-5">

     <div class="card shadow border-0 rounded-4">
         <div class="card-body">

             <h3 class="text-center mb-4 fw-semibold">
                 <i class="bi bi-cash-stack text-success"></i>Gastos por Conductor
             </h3>

             <div class="table-responsive">
                 <asp:GridView
                     ID="gvGastos"
                     runat="server"
                     AutoGenerateColumns="false"
                     CssClass="table table-hover table-striped table-bordered align-middle"
                     HeaderStyle-CssClass="table-success text-center"
                     RowStyle-CssClass="text-center"
                     BorderStyle="None">

                     <Columns>
                         <asp:BoundField DataField="nombreConductor" HeaderText="Conductor" />
                         <asp:BoundField DataField="apellidoConductor" HeaderText="Apellido" />
                         <asp:BoundField DataField="tipoGasto" HeaderText="Tipo" />
                         <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:N2}" />
                         <asp:BoundField DataField="fechaGasto" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                         <asp:BoundField DataField="placa" HeaderText="Placa" />
                         <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                         <asp:BoundField DataField="rol" HeaderText="Rol" />
                     </Columns>

                 </asp:GridView>
             </div>

         </div>
     </div>

 </div>
</asp:Content>
