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

        private String TenBacHoc(String MaBacHoc)
        {
            switch (MaBacHoc)
            {
                case "0": return "0";
                case "1": return "Đại học chính quy";
                case "2": return "Đại học chính quy Khối K - 3/7";
                case "3": return "Đại học chính quy hệ chuyển tiếp";
                case "4": return "Đại học tại chức";
                case "5": return "Đại học tại chức khối K - 3/7";
                case "6": return "Đại học tại chức hệ hoàn chỉnh";
                case "7": return "Cao đẳng chính quy";
                case "8": return "8";
                case "9": return "9";
                case "B": return "ĐHCQ (Bằng thứ 2)";
                case "D": return "Trung cấp chuyên nghiệp chính quy";
                case "F": return "F";
                case "S1": return "K08101SP";
                case "T1": return "T1";
                case "T2": return "T2";
                case "T3": return "T3";
                case "T4": return "T4";
                case "T5": return "T5";
                case "T7": return "T7";
                case "T9": return "ĐHCQ - Các ngành Sư phạm kỹ thuật";
            }
            return "";
        }

        private void ComboBacHoc(ComboBox cbo)
        {
            DataTable dTable3 = new DataTable();
            Global.FillTable("Select * From bachoc", dTable3);
            foreach (DataRow dRow in dTable3.Rows)
            {
                string MaBacHoc = dRow["MaBacHoc"].ToString().Trim();
                cbo.Items.Add(new SQLItem(dRow["MaBacHoc"], (object)TenBacHoc(MaBacHoc)));
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
            Global.ShowOnGirdView(dataGridView, "Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh],[Lop] From sinhvien Where Lop = '" + cboClass.SelectedItem.ToString() + "'");
        }

        private void ShowFaculty()
        {
            String themHeDaoTao = ((SQLItem)cboHeDaoTao.SelectedItem).ID != null ? " and khoi.BacHoc= '" + ((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString() + "'" : "";
            Global.ShowOnGirdView(dataGridView, "select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh],[Lop] from sinhvien, khoi, khoa, lop where sinhvien.lop = lop.MaLop and lop.MaKhoi = khoi.MaKhoi and khoa.MaKhoa = khoi.KhoaQL and khoa.MaKhoa = '" + ((SQLItem)cboFaculty.SelectedItem).ID.ToString() + "'" + themHeDaoTao);

        }

        private void ShowHeDaoTao()
        {
            Global.ShowOnGirdView(dataGridView, "Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh],[Lop] From sinhvien, lop, khoi  where lop = Malop and lop.Makhoi = khoi.MaKhoi and BacHoc = '" + ((SQLItem)cboHeDaoTao.SelectedItem).ID.ToString() + "'");
        }

        private void ShowAllStudents()
        {
            Global.ShowOnGirdView(dataGridView, "Select [MaSinhVien],[Ho],[Ten],[Phai],[NgaySinh] From sinhvien");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
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
            panel1.Enabled = true;
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
            panel1.Enabled = false;
            txtMSSV.Text =
            txtLastName.Text =
            txtFristName.Text = "";
            cboSex.SelectedIndex = 0;
            ComboBacHoc(cboBacHocAdd);
            pnlEdit.Visible = true;
            dateBorn.Value = DateTime.Parse("01/01/1990");
            cboBacHocAdd.SelectedIndex = 1;
            txtMSSV.Focus();
            IsModifyMode = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
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
                if (CheckMSSV())
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "UPDATE sinhvien"
                                                + " SET MaSinhVien = @MaSinhVien, Ho = @Ho, Ten = @Ten, Phai = @Phai, Lop = @Lop, NgaySinh = @NgaySinh"
                                                + " WHERE MaSinhVien = @MaSVCu";
                    mySqlCommand.Parameters.AddWithValue("@MaSinhVien", txtMSSV.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ho", txtLastName.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ten", txtFristName.Text);
                    mySqlCommand.Parameters.AddWithValue("@Phai", (cboSex.SelectedItem.ToString()) == "Nam" ? 1 : 0);
                    mySqlCommand.Parameters.AddWithValue("@NgaySinh", dateBorn.Value);
                    mySqlCommand.Parameters.AddWithValue("@Lop", ((SQLItem)cboClassAdd.SelectedItem).ID.ToString());
                    mySqlCommand.Parameters.AddWithValue("@MaSVCu", dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value.ToString());

                    if (mySqlCommand.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Sửa đổi thành công", "Thông báo");
                        panel1.Enabled = true;
                        btnShow.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Sửa đổi không thực hiện được", "Thông báo");
                    }
                }
                pnlEdit.Visible = false;
                panel1.Enabled = true;
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

                    try
                    {
                        if (mySqlCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thêm mới thành công", "Thông báo");
                            cboHeDaoTao.SelectedIndex = 1;
                            btnShow.PerformClick();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thêm mới không thực hiện được", "Thông báo");
                    }
                    panel1.Enabled = true;
                    pnlEdit.Visible = false;
                }
                else
                {
                }
            }
        }

        private Boolean CheckMSSV()
        {
            Boolean Ok = false;
            if (txtMSSV.Text != dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value.ToString())
            {
                Ok = true;
                if (Global.RunNonQuery("select * from monhoc where MaMonHoc = " + txtMSSV.Text) > 0)
                {
                    errorProvider.SetError(txtMSSV, "Đã được sử dụng, đề nghị nhập cái khác");
                    Ok = false;
                }
                else
                    Ok = CheckInfo(1);
            }
            return Ok;
        }

        private Boolean CheckLastName()
        {
            Boolean ok = false;
            if (txtLastName.Text != dataGridView.SelectedRows[0].Cells["Ho"].Value.ToString())
                ok = CheckInfo(2);
            return ok;
        }

        private Boolean CheckFirstName()
        {
            Boolean ok = false;
            if (txtFristName.Text != dataGridView.SelectedRows[0].Cells["Ten"].Value.ToString())
                ok = CheckInfo(3);
            return ok;
        }

        private Boolean CheckDateBorn()
        {
            Boolean ok = false;
            if (dateBorn.Value != DateTime.Parse(dataGridView.SelectedRows[0].Cells["NgaySinh"].Value.ToString()))
                ok = CheckInfo(4);
            return ok;
        }

        private Boolean CheckClass()
        {
            Boolean ok = false;
            if (((SQLItem)cboClassAdd.SelectedItem).ID.ToString() != dataGridView.SelectedRows[0].Cells["Lop"].Value.ToString())
                ok = CheckInfo(5);
            return ok;
        }

        private Boolean CheckInfo(int i)
        {
            Boolean ok = true;
            if (i == 1)
            {
                if (txtMSSV.Text.Length != 8)
                {
                    errorProvider.SetError(txtMSSV, "Phải có đúng 8 ký tự");
                    ok = false;
                }
            }
            else
                if (i == 2)
                {
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
                }
                else
                    if (i == 3)
                    {
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
                    }
                    else
                        if (i == 4)
                        {

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
                        }
                        else
                        {
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
                        }

            return ok;
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
                ok = CheckInfo(1);
            }

            ok = CheckInfo(2);
            ok = CheckInfo(3);
            ok = CheckInfo(4);
            ok = CheckInfo(5);

            return ok;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Đề nghị chọn dòng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                panel1.Enabled = false;
                IsModifyMode = false;
                txtMSSV.Text = dataGridView.SelectedRows[0].Cells["MaSinhVien"].Value.ToString();
                txtLastName.Text = dataGridView.SelectedRows[0].Cells["Ho"].Value.ToString();
                txtFristName.Text = dataGridView.SelectedRows[0].Cells["Ten"].Value.ToString();
                dateBorn.Value = DateTime.Parse(dataGridView.SelectedRows[0].Cells["NgaySinh"].Value.ToString());
                cboSex.SelectedIndex = (dataGridView.SelectedRows[0].Cells["Phai"].Value.ToString() == "1") ? 0 : 1;
                IsModifyMode = true;
                ComboBacHoc(cboBacHocAdd);

                String lop = dataGridView.SelectedRows[0].Cells["Lop"].Value.ToString();

                DataTable dTable = new DataTable();
                Global.FillTable("Select [BacHoc],[KhoaQL] From Khoi,lop where lop.Makhoi = Khoi.Makhoi and Malop= '" + lop + "'", dTable);
                bool ok = true;

                foreach (DataRow dRow in dTable.Rows)
                {
                    foreach (SQLItem item in cboBacHocAdd.Items)
                        if (item.ID.ToString() == dRow["BacHoc"].ToString())
                        {
                            cboBacHocAdd.SelectedItem = item;
                            ok = false;
                            break;
                        }
                    if (!ok)
                        break;
                }


                ok = true;

                foreach (DataRow dRow in dTable.Rows)
                {
                    foreach (SQLItem item in cboFacultyAdd.Items)
                    {
                        if (item.ID != null)
                            if (item.ID.ToString() == dRow["KhoaQL"].ToString())
                            {
                                cboFacultyAdd.SelectedItem = item;
                                ok = false;
                                break;
                            }
                    }
                    if (!ok)
                        break;
                }


                foreach (SQLItem items in cboClassAdd.Items)
                {
                    if (items.ID != null)
                        if (items.ID.ToString() == lop)
                            cboClassAdd.SelectedItem = items;
                }
                pnlEdit.Visible = true;
            }
        }
    }
}
