using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Test_Wpf
{
    internal class SqlModel
    {
        /// <summary>
        /// Запрос к бд
        /// </summary>
        /// <param name="selectSQL"></param>
        /// <returns></returns>
        /// 
        public static DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("database");
            SqlConnection sqlConnection = new SqlConnection("server=192.168.140.128;Trusted_Connection=No;DataBase=Test;User=s;PWD=s");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

    }
}
