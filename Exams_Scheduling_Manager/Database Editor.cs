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
    public partial class frmDatabaseEditor : Form
    {
        DATABASE dbViewer;
        public frmDatabaseEditor(String ConnectionString)
        {
            InitializeComponent();
            dbViewer = new DATABASE(ConnectionString);
        }

        private void frmDatabaseEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
