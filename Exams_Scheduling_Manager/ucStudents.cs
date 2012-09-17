using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exams_Scheduling_Manager
{
    public partial class ucStudents : UserControl
    {
        public ucStudents()
        {
            InitializeComponent();
            dataGridView.ReadOnly = true;
        }

        private void ucStudents_Load(object sender, EventArgs e)
        {
            DataTable dTable = new DataTable();
            Global.FillTable("Select * From lop", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cboClass.Items.Add(dRow["MaLop"]);
            }
            cboClass.SelectedIndex = 0;
        }

        
        private class SQLItem
        {
            public object ID;
            public object CVHT;
            public object Info;
            public SQLItem(object _ID,object _CVHT)
            {
                ID = _ID;
                CVHT = _CVHT;
                Info = null;
            }
            public SQLItem(object _ID, object _CVHT, Object _Info)
            {
                ID = _ID;
                CVHT = _CVHT;
                Info = _Info;
            }

            public override string ToString()
            {
                return CVHT.ToString();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowOnGirdView("select * from sinhvien where Lop = " + cboClass.SelectedItem.ToString());
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        

        private void ShowOnGirdView(String Query)
        {
            //Create a DataTable to hold the query results.
            DataTable dTable = new DataTable();
            //Fill the DataTable.
            Global.FillTable(Query, dTable);
            //BindingSource to sync DataTable and DataGridView.
            BindingSource bSource = new BindingSource();
            //Set the BindingSource DataSource.
            bSource.DataSource = dTable;
            //Set the DataGridView DataSource.
            dataGridView.DataSource = bSource;
        }

      
    }
}
