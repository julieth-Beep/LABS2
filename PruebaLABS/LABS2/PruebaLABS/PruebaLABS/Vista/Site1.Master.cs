using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Mostrar información del cliente si existe en sesión
                if (Session["cliente_nombre"] != null)
                {
                    lblClienteNombre.Text = Session["cliente_nombre"].ToString();
                }
                else
                {
                    lblClienteNombre.Text = "Cliente";
                }

                if (Session["cliente_empresa"] != null)
                {
                    lblClienteEmpresa.Text = "(" + Session["cliente_empresa"].ToString() + ")";
                }
                else
                {
                    lblClienteEmpresa.Text = "";
                }
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            // Limpiar sesión del cliente
            Session.Remove("cliente_documento");
            Session.Remove("cliente_nombre");
            Session.Remove("cliente_empresa");
            Session.Remove("cliente_correo");

            // Redirigir al inicio
            Response.Redirect("Inicio.aspx");
        }
    }
}