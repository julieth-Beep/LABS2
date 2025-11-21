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
        public DataTable ListaDatVehiculo()
        {
            return oContD.DatVehiculo();
        }
        public ClClienteM MtLoginCliente(string user,string pass)
        {
            return oContD.MtLoginCliente(user, pass);

        }

        public string MtRegistrarCliente(string documento, string nombre, string apellido, string empresa, string telefono, string correo, string contraseña)
        {
            ClClienteM cliente = new ClClienteM
            {
                documento = documento,
                nombre = nombre,
                apellido = apellido,
                empresa = empresa,
                telefono = telefono,
                correo = correo,
                contraseña = contraseña,
                idEstado = 1
            };

            return oContD.MtRegistrarCliente(cliente);
        }

    }

    
}