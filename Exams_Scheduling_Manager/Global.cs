using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

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
        public static void ShowOnGirdView(DataGridView dataGridView, String Query)
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
        }
        /// <summary>
        /// Ch?y d?ng l?nh kh?ng tr? v? b?ng
        /// </summary>
        /// <param name="Query">L?nh c?n ch?y</param>
        /// <returns>S? l??ng h?ng b? ?nh h??ng</returns>
        public static int RunNonQuery(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SQLConnection;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteNonQuery();
        }
        /// <summary>
        /// Ch?y d?ng l?nh, tr? v? gi? tr? trong ? ??u ti?n trong b?ng (? ? d?ng 1 c?t 1)
        /// </summary>
        /// <param name="Query">L?nh c?n ch?y</param>
        /// <returns>Gi? tr? c?a ?</returns>
        public static object RunScalar(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SQLConnection;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteScalar();
        }

        /// <summary>
        /// Ch?y d?ng l?nh tr? v? m?t b?ng
        /// </summary>
        /// <param name="Query">L?nh c?n ch?y</param>
        /// <returns>K?t qu?</returns>
        public static SqlDataReader RunReader(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SQLConnection;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteReader();
        }
      
    }
}
