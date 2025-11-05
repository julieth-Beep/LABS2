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

            string consulta = $"select usuario.idUsuario,usuario.documento,usuario.nombre,usuario.apellido,usuario.telefono,usuario.correo,usuario.contraseña from usuario where Email='{email}' and Clave='{pass}'";
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

            }
            oConexion.MtCerrarConexion();
            return oDatosUser;
        }
    }
}