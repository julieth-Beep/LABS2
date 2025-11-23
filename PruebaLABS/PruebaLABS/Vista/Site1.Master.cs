using System;
using System.Web.UI;

namespace PruebaLABS.Vista
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["nombre"] == null && Session["idCliente"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Session["idCliente"] != null)
                {
                    lblUsuario.Text = Session["nombre"].ToString();
                    lblRol.Text = "Cliente";
                }
                else if (Session["nombre"] != null)
                {
                    lblUsuario.Text = Session["nombre"].ToString();

                    string nombreRol = "";
                    switch (Session["rol"].ToString())
                    {
                        case "1":
                            nombreRol = "Conductor";
                            break;
                        case "2":
                            nombreRol = "Administrador";
                            break;
                        case "3":
                            nombreRol = "Contador";
                            break;
                        default:
                            nombreRol = "Usuario";
                            break;
                    }
                    lblRol.Text = nombreRol;
                }
            }
        }


        protected void btnCerrar_Click(object sender, EventArgs e)
        {
          
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Response.Redirect("Login.aspx");
        }
    }
}
