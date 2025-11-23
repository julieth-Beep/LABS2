using PruebaLABS.Modelo;
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
                    mensaje = "El cliente ya está registrado con ese correo.";
                    return mensaje;
                }

                string insertar = @"insert into cliente (documento, nombre, apellido, empresa, telefono, correo, idEstado) values (@documento, @nombre, @apellido, @empresa, @telefono, @correo, 1)";

                SqlCommand cmdInsertar = new SqlCommand(insertar, oConexion.MtAbrirConexion());
                cmdInsertar.Parameters.AddWithValue("@documento", cliente.documento);
                cmdInsertar.Parameters.AddWithValue("@nombre", cliente.nombre);
                cmdInsertar.Parameters.AddWithValue("@apellido", cliente.apellido);
                cmdInsertar.Parameters.AddWithValue("@empresa", cliente.empresa ?? "");
                cmdInsertar.Parameters.AddWithValue("@telefono", cliente.telefono ?? "");
                cmdInsertar.Parameters.AddWithValue("@correo", cliente.correo);

                int client = cmdInsertar.ExecuteNonQuery();

                if (client > 0)
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
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return dt;


        }

    }
}