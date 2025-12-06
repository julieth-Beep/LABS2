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
           select  v.idViaje,v.puntoPartida ,v.destino,v.fechaInicio,v.fechaFin ,v.estadoViaje , v.distancia,v.costo,v.motivo,v.observaciones,v.tipoCarga,veh.placa,veh.modelo,veh.capacidad,c.idCliente from viaje v
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
                viaje.tipoCarga = dr["tipoCarga"].ToString();
                viaje.motivo = dr["motivo"].ToString();
                viaje.observaciones = dr["observaciones"].ToString();
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

        public List<ClGastoM> ReporteGastosConductor(int idConductor)
        {
            ClConexion oConex = new ClConexion();
            List<ClGastoM> lista = new List<ClGastoM>();
            string consulta = @"select g.idGasto,g.tipoGasto, g.descripcion, g.monto, g.fecha ,g.imagenRecibo,v.idViaje from gasto g inner join viajeVehiculo vv on g.idViajeVehiculo = vv.idViajeVehiculo inner join cargo car ON vv.idConductor = car.idCargo inner join usuario u ON car.idUsuario = u.idUsuario inner join viaje v ON vv.idViaje = v.idViaje where u.idUsuario = @idConductor ORDER BY g.fecha DESC";


            SqlCommand cmd = new SqlCommand(consulta, oConex.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idConductor", idConductor);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ClGastoM reporte = new ClGastoM();
                reporte.idGasto = dr.GetInt32(dr.GetOrdinal("idGasto"));
                reporte.tipoGasto = dr["tipoGasto"].ToString();
                reporte.descripcionGasto = dr["descripcion"].ToString();
                reporte.monto = dr["monto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["monto"]);
                reporte.fechaGasto = dr["fecha"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["fecha"]);
                reporte.imagenRecibo = dr["imagenRecibo"] != DBNull.Value ? dr["imagenRecibo"].ToString() : "";
                reporte.idViaje = dr.GetInt32(dr.GetOrdinal("idViaje"));


                lista.Add(reporte);
            }
            dr.Close();
            oConex.MtCerrarConexion();
            return lista;
        }


        public string MtInsertarGastoConImagen(ClGastoM gasto)
        {
            ClConexion Oconex = new ClConexion();
            string mensaje = "";

            try
            {
                string consulta = @"INSERT INTO gasto(idViajeVehiculo,tipoGasto,monto,descripcion,fecha,imagenRecibo) 
                           VALUES (@idViajeVehiculo,@tipoGasto,@monto,@descripcion,@fecha,@imagenRecibo)";
                SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idViajeVehiculo", gasto.idViajeVehiculo);
                cmd.Parameters.AddWithValue("@tipoGasto", gasto.tipoGasto);
                cmd.Parameters.AddWithValue("@monto", gasto.monto);
                cmd.Parameters.AddWithValue("@descripcion", gasto.descripcionGasto ?? "");
                cmd.Parameters.AddWithValue("@fecha", gasto.fechaGasto);
                cmd.Parameters.AddWithValue("@imagenRecibo", gasto.imagenRecibo ?? "");

                cmd.ExecuteNonQuery();
                mensaje = "Gasto registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar gasto:" + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
            return mensaje;
        }


        public int obteneridVeviculo(int idViaje, int idConductor)
        {
            ClConexion Oconex = new ClConexion();
            int idViajeVehiculo = 0;


            try
            {
                string consulta = @"select vv.idViajeVehiculo from viajeVehiculo vv inner join cargo car on vv.idConductor=car.idCargo inner join usuario u on car.idUsuario=u.idUsuario where vv.idViaje=@idViaje and u.idUsuario=@idConductor";
                SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@idConductor", idConductor);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    idViajeVehiculo = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener idViajeVehiculo: " + ex.Message);
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }

            return idViajeVehiculo;
        }
        public List<ClViajesAdminM> MtObtenerViajesAdmin()
        {
            List<ClViajesAdminM> listaViajes = new List<ClViajesAdminM>();
            ClConexion Oconex = new ClConexion();

            string consulta = " select v.idViaje,v.puntoPartida,v.destino,v.fechaInicio,v.fechaFin,v.estadoViaje,v.costo,v.distancia,v.tipoCarga,v.observaciones,u.nombre AS Conductor, u.telefono ,ve.placa , ve.modelo, ve.capacidad, v.idCliente,c.nombre AS cliente, c.empresa FROM viaje v INNER JOIN viajeVehiculo vv ON v.idViaje = vv.idViaje INNER JOIN cargo ca ON vv.idConductor = ca.idCargo INNER JOIN usuario u ON ca.idUsuario = u.idUsuario INNER JOIN vehiculo ve ON vv.idVehiculo = ve.idVehiculo INNER JOIN estadoVehiculo ev ON ve.idEstadoVehiculo = ev.idEstadoVehiculo  INNER JOIN cliente c ON v.idCliente = c.idCliente  INNER JOIN estadoCliente ec ON c.idEstado = ec.idEstadoCliente ORDER BY v.idViaje";

            SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());

            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    ClViajesAdminM viaje = new ClViajesAdminM();

                    viaje.idViaje = dr.GetInt32(dr.GetOrdinal("idViaje"));
                    viaje.puntoPartida = dr["puntoPartida"].ToString();
                    viaje.destino = dr["destino"].ToString();
                    viaje.fechaInicio = dr["fechaInicio"].ToString();
                    viaje.fechaFin = dr["fechaFin"].ToString();
                    viaje.estadoViaje = dr["estadoViaje"].ToString();
                    viaje.distancia = dr["distancia"].ToString();
                    viaje.costo = dr["costo"].ToString();
                    viaje.tipoCarga = dr["tipoCarga"].ToString();
                    viaje.observaciones = dr["observaciones"].ToString();
                    viaje.nombreU = dr["Conductor"].ToString();
                    viaje.telefonoU = dr["telefono"].ToString();
                    viaje.placa = dr["placa"].ToString();
                    viaje.modelo = dr["modelo"].ToString();
                    viaje.capacidad = dr["capacidad"].ToString();
                    viaje.idCliente = dr.GetInt32(dr.GetOrdinal("idCliente"));
                    viaje.nombreC = dr["cliente"].ToString();
                    viaje.empresa = dr["empresa"].ToString();

                    listaViajes.Add(viaje);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener viajes: " + ex.Message);
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }

            return listaViajes;
        }
        // En la clase de datos
        public List<ClGastoM> ReporteGastosAdmin()
        {
            ClConexion oConex = new ClConexion();
            List<ClGastoM> lista = new List<ClGastoM>();
            string consulta = @"select g.idGasto,g.tipoGasto, g.descripcion, g.monto, g.fecha ,g.imagenRecibo,v.idViaje,u.nombre as conductor ,veh.placa
                        from gasto g 
                        inner join viajeVehiculo vv on g.idViajeVehiculo = vv.idViajeVehiculo 
                        inner join cargo car ON vv.idConductor = car.idCargo 
                        inner join usuario u ON car.idUsuario = u.idUsuario 
                        inner join viaje v ON vv.idViaje = v.idViaje  
                        INNER JOIN vehiculo veh ON vv.idVehiculo = veh.idVehiculo 
                        ORDER BY g.fecha DESC";

            SqlCommand cmd = new SqlCommand(consulta, oConex.MtAbrirConexion());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                try
                {
                    ClGastoM reporte = new ClGastoM();
                    reporte.idGasto = dr.GetInt32(dr.GetOrdinal("idGasto"));
                    reporte.tipoGasto = dr["tipoGasto"] != DBNull.Value ? dr["tipoGasto"].ToString() : "";
                    reporte.descripcionGasto = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "";

                    // Manejo de monto (varchar a decimal)
                    string montoStr = dr["monto"] != DBNull.Value ? dr["monto"].ToString() : "0";
                    decimal monto;
                    if (decimal.TryParse(montoStr, out monto))
                    {
                        reporte.monto = monto;
                    }
                    else
                    {
                        reporte.monto = 0;
                    }

                    // Manejo de fecha (varchar a DateTime)
                    string fechaStr = dr["fecha"] != DBNull.Value ? dr["fecha"].ToString() : "";
                    DateTime fecha;
                    if (DateTime.TryParse(fechaStr, out fecha))
                    {
                        reporte.fechaGasto = fecha;
                    }
                    else
                    {
                        reporte.fechaGasto = DateTime.MinValue;
                    }

                    reporte.imagenRecibo = dr["imagenRecibo"] != DBNull.Value ? dr["imagenRecibo"].ToString() : "";
                    reporte.nombreUsuario = dr["conductor"] != DBNull.Value ? dr["conductor"].ToString() : "";
                    reporte.placa = dr["placa"] != DBNull.Value ? dr["placa"].ToString() : "";
                    reporte.idViaje = dr["idViaje"] != DBNull.Value ? Convert.ToInt32(dr["idViaje"]) : 0;

                    lista.Add(reporte);
                }
                catch (Exception ex)
                {
                    // Log del error pero continúa con los demás registros
                    Console.WriteLine($"Error procesando registro: {ex.Message}");
                }
            }
            dr.Close();
            oConex.MtCerrarConexion();
            return lista;
        }



    }



}
