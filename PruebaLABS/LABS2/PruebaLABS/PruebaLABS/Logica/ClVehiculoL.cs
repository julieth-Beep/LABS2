using System.Collections.Generic;
using PruebaLABS.Datos;
using System;
using System.Linq;
using System.Web;

using PruebaLABS.Modelo;

namespace PruebaLABS.Logica
{
    public class ClVehiculoL
    {
        ClVehiculoD datosVehiculo = new ClVehiculoD();

        public List<ClVehiculoM> MtListarVehiculos()
        {
            return datosVehiculo.MtListarVehiculos();
        }

        public ClVehiculoM MtBuscarPorPlaca(string placa)
        {
            return datosVehiculo.MtBuscarPorPlaca(placa);
        }

        public List<ClVehiculoM> MtVehiculosDisponibles()
        {
            return datosVehiculo.MtVehiculosDisponibles();
        }

        public string MtEditarVehiculo(ClVehiculoM v)
        {
            return datosVehiculo.MtEditarVehiculo(v);
        }

        public string MtCambiarEstado(int idVehiculo, int idEstado)
        {
            return datosVehiculo.MtCambiarEstado(idVehiculo, idEstado);
        }
        public string AgregarVehiculo(ClVehiculoM v)
        {
            ClVehiculoD obj = new ClVehiculoD();
            return obj.MtAgregarVehiculo(v);
        }

    }
}
