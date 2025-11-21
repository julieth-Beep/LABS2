using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {

        ClUsuarioL logicaUsuario = new ClUsuarioL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Visible = false;
            }
        }

        protected void btnRegistrarr_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumento.Text) ||
                    string.IsNullOrEmpty(txtNombre.Text) ||
                    string.IsNullOrEmpty(txtApellido.Text) ||
                    string.IsNullOrEmpty(txtCorreo.Text) ||
                    string.IsNullOrEmpty(txtPassword.Text) ||
                    string.IsNullOrEmpty(ddlRol.SelectedValue))
                {
                    lblMensaje.Text = "Por favor complete todos los campos obligatorios";
                    lblMensaje.Style["color"] = "#dc3545";
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    lblMensaje.Text = "Las contraseñas no coinciden";
                    lblMensaje.Style["color"] = "#dc3545";
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    lblMensaje.Text = "La contraseña debe tener al menos 6 caracteres";
                    lblMensaje.Style["color"] = "#dc3545";
                    return;
                }

                string resultado = logicaUsuario.MtRegistrarUsuario(
                    txtDocumento.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtPassword.Text,
                    ddlRol.SelectedValue
                );

                if (resultado.Contains("exitosamente"))
                {
                    lblMensaje.Text = "✅ " + resultado + ". Redirigiendo a tu página...";
                    lblMensaje.Style["color"] = "#198754";

                    string rol = ddlRol.SelectedValue;
                    string redirectPage = "";

                    switch (rol)
                    {
                        case "1":
                            redirectPage = "OpcionesConductor.aspx";
                            break;
                        case "2":
                            redirectPage = "opcionesAdmin.aspx";
                            break;
                        case "3":
                            redirectPage = "OpcionesContador.aspx";
                            break;
                    }

                    Session["rol"] = rol;
                    Session["nombre"] = txtNombre.Text.Trim();
                    Session["correo"] = txtCorreo.Text.Trim();

                    Response.Redirect(redirectPage);
                }
                else
                {
                    lblMensaje.Text = "❌ " + resultado;
                    lblMensaje.Style["color"] = "#dc3545";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error al registrar: " + ex.Message;
                lblMensaje.Style["color"] = "#dc3545";
            }
        }

        private void LimpiarFormulario()
        {
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            ddlRol.SelectedIndex = 0;
        }
    }
}