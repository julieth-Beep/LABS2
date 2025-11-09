using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClUusuarioD
    {
        public ClUsuarioM MtLogin(string email,string pass)
        {
            ClConexion oConexion=new ClConexion();
            ClUsuarioM oDatosUser = null;

            string consulta = $"select usuario.idUsuario,usuario.documento,usuario.nombre,usuario.apellido,usuario.correo,usuario.telefono,usuario.contraseña,rol.idRol from usuario inner join cargo on usuario.idUsuario=cargo.idUsuario inner join rol on cargo.idRol=rol.idRol where correo='{email}' and contraseña='{pass}'";
            SqlCommand conexion=new SqlCommand(consulta,oConexion.MtAbrirConexion());
            SqlDataReader tbldat=conexion.ExecuteReader();

           

            if (tbldat.Read())
            {
                oDatosUser=new ClUsuarioM();
                oDatosUser.idUusuario = tbldat.GetInt32(tbldat.GetOrdinal("idUsuario"));
                oDatosUser.documento = (tbldat["documento"].ToString());
                oDatosUser.nombre = (tbldat["nombre"].ToString());
                oDatosUser.apellido = (tbldat["apellido"].ToString());
                oDatosUser.telefono = (tbldat["telefono"].ToString());
                oDatosUser.correo = (tbldat["correo"].ToString());
                oDatosUser.contraseña = (tbldat["contraseña"].ToString());
                oDatosUser.idRol= tbldat.GetInt32(tbldat.GetOrdinal("idRol"));

            }
            tbldat.Close();
            oConexion.MtCerrarConexion();
            return oDatosUser;
        }

        public string MtRegistrarUsuario(ClUsuarioM usuario)
        {
            ClConexion oConexion = new ClConexion();
            string mensaje = "";

            try
            {
                string consultaVerificar = $"select count(*) from usuario where documento = '{usuario.documento}' or correo = '{usuario.correo}'";
                SqlCommand cmdVerificar = new SqlCommand(consultaVerificar, oConexion.MtAbrirConexion());
                int existe = (int)cmdVerificar.ExecuteScalar();
                cmdVerificar.Connection.Close(); 

                if (existe > 0)
                {
                    mensaje = "El usuario ya está registrado con ese documento o correo.";
                    return mensaje;
                }

                string consulta = @"insert into usuario (documento, nombre, apellido, telefono, correo, contraseña) 
                           values (@documento, @nombre, @apellido, @telefono, @correo, @contraseña);
                           select SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(consulta, oConexion.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@documento", usuario.documento);
                cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                cmd.Parameters.AddWithValue("@telefono", usuario.telefono ?? "");
                cmd.Parameters.AddWithValue("@correo", usuario.correo);
                cmd.Parameters.AddWithValue("@contraseña", usuario.contraseña);

                int idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Connection.Close(); 

                string consultaCargo = "insert into cargo (idUsuario, idRol) values (@idUsuario, 3)";
                SqlCommand cmdCargo = new SqlCommand(consultaCargo, oConexion.MtAbrirConexion());
                cmdCargo.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdCargo.ExecuteNonQuery();
                cmdCargo.Connection.Close(); 

                mensaje = "Usuario registrado exitosamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar usuario: " + ex.Message;

                if (oConexion != null)
                {
                    oConexion.MtCerrarConexion();
                }
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }

            return mensaje;
        }
    }
}