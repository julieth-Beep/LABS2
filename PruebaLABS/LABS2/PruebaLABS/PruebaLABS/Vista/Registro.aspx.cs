using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class Registro : System.Web.UI.Page
    {
        ClClienteL logicaCliente = new ClClienteL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Visible = false;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrEmpty(txtDocumento.Text) ||
                    string.IsNullOrEmpty(txtNombre.Text) ||
                    string.IsNullOrEmpty(txtApellido.Text) ||
                    string.IsNullOrEmpty(txtEmpresa.Text) ||
                    string.IsNullOrEmpty(txtCorreo.Text))
                {
                    lblMensaje.Text = "Por favor complete todos los campos obligatorios (*)";
                    lblMensaje.Style["color"] = "#dc3545";
                    lblMensaje.Visible = true;
                    return;
                }

                // Registrar cliente
                string resultado = logicaCliente.MtRegistrarCliente(
                    txtDocumento.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtEmpresa.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCorreo.Text.Trim()
                );

                if (resultado.Contains("exitosamente"))
                {
                    lblMensaje.Text = "✅ " + resultado + ". Redirigiendo a tu panel...";
                    lblMensaje.Style["color"] = "#198754";
                    lblMensaje.Visible = true;

                    // Guardar datos en sesión para usar en OpcionesCliente
                    Session["cliente_documento"] = txtDocumento.Text.Trim();
                    Session["cliente_nombre"] = txtNombre.Text.Trim();
                    Session["cliente_empresa"] = txtEmpresa.Text.Trim();
                    Session["cliente_correo"] = txtCorreo.Text.Trim();

                    // Redirigir inmediatamente a OpcionesCliente
                    Response.AddHeader("REFRESH", "2;URL=OpcionesCliente.aspx");

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
                lblMensaje.Text = "❌ Error al registrar: " + ex.Message;
                lblMensaje.Style["color"] = "#dc3545";
                lblMensaje.Visible = true;
            }
        }

        private void LimpiarFormulario()
        {
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmpresa.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
        }
    }
}