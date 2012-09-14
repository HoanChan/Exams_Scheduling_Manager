using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Database_Viewer
{
    public class DatabaseViewer
    {
        DATABASE dbViewer;
        Button[] butOpenTable;
        Button[] butOpenView;
        string ConnectionString;
        private enum ButtonSelection
        {
            Button1 = 1,
            Button2 = 2,
            Cancel = 3
        }
        public DatabaseViewer(string _Connection)
        {
            ConnectionString = _Connection;
        }
        public void Show(string Title)
        {
            Form frmDatabaseViewer = new Form();
            frmDatabaseViewer.Text = Title;
            frmDatabaseViewer.FormClosing += new FormClosingEventHandler(frmDatabaseViewer_FormClosing);
            dbViewer = new DATABASE(ConnectionString);
            int ButtonWidth = 200;
            int ButtonHeight = 25;
            int ButtonPerLine = 3;
            butOpenTable = new Button[dbViewer.Tables.Count];
            for (int i = 0; i < dbViewer.Tables.Count; i++)
            {
                butOpenTable[i] = new Button();
                butOpenTable[i].SetBounds(10 + i % ButtonPerLine * (ButtonWidth + 5), 10 + (int)(i / ButtonPerLine) * (ButtonHeight + 5), ButtonWidth, ButtonHeight);
                butOpenTable[i].Text = dbViewer.Tables[i];
                butOpenTable[i].Click += new EventHandler(butOpenTable_Click);
            }
            frmDatabaseViewer.Controls.AddRange(butOpenTable);
            butOpenView = new Button[dbViewer.Views.Count];
            for (int i = 0; i < dbViewer.Views.Count; i++)
            {
                butOpenView[i] = new Button();
                butOpenView[i].SetBounds(10 + i % ButtonPerLine * (ButtonWidth + 5), (int)(dbViewer.Tables.Count / ButtonPerLine + (int)(i / ButtonPerLine) + 1) * (ButtonHeight + 5) + 20, ButtonWidth, ButtonHeight);
                butOpenView[i].Text = dbViewer.Views[i];
                butOpenView[i].Click += new EventHandler(butOpenView_Click);
            }
            frmDatabaseViewer.Controls.AddRange(butOpenView);
            frmDatabaseViewer.ClientSize = new Size(ButtonPerLine * (ButtonWidth + 5) + 15, (int)(dbViewer.Tables.Count / ButtonPerLine + dbViewer.Views.Count / ButtonPerLine + 2) * (ButtonHeight + 5) + 25);
            frmDatabaseViewer.ShowDialog();
        }
        private void frmDatabaseViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbViewer.Close();
            //base.OnClosing(e);
        }
        private void butOpenTable_Click(object sender, EventArgs e)
        {
            Button Me = sender as Button;
            int Index = 0;
            for (int i = 0; i < butOpenTable.Length; i++)
                if (Me == butOpenTable[i])
                {
                    Index = i;
                    break;
                }
            ButtonSelection btnSe = ShowSelection("Chọn chế độ hiển thị", "Hiện &Form", "Hiện &bảng");
            if (btnSe == ButtonSelection.Button1)
                dbViewer.Show(butOpenTable[Index].Text, butOpenTable[Index].Text, true);
            else if (btnSe == ButtonSelection.Button2)
                dbViewer.ShowDataGridView(butOpenTable[Index].Text, butOpenTable[Index].Text);
        }
        private void butOpenView_Click(object sender, EventArgs e)
        {
            Button Me = sender as Button;
            int Index = 0;
            for (int i = 0; i < butOpenView.Length; i++)
                if (Me == butOpenView[i])
                {
                    Index = i;
                    break;
                }
            ButtonSelection btnSe = ShowSelection("Chọn chế độ hiển thị", "Hiện Form", "Hiện bảng");
            if (btnSe == ButtonSelection.Button1)
                dbViewer.Show(butOpenView[Index].Text, butOpenView[Index].Text, false);
            else if (btnSe == ButtonSelection.Button2)
                dbViewer.ShowDataGridView(butOpenView[Index].Text, butOpenView[Index].Text);
        }
        private ButtonSelection ShowSelection(string title, string Button1Text, string Button2Text)
        {
            Button btn1 = new Button();
            Button btn2 = new Button();
            btn1.Text = Button1Text;
            btn1.SetBounds(10, 10, 75, 23);
            btn1.DialogResult = DialogResult.Yes;
            btn2.Text = Button2Text;
            btn2.DialogResult = DialogResult.No;
            btn2.SetBounds(95, 10, 75, 23);
            Form frmInputBinary = new Form();
            frmInputBinary.Text = title;
            frmInputBinary.ClientSize = new Size(btn2.Right + 10, btn2.Bottom + 10);
            frmInputBinary.Controls.AddRange(new Control[] { btn1, btn2 });
            frmInputBinary.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmInputBinary.StartPosition = FormStartPosition.CenterScreen;
            frmInputBinary.MinimizeBox = false;
            frmInputBinary.MaximizeBox = false;
            DialogResult dialogResult = frmInputBinary.ShowDialog();
            if (dialogResult == DialogResult.Yes)
                return ButtonSelection.Button1;
            if (dialogResult == DialogResult.No)
                return ButtonSelection.Button2;
            return ButtonSelection.Cancel;
        }
    }
}
