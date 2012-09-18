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
        private Boolean IsModifyMode = false;
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
                    cboManagerSubject.Items.Add(new SQLItem(dRow["MaBoMon"], dRow["TenBoMon"]));
                }
                cboSubject.Items.Add(new SQLItem(null, "?"));
                cboSubject.SelectedIndex = 0;

                DataTable dTable2 = new DataTable();
                Global.FillTable("Select * From Khoa", dTable2);
                foreach (DataRow dRow in dTable2.Rows)
                {
                    cboFaculty.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                    cboSchedulingBy.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
                }
            }
            pnlEdit.Left = (Width - pnlEdit.Width) / 2;
            pnlEdit.Top = (dataGridView.Height - pnlEdit.Height) / 2;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cboFaculty.Enabled)
            {
                Global.ShowOnGirdView(dataGridView, "SELECT [MaMonHoc],[TenMonHoc],[TCLyThuyet],[TCThucHanh],[MonThiNghiem],[KhoaXepLich],[BoMonQL],[GhiChu]"
                                                    + " FROM monhoc, khoa, bomon"
                                                    + " WHERE MaKhoa = " + ((SQLItem)cboFaculty.SelectedItem).ID + " and MaKhoa = KhoaQL and BoMonQL = MaBoMon");
            }
            else
            {
                Global.ShowOnGirdView(dataGridView, "Select * from monhoc where BoMonQL = " + ((SQLItem)cboSubject.SelectedItem).ID);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtID.Text =
            txtName.Text =
            txtInfo.Text = "";
            nudPracticesCredit.Value =
            nudTheoryCredit.Value = 0;
            chbIsExperimentalSubject.Checked = false;
            cboSchedulingBy.SelectedIndex = -1;
            cboManagerSubject.SelectedIndex = -1;
            IsModifyMode = false;
            pnlEdit.Visible = true;
            pnlCommands.Enabled = false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsModifyMode)
            {
                if (CheckModifyModeInfo())
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "UPDATE MonHoc"
                                                + " SET MaMonHoc = @MaMonHoc, TenMonHoc = @TenMonHoc, TCLyThuyet = @TCLyThuyet, TCThucHanh = @TCThucHanh, MonThiNghiem = @MonThiNghiem, KhoaXepLich = @KhoaXepLich, BoMonQL = @BoMonQL, GhiChu = @GhiChu"
                                                + " WHERE MaMonHoc = @MaMonHocCu";
                    mySqlCommand.Parameters.AddWithValue("@MaMonHoc", txtID.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenMonHoc", txtName.Text);
                    mySqlCommand.Parameters.AddWithValue("@TCLyThuyet", nudTheoryCredit.Value);
                    mySqlCommand.Parameters.AddWithValue("@TCThucHanh", nudPracticesCredit.Value);
                    mySqlCommand.Parameters.AddWithValue("@MonThiNghiem", (chbIsExperimentalSubject.Checked ? 1 : 0));
                    mySqlCommand.Parameters.AddWithValue("@KhoaXepLich", (cboSchedulingBy.SelectedIndex == -1 ? DBNull.Value : ((SQLItem)cboSchedulingBy.SelectedItem).ID));
                    mySqlCommand.Parameters.AddWithValue("@BoMonQL", ((SQLItem)cboManagerSubject.SelectedItem).ID);
                    mySqlCommand.Parameters.AddWithValue("@GhiChu", (txtInfo.Text.Length == 0 ? DBNull.Value : (object)txtInfo.Text));
                    mySqlCommand.Parameters.AddWithValue("@MaMonHocCu", dataGridView.SelectedRows[0].Cells["MaMonHoc"].Value.ToString());
                    if (mySqlCommand.ExecuteNonQuery() != -1)
                    {
                        MessageBox.Show("Sửa đổi thành công", "Thông báo");
                        btnShow.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Sửa đổi không thực hiện được", "Thông báo");
                    }
                    pnlEdit.Visible = false;
                    pnlCommands.Visible = true;
                }
            }
            else
            {
                if (CheckAddModeInfo())
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "INSERT INTO MonHoc (MaMonHoc, TenMonHoc, TCLyThuyet, TCThucHanh, MonThiNghiem, KhoaXepLich, BoMonQL, GhiChu)"
                                                + " VALUES (@MaMonHoc, @TenMonHoc, @TCLyThuyet, @TCThucHanh, @MonThiNghiem, @KhoaXepLich, @BoMonQL, @GhiChu)";
                    mySqlCommand.Parameters.AddWithValue("@MaMonHoc", txtID.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenMonHoc", txtName.Text);
                    mySqlCommand.Parameters.AddWithValue("@TCLyThuyet", nudTheoryCredit.Value);
                    mySqlCommand.Parameters.AddWithValue("@TCThucHanh", nudPracticesCredit.Value);
                    mySqlCommand.Parameters.AddWithValue("@MonThiNghiem", (chbIsExperimentalSubject.Checked ? 1 : 0));
                    mySqlCommand.Parameters.AddWithValue("@KhoaXepLich", (cboSchedulingBy.SelectedIndex == -1 ? DBNull.Value : ((SQLItem)cboSchedulingBy.SelectedItem).ID));
                    mySqlCommand.Parameters.AddWithValue("@BoMonQL", ((SQLItem)cboManagerSubject.SelectedItem).ID);
                    mySqlCommand.Parameters.AddWithValue("@GhiChu", (txtInfo.Text.Length == 0 ? DBNull.Value : (object)txtInfo.Text));

                    if (mySqlCommand.ExecuteNonQuery() != -1)
                    {
                        MessageBox.Show("Thêm mới thành công", "Thông báo");
                        btnShow.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới không thực hiện được", "Thông báo");
                    }
                    pnlEdit.Visible = false;
                    pnlCommands.Visible = true;
                }
            }
        }
        private Boolean CheckInfo()
        {
            Boolean Ok = true;

            if (txtID.Text.Length != 7)
            {
                errorProvider.SetError(txtID, "Phải có đúng 7 ký tự");
                Ok = false;
            }

            if (txtName.Text.Length > 60)
            {
                errorProvider.SetError(txtName, "Không được quá 60 ký tự");
                Ok = false;
            }
            else if (txtName.Text == "")
            {
                errorProvider.SetError(txtName, "Chưa nhập");
                Ok = false;
            }

            if (txtInfo.Text.Length > 500)
            {
                errorProvider.SetError(txtInfo, "Không được quá 500 ký tự");
                Ok = false;
            }

            if (cboManagerSubject.SelectedIndex == -1)
            {
                errorProvider.SetError(cboManagerSubject, "Chưa chọn");
                Ok = false;
            }
            return Ok;
        }
        private Boolean CheckAddModeInfo()
        {
            Boolean Ok = true;
            if (Global.RunNonQuery("select * from monhoc where MaMonHoc = " + txtID.Text) != -1)
            {
                errorProvider.SetError(txtID, "Đã được sử dụng, đề nghị nhập cái khác");
                Ok = false;
            }
            if (!CheckInfo())
            {
                Ok = false;
            }
            return Ok;
        }
        private Boolean CheckModifyModeInfo()
        {
            Boolean Ok = true;
            if (txtID.Text != dataGridView.SelectedRows[0].Cells["MaMonHoc"].Value.ToString())
            {
                if (Global.RunNonQuery("select * from monhoc where MaMonHoc = " + txtID.Text) != -1)
                {
                    errorProvider.SetError(txtID, "Đã được sử dụng, đề nghị nhập cái khác");
                    Ok = false;
                }
            }
            if (!CheckInfo())
            {
                Ok = false;
            }
            return Ok;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.Visible = false;
            pnlCommands.Visible = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Đề nghị chọn dòng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Bạn có muốn xóa môn học có mã là: " + dataGridView.SelectedRows[0].Cells["MaMonHoc"].Value.ToString() + " và có tên là: " + dataGridView.SelectedRows[0].Cells["TenMonHoc"].Value.ToString() + " hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                mySqlCommand.CommandText = "DELETE FROM MonHoc WHERE MaMonHoc = @MaMonHoc";
                mySqlCommand.Parameters.AddWithValue("@MaMonHoc", dataGridView.SelectedRows[0].Cells["MaMonHoc"].Value.ToString());

                if (mySqlCommand.ExecuteNonQuery() != -1)
                {
                    MessageBox.Show("Xóa thành công", "Thông báo");
                    btnShow.PerformClick();
                }
                else
                {
                    MessageBox.Show("Xóa không thực hiện được", "Thông báo");
                }
            }
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Đề nghị chọn dòng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtID.Text = dataGridView.SelectedRows[0].Cells["MaMonHoc"].Value.ToString();
                txtName.Text = dataGridView.SelectedRows[0].Cells["TenMonHoc"].Value.ToString();
                txtInfo.Text = dataGridView.SelectedRows[0].Cells["GhiChu"].Value.ToString();
                nudPracticesCredit.Value = Convert.ToDecimal(dataGridView.SelectedRows[0].Cells["TCThucHanh"].Value);
                nudTheoryCredit.Value = Convert.ToDecimal(dataGridView.SelectedRows[0].Cells["TCLyThuyet"].Value);
                chbIsExperimentalSubject.Checked = Convert.ToDecimal(dataGridView.SelectedRows[0].Cells["MonThiNghiem"].Value) == 1 ? true : false;
                foreach (SQLItem item in cboSchedulingBy.Items)
                {
                    if (item.ID.ToString() == dataGridView.SelectedRows[0].Cells["KhoaXepLich"].Value.ToString())
                    {
                        cboSchedulingBy.SelectedItem = item;
                        break;
                    }
                }
                foreach (SQLItem item in cboManagerSubject.Items)
                {
                    if (item.ID.ToString() == dataGridView.SelectedRows[0].Cells["BoMonQL"].Value.ToString())
                    {
                        cboManagerSubject.SelectedItem = item;
                        break;
                    }
                }
                IsModifyMode = true;
                pnlEdit.Visible = true;
                pnlCommands.Enabled = false;
            }
            
        }
    }
}
