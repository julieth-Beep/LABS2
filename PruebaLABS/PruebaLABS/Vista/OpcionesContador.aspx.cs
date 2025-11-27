using OfficeOpenXml;
using OfficeOpenXml.Table;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Contracts;
using System.IO;
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
                CargarContratosEmp();
            }
        }
        protected void btnContraEmp_Click(object sender, EventArgs e)
        {
            pnlContraEmp.Visible = true;
            pnlContraViaj.Visible = false;
            pnlGastos.Visible = false;
            pnlBonos.Visible = false;

            CargarContratosEmp();
        }

        protected void btnContraViaj_Click(object sender, EventArgs e)
        {
            pnlContraEmp.Visible = false;
            pnlContraViaj.Visible = true;
            pnlGastos.Visible = false;
            pnlBonos.Visible = false;

            CargarContratosViaje();
        }

        protected void btnGastos_Click(object sender, EventArgs e)
        {
            pnlContraEmp.Visible = false;
            pnlContraViaj.Visible = false;
            pnlGastos.Visible = true;
            pnlBonos.Visible = false;

            gvGastos.DataSource = null;
            gvGastos.DataBind();
        }

        protected void btnBonos_Click(object sender, EventArgs e)
        {
            pnlContraEmp.Visible = false;
            pnlContraViaj.Visible = false;
            pnlGastos.Visible = false;
            pnlBonos.Visible = true;

            CargarBonos();
        }
        private void CargarContratosEmp()
        {
            DataTable dt = oContL.ContratosEmp();
            gvContraEmp.DataSource = dt;
            gvContraEmp.DataBind();
        }

        protected void gvContraEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            // Obtener valores de la fila
            GridViewRow row = gvContraEmp.Rows[index];
            int idContrato = Convert.ToInt32(row.Cells[0].Text);

            if (e.CommandName == "editar")
            {
                txtIdContrato.Text = idContrato.ToString();
                txtFecha.Text = row.Cells[5].Text;
                txtSalario.Text = row.Cells[6].Text;
                txtBono.Text = "";
                ddlTipo.SelectedValue = row.Cells[7].Text;

                lblMensaje.Text = "";
            }
            else if (e.CommandName == "eliminar")
            {
                string resp = oContL.EliminarContra(idContrato);
                lblMensaje.Text = resp;

                CargarContratosEmp();
            }
            else if (e.CommandName == "cambiarTipo")
            {
                string tipoActual = row.Cells[7].Text;
                string nuevoTipo = (tipoActual == "Fijo") ? "Indefinido" : "Fijo";

                ClContratoM m = new ClContratoM();
                m.idContrato = idContrato;
                m.tipo = nuevoTipo;

                oContL.MtEditContrato(m);

                CargarContratosEmp();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ClContratoM m = new ClContratoM();
            m.idContrato = Convert.ToInt32(txtIdContrato.Text);
            m.fecha = Convert.ToDateTime(txtFecha.Text);
            m.salario = Convert.ToDecimal(txtSalario.Text);
            m.bono = Convert.ToDecimal(txtBono.Text);
            m.tipo = ddlTipo.SelectedValue;

            string respuesta = oContL.MtEditContrato(m);
            lblMensaje.Text = respuesta;

            CargarContratosEmp();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ClContratoM m = new ClContratoM();

            m.documento = txtAddDocumento.Text;
            m.fecha = Convert.ToDateTime(txtFecha.Text);
            m.salario = Convert.ToDecimal(txtSalario.Text);
            m.bono = Convert.ToDecimal(txtBono.Text);
            m.tipo = DropDownList1.SelectedValue;

            string r = oContL.Registrar(m);
            lblAddMensaje.Text = r;

            CargarContratosEmp();
        }

        private void CargarContratosViaje()
        {
            DataTable dt = oContL.ContratosViaje();
            gvContraViaj.DataSource = dt;
            gvContraViaj.DataBind();
        }

        protected void btnBuscarGastos_Click(object sender, EventArgs e)
        {
            if (txtIdViajeBuscar.Text == "")
            {
                lblGastosMensaje.Text = "Ingrese el ID del viaje.";
                return;
            }

            int idViaje = Convert.ToInt32(txtIdViajeBuscar.Text);

            DataTable dt = oContL.ListarGastosViaje(idViaje);

            if (dt.Rows.Count == 0)
            {
                lblGastosMensaje.Text = "No se encontraron gastos para este viaje.";
                gvGastos.DataSource = null;
                gvGastos.DataBind();
            }
            else
            {
                lblGastosMensaje.Text = "";
                gvGastos.DataSource = dt;
                gvGastos.DataBind();
            }
        }

        private void CargarBonos()
        {
            DataTable dt = oContL.Bonos();
            gvBonos.DataSource = dt;
            gvBonos.DataBind();
        }
        protected void gvGastos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal total = 0;

                foreach (GridViewRow row in gvGastos.Rows)
                {
                    total += Convert.ToDecimal(row.Cells[2].Text); 
                }

                e.Row.Cells[1].Text = "TOTAL:";
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].Text = total.ToString("N0");
                e.Row.Cells[2].Font.Bold = true;
            }
        }
        private void ExportarExcel(GridView grid, string nombreArchivo)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                var ws = excel.Workbook.Worksheets.Add("Datos");

                DataTable dt = new DataTable();

                foreach (DataControlField col in grid.Columns)
                    dt.Columns.Add(col.HeaderText);

                foreach (GridViewRow row in grid.Rows)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++)
                        dr[i] = row.Cells[i].Text;
                    dt.Rows.Add(dr);
                }

                ws.Cells["A1"].LoadFromDataTable(dt, true);
                ws.Cells.AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", $"attachment; filename={nombreArchivo}.xlsx");
                Response.BinaryWrite(excel.GetAsByteArray());
                Response.End();
            }
        }


        private DataTable ImportarExcel(FileUpload archivo)
        {
            string ruta = Server.MapPath("~/Vista/Archivos/");
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string path = Path.Combine(ruta, archivo.FileName);
            archivo.SaveAs(path);

            DataTable dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var ws = package.Workbook.Worksheets[0];
                int colCount = ws.Dimension.End.Column;
                int rowCount = ws.Dimension.End.Row;

                // Crear columnas
                for (int col = 1; col <= colCount; col++)
                    dt.Columns.Add(ws.Cells[1, col].Text);

                // Crear filas
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                        dr[col - 1] = ws.Cells[row, col].Text;
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            ExportarExcel(gvContraEmp, "Contratos_Empleados");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (!FileUpload2.HasFile)
            {
                lblMensaje.Text = "Suba un archivo Excel.";
                return;
            }

            DataTable dt = ImportarExcel(FileUpload2);

            foreach (DataRow row in dt.Rows)
            {
                ClContratoM c = new ClContratoM();

                c.documento = row["Documento"].ToString();
                c.fecha = Convert.ToDateTime(row["Fecha"].ToString());
                c.salario = Convert.ToDecimal(row["Salario"]);
                c.bono = Convert.ToDecimal(row["Bono"]);
                c.tipo = row["Tipo de Contrato"].ToString();

                new ClContadorL().Registrar(c);
            }

            lblMensaje.Text = "Importación completada.";
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ExportarExcel(gvContraViaj, "Contratos_Viajes");
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarExcel(gvGastos, "Gastos_Viaje");
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            if (!fileExcel.HasFile)
            {
                lblGastosMensaje.Text = "Seleccione un archivo.";
                return;
            }

            DataTable dt = ImportarExcel(fileExcel);

            foreach (DataRow row in dt.Rows)
            {
                ClGastoM g = new ClGastoM();

                g.tipoGasto = row["Tipo"].ToString();
                g.monto = Convert.ToDecimal(row["Monto"]);
                g.descripcionGasto = row["Descripción"].ToString();
                g.fechaGasto = Convert.ToDateTime(row["Fecha"]);
                g.idViajeVehiculo = Convert.ToInt32(txtIdViajeBuscar.Text);

                new ClContadorL().RegistrarGasto(g);
            }

            lblGastosMensaje.Text = "Importación completada.";
        }
        protected void Export_Click(object sender, EventArgs e)
        {
            ExportarExcel(gvBonos, "Bonos_Empleados");
        }



        public override void VerifyRenderingInServerForm(Control control) { }



    }
}