using OfficeOpenXml;
using PruebaLABS.Logica;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaLABS.Vista
{
    public partial class OpcionesContador : System.Web.UI.Page
    {
        ClContadorL oContL = new ClContadorL();
        ClEstadisticaL estadisticaL = new ClEstadisticaL();

        ClViajeL viajesL = new ClViajeL();
        List<ClGastoM> listaGastosCompleta = new List<ClGastoM>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlContraEmp.Visible = true;
                pnlContraViaj.Visible = false;
                pnlGastos.Visible = false;
                pnlBonos.Visible = false;
                pnlContabilidad.Visible = false;
                pnlReportes.Visible = false;

                CargarContratosEmp();
            }
        }

        private void OcultarTodo()
        {
            pnlContraEmp.Visible = false;
            pnlContraViaj.Visible = false;
            pnlGastos.Visible = false;
            pnlBonos.Visible = false;
            pnlContabilidad.Visible = false;
            pnlReportes.Visible = false;
        }

        protected void btnContraEmp_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            pnlContraEmp.Visible = true;
            CargarContratosEmp();
        }

        protected void btnContraViaj_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            pnlContraViaj.Visible = true;
            CargarContratosViaje();
        }

        protected void btnGastos_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            pnlGastos.Visible = true;

            gvGastos.DataSource = null;
            gvGastos.DataBind();
        }

        protected void btnBonos_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            pnlBonos.Visible = true;
            CargarBonos();
        }

        protected void btnEstadistica_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            pnlContabilidad.Visible = true;
            ObtenerEstadistica();
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
            m.fecha = Convert.ToDateTime(txtAddFecha.Text);
            m.salario = Convert.ToDecimal(txtAddSalario.Text);
            m.bono = Convert.ToDecimal(txtAddBono.Text);
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

        protected void gvGastos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal total = 0;

                foreach (GridViewRow row in gvGastos.Rows)
                {
                    decimal v;
                    if (decimal.TryParse(row.Cells[11].Text, out v))
                        total += v;
                }

                e.Row.Cells[1].Text = "TOTAL:";
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].Text = total.ToString("N0");
                e.Row.Cells[2].Font.Bold = true;
            }
        }

        private void CargarBonos()
        {
            DataTable dt = oContL.Bonos();
            gvBonos.DataSource = dt;
            gvBonos.DataBind();
        }

        protected void gvBonos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBonos.EditIndex = e.NewEditIndex;
            CargarBonos();
        }

        protected void gvBonos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBonos.EditIndex = -1;
            CargarBonos();
        }

        protected void gvBonos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idUsuario = Convert.ToInt32(gvBonos.DataKeys[e.RowIndex].Value);
            TextBox txtBono = gvBonos.Rows[e.RowIndex].FindControl("txtBonoEdit") as TextBox;

            decimal nuevoBono = 0;
            decimal.TryParse(txtBono.Text, out nuevoBono);

            ClContadorL logica = new ClContadorL();
            logica.EditarBono(idUsuario, nuevoBono);

            gvBonos.EditIndex = -1;
            CargarBonos();
        }

        private void ObtenerEstadistica()
        {
            DataTable dt = estadisticaL.ObtenerEstadistica();

            decimal totalIngreso = 0;
            decimal totalGasto = 0;
            decimal totalContrato = 0;

            foreach (DataRow row in dt.Rows)
            {
                string tipo = row["TipoMovimiento"].ToString().ToUpper();
                decimal monto = Convert.ToDecimal(row["Monto"]);

                if (tipo == "INGRESO")
                    totalIngreso += monto;
                else if (tipo == "GASTO")
                    totalGasto += monto;
                else if (tipo == "CONTRATO")
                    totalContrato += monto;
            }

            List<string> labels = new List<string> { "Ingreso", "Gasto", "Contrato" };
            List<decimal> valores = new List<decimal> { totalIngreso, totalGasto, totalContrato };

            string labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
            string valoresJson = Newtonsoft.Json.JsonConvert.SerializeObject(valores);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarGrafico",
                $"CargarGrafico({labelsJson}, {valoresJson});",
                true
            );
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
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < row.Cells.Count; i++)
                            dr[i] = row.Cells[i].Text.Replace("&nbsp;", "");
                        dt.Rows.Add(dr);
                    }
                }

                ws.Cells["A1"].LoadFromDataTable(dt, true);
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

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

                for (int col = 1; col <= colCount; col++)
                    dt.Columns.Add(ws.Cells[1, col].Text);

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

        protected void Button4_Click(object sender, EventArgs e) => ExportarExcel(gvContraEmp, "Contratos_Empleados");
        protected void Button2_Click(object sender, EventArgs e) => ExportarExcel(gvContraViaj, "Contratos_Viajes");
        protected void btnExportar_Click(object sender, EventArgs e) => ExportarExcel(gvGastos, "Gastos_Viaje");

        protected void Export_Click(object sender, EventArgs e)
        {
            pnlBonos.Visible = true;
            ExportarExcel(gvBonos, "Bonos_Empleados");
            pnlBonos.Visible = false;
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
            CargarContratosEmp();
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

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            pnlReportes.Visible = true;

            pnlReportesViajes.Visible = true;
            pnlReportesGastos.Visible = false;
            ActivarSubmenuReportes(btnReportesViajes);
            MtCargarReporteViajes();
        }

        protected void btnReportesViajes_Click(object sender, EventArgs e)
        {
            pnlReportesViajes.Visible = true;
            pnlReportesGastos.Visible = false;
            ActivarSubmenuReportes(btnReportesViajes);
            MtCargarReporteViajes();
        }

        protected void btnReportesGastos_Click(object sender, EventArgs e)
        {
            pnlReportesViajes.Visible = false;
            pnlReportesGastos.Visible = true;
            ActivarSubmenuReportes(btnReportesGastos);
            MtCargarReporteGastos();
        }

        private void ActivarSubmenuReportes(Button boton)
        {
            btnReportesViajes.CssClass = "nav-reporte-item";
            btnReportesGastos.CssClass = "nav-reporte-item";
            boton.CssClass = "nav-reporte-item active";
        }

        private void MtCargarReporteViajes()
        {
            try
            {
                var listaViajes = viajesL.MtViajesAdmin();

                if (listaViajes == null || listaViajes.Count == 0)
                {
                    lblMensajeReportesViajes.Text = "No se encontraron viajes registrados.";
                    lblMensajeReportesViajes.CssClass = "text-muted";
                    gvReportesViajes.DataSource = null;
                    gvReportesViajes.DataBind();
                    lblTotalViajesMostrados.Text = "0";

                    string scriptEmpty = @"
                        document.getElementById('totalViajes').innerText = '0';
                        document.getElementById('viajesPendientes').innerText = '0';
                        document.getElementById('viajesEnCurso').innerText = '0';
                        document.getElementById('viajesCompletados').innerText = '0';
                    ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "resetStatsViajesCont", scriptEmpty, true);
                    return;
                }

                int pendientes = 0, enCurso = 0, completados = 0;

                foreach (var viaje in listaViajes)
                {
                    if (viaje.estadoViaje != null)
                    {
                        switch (viaje.estadoViaje.ToLower())
                        {
                            case "pendiente": pendientes++; break;
                            case "en curso": enCurso++; break;
                            case "completado": completados++; break;
                        }
                    }
                }

                string script = $@"
                    document.getElementById('totalViajes').innerText = '{listaViajes.Count}';
                    document.getElementById('viajesPendientes').innerText = '{pendientes}';
                    document.getElementById('viajesEnCurso').innerText = '{enCurso}';
                    document.getElementById('viajesCompletados').innerText = '{completados}';
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "actualizarEstadisticasViajesCont", script, true);

                DataTable dt = new DataTable();
                dt.Columns.Add("idViaje", typeof(int));
                dt.Columns.Add("puntoPartida", typeof(string));
                dt.Columns.Add("destino", typeof(string));
                dt.Columns.Add("fechaInicio", typeof(string));
                dt.Columns.Add("estadoViaje", typeof(string));
                dt.Columns.Add("costo", typeof(string));
                dt.Columns.Add("Conductor", typeof(string));
                dt.Columns.Add("telefono", typeof(string));
                dt.Columns.Add("cliente", typeof(string));
                dt.Columns.Add("empresa", typeof(string));
                dt.Columns.Add("placa", typeof(string));
                dt.Columns.Add("modelo", typeof(string));

                foreach (var v in listaViajes)
                {
                    dt.Rows.Add(
                        v.idViaje,
                        v.puntoPartida ?? "",
                        v.destino ?? "",
                        v.fechaInicio ?? "",
                        v.estadoViaje ?? "Pendiente",
                        v.costo ?? "0",
                        v.nombreU ?? "Sin asignar",
                        v.telefonoU ?? "",
                        v.nombreC ?? "",
                        v.empresa ?? "",
                        v.placa ?? "",
                        v.modelo ?? ""
                    );
                }

                Session["TodosViajesCont"] = dt;
                gvReportesViajes.DataSource = dt;
                gvReportesViajes.DataBind();

                lblTotalViajesMostrados.Text = dt.Rows.Count.ToString();
                lblMensajeReportesViajes.Text = $"Se cargaron {dt.Rows.Count} viajes.";
                lblMensajeReportesViajes.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                lblMensajeReportesViajes.Text = $"Error al cargar viajes: {ex.Message}";
                lblMensajeReportesViajes.CssClass = "text-danger";
                gvReportesViajes.DataSource = null;
                gvReportesViajes.DataBind();
                lblTotalViajesMostrados.Text = "0";
            }
        }

        private void MtCargarReporteGastos()
        {
            try
            {
                listaGastosCompleta = viajesL.ReporteGastosAdmin();
                Session["TodosGastosCont"] = listaGastosCompleta;

                if (listaGastosCompleta == null || listaGastosCompleta.Count == 0)
                {
                    lblMensajeReportesGastos.Text = "No se encontraron gastos registrados.";
                    lblMensajeReportesGastos.CssClass = "text-muted";
                    gvReportesGastos.DataSource = null;
                    gvReportesGastos.DataBind();
                    lblTotalGastosMostrados.Text = "0";

                    string scriptEmpty = @"
                        document.getElementById('totalGastos').innerText = '0';
                        document.getElementById('gastosCombustible').innerText = '0';
                        document.getElementById('gastosMantenimiento').innerText = '0';
                        document.getElementById('gastosOtros').innerText = '0';
                        document.getElementById('montoTotalGastos').innerText = '$0.00';
                    ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "resetStatsGastosCont", scriptEmpty, true);
                    return;
                }

                CalcularEstadisticasGastos(listaGastosCompleta);
                MostrarGastosFiltrados(listaGastosCompleta);

                lblTotalGastosMostrados.Text = listaGastosCompleta.Count.ToString();
                lblMensajeReportesGastos.Text = $"Se cargaron {listaGastosCompleta.Count} gastos.";
                lblMensajeReportesGastos.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                lblMensajeReportesGastos.Text = $"Error al cargar gastos: {ex.Message}";
                lblMensajeReportesGastos.CssClass = "text-danger";
                gvReportesGastos.DataSource = null;
                gvReportesGastos.DataBind();
                lblTotalGastosMostrados.Text = "0";
            }
        }

        private void CalcularEstadisticasGastos(List<ClGastoM> gastos)
        {
            if (gastos == null || gastos.Count == 0)
            {
                string scriptEmpty = @"
                    document.getElementById('totalGastos').innerText = '0';
                    document.getElementById('gastosCombustible').innerText = '0';
                    document.getElementById('gastosMantenimiento').innerText = '0';
                    document.getElementById('gastosOtros').innerText = '0';
                    document.getElementById('montoTotalGastos').innerText = '$0.00';
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "resetStatsGastosCont2", scriptEmpty, true);
                return;
            }

            int total = gastos.Count;
            decimal montoTotal = gastos.Sum(g => g.monto);

            int combustible = gastos.Count(g => (g.tipoGasto ?? "").ToLower().Contains("combustible"));
            int mantenimiento = gastos.Count(g => (g.tipoGasto ?? "").ToLower().Contains("mantenimiento"));

            int otros = total - (combustible + mantenimiento);
            if (otros < 0) otros = 0;

            string script = $@"
                document.getElementById('totalGastos').innerText = '{total}';
                document.getElementById('gastosCombustible').innerText = '{combustible}';
                document.getElementById('gastosMantenimiento').innerText = '{mantenimiento}';
                document.getElementById('gastosOtros').innerText = '{otros}';
                document.getElementById('montoTotalGastos').innerText = '${montoTotal:N2}';
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "actualizarEstadisticasGastosCont", script, true);
        }

        private void MostrarGastosFiltrados(List<ClGastoM> gastos)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idGasto", typeof(int));
            dt.Columns.Add("tipoGasto", typeof(string));
            dt.Columns.Add("descripcionGasto", typeof(string));
            dt.Columns.Add("monto", typeof(decimal));
            dt.Columns.Add("fechaGasto", typeof(DateTime));
            dt.Columns.Add("nombreUsuario", typeof(string));
            dt.Columns.Add("imagenRecibo", typeof(string));
            dt.Columns.Add("placa", typeof(string));
            dt.Columns.Add("idViaje", typeof(int));

            foreach (var g in gastos)
            {
                dt.Rows.Add(
                    g.idGasto,
                    g.tipoGasto ?? "",
                    g.descripcionGasto ?? "",
                    g.monto,
                    g.fechaGasto,
                    g.nombreUsuario ?? "",
                    g.imagenRecibo ?? "",
                    g.placa ?? "",
                    g.idViaje
                );
            }

            gvReportesGastos.DataSource = dt;
            gvReportesGastos.DataBind();
            lblTotalGastosMostrados.Text = dt.Rows.Count.ToString();
        }

        protected void btnBuscarPlacaGastos_Click(object sender, EventArgs e)
        {
            string placa = txtBuscarPlacaGastos.Text.Trim();

            if (string.IsNullOrEmpty(placa))
            {
                lblMensajeReportesGastos.Text = "Por favor ingrese una placa para buscar.";
                lblMensajeReportesGastos.CssClass = "text-danger";
                return;
            }

            try
            {
                var todos = Session["TodosGastosCont"] as List<ClGastoM>;
                if (todos == null || todos.Count == 0)
                {
                    todos = viajesL.ReporteGastosAdmin();
                    Session["TodosGastosCont"] = todos;
                }

                var filtrados = todos
                    .Where(g => !string.IsNullOrEmpty(g.placa) && g.placa.ToLower().Contains(placa.ToLower()))
                    .ToList();

                if (filtrados.Count == 0)
                {
                    lblMensajeReportesGastos.Text = $"No se encontraron gastos para la placa: {placa}";
                    lblMensajeReportesGastos.CssClass = "text-muted";
                }
                else
                {
                    lblMensajeReportesGastos.Text = $"Se encontraron {filtrados.Count} gastos para la placa: {placa}";
                    lblMensajeReportesGastos.CssClass = "text-success";
                }

                CalcularEstadisticasGastos(filtrados);
                MostrarGastosFiltrados(filtrados);
            }
            catch (Exception ex)
            {
                lblMensajeReportesGastos.Text = $"Error al buscar gastos: {ex.Message}";
                lblMensajeReportesGastos.CssClass = "text-danger";
            }
        }

        protected void btnLimpiarFiltroPlaca_Click(object sender, EventArgs e)
        {
            txtBuscarPlacaGastos.Text = "";
            MtCargarReporteGastos();
        }

        public string GetEstadoIcon(string estado)
        {
            switch ((estado ?? "").ToLower())
            {
                case "pendiente": return "bi bi-clock";
                case "en curso": return "bi bi-arrow-right-circle";
                case "completado": return "bi bi-check-circle";
                case "cancelado": return "bi bi-x-circle";
                default: return "bi bi-question-circle";
            }
        }

        public string GetClaseTipoGasto(string tipo)
        {
            var t = (tipo ?? "").ToLower().Trim();

            if (t.Contains("combustible")) return "badge-combustible";
            if (t.Contains("mantenimiento")) return "badge-mantenimiento";

            return "badge-otros";
        }

        public string GetIconoTipoGasto(string tipo)
        {
            var t = (tipo ?? "").ToLower().Trim();

            if (t.Contains("combustible")) return "bi bi-fuel-pump";
            if (t.Contains("mantenimiento")) return "bi bi-tools";
            if (t.Contains("peaje")) return "bi bi-signpost";
            if (t.Contains("cargue")) return "bi bi-box-seam";
            if (t.Contains("descargue")) return "bi bi-box-arrow-down";
            if (t.Contains("lavada") || t.Contains("lavado")) return "bi bi-droplet";
            if (t.Contains("engrase")) return "bi bi-droplet-half";
            if (t.Contains("parqueo")) return "bi bi-p-square";
            if (t.Contains("4x1000")) return "bi bi-bank";
            if (t.Contains("banco")) return "bi bi-bank";

            return "bi bi-cash-stack";
        }

        public string MostrarBotonEvidencia(string rutaImagen)
        {
            if (!string.IsNullOrEmpty(rutaImagen) && rutaImagen != "")
            {
                string rutaCompleta = rutaImagen.StartsWith("~/") || rutaImagen.StartsWith("http")
                    ? rutaImagen
                    : "~/Vista/Imagenes/" + rutaImagen;

                string rutaParaCliente = ResolveUrl(rutaCompleta);

                return $@"
                  <button type='button' class='btn btn-sm btn-outline-primary'
                    onclick='mostrarImagen(""{rutaParaCliente.Replace("\"", "\\\"")}"")'
                    data-bs-toggle='modal' data-bs-target='#modalImagen'>
                    <i class='bi bi-receipt'></i> Ver
                  </button>";
            }
            return "<span class='text-muted'>Sin evidencia</span>";
        }

        protected void btnExportarReportesViajes_Click(object sender, EventArgs e)
        {
            ExportarExcel(gvReportesViajes, "Reporte_Viajes");
        }

        protected void btnExportarReportesGastos_Click(object sender, EventArgs e)
        {
            ExportarExcel(gvReportesGastos, "Reporte_Gastos");
        }

        protected void gvReportesGastos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    }
}
