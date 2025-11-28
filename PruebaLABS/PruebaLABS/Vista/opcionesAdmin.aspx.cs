using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesAdmin : Page
    {
        ClVehiculoD oVehiculoD = new ClVehiculoD();
        ClUsuarioL logicaUsuario = new ClUsuarioL();
        ClSolicitudViajeL logicaSolicitud = new ClSolicitudViajeL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] == null || Session["rol"] == null || (int)Session["rol"] != 2)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                pnlVehiculos.Visible = true;
                pnlUsuarios.Visible = false;
                pnlRegistro.Visible = false;
                pnlReportes.Visible = false;
                pnlClientes.Visible = false;

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
            btnClientes.CssClass = "sidebar-item";

            boton.CssClass = "sidebar-item active";
        }

        protected void btnVehiculos_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = true;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = false;
            pnlClientes.Visible = false;

            ActivarMenu(btnVehiculos);
            MtCargarVehiculos();
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = true;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = false;
            pnlClientes.Visible = false;

            ActivarMenu(btnUsuarios);
            MtCargarUsuarios();
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = true;
            pnlReportes.Visible = false;
            pnlClientes.Visible = false;

            ActivarMenu(btnRegistro);
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = true;
            pnlClientes.Visible = false;

            ActivarMenu(btnReportes);
        }

        protected void btnClientes_Click(object sender, EventArgs e)
        {
            pnlVehiculos.Visible = false;
            pnlUsuarios.Visible = false;
            pnlRegistro.Visible = false;
            pnlReportes.Visible = false;
            pnlClientes.Visible = true;

            ActivarMenu(btnClientes);
            CargarTodasLasSolicitudes();
        }

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
                    lblMensajeRegistro.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalUsuario", "mostrarModalConfirmacionUsuario();", true);

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


        private void CargarTodasLasSolicitudes()
        {
            try
            {
                DataTable dtSolicitudes = logicaSolicitud.MtObtenerTodasLasSolicitudes();
                Session["SolicitudesData"] = dtSolicitudes;
                gvSolicitudesClientes.DataSource = dtSolicitudes;
                gvSolicitudesClientes.DataBind();

                lblTotalRegistros.Text = dtSolicitudes.Rows.Count.ToString();
                lblMensajeSolicitudes.Text = "Se cargaron " + dtSolicitudes.Rows.Count + " solicitudes.";
                lblMensajeSolicitudes.Style["color"] = "#198754";
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al cargar las solicitudes: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string documento = txtBuscarDocumento.Text.Trim();
            if (string.IsNullOrEmpty(documento))
            {
                lblMensajeSolicitudes.Text = "Por favor ingrese un documento para buscar.";
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
                return;
            }

            try
            {
                DataTable dtSolicitudes = logicaSolicitud.MtObtenerSolicitudesPorDocumento(documento);
                Session["SolicitudesData"] = dtSolicitudes;
                gvSolicitudesClientes.DataSource = dtSolicitudes;
                gvSolicitudesClientes.DataBind();

                lblTotalRegistros.Text = dtSolicitudes.Rows.Count.ToString();

                if (dtSolicitudes.Rows.Count > 0)
                {
                    lblMensajeSolicitudes.Text = "Se encontraron " + dtSolicitudes.Rows.Count + " solicitudes para el documento: " + documento;
                    lblMensajeSolicitudes.Style["color"] = "#198754";
                }
                else
                {
                    lblMensajeSolicitudes.Text = "No se encontraron solicitudes para el documento: " + documento;
                    lblMensajeSolicitudes.Style["color"] = "#6c757d";
                }
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al buscar: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
            }
        }

        protected void ddlFiltrarEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estado = ddlFiltrarEstado.SelectedValue;
            if (!string.IsNullOrEmpty(estado))
            {
                FiltrarPorEstado(estado);
            }
            else
            {
                CargarTodasLasSolicitudes();
            }
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscarDocumento.Text = "";
            ddlFiltrarEstado.SelectedIndex = 0;
            CargarTodasLasSolicitudes();
        }

        private void FiltrarPorEstado(string estado)
        {
            try
            {
                DataTable dtSolicitudes = logicaSolicitud.MtObtenerSolicitudesPorEstado(estado);
                Session["SolicitudesData"] = dtSolicitudes;
                gvSolicitudesClientes.DataSource = dtSolicitudes;
                gvSolicitudesClientes.DataBind();

                lblTotalRegistros.Text = dtSolicitudes.Rows.Count.ToString();
                lblMensajeSolicitudes.Text = "Solicitudes con estado: " + estado + " (" + dtSolicitudes.Rows.Count + " encontradas)";
                lblMensajeSolicitudes.Style["color"] = "#198754";
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al filtrar: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
            }
        }

        protected void gvSolicitudesClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSolicitudesClientes.EditIndex = e.NewEditIndex;
            CargarDatosGridView();
        }

        protected void gvSolicitudesClientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idViaje = Convert.ToInt32(gvSolicitudesClientes.DataKeys[e.RowIndex].Value);

                GridViewRow row = gvSolicitudesClientes.Rows[e.RowIndex];

                string fechaLlegada = ((TextBox)row.FindControl("txtFechaLlegada")).Text;
                string estado = ((DropDownList)row.FindControl("ddlEstado")).SelectedValue;
                string costo = ((TextBox)row.FindControl("txtCosto")).Text;

                string resultado = logicaSolicitud.MtActualizarSolicitud(idViaje, estado, costo, fechaLlegada, "");

                gvSolicitudesClientes.EditIndex = -1;
                CargarDatosGridView();

                lblMensajeSolicitudes.Text = resultado;
                lblMensajeSolicitudes.Style["color"] = "#198754";
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al actualizar: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
            }
        }

        protected void gvSolicitudesClientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSolicitudesClientes.EditIndex = -1;
            CargarDatosGridView();
        }

        protected void gvSolicitudesClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idViaje = Convert.ToInt32(gvSolicitudesClientes.DataKeys[e.RowIndex].Value);

                string resultado = logicaSolicitud.MtEliminarSolicitud(idViaje);
                CargarDatosGridView();

                lblMensajeSolicitudes.Text = resultado;
                lblMensajeSolicitudes.Style["color"] = "#198754";
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al eliminar: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
            }
        }

        private void CargarDatosGridView()
        {
            DataTable dt = Session["SolicitudesData"] as DataTable;
            if (dt != null)
            {
                gvSolicitudesClientes.DataSource = dt;
                gvSolicitudesClientes.DataBind();
            }
        }

        public string GetEstadoBadgeClass(string estado)
        {
            switch (estado)
            {
                case "Pendiente": return "warning";
                case "Aprobado": return "info";
                case "En curso": return "primary";
                case "Completado": return "success";
                case "Cancelado": return "danger";
                default: return "secondary";
            }
        }

        private void MtCargarUsuarios()
        {
            gvUsuarios.DataSource = logicaUsuario.MtListarUsuarios();
            gvUsuarios.DataBind();
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
