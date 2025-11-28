using PruebaLABS.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClUsuarioL
    {
        ClUusuarioD DatUsu = new ClUusuarioD();

        public ClUsuarioM MtLogin(string usuario, string pass)
        {
            ClUsuarioM oDtos = DatUsu.MtLogin(usuario, pass);
            return oDtos ?? null;
        }

        public string MtRegistrarUsuario(string documento, string nombre, string apellido, string telefono, string correo,
             string contraseña, string idRol)
        {
            ClUsuarioM user = new ClUsuarioM
            {
                documento = documento,
                nombre = nombre,
                apellido = apellido,
                telefono = telefono,
                correo = correo,
                contraseña = contraseña,
                idRol = Convert.ToInt32(idRol),
            };

            return DatUsu.MtRegistrarUsuario(user);
        }
        public List<ClUsuarioM> MtListarUsuarios()
        {
            return DatUsu.MtListarUsuarios();
        }
    }
}