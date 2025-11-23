using PruebaLABS.Datos;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PruebaLABS.Logica
{
    public class ClContadorL
    {
        ClContadorD oContD = new ClContadorD();
        public DataTable ListarGastosPorConductor()
        {
            return oContD.GastosporConductor();
        }
        public List<ClContratoM> MtListContra(string documento)
        {
            return oContD.MtListContraUsu(documento);
        }
        public string MtEditContrato(ClContratoM v)
        {
            return oContD.MtEditar(v);
        }
        public DataTable Bono()
        {
            return oContD.MtBonoConductor();
        }
        public DataTable Contratos()
        {
            return oContD.MtContraTotal();
        }
        public string Registrar(ClContratoM m)
        {
            return oContD.MtRegistrarContrato(m);
        }
    }
}