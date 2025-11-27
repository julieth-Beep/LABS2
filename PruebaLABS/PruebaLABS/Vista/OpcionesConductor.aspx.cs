using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesConductor : System.Web.UI.Page
    {
        ClViajeL logicaViaje = new ClViajeL();
        ClViajeD viaje = new ClViajeD();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["nombre"] == null || Session["idUsuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                if (Session["rol"] == null || (int)Session["rol"] != 1)
                {
                    Response.Redirect("inicio.aspx");
                    return;
                }
                lblNombreConductor.Text = Session["nombre"].ToString();
                CargarViajes();
                CargarViajesParaGastos();

                if (pnlGastos.Visible)
                {
                    CargarGastos();
                }
            }
        }

        private void CargarViajes()
        {
            try
            {
                int idConductor = Convert.ToInt32(Session["idUsuario"]);
                var viajes = logicaViaje.MtViajesConductor(idConductor);
                gvViajes.DataSource = viajes;
                gvViajes.DataBind();
            }
            catch (Exception ex)
            {
                MostarMensajes("Error al cargar los viajes:" + ex.Message, "danger");
            }
        }

        protected void btnActualizar_Click(Object sender, EventArgs e)
        {
            CargarViajes();
            MostarMensajes("Lista de viajes actualizada", "success");
        }

        private void MostarMensajes(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = $"alert alert-{tipo} fade show";
            lblMensaje.Visible = true;
        }

        protected void gvViajes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cambiarEstado")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idViaje = Convert.ToInt32(gvViajes.DataKeys[index].Value);
                txtEstado.Text = idViaje.ToString();
                pnlEditar.Visible = true;
                ddlEstado.SelectedIndex = 0;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idViaje = Convert.ToInt32(txtEstado.Text);
            string nuevoEstado = ddlEstado.SelectedItem.Text;

            viaje.MtCambiarEstadoViaje(idViaje, nuevoEstado);

            lblMensaje.Text = "Estado actualizado correctamente";
            lblMensaje.Visible = true;

            CargarViajes();
        }

        private void CargarGastos()
        {
            int idConductor = Convert.ToInt32(Session["idUsuario"]);
            var gastos = logicaViaje.GRConductor(idConductor);
            gvGastos.DataSource = gastos;
            gvGastos.DataBind();
        }

        private void CargarViajesParaGastos()
        {
            try
            {
                int idConductor = Convert.ToInt32(Session["idUsuario"]);
                var viajes = logicaViaje.MtViajesConductor(idConductor);
                ddlViaje.DataSource = viajes;
                ddlViaje.DataTextField = "destino";
                ddlViaje.DataValueField = "idViaje";
                ddlViaje.DataBind();
                ddlViaje.Items.Insert(0, new ListItem("Seleccione un viaje", "0"));
            }
            catch (Exception ex)
            {
                MostarMensajes("Error al cargar viajes para gastos: " + ex.Message, "danger");
            }
        }


        protected void btnViajes_Click(object sender, EventArgs e)
        {
            pnlTablaViajes.Visible = true;
            pnlGastos.Visible = false;
            pnlEditar.Visible = false;
            ActualizarMenuActivo(btnViajes, btnReportes);
            CargarViajes();
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            pnlTablaViajes.Visible = false;
            pnlGastos.Visible = true;
            pnlEditar.Visible = false;
            ActualizarMenuActivo(btnReportes, btnViajes);
            CargarGastos();
        }

        private void ActualizarMenuActivo(Button botonActivo, Button botonInactivo)
        {
            botonActivo.CssClass = "sidebar-item active";
            botonInactivo.CssClass = "sidebar-item";
        }


        protected void btnNuevoGasto_Click(object sender, EventArgs e)
        {
            LimpiarFormularioGasto();
            pnlModalGasto.Visible = true;
        }

        protected void btnGuardarGasto_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlViaje.SelectedValue == "0")
                {
                    MostarMensajes("Debe seleccionar un viaje", "warning");
                    return;
                }

                if (string.IsNullOrEmpty(txtMonto.Text) || !decimal.TryParse(txtMonto.Text, out decimal monto))
                {
                    MostarMensajes("Debe ingresar un monto válido", "warning");
                    return;
                }

                string nombreArchivo = "";

                if (fuEvidencia.HasFile)
                {
                    string fileName = Path.GetFileName(fuEvidencia.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();

                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif")
                    {
                        MostarMensajes("Solo se permiten archivos de imagen (JPG, PNG, GIF)", "warning");
                        return;
                    }

                    if (fuEvidencia.PostedFile.ContentLength > 5 * 1024 * 1024)
                    {
                        MostarMensajes("El archivo no puede ser mayor a 5MB", "warning");
                        return;
                    }

                    string folderPath = Server.MapPath("~/Evidencias/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    nombreArchivo = $"gasto_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                    string filePath = Path.Combine(folderPath, nombreArchivo);
                    fuEvidencia.SaveAs(filePath);
                }

                int idConductor = Convert.ToInt32(Session["idUsuario"]);
                int idViaje = Convert.ToInt32(ddlViaje.SelectedValue);
                int idViajeVehiculo = logicaViaje.MtObtenerIdViajeVehiculo(idViaje, idConductor);

                if (idViajeVehiculo == 0)
                {
                    MostarMensajes("No se pudo asociar el gasto al viaje", "danger");
                    return;
                }

                var gasto = new ClGastoM
                {
                    idViajeVehiculo = idViajeVehiculo,
                    tipoGasto = ddlTipoGasto.SelectedValue,
                    monto = monto,
                    descripcionGasto = txtDescripcionGasto.Text ?? "",
                    fechaGasto = DateTime.Now,
                    imagenRecibo = nombreArchivo
                };


                string resultado = logicaViaje.MtRegistrarGastoConImagen(gasto);
                MostarMensajes(resultado, "success");

                pnlModalGasto.Visible = false;
                CargarGastos();
            }
            catch (Exception ex)
            {
                MostarMensajes("Error al guardar el gasto: " + ex.Message, "danger");
            }
        }


        protected void btnVerEvidencia_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string nombreArchivo = btn.CommandArgument;

            if (!string.IsNullOrEmpty(nombreArchivo))
            {
                string imagePath = "~/Evidencias/" + nombreArchivo;
                if (File.Exists(Server.MapPath(imagePath)))
                {
                    imgAmpliada.ImageUrl = imagePath;
                    pnlModalImagen.Visible = true;
                }
                else
                {
                    MostarMensajes("La imagen no se encuentra disponible", "warning");
                }
            }
        }

        private void LimpiarFormularioGasto()
        {
            ddlTipoGasto.SelectedIndex = 0;
            txtMonto.Text = "";
            txtDescripcionGasto.Text = "";
            ddlViaje.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "limpiarPreview", "limpiarPreview();", true);
        }


        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlModalGasto.Visible = false;
        }

        protected void btnCerrarImagen_Click(object sender, EventArgs e)
        {
            pnlModalImagen.Visible = false;
        }

        protected void btnCancelarGasto_Click(object sender, EventArgs e)
        {
            pnlModalGasto.Visible = false;
        }
    }
}