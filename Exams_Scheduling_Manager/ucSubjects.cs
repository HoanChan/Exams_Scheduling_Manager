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
    public partial class ucSubjects : UserControl
    {
        public ucSubjects()
        {
            InitializeComponent();
            dataGridView.ReadOnly = true;  
        }

        private void ucSubjects_Load(object sender, EventArgs e)
        {
            if (!DesignMode) // Không phải đang trong chế độ Design
            {
                DataTable dTable1 = new DataTable();
                Global.FillTable("Select * From BoMon", dTable1);
                foreach (DataRow dRow in dTable1.Rows)
                {
                    cboSubject.Items.Add(new SQLItem(dRow["MaBoMon"], dRow["TenBoMon"], dRow["KhoaQL"]));
                }
                cboSubject.Items.Add(new SQLItem(null, "?"));
                cboSubject.SelectedIndex = 0;

                DataTable dTable2 = new DataTable();
                Global.FillTable("Select * From Khoa", dTable2);
                foreach (DataRow dRow in dTable2.Rows)
                {
                    cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                }
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cboFaculty.Enabled)
            {
                ShowOnGirdView("SELECT [MaMonHoc],[TenMonHoc],[TCLyThuyet],[TCThucHanh],[MonThiNghiem],                                         [KhoaXepLich],[BoMonQL],[GhiChu]"
                                + " FROM monhoc, khoa, bomon"
                                + " WHERE MaKhoa = " + ((SQLItem)cboFaculty.SelectedItem).ID + " and MaKhoa                                 = KhoaQL and BoMonQL = MaBoMon");
            }
            else
            {
                ShowOnGirdView("Select * from monhoc where BoMonQL = " + ((SQLItem)cboSubject.SelectedItem).ID);
            }
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

        private void cboSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((SQLItem)cboSubject.SelectedItem).ID == null)
            {
                cboFaculty.Enabled = true;
            }
            else
            {
                cboFaculty.Enabled = false;
                foreach (SQLItem item in cboFaculty.Items)
                {
                    if (item.ID == ((SQLItem)cboSubject.SelectedItem).Info)
                    {
                        cboFaculty.SelectedItem = item;
                        break;
                    }
                }
            }
        }


        private class SQLItem
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
    }
}
