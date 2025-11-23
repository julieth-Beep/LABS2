using PruebaLABS.Datos;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesAdmin : System.Web.UI.Page
    {
        ClVehiculoD oVehiculoD = new ClVehiculoD();
        ClVehiculoL oVehiculoL = new ClVehiculoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarVehiculos();
            }
        }

        private void MtCargarVehiculos()
        {
            gvVehiculos.DataSource = oVehiculoD.MtListarVehiculos();
            gvVehiculos.DataBind();
        }

      
        protected void gvVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvVehiculos.Rows[index];
            int idVehiculo = Convert.ToInt32(row.Cells[0].Text);

           
            if (e.CommandName == "editar")
            {
                txtIdVehiculo.Text = row.Cells[0].Text;
                txtPlaca.Text = row.Cells[1].Text;
                txtModelo.Text = row.Cells[2].Text;
                txtCapacidad.Text = row.Cells[3].Text;

                // Seleccionar estado correctamente
                ddlEstado.ClearSelection();
                ListItem item = ddlEstado.Items.FindByText(row.Cells[4].Text);
                if (item != null)
                    ddlEstado.SelectedValue = item.Value;

                lblMensaje.Text = "Vehículo cargado para edición.";
            }

            if (e.CommandName == "cambiarEstado")
            {
                string estadoActual = row.Cells[4].Text;
                int nuevoEstado = 1;

                if (estadoActual == "Disponible") nuevoEstado = 2;
                else if (estadoActual == "En mantenimiento") nuevoEstado = 3;
                else if (estadoActual == "Fuera de servicio") nuevoEstado = 1;

                string mensaje = oVehiculoD.MtCambiarEstado(idVehiculo, nuevoEstado);
                lblMensaje.Text = mensaje;

                MtCargarVehiculos();
            }

            if (e.CommandName == "eliminar")
            {
                string mensaje = oVehiculoD.MtEliminarVehiculo(idVehiculo);
                lblMensaje.Text = mensaje;

                MtCargarVehiculos();
            }
        }

       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ClVehiculoM v = new ClVehiculoM()
            {
                idVehiculo = int.Parse(txtIdVehiculo.Text),
                placa = txtPlaca.Text,
                modelo = txtModelo.Text,
                capacidad = txtCapacidad.Text,
                idEstadoVehiculo = int.Parse(ddlEstado.SelectedValue)
            };

            string mensaje = oVehiculoD.MtEditarVehiculo(v);
            lblMensaje.Text = mensaje;

            MtCargarVehiculos();
        }

       
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ClVehiculoM v = new ClVehiculoM()
            {
                placa = txtAddPlaca.Text,
                modelo = txtAddModelo.Text,
                capacidad = txtAddCapacidad.Text,
                idEstadoVehiculo = int.Parse(ddlAddEstado.SelectedValue)
            };

            string mensaje = oVehiculoD.MtAgregarVehiculo(v);
            lblAddMensaje.Text = mensaje;

            MtCargarVehiculos();

            txtAddPlaca.Text = "";
            txtAddModelo.Text = "";
            txtAddCapacidad.Text = "";
            ddlAddEstado.SelectedIndex = 0;
        }
    }
}
