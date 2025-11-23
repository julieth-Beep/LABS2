using PruebaLABS.Datos;
using PruebaLABS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaLABS.Logica
{
    public class ClClienteL
    {
        ClClienteD oContD = new ClClienteD();

        public string MtRegistrarCliente(string documento, string nombre, string apellido, string empresa, string telefono, string correo)
        {
            ClClienteM cliente = new ClClienteM
            {
                documento = documento,
                nombre = nombre,
                apellido = apellido,
                empresa = empresa,
                telefono = telefono,
                correo = correo,
                idEstado = 1
            };

            return oContD.MtRegistrarCliente(cliente);
        }

        public DataTable ListaDatVehiculo()
        {
            return oContD.DatVehiculo();
        }

    }
}