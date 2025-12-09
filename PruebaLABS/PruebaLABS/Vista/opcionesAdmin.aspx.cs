using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesAdmin : Page
    {
        ClVehiculoD oVehiculoD = new ClVehiculoD();
        ClUsuarioL logicaUsuario = new ClUsuarioL();
        ClSolicitudViajeL logicaSolicitud = new ClSolicitudViajeL();
        ClViajeL viajesL = new ClViajeL();

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

            pnlReportesViajes.Visible = true;

            MtCargarReporteViajes();          
            MtCargarGridEdicionViajes();     
            MtCargarConductoresCrear();
            MtCargarVehiculosCrear();
            LimpiarInfoAsignacionCrear();
        }

        protected void btnReportesViajes_Click(object sender, EventArgs e)
        {
            pnlReportesViajes.Visible = true;
            MtCargarReporteViajes();
            MtCargarGridEdicionViajes();

            btnReportesViajes.CssClass = "nav-reporte-item active";

            MtCargarConductoresCrear();
            MtCargarVehiculosCrear();
            LimpiarInfoAsignacionCrear();
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
                if (item != null) item.Selected = true;

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

        private void MtCargarUsuarios()
        {
            gvUsuarios.DataSource = logicaUsuario.MtListarUsuarios();
            gvUsuarios.DataBind();
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
                    lblMensajeRegistro.Text = "❌" + resultado;
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

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                string documento = txtBuscarDocumento.Text.Trim();
                string estado = ddlFiltrarEstado.SelectedValue;
                string fechaDesde = txtFechaDesde.Text;
                string fechaHasta = txtFechaHasta.Text;

                if (string.IsNullOrEmpty(documento) && string.IsNullOrEmpty(estado) &&
                    string.IsNullOrEmpty(fechaDesde) && string.IsNullOrEmpty(fechaHasta))
                {
                    CargarTodasLasSolicitudes();
                    return;
                }

                DataTable dtSolicitudes = logicaSolicitud.MtObtenerTodasLasSolicitudes();
                DataTable dtFiltrado = FiltrarSolicitudes(dtSolicitudes, documento, estado, fechaDesde, fechaHasta);

                Session["SolicitudesData"] = dtFiltrado;
                gvSolicitudesClientes.DataSource = dtFiltrado;
                gvSolicitudesClientes.DataBind();

                lblTotalRegistros.Text = dtFiltrado.Rows.Count.ToString();

                if (dtFiltrado.Rows.Count > 0)
                {
                    string mensaje = "Se encontraron " + dtFiltrado.Rows.Count + " solicitudes";
                    if (!string.IsNullOrEmpty(documento)) mensaje += " para documento: " + documento;
                    if (!string.IsNullOrEmpty(estado)) mensaje += ", estado: " + estado;

                    lblMensajeSolicitudes.Text = mensaje;
                    lblMensajeSolicitudes.Style["color"] = "#198754";
                }
                else
                {
                    lblMensajeSolicitudes.Text = "No se encontraron solicitudes con los filtros aplicados";
                    lblMensajeSolicitudes.Style["color"] = "#6c757d";
                }
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al buscar: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
            }
        }

        private DataTable FiltrarSolicitudes(DataTable dtOriginal, string documento, string estado, string fechaDesde, string fechaHasta)
        {
            DataTable dtFiltrado = dtOriginal.Clone();

            foreach (DataRow row in dtOriginal.Rows)
            {
                bool cumpleFiltros = true;

                if (!string.IsNullOrEmpty(documento))
                {
                    string docCliente = row["documento"].ToString().ToLower();
                    if (!docCliente.Contains(documento.ToLower())) cumpleFiltros = false;
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    string estadoViaje = row["estadoViaje"].ToString();
                    if (estadoViaje != estado) cumpleFiltros = false;
                }

                if (!string.IsNullOrEmpty(fechaDesde))
                {
                    DateTime fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                    DateTime fechaDesdeFiltro = Convert.ToDateTime(fechaDesde);
                    if (fechaInicio < fechaDesdeFiltro) cumpleFiltros = false;
                }

                if (!string.IsNullOrEmpty(fechaHasta))
                {
                    DateTime fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                    DateTime fechaHastaFiltro = Convert.ToDateTime(fechaHasta);
                    if (fechaInicio > fechaHastaFiltro) cumpleFiltros = false;
                }

                if (cumpleFiltros) dtFiltrado.ImportRow(row);
            }

            return dtFiltrado;
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscarDocumento.Text = "";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            ddlFiltrarEstado.SelectedIndex = 0;

            CargarTodasLasSolicitudes();

            lblMensajeSolicitudes.Text = "Mostrando todas las solicitudes";
            lblMensajeSolicitudes.Style["color"] = "#198754";
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
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al cargar las solicitudes: " + ex.Message;
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

                int idCliente = ObtenerIdClientePorViaje(idViaje);

                string mensajeNotificacion;
                if (estado == "Cancelado") mensajeNotificacion = $" Tu viaje #{idViaje} ha sido CANCELADO. Contacta al soporte para más información.";
                else if (estado == "Completado") mensajeNotificacion = $" Tu viaje #{idViaje} ha sido COMPLETADO! Gracias por confiar en nosotros.";
                else if (estado == "En curso") mensajeNotificacion = $" Tu viaje #{idViaje} está EN CURSO. El vehículo está en camino.";
                else if (estado == "Aprobado") mensajeNotificacion = $" Tu viaje #{idViaje} ha sido APROBADO. Pronto te contactaremos con los detalles.";
                else mensajeNotificacion = $" Tu viaje #{idViaje} ha cambiado de estado a: {estado}.";

                if (!string.IsNullOrEmpty(costo) && costo != "0" && costo != "$0.00")
                    mensajeNotificacion += $" Costo estimado: {costo}.";

                ClNotificacionL logicaNotificacion = new ClNotificacionL();
                string resultadoNotificacion = logicaNotificacion.MtCrearNotificacion(idCliente, mensajeNotificacion);

                gvSolicitudesClientes.EditIndex = -1;
                CargarDatosGridView();

                lblMensajeSolicitudes.Text = resultado + " | " + resultadoNotificacion;
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
                if (gvSolicitudesClientes.DataKeys.Count > e.RowIndex)
                {
                    int idViaje = Convert.ToInt32(gvSolicitudesClientes.DataKeys[e.RowIndex].Value);
                    string resultado = logicaSolicitud.MtEliminarSolicitud(idViaje);

                    RecargarSolicitudes();

                    lblMensajeSolicitudes.Text = resultado;
                    lblMensajeSolicitudes.Style["color"] = resultado.Contains("correctamente") ? "#198754" : "#dc3545";
                }
                else
                {
                    lblMensajeSolicitudes.Text = "Error: No se pudo obtener el ID de la solicitud.";
                    lblMensajeSolicitudes.Style["color"] = "#dc3545";
                }
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
                lblTotalRegistros.Text = dt.Rows.Count.ToString();
            }
            else
            {
                CargarTodasLasSolicitudes();
            }
        }

        private void RecargarSolicitudes()
        {
            try
            {
                string documento = txtBuscarDocumento.Text.Trim();
                string estado = ddlFiltrarEstado.SelectedValue;
                string fechaDesde = txtFechaDesde.Text;
                string fechaHasta = txtFechaHasta.Text;

                if (string.IsNullOrEmpty(documento) && string.IsNullOrEmpty(estado) &&
                    string.IsNullOrEmpty(fechaDesde) && string.IsNullOrEmpty(fechaHasta))
                {
                    CargarTodasLasSolicitudes();
                }
                else
                {
                    DataTable dtSolicitudes = logicaSolicitud.MtObtenerTodasLasSolicitudes();
                    DataTable dtFiltrado = FiltrarSolicitudes(dtSolicitudes, documento, estado, fechaDesde, fechaHasta);
                    Session["SolicitudesData"] = dtFiltrado;

                    gvSolicitudesClientes.DataSource = dtFiltrado;
                    gvSolicitudesClientes.DataBind();
                    lblTotalRegistros.Text = dtFiltrado.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMensajeSolicitudes.Text = "Error al recargar datos: " + ex.Message;
                lblMensajeSolicitudes.Style["color"] = "#dc3545";
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

        public string GetEstadoIcon(string estado)
        {
            switch ((estado ?? "").ToLower())
            {
                case "pendiente": return "bi bi-clock";
                case "en curso":
                case "en progreso": return "bi bi-arrow-right-circle";
                case "completado":
                case "finalizado": return "bi bi-check-circle";
                case "cancelado": return "bi bi-x-circle";
                case "aprobado": return "bi bi-check-lg";
                default: return "bi bi-question-circle";
            }
        }

        private int ObtenerIdClientePorViaje(int idViaje)
        {
            string query = "select idCliente from viaje where idViaje = @idViaje";

            ClConexion conexion = new ClConexion();
            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idViaje", idViaje);
            object result = cmd.ExecuteScalar();
            conexion.MtCerrarConexion();

            return result != null ? Convert.ToInt32(result) : 0;
        }

        private void MtCargarReporteViajes()
        {
            try
            {
                List<ClViajesAdminM> listaViajes = viajesL.MtViajesAdmin();

                if (listaViajes == null || listaViajes.Count == 0)
                {
                    if (lblMensajeReportesViajes != null)
                    {
                        lblMensajeReportesViajes.Text = "No se encontraron viajes registrados.";
                        lblMensajeReportesViajes.Style["color"] = "#6c757d";
                    }
                    gvReportesViajes.DataSource = null;
                    gvReportesViajes.DataBind();
                    return;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("idViaje", typeof(int));
                dt.Columns.Add("puntoPartida", typeof(string));
                dt.Columns.Add("destino", typeof(string));
                dt.Columns.Add("fechaInicio", typeof(string));
                dt.Columns.Add("fechaFin", typeof(string));
                dt.Columns.Add("estadoViaje", typeof(string));
                dt.Columns.Add("costo", typeof(string));
                dt.Columns.Add("distancia", typeof(string));
                dt.Columns.Add("tipoCarga", typeof(string));
                dt.Columns.Add("observaciones", typeof(string));
                dt.Columns.Add("Conductor", typeof(string));
                dt.Columns.Add("telefono", typeof(string));
                dt.Columns.Add("placa", typeof(string));
                dt.Columns.Add("modelo", typeof(string));
                dt.Columns.Add("capacidad", typeof(string));
                dt.Columns.Add("cliente", typeof(string));
                dt.Columns.Add("empresa", typeof(string));

                foreach (var viaje in listaViajes)
                {
                    dt.Rows.Add(
                        viaje.idViaje,
                        viaje.puntoPartida ?? "",
                        viaje.destino ?? "",
                        viaje.fechaInicio ?? "",
                        viaje.fechaFin ?? "",
                        viaje.estadoViaje ?? "Pendiente",
                        viaje.costo ?? "0",
                        viaje.distancia ?? "0",
                        viaje.tipoCarga ?? "General",
                        viaje.observaciones ?? "",
                        viaje.nombreU ?? "Sin asignar",
                        viaje.telefonoU ?? "",
                        viaje.placa ?? "Sin asignar",
                        viaje.modelo ?? "",
                        viaje.capacidad ?? "",
                        viaje.nombreC ?? "",
                        viaje.empresa ?? ""
                    );
                }

                gvReportesViajes.DataSource = dt;
                gvReportesViajes.DataBind();

                if (lblMensajeReportesViajes != null)
                {
                    lblMensajeReportesViajes.Text = $"Se cargaron {listaViajes.Count} viajes.";
                    lblMensajeReportesViajes.Style["color"] = "#198754";
                }
            }
            catch (Exception ex)
            {
                if (lblMensajeReportesViajes != null)
                {
                    lblMensajeReportesViajes.Text = $"Error al cargar viajes: {ex.Message}";
                    lblMensajeReportesViajes.Style["color"] = "#dc3545";
                }
                gvReportesViajes.DataSource = null;
                gvReportesViajes.DataBind();
            }
        }

        private void MtCargarGridEdicionViajes()
        {
            try
            {
                List<ClViajesAdminM> listaViajes = viajesL.MtViajesAdmin();
                DataTable dt = new DataTable();

                dt.Columns.Add("idViaje", typeof(int));
                dt.Columns.Add("origen", typeof(string));
                dt.Columns.Add("destino", typeof(string));
                dt.Columns.Add("distancia", typeof(string));
                dt.Columns.Add("costo", typeof(string));
                dt.Columns.Add("tipoCarga", typeof(string));
                dt.Columns.Add("motivo", typeof(string));
                dt.Columns.Add("observaciones", typeof(string));

                dt.Columns.Add("idConductorCargo", typeof(int)); 
                dt.Columns.Add("conductorNombre", typeof(string));
                dt.Columns.Add("idVehiculo", typeof(int));
                dt.Columns.Add("vehiculoTexto", typeof(string));
                dt.Columns.Add("anticipo", typeof(string));

                foreach (var v in listaViajes)
                {
                    int idViaje = v.idViaje;

                    var extra = ObtenerInfoEditableViaje(idViaje);

                    dt.Rows.Add(
                        idViaje,
                        extra.Origen,
                        extra.Destino,
                        extra.Distancia,
                        extra.Costo,
                        extra.TipoCarga,
                        extra.Motivo,
                        extra.Observaciones,
                        extra.IdConductorCargo,
                        extra.ConductorNombre,
                        extra.IdVehiculo,
                        extra.VehiculoTexto,
                        extra.Anticipo
                    );
                }

                gvViajesAdminEditar.DataSource = dt;
                gvViajesAdminEditar.DataBind();
            }
            catch
            {
                gvViajesAdminEditar.DataSource = null;
                gvViajesAdminEditar.DataBind();
            }
        }

        private class ViajeEditableInfo
        {
            public string Origen = "";
            public string Destino = "";
            public string Distancia = "0";
            public string Costo = "0";
            public string TipoCarga = "General";
            public string Motivo = "";
            public string Observaciones = "";
            public int IdConductorCargo = 0;
            public string ConductorNombre = "";
            public int IdVehiculo = 0;
            public string VehiculoTexto = "";
            public string Anticipo = "";
        }

        private ViajeEditableInfo ObtenerInfoEditableViaje(int idViaje)
        {
            var info = new ViajeEditableInfo();

            string sql = @"
SELECT
    v.puntoPartida, v.destino, v.distancia, v.costo, v.tipoCarga, v.motivo, v.observaciones,
    vv.idConductor AS idCargo,
    vv.idVehiculo,
    vv.anticipo,
    (u.nombre + ' ' + u.apellido) AS Conductor,
    (ve.placa + ' - ' + ve.modelo) AS Vehiculo
FROM viaje v
LEFT JOIN viajeVehiculo vv ON v.idViaje = vv.idViaje
LEFT JOIN cargo c ON vv.idConductor = c.idCargo
LEFT JOIN usuario u ON c.idUsuario = u.idUsuario
LEFT JOIN vehiculo ve ON vv.idVehiculo = ve.idVehiculo
WHERE v.idViaje = @idViaje";

            ClConexion cx = new ClConexion();
            SqlCommand cmd = new SqlCommand(sql, cx.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idViaje", idViaje);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                info.Origen = dr["puntoPartida"] != DBNull.Value ? dr["puntoPartida"].ToString() : "";
                info.Destino = dr["destino"] != DBNull.Value ? dr["destino"].ToString() : "";
                info.Distancia = dr["distancia"] != DBNull.Value ? dr["distancia"].ToString() : "0";
                info.Costo = dr["costo"] != DBNull.Value ? dr["costo"].ToString() : "0";
                info.TipoCarga = dr["tipoCarga"] != DBNull.Value ? dr["tipoCarga"].ToString() : "General";
                info.Motivo = dr["motivo"] != DBNull.Value ? dr["motivo"].ToString() : "";
                info.Observaciones = dr["observaciones"] != DBNull.Value ? dr["observaciones"].ToString() : "";

                info.IdConductorCargo = dr["idCargo"] != DBNull.Value ? Convert.ToInt32(dr["idCargo"]) : 0;
                info.IdVehiculo = dr["idVehiculo"] != DBNull.Value ? Convert.ToInt32(dr["idVehiculo"]) : 0;
                info.Anticipo = dr["anticipo"] != DBNull.Value ? dr["anticipo"].ToString() : "";

                info.ConductorNombre = dr["Conductor"] != DBNull.Value ? dr["Conductor"].ToString() : "";
                info.VehiculoTexto = dr["Vehiculo"] != DBNull.Value ? dr["Vehiculo"].ToString() : "";
            }
            dr.Close();
            cx.MtCerrarConexion();

            return info;
        }

        protected void btnCrearViaje_Click(object sender, EventArgs e)
        {
            try
            {
                string origen = (txtOrigenCrear.Text ?? "").Trim();
                string destino = (txtDestinoCrear.Text ?? "").Trim();
                string distancia = (txtDistanciaCrear.Text ?? "").Trim();
                string costo = (txtCostoCrear.Text ?? "").Trim();
                string tipoCarga = (txtTipoCargaCrear.Text ?? "").Trim();
                string motivo = (txtMotivoCrear.Text ?? "").Trim(); 
                string observaciones = (txtObservacionesCrear.Text ?? "").Trim();
                string idClienteStr = (txtIdClienteCrear.Text ?? "").Trim();

                string idConductorCargoStr = (ddlConductorCrear.SelectedValue ?? "").Trim(); 
                string idVehiculoStr = (ddlVehiculoCrear.SelectedValue ?? "").Trim();
                string anticipoStr = (txtAnticipoCrear.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(origen) || string.IsNullOrWhiteSpace(destino) || string.IsNullOrWhiteSpace(idClienteStr))
                {
                    lblMensajeCrearViaje.Text = "❌ Completa Origen, Destino y Cliente (ID).";
                    lblMensajeCrearViaje.Style["color"] = "#dc3545";
                    return;
                }

                if (!int.TryParse(idClienteStr, out int idCliente) || idCliente <= 0)
                {
                    lblMensajeCrearViaje.Text = "❌ Cliente (ID) inválido.";
                    lblMensajeCrearViaje.Style["color"] = "#dc3545";
                    return;
                }

                string rCrear = viajesL.MtCrearViajeSinSP(idCliente, origen, destino, distancia, costo, tipoCarga, motivo, observaciones);

                lblMensajeCrearViaje.Text = rCrear;
                lblMensajeCrearViaje.Style["color"] = (rCrear ?? "").ToLower().Contains("❌") || (rCrear ?? "").ToLower().Contains("error")
                    ? "#dc3545"
                    : "#198754";

                int idViajeNuevo = ExtraerIdDesdeMensaje(rCrear);

                if (idViajeNuevo > 0 &&
                    int.TryParse(idConductorCargoStr, out int idCargo) && idCargo > 0 &&
                    int.TryParse(idVehiculoStr, out int idVehiculo) && idVehiculo > 0)
                {
                    string rAsig = viajesL.MtEditarAsignacionViaje(idViajeNuevo, idCargo, idVehiculo, anticipoStr);

                    lblMensajeCrearViaje.Text = lblMensajeCrearViaje.Text + " | " + rAsig;
                    if ((rAsig ?? "").ToLower().Contains("❌") || (rAsig ?? "").ToLower().Contains("error"))
                        lblMensajeCrearViaje.Style["color"] = "#dc3545";
                }

                MtCargarReporteViajes();
                MtCargarGridEdicionViajes();
                MtCargarConductoresCrear();
                MtCargarVehiculosCrear();
                LimpiarInfoAsignacionCrear();

                txtOrigenCrear.Text = "";
                txtDestinoCrear.Text = "";
                txtDistanciaCrear.Text = "";
                txtCostoCrear.Text = "";
                txtTipoCargaCrear.Text = "";
                txtObservacionesCrear.Text = "";
                txtIdClienteCrear.Text = "";
                txtAnticipoCrear.Text = "";
            }
            catch (Exception ex)
            {
                lblMensajeCrearViaje.Text = "❌ Error al crear viaje: " + ex.Message;
                lblMensajeCrearViaje.Style["color"] = "#dc3545";
            }
        }

        private int ExtraerIdDesdeMensaje(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return 0;
            int idx = msg.LastIndexOf("ID:");
            if (idx < 0) return 0;

            string parte = msg.Substring(idx + 3).Trim();
            if (int.TryParse(parte, out int id)) return id;

            parte = parte.Replace(".", "").Trim();
            if (int.TryParse(parte, out id)) return id;

            return 0;
        }

        private void MtCargarConductoresCrear()
        {
            try
            {
                DataTable dt = viajesL.MtConductores(); 
                ddlConductorCrear.Items.Clear();
                ddlConductorCrear.Items.Add(new ListItem("Seleccione un conductor", ""));

                if (dt == null) return;

                foreach (DataRow row in dt.Rows)
                {
                    string idCargo = row.Table.Columns.Contains("idCargo") ? row["idCargo"].ToString() : "";
                    string nombre = row.Table.Columns.Contains("Conductor") ? row["Conductor"].ToString() : "Conductor";
                    string tel = row.Table.Columns.Contains("telefono") ? row["telefono"].ToString() : "";

                    string texto = nombre;
                    if (!string.IsNullOrEmpty(tel)) texto += " - " + tel;

                    if (!string.IsNullOrEmpty(idCargo))
                        ddlConductorCrear.Items.Add(new ListItem(texto, idCargo));
                }
            }
            catch
            {
                ddlConductorCrear.Items.Clear();
                ddlConductorCrear.Items.Add(new ListItem("Seleccione un conductor", ""));
            }
        }

        private void MtCargarVehiculosCrear()
        {
            try
            {
                DataTable dt = viajesL.MtVehiculosDisponibles(); 
                ddlVehiculoCrear.Items.Clear();
                ddlVehiculoCrear.Items.Add(new ListItem("Seleccione un vehículo", ""));

                if (dt == null) return;

                foreach (DataRow row in dt.Rows)
                {
                    string idVeh = row.Table.Columns.Contains("idVehiculo") ? row["idVehiculo"].ToString() : "";
                    string vehiculoTexto = row.Table.Columns.Contains("vehiculo") ? row["vehiculo"].ToString() : "";
                    string modelo = row.Table.Columns.Contains("modelo") ? row["modelo"].ToString() : "";
                    string capacidad = row.Table.Columns.Contains("capacidad") ? row["capacidad"].ToString() : "";

                    string texto = !string.IsNullOrEmpty(vehiculoTexto) ? vehiculoTexto : ("Vehículo " + idVeh);
                    if (!string.IsNullOrEmpty(capacidad)) texto += " (" + capacidad + ")";

                    if (!string.IsNullOrEmpty(idVeh))
                        ddlVehiculoCrear.Items.Add(new ListItem(texto, idVeh));
                }
            }
            catch
            {
                ddlVehiculoCrear.Items.Clear();
                ddlVehiculoCrear.Items.Add(new ListItem("Seleccione un vehículo", ""));
            }
        }

        protected void ddlConductorCrear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblTelConductorCrear.Text = "";
                if (string.IsNullOrEmpty(ddlConductorCrear.SelectedValue)) return;

                DataTable dt = viajesL.MtConductores();
                if (dt == null) return;

                foreach (DataRow row in dt.Rows)
                {
                    string id = row.Table.Columns.Contains("idCargo") ? row["idCargo"].ToString() : "";
                    if (id == ddlConductorCrear.SelectedValue)
                    {
                        string tel = row.Table.Columns.Contains("telefono") ? row["telefono"].ToString() : "";
                        lblTelConductorCrear.Text = tel ?? "";
                        return;
                    }
                }
            }
            catch
            {
                lblTelConductorCrear.Text = "";
            }
        }

        protected void ddlVehiculoCrear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblModeloVehCrear.Text = "";
                lblCapacidadVehCrear.Text = "";

                if (string.IsNullOrEmpty(ddlVehiculoCrear.SelectedValue)) return;

                DataTable dt = viajesL.MtVehiculosDisponibles();
                if (dt == null) return;

                foreach (DataRow row in dt.Rows)
                {
                    string id = row.Table.Columns.Contains("idVehiculo") ? row["idVehiculo"].ToString() : "";
                    if (id == ddlVehiculoCrear.SelectedValue)
                    {
                        lblModeloVehCrear.Text = row.Table.Columns.Contains("modelo") ? (row["modelo"].ToString() ?? "") : "";
                        lblCapacidadVehCrear.Text = row.Table.Columns.Contains("capacidad") ? (row["capacidad"].ToString() ?? "") : "";
                        return;
                    }
                }
            }
            catch
            {
                lblModeloVehCrear.Text = "";
                lblCapacidadVehCrear.Text = "";
            }
        }

        private void LimpiarInfoAsignacionCrear()
        {
            if (lblTelConductorCrear != null) lblTelConductorCrear.Text = "";
            if (lblModeloVehCrear != null) lblModeloVehCrear.Text = "";
            if (lblCapacidadVehCrear != null) lblCapacidadVehCrear.Text = "";
            if (ddlConductorCrear != null) ddlConductorCrear.SelectedIndex = 0;
            if (ddlVehiculoCrear != null) ddlVehiculoCrear.SelectedIndex = 0;
            if (txtAnticipoCrear != null) txtAnticipoCrear.Text = "";
        }

        protected void gvViajesAdminEditar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvViajesAdminEditar.EditIndex = e.NewEditIndex;
            MtCargarGridEdicionViajes();
        }

        protected void gvViajesAdminEditar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvViajesAdminEditar.EditIndex = -1;
            MtCargarGridEdicionViajes();
        }

        protected void gvViajesAdminEditar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                // conductores
                var ddlCon = e.Row.FindControl("ddlConductorEdit") as DropDownList;
                if (ddlCon != null)
                {
                    ddlCon.Items.Clear();
                    ddlCon.Items.Add(new ListItem("Seleccione", ""));
                    DataTable dtC = viajesL.MtConductores();
                    if (dtC != null)
                    {
                        foreach (DataRow r in dtC.Rows)
                        {
                            string idCargo = r["idCargo"].ToString();
                            string nom = r["Conductor"].ToString();
                            ddlCon.Items.Add(new ListItem(nom, idCargo));
                        }
                    }

                    var hid = e.Row.FindControl("hidConductorCargo") as HiddenField;
                    if (hid != null && !string.IsNullOrEmpty(hid.Value))
                    {
                        ListItem it = ddlCon.Items.FindByValue(hid.Value);
                        if (it != null) { ddlCon.ClearSelection(); it.Selected = true; }
                    }
                }

                var ddlVeh = e.Row.FindControl("ddlVehiculoEdit") as DropDownList;
                if (ddlVeh != null)
                {
                    ddlVeh.Items.Clear();
                    ddlVeh.Items.Add(new ListItem("Seleccione", ""));
                    DataTable dtV = viajesL.MtVehiculosDisponibles();
                    if (dtV != null)
                    {
                        foreach (DataRow r in dtV.Rows)
                        {
                            string idVeh = r["idVehiculo"].ToString();
                            string texto = r["vehiculo"].ToString();
                            ddlVeh.Items.Add(new ListItem(texto, idVeh));
                        }
                    }

                    var hidV = e.Row.FindControl("hidVehiculo") as HiddenField;
                    if (hidV != null && !string.IsNullOrEmpty(hidV.Value))
                    {
                        ListItem it2 = ddlVeh.Items.FindByValue(hidV.Value);
                        if (it2 != null) { ddlVeh.ClearSelection(); it2.Selected = true; }
                    }
                }
            }
        }

        protected void gvViajesAdminEditar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idViaje = Convert.ToInt32(gvViajesAdminEditar.DataKeys[e.RowIndex].Value);
                GridViewRow row = gvViajesAdminEditar.Rows[e.RowIndex];

                string origen = ((TextBox)row.FindControl("txtOrigenEdit")).Text.Trim();
                string destino = ((TextBox)row.FindControl("txtDestinoEdit")).Text.Trim();
                string distancia = ((TextBox)row.FindControl("txtDistanciaEdit")).Text.Trim();
                string costo = ((TextBox)row.FindControl("txtCostoEdit")).Text.Trim();
                string tipoCarga = ((TextBox)row.FindControl("txtTipoCargaEdit")).Text.Trim();
                string motivo = ((TextBox)row.FindControl("txtMotivoEdit")).Text.Trim();
                string observaciones = ((TextBox)row.FindControl("txtObservacionesEdit")).Text.Trim();

                string conductorCargoStr = ((DropDownList)row.FindControl("ddlConductorEdit")).SelectedValue;
                string vehiculoStr = ((DropDownList)row.FindControl("ddlVehiculoEdit")).SelectedValue;
                string anticipo = ((TextBox)row.FindControl("txtAnticipoEdit")).Text.Trim();

                string r1 = viajesL.MtEditarViajeBase(idViaje, origen, destino, distancia, costo, tipoCarga, motivo, observaciones);

                string r2 = "";
                if (int.TryParse(conductorCargoStr, out int idCargo) && idCargo > 0 &&
                    int.TryParse(vehiculoStr, out int idVeh) && idVeh > 0)
                {
                    r2 = viajesL.MtEditarAsignacionViaje(idViaje, idCargo, idVeh, anticipo);
                }

                gvViajesAdminEditar.EditIndex = -1;
                MtCargarReporteViajes();
                MtCargarGridEdicionViajes();
                MtCargarConductoresCrear();
                MtCargarVehiculosCrear();

                if (lblMensajeReportesViajes != null)
                {
                    string msg = r1;
                    if (!string.IsNullOrEmpty(r2)) msg += " | " + r2;

                    lblMensajeReportesViajes.Text = msg;
                    lblMensajeReportesViajes.Style["color"] = (msg.ToLower().Contains("❌") || msg.ToLower().Contains("error")) ? "#dc3545" : "#198754";
                }
            }
            catch (Exception ex)
            {
                if (lblMensajeReportesViajes != null)
                {
                    lblMensajeReportesViajes.Text = "❌ Error al actualizar: " + ex.Message;
                    lblMensajeReportesViajes.Style["color"] = "#dc3545";
                }
            }
        }
    }
}
