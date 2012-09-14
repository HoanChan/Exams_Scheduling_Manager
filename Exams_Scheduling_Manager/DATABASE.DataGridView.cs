using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Globalization;
namespace Exams_Scheduling_Manager
{
	partial class DATABASE
	{
		DataGridView dgvShowData;
		Form frmDataGridView;
		public void ShowDataGridView(string title, string TableName)
		{
			dgvShowData = new DataGridView();
			frmDataGridView = new Form();
			dgvShowData.AllowUserToAddRows = false;
			dgvShowData.AllowUserToDeleteRows = false;
			dgvShowData.Dock = DockStyle.Fill;
			dgvShowData.ReadOnly = true;
			dgvShowData.AutoSize = true;
			dgvShowData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgvShowData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            tableData = new DataTable();
            tableData.Load(Table(TableName));
            BindingSource bindingSource = new BindingSource();
            dgvShowData.DataSource = bindingSource;
            bindingSource.DataSource = tableData;

			frmDataGridView.Text = title;            
			frmDataGridView.Controls.Add(dgvShowData);
			frmDataGridView.FormBorderStyle = FormBorderStyle.FixedSingle;
			frmDataGridView.MinimizeBox = false;
			frmDataGridView.MaximizeBox = false;
			frmDataGridView.Shown += new EventHandler(BaseDataGridView_Shown);
			dgvShowData.Show();
			DialogResult dialogResult = frmDataGridView.ShowDialog();
		}

		private void BaseDataGridView_Shown(object sender, EventArgs e)
		{
			for (int RowIndex = 0; RowIndex < dgvShowData.Rows.Count; RowIndex++)
			{
				if (RowIndex % 2 == 0)
					dgvShowData.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(223, 223, 223);
				else
					dgvShowData.Rows[RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(233, 233, 233); ;
			}
			int WS = 45;
			int HS = 25;
			int ScreenWidth = Screen.GetWorkingArea(frmDataGridView).Width;
			int ScreenHeight = Screen.GetWorkingArea(frmDataGridView).Height;
			for (int i = 0; i < dgvShowData.Rows.Count; i++) HS += dgvShowData.Rows[i].Height;
			for (int i = 0; i < dgvShowData.Columns.Count; i++) WS += dgvShowData.Columns[i].Width;
			if (WS > ScreenWidth - 100) WS = ScreenWidth - 100;
			if (HS > ScreenHeight - 100) HS = ScreenHeight - 100;
			if (WS == ScreenWidth - 100 || HS == ScreenHeight - 100)
			{
				frmDataGridView.FormBorderStyle = FormBorderStyle.Sizable;
				WS += 15;
				HS += 15;
			}
			frmDataGridView.ClientSize = new Size(WS, HS);
			frmDataGridView.Location = new Point((Screen.PrimaryScreen.Bounds.Width - frmDataGridView.Size.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmDataGridView.Size.Height) / 2);
		}
	}
}
