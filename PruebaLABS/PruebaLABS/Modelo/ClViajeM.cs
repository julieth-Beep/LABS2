using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClViajeM:ClVehiculoM
    {
        public int idViaje { get; set; }
        public string puntoPartida { get; set; }
        public string destino { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string estadoViaje { get; set; }
        public string costo { get; set; }
        public string distancia { get; set; }
        public string motivo { get; set; }
        public string observaciones { get; set; }
        public string tipoCarga {  get; set; }
        public int idCliente { get; set; }
        public int idAdministrador { get; set; }

    }
}