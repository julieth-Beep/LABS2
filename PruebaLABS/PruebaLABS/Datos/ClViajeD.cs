using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PruebaLABS.Datos
{
    public class ClViajeD
    {
        ClConexion Oconex = new ClConexion();

        public List<ClViajeM> MtObtenerViajesC(int idConductor)
        {
            List<ClViajeM> listaViajes = new List<ClViajeM>();

            string consulta = @"
                SELECT v.idViaje, v.puntoPartida, v.destino, v.fechaInicio, v.fechaFin, v.estadoViaje,
                       v.distancia, v.costo, v.motivo, v.observaciones, v.tipoCarga,
                       veh.placa, veh.modelo, veh.capacidad, c.idCliente
                FROM viaje v
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
            string mensaje = "";
            try
            {
                string consulta = @"
                    UPDATE viaje
                    SET estadoViaje = @estado
                    WHERE idViaje = @idViaje";

                SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@estado", estado);

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
            List<ClGastoM> lista = new List<ClGastoM>();

            string consulta = @"
                SELECT g.idGasto, g.tipoGasto, g.descripcion, g.monto, g.fecha, g.imagenRecibo, v.idViaje
                FROM gasto g
                INNER JOIN viajeVehiculo vv ON g.idViajeVehiculo = vv.idViajeVehiculo
                INNER JOIN cargo car ON vv.idConductor = car.idCargo
                INNER JOIN usuario u ON car.idUsuario = u.idUsuario
                INNER JOIN viaje v ON vv.idViaje = v.idViaje
                WHERE u.idUsuario = @idConductor
                ORDER BY g.fecha DESC";

            SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
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
            Oconex.MtCerrarConexion();
            return lista;
        }

        public string MtInsertarGastoConImagen(ClGastoM gasto)
        {
            string mensaje = "";
            try
            {
                string consulta = @"
                    INSERT INTO gasto(idViajeVehiculo,tipoGasto,monto,descripcion,fecha,imagenRecibo)
                    VALUES (@idViajeVehiculo,@tipoGasto,@monto,@descripcion,@fecha,@imagenRecibo)";

                SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idViajeVehiculo", gasto.idViajeVehiculo);
                cmd.Parameters.AddWithValue("@tipoGasto", gasto.tipoGasto ?? "");
                cmd.Parameters.AddWithValue("@monto", gasto.monto);
                cmd.Parameters.AddWithValue("@descripcion", gasto.descripcionGasto ?? "");
                cmd.Parameters.AddWithValue("@fecha", gasto.fechaGasto);
                cmd.Parameters.AddWithValue("@imagenRecibo", gasto.imagenRecibo ?? "");

                cmd.ExecuteNonQuery();
                mensaje = "Gasto registrado correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar gasto: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
            return mensaje;
        }

        public int obteneridVeviculo(int idViaje, int idConductor)
        {
            int idViajeVehiculo = 0;
            try
            {
                string consulta = @"
                    SELECT vv.idViajeVehiculo
                    FROM viajeVehiculo vv
                    INNER JOIN cargo car ON vv.idConductor = car.idCargo
                    INNER JOIN usuario u ON car.idUsuario = u.idUsuario
                    WHERE vv.idViaje = @idViaje AND u.idUsuario = @idConductor";

                SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@idConductor", idConductor);

                object result = cmd.ExecuteScalar();
                if (result != null) idViajeVehiculo = Convert.ToInt32(result);
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

            string consulta = @"
                SELECT v.idViaje, v.puntoPartida, v.destino, v.fechaInicio, v.fechaFin, v.estadoViaje,
                       v.costo, v.distancia, v.tipoCarga, v.observaciones,
                       u.nombre AS Conductor, u.telefono,
                       ve.placa, ve.modelo, ve.capacidad,
                       v.idCliente, c.nombre AS cliente, c.empresa
                FROM viaje v
                INNER JOIN viajeVehiculo vv ON v.idViaje = vv.idViaje
                INNER JOIN cargo ca ON vv.idConductor = ca.idCargo
                INNER JOIN usuario u ON ca.idUsuario = u.idUsuario
                INNER JOIN vehiculo ve ON vv.idVehiculo = ve.idVehiculo
                INNER JOIN estadoVehiculo ev ON ve.idEstadoVehiculo = ev.idEstadoVehiculo
                INNER JOIN cliente c ON v.idCliente = c.idCliente
                INNER JOIN estadoCliente ec ON c.idEstado = ec.idEstadoCliente
                ORDER BY v.idViaje";

            SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    ClViajesAdminM viaje = new ClViajesAdminM();

                    viaje.idViaje = Convert.ToInt32(dr["idViaje"]);
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

                    viaje.idCliente = Convert.ToInt32(dr["idCliente"]);
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
                dr.Close();
                Oconex.MtCerrarConexion();
            }

            return listaViajes;
        }

        public List<ClGastoM> ReporteGastosAdmin()
        {
            List<ClGastoM> lista = new List<ClGastoM>();

            string consulta = @"
                SELECT g.idGasto, g.tipoGasto, g.descripcion, g.monto, g.fecha, g.imagenRecibo,
                       v.idViaje, u.nombre AS conductor, veh.placa
                FROM gasto g
                INNER JOIN viajeVehiculo vv ON g.idViajeVehiculo = vv.idViajeVehiculo
                INNER JOIN cargo car ON vv.idConductor = car.idCargo
                INNER JOIN usuario u ON car.idUsuario = u.idUsuario
                INNER JOIN viaje v ON vv.idViaje = v.idViaje
                INNER JOIN vehiculo veh ON vv.idVehiculo = veh.idVehiculo
                ORDER BY g.fecha DESC";

            SqlCommand cmd = new SqlCommand(consulta, Oconex.MtAbrirConexion());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ClGastoM r = new ClGastoM();

                r.idGasto = dr["idGasto"] != DBNull.Value ? Convert.ToInt32(dr["idGasto"]) : 0;
                r.tipoGasto = dr["tipoGasto"] != DBNull.Value ? dr["tipoGasto"].ToString() : "";
                r.descripcionGasto = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "";

                r.monto = dr["monto"] != DBNull.Value ? Convert.ToDecimal(dr["monto"]) : 0;
                r.fechaGasto = dr["fecha"] != DBNull.Value ? Convert.ToDateTime(dr["fecha"]) : DateTime.MinValue;

                r.imagenRecibo = dr["imagenRecibo"] != DBNull.Value ? dr["imagenRecibo"].ToString() : "";
                r.idViaje = dr["idViaje"] != DBNull.Value ? Convert.ToInt32(dr["idViaje"]) : 0;

                r.nombreUsuario = dr["conductor"] != DBNull.Value ? dr["conductor"].ToString() : "";
                r.placa = dr["placa"] != DBNull.Value ? dr["placa"].ToString() : "";

                lista.Add(r);
            }

            dr.Close();
            Oconex.MtCerrarConexion();
            return lista;
        }

        public string MtAsignarViaje(int idViaje, int idConductorCargo, int idVehiculo, string anticipo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spAsignarViaje", Oconex.MtAbrirConexion());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@idConductor", idConductorCargo);
                cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                cmd.Parameters.AddWithValue("@anticipo", string.IsNullOrWhiteSpace(anticipo) ? (object)DBNull.Value : anticipo);

                object r = cmd.ExecuteScalar();
                return r != null ? r.ToString() : " Viaje asignado.";
            }
            catch (Exception ex)
            {
                return " Error al asignar: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
        }

        public DataTable MtViajesNoAsignados()
        {
            string sql = @"
                SELECT v.idViaje, v.puntoPartida, v.destino
                FROM viaje v
                LEFT JOIN viajeVehiculo vv ON v.idViaje = vv.idViaje
                WHERE vv.idViaje IS NULL
                ORDER BY v.idViaje DESC";

            SqlDataAdapter da = new SqlDataAdapter(sql, Oconex.MtAbrirConexion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            Oconex.MtCerrarConexion();
            return dt;
        }

        public DataTable MtConductores()
        {
            string sql = @"
                SELECT c.idCargo, u.nombre + ' ' + u.apellido AS Conductor, u.telefono
                FROM cargo c
                INNER JOIN usuario u ON u.idUsuario = c.idUsuario
                WHERE c.idRol = 1";

            SqlDataAdapter da = new SqlDataAdapter(sql, Oconex.MtAbrirConexion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            Oconex.MtCerrarConexion();
            return dt;
        }

        public DataTable MtVehiculosDisponibles()
        {
            string sql = @"
                SELECT idVehiculo, placa + ' - ' + modelo AS vehiculo, modelo, capacidad
                FROM vehiculo
                WHERE idEstadoVehiculo = 1";

            SqlDataAdapter da = new SqlDataAdapter(sql, Oconex.MtAbrirConexion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            Oconex.MtCerrarConexion();
            return dt;
        }

        public string MtAsignarViajeSinSP(int idViaje, int idConductorCargo, int idVehiculo,
                                          string anticipo, string destino, string costo, string distancia)
        {
            SqlConnection cn = Oconex.MtAbrirConexion();
            SqlTransaction tx = cn.BeginTransaction();

            try
            {
                using (SqlCommand cmdVal = new SqlCommand("SELECT COUNT(1) FROM viajeVehiculo WHERE idViaje=@idViaje", cn, tx))
                {
                    cmdVal.Parameters.AddWithValue("@idViaje", idViaje);
                    int existe = Convert.ToInt32(cmdVal.ExecuteScalar());
                    if (existe > 0)
                    {
                        tx.Rollback();
                        return "❌ Este viaje ya está asignado.";
                    }
                }

                using (SqlCommand cmdVeh = new SqlCommand("SELECT idEstadoVehiculo FROM vehiculo WHERE idVehiculo=@idVehiculo", cn, tx))
                {
                    cmdVeh.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                    object estadoObj = cmdVeh.ExecuteScalar();
                    int estadoVeh = (estadoObj == null) ? 0 : Convert.ToInt32(estadoObj);

                    if (estadoVeh != 1)
                    {
                        tx.Rollback();
                        return "❌ El vehículo no está disponible.";
                    }
                }

                using (SqlCommand cmdUp = new SqlCommand(@"
                    UPDATE viaje
                    SET destino=@destino, costo=@costo, distancia=@distancia
                    WHERE idViaje=@idViaje", cn, tx))
                {
                    cmdUp.Parameters.AddWithValue("@idViaje", idViaje);
                    cmdUp.Parameters.AddWithValue("@destino", destino ?? "");
                    cmdUp.Parameters.AddWithValue("@costo", string.IsNullOrWhiteSpace(costo) ? "0" : costo);
                    cmdUp.Parameters.AddWithValue("@distancia", string.IsNullOrWhiteSpace(distancia) ? "0" : distancia);
                    cmdUp.ExecuteNonQuery();
                }

                using (SqlCommand cmdIns = new SqlCommand(@"
                    INSERT INTO viajeVehiculo (idViaje, idConductor, anticipo, idVehiculo)
                    VALUES (@idViaje, @idConductor, @anticipo, @idVehiculo)", cn, tx))
                {
                    cmdIns.Parameters.AddWithValue("@idViaje", idViaje);
                    cmdIns.Parameters.AddWithValue("@idConductor", idConductorCargo);
                    cmdIns.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                    cmdIns.Parameters.AddWithValue("@anticipo", string.IsNullOrWhiteSpace(anticipo) ? (object)DBNull.Value : anticipo);
                    cmdIns.ExecuteNonQuery();
                }

                tx.Commit();
                return "Viaje asignado correctamente.";
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { }
                return " Error al asignar: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
        }

        public string MtCrearViajeSinSP(int idCliente, string origen, string destino, string distancia,
                                        string costo, string tipoCarga, string motivo, string observaciones)
        {
            try
            {
                string sql = @"
                    INSERT INTO viaje
                    (idCliente, puntoPartida, destino, fechaInicio, fechaFin, estadoViaje, distancia, costo, motivo, observaciones, tipoCarga)
                    VALUES
                    (@idCliente, @origen, @destino, GETDATE(), NULL, 'Pendiente', @distancia, @costo, @motivo, @observaciones, @tipoCarga);

                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (SqlCommand cmd = new SqlCommand(sql, Oconex.MtAbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@origen", origen ?? "");
                    cmd.Parameters.AddWithValue("@destino", destino ?? "");
                    cmd.Parameters.AddWithValue("@distancia", string.IsNullOrWhiteSpace(distancia) ? "0" : distancia);
                    cmd.Parameters.AddWithValue("@costo", string.IsNullOrWhiteSpace(costo) ? "0" : costo);
                    cmd.Parameters.AddWithValue("@tipoCarga", string.IsNullOrWhiteSpace(tipoCarga) ? "General" : tipoCarga);
                    cmd.Parameters.AddWithValue("@motivo", motivo ?? "");
                    cmd.Parameters.AddWithValue("@observaciones", observaciones ?? "");

                    object r = cmd.ExecuteScalar();
                    int idNuevo = (r == null) ? 0 : Convert.ToInt32(r);

                    return idNuevo > 0 ? $"✅ Viaje creado correctamente. ID: {idNuevo}" : "❌ No se pudo crear el viaje.";
                }
            }
            catch (Exception ex)
            {
                return "❌ Error al crear viaje: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
        }

        public string MtEditarViajeBase(int idViaje, string origen, string destino, string distancia, string costo,
                                        string tipoCarga, string motivo, string observaciones)
        {
            try
            {
                string sql = @"
                    UPDATE viaje
                    SET puntoPartida=@origen,
                        destino=@destino,
                        distancia=@distancia,
                        costo=@costo,
                        tipoCarga=@tipoCarga,
                        motivo=@motivo,
                        observaciones=@observaciones
                    WHERE idViaje=@idViaje";

                using (SqlCommand cmd = new SqlCommand(sql, Oconex.MtAbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@idViaje", idViaje);
                    cmd.Parameters.AddWithValue("@origen", origen ?? "");
                    cmd.Parameters.AddWithValue("@destino", destino ?? "");
                    cmd.Parameters.AddWithValue("@distancia", string.IsNullOrWhiteSpace(distancia) ? "0" : distancia);
                    cmd.Parameters.AddWithValue("@costo", string.IsNullOrWhiteSpace(costo) ? "0" : costo);
                    cmd.Parameters.AddWithValue("@tipoCarga", string.IsNullOrWhiteSpace(tipoCarga) ? "General" : tipoCarga);
                    cmd.Parameters.AddWithValue("@motivo", motivo ?? "");
                    cmd.Parameters.AddWithValue("@observaciones", observaciones ?? "");

                    cmd.ExecuteNonQuery();
                    return " Viaje actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return " Error al editar viaje: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
        }

        public string MtEditarAsignacionViaje(int idViaje, int idConductorCargo, int idVehiculo, string anticipo)
        {
            SqlConnection cn = Oconex.MtAbrirConexion();
            SqlTransaction tx = cn.BeginTransaction();

            try
            {
                using (SqlCommand c1 = new SqlCommand("SELECT COUNT(1) FROM viaje WHERE idViaje=@id", cn, tx))
                {
                    c1.Parameters.AddWithValue("@id", idViaje);
                    int existeViaje = Convert.ToInt32(c1.ExecuteScalar());
                    if (existeViaje == 0)
                    {
                        tx.Rollback();
                        return "❌ El viaje no existe.";
                    }
                }

                using (SqlCommand c2 = new SqlCommand("SELECT idEstadoVehiculo FROM vehiculo WHERE idVehiculo=@idVeh", cn, tx))
                {
                    c2.Parameters.AddWithValue("@idVeh", idVehiculo);
                    object estadoObj = c2.ExecuteScalar();
                    int estadoVeh = (estadoObj == null) ? 0 : Convert.ToInt32(estadoObj);

                    if (estadoVeh != 1)
                    {
                        using (SqlCommand c3 = new SqlCommand("SELECT COUNT(1) FROM viajeVehiculo WHERE idViaje=@idViaje AND idVehiculo=@idVeh", cn, tx))
                        {
                            c3.Parameters.AddWithValue("@idViaje", idViaje);
                            c3.Parameters.AddWithValue("@idVeh", idVehiculo);
                            int ya = Convert.ToInt32(c3.ExecuteScalar());
                            if (ya == 0)
                            {
                                tx.Rollback();
                                return "❌ El vehículo no está disponible.";
                            }
                        }
                    }
                }

                int existeAsignacion = 0;
                using (SqlCommand c4 = new SqlCommand("SELECT COUNT(1) FROM viajeVehiculo WHERE idViaje=@idViaje", cn, tx))
                {
                    c4.Parameters.AddWithValue("@idViaje", idViaje);
                    existeAsignacion = Convert.ToInt32(c4.ExecuteScalar());
                }

                if (existeAsignacion > 0)
                {
                    using (SqlCommand up = new SqlCommand(@"
                        UPDATE viajeVehiculo
                        SET idConductor=@idConductor,
                            idVehiculo=@idVehiculo,
                            anticipo=@anticipo
                        WHERE idViaje=@idViaje", cn, tx))
                    {
                        up.Parameters.AddWithValue("@idViaje", idViaje);
                        up.Parameters.AddWithValue("@idConductor", idConductorCargo);
                        up.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                        up.Parameters.AddWithValue("@anticipo", string.IsNullOrWhiteSpace(anticipo) ? (object)DBNull.Value : anticipo);
                        up.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return "Asignación actualizada .";
                }
                else
                {
                    using (SqlCommand ins = new SqlCommand(@"
                        INSERT INTO viajeVehiculo (idViaje, idConductor, idVehiculo, anticipo)
                        VALUES (@idViaje, @idConductor, @idVehiculo, @anticipo)", cn, tx))
                    {
                        ins.Parameters.AddWithValue("@idViaje", idViaje);
                        ins.Parameters.AddWithValue("@idConductor", idConductorCargo);
                        ins.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                        ins.Parameters.AddWithValue("@anticipo", string.IsNullOrWhiteSpace(anticipo) ? (object)DBNull.Value : anticipo);
                        ins.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return " Asignación creada.";
                }
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { }
                return " Error al editar asignación: " + ex.Message;
            }
            finally
            {
                Oconex.MtCerrarConexion();
            }
        }
    }
}
