using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Datos
{
    public class ClUsuarioM
    {
        public int idUusuario { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public int idRol { get; set; }

    }
}