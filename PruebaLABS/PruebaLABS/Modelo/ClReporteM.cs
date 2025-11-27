using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClReporteM
    {
        public  int idReporte { get; set; }
        public string tipoReporte { get; set; }
        public string descripcion {  get; set; }
        public string  fechaGeneracion { get; set; }
        public int idViaje { get; set; }
        public int ViajeVehiculo { get; set; }
    }
}