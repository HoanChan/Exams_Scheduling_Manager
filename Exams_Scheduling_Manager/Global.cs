using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Exams_Scheduling_Manager
{
    public class Global
    {
        public static SqlConnection SQLConnection;
        public static bool Connect(String ConnectionString)
        {
            try
            {
                SQLConnection = new SqlConnection(ConnectionString);
                SQLConnection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void FillTable(String Query, DataTable dTable)
        {
            //try
            //{
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = SQLConnection.CreateCommand();
                sqlCommand.CommandText = Query;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand.CommandText, SQLConnection);
                SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                //Fill the DataTable.
                sda.Fill(dTable);
            //}
            //catch (Exception)
            //{

            //}
        }
        /// <summary>
        /// Chạy dòng lệnh không trả về bảng
        /// </summary>
        /// <param name="Query">Lệnh cần chạy</param>
        /// <returns>Số lượng hàng bị ảnh hưởng</returns>
        public static int RunNonQuery(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SQLConnection;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteNonQuery();
        }
        /// <summary>
        /// Chạy dòng lệnh trả về một bảng
        /// </summary>
        /// <param name="Query">Lệnh cần chạy</param>
        /// <returns>Kết quả</returns>
        public static SqlDataReader RunReader(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SQLConnection;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteReader();
        }
    }
}
