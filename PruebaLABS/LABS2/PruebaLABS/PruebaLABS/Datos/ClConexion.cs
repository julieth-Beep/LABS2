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
            Oconex = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbLABS;Integrated Security=True;");
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