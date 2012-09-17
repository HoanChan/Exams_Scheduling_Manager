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
        
        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowOnGirdView("Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien Where Lop = '" + cboClass.SelectedItem.ToString() + "'");
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
