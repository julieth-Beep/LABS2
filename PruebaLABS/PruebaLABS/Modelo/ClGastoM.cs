using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClGastoM:ClReporteM
    {
        public int idGasto { get; set; }
        public int idViajeVehiculo { get; set; }
        public string tipoGasto { get; set; }
        public decimal monto { get; set; }
        public string descripcionGasto { get; set; }
        public DateTime fechaGasto { get; set; }
        public string imagenRecibo { get; set; }

    }
}