using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PruebaLABS.Modelo;

namespace PruebaLABS.Datos
{
    public class ClVehiculoD
    {
        ClConexion oConexion = new ClConexion();

     
        public List<ClVehiculoM> MtListarVehiculos()
        {
            List<ClVehiculoM> lista = new List<ClVehiculoM>();

            string consulta = @"
                SELECT 
                    v.idVehiculo,
                    v.placa,
                    v.modelo,
                    v.capacidad,
                    e.estado AS Estado
                FROM vehiculo v
                INNER JOIN estadoVehiculo e
                    ON v.idEstadoVehiculo = e.idEstadoVehiculo";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            SqlDataReader tbl = cmd.ExecuteReader();

            while (tbl.Read())
            {
                ClVehiculoM v = new ClVehiculoM();
                v.idVehiculo = tbl.GetInt32(tbl.GetOrdinal("idVehiculo"));
                v.placa = tbl["placa"].ToString();
                v.modelo = tbl["modelo"].ToString();
                v.capacidad = tbl["capacidad"].ToString();
                v.estado = tbl["Estado"].ToString(); 

                lista.Add(v);
            }

            tbl.Close();
            oConexion.MtCerrarConexion();
            return lista;
        }

      
        public ClVehiculoM MtBuscarPorPlaca(string placa)
        {
            ClVehiculoM v = null;

            string consulta = $@"
                SELECT 
                    v.idVehiculo,
                    v.placa,
                    v.modelo,
                    v.capacidad,
                    e.estado AS Estado
                FROM vehiculo v
                INNER JOIN estadoVehiculo e
                    ON v.idEstadoVehiculo = e.idEstadoVehiculo
                WHERE v.placa = '{placa}'";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            SqlDataReader tbl = cmd.ExecuteReader();

            if (tbl.Read())
            {
                v = new ClVehiculoM();
                v.idVehiculo = tbl.GetInt32(tbl.GetOrdinal("idVehiculo"));
                v.placa = tbl["placa"].ToString();
                v.modelo = tbl["modelo"].ToString();
                v.capacidad = tbl["capacidad"].ToString();
                v.estado = tbl["Estado"].ToString();
            }

            tbl.Close();
            oConexion.MtCerrarConexion();
            return v;
        }

        public List<ClVehiculoM> MtVehiculosDisponibles()
        {
            List<ClVehiculoM> lista = new List<ClVehiculoM>();

            string consulta = @"
                SELECT 
                    v.idVehiculo,
                    v.placa,
                    v.modelo,
                    v.capacidad,
                    e.estado AS Estado
                FROM vehiculo v
                INNER JOIN estadoVehiculo e
                    ON v.idEstadoVehiculo = e.idEstadoVehiculo
                WHERE v.idEstadoVehiculo = 1";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            SqlDataReader tbl = cmd.ExecuteReader();

            while (tbl.Read())
            {
                ClVehiculoM v = new ClVehiculoM();
                v.idVehiculo = tbl.GetInt32(tbl.GetOrdinal("idVehiculo"));
                v.placa = tbl["placa"].ToString();
                v.modelo = tbl["modelo"].ToString();
                v.capacidad = tbl["capacidad"].ToString();
                v.estado = tbl["Estado"].ToString();

                lista.Add(v);
            }

            tbl.Close();
            oConexion.MtCerrarConexion();
            return lista;
        }

        public string MtEditarVehiculo(ClVehiculoM v)
        {
            string mensaje = "";

            try
            {
                string consulta = @"
                    UPDATE vehiculo SET 
                        placa = @placa,
                        modelo = @modelo,
                        capacidad = @capacidad,
                        idEstadoVehiculo = @idEstado
                    WHERE idVehiculo = @idVehiculo";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@placa", v.placa);
                cmd.Parameters.AddWithValue("@modelo", v.modelo);
                cmd.Parameters.AddWithValue("@capacidad", v.capacidad);
                cmd.Parameters.AddWithValue("@idEstado", v.idEstadoVehiculo);
                cmd.Parameters.AddWithValue("@idVehiculo", v.idVehiculo);

                cmd.ExecuteNonQuery();
                mensaje = "Vehículo actualizado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }

      
        public string MtCambiarEstado(int idVehiculo, int idEstado)
        {
            string mensaje = "";

            try
            {
                string consulta = @"
                    UPDATE vehiculo 
                    SET idEstadoVehiculo = @idEstado 
                    WHERE idVehiculo = @idVehiculo";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                cmd.Parameters.AddWithValue("@idEstado", idEstado);

                cmd.ExecuteNonQuery();
                mensaje = "Estado actualizado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }
   
        public string MtEliminarVehiculo(int idVehiculo)
        {
            string mensaje = "";
            try
            {
                string consulta = "DELETE FROM vehiculo WHERE idVehiculo = @idVehiculo";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);

                int filas = cmd.ExecuteNonQuery();

                mensaje = filas > 0 ? "Vehículo eliminado correctamente" : "No se encontró el vehículo";
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar vehículo: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }

      
        public string MtAgregarVehiculo(ClVehiculoM v)
        {
            string mensaje = "";

            try
            {
                string consulta = @"INSERT INTO vehiculo (placa, modelo, capacidad, idEstadoVehiculo)
                            VALUES (@placa, @modelo, @capacidad, @idEstadoVehiculo)";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@placa", v.placa);
                cmd.Parameters.AddWithValue("@modelo", v.modelo);
                cmd.Parameters.AddWithValue("@capacidad", v.capacidad);
                cmd.Parameters.AddWithValue("@idEstadoVehiculo", v.idEstadoVehiculo);

                cmd.ExecuteNonQuery();
                mensaje = "Vehículo registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }


    }
}
