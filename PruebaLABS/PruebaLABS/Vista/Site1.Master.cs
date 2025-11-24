using System;

namespace PruebaLABS.Vista
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombre"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            lblUsuario.Text = Session["nombre"].ToString();

            string rol = "Usuario";

            bool esCliente = Session["esCliente"] != null && (bool)Session["esCliente"];

            if (esCliente)
                rol = "Cliente";
            else if (Session["rol"] != null)
            {
                switch (Session["rol"].ToString())
                {
                    case "1": rol = "Conductor"; break;
                    case "2": rol = "Administrador"; break;
                    case "3": rol = "Contador"; break;
                }
            }

            lblRol.Text = rol;
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
