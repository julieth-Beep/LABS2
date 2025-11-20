using PruebaLABS.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesCliente : System.Web.UI.Page
    {
        ClClienteL clienteL = new ClClienteL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatFlota();
            }
        }
        private void CargarDatFlota()
        {
            DataTable dt = clienteL.ListaDatVehiculo();

            gvFlota.DataSource = dt;
            gvFlota.DataBind();
        }
    }

}