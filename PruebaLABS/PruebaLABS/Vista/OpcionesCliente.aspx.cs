using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesCliente : System.Web.UI.Page
    {
        ClClienteL clienteL = new ClClienteL();
        ClSolicitudViajeL viajeL = new ClSolicitudViajeL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idCliente"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                CargarDatFlota();
                CargarEmpresas();
                CargarMisPedidos();

                MostrarPanelSolicitarPedido();
            }
        }

        private void CargarDatFlota()
        {
            DataTable dt = clienteL.ListaDatVehiculo();
            gvFlota.DataSource = dt;
            gvFlota.DataBind();
        }

        private void CargarMisPedidos()
        {
            try
            {
                int idCliente = Convert.ToInt32(Session["idCliente"]);
                DataTable dtPedidos = viajeL.MtObtenerViajesCliente(idCliente);

                if (dtPedidos != null && dtPedidos.Rows.Count > 0)
                {
                    gvMisPedidos.DataSource = dtPedidos;
                    gvMisPedidos.DataBind();
                }
                else
                {
                    gvMisPedidos.DataSource = null;
                    gvMisPedidos.DataBind();
                }
            }
            catch 
            {
                gvMisPedidos.DataSource = null;
                gvMisPedidos.DataBind();
            }
        }

        private void CargarEmpresas()
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.Items.Add(new ListItem("Seleccione su empresa", ""));
            ddlEmpresa.Items.Add(new ListItem("TransporteAndes", "TransporteAndes"));
            ddlEmpresa.Items.Add(new ListItem("LogiCar S.A.", "LogiCar S.A."));
            ddlEmpresa.Items.Add(new ListItem("CargaExpress", "CargaExpress"));
            ddlEmpresa.Items.Add(new ListItem("Otra empresa", "Otra"));
        }

        private void MostrarPanelSolicitarPedido()
        {
            pnlSolicitarPedido.Visible = true;
            pnlVisualizarPedidos.Visible = false;
            pnlCajonPreguntas.Visible = false;
            pnlFlotaVehiculos.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item active";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnCajonPreguntas.CssClass = "sidebar-item";
            btnFlotaVehiculos.CssClass = "sidebar-item";
        }

        private void MostrarPanelVisualizarPedidos()
        {
            pnlSolicitarPedido.Visible = false;
            pnlVisualizarPedidos.Visible = true;
            pnlCajonPreguntas.Visible = false;
            pnlFlotaVehiculos.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item active";
            btnCajonPreguntas.CssClass = "sidebar-item";
            btnFlotaVehiculos.CssClass = "sidebar-item";

            CargarMisPedidos();
        }

        private void MostrarPanelCajonPreguntas()
        {
            pnlSolicitarPedido.Visible = false;
            pnlVisualizarPedidos.Visible = false;
            pnlCajonPreguntas.Visible = true;
            pnlFlotaVehiculos.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnCajonPreguntas.CssClass = "sidebar-item active";
            btnFlotaVehiculos.CssClass = "sidebar-item";
        }

        private void MostrarPanelFlotaVehiculos()
        {
            pnlSolicitarPedido.Visible = false;
            pnlVisualizarPedidos.Visible = false;
            pnlCajonPreguntas.Visible = false;
            pnlFlotaVehiculos.Visible = true;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnCajonPreguntas.CssClass = "sidebar-item";
            btnFlotaVehiculos.CssClass = "sidebar-item active";

            CargarDatFlota();
        }

        protected void btnSolicitarPedido_Click(object sender, EventArgs e)
        {
            MostrarPanelSolicitarPedido();
        }

        protected void btnVisualizarPedidos_Click(object sender, EventArgs e)
        {
            MostrarPanelVisualizarPedidos();
        }

        protected void btnCajonPreguntas_Click(object sender, EventArgs e)
        {
            MostrarPanelCajonPreguntas();
        }

        protected void btnFlotaVehiculos_Click(object sender, EventArgs e)
        {
            MostrarPanelFlotaVehiculos();
        }

        protected void btnSolicitarViaje_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrigen.Text) ||
                    string.IsNullOrEmpty(txtDestino.Text) ||
                    string.IsNullOrEmpty(txtFechaSalida.Text))
                {
                    lblMensaje.Text = "Por favor complete los campos obligatorios: Origen, Destino y Fecha de Salida";
                    lblMensaje.Style["color"] = "#dc3545";
                    lblMensaje.Visible = true;
                    return;
                }

                int idCliente = Convert.ToInt32(Session["idCliente"]);

                string resultado = viajeL.MtSolicitarViaje(
                    txtOrigen.Text.Trim(),
                    txtDestino.Text.Trim(),
                    txtFechaSalida.Text,
                    txtFechaLlegada.Text,
                    ddlTipoCarga.SelectedValue,
                    txtMotivo.Text.Trim(),
                    txtObservaciones.Text.Trim(),
                    idCliente
                );

                if (resultado.Contains("exitosamente"))
                {
                    lblMensaje.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal", "mostrarModalConfirmacion();", true);

                    LimpiarFormulario();

                    CargarMisPedidos();
                }
                else
                {
                    lblMensaje.Text = "❌ " + resultado;
                    lblMensaje.Style["color"] = "#dc3545";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error al solicitar el viaje: " + ex.Message;
                lblMensaje.Style["color"] = "#dc3545";
                lblMensaje.Visible = true;
            }
        }

        protected void btnEnviarConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlTipoConsulta.SelectedValue) ||
                    string.IsNullOrEmpty(txtAsunto.Text) ||
                    string.IsNullOrEmpty(txtMensajeConsulta.Text))
                {
                    lblMensajeConsultaResult.Text = "Por favor complete todos los campos de la consulta";
                    lblMensajeConsultaResult.Style["color"] = "#dc3545";
                    return;
                }


                lblMensajeConsultaResult.Text = "✅ Tu consulta ha sido enviada exitosamente. Te contactaremos pronto.";
                lblMensajeConsultaResult.Style["color"] = "#198754";

                ddlTipoConsulta.SelectedIndex = 0;
                txtAsunto.Text = "";
                txtMensajeConsulta.Text = "";
            }
            catch (Exception ex)
            {
                lblMensajeConsultaResult.Text = "❌ Error al enviar la consulta: " + ex.Message;
                lblMensajeConsultaResult.Style["color"] = "#dc3545";
            }
        }

        private void LimpiarFormulario()
        {
            txtOrigen.Text = "";
            txtDestino.Text = "";
            txtFechaSalida.Text = "";
            txtFechaLlegada.Text = "";
            txtMotivo.Text = "";
            txtObservaciones.Text = "";
            ddlTipoCarga.SelectedIndex = 0;
            ddlEmpresa.SelectedIndex = 0;
        }
    }
}