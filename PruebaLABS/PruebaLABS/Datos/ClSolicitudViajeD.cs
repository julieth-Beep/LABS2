using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClSolicitudViajeD
    {
        ClConexion oConexion = new ClConexion();

        public string MtRegistrarViaje(ClSolicitudViajeM viaje)
        {
            string mensaje = "";

            try
            {
                string consulta = @"insert into viaje (puntoPartida, destino, fechaInicio, fechaFin, estadoViaje, costo, distancia, tipoCarga, idAdministrador, idCliente, motivo, observaciones) values (@puntoPartida, @destino, @fechaInicio, @fechaFin, @estadoViaje, @costo, @distancia, @tipoCarga, @idAdministrador, @idCliente, @motivo, @observaciones)";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@puntoPartida", viaje.puntoPartida);
                cmd.Parameters.AddWithValue("@destino", viaje.destino);
                cmd.Parameters.AddWithValue("@fechaInicio", viaje.fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", viaje.fechaFin ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@estadoViaje", "Pendiente");
                cmd.Parameters.AddWithValue("@costo", viaje.costo ?? "0");
                cmd.Parameters.AddWithValue("@distancia", viaje.distancia ?? "0");
                cmd.Parameters.AddWithValue("@tipoCarga", viaje.tipoCarga ?? "");
                cmd.Parameters.AddWithValue("@idAdministrador", "");
                cmd.Parameters.AddWithValue("@idCliente", viaje.idCliente);
                cmd.Parameters.AddWithValue("@motivo", viaje.motivo ?? "");
                cmd.Parameters.AddWithValue("@observaciones", viaje.observaciones ?? "");

                int resultado = cmd.ExecuteNonQuery();

                if (resultado > 0)
                {
                    mensaje = "Viaje solicitado exitosamente. Será revisado por un administrador.";
                }
                else
                {
                    mensaje = "Error al solicitar el viaje.";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el viaje: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }
            return mensaje;
        }
    }
}