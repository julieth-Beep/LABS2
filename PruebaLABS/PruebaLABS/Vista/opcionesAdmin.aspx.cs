using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        List<ClGastoM> listaGastosCompleta = new List<ClGastoM>();


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

                    if (!string.IsNullOrEmpty(documento))
                        mensaje += " para documento: " + documento;

                    if (!string.IsNullOrEmpty(estado))
                        mensaje += ", estado: " + estado;

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
                    if (!docCliente.Contains(documento.ToLower()))
                        cumpleFiltros = false;
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    string estadoViaje = row["estadoViaje"].ToString();
                    if (estadoViaje != estado)
                        cumpleFiltros = false;
                }

                if (!string.IsNullOrEmpty(fechaDesde))
                {
                    DateTime fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                    DateTime fechaDesdeFiltro = Convert.ToDateTime(fechaDesde);

                    if (fechaInicio < fechaDesdeFiltro)
                        cumpleFiltros = false;
                }

                if (!string.IsNullOrEmpty(fechaHasta))
                {
                    DateTime fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                    DateTime fechaHastaFiltro = Convert.ToDateTime(fechaHasta);

                    if (fechaInicio > fechaHastaFiltro)
                        cumpleFiltros = false;
                }

                if (cumpleFiltros)
                {
                    dtFiltrado.ImportRow(row);
                }
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

                string mensajeNotificacion = "";

                if (estado == "Cancelado")
                {
                    mensajeNotificacion = $"⚠️ Tu viaje #{idViaje} ha sido CANCELADO. Contacta al soporte para más información.";
                }
                else if (estado == "Completado")
                {
                    mensajeNotificacion = $"✅ ¡Tu viaje #{idViaje} ha sido COMPLETADO! Gracias por confiar en nosotros.";
                }
                else if (estado == "En curso")
                {
                    mensajeNotificacion = $"🚚 Tu viaje #{idViaje} está EN CURSO. El vehículo está en camino.";
                }
                else if (estado == "Aprobado")
                {
                    mensajeNotificacion = $"👍 Tu viaje #{idViaje} ha sido APROBADO. Pronto te contactaremos con los detalles.";
                }
                else
                {
                    mensajeNotificacion = $"📋 Tu viaje #{idViaje} ha cambiado de estado a: {estado}.";
                }

                if (!string.IsNullOrEmpty(costo) && costo != "0" && costo != "$0.00")
                {
                    mensajeNotificacion += $" Costo estimado: {costo}.";
                }

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
                string debugInfo = $"RowIndex: {e.RowIndex}, ";
                debugInfo += $"DataKeys Count: {gvSolicitudesClientes.DataKeys.Count}, ";

                if (gvSolicitudesClientes.DataKeys.Count > e.RowIndex)
                {
                    int idViaje = Convert.ToInt32(gvSolicitudesClientes.DataKeys[e.RowIndex].Value);
                    debugInfo += $"idViaje: {idViaje}";

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
                lblMensajeSolicitudes.Text = "Error al eliminar: " + ex.Message + " Stack: " + ex.StackTrace;
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
        protected void btnReportesViajes_Click(object sender, EventArgs e)
        {
            pnlReportesViajes.Visible = true;
            pnlReportesGastos.Visible = false;
            ActivarSubmenuReportes(btnReportesViajes);
            MtCargarReporteViajes();
        }

        protected void btnReportesGastos_Click(object sender, EventArgs e)
        {
            pnlReportesViajes.Visible = false;
            pnlReportesGastos.Visible = true;
            ActivarSubmenuReportes(btnReportesGastos);
            MtCargarReporteGastos();
        }

        private void ActivarSubmenuReportes(Button boton)
        {
            btnReportesViajes.CssClass = "nav-reporte-item";
            btnReportesGastos.CssClass = "nav-reporte-item";
            boton.CssClass = "nav-reporte-item active";
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

                
                int pendientes = 0;
                int enCurso = 0;
                int completados = 0;

                foreach (var viaje in listaViajes)
                {
                    if (viaje.estadoViaje != null)
                    {
                        switch (viaje.estadoViaje.ToLower())
                        {
                            case "pendiente":
                                pendientes++;
                                break;
                            case "en curso":
                                enCurso++;
                                break;
                            case "completado":
                                completados++;
                                break;
                        }
                    }
                }

                
                string script = $@"
                 document.getElementById('totalViajes').innerText = '{listaViajes.Count}';
                 document.getElementById('viajesPendientes').innerText = '{pendientes}';
                 document.getElementById('viajesEnCurso').innerText = '{enCurso}';
                 document.getElementById('viajesCompletados').innerText = '{completados}';
                ";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "actualizarEstadisticas", script, true);

                
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

                Session["TodosViajes"] = dt;
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

        public string GetEstadoIcon(string estado)
        {
            switch (estado.ToLower())
            {
                case "pendiente":
                    return "bi bi-clock";
                case "en curso":
                case "en progreso":
                    return "bi bi-arrow-right-circle";
                case "completado":
                case "finalizado":
                    return "bi bi-check-circle";
                case "cancelado":
                    return "bi bi-x-circle";
                case "aprobado":
                    return "bi bi-check-lg";
                default:
                    return "bi bi-question-circle";
            }
        }

        
        private void MtCargarReporteGastos()
        {
            try
            {
                
                listaGastosCompleta = viajesL.ReporteGastosAdmin();
                Session["TodosGastos"] = listaGastosCompleta;

                if (listaGastosCompleta == null || listaGastosCompleta.Count == 0)
                {
                    lblMensajeReportesGastos.Text = "No se encontraron gastos registrados.";
                    lblMensajeReportesGastos.Style["color"] = "#6c757d";
                    gvReportesGastos.DataSource = null;
                    gvReportesGastos.DataBind();
                    return;
                }




                
                CalcularEstadisticasGastos(listaGastosCompleta);

                
                MostrarGastosFiltrados(listaGastosCompleta);

                lblMensajeReportesGastos.Text = $"Se cargaron {listaGastosCompleta.Count} gastos.";
                lblMensajeReportesGastos.Style["color"] = "#198754";
            }
            catch (Exception ex)
            {
                lblMensajeReportesGastos.Text = $"Error al cargar gastos: {ex.Message}";
                lblMensajeReportesGastos.Style["color"] = "#dc3545";
                gvReportesGastos.DataSource = null;
                gvReportesGastos.DataBind();
            }
        }



        private void CalcularEstadisticasGastos(List<ClGastoM> gastos)
        {
            if (gastos == null || gastos.Count == 0) return;

            int total = gastos.Count;
            decimal montoTotal = gastos.Sum(g => g.monto);
            int combustible = gastos.Count(g => g.tipoGasto?.ToLower().Contains("combustible") == true);
            int mantenimiento = gastos.Count(g => g.tipoGasto?.ToLower().Contains("mantenimiento") == true);
            int otros = total - (combustible + mantenimiento);

            
            string script = $@"
              document.getElementById('totalGastos').innerText = '{total}';
              document.getElementById('gastosCombustible').innerText = '{combustible}';
              document.getElementById('gastosMantenimiento').innerText = '{mantenimiento}';
              document.getElementById('gastosOtros').innerText = '{otros}';
              document.getElementById('montoTotalGastos').innerText = '${montoTotal:N2}';
            ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "actualizarEstadisticasGastos", script, true);
        }

        private void MostrarGastosFiltrados(List<ClGastoM> gastos)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idGasto", typeof(int));
            dt.Columns.Add("tipoGasto", typeof(string));
            dt.Columns.Add("descripcionGasto", typeof(string));
            dt.Columns.Add("monto", typeof(decimal));
            dt.Columns.Add("fechaGasto", typeof(DateTime));
            dt.Columns.Add("nombreUsuario", typeof(string));
            dt.Columns.Add("imagenRecibo", typeof(string));
            dt.Columns.Add("placa", typeof(string));
            dt.Columns.Add("idViaje", typeof(int));

            foreach (var gasto in gastos)
            {
                dt.Rows.Add(
                    gasto.idGasto,
                    gasto.tipoGasto ?? "",
                    gasto.descripcionGasto ?? "",
                    gasto.monto,
                    gasto.fechaGasto,
                    gasto.nombreUsuario ?? "",
                    gasto.imagenRecibo ?? "",
                    gasto.placa ?? "",
                    gasto.idViaje
                );
            }

            gvReportesGastos.DataSource = dt;
            gvReportesGastos.DataBind();
        }

        


        
        public string GetClaseTipoGasto(string tipo)
        {
            switch (tipo?.ToLower())
            {
                case "combustible":
                    return "badge-combustible";
                case "mantenimiento":
                    return "badge-mantenimiento";
                case "peajes":
                case "alimentación":
                case "otros":
                    return "badge-otros";
                default:
                    return "badge-otros";
            }
        }

        public string GetIconoTipoGasto(string tipo)
        {
            switch (tipo?.ToLower())
            {
                case "combustible":
                    return "bi bi-fuel-pump";
                case "mantenimiento":
                    return "bi bi-tools";
                case "peajes":
                    return "bi bi-signpost";
                case "alimentación":
                    return "bi bi-egg-fried";
                case "otros":
                    return "bi bi-cash-stack";
                default:
                    return "bi bi-cash";
            }
        }

        public string MostrarBotonEvidencia(string rutaImagen)
        {
            if (!string.IsNullOrEmpty(rutaImagen) && rutaImagen != "")
            {
                
                string rutaCompleta = "";

                if (rutaImagen.StartsWith("~/") || rutaImagen.StartsWith("http"))
                {
                    
                    rutaCompleta = rutaImagen;
                }
                else
                {
                   
                    rutaCompleta = "~/Vista/Imagenes/" + rutaImagen;
                }

                
                string rutaParaCliente = ResolveUrl(rutaCompleta);

                return $@"
                  <button type='button' class='btn btn-sm btn-outline-primary' 
                   onclick='mostrarImagen(""{rutaParaCliente.Replace("\"", "\\\"")}"")' 
                   data-bs-toggle='modal' data-bs-target='#modalImagen'>
                  <i class='bi bi-receipt'></i> Ver
                  </button>";
            }
            return "<span class='text-muted'>Sin evidencia</span>";
        }
       

        protected void btnBuscarPlacaGastos_Click(object sender, EventArgs e)
        {
            string placa = txtBuscarPlacaGastos.Text.Trim();

            if (string.IsNullOrEmpty(placa))
            {
                lblMensajeReportesGastos.Text = "Por favor ingrese una placa para buscar.";
                lblMensajeReportesGastos.Style["color"] = "#dc3545";
                return;
            }

            try
            {
                
                List<ClGastoM> todosGastos = Session["TodosGastos"] as List<ClGastoM>;

                if (todosGastos == null || todosGastos.Count == 0)
                {
                    
                    todosGastos = viajesL.ReporteGastosAdmin();
                    Session["TodosGastos"] = todosGastos;
                }

               
                var gastosFiltrados = todosGastos
                    .Where(g => g.placa != null && g.placa.ToLower().Contains(placa.ToLower()))
                    .ToList();

                if (gastosFiltrados.Count == 0)
                {
                    lblMensajeReportesGastos.Text = $"No se encontraron gastos para la placa: {placa}";
                    lblMensajeReportesGastos.Style["color"] = "#6c757d";
                }
                else
                {
                    lblMensajeReportesGastos.Text = $"Se encontraron {gastosFiltrados.Count} gastos para la placa: {placa}";
                    lblMensajeReportesGastos.Style["color"] = "#198754";
                }

                
                CalcularEstadisticasGastos(gastosFiltrados);

                
                MostrarGastosFiltrados(gastosFiltrados);
            }
            catch (Exception ex)
            {
                lblMensajeReportesGastos.Text = $"Error al buscar gastos: {ex.Message}";
                lblMensajeReportesGastos.Style["color"] = "#dc3545";
            }
        }

        protected void btnLimpiarFiltroPlaca_Click(object sender, EventArgs e)
        {
            
            txtBuscarPlacaGastos.Text = "";

           
            MtCargarReporteGastos();
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
            pnlReportesGastos.Visible = false;
            ActivarSubmenuReportes(btnReportesViajes);
            MtCargarReporteViajes();
        }




    }
}