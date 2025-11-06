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
    }
}