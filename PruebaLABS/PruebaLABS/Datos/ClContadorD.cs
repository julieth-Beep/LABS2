using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PruebaLABS.Modelo;

namespace PruebaLABS.Datos
{
    public class ClContadorD
    {
        ClConexion oConexion = new ClConexion();
        public DataTable GastosViaje(int idViaje)
        {
            DataTable dt = new DataTable();

            string consulta = @"SELECT g.idGasto,g.tipoGasto,g.monto,g.descripcion,g.fecha,v.idViaje,(SELECT SUM(g2.monto) FROM gasto g2
            INNER JOIN viajeVehiculo vv2 ON vv2.idViajeVehiculo = g2.idViajeVehiculo WHERE vv2.idViaje = v.idViaje) AS totalGastosDelViaje 
            FROM gasto g INNER JOIN viajeVehiculo v ON v.idViajeVehiculo = g.idViajeVehiculo WHERE v.idViaje = @idViaje;";


            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idViaje", idViaje);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener gastos " + ex.Message, ex);
            }

            return dt;
        }
        public List<ClContratoM> MtListContraUsu(string documento)
        {
            List<ClContratoM> lista = new List<ClContratoM>();

            string consulta = @"select u.idUsuario,u.documento,u.nombre, u.apellido,c.idContrato,c.fecha,c.salario,c.tipo,c.bono from usuario u
            left join contrato c on u.idUsuario = c.idUsuario where u.documento = @documento;";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@documento", documento);
            SqlDataReader tbl = cmd.ExecuteReader();

            while (tbl.Read())
            {
                ClContratoM v = new ClContratoM();
                v.idUsuario = Convert.ToInt32(tbl["idUsuario"]);
                v.documento = tbl["documento"].ToString();
                v.nombre = tbl["nombre"].ToString();
                v.apellido = tbl["apellido"].ToString();

                v.idContrato = tbl["idContrato"] == DBNull.Value ? 0 : Convert.ToInt32(tbl["idContrato"]);
                v.fecha = tbl["fecha"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(tbl["fecha"]);
                v.salario = tbl["salario"] == DBNull.Value ? 0 : Convert.ToDecimal(tbl["salario"]);
                v.tipo = tbl["tipo"] == DBNull.Value ? "Sin contrato" : tbl["tipo"].ToString();
                v.bono = tbl["bono"] == DBNull.Value ? 0 : Convert.ToDecimal(tbl["bono"]);

                lista.Add(v);
            }

            tbl.Close();
            oConexion.MtCerrarConexion();
            return lista;
        }

        public string MtEditar(ClContratoM c)
        {
            string mensaje = "";
            string consulta = @"update contrato set fecha = @fecha, salario = @salario, tipo = @tipo, bono = @bono WHERE idContrato = @idContrato";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@fecha", c.fecha);
            cmd.Parameters.AddWithValue("@salario", c.salario);
            cmd.Parameters.AddWithValue("@tipo", c.tipo);
            cmd.Parameters.AddWithValue("@bono", c.bono);
            cmd.Parameters.AddWithValue("@idContrato", c.idContrato);

            cmd.ExecuteNonQuery();
            mensaje = "Contrato actualizado correctamente";
            oConexion.MtCerrarConexion();

            return mensaje;

        }
        public DataTable MtBonos()
        {
            DataTable dt = new DataTable();

            string consulta = @"select u.idUsuario,u.nombre, u.apellido,r.nombre, c.bono from usuario u join contrato c on c.idUsuario=u.idUsuario join cargo cr on cr.idUsuario=u.idUsuario join rol r on cr.idRol=r.idRol;";

            SqlDataAdapter da = new SqlDataAdapter(consulta, oConexion.MtAbrirConexion());

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener bonos " + ex.Message, ex);
            }
            oConexion.MtCerrarConexion();

            return dt;

        }
        public DataTable MtContraEmp()
        {
            DataTable dt = new DataTable();

            string consulta = @"select c.idContrato, r.nombre, u.documento, u.nombre, u.apellido, c.fecha, c.salario, c.tipo from usuario u join contrato c on c.idUsuario=u.idUsuario 
            join cargo cr on cr.idUsuario=u.idUsuario join rol r on cr.idRol=r.idRol;";

            SqlDataAdapter da = new SqlDataAdapter(consulta, oConexion.MtAbrirConexion());

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener contratos: " + ex.Message, ex);
            }
            oConexion.MtCerrarConexion();

            return dt;
        }
        public DataTable MtContraViaj()
        {
            DataTable dt = new DataTable();

            string consulta = @"select c.idCliente,c.nombre,c.apellido,c.empresa,e.estado,v.* from cliente c join estadoCliente e on c.idEstado=e.idEstadoCliente join viaje v on c.idCliente=v.idCliente;";

            SqlDataAdapter da = new SqlDataAdapter(consulta, oConexion.MtAbrirConexion());

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener contratos de viajes: " + ex.Message, ex);
            }
            oConexion.MtCerrarConexion();

            return dt;
        }
        public string MtRegistrarContrato(ClContratoM c)
        {
            string consulta = @"INSERT INTO contrato (idUsuario, fecha, salario, tipo, bono)
                        VALUES (@idUsuario, @fecha, @salario, @tipo, 0)";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idUsuario", c.idUsuario);
            cmd.Parameters.AddWithValue("@fecha", c.fecha);
            cmd.Parameters.AddWithValue("@salario", c.salario);
            cmd.Parameters.AddWithValue("@tipo", c.tipo);

            cmd.ExecuteNonQuery();
            oConexion.MtCerrarConexion();

            return "Contrato registrado correctamente.";
        }
        public string MtEliminarContrato(int idContrato)
        {
            string mensaje = "";
            try
            {
                string consulta = "DELETE FROM contrato WHERE idContrato = @idContrato";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idContrato", idContrato);

                int filas = cmd.ExecuteNonQuery();

                mensaje = filas > 0 ? "Contrato eliminado correctamente" : "No se encontró el contrato";
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar contrato: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }
        public string MtRegistrarGasto(ClGastoM g)
        {
            string mensaje = "";
            try
            {
                string consulta = @"INSERT INTO gasto (idViajeVehiculo, tipoGasto, monto, descripcion, fecha, imagenRecibo)
                            VALUES (@idViajeVehiculo, @tipoGasto, @monto, @descripcion, @fecha, @imagenRecibo)";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                // si no tienes idViajeVehiculo en el Excel, ajusta para asignar 0 o buscar el id correspondiente
                cmd.Parameters.AddWithValue("@idViajeVehiculo", g.idViajeVehiculo);
                cmd.Parameters.AddWithValue("@tipoGasto", g.tipoGasto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@monto", g.monto);
                cmd.Parameters.AddWithValue("@descripcion", g.descripcionGasto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha", g.fechaGasto);
                cmd.Parameters.AddWithValue("@imagenRecibo", g.imagenRecibo ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
                mensaje = "Gasto registrado";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar gasto: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }

    }
}