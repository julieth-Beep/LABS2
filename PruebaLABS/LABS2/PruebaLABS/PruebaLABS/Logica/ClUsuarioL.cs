using PruebaLABS.Datos;
using PruebaLABS.Modelo;
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

       
    }
}