using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesAdmin : Page
    {
        // Solo las instancias que realmente usamos
        ClVehiculoD oVehiculoD = new ClVehiculoD();
        ClUsuarioL logicaUsuario = new ClUsuarioL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Panel por defecto
                pnlVehiculos.Visible = true;
                pnlUsuarios.Visible = false;
                pnlRegistro.Visible = false;
                pnlReportes.Visible = false;

                MtCargarVehiculos();
                ActivarMenu(btnVehiculos);

                if (lblMensajeRegistro != null)
                    lblMensajeRegistro.Visible = false;
            }
        }

        private void ActivarMenu(Button boton)
        {
            btnVehiculos.CssClass = "sidebar-item";
            btnUsuarios.CssClass = "sidebar-item";
            btnRegistro.CssClass = "sidebar-item";
            btnReportes.CssClass = "sidebar-item";

            boton.CssClass = "sidebar-item active";
        }

        // ----- BOTONES DEL MENÚ LATERAL -----

        protected void btnVehiculos_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = true;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = false;

            ActivarMenu(btnVehiculos);
            MtCargarVehiculos();
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = true;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = false;

            ActivarMenu(btnUsuarios);
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = true;
            pnlReportes.Visible = false;

            ActivarMenu(btnRegistro);
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = true;

            ActivarMenu(btnReportes);
        }

        // ----- VEHÍCULOS -----

        private void MtCargarVehiculos()
        {
            gvVehiculos.DataSource = oVehiculoD.MtListarVehiculos();
            gvVehiculos.DataBind();
        }

        protected void gvVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvVehiculos.Rows[index];
            int idVehiculo = Convert.ToInt32(row.Cells[0].Text);

            if (e.CommandName == "editar")
            {
                txtIdVehiculo.Text = row.Cells[0].Text;
                txtPlaca.Text = row.Cells[1].Text;
                txtModelo.Text = row.Cells[2].Text;
                txtCapacidad.Text = row.Cells[3].Text;

                ddlEstado.ClearSelection();
                ListItem item = ddlEstado.Items.FindByText(row.Cells[4].Text);
                if (item != null)
                    item.Selected = true;

                lblMensaje.Text = "Vehículo cargado para edición.";
            }

            if (e.CommandName == "cambiarEstado")
            {
                string estadoActual = row.Cells[4].Text;
                int nuevoEstado = 1;

                if (estadoActual == "Disponible") nuevoEstado = 2;
                else if (estadoActual == "En mantenimiento") nuevoEstado = 3;
                else if (estadoActual == "Fuera de servicio") nuevoEstado = 1;

                string mensaje = oVehiculoD.MtCambiarEstado(idVehiculo, nuevoEstado);
                lblMensaje.Text = mensaje;

                MtCargarVehiculos();
            }

            if (e.CommandName == "eliminar")
            {
                string mensaje = oVehiculoD.MtEliminarVehiculo(idVehiculo);
                lblMensaje.Text = mensaje;

                MtCargarVehiculos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ClVehiculoM v = new ClVehiculoM()
            {
                idVehiculo = int.Parse(txtIdVehiculo.Text),
                placa = txtPlaca.Text,
                modelo = txtModelo.Text,
                capacidad = txtCapacidad.Text,
                idEstadoVehiculo = ddlEstado.SelectedIndex + 1
            };

            string mensaje = oVehiculoD.MtEditarVehiculo(v);
            lblMensaje.Text = mensaje;

            MtCargarVehiculos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ClVehiculoM v = new ClVehiculoM()
            {
                placa = txtAddPlaca.Text,
                modelo = txtAddModelo.Text,
                capacidad = txtAddCapacidad.Text,
                idEstadoVehiculo = ddlAddEstado.SelectedIndex + 1
            };

            string mensaje = oVehiculoD.MtAgregarVehiculo(v);
            lblAddMensaje.Text = mensaje;

            MtCargarVehiculos();

            txtAddPlaca.Text = "";
            txtAddModelo.Text = "";
            txtAddCapacidad.Text = "";
            ddlAddEstado.SelectedIndex = 0;
        }



        protected void btnRegistrarr_Click(object sender, EventArgs e)
        {
            lblMensajeRegistro.Visible = true;

            try
            {
        
                if (string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(ddlRol.SelectedValue))
                {
                    lblMensajeRegistro.Text = "Por favor complete todos los campos obligatorios.";
                    lblMensajeRegistro.Style["color"] = "#dc3545";
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    lblMensajeRegistro.Text = "Las contraseñas no coinciden.";
                    lblMensajeRegistro.Style["color"] = "#dc3545";
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    lblMensajeRegistro.Text = "La contraseña debe tener al menos 6 caracteres.";
                    lblMensajeRegistro.Style["color"] = "#dc3545";
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
                    lblMensajeRegistro.Text = "✅ " + resultado;
                    lblMensajeRegistro.Style["color"] = "#198754";
                    LimpiarFormularioRegistro();
                }
                else
                {
                    lblMensajeRegistro.Text = "❌ " + resultado;
                    lblMensajeRegistro.Style["color"] = "#dc3545";
                }
            }
            catch (Exception ex)
            {
                lblMensajeRegistro.Text = "❌ Error al registrar: " + ex.Message;
                lblMensajeRegistro.Style["color"] = "#dc3545";
            }
        }

        private void LimpiarFormularioRegistro()
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
