using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exams_Scheduling_Manager
{
    public partial class ucStep_SelectStudents : UserControl
    {
        public ucStep_SelectStudents()
        {
            InitializeComponent();
        }

        private void ucStep_SelectStudents_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                String Query = " from monhoc, pdkmh, bomon, khoa"
                                + " where monhoc.MaMonHoc = pdkmh.MaMonHoc and bomon.MaBoMon = monhoc.BoMonQL and khoa.MaKhoa = bomon.KhoaQL";
                DataTable dTable2 = new DataTable();
                Global.FillTable("select distinct khoa.MaKhoa, khoa.TenKhoa" + Query, dTable2);

                foreach (DataRow dRow in dTable2.Rows)
                {
                    cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                }
                //          cboFaculty.Items.Add(new SQLItem(null, "?"));
                cboFaculty.SelectedIndex = 0;
            }
        }

        private void cboFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubject.Items.Clear();
            String Query = " from monhoc, pdkmh, bomon, khoa"
                                + " where monhoc.MaMonHoc = pdkmh.MaMonHoc and bomon.MaBoMon = monhoc.BoMonQL and khoa.MaKhoa = bomon.KhoaQL";
            if (((SQLItem)cboFaculty.SelectedItem).ID != null)
            {

                DataTable dTable1 = new DataTable();

                Global.FillTable("select distinct monhoc.BoMonQL, bomon.TenBoMon, khoa.MaKhoa" + Query + " and MaKhoa = '" + ((SQLItem)cboFaculty.SelectedItem).ID.ToString() + "'", dTable1);
                foreach (DataRow dRow in dTable1.Rows)
                {
                    cboSubject.Items.Add(new SQLItem(dRow["BoMonQL"], dRow["TenBoMon"], dRow["MaKhoa"]));
                }

            }

            cboSubject.Items.Add(new SQLItem(null, "?"));
            cboSubject.SelectedIndex = 0;
        }

        private void cboSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMon.Items.Clear();
            string query = " select distinct [MaMonHoc],[TenMonHoc] from monhoc,bomon where BoMOnQL = MaBoMon ";
            if (((SQLItem)cboSubject.SelectedItem).ID != null)
            {
                DataTable dTable = new DataTable();
                Global.FillTable(query + "and MaBoMon='" + ((SQLItem)cboSubject.SelectedItem).ID.ToString() + "'", dTable);
                foreach (DataRow dRow in dTable.Rows)
                {
                    cboMon.Items.Add(new SQLItem(dRow["MaMonHoc"], dRow["TenMonHoc"]));
                }
            }
            else
                if (((SQLItem)cboFaculty.SelectedItem).ID != null)
                {

                    DataTable dTable = new DataTable();
                    Global.FillTable(query + "and KhoaQL='" + ((SQLItem)cboFaculty.SelectedItem).ID.ToString() + "'", dTable);
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        cboMon.Items.Add(new SQLItem(dRow["MaMonHoc"], dRow["TenMonHoc"]));
                    }
                }
            cboMon.SelectedIndex = 0;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            String Query = "select distinct [sinhvien].[MaSinhVien], [sinhvien].[Ho], [sinhvien].[Ten], [sinhvien].[NgaySinh], [sinhvien].[lop] from monhoc, pdkmh, bomon, khoa, lop, sinhvien, khoi"
                            + " where monhoc.MaMonHoc = pdkmh.MaMonHoc and bomon.MaBoMon = monhoc.BoMonQL and khoa.MaKhoa = bomon.KhoaQL and lop = malop and lop.MaKhoi = khoi.MaKhoi and khoi.KhoaQL = MaKhoa and monhoc.MaMonHoc = '" + ((SQLItem)cboMon.SelectedItem).ID.ToString() + "'";

            Global.ShowOnGridView(dataGridView, Query);

            foreach (DataGridViewRow Row in dataGridView.Rows)
            {
                if (Global.IgoreStudents.Contains(Row.Cells["MaSinhVien"].Value))
                    Row.Cells[dataGridView.CheckBoxCollumnName].Value = false;
            }

            panel1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (String StudentID in dataGridView.UnCheckedRows("MaSinhVien"))
            {
                if (!Global.IgoreStudents.Contains(StudentID))
                {
                    Global.IgoreStudents.Add(StudentID);
                }
            }
            MessageBox.Show("Đã lưu xong!", "Thông Báo");
        }
    }
}
