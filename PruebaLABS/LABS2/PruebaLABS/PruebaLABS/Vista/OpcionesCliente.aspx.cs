using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar que el cliente esté registrado (tenga datos en sesión)
                if (Session["cliente_nombre"] == null)
                {
                    // Si no hay datos de cliente, redirigir al registro
                    Response.Redirect("Registro.aspx");
                }

                // Aquí puedes cargar los datos específicos del cliente si es necesario
                // Por ejemplo, cargar los vehículos disponibles
                CargarVehiculos();
            }
        }

        private void CargarVehiculos()
        {
            // Tu código existente para cargar vehículos
            ClClienteL logicaCliente = new ClClienteL();
            gvFlota.DataSource = logicaCliente.ListaDatVehiculo();
            gvFlota.DataBind();
        }

        // Tus otros métodos existentes...
    }
}