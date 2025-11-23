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

        public string MtRegistrarCliente(ClClienteM cliente)
        {
            string mensaje = "";

            try
            {
                string consultaVerificar = @"select count(*) from cliente where documento = @documento or correo = @correo";
                SqlCommand cmdVerificar = new SqlCommand(consultaVerificar, oConexion.MtAbrirConexion());
                cmdVerificar.Parameters.AddWithValue("@documento", cliente.documento);
                cmdVerificar.Parameters.AddWithValue("@correo", cliente.correo);

                int existe = (int)cmdVerificar.ExecuteScalar();
                oConexion.MtCerrarConexion();

                if (existe > 0)
                {
                    mensaje = "El cliente ya está registrado con ese documento o correo.";
                    return mensaje;
                }

                string insertar = @"insert into cliente (documento, nombre, apellido, empresa, telefono, correo, pasword, idEstado) 
                          values (@documento, @nombre, @apellido, @empresa, @telefono, @correo, @pasword, 1);
                          SELECT SCOPE_IDENTITY();";

                SqlCommand cmdInsertar = new SqlCommand(insertar, oConexion.MtAbrirConexion());
                cmdInsertar.Parameters.AddWithValue("@documento", cliente.documento);
                cmdInsertar.Parameters.AddWithValue("@nombre", cliente.nombre);
                cmdInsertar.Parameters.AddWithValue("@apellido", cliente.apellido);
                cmdInsertar.Parameters.AddWithValue("@empresa", cliente.empresa ?? "");
                cmdInsertar.Parameters.AddWithValue("@telefono", cliente.telefono ?? "");
                cmdInsertar.Parameters.AddWithValue("@correo", cliente.correo);
                cmdInsertar.Parameters.AddWithValue("@pasword", cliente.contraseña);

                int idCliente = Convert.ToInt32(cmdInsertar.ExecuteScalar());

                if (idCliente > 0)
                {
                    mensaje = "Cliente registrado exitosamente";
                }
                else
                {
                    mensaje = "Error al registrar el cliente";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar cliente: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }


    }


}