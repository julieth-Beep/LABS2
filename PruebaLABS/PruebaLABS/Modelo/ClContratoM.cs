using PruebaLABS.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLABS.Modelo
{
    public class ClContratoM:ClUsuarioM
    {
        public int idContrato { get; set; }
        public DateTime fecha { get; set; }
        public decimal salario { get; set; }
        public string tipo { get; set; }
        public decimal bono { get; set; }
    }
}