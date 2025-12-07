<%@ WebHandler Language="C#" Class="NotificacionesCliente" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using PruebaLABS.Datos;

public class NotificacionesCliente : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        
        try
        {
            string idClienteParam = context.Request.QueryString["idCliente"];
            string soloNuevasParam = context.Request.QueryString["soloNuevas"];
            
            if (string.IsNullOrEmpty(idClienteParam))
            {
                context.Response.Write(JsonConvert.SerializeObject(new 
                { 
                    success = false, 
                    error = "idCliente es requerido" 
                }));
                return;
            }

            int idCliente;
            if (!int.TryParse(idClienteParam, out idCliente))
            {
                context.Response.Write(JsonConvert.SerializeObject(new 
                { 
                    success = false, 
                    error = "idCliente debe ser un número válido" 
                }));
                return;
            }

            bool soloNuevas = soloNuevasParam != null && soloNuevasParam.ToLower() == "true";

            string query = soloNuevas
                ? @"SELECT TOP 5 idNotificacion, mensaje, 
                           CONVERT(varchar, fecha, 120) as fecha, 
                           leido 
                    FROM Notificacion 
                    WHERE idCliente = @idCliente AND leido = 0 
                    ORDER BY fecha DESC"
                : @"SELECT idNotificacion, mensaje, 
                           CONVERT(varchar, fecha, 120) as fecha, 
                           leido 
                    FROM Notificacion 
                    WHERE idCliente = @idCliente 
                    ORDER BY fecha DESC";

            List<Dictionary<string, object>> notificaciones = new List<Dictionary<string, object>>();
            
            ClConexion conexion = new ClConexion();
            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idCliente", idCliente);
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var notificacion = new Dictionary<string, object>
                    {
                        ["idNotificacion"] = reader["idNotificacion"],
                        ["mensaje"] = reader["mensaje"],
                        ["fecha"] = reader["fecha"],
                        ["leido"] = reader["leido"]
                    };
                    notificaciones.Add(notificacion);
                }
            }
            conexion.MtCerrarConexion();

            if (soloNuevas && notificaciones.Count > 0)
            {
                string updateQuery = @"UPDATE Notificacion 
                                      SET leido = 1 
                                      WHERE idCliente = @idCliente AND leido = 0";
                
                ClConexion conexion2 = new ClConexion();
                SqlCommand updateCmd = new SqlCommand(updateQuery, conexion2.MtAbrirConexion());
                updateCmd.Parameters.AddWithValue("@idCliente", idCliente);
                updateCmd.ExecuteNonQuery();
                conexion2.MtCerrarConexion();
            }

            var response = new
            {
                success = true,
                notificaciones = notificaciones.Count,
                data = notificaciones
            };

            context.Response.Write(JsonConvert.SerializeObject(response));
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                success = false,
                error = ex.Message
            };
            context.Response.Write(JsonConvert.SerializeObject(errorResponse));
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}