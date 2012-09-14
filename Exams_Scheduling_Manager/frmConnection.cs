using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Exams_Scheduling_Manager
{
    public partial class frmConnection : Form
    {
        public frmConnection()
        {
            InitializeComponent();
            cboAuthentication.SelectedIndex = 0;
        }

        private void cboAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 0)
            {
                txtPassword.Enabled =
                lblPassword.Enabled =
                txtUserName.Enabled =
                lblUserName.Enabled = false;
            }
            else
            {
                txtPassword.Enabled =
                lblPassword.Enabled =
                txtUserName.Enabled =
                lblUserName.Enabled = true;
            }
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnectionString;
                if (cboAuthentication.SelectedIndex == 0)
                {
                    ConnectionString = string.Format("Data Source = '{0}';Integrated Security = SSPI", txtServerName.Text);
                }
                else
                {
                    ConnectionString = string.Format("Data Source = '{0}'; User Id = '{1}'; Password = '{2}';", txtServerName.Text, txtUserName.Text, txtPassword.Text);

                }
                SqlConnection SqlCon = new SqlConnection(ConnectionString);
                SqlCon.Open();
                SqlCommand SqlCom = new SqlCommand();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandType = CommandType.Text;
                SqlCom.CommandText = "SELECT name FROM SYS.DATABASES WHERE owner_sid <> 0x01";
                SqlDataReader SqlDR;
                SqlDR = SqlCom.ExecuteReader();
                cboDatabases.Items.Clear();
                while (SqlDR.Read())
                {
                    cboDatabases.Items.Add(SqlDR.GetString(0));
                }
                cboDatabases.SelectedIndex = 0;
                btnOK.Enabled = 
                lblDatabases.Enabled =
                cboDatabases.Enabled = true;
                SqlDR.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Kết nối tới Server thất bại!\nĐề nghị kiểm tra lại các thông số kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnOK.Enabled =
                lblDatabases.Enabled =
                cboDatabases.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string ConnectionString;
            if (cboAuthentication.SelectedIndex == 0)
            {
                ConnectionString = string.Format("Data Source = '{0}';Integrated Security = SSPI; Database = '{1}'", txtServerName.Text, cboDatabases.Text);
            }
            else
            {
                ConnectionString = string.Format("Data Source = '{0}'; User Id = '{1}'; Password = '{2}'; Database = '{3}'", txtServerName.Text, txtUserName.Text, txtPassword.Text, cboDatabases.Text);

            }
            frmDatabaseEditor frmDBViewer = new frmDatabaseEditor(ConnectionString);
            this.Hide();
            frmDBViewer.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            this.Height += lblAbout.Visible ? -65 : 65;
            lblAbout.Visible = !lblAbout.Visible;
            btnAbout.Text = btnAbout.Text.Substring(0, btnAbout.Text.Length - 2) + (lblAbout.Visible ? "<<" : ">>");
        }

        
    }
}
