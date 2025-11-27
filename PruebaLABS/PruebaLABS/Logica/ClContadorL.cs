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
        ClSolicitudViajeL v = new ClSolicitudViajeL();
        public DataTable ListarGastosViaje(int id)
        {
            return oContD.GastosViaje(id);
        }
        public List<ClContratoM> MtListContra(string documento)
        {
            return oContD.MtListContraUsu(documento);
        }
        public string MtEditContrato(ClContratoM v)
        {
            return oContD.MtEditar(v);
        }
        public DataTable Bonos()
        {
            return oContD.MtBonos();
        }
        public DataTable ContratosEmp()
        {
            return oContD.MtContraEmp();
        }
        public DataTable ContratosViaje()
        {
            return oContD.MtContraViaj();
        }
        public string Registrar(ClContratoM m)
        {
            return oContD.MtRegistrarContrato(m);
        }
        public string EliminarContra(int v)
        {
            return oContD.MtEliminarContrato(v);
        }
        public string RegistrarGasto(ClGastoM g)
        {
            return oContD.MtRegistrarGasto(g);
        }


    }
}