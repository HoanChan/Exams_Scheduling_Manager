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
    public partial class ucRoom : UserControl
    {
        public ucRoom()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Global.ShowOnGridView(dataGridView, "select * from phong where MaPhong like '" + txtSearch.Text + "%'");
        }
    }
}
