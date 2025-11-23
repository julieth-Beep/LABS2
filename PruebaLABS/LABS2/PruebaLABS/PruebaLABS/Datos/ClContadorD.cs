using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
    }
}