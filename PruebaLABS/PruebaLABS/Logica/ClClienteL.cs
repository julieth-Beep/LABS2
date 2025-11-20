using PruebaLABS.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClClienteL
    {
        ClClienteD oContD = new ClClienteD();
        public DataTable ListaDatVehiculo()
        {
            return oContD.DatVehiculo();
        }

    }
}