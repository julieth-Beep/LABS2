using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClClienteD
    {
        ClConexion oConexion = new ClConexion();
        public DataTable DatVehiculo()
        {
            DataTable dt = new DataTable();

            string consulta = @"select placa, modelo, capacidad, estado from vehiculo v inner join estadoVehiculo e on v.idEstadoVehiculo = e.idEstadoVehiculo;";

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