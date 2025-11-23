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
            }
        }

        private void CargarDatFlota()
        {
            DataTable dt = clienteL.ListaDatVehiculo();
            gvFlota.DataSource = dt;
            gvFlota.DataBind();
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