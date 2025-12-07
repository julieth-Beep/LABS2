using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClEstadisticaM
    {
        public string IdMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public string PlacaVehiculo { get; set; }
        public string Conductor { get; set; }
    }
}