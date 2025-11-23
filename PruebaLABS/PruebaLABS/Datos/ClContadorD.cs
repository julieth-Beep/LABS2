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
        public DataTable GastosporConductor()
        {
            DataTable dt = new DataTable();

            string consulta = @"select g.tipoGasto,g.monto,g.descripcion AS descripcionGasto,g.fecha AS fechaGasto,g.imagenRecibo,u.documento AS documentoConductor,u.nombre 
            AS nombreConductor,u.apellido AS apellidoConductor,u.telefono,u.correo,r.nombre AS rol,r.descripcion AS descripcionRol,v.idViaje,v.puntoPartida,v.destino,v.fechaInicio,v.fechaFin,v.estadoViaje,v.costo AS costoViaje,v.distancia,
            ve.idVehiculo,ve.placa,ve.modelo,ve.capacidad,ev.estado AS estadoVehiculo,ev.descripcion AS descripcionEstadoVehiculo
            FROM gasto g INNER JOIN viajeVehiculo vv ON g.idViajeVehiculo = vv.idViajeVehiculo
            INNER JOIN usuario u ON vv.idConductor = u.idUsuario INNER JOIN cargo c ON u.idUsuario = c.idUsuario INNER JOIN rol r ON c.idRol = r.idRol
            INNER JOIN viaje v ON vv.idViaje = v.idViaje INNER JOIN vehiculo ve ON vv.idVehiculo = ve.idVehiculo INNER JOIN estadoVehiculo ev ON ve.idEstadoVehiculo = ev.idEstadoVehiculo ORDER BY u.nombre, g.fecha DESC;";

            SqlDataAdapter da = new SqlDataAdapter(consulta, oConexion.MtAbrirConexion());

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener gastos por conductor: " + ex.Message, ex);
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
                v.idUusuario = Convert.ToInt32(tbl["idUsuario"]);
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
        public DataTable MtBonoConductor()
        {
            DataTable dt = new DataTable();

            string consulta = @"select u.idUsuario,u.nombre, u.apellido,count(vv.idViaje) as totalViajes, (count(vv.idViaje) * 200000) as bonoTotal 
            from usuario u inner join viajeVehiculo vv on u.idUsuario = vv.idConductor group by u.idUsuario, u.nombre, u.apellido;";

            SqlDataAdapter da = new SqlDataAdapter(consulta, oConexion.MtAbrirConexion());

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener bono por conductor: " + ex.Message, ex);
            }
            oConexion.MtCerrarConexion();

            return dt;

        }
        public DataTable MtContraTotal()
        {
            DataTable dt = new DataTable();

            string consulta = @"SELECT u.documento, u.idUsuario,u.nombre,u.apellido,c.idContrato,c.salario, c.tipo, ISNULL(vViajes.cantidadViajes * 200000, 0) AS bono, (c.salario + ISNULL(vViajes.cantidadViajes * 200000, 0)) AS totalPagar
            FROM usuario u LEFT JOIN contrato c ON u.idUsuario = c.idUsuario 
            LEFT JOIN (SELECT vv.idConductor, COUNT(vv.idViaje) AS cantidadViajes 
            FROM viajeVehiculo vv GROUP BY vv.idConductor) vViajes ON vViajes.idConductor = u.idUsuario;";

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
        public string MtRegistrarContrato(ClContratoM c)
        {
            string consulta = @"INSERT INTO contrato (idUsuario, fecha, salario, tipo, bono)
                        VALUES (@idUsuario, @fecha, @salario, @tipo, 0)";

            SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idUsuario", c.idUusuario);
            cmd.Parameters.AddWithValue("@fecha", c.fecha);
            cmd.Parameters.AddWithValue("@salario", c.salario);
            cmd.Parameters.AddWithValue("@tipo", c.tipo);

            cmd.ExecuteNonQuery();
            oConexion.MtCerrarConexion();

            return "Contrato registrado correctamente.";
        }   
    }
}