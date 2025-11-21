using PruebaLABS.Datos;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClViajeL
    {
        ClViajeD datosViaje = new ClViajeD();
        public List<ClViajeM> MtViajesConductor(int idConductor)
        {
            return datosViaje.MtObtenerViajesC(idConductor);

        }
        public string MtCambiarEstado(int idViaje, string estado)
        {
            return datosViaje.MtCambiarEstadoViaje(idViaje,estado);
        }
    }
}