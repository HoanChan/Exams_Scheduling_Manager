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
            if (!DesignMode) 
            {
                DataTable dTable3 = new DataTable();
                Global.FillTable("Select * From bachoc", dTable3);
                foreach (DataRow dRow in dTable3.Rows)
                {
                    cboHeDaoTao.Items.Add(new SQLItem(dRow["MaBacHoc"], dRow["TenBacHoc"]));
                }
                cboHeDaoTao.Items.Add(new SQLItem(null, "?"));
                cboHeDaoTao.SelectedIndex = 1;
            }
        }

        private void ShowClass()
        {
            ShowOnGirdView("Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien Where Lop = '" + cboClass.SelectedItem.ToString() + "'");
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ShowFaculty()
        {
            String themHeDaoTao = ((SQLItem)cboHeDaoTao.SelectedItem).ID != null ? " and khoi.BacHoc= '" + ((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString() + "'" : "";

            ShowOnGirdView("Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien, lop, khoi  where lop = Malop and lop.Makhoi = khoi.MaKhoi and KhoaQL = '" + ((SQLItem)cboFaculty.SelectedItem).ID.ToString() + "'" + themHeDaoTao);

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ShowHeDaoTao()
        {
            ShowOnGirdView("Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien, lop, khoi  where lop = Malop and lop.Makhoi = khoi.MaKhoi and BacHoc = '" + ((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString() + "'");
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ShowAllStudents()
        {
            ShowOnGirdView("Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien");
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (((SQLItem)cboClass.SelectedItem).ID != null)
            {
                ShowClass();
            }
            else
                if (((SQLItem)cboFaculty.SelectedItem).ID != null)
                {
                    ShowFaculty();
                }
                else
                    if (((SQLItem)cboHeDaoTao.SelectedItem).ID != null)
                    {
                        ShowHeDaoTao();
                    }
                    else
                        ShowAllStudents();
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

        private void SeachFaculty(String ID)
        {
            DataTable dTable = new DataTable();
            Global.FillTable("Select DISTINCT [MaKHoa],[TenKhoa] from khoa, khoi where MaKhoa = KhoaQL and BacHoc = '" + ID + "' ORDER BY MaKHoa", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
            }
        }

        private void cboHeDaoTao_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFaculty.Items.Clear();
            cboFaculty.Items.Add(new SQLItem(null, "?"));
            if (((SQLItem)cboHeDaoTao.SelectedItem).ID != null)
            {
                SeachFaculty(((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString());
            }
            else
            {
                DataTable dTable2 = new DataTable();
                Global.FillTable("Select * From Khoa", dTable2);
                foreach (DataRow dRow in dTable2.Rows)
                {
                    cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                }
            }
            cboFaculty.SelectedIndex = 0;
            cboClass.SelectedIndex = 0;
        }

        private void SelectClass(String ID)
        {
            DataTable dTable = new DataTable();
            
            Global.FillTable("Select DISTINCT [MaLop] from lop, khoi where lop.MaKhoi = khoi.MaKhoi and khoi.KhoaQL = '" + ID + 
                            "' ORDER BY MaLop", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cboClass.Items.Add(new SQLItem(dRow["MaLop"], dRow["MaLop"]));
            }
        }

        private void cboFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboClass.Items.Clear();
            cboClass.Items.Add(new SQLItem(null, "?"));
            if (((SQLItem)cboFaculty.SelectedItem).ID != null)
            {
                SelectClass(((SQLItem)cboFaculty.SelectedItem).ID.ToString());
            }
            else
            {
                DataTable dTable = new DataTable();
                Global.FillTable("Select * From lop", dTable);
                foreach (DataRow dRow in dTable.Rows)
                {
                    cboClass.Items.Add(new SQLItem(dRow["MaLop"], dRow["MaLop"]));
                }
            }
            cboClass.SelectedIndex = 0;
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
