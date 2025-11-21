using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClUusuarioD
    {
        public ClUsuarioM MtLogin(string email, string pass)
        {
            ClConexion oConexion = new ClConexion();
            ClUsuarioM oDatosUser = null;

            string consulta = @"select u.idUsuario, u.documento, u.nombre, u.apellido, u.telefono, u.correo, u.contraseña, r.idRol, r.nombre as nombreRol from usuario u inner join cargo c on u.idUsuario = c.idUsuario inner join rol r on c.idRol = r.idRol where u.correo = @email and u.contraseña = @pass";

            SqlCommand conexion = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            conexion.Parameters.AddWithValue("@email", email);
            conexion.Parameters.AddWithValue("@pass", pass);

            SqlDataReader tbldat = conexion.ExecuteReader();

            if (tbldat.Read())
            {
                oDatosUser = new ClUsuarioM();
                oDatosUser.idUusuario = tbldat.GetInt32(tbldat.GetOrdinal("idUsuario"));
                oDatosUser.documento = tbldat["documento"].ToString();
                oDatosUser.nombre = tbldat["nombre"].ToString();
                oDatosUser.apellido = tbldat["apellido"].ToString();
                oDatosUser.telefono = tbldat["telefono"].ToString();
                oDatosUser.correo = tbldat["correo"].ToString();
                oDatosUser.contraseña = tbldat["contraseña"].ToString();
                oDatosUser.idRol = tbldat.GetInt32(tbldat.GetOrdinal("idRol"));
                oDatosUser.nombreRol = tbldat["nombreRol"].ToString();
            }
            tbldat.Close();
            oConexion.MtCerrarConexion();
            return oDatosUser;
        }

        public string MtRegistrarUsuario(ClUsuarioM user)
        {
            string mensaje = "";
            ClConexion oConexion = new ClConexion();

            try
            {
                string consultaUsu = @"select count(*) from usuario where documento = @documento or correo = @correo";
                SqlCommand cmdVerificar = new SqlCommand(consultaUsu, oConexion.MtAbrirConexion());
                cmdVerificar.Parameters.AddWithValue("@documento", user.documento);
                cmdVerificar.Parameters.AddWithValue("@correo", user.correo);

                int existe = (int)cmdVerificar.ExecuteScalar();
                oConexion.MtCerrarConexion();

                if (existe > 0)
                {
                    mensaje = "El usuario ya está registrado con ese correo.";
                    return mensaje;
                }

                string insertar = @"insert into usuario (documento, nombre, apellido, telefono, correo, idRol) values (@documento, @nombre, @apellido, @telefono, @correo, 1)";

                SqlCommand cmdInsertar = new SqlCommand(insertar, oConexion.MtAbrirConexion());
                cmdInsertar.Parameters.AddWithValue("@documento", user.documento);
                cmdInsertar.Parameters.AddWithValue("@nombre", user.nombre);
                cmdInsertar.Parameters.AddWithValue("@apellido", user.apellido); ;
                cmdInsertar.Parameters.AddWithValue("@telefono", user.telefono ?? "");
                cmdInsertar.Parameters.AddWithValue("@correo", user.correo);

                int idUsuario = Convert.ToInt32(cmdInsertar.ExecuteScalar());
                oConexion.MtCerrarConexion();

                string consultaCargo = @"insert into cargo (idUsuario, idRol) values (@idUsuario, @idRol)";

                SqlCommand cmdCargo = new SqlCommand(consultaCargo, oConexion.MtAbrirConexion());
                cmdCargo.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdCargo.Parameters.AddWithValue("@idRol", user.idRol);

                cmdCargo.ExecuteNonQuery();
                oConexion.MtCerrarConexion();

                mensaje = "Usuario registrado exitosamente como " + GetNombreRol(user.idRol);
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar usuario: " + ex.Message;
            }
            finally
            {
                if (oConexion != null)
                {
                    oConexion.MtCerrarConexion();
                }
            }

            return mensaje;
        }

        private string GetNombreRol(int idRol)
        {
            switch (idRol)
            {
                case 1: return "Conductor";
                case 2: return "Administrador";
                case 3: return "Contador";
                default: return "Usuario";
            }
        }
    }
}