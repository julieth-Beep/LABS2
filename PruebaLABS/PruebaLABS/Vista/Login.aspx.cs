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
                Session["esCliente"] = false;

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
               
                
            }
            else
            {
                ClClienteL cliente = new ClClienteL();
                ClClienteM ingresoC = cliente.MtLoginCliente(user, pass);

                if (ingresoC != null)
                {
                    Session["esCliente"] = true;
                    Session["idCliente"] = ingresoC.idCliente;
                    Session["nombre"] = ingresoC.nombre;
                    Session["correo"] = ingresoC.correo;

                    Response.Redirect("OpcionesCliente.aspx");
                }
                else
                {
                    lnlMensaje.Text = "Credenciales incorrectas.Verifique su email y contraseña.";
                }

            }

        }
    }
}