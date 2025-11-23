using PruebaLABS.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClContadorL
    {
        ClContadorD oContD = new ClContadorD();
        public DataTable ListarGastosPorConductor()
        {
            return oContD.GastosporConductor();
        }
    }
}