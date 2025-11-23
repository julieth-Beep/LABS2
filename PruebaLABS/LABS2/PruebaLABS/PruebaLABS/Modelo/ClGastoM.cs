using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClGastoM
    {
        public int idGasto { get; set; }
        public string tipoGasto { get; set; }
        public string monto { get; set; }
        public string descripcionGasto { get; set; }
        public string fechaGasto { get; set; }
        public string imagenRecibo { get; set; }
    }
}