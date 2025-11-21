using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClViajeD
    {
        public List<ClViajeM> MtObtenerViajesC(int idConductor)
        {
            List<ClViajeM> listaViajes = new List<ClViajeM>();
            ClConexion Oconex = new ClConexion();

            string consulta = @"
           select  v.idViaje,v.puntoPartida ,v.destino,v.fechaInicio,v.fechaFin ,v.estadoViaje , v.distancia,v.costo,veh.placa,veh.modelo,veh.capacidad,c.idCliente from viaje v
           INNER JOIN viajeVehiculo vv ON v.idViaje = vv.idViaje
           INNER JOIN cargo car ON vv.idConductor = car.idCargo
           INNER JOIN usuario u ON car.idUsuario = u.idUsuario
           INNER JOIN vehiculo veh ON vv.idVehiculo = veh.idVehiculo
           INNER JOIN cliente c ON v.idCliente = c.idCliente
           WHERE u.idUsuario = @idConductor
           ORDER BY v.fechaInicio DESC";

            SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idConductor", idConductor);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ClViajeM viaje = new ClViajeM();

                viaje.idViaje = dr.GetInt32(dr.GetOrdinal("idViaje"));
                viaje.puntoPartida = dr["puntoPartida"].ToString();
                viaje.destino = dr["destino"].ToString();
                viaje.fechaInicio = dr["fechaInicio"].ToString();
                viaje.fechaFin = dr["fechaFin"].ToString();
                viaje.distancia = dr["distancia"].ToString();
                viaje.costo = dr["costo"].ToString();
                viaje.placa = dr["placa"].ToString();
                viaje.modelo = dr["modelo"].ToString();
                viaje.capacidad = dr["capacidad"].ToString();
                viaje.idCliente = dr.GetInt32(dr.GetOrdinal("idCliente"));
                viaje.estadoViaje = dr["estadoViaje"].ToString();

                listaViajes.Add(viaje);
            }

            dr.Close();
            Oconex.MtCerrarConexion();
            return listaViajes;
        }
        public string MtCambiarEstadoViaje(int idViaje, string estado)
        {
            ClConexion Oconex = new ClConexion();
            string mensaje = "";

            try
            {
                string consulta = @"
                    UPDATE viaje 
                    SET estadoViaje = @idEstado 
                    WHERE idViaje = @idViaje";

                SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@idEstado", estado);

                cmd.ExecuteNonQuery();
                mensaje = "Estado actualizado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }

            return mensaje;
        }
    }
}