using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exams_Scheduling_Manager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnDatabaseEditor_Click(object sender, EventArgs e)
        {
            frmDatabaseEditor frmData = new frmDatabaseEditor();
            this.Hide();
            frmData.ShowDialog();
            this.Show();
        }

        private void btnSheduling_Click(object sender, EventArgs e)
        {
            frmSheduling frmSheduling = new frmSheduling();
            this.Hide();
            frmSheduling.ShowDialog();
            this.Show();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
