using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Exams_Scheduling_Manager
{
    public class SQLItem
    {
        public object ID;
        public object Name;
        public object Info;
        public SQLItem(object _ID, object _Name)
        {
            ID = _ID;
            Name = _Name;
            Info = null;
        }
        public SQLItem(object _ID, object _Name, Object _Info)
        {
            ID = _ID;
            Name = _Name;
            Info = _Info;
        }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
    public class Global
    {
        public static List<String> IgnoreSubject = new List<String>();
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
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand = SQLConnection.CreateCommand();
            sqlCommand.CommandText = Query;
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand.CommandText, SQLConnection);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            sda.Fill(dTable);
        }
        public static void ShowOnGridView(DataGridView dataGridView, String Query)
        {
            //Create a DataTable to hold the query results.
            DataTable dTable = new DataTable();
            //Fill the DataTable.
            FillTable(Query, dTable);
            //BindingSource to sync DataTable and DataGridView.
            BindingSource bSource = new BindingSource();
            //Set the BindingSource DataSource.
            bSource.DataSource = dTable;
            //Set the DataGridView DataSource.
            dataGridView.DataSource = bSource;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            for (int i = 0; i < dataGridView.RowCount; i = i + 2)
            {
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            }
            for (int i = 1; i < dataGridView.RowCount; i = i + 2)
            {
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            }
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
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
        /// Chạy dòng lệnh, trả về giá trị trong ô đầu tiên trong bảng (ô ở dòng 1 cột 1)
        /// </summary>
        /// <param name="Query">Lệnh cần chạy</param>
        /// <returns>Giá trị của ô</returns>
        public static object RunScalar(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SQLConnection;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteScalar();
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
