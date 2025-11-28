using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Web.UI;

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
            ClUsuarioM ingresoUsuario = ousuL.MtLogin(user, pass);

            if (ingresoUsuario != null)
            {
                Session["idUsuario"]=ingresoUsuario.idUsuario;
                Session["rol"] = ingresoUsuario.idRol;
                Session["nombre"] = ingresoUsuario.nombre;
                Session["correo"] = ingresoUsuario.correo;

                if (ingresoUsuario.idRol == 2)
                    Response.Redirect("opcionesAdmin.aspx");
                else if (ingresoUsuario.idRol == 1)
                    Response.Redirect("OpcionesConductor.aspx");
                else if (ingresoUsuario.idRol == 3)
                    Response.Redirect("OpcionesContador.aspx");

                return;
            }


            ClClienteL oClienteL = new ClClienteL();
            ClClienteM ingresoCliente = oClienteL.MtLoginCliente(user, pass);

            if (ingresoCliente != null)
            {
                Session["idCliente"] = ingresoCliente.idCliente;
                Session["nombre"] = ingresoCliente.nombre + " " + ingresoCliente.apellido;
                Session["correo"] = ingresoCliente.correo;

                Response.Redirect("OpcionesCliente.aspx");
                return;
            }



            lnlMensaje.Text = "Correo o contraseña incorrectos.";
        }
    }
}