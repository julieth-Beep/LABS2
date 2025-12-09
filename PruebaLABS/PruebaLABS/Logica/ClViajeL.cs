using PruebaLABS.Datos;
using PruebaLABS.Modelo;
using System.Collections.Generic;
using System.Data;

namespace PruebaLABS.Logica
{
    public class ClViajeL
    {
        private readonly ClViajeD datos = new ClViajeD();

        public List<ClViajeM> MtViajesConductor(int idConductor)
        {
            return datos.MtObtenerViajesC(idConductor);
        }

        public string MtCambiarEstado(int idViaje, string estado)
        {
            return datos.MtCambiarEstadoViaje(idViaje, estado);
        }

        public int MtObtenerIdViajeVehiculo(int idViaje, int idConductor)
        {
            return datos.obteneridVeviculo(idViaje, idConductor);
        }

        public List<ClViajesAdminM> MtViajesAdmin()
        {
            return datos.MtObtenerViajesAdmin();
        }

        public DataTable MtConductores()
        {
            return datos.MtConductores();
        }

        public DataTable MtVehiculosDisponibles()
        {
            return datos.MtVehiculosDisponibles();
        }

        public string MtCrearViajeSinSP(int idCliente, string origen, string destino, string distancia,
                                        string costo, string tipoCarga, string motivo, string observaciones)
        {
            if (idCliente <= 0) return " Cliente inválido";
            if (string.IsNullOrWhiteSpace(origen)) return " Origen requerido";
            if (string.IsNullOrWhiteSpace(destino)) return " Destino requerido";

            return datos.MtCrearViajeSinSP(idCliente, origen, destino, distancia, costo, tipoCarga, motivo, observaciones);
        }

        public string MtAsignarViaje(int idViaje, int idConductorCargo, int idVehiculo, string anticipo)
        {
            if (idViaje <= 0) return " Viaje inválido";
            if (idConductorCargo <= 0) return " Seleccione un conductor";
            if (idVehiculo <= 0) return " Seleccione un vehículo";

            return datos.MtAsignarViaje(idViaje, idConductorCargo, idVehiculo, anticipo);
        }

        public string MtEditarViajeBase(int idViaje, string origen, string destino, string distancia,
                                        string costo, string tipoCarga, string motivo, string observaciones)
        {
            if (idViaje <= 0) return " Viaje inválido";
            if (string.IsNullOrWhiteSpace(origen)) return " Origen requerido";
            if (string.IsNullOrWhiteSpace(destino)) return " Destino requerido";

            return datos.MtEditarViajeBase(idViaje, origen, destino, distancia, costo, tipoCarga, motivo, observaciones);
        }

        public string MtEditarAsignacionViaje(int idViaje, int idConductorCargo, int idVehiculo, string anticipo)
        {
            if (idViaje <= 0) return " Viaje inválido";
            if (idConductorCargo <= 0) return " Seleccione un conductor";
            if (idVehiculo <= 0) return "❌ Seleccione un vehículo";

            return datos.MtEditarAsignacionViaje(idViaje, idConductorCargo, idVehiculo, anticipo);
        }
        public DataTable MtViajesNoAsignados()
        {
            return datos.MtViajesNoAsignados();
        }

        public string MtAsignarViajeSinSP(int idViaje, int idConductorCargo, int idVehiculo,
                                          string anticipo, string destino, string costo, string distancia)
        {
            if (idViaje <= 0) return " Viaje inválido";
            if (idConductorCargo <= 0) return " Seleccione un conductor";
            if (idVehiculo <= 0) return " Seleccione un vehículo";

            return datos.MtAsignarViajeSinSP(idViaje, idConductorCargo, idVehiculo, anticipo, destino, costo, distancia);
        }
        public List<ClGastoM> GRConductor(int idConductor)
        {
            return datos.ReporteGastosConductor(idConductor);
        }

        public string MtRegistrarGastoConImagen(ClGastoM gasto)
        {
            if (gasto == null) return " Gasto inválido";
            if (gasto.idViajeVehiculo <= 0) return "❌ idViajeVehiculo inválido";
            if (string.IsNullOrWhiteSpace(gasto.tipoGasto)) return " Tipo de gasto requerido";
            if (gasto.monto < 0) return " Monto inválido";

            if (gasto.fechaGasto == default) gasto.fechaGasto = System.DateTime.Now;

            return datos.MtInsertarGastoConImagen(gasto);
        }

        
        public List<ClGastoM> ReporteGastosAdmin()
        {
            return datos.ReporteGastosAdmin();
        }


    }
}
