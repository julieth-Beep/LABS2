using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClEstadisticaD
    {
        private ClConexion conexion = new ClConexion();
        public DataTable ObtenerEstadistica()
        {
            SqlConnection connection = conexion.MtAbrirConexion();
            SqlCommand cmd = new SqlCommand("spObtenerMovimientosContables", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conexion.MtCerrarConexion();
            return dt;
        }
    }
}