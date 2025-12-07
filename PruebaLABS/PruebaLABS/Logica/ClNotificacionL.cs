using System;
using System.Data;
using System.Data.SqlClient;
using PruebaLABS.Datos;

namespace PruebaLABS.Logica
{
    public class ClNotificacionL
    {
        public DataTable MtListarNotificaciones(int idCliente, bool soloNuevas = false)
        {
            DataTable dt = new DataTable();

            try
            {
                string query = soloNuevas
                    ? @"SELECT TOP 10 idNotificacion, mensaje, 
                               CONVERT(varchar, fecha, 120) as fecha, 
                               leido 
                        FROM Notificacion 
                        WHERE idCliente = @idCliente AND leido = 0 
                        ORDER BY fecha DESC"
                    : @"SELECT idNotificacion, mensaje, 
                               CONVERT(varchar, fecha, 120) as fecha, 
                               leido 
                        FROM Notificacion 
                        WHERE idCliente = @idCliente 
                        ORDER BY fecha DESC";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conexion.MtCerrarConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar notificaciones: " + ex.Message);
            }

            return dt;
        }

        public string MtCrearNotificacion(int idCliente, string mensaje)
        {
            try
            {
                string query = @"INSERT INTO Notificacion (idCliente, mensaje, leido, fecha) 
                                VALUES (@idCliente, @mensaje, 0, GETDATE())";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@mensaje", mensaje);
                cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();

                return "Notificación creada exitosamente";
            }
            catch (Exception ex)
            {
                return "Error al crear notificación: " + ex.Message;
            }
        }

        public string MtMarcarComoLeidas(int idCliente)
        {
            try
            {
                string query = @"UPDATE Notificacion 
                                SET leido = 1 
                                WHERE idCliente = @idCliente AND leido = 0";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                int afectadas = cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();
                return $"Se marcaron {afectadas} notificaciones como leídas";
            }
            catch (Exception ex)
            {
                return "Error al marcar notificaciones: " + ex.Message;
            }
        }

        public string MtEliminarNotificacionesLeidas(int idCliente)
        {
            try
            {
                string query = @"DELETE FROM Notificacion 
                                WHERE idCliente = @idCliente AND leido = 1";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                int eliminadas = cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();
                return $"Se eliminaron {eliminadas} notificaciones leídas";
            }
            catch (Exception ex)
            {
                return "Error al eliminar notificaciones: " + ex.Message;
            }
        }

        public string MtEliminarTodasNotificaciones(int idCliente)
        {
            try
            {
                string query = @"DELETE FROM Notificacion WHERE idCliente = @idCliente";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                int eliminadas = cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();
                return $"Se eliminaron {eliminadas} notificaciones";
            }
            catch (Exception ex)
            {
                return "Error al eliminar notificaciones: " + ex.Message;
            }
        }

        public int MtContarNotificacionesNuevas(int idCliente)
        {
            try
            {
                string query = @"SELECT COUNT(*) as total 
                                FROM Notificacion 
                                WHERE idCliente = @idCliente AND leido = 0";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                object result = cmd.ExecuteScalar();
                conexion.MtCerrarConexion();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int MtContarTotalNotificaciones(int idCliente)
        {
            try
            {
                string query = @"SELECT COUNT(*) as total 
                                FROM Notificacion 
                                WHERE idCliente = @idCliente";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                object result = cmd.ExecuteScalar();
                conexion.MtCerrarConexion();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string MtMarcarNotificacionLeida(int idNotificacion)
        {
            try
            {
                string query = @"UPDATE Notificacion 
                                SET leido = 1 
                                WHERE idNotificacion = @idNotificacion";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idNotificacion", idNotificacion);
                int afectadas = cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();
                return afectadas > 0 ? "Notificación marcada como leída" : "Notificación no encontrada";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string MtEliminarNotificacion(int idNotificacion)
        {
            try
            {
                string query = @"DELETE FROM Notificacion WHERE idNotificacion = @idNotificacion";

                ClConexion conexion = new ClConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idNotificacion", idNotificacion);
                int eliminadas = cmd.ExecuteNonQuery();
                conexion.MtCerrarConexion();
                return eliminadas > 0 ? "Notificación eliminada" : "Notificación no encontrada";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}