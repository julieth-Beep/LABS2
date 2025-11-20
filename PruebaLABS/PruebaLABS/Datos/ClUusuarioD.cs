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

        public string MtRegistrarUsuario(ClUsuarioM usuario)
        {
            ClConexion oConexion = new ClConexion();
            string mensaje = "";

            try
            {
                if (usuario.idRol == 3)
                {
                    string consultaVerificarCliente = @"select count(*) from cliente where documento = @documento or correo = @correo";
                    SqlCommand cmdVerificarCliente = new SqlCommand(consultaVerificarCliente, oConexion.MtAbrirConexion());
                    cmdVerificarCliente.Parameters.AddWithValue("@documento", usuario.documento);
                    cmdVerificarCliente.Parameters.AddWithValue("@correo", usuario.correo);

                    int existeCliente = (int)cmdVerificarCliente.ExecuteScalar();
                    oConexion.MtCerrarConexion();

                    if (existeCliente > 0)
                    {
                        mensaje = "El cliente ya está registrado con ese documento o correo.";
                        return mensaje;
                    }

                    string consultaCliente = @"insert into cliente (documento, nombre, apellido, empresa, telefono, correo, idEstado) values (@documento, @nombre, @apellido, @empresa, @telefono, @correo, 1)";

                    SqlCommand cmdCliente = new SqlCommand(consultaCliente, oConexion.MtAbrirConexion());

                    cmdCliente.Parameters.AddWithValue("@documento", usuario.documento);
                    cmdCliente.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmdCliente.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmdCliente.Parameters.AddWithValue("@empresa", usuario.empresa ?? "");
                    cmdCliente.Parameters.AddWithValue("@telefono", usuario.telefono ?? "");
                    cmdCliente.Parameters.AddWithValue("@correo", usuario.correo);

                    cmdCliente.ExecuteNonQuery();
                    oConexion.MtCerrarConexion();

                    mensaje = "Cliente registrado exitosamente";
                }
                else
                {
                    string consultaVerificar = @"select count(*) FROM usuario where documento = @documento or correo = @correo";
                    SqlCommand cmdVerificar = new SqlCommand(consultaVerificar, oConexion.MtAbrirConexion());
                    cmdVerificar.Parameters.AddWithValue("@documento", usuario.documento);
                    cmdVerificar.Parameters.AddWithValue("@correo", usuario.correo);

                    int existe = (int)cmdVerificar.ExecuteScalar();
                    oConexion.MtCerrarConexion();

                    if (existe > 0)
                    {
                        mensaje = "El usuario ya está registrado con ese documento o correo.";
                        return mensaje;
                    }

                    string consultaUsuario = @"insert into usuario (documento, nombre, apellido, telefono, correo, contraseña) values (@documento, @nombre, @apellido, @telefono, @correo, @contraseña); select SCOPE_IDENTITY();";

                    SqlCommand cmdUsuario = new SqlCommand(consultaUsuario, oConexion.MtAbrirConexion());
                    cmdUsuario.Parameters.AddWithValue("@documento", usuario.documento);
                    cmdUsuario.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmdUsuario.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmdUsuario.Parameters.AddWithValue("@telefono", usuario.telefono ?? "");
                    cmdUsuario.Parameters.AddWithValue("@correo", usuario.correo);
                    cmdUsuario.Parameters.AddWithValue("@contraseña", usuario.contraseña);

                    int idUsuario = Convert.ToInt32(cmdUsuario.ExecuteScalar());
                    oConexion.MtCerrarConexion();

                    string consultaCargo = @"insert into cargo (idUsuario, idRol) values (@idUsuario, @idRol)";

                    SqlCommand cmdCargo = new SqlCommand(consultaCargo, oConexion.MtAbrirConexion());
                    cmdCargo.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmdCargo.Parameters.AddWithValue("@idRol", usuario.idRol);

                    cmdCargo.ExecuteNonQuery();
                    oConexion.MtCerrarConexion();

                    mensaje = "Usuario registrado exitosamente como " + GetNombreRol(usuario.idRol);
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar: " + ex.Message;
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
                case 4: return "Cliente";
                case 3: return "Contador";
                default: return "Usuario";
            }
        }
    }
}