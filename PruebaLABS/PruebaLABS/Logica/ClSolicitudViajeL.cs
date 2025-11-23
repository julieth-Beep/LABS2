using PruebaLABS.Datos;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClSolicitudViajeL
    {
        ClSolicitudViajeD datosViaje = new ClSolicitudViajeD();

        public string MtSolicitarViaje(string puntoPartida, string destino, string fechaInicio, string fechaFin,
            string tipoCarga, string motivo, string observaciones, int idCliente)
        {
            ClSolicitudViajeM viaje = new ClSolicitudViajeM
            {
                puntoPartida = puntoPartida,
                destino = destino,
                fechaInicio = fechaInicio,
                fechaFin = fechaFin,
                tipoCarga = tipoCarga,
                motivo = motivo, 
                observaciones = observaciones,
                idCliente = idCliente,
                costo = "0",
                distancia = "0"
            };

            return datosViaje.MtRegistrarViaje(viaje);
        }

        public DataTable MtObtenerViajesCliente(int idCliente)
        {
            return datosViaje.MtObtenerViajesCliente(idCliente);
        }
    }
}