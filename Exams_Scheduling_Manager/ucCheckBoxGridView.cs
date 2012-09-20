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
    public partial class ucCheckBoxGridView : DataGridView
    {
        DataGridViewCheckBoxColumn CheckBoxCollumn;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        CheckBox HeaderCheckBox = null;
        bool IsHeaderCheckBoxClicked = false;

        public String CheckBoxCollumnName
        {
            get { return "Checked"; }
        }

        public IEnumerable<object> CheckedRows(String CollumnName)
        {
            foreach (DataGridViewRow Row in Rows)
            {
                if ((bool)((DataGridViewCheckBoxCell)Row.Cells[CheckBoxCollumnName]).Value == true)
                {
                    yield return Row.Cells[CollumnName].Value;
                }
            }
        }

        public IEnumerable<object> UnCheckedRows(String CollumnName)
        {
            foreach (DataGridViewRow Row in Rows)
            {
                if ((bool)((DataGridViewCheckBoxCell)Row.Cells[CheckBoxCollumnName]).Value != true)
                {
                    yield return Row.Cells[CollumnName].Value;
                }
            }
        }

        public void BeginUpdate()
        {
            if (HeaderCheckBox == null)
            {
                AddHeaderCheckBox();
                HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
                HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
                CellValueChanged += new DataGridViewCellEventHandler(dataGridView_CellValueChanged);
                CurrentCellDirtyStateChanged += new EventHandler(dataGridView_CurrentCellDirtyStateChanged);
                CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
                CellBeginEdit += new DataGridViewCellCancelEventHandler(TheCellBeginEdit);
                CheckBoxCollumn = new DataGridViewCheckBoxColumn();
                CheckBoxCollumn.Name = CheckBoxCollumnName;
                CheckBoxCollumn.HeaderText = "";
                Columns.Add(CheckBoxCollumn);
            }
        }
        public void EndUpdate()
        {
            ReadOnly = false;
            foreach (DataGridViewRow Row in Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[CheckBoxCollumnName]).Value = true;
            RefreshEdit();
            TotalCheckBoxes = RowCount;
            TotalCheckedCheckBoxes = RowCount;
        }
        private void TheCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
          if (e.ColumnIndex != 0) e.Cancel = true;
        }
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsHeaderCheckBoxClicked && this[e.ColumnIndex, e.RowIndex] == this[CheckBoxCollumnName, e.RowIndex])
                RowCheckBoxClick((DataGridViewCheckBoxCell)this[e.ColumnIndex, e.RowIndex]);
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (CurrentCell is DataGridViewCheckBoxCell)
                CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);
            HeaderCheckBox.ThreeState = true;
            //Add the CheckBox into the DataGridView
            this.Controls.Add(HeaderCheckBox);
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2;

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBox.Location = oPoint;
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckBoxClicked = true;
            if (HeaderCheckBox.CheckState == CheckState.Indeterminate)
            {
                HeaderCheckBox.CheckState = CheckState.Unchecked;
            }
            foreach (DataGridViewRow Row in Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[CheckBoxCollumnName]).Value = HCheckBox.Checked;

            RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsHeaderCheckBoxClicked = false;
        }

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                {
                    //HeaderCheckBox.Checked = false;
                    HeaderCheckBox.CheckState = CheckState.Unchecked;
                    if (TotalCheckedCheckBoxes != 0)
                    {
                        HeaderCheckBox.CheckState = CheckState.Indeterminate;
                    }
                }
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                {
                    //HeaderCheckBox.Checked = true;
                    HeaderCheckBox.CheckState = CheckState.Checked;
                }
            }
        }
    }
}
