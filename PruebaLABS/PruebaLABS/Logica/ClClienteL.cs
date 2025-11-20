using PruebaLABS.Datos;
using PruebaLABS.Modelo;
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
        public ClClienteM MtLoginCliente(string user,string pass)
        {
            return oContD.MtLoginCliente(user, pass);

        }

    }

    
}