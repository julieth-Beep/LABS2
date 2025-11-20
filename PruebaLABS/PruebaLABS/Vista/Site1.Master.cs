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

}