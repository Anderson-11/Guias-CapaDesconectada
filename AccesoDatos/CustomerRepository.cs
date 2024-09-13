using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class CustomerRepository
    {
        public DataTable ObtenerTodos()
        {
            DataTable dataTable = new DataTable();

            String selectALl = "";
            selectALl = selectALl + "SELECT [CustomerID] " + "\n";
            selectALl = selectALl + "      ,[CompanyName] " + "\n";
            selectALl = selectALl + "      ,[ContactName] " + "\n";
            selectALl = selectALl + "      ,[ContactTitle] " + "\n";
            selectALl = selectALl + "      ,[Address] " + "\n";
            selectALl = selectALl + "      ,[City] " + "\n";
            selectALl = selectALl + "      ,[Region] " + "\n";
            selectALl = selectALl + "      ,[PostalCode] " + "\n";
            selectALl = selectALl + "      ,[Country] " + "\n";
            selectALl = selectALl + "      ,[Phone] " + "\n";
            selectALl = selectALl + "      ,[Fax] " + "\n";
            selectALl = selectALl + "  FROM [dbo].[Customers]";

            //var conexion = @"Data Source=DESKTOP-DHSOO9J\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlDataAdapter adapter = new SqlDataAdapter(selectALl, DataBase.ConnectionString);
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
