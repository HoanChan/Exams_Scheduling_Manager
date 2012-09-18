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

        private void ComboBacHoc(ComboBox cbo)
        {
            DataTable dTable3 = new DataTable();
            Global.FillTable("Select * From bachoc", dTable3);
            foreach (DataRow dRow in dTable3.Rows)
            {
                cbo.Items.Add(new SQLItem(dRow["MaBacHoc"], dRow["TenBacHoc"]));
            }
            cbo.Items.Add(new SQLItem(null, "?"));
            cbo.SelectedIndex = 1;
        }

        private void ucStudents_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ComboBacHoc(cboHeDaoTao);
            }
        }

        private void ShowClass()
        {
            Global.ShowOnGirdView(dataGridView, "Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien Where Lop = '" + cboClass.SelectedItem.ToString() + "'");
        }

        private void ShowFaculty()
        {
            String themHeDaoTao = ((SQLItem)cboHeDaoTao.SelectedItem).ID != null ? " and khoi.BacHoc= '" + ((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString() + "'" : "";

            Global.ShowOnGirdView(dataGridView, "select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] from sinhvien, khoi, khoa, lop where sinhvien.lop = lop.MaLop and lop.MaKhoi = khoi.MaKhoi and khoa.MaKhoa = khoi.KhoaQL and khoa.MaKhoa = '" + ((SQLItem)cboFaculty.SelectedItem).ID.ToString() + "'" + themHeDaoTao);

        }

        private void ShowHeDaoTao()
        {
            Global.ShowOnGirdView(dataGridView, "Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien, lop, khoi  where lop = Malop and lop.Makhoi = khoi.MaKhoi and BacHoc = '" + ((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString() + "'");
        }

        private void ShowAllStudents()
        {
            Global.ShowOnGirdView(dataGridView, "Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien");
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

        private void SeachFaculty(String ID, ComboBox cboFaculty)
        {
            DataTable dTable = new DataTable();
            Global.FillTable("Select DISTINCT [MaKHoa],[TenKhoa] from khoa, khoi where MaKhoa = KhoaQL and BacHoc = '" + ID + "' ORDER BY MaKHoa", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
            }
        }

        private void ChangeHeDaoTao(ComboBox cboHDT, ComboBox cboKhoa, ComboBox cboLop)
        {
            cboKhoa.Items.Clear();
            cboKhoa.Items.Add(new SQLItem(null, "?"));
            if (((SQLItem)cboHDT.SelectedItem).ID != null)
            {
                SeachFaculty(((SQLItem)cboHDT.SelectedItem).ID.ToString(), cboKhoa);
            }
            else
            {
                DataTable dTable2 = new DataTable();
                Global.FillTable("Select * From Khoa", dTable2);
                foreach (DataRow dRow in dTable2.Rows)
                {
                    cboKhoa.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                }
            }
            cboKhoa.SelectedIndex = 0;
            cboLop.SelectedIndex = 0;
        }

        private void cboHeDaoTao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeHeDaoTao(cboHeDaoTao, cboFaculty, cboClass);
        }

        private void SelectClass(String ID, ComboBox cboClass)
        {
            DataTable dTable = new DataTable();

            Global.FillTable("Select DISTINCT [MaLop] from lop, khoi where lop.MaKhoi = khoi.MaKhoi and khoi.KhoaQL = '" + ID +
                            "' ORDER BY MaLop", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cboClass.Items.Add(new SQLItem(dRow["MaLop"], dRow["MaLop"]));
            }
        }

        private void ChangeCboFaculty(ComboBox cboKhoa, ComboBox cboLop)
        {
            cboLop.Items.Clear();
            cboLop.Items.Add(new SQLItem(null, "?"));
            if (((SQLItem)cboKhoa.SelectedItem).ID != null)
            {
                SelectClass(((SQLItem)cboKhoa.SelectedItem).ID.ToString(), cboLop);
            }
            else
            {
                DataTable dTable = new DataTable();
                Global.FillTable("Select * From lop", dTable);
                foreach (DataRow dRow in dTable.Rows)
                {
                    cboLop.Items.Add(new SQLItem(dRow["MaLop"], dRow["MaLop"]));
                }
            }
            cboLop.SelectedIndex = 0;
        }

        private void cboFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCboFaculty(cboFaculty, cboClass);
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

        private Boolean IsModifyMode = false;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Đề nghị chọn dòng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Bạn có muốn xóa sinh viên có thông tin:\r\n_ MSSV: " + dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value + "\r\n_ Họ tên: " + dataGridView.SelectedRows[0].Cells["Ho"].Value + " " + dataGridView.SelectedRows[0].Cells["Ten"].Value, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "DELETE FROM sinhvien WHERE MaSinhVien = @MaSV";
                    mySqlCommand.Parameters.AddWithValue("@MaSV", dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value);

                    if (mySqlCommand.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo");
                        btnShow.PerformClick();
                    }
                }
                catch
                {
                    if (MessageBox.Show("Sinh viên này đã đăng ký môn học rồi. Bạn có thật sự muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SqlCommand mySqlCommand1 = Global.SQLConnection.CreateCommand();
                        mySqlCommand1.CommandText = "DELETE FROM pdkmh  WHERE MaSinhVien = @MaSV";
                        mySqlCommand1.Parameters.AddWithValue("@MaSV", dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value);
                        mySqlCommand1.ExecuteNonQuery();

                        mySqlCommand1 = Global.SQLConnection.CreateCommand();
                        mySqlCommand1.CommandText = "DELETE FROM sinhvien WHERE MaSinhVien = @MaSV";
                        mySqlCommand1.Parameters.AddWithValue("@MaSV", dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value);

                        try
                        {
                            if (mySqlCommand1.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Xóa thành công", "Thông báo");
                                btnShow.PerformClick();
                            }
                        }
                        catch
                        {
                            SqlCommand mySqlCommand2 = Global.SQLConnection.CreateCommand();
                            mySqlCommand2.CommandText = "DELETE FROM tkbmacdinhsinhvien WHERE MaSinhVien = @MaSV";
                            mySqlCommand2.Parameters.AddWithValue("@MaSV", dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value);
                            mySqlCommand2.ExecuteNonQuery();

                            mySqlCommand2 = Global.SQLConnection.CreateCommand();
                            mySqlCommand2.CommandText = "DELETE FROM sinhvien WHERE MaSinhVien = @MaSV";
                            mySqlCommand2.Parameters.AddWithValue("@MaSV", dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value);
                            if (mySqlCommand2.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Xóa thành công", "Thông báo");
                                btnShow.PerformClick();
                            }
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtMSSV.Text =
            txtLastName.Text =
            txtFristName.Text = "";
            cboSex.SelectedIndex = 0;
            ComboBacHoc(cboBacHocAdd);
            pnlEdit.Visible = true;
            dateBorn.Value = DateTime.Parse("01/01/1990");
            cboBacHocAdd.SelectedIndex = 1;
            txtMSSV.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.Visible = false;
        }

        private void cboBacHocAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeHeDaoTao(cboBacHocAdd, cboFacultyAdd, cboClassAdd);
        }

        private void cboFacultyAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCboFaculty(cboFacultyAdd, cboClassAdd);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsModifyMode)
            {

            }
            else
            {
                if (CheckAddModeInfo())
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "INSERT INTO sinhvien (MaSinhVien, Ho, Ten, Phai, Lop, NgaySinh) VALUES (@MaSinhVien, @Ho, @Ten, @Phai, @Lop, @NgaySinh)";
                    mySqlCommand.Parameters.AddWithValue("@MaSinhVien", txtMSSV.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ho", txtLastName.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ten", txtFristName.Text);
                    mySqlCommand.Parameters.AddWithValue("@Phai", (cboSex.SelectedItem.ToString()) == "Nam" ? 1 : 0);
                    mySqlCommand.Parameters.AddWithValue("@Lop", ((SQLItem)cboClassAdd.SelectedItem).ID.ToString());
                    mySqlCommand.Parameters.AddWithValue("@NgaySinh", dateBorn.Value);
                    if (mySqlCommand.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Thêm mới thành công", "Thông báo");
                        cboClass.SelectedValue = ((SQLItem)cboClassAdd.SelectedItem).ID.ToString();
                        btnShow.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới không thực hiện được", "Thông báo");
                    }
                    pnlEdit.Visible = false;
                }
                else
                {
                }
            }
        }

        private Boolean CheckAddModeInfo()
        {
            Boolean ok = true;
            if (Global.RunScalar("select * from sinhvien where MaSinhVien = '" + txtMSSV.Text + "'") != null)
            {
                errorProvider.SetError(txtMSSV, "Mã số sinh viên bị trùng");
                ok = false;
            }
            else
            {
                if (txtMSSV.Text.Length != 8)
                {
                    errorProvider.SetError(txtMSSV, "Phải có đúng 8 ký tự");
                    ok = false;
                }
            }

            if (txtLastName.Text.Length > 20)
            {
                errorProvider.SetError(txtLastName, "Không được quá 20 ký tự");
                ok = false;
            }
            else if (txtLastName.Text == "")
            {
                errorProvider.SetError(txtLastName, "Chưa nhập");
                ok = false;
            }

            if (txtFristName.Text.Length > 10)
            {
                errorProvider.SetError(txtFristName, "Không được quá 10 ký tự");
                ok = false;
            }
            else if (txtFristName.Text == "")
            {
                errorProvider.SetError(txtFristName, "Chưa nhập");
                ok = false;
            }

            if (dateBorn.Value > DateTime.Now)
            {
                errorProvider.SetError(dateBorn, "Ngày Sinh chưa hợp lệ");
                ok = false;
            }
            else
                if (dateBorn.Value.Year + 17 > DateTime.Now.Year)
                {
                    errorProvider.SetError(dateBorn, "Chưa đủ tuổi học");
                    ok = false;
                }

            if (((SQLItem)cboClassAdd.SelectedItem).ID == null)
            {
                errorProvider.SetError(cboClassAdd, "Chưa chọn lớp");
                ok = false;
            }
            else
                if (Global.RunScalar("select * from lop where MaLop = '" + ((SQLItem)cboClassAdd.SelectedItem).ID.ToString() + "'") == null)
                {
                    errorProvider.SetError(cboClassAdd, "Lớp ko có trong hệ thống");
                    ok = false;
                }
            return ok;
        }
    }
}
