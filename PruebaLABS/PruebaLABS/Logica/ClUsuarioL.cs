using PruebaLABS.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClUsuarioL
    {
        ClUusuarioD DatUsu=new ClUusuarioD();
        public ClUsuarioM MtLogin(string usuario,string pass)
        {
            ClUsuarioM oDtos=DatUsu.MtLogin(usuario, pass);

            if (oDtos != null)
            {
                HttpContext.Current.Session["idUsuario"] = oDtos.idUusuario;
                HttpContext.Current.Session["nombre"] = oDtos.nombre;
                HttpContext.Current.Session["correo"] = oDtos.correo;

            }
            else
            {
                return null;
            }
            return oDtos;
        }
    }
}