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
    public partial class ucTeachers : UserControl
    {
        public ucTeachers()
        {
            InitializeComponent();
        }

        private Boolean IsModifyMode;

        private void ComboboxFacutly(ComboBox cbo)
        {
            DataTable dTable = new DataTable();
            Global.FillTable("Select [Makhoa],[TenKhoa] from khoa ", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cbo.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
            }
            cbo.Items.Add(new SQLItem(null, "?"));
            cbo.SelectedIndex = 0;
        }

        private void ucTeachers_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ComboboxFacutly(cboFaculty);
            }
            
        }

        private void SearchFacutly(ComboBox cbofacutly, ComboBox cbosubjects)
        {
            cbosubjects.Items.Clear();
            cbosubjects.Items.Add(new SQLItem(null, "?"));
            DataTable dTable = new DataTable();
            Global.FillTable("Select DISTINCT [MaBoMon],[TenBoMon] from bomon, khoa where KhoaQL = '" + ((SQLItem)cbofacutly.SelectedItem).ID.ToString() + "' ORDER BY MaBoMon", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cbosubjects.Items.Add(new SQLItem(dRow["MaBoMon"], dRow["TenBoMon"]));
            }
            cbosubjects.SelectedIndex = 0;
        }

        private void ChangeFacutly(ComboBox cbokhoa,ComboBox cbobomon)
        {
            cbobomon.Items.Clear();
            cbobomon.Items.Add(new SQLItem(null, "?"));
            if (((SQLItem)cbokhoa.SelectedItem).ID != null)
            {
                SearchFacutly(cbokhoa, cbobomon);
            }
            else
            {
                DataTable dTable = new DataTable();
                Global.FillTable("Select * From bomon", dTable);
                foreach (DataRow dRow in dTable.Rows)
                {
                    cbobomon.Items.Add(new SQLItem(dRow["MaBoMon"], dRow["TenBoMon"]));
                }
            }
            cbobomon.SelectedIndex = 0;
        }

        private void cboFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFacutly(cboFaculty, cboSubject);
        }

        private void ShowFacutly()
        {
            Global.ShowOnGridView(dataGridView, "Select [MaGiaoVien],[HoLot],[TenGiaoVien],[HocHam],[HocVi] From giaovien, bomon where BoMonQL = MaBoMon and KhoaQL = '" + ((SQLItem)cboFaculty.SelectedItem).ID.ToString() + "'");
        }

        private void ShowSubjects()
        {
            Global.ShowOnGridView(dataGridView, "Select [MaGiaoVien],[HoLot],[TenGiaoVien],[HocHam],[HocVi] From giaovien where BoMonQL = '" + ((SQLItem)cboSubject.SelectedItem).ID.ToString() + "'");
        }

        private void ShowAllTeachers()
        {
            Global.ShowOnGridView(dataGridView, "Select [MaGiaoVien],[HoLot],[TenGiaoVien],[HocHam],[HocVi] From giaovien");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            if (((SQLItem)cboSubject.SelectedItem).ID != null)
            {
                ShowSubjects();
            }
            else
                if (((SQLItem)cboFaculty.SelectedItem).ID != null)
                {
                    ShowFacutly();
                }
                else
                {
                    ShowAllTeachers();
                }
            panel.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IsModifyMode = false;
        }
    }
}
