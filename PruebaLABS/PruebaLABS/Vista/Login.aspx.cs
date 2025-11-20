using PruebaLABS.Datos;
using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void bntIngresar_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Text;

            ClUsuarioL ousuL = new ClUsuarioL();
            ClUsuarioM ingreso = ousuL.MtLogin(user, pass);

            if (ingreso != null)
            {
                Session["rol"] = ingreso.idRol;
                Session["nombre"] = ingreso.nombre;
                Session["correo"] = ingreso.correo;

                if (ingreso.idRol == 2) 
                {
                    Response.Redirect("opcionesAdmin.aspx");
                }
                else if (ingreso.idRol == 1) 
                {
                    Response.Redirect("OpcionesConductor.aspx");
                }
                else if (ingreso.idRol == 3) 
                {
                    Response.Redirect("OpcionesContador.aspx");
                }
                else if (ingreso.idRol == 4) 
                {
                    Response.Redirect("OpcionesCliente.aspx");
                }
            }
            else
            {
                lnlMensaje.Text = "No tiene cuenta aún, regístrese para poder ingresar";
            }
        }
    }
}