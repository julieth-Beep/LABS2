using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace PruebaLABS.Datos
{
    public class ClConexion
    {
        SqlConnection Oconex;
         public ClConexion()
        {
            Oconex = new SqlConnection("Data Source=Nino\\SQLEXPRESS;Initial Catalog=dbLABS;Persist Security Info=True;User ID=Labs;Password=12345;");
        }

        public SqlConnection MtAbrirConexion()
        {
            Oconex.Open();
            return Oconex;
        }

        public void MtCerrarConexion()
        {
            Oconex.Close();
        }
    }
}