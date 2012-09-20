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
    public partial class ucTeachers : UserControl
    {
        public ucTeachers()
        {
            InitializeComponent();
        }

        private Boolean IsModifyMode;

        private void ComboboxFacutly(ComboBox cbo)
        {
            cbo.Items.Add(new SQLItem(null, "?"));
            DataTable dTable = new DataTable();
            Global.FillTable("Select [Makhoa],[TenKhoa] from khoa ", dTable);
            foreach (DataRow dRow in dTable.Rows)
            {
                cbo.Items.Add(new SQLItem(dRow["MaKhoa"], dRow["TenKhoa"]));
            }
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

        private void ChangeFacutly(ComboBox cbokhoa, ComboBox cbobomon)
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
            panel.Enabled = false;
            pnlEdit.Visible = true;
            ComboboxFacutly(cboFacultyAdd);
            cboFacultyAdd.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            pnlEdit.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsModifyMode)
            {
                if (CheckMGV())
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "UPDATE giaovien"
                                                + " SET MaGiaoVien = @MaGV, HoLot = @HoLot, TenGiaoVien = @TenGV, BoMonQL = @BoMonQL, HocHam = @HocHam , HocVi = @HocVi " + " WHERE MaGiaoVien = @MaGVCu";

                    mySqlCommand.Parameters.AddWithValue("@MaGV", txtMGV.Text);
                    mySqlCommand.Parameters.AddWithValue("@HoLot", txtLastName.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenGV", txtFirstName.Text);
                    mySqlCommand.Parameters.AddWithValue("@BoMonQL", ((SQLItem)cboSubjectsAdd.SelectedItem).ID.ToString());
                    mySqlCommand.Parameters.AddWithValue("@HocHam", txtHocHam.Text);
                    mySqlCommand.Parameters.AddWithValue("@HocVi", txtHocVi.Text);
                    mySqlCommand.Parameters.AddWithValue("@MaGVCu", dataGridView.SelectedRows[0].Cells["MaGiaoVien"].Value.ToString());

                    if (mySqlCommand.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Sửa đổi thành công", "Thông báo");
                        panel.Enabled = true;
                        btnShow.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Sửa đổi không thực hiện được", "Thông báo");
                    }
                }
                pnlEdit.Visible = false;
                panel.Enabled = true;
            }
            else
            {
                if (CheckAddModeInfo())
                {
                    SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                    mySqlCommand.CommandText = "INSERT INTO giaovien (MaGiaoVien, HoLot, TenGiaoVien, BoMonQL, HocHam, HocVi) VALUES (@MaGV, @HoLot, @TenGV, @BoMonQL, @HocHam, @HocVi)";
                    mySqlCommand.Parameters.AddWithValue("@MaGV", txtMGV.Text);
                    mySqlCommand.Parameters.AddWithValue("@HoLot", txtLastName.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenGV", txtFirstName.Text);
                    mySqlCommand.Parameters.AddWithValue("@BoMonQL", ((SQLItem)cboSubjectsAdd.SelectedItem).ID.ToString());
                    mySqlCommand.Parameters.AddWithValue("@HocHam", txtHocHam.Text);
                    mySqlCommand.Parameters.AddWithValue("@HocVi", txtHocVi.Text);

                    if (mySqlCommand.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Thêm mới thành công", "Thông báo");
                        cboFaculty.SelectedIndex = 0;
                        foreach (SQLItem items in cboSubject.Items)
                        {
                            if (items != null)
                                if (((SQLItem)items).Name.ToString() == ((SQLItem)cboSubjectsAdd.SelectedItem).ID.ToString())
                                {
                                    cboSubject.SelectedItem = items;
                                    break;
                                }
                        }
                        panel.Enabled = true;
                        btnShow.PerformClick();
                    }
                    else
                        MessageBox.Show("Thêm mới không thực hiện được", "Thông báo");

                    panel.Enabled = true;
                    pnlEdit.Visible = false;
                }
            }

        }

        private Boolean CheckMGV()
        {
            Boolean ok = true;
            if (txtMGV.Text != dataGridView.SelectedRows[0].Cells["MaGiaoVien"].Value.ToString())
            {
                ok = true;
                if (Global.RunNonQuery("select * from giaovien where MaGiaoVien = " + txtMGV.Text) > 0)
                {
                    errorProvider.SetError(txtMGV, "Đã được sử dụng, đề nghị nhập cái khác");
                    ok = false;
                }
                else
                    if (txtMGV.Text.Length != 4 && txtMGV.Text.Length != 5)
                    {
                        errorProvider.SetError(txtMGV, "Phải có đúng 4 hoặc 5 ký tự");
                        ok = false;
                    }
            }
            return ok;
        }

        private Boolean CheckAddModeInfo()
        {
            Boolean ok = true;
            if (Global.RunScalar("select * from giaovien where MaGiaoVien = '" + txtMGV.Text + "'") != null)
            {
                errorProvider.SetError(txtMGV, "Mã giáo viên bị trùng");
                ok = false;
            }
            else
            {
                if (txtMGV.Text.Length != 4 && txtMGV.Text.Length != 5)
                {
                    errorProvider.SetError(txtMGV, "Phải có đúng 4 hoặc 5 ký tự");
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

            if (txtFirstName.Text.Length > 10)
            {
                errorProvider.SetError(txtFirstName, "Không được quá 10 ký tự");
                ok = false;
            }
            else if (txtFirstName.Text == "")
            {
                errorProvider.SetError(txtFirstName, "Chưa nhập");
                ok = false;
            }

            if (txtHocHam.Text.Length > 5)
            {
                errorProvider.SetError(txtHocHam, "Không được quá 5 ký tự");
                ok = false;
            }
            else if (txtHocHam.Text == "")
            {
                errorProvider.SetError(txtHocHam, "Chưa nhập");
                ok = false;
            }

            return ok;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Đề nghị chọn dòng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = 0; i < dataGridView.SelectedRows.Count; ++i)
                {
                    if (MessageBox.Show("Bạn có muốn xóa giáo viên có thông tin:\r\n_ Mã giáo viên: " + dataGridView.SelectedRows[i].Cells["MaGiaoVien"].Value + "\r\n_ Họ tên: " + dataGridView.SelectedRows[i].Cells["HoLot"].Value + " " + dataGridView.SelectedRows[i].Cells["TenGiaoVien"].Value, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SqlCommand mySqlCommand = Global.SQLConnection.CreateCommand();
                        mySqlCommand.CommandText = "DELETE FROM giaovien WHERE MaGiaoVien = @MaGV";
                        mySqlCommand.Parameters.AddWithValue("@MaGV", dataGridView.SelectedRows[i].Cells["MaGiaoVien"].Value);
                        try
                        {
                            if (mySqlCommand.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Xóa thành công", "Thông báo");
                                //   btnShow.PerformClick();
                            }
                        }
                        catch
                        {
                            if (MessageBox.Show("Giáo viên này đã đăng ký tiết dạy rồi. Bạn có thật sự muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                mySqlCommand = Global.SQLConnection.CreateCommand();
                                mySqlCommand.CommandText = "DELETE FROM bangphancongday WHERE MaGiaoVien = @MaGV";
                                mySqlCommand.Parameters.AddWithValue("@MaGV", dataGridView.SelectedRows[i].Cells["MaGiaoVien"].Value);
                                mySqlCommand.ExecuteNonQuery();

                                mySqlCommand = Global.SQLConnection.CreateCommand();
                                mySqlCommand.CommandText = "DELETE FROM giaovien WHERE MaGiaoVien = @MaGV";
                                mySqlCommand.Parameters.AddWithValue("@MaGV", dataGridView.SelectedRows[i].Cells["MaGiaoVien"].Value);
                                if (mySqlCommand.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show("Xóa thành công", "Thông báo");
                                }
                            }
                        }
                    }
                }
            }
            panel.Enabled = true;
            btnShow.PerformClick();
        }

        private void cboFacultyAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFacutly(cboFacultyAdd, cboSubjectsAdd);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Đề nghị chọn dòng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                IsModifyMode = true;
                panel.Enabled = false;
                pnlEdit.Visible = true;

                txtMGV.Text = dataGridView.SelectedRows[0].Cells["MaGiaoVien"].Value.ToString();
                txtLastName.Text = dataGridView.SelectedRows[0].Cells["HoLot"].Value.ToString();
                txtFirstName.Text = dataGridView.SelectedRows[0].Cells["TenGiaoVien"].Value.ToString();
                txtHocHam.Text = dataGridView.SelectedRows[0].Cells["HocHam"].Value.ToString();
                txtHocVi.Text = dataGridView.SelectedRows[0].Cells["HocVi"].Value.ToString();
                ComboboxFacutly(cboFacultyAdd);

                DataTable dTable = new DataTable();
                Global.FillTable("Select [KhoaQL],[MaBoMon] From giaovien, bomon where BoMonQL = MaBoMon and MaGiaoVien= '" + txtMGV.Text + "'", dTable);


                bool ok = true;
                foreach (DataRow dRow in dTable.Rows)
                {
                    foreach (SQLItem item in cboFacultyAdd.Items)
                        if (item.ID != null)
                            if (item.ID.ToString() == dRow["KhoaQL"].ToString())
                            {
                                cboFacultyAdd.SelectedItem = item;
                                ok = false;
                                break;
                            }
                    if (!ok)
                        break;
                }

                ok = true;
                foreach (DataRow dRow in dTable.Rows)
                {
                    foreach (SQLItem item in cboSubjectsAdd.Items)
                        if (item.ID != null)
                            if (item.ID.ToString() == dRow["MaBoMon"].ToString())
                            {
                                cboSubjectsAdd.SelectedItem = item;
                                ok = false;
                                break;
                            }
                    if (!ok)
                        break;
                }
            }
        }
    }
}
