using PruebaLABS.Logica;
using PruebaLABS.Modelo;
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
        ClContadorL oContL = new ClContadorL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowContratos(null, null);
            }
        }

        protected void ShowContratos(object sender, EventArgs e)
        {
            HideAllPanels();
            gvContratos.Visible = true;
            LoadContratos();
            lblFeedback.Text = "";
        }

        protected void ShowGastos(object sender, EventArgs e)
        {
            HideAllPanels();
            panelGastos.Visible = true;
            LoadGastos();
            lblFeedback.Text = "";
        }

        protected void ShowBonos(object sender, EventArgs e)
        {
            HideAllPanels();
            panelBonos.Visible = true;
            LoadBonos();
            lblFeedback.Text = "";
        }

        protected void ShowTotales(object sender, EventArgs e)
        {
            HideAllPanels();
            panelTotales.Visible = true;
            LoadTotales();
            lblFeedback.Text = "";
        }

        protected void ShowContratoUsuario(object sender, EventArgs e)
        {
            HideAllPanels();
            panelContratoUsuario.Visible = true;
            lblFeedback.Text = "";
        }

        private void HideAllPanels()
        {
            gvContratos.Visible = false;
            panelGastos.Visible = false;
            panelBonos.Visible = false;
            panelTotales.Visible = false;
            panelContratoUsuario.Visible = false;
        }

        private void LoadContratos()
        {
            DataTable dt = oContL.Contratos();
            gvContratos.DataSource = dt;
            gvContratos.DataBind();
        }

        private void LoadGastos()
        {
            DataTable dt = oContL.ListarGastosPorConductor();
            gvGastos.DataSource = dt;
            gvGastos.DataBind();
        }

        private void LoadBonos()
        {
            DataTable dt = oContL.Bono();
            gvBonos.DataSource = dt;
            gvBonos.DataBind();
        }

        private void LoadTotales()
        {
            DataTable dt = oContL.Contratos();
            gvTotal.DataSource = dt;
            gvTotal.DataBind();
        }
        protected void gvContratos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idContrato = Convert.ToInt32(e.CommandArgument);

                hfIdContratoEditar.Value = idContrato.ToString();

                DataTable dt = oContL.Contratos();
                var row = dt.AsEnumerable().FirstOrDefault(r => r.Field<int>("idContrato") == idContrato);

                if (row != null)
                {
                    txtEditDocumento.Text = row["documento"].ToString();
                    txtEditSalario.Text = row["salario"].ToString();
                    txtEditTipo.Text = row["tipo"].ToString();
                    txtEditBono.Text = row["bono"].ToString();
                }

                panelEditarContrato.Visible = true;
            }
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            ClContratoM c = new ClContratoM();
            c.idContrato = Convert.ToInt32(hfIdContratoEditar.Value);
            c.salario = Convert.ToDecimal(txtEditSalario.Text);
            c.tipo = txtEditTipo.Text;
            c.bono = Convert.ToDecimal(txtEditBono.Text);
            c.fecha = DateTime.Now;

            string mensaje = oContL.MtEditContrato(c);
            lblFeedback.Text = mensaje;

            panelEditarContrato.Visible = false;
            LoadContratos(); 
        }
        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            panelEditarContrato.Visible = false;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string doc = txtBuscarDocumento.Text.Trim();
            if (string.IsNullOrEmpty(doc))
            {
                lblFeedback.Text = "Ingrese un documento para buscar.";
                return;
            }

            var lista = oContL.MtListContra(doc);
            if (lista == null || lista.Count == 0)
            {
                lblFeedback.Text = "No se encontraron contratos para el documento.";
                gvUsuario.DataSource = null;
                gvUsuario.DataBind();
                return;
            }
            gvUsuario.DataSource = lista;
            gvUsuario.DataBind();
            lblFeedback.Text = "";
        }

        protected void btnRegistrarContrato_Click(object sender, EventArgs e)
        {
            string doc = txtRegDocumento.Text.Trim();
            if (string.IsNullOrEmpty(doc))
            {
                lblFeedback.Text = "Ingrese documento para registrar.";
                return;
            }

            if (!decimal.TryParse(txtRegSalario.Text.Trim(), out decimal salario))
            {
                lblFeedback.Text = "Salario inválido.";
                return;
            }

            var lista = oContL.MtListContra(doc);
            if (lista == null || lista.Count == 0)
            {
                lblFeedback.Text = "Usuario no encontrado. No se puede registrar contrato.";
                return;
            }

            if (lista[0].salario > 0)
            {
                lblFeedback.Text = "El usuario ya tiene un contrato registrado.";
                return;
            }

            ClContratoM nuevo = new ClContratoM()
            {
                idUusuario = lista[0].idUusuario,
                documento = doc,
                salario = salario,
                tipo = ddlRegTipo.SelectedValue,
                fecha = DateTime.Now,
                bono = 0
            };

            string resp = oContL.Registrar(nuevo);
            lblFeedback.Text = resp;

            gvUsuario.DataSource = null;
            gvUsuario.DataBind();
            LoadContratos();
            LoadTotales();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            int index = row.RowIndex;
            int idContratoVal = Convert.ToInt32(gvUsuario.DataKeys[index].Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#modalEditar').modal('show');", true);
        }

    }
}