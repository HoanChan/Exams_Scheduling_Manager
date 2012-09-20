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
    public partial class ucCourseRegistration : UserControl
    {
        public ucCourseRegistration()
        {
            InitializeComponent();
        }
        public void ShowStudent(String StudentID)
        {
            txtStudentID.Text = StudentID;
            btnShow.PerformClick();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            //if (Global.RunNonQuery("select * from pdkmh where MaSinhVien = '" + txtStudentID.Text + "'") != -1)
            //{
                String Query = "SELECT monhoc.MaMonHoc, TenMonHoc,lichhocvu.Nhom,(TCLyThuyet + TCThucHanh) as SoTC, (giaovien.HoLot + ' ' + giaovien.TenGiaoVien) as GiaoVien, Thu, TietBD, SoTiet, MaPhong as Phong, TuanBD, SoTuan"
                                + " FROM pdkmh, monhoc, lichhocvu, bangphancongday, giaovien"
                                + " WHERE pdkmh.MaMonHoc = monhoc.MaMonHoc and monhoc.MaMonHoc = lichhocvu.MaMonHoc and pdkmh.Nhom = lichhocvu.Nhom and monhoc.MaMonHoc = bangphancongday.MaMonHoc and lichhocvu.Nhom = bangphancongday.Nhom and bangphancongday.MaGiaoVien = giaovien.MaGiaoVien"
                                + " and pdkmh.MaSinhVien = '" + txtStudentID.Text + "'";
                Global.ShowOnGridView(dataGridView, Query);
            //}
            //else
            //{
            //    MessageBox.Show("Tìm không thấy!", "Thông báo");
            //}
        }
    }
}
