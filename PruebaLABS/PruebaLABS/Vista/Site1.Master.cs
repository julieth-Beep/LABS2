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

                if (Session["nombre"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                lblUsuario.Text = Session["nombre"].ToString();

                string nombreRol = "";

     
                if (Session["esCliente"] != null && Convert.ToBoolean(Session["esCliente"]))
                {
                    nombreRol = "Cliente";
                }
                else if (Session["rol"] != null)
                {
          
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
                }
                else
                {
                    nombreRol = "Usuario";
                }

                lblRol.Text = nombreRol;
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
