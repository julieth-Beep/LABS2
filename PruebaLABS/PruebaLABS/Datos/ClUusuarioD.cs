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
                oDatosUser.idUsuario = tbldat.GetInt32(tbldat.GetOrdinal("idUsuario"));
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

            ClConexion oConexion = new ClConexion();

            try
            {
                
                string consultaUsu = @"select count(*) from usuario 
                               where documento = @documento or correo = @correo";
                SqlCommand cmdVerificar = new SqlCommand(consultaUsu, oConexion.MtAbrirConexion());
                cmdVerificar.Parameters.AddWithValue("@documento", user.documento);
                cmdVerificar.Parameters.AddWithValue("@correo", user.correo);

                int existe = (int)cmdVerificar.ExecuteScalar();
                oConexion.MtCerrarConexion();

                if (existe > 0)
                    return "El usuario ya está registrado.";

                
                string insertar = @"
            insert into usuario (documento, nombre, apellido, telefono, correo, contraseña)
            output inserted.idUsuario
            values (@documento, @nombre, @apellido, @telefono, @correo, @contraseña)";

                SqlCommand cmdInsertar = new SqlCommand(insertar, oConexion.MtAbrirConexion());
                cmdInsertar.Parameters.AddWithValue("@documento", user.documento);
                cmdInsertar.Parameters.AddWithValue("@nombre", user.nombre);
                cmdInsertar.Parameters.AddWithValue("@apellido", user.apellido);
                cmdInsertar.Parameters.AddWithValue("@telefono", user.telefono);
                cmdInsertar.Parameters.AddWithValue("@correo", user.correo);
                cmdInsertar.Parameters.AddWithValue("@contraseña", user.contraseña);

                int idUsuario = (int)cmdInsertar.ExecuteScalar();
                oConexion.MtCerrarConexion();

                string insertarCargo = @"insert into cargo (idRol, idUsuario)
                                 values (@idRol, @idUsuario)";
                SqlCommand cmdCargo = new SqlCommand(insertarCargo, oConexion.MtAbrirConexion());
                cmdCargo.Parameters.AddWithValue("@idRol", user.idRol);
                cmdCargo.Parameters.AddWithValue("@idUsuario", idUsuario);

                cmdCargo.ExecuteNonQuery();
                oConexion.MtCerrarConexion();

                return "Usuario registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return "Error al registrar: " + ex.Message;
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }


           
        }
        public List<ClUsuarioM> MtListarUsuarios()
        {
            List<ClUsuarioM> lista = new List<ClUsuarioM>();

            string consulta = @"
        SELECT u.idUsuario, u.documento, u.nombre, u.apellido, 
               u.telefono, u.correo, r.nombre AS nombreRol, r.idRol
        FROM usuario u
        INNER JOIN cargo c ON u.idUsuario = c.idUsuario
        INNER JOIN rol r ON c.idRol = r.idRol";

            ClConexion cn = new ClConexion();
            SqlCommand cmd = new SqlCommand(consulta, cn.MtAbrirConexion());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new ClUsuarioM()
                {
                    idUsuario = Convert.ToInt32(dr["idUsuario"]),
                    documento = dr["documento"].ToString(),
                    nombre = dr["nombre"].ToString(),
                    apellido = dr["apellido"].ToString(),
                    telefono = dr["telefono"].ToString(),
                    correo = dr["correo"].ToString(),
                    nombreRol = dr["nombreRol"].ToString(),
                    idRol = Convert.ToInt32(dr["idRol"])
                });
            }

            dr.Close();
            cn.MtCerrarConexion();
            return lista;
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