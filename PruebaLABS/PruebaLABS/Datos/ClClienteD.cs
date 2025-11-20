using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public ClClienteM MtLoginCliente(string correo,string pass)
        {
            ClClienteM oDatosCliente = null;

            string consulta = $"select idCliente,documento,nombre,apellido,empresa,telefono,correo,pasword from cliente where correo='{correo}' and pasword='{pass}'";
            SqlCommand cmd=new SqlCommand(consulta,oConexion.MtAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                oDatosCliente= new ClClienteM();
                oDatosCliente.idCliente=reader.GetInt32(reader.GetOrdinal("idCliente"));
                oDatosCliente.documento = reader["documento"].ToString();
                oDatosCliente.nombre = reader["nombre"].ToString();
                oDatosCliente.apellido = reader["apellido"].ToString();
                oDatosCliente.empresa = reader["empresa"].ToString();
                oDatosCliente.telefono = reader["telefono"].ToString();
                oDatosCliente.correo = reader["correo"].ToString();
                oDatosCliente.contraseña = reader["pasword"].ToString();
             
            }
            reader.Close();
            oConexion.MtCerrarConexion();
            return oDatosCliente;

        }

    }

    
}