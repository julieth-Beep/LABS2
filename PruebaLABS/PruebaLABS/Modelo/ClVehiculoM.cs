using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClVehiculoM
    {
        public int idVehiculo { get; set; }
        public string placa { get; set; }
        public string modelo { get; set; }
        public string capacidad { get; set; }

        public string estado { get; set; }  
        public int idEstadoVehiculo { get; set; } 
    }


}
