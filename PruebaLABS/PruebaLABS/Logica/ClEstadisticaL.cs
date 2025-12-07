using PruebaLABS.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClEstadisticaL
    {
        private ClEstadisticaD estadistica = new ClEstadisticaD();
        public DataTable ObtenerEstadistica()
        {
            return estadistica.ObtenerEstadistica();
        }
    }
}