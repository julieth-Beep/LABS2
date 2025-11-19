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
    public partial class OpcionesContador : System.Web.UI.Page
    {
        ClContadorL contadorL = new ClContadorL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGridGastos();
            }
        }
        private void CargarGridGastos()
        {
            DataTable dt = contadorL.ListarGastosPorConductor();

            gvGastos.DataSource = dt;
            gvGastos.DataBind();
        }
    }
}