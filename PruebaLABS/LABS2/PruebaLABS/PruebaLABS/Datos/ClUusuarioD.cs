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
            }
            tbldat.Close();
            oConexion.MtCerrarConexion();
            return oDatosUser;
        }

        
    }
}