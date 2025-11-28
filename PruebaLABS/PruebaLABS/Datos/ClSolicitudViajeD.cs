using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
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
                string consultaAdmin = @"SELECT TOP 1 idCargo from cargo where idRol = 2";
                SqlCommand cmdAdmin = new SqlCommand(consultaAdmin, oConexion.MtAbrirConexion());
                object result = cmdAdmin.ExecuteScalar();
                int idAdministrador = result != null ? Convert.ToInt32(result) : 1;
                oConexion.MtCerrarConexion();

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
                cmd.Parameters.AddWithValue("@idAdministrador", idAdministrador);
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

        public DataTable MtObtenerViajesCliente(int idCliente)
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = @"select idViaje, puntoPartida, destino, fechaInicio, fechaFin, estadoViaje, costo, motivo, tipoCarga, observaciones from viaje where idCliente = @idCliente order by fechaInicio DESC, idViaje DESC";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener viajes del cliente: " + ex.Message);
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }
            return dt;
        }


        public DataTable MtObtenerTodasLasSolicitudes()
        {
            DataTable dt = new DataTable();
            string consulta = @"select v.idViaje, c.documento, c.nombre + ' ' + c.apellido as Cliente, c.empresa, v.puntoPartida, v.destino, v.fechaInicio, v.fechaFin, v.estadoViaje, v.costo, v.tipoCarga, v.motivo, v.observaciones from viaje v inner join cliente c on v.idCliente = c.idCliente order by v.fechaInicio DESC, v.idViaje DESC";

            SqlDataAdapter da = new SqlDataAdapter(consulta, oConexion.MtAbrirConexion());

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las solicitudes: " + ex.Message, ex);
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return dt;
        }

        public DataTable MtObtenerSolicitudesPorDocumento(string documento)
        {
            DataTable dt = new DataTable();
            string consulta = @"select v.idViaje, c.documento, c.nombre + ' ' + c.apellido as Cliente, c.empresa, v.puntoPartida, v.destino, v.fechaInicio, v.fechaFin, v.estadoViaje, v.costo, v.tipoCarga, v.motivo, v.observaciones from viaje v  inner join cliente c ON v.idCliente = c.idCliente where c.documento = @documento order by v.fechaInicio DESC";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@documento", documento);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener solicitudes por documento: " + ex.Message, ex);
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return dt;
        }

        public DataTable MtObtenerSolicitudesPorEstado(string estado)
        {
            DataTable dt = new DataTable();
            string consulta = @"select v.idViaje, c.documento, c.nombre + ' ' + c.apellido as Cliente, c.empresa, v.puntoPartida, v.destino, v.fechaInicio, v.fechaFin, v.estadoViaje, v.costo, v.tipoCarga, v.motivo, v.observaciones from viaje v  inner join cliente c on v.idCliente = c.idCliente where v.estadoViaje = @estado order by v.fechaInicio DESC";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@estado", estado);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener solicitudes por estado: " + ex.Message, ex);
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return dt;
        }

        public string MtActualizarSolicitud(int idViaje, string estado, string costo, string fechaLlegada, string observaciones)
        {
            string mensaje = "";
            string consulta = @"UPDATE viaje SET estadoViaje = @estado, costo = @costo, fechaFin = @fechaFin, observaciones = @observaciones where idViaje = @idViaje";

            try
            {
                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@costo", string.IsNullOrEmpty(costo) ? (object)DBNull.Value : decimal.Parse(costo));
                cmd.Parameters.AddWithValue("@fechaFin", string.IsNullOrEmpty(fechaLlegada) ? (object)DBNull.Value : fechaLlegada);
                cmd.Parameters.AddWithValue("@observaciones", observaciones ?? "");
                cmd.Parameters.AddWithValue("@idViaje", idViaje);

                int resultado = cmd.ExecuteNonQuery();
                mensaje = resultado > 0 ? "Solicitud actualizada correctamente." : "No se pudo actualizar la solicitud.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar solicitud: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }

        public string MtEliminarSolicitud(int idViaje)
        {
            string mensaje = "";
            string consulta = "delet from viaje where idViaje = @idViaje";

            try
            {
                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idViaje", idViaje);

                int resultado = cmd.ExecuteNonQuery();
                mensaje = resultado > 0 ? "Solicitud eliminada correctamente." : "No se pudo eliminar la solicitud.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar solicitud: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }
    }
}