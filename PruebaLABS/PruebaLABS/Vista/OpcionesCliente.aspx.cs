using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PruebaLABS.Datos;

namespace PruebaLABS.Vista
{
    public partial class OpcionesCliente : System.Web.UI.Page
    {
        ClClienteL clienteL = new ClClienteL();
        ClSolicitudViajeL viajeL = new ClSolicitudViajeL();
        ClNotificacionL notificacionL = new ClNotificacionL();

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
                CargarTickets();
                MostrarPanelSolicitarPedido();
                ActualizarContadorNotificaciones();
            }
        }

        private void MostrarPanelNotificaciones()
        {
            pnlSolicitarPedido.Visible = false;
            pnlVisualizarPedidos.Visible = false;
            pnlCajonPreguntas.Visible = false;
            pnlFlotaVehiculos.Visible = false;
            pnlNotificaciones.Visible = true;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnCajonPreguntas.CssClass = "sidebar-item";
            btnFlotaVehiculos.CssClass = "sidebar-item";
            btnNotificaciones.CssClass = "sidebar-item active";

            CargarNotificaciones();
        }

        private void CargarNotificaciones()
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);

                DataTable dt = notificacionL.MtListarNotificaciones(idCliente, false);

                gvNotificaciones.DataSource = dt;
                gvNotificaciones.DataBind();

                int total = notificacionL.MtContarTotalNotificaciones(idCliente);
                int nuevas = notificacionL.MtContarNotificacionesNuevas(idCliente);

                lblContadorNotificaciones.Text = $"{total} notificaciones ({nuevas} nuevas)";
                lblUltimaActualizacion.Text = "Última actualización: " + DateTime.Now.ToString("HH:mm:ss");

                ActualizarBadgeNotificaciones(nuevas);
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar notificaciones: " + ex.Message);
            }
        }

        private void ActualizarContadorNotificaciones()
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);
                int nuevas = notificacionL.MtContarNotificacionesNuevas(idCliente);

                ActualizarBadgeNotificaciones(nuevas);
            }
            catch (Exception)
            {
            }
        }

        private void ActualizarBadgeNotificaciones(int cantidadNuevas)
        {
            if (cantidadNuevas > 0)
            {
                string script = $@"
                    <script>
                        function actualizarBadgeNotificaciones() {{
                            var btn = document.getElementById('{btnNotificaciones.ClientID}');
                            if (btn) {{
                                var badgeExistente = btn.querySelector('.badge-notificacion');
                                if (badgeExistente) badgeExistente.remove();
                                
                                var badge = document.createElement('span');
                                badge.className = 'badge-notificacion';
                                badge.textContent = '{cantidadNuevas}';
                                badge.style.cssText = 'position: absolute; top: -8px; right: -8px; background: #dc3545; color: white; border-radius: 50%; width: 22px; height: 22px; font-size: 12px; display: flex; align-items: center; justify-content: center; font-weight: bold;';
                                btn.style.position = 'relative';
                                btn.appendChild(badge);
                            }}
                        }}
                        actualizarBadgeNotificaciones();
                    </script>
                ";

                ClientScript.RegisterStartupScript(this.GetType(), "ActualizarBadge", script);
            }
            else
            {
                string script = $@"
                    <script>
                        function eliminarBadgeNotificaciones() {{
                            var btn = document.getElementById('{btnNotificaciones.ClientID}');
                            if (btn) {{
                                var badge = btn.querySelector('.badge-notificacion');
                                if (badge) badge.remove();
                            }}
                        }}
                        eliminarBadgeNotificaciones();
                    </script>
                ";

                ClientScript.RegisterStartupScript(this.GetType(), "EliminarBadge", script);
            }
        }

        protected void btnNotificaciones_Click(object sender, EventArgs e)
        {
            MostrarPanelNotificaciones();
        }

        protected void btnMarcarTodasLeidas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);

                string resultado = notificacionL.MtMarcarComoLeidas(idCliente);
                MostrarExito(resultado);
                CargarNotificaciones();
                ActualizarContadorNotificaciones();
            }
            catch (Exception ex)
            {
                MostrarError("Error al marcar notificaciones: " + ex.Message);
            }
        }

        protected void btnEliminarLeidas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);

                string resultado = notificacionL.MtEliminarNotificacionesLeidas(idCliente);
                MostrarExito(resultado);
                CargarNotificaciones();
                ActualizarContadorNotificaciones();
            }
            catch (Exception ex)
            {
                MostrarError("Error al eliminar notificaciones: " + ex.Message);
            }
        }

        protected void btnEliminarTodas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);

                string resultado = notificacionL.MtEliminarTodasNotificaciones(idCliente);
                MostrarExito(resultado);
                CargarNotificaciones();
                ActualizarContadorNotificaciones();
            }
            catch (Exception ex)
            {
                MostrarError("Error al eliminar notificaciones: " + ex.Message);
            }
        }

        protected void btnActualizarNotificaciones_Click(object sender, EventArgs e)
        {
            CargarNotificaciones();
        }

        protected void gvNotificaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idNotificacion = Convert.ToInt32(gvNotificaciones.DataKeys[rowIndex].Value);

                switch (e.CommandName)
                {
                    case "marcarLeida":
                        string resultado1 = notificacionL.MtMarcarNotificacionLeida(idNotificacion);
                        MostrarExito(resultado1);
                        break;
                    case "marcarNoLeida":
                        string updateQuery = @"UPDATE Notificacion SET leido = 0 WHERE idNotificacion = @idNotificacion";

                        ClConexion conexion = new ClConexion();
                        SqlCommand cmd = new SqlCommand(updateQuery, conexion.MtAbrirConexion());
                        cmd.Parameters.AddWithValue("@idNotificacion", idNotificacion);
                        cmd.ExecuteNonQuery();
                        conexion.MtCerrarConexion();

                        MostrarExito("Notificación marcada como no leída");
                        break;
                    case "eliminar":
                        string resultado2 = notificacionL.MtEliminarNotificacion(idNotificacion);
                        MostrarExito(resultado2);
                        break;
                }

                CargarNotificaciones();
                ActualizarContadorNotificaciones();
            }
            catch (Exception ex)
            {
                MostrarError("Error en la operación: " + ex.Message);
            }
        }

        private void CargarDatFlota()
        {
            try
            {
                DataTable dt = clienteL.ListaDatVehiculo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvFlota.DataSource = dt;
                    gvFlota.DataBind();
                }
                else
                {
                    gvFlota.DataSource = null;
                    gvFlota.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar la flota: " + ex.Message);
            }
        }

        private void CargarMisPedidos()
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);
                DataTable dtPedidos = viajeL.MtObtenerViajesCliente(idCliente);

                if (dtPedidos != null && dtPedidos.Rows.Count > 0)
                {
                    gvHistorial.DataSource = dtPedidos;
                    gvHistorial.DataBind();
                }
                else
                {
                    gvHistorial.DataSource = null;
                    gvHistorial.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar los pedidos: " + ex.Message);
                gvHistorial.DataSource = null;
                gvHistorial.DataBind();
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
            pnlNotificaciones.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item active";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnNotificaciones.CssClass = "sidebar-item";
            btnCajonPreguntas.CssClass = "sidebar-item";
            btnFlotaVehiculos.CssClass = "sidebar-item";
        }

        private void MostrarPanelVisualizarPedidos()
        {
            pnlSolicitarPedido.Visible = false;
            pnlVisualizarPedidos.Visible = true;
            pnlCajonPreguntas.Visible = false;
            pnlFlotaVehiculos.Visible = false;
            pnlNotificaciones.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item active";
            btnNotificaciones.CssClass = "sidebar-item";
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
            pnlNotificaciones.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnNotificaciones.CssClass = "sidebar-item";
            btnCajonPreguntas.CssClass = "sidebar-item active";
            btnFlotaVehiculos.CssClass = "sidebar-item";

            CargarTickets();
        }

        private void MostrarPanelFlotaVehiculos()
        {
            pnlSolicitarPedido.Visible = false;
            pnlVisualizarPedidos.Visible = false;
            pnlCajonPreguntas.Visible = false;
            pnlFlotaVehiculos.Visible = true;
            pnlNotificaciones.Visible = false;

            btnSolicitarPedido.CssClass = "sidebar-item";
            btnVisualizarPedidos.CssClass = "sidebar-item";
            btnNotificaciones.CssClass = "sidebar-item";
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
                    MostrarError("Por favor complete los campos obligatorios: Origen, Destino y Fecha de Salida");
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
                    MostrarError("❌ " + resultado);
                }
            }
            catch (Exception ex)
            {
                MostrarError("❌ Error al solicitar el viaje: " + ex.Message);
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
                    MostrarErrorConsulta("Por favor complete todos los campos de la consulta");
                    return;
                }

                MostrarExitoConsulta("Tu consulta ha sido enviada exitosamente. Te contactaremos pronto.");

                ddlTipoConsulta.SelectedIndex = 0;
                txtAsunto.Text = "";
                txtMensajeConsulta.Text = "";
            }
            catch (Exception ex)
            {
                MostrarErrorConsulta("Error al enviar la consulta: " + ex.Message);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);
                DataTable dt = viajeL.MtObtenerViajesCliente(idCliente);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var rows = dt.AsEnumerable();

                    if (!string.IsNullOrEmpty(ddlFiltroEstado.SelectedValue))
                    {
                        rows = rows.Where(row => row["estadoViaje"].ToString() == ddlFiltroEstado.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(txtFiltroDestino.Text))
                    {
                        rows = rows.Where(row => row["destino"].ToString().ToLower()
                            .Contains(txtFiltroDestino.Text.ToLower()));
                    }

                    if (!string.IsNullOrEmpty(txtFiltroDesde.Text))
                    {
                        DateTime fechaDesde = Convert.ToDateTime(txtFiltroDesde.Text);
                        rows = rows.Where(row => Convert.ToDateTime(row["fechaInicio"]) >= fechaDesde);
                    }

                    if (!string.IsNullOrEmpty(txtFiltroHasta.Text))
                    {
                        DateTime fechaHasta = Convert.ToDateTime(txtFiltroHasta.Text);
                        rows = rows.Where(row => Convert.ToDateTime(row["fechaInicio"]) <= fechaHasta);
                    }

                    if (rows.Any())
                    {
                        DataTable dtFiltrado = rows.CopyToDataTable();
                        gvHistorial.DataSource = dtFiltrado;
                        gvHistorial.DataBind();
                    }
                    else
                    {
                        gvHistorial.DataSource = null;
                        gvHistorial.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al aplicar filtros: " + ex.Message);
            }
        }

        protected void gvHistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "verDetalles")
            {
                try
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    int idViaje = Convert.ToInt32(gvHistorial.DataKeys[rowIndex].Value);

                    int idCliente = Convert.ToInt32(Session["idCliente"]);
                    DataTable dt = viajeL.MtObtenerViajesCliente(idCliente);

                    if (dt != null)
                    {
                        DataRow[] rows = dt.Select($"idViaje = {idViaje}");

                        if (rows.Length > 0)
                        {
                            DataRow row = rows[0];

                            string fechaInicio = row["fechaInicio"] != DBNull.Value ?
                                Convert.ToDateTime(row["fechaInicio"]).ToString("dd/MM/yyyy HH:mm") : "No especificada";

                            string fechaFin = row["fechaFin"] != DBNull.Value ?
                                Convert.ToDateTime(row["fechaFin"]).ToString("dd/MM/yyyy HH:mm") : "No especificada";

                            string costo = row["costo"] != DBNull.Value ?
                                string.Format("${0:#,##0.00}", Convert.ToDecimal(row["costo"])) : "$0.00";

                            string html = $@"
                            <div class='detalles-viaje'>
                                <div class='row mb-3'>
                                    <div class='col-md-6'>
                                        <h6><strong>ID Viaje:</strong></h6>
                                        <p>{row["idViaje"]}</p>
                                    </div>
                                    <div class='col-md-6'>
                                        <h6><strong>Estado:</strong></h6>
                                        <span class='badge bg-{GetEstadoBadgeClass(row["estadoViaje"].ToString())} p-2'>
                                            {row["estadoViaje"]}
                                        </span>
                                    </div>
                                </div>
                                
                                <div class='row mb-3'>
                                    <div class='col-md-6'>
                                        <h6><strong>Origen:</strong></h6>
                                        <p>{row["puntoPartida"]}</p>
                                    </div>
                                    <div class='col-md-6'>
                                        <h6><strong>Destino:</strong></h6>
                                        <p>{row["destino"]}</p>
                                    </div>
                                </div>
                                
                                <div class='row mb-3'>
                                    <div class='col-md-6'>
                                        <h6><strong>Fecha Salida:</strong></h6>
                                        <p>{fechaInicio}</p>
                                    </div>
                                    <div class='col-md-6'>
                                        <h6><strong>Fecha Llegada:</strong></h6>
                                        <p>{fechaFin}</p>
                                    </div>
                                </div>
                                
                                <div class='row mb-3'>
                                    <div class='col-md-6'>
                                        <h6><strong>Tipo de Carga:</strong></h6>
                                        <p>{row["tipoCarga"]}</p>
                                    </div>
                                    <div class='col-md-6'>
                                        <h6><strong>Costo:</strong></h6>
                                        <p>{costo}</p>
                                    </div>
                                </div>
                                
                                <div class='row mb-3'>
                                    <div class='col-12'>
                                        <h6><strong>Motivo:</strong></h6>
                                        <p>{row["motivo"]}</p>
                                    </div>
                                </div>
                                
                                <div class='row'>
                                    <div class='col-12'>
                                        <h6><strong>Observaciones:</strong></h6>
                                        <p>{(string.IsNullOrEmpty(row["observaciones"].ToString()) ? "Sin observaciones" : row["observaciones"])}</p>
                                    </div>
                                </div>
                            </div>";

                            litDetalles.Text = html;

                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "MostrarModalDetalles",
                                "mostrarModalDetalles();",
                                true
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MostrarError("Error al cargar los detalles: " + ex.Message);
                }
            }
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Origen");
                dt.Columns.Add("Destino");
                dt.Columns.Add("Fecha Salida");
                dt.Columns.Add("Estado");
                dt.Columns.Add("Costo");

                foreach (GridViewRow row in gvHistorial.Rows)
                {
                    dt.Rows.Add(
                        row.Cells[0].Text,
                        row.Cells[1].Text,
                        row.Cells[2].Text,
                        row.Cells[3].Text,
                        row.Cells[4].Text,
                        row.Cells[5].Text
                    );
                }

                using (var package = new OfficeOpenXml.ExcelPackage())
                {
                    var ws = package.Workbook.Worksheets.Add("Historial");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    ws.Cells.AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=HistorialViajes.xlsx");
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al exportar a Excel: " + ex.Message);
            }
        }

        protected void btnEnviarTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAsuntoTicket.Text.Trim()) ||
                    string.IsNullOrEmpty(txtMensajeTicket.Text.Trim()))
                {
                    MostrarErrorTicket("Por favor complete el asunto y mensaje del ticket");
                    return;
                }

                int idCliente = Convert.ToInt32(Session["idCliente"]);
                string asunto = txtAsuntoTicket.Text.Trim();
                string mensaje = txtMensajeTicket.Text.Trim();
                string prioridad = ddlPrioridadTicket.SelectedValue;
                string categoria = ddlCategoriaTicket.SelectedValue;

                string query = @"insert into SoporteTicket (idCliente, asunto, mensaje, prioridad, categoria, estado, fechaCreacion) VALUES (@idCliente, @asunto, @mensaje, @prioridad, @categoria, 'Abierto', GETDATE())";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@asunto", asunto);
                cmd.Parameters.AddWithValue("@mensaje", mensaje);
                cmd.Parameters.AddWithValue("@prioridad", prioridad);
                cmd.Parameters.AddWithValue("@categoria", categoria);
                cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();

                MostrarExitoTicket("Ticket creado exitosamente. Te contactaremos pronto.");
                txtAsuntoTicket.Text = "";
                txtMensajeTicket.Text = "";

                CargarTickets();
            }
            catch (Exception ex)
            {
                MostrarErrorTicket("❌ Error al crear el ticket: " + ex.Message);
            }
        }

        private void CargarTickets()
        {
            try
            {
                if (Session["idCliente"] == null) return;

                int idCliente = Convert.ToInt32(Session["idCliente"]);
                string estadoFiltro = ddlFiltroEstadoTicket.SelectedValue;

                string query = @"select idTicket, asunto, mensaje, prioridad, categoria, estado, CONVERT(varchar, fechaCreacion, 120) as fechaCreacion, respuesta, fechaRespuesta from SoporteTicket where idCliente = @idCliente";

                if (!string.IsNullOrEmpty(estadoFiltro))
                {
                    query += " and estado = @estado";
                }

                query += " ORDER BY fechaCreacion DESC";

                DataTable dt = new DataTable();
                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                if (!string.IsNullOrEmpty(estadoFiltro))
                {
                    cmd.Parameters.AddWithValue("@estado", estadoFiltro);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conexion.MtCerrarConexion();

                gvTickets.DataSource = dt;
                gvTickets.DataBind();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar tickets: " + ex.Message);
            }
        }

        protected void ddlFiltroEstadoTicket_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTickets();
        }

        protected void btnRefrescarTickets_Click(object sender, EventArgs e)
        {
            CargarTickets();
        }

        protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "verTicket")
            {
                try
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    int idTicket = Convert.ToInt32(gvTickets.DataKeys[rowIndex].Value);

                    string query = @"select * from SoporteTicket where idTicket = @idTicket";

                    ClConexion conexion = new ClConexion();
                    SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                    cmd.Parameters.AddWithValue("@idTicket", idTicket);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string html = $@"
                                <div class='detalles-ticket'>
                                    <div class='row mb-3'>
                                        <div class='col-md-6'>
                                            <h6><strong>ID Ticket:</strong></h6>
                                            <p>{reader["idTicket"]}</p>
                                        </div>
                                        <div class='col-md-6'>
                                            <h6><strong>Estado:</strong></h6>
                                            <span class='badge-estado-ticket badge-{reader["estado"].ToString().ToLower().Replace(" ", "")}'>
                                                {reader["estado"]}
                                            </span>
                                        </div>
                                    </div>
                                    
                                    <div class='row mb-3'>
                                        <div class='col-md-6'>
                                            <h6><strong>Categoría:</strong></h6>
                                            <p>{reader["categoria"]}</p>
                                        </div>
                                        <div class='col-md-6'>
                                            <h6><strong>Prioridad:</strong></h6>
                                            <span class='badge-prioridad {reader["prioridad"].ToString().ToLower()}'>
                                                {reader["prioridad"]}
                                            </span>
                                        </div>
                                    </div>
                                    
                                    <div class='row mb-3'>
                                        <div class='col-md-6'>
                                            <h6><strong>Fecha Creación:</strong></h6>
                                            <p>{Convert.ToDateTime(reader["fechaCreacion"]).ToString("dd/MM/yyyy HH:mm")}</p>
                                        </div>
                                        <div class='col-md-6'>
                                            <h6><strong>Última Actualización:</strong></h6>
                                            <p>{(reader["fechaRespuesta"] != DBNull.Value ?
                                                Convert.ToDateTime(reader["fechaRespuesta"]).ToString("dd/MM/yyyy HH:mm") :
                                                "Sin actualizaciones")}</p>
                                        </div>
                                    </div>
                                    
                                    <div class='row mb-3'>
                                        <div class='col-12'>
                                            <h6><strong>Asunto:</strong></h6>
                                            <p class='fw-bold'>{reader["asunto"]}</p>
                                        </div>
                                    </div>
                                    
                                    <div class='row mb-3'>
                                        <div class='col-12'>
                                            <h6><strong>Mensaje:</strong></h6>
                                            <p style='background-color: #f8f9fa; padding: 15px; border-radius: 5px;'>
                                                {reader["mensaje"]}
                                            </p>
                                        </div>
                                    </div>";

                            if (reader["respuesta"] != DBNull.Value && !string.IsNullOrEmpty(reader["respuesta"].ToString()))
                            {
                                html += $@"
                                    <div class='row'>
                                        <div class='col-12'>
                                            <h6 class='text-success'><strong>Respuesta del Soporte:</strong></h6>
                                            <div class='respuesta-soporte'>
                                                <p>{reader["respuesta"]}</p>
                                                <small class='text-muted'>
                                                    <i class='bi bi-clock'></i> 
                                                    Respondido el {Convert.ToDateTime(reader["fechaRespuesta"]).ToString("dd/MM/yyyy HH:mm")}
                                                </small>
                                            </div>
                                        </div>
                                    </div>";
                            }
                            else
                            {
                                html += $@"
                                    <div class='row'>
                                        <div class='col-12'>
                                            <div class='alert alert-info'>
                                                <i class='bi bi-info-circle me-2'></i>
                                                Tu ticket está siendo revisado por nuestro equipo de soporte.
                                            </div>
                                        </div>
                                    </div>";
                            }

                            html += "</div>";

                            litDetallesTicket.Text = html;

                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "MostrarModalTicket",
                                "mostrarModalTicket();",
                                true
                            );
                        }
                    }
                    conexion.MtCerrarConexion();
                }
                catch (Exception ex)
                {
                    MostrarError("Error al cargar los detalles del ticket: " + ex.Message);
                }
            }
        }

        private string GetEstadoBadgeClass(string estado)
        {
            switch (estado.ToLower())
            {
                case "aprobado": return "success";
                case "pendiente": return "warning";
                case "en curso": return "info";
                case "completado": return "primary";
                case "cancelado": return "danger";
                default: return "secondary";
            }
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Style["color"] = "#dc3545";
            lblMensaje.Style["background-color"] = "#f8d7da";
            lblMensaje.Style["border-color"] = "#f5c6cb";
            lblMensaje.Visible = true;
        }

        private void MostrarExito(string mensaje)
        {
            lblMensaje.Text = "✅ " + mensaje;
            lblMensaje.Style["color"] = "#198754";
            lblMensaje.Style["background-color"] = "#d4edda";
            lblMensaje.Style["border-color"] = "#c3e6cb";
            lblMensaje.Visible = true;
        }

        private void MostrarErrorConsulta(string mensaje)
        {
            lblMensajeConsultaResult.Text = mensaje;
            lblMensajeConsultaResult.Style["color"] = "#dc3545";
            lblMensajeConsultaResult.Style["background-color"] = "#f8d7da";
            lblMensajeConsultaResult.Style["border-color"] = "#f5c6cb";
            lblMensajeConsultaResult.Visible = true;
        }

        private void MostrarExitoConsulta(string mensaje)
        {
            lblMensajeConsultaResult.Text = mensaje;
            lblMensajeConsultaResult.Style["color"] = "#198754";
            lblMensajeConsultaResult.Style["background-color"] = "#d4edda";
            lblMensajeConsultaResult.Style["border-color"] = "#c3e6cb";
            lblMensajeConsultaResult.Visible = true;
        }

        private void MostrarErrorTicket(string mensaje)
        {
            lblMensajeTicket.Text = mensaje;
            lblMensajeTicket.Style["color"] = "#dc3545";
            lblMensajeTicket.Style["background-color"] = "#f8d7da";
            lblMensajeTicket.Style["border-color"] = "#f5c6cb";
            lblMensajeTicket.Visible = true;
        }

        private void MostrarExitoTicket(string mensaje)
        {
            lblMensajeTicket.Text = mensaje;
            lblMensajeTicket.Style["color"] = "#198754";
            lblMensajeTicket.Style["background-color"] = "#d4edda";
            lblMensajeTicket.Style["border-color"] = "#c3e6cb";
            lblMensajeTicket.Visible = true;
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