using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
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
            lblMensaje.CssClass = $"alert alert-{tipo}fade show";
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
    }
}
