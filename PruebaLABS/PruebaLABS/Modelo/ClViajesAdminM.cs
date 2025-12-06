using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClViajesAdminM
    {
        public int idCliente { get; set; }
        public string documentoC { get; set; }
        public string nombreC { get; set; }
        public string apellidoC { get; set; }
        public string empresa { get; set; }
        public int idViaje { get; set; }
        public string puntoPartida { get; set; }
        public string destino { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string estadoViaje { get; set; }
        public string costo { get; set; }
        public string distancia { get; set; }
        public string observaciones { get; set; }
        public string tipoCarga { get; set; }
        public int idVehiculo { get; set; }
        public string placa { get; set; }
        public string modelo { get; set; }
        public string capacidad { get; set; }
        public int idUsuario { get; set; }
        public string documentoU { get; set; }
        public string nombreU { get; set; }
        public string apellidoU { get; set; }
        public string telefonoU { get; set; }

    }
}