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
    public partial class ucStep_SelectSubjects : UserControl
    {
        public ucStep_SelectSubjects()
        {
            InitializeComponent();
        }

        private void ucStep_SelectSubjects_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                String Query = " from monhoc, pdkmh, bomon, khoa"
                                + " where monhoc.MaMonHoc = pdkmh.MaMonHoc and bomon.MaBoMon = monhoc.BoMonQL and khoa.MaKhoa = bomon.KhoaQL";
                DataTable dTable1 = new DataTable();
                Global.FillTable("select distinct monhoc.BoMonQL, bomon.TenBoMon, khoa.MaKhoa" + Query, dTable1);
                foreach (DataRow dRow in dTable1.Rows)
                {
                    cboSubject.Items.Add(new SQLItem(dRow["BoMonQL"], dRow["TenBoMon"], dRow["MaKhoa"]));
                }
                cboSubject.Items.Add(new SQLItem(null, "?"));
                cboSubject.SelectedIndex = 0;

                DataTable dTable2 = new DataTable();
                Global.FillTable("select distinct khoa.MaKhoa, khoa.TenKhoa" + Query, dTable2);
                foreach (DataRow dRow in dTable2.Rows)
                {
                    cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                }
                cboFaculty.Items.Add(new SQLItem(null, "?"));
                cboFaculty.SelectedIndex = 0;
            }
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
                    if (item.ID.ToString() == ((SQLItem)cboSubject.SelectedItem).Info.ToString())
                    {
                        cboFaculty.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            dataGridView.BeginUpdate();
            String Query = "select distinct monhoc.MaMonHoc, monhoc.TenMonHoc from monhoc, pdkmh, bomon, khoa"
                            + " where monhoc.MaMonHoc = pdkmh.MaMonHoc and bomon.MaBoMon = monhoc.BoMonQL and khoa.MaKhoa = bomon.KhoaQL";
            if (cboFaculty.Enabled)
            {
                if (((SQLItem)cboFaculty.SelectedItem).ID == null)
                {
                    Global.ShowOnGridView(dataGridView, Query);
                }
                else
                {
                    Global.ShowOnGridView(dataGridView, Query + " and khoa.MaKhoa = '" + ((SQLItem)cboFaculty.SelectedItem).ID + "'");
                }
            }
            else
            {
                Global.ShowOnGridView(dataGridView, Query + " and bomon.MaBoMon = '" + ((SQLItem)cboSubject.SelectedItem).ID + "'");
            }
            dataGridView.EndUpdate();
            foreach (DataGridViewRow Row in dataGridView.Rows)
            {
                if (Global.IgnoreSubject.Contains(Row.Cells["MaMonHoc"].Value))
                {
                    Row.Cells[dataGridView.CheckBoxCollumnName].Value = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (String SubjectID in dataGridView.UnCheckedRows("MaMonHoc"))
            {
                if (!Global.IgnoreSubject.Contains(SubjectID))
                {
                    Global.IgnoreSubject.Add(SubjectID);
                }
            }
            MessageBox.Show("Đã lưu xong!", "Thông Báo");
        }
    }
}
