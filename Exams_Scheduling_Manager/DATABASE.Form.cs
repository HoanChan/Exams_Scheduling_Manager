using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace Exams_Scheduling_Manager
{
    partial class DATABASE
    {
        private Form DialogForm;
        private Label[] label;
        private MultiTypeInputBox[] textBox;
        private Button btnNext;
        private Button btnGoTo;
        private Button btnPrevious;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnInsert;
        private object[] BinaryData;
        /// <summary>
        /// Hiển thị hộp thoại thao tác dữ liệu của một bảng
        /// </summary>
        /// <param name="title">Tiêu đề hộp thoại</param>
        /// <param name="aName">Tên Table / View cần thao tác</param>
        /// <param name="isTable">Nó có phải là bảng không?</param>
        public void Show(string title, string aName, bool isTable)
        {
            DialogForm = new Form();
            TableName = aName;
            //SQLReader = ColumnList(TableName);
            tableDataType = new DataTable();
            tableDataType.Load(ColumnList(TableName));
            //SQLReader.Close();
            label = new Label[tableDataType.Rows.Count];
            textBox = new MultiTypeInputBox[label.Length];
            DialogForm.Text = title + " [0/0]";
            Collunms = new List<String>();
            isNotView = isTable;
            for (int Index = 0; Index < label.Length; Index++)
            {
                label[Index] = new Label();
                label[Index].AutoSize = false;
                string ColumnName = tableDataType.Rows[Index]["COLUMN_NAME"].ToString();
                label[Index].Text = ColumnName;
                Collunms.Add(label[Index].Text);
                label[Index].SetBounds(10, Index * 25 + 10, 100, 25);
                if (IsPrimaryKey(ColumnName))
                {                    
                    Font aFont = new Font(label[Index].Font, FontStyle.Underline & FontStyle.Bold);
                    label[Index].Font = aFont;
                }
                if (IsForeignKey(ColumnName))
                {
                    textBox[Index] = new MultiTypeInputBox(MultiInputBoxType.ComboBox);
                    if (isNotView)
                    {
                        textBox[Index].TextChanged += new System.EventHandler(this.textBox_TextChanged);
                        //textBox[Index].Enter += new System.EventHandler(this.textBox_Enter);
                        //textBox[Index].Leave += new System.EventHandler(this.textBox_Leave);
                    }
                }
                else
                    switch (tableDataType.Rows[Index]["Data_Type"].ToString().ToLower())
                    {
                        case "bit":
                            textBox[Index] = new MultiTypeInputBox(MultiInputBoxType.CheckBox);
                            if (isNotView)
                            {
                                textBox[Index].TextChanged += new System.EventHandler(this.textBox_TextChanged);
                                //textBox[Index].Enter += new System.EventHandler(this.textBox_Enter);
                                textBox[Index].Leave += new System.EventHandler(this.textBox_Leave);
                            }
                            break;
                        case "image":
                        case "varbinary":
                        case "binary":
                        case "timestamp":
                            textBox[Index] = new MultiTypeInputBox(MultiInputBoxType.Button);
                            if (isNotView)
                            {
                                //textBox[Index].TextChanged += new System.EventHandler(this.textBox_TextChanged);
                                textBox[Index].Enter += new System.EventHandler(this.textBox_Enter);
                                textBox[Index].Leave += new System.EventHandler(this.textBox_Leave);
                            }
                            break;
                        default:
                            textBox[Index] = new MultiTypeInputBox(MultiInputBoxType.TextBox);
                            if (isNotView)
                            {
                                textBox[Index].TextChanged += new System.EventHandler(this.textBox_TextChanged);
                                textBox[Index].Enter += new System.EventHandler(this.textBox_Enter);
                                textBox[Index].Leave += new System.EventHandler(this.textBox_Leave);
                            }
                            break;
                    } 
               
                textBox[Index].SetBounds(label[Index].Right + 5, label[Index].Top, 300, 25);
                textBox[Index].TabIndex = Index;
            }
            DialogForm.Controls.AddRange(label);
            DialogForm.Controls.AddRange(textBox);
            btnNext = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            btnGoTo = new Button();
            btnPrevious = new Button();

            btnNext.Text = ">>";
            btnDelete.Text = "&Xoá";
            btnUpdate.Text = "&Sửa";
            btnInsert.Text = "&Thêm";
            btnGoTo.Text = "Tớ&i...";
            btnPrevious.Text = "<<";
            btnPrevious.TabIndex = label.Length;
            btnInsert.TabIndex = label.Length + 1;
            btnUpdate.TabIndex = label.Length + 2;
            btnDelete.TabIndex = label.Length + 3;
            btnGoTo.TabIndex = label.Length + 4;
            btnNext.TabIndex = label.Length + 5;
            int bWidth = 65;
            int bHeight = 25;
            int bAlpha = (textBox[0].Right - label[0].Left - bWidth * 6) / 5;
            btnPrevious.SetBounds(label[0].Left, label[label.Length - 1].Bottom + 10, bWidth, bHeight);
            btnInsert.SetBounds(label[0].Left + bWidth * 1 + bAlpha * 1, btnPrevious.Top, bWidth, bHeight);
            btnUpdate.SetBounds(label[0].Left + bWidth * 2 + bAlpha * 2, btnPrevious.Top, bWidth, bHeight);
            btnDelete.SetBounds(label[0].Left + bWidth * 3 + bAlpha * 3, btnPrevious.Top, bWidth, bHeight);
            btnGoTo.SetBounds(label[0].Left + bWidth * 4 + bAlpha * 4, btnPrevious.Top, bWidth, bHeight);
            btnNext.SetBounds(label[0].Left + bWidth * 5 + bAlpha * 5, btnPrevious.Top, bWidth, bHeight);
            btnPrevious.Click += new System.EventHandler(this.butPrevious_Click);
            btnInsert.Click += new System.EventHandler(this.butInsert_Click);
            btnUpdate.Click += new System.EventHandler(this.butUpdate_Click);
            btnDelete.Click += new System.EventHandler(this.butDelete_Click);
            btnGoTo.Click += new EventHandler(btnGoTo_Click);
            btnNext.Click += new System.EventHandler(this.butNext_Click);
            if (!isNotView)
            {
                btnDelete.Visible = false;
                btnInsert.Visible = false;
                btnUpdate.Visible = false;
            }
            DialogForm.Controls.AddRange(new Control[] { btnNext, btnPrevious,btnGoTo, btnDelete, btnUpdate, btnInsert });
            DialogForm.ClientSize = new Size(btnNext.Right + 10, btnNext.Bottom + 10);
            DialogForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            DialogForm.StartPosition = FormStartPosition.CenterScreen;
            DialogForm.MinimizeBox = false;
            DialogForm.MaximizeBox = false;
            DialogForm.Shown += new EventHandler(BaseForm_Shown);
            ReloadData();
            isUserEvents = false;
            DialogResult dialogResult = DialogForm.ShowDialog();
        }
        private void BaseForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents(); // Chờ Form load cho xong. đã hiện lên.
            isUserEvents = true;
        }
        /// <summary>
        /// Hiển thị thông tin của dòng hiện tại lên from
        /// </summary>
        private void ShowData()
        {
            isUserEvents = false;
            string aStr = DialogForm.Text;
            DialogForm.Text = aStr.Substring(0, aStr.LastIndexOf('[') + 1) + (RowIndex + 1).ToString() + "/" + tableData.Rows.Count + "]";
            BinaryData = new object[textBox.Length];
            if (tableData.Rows.Count > 0)
                for (int Index = 0; Index < textBox.Length; Index++)
                {
                    string aText = tableData.Rows[RowIndex][Index].ToString();
                    string ColumnName = tableDataType.Rows[Index]["COLUMN_NAME"].ToString();
                    if (IsPrimaryKey(ColumnName) && isNotView)
                    {
                        label[Index].Font = new Font(label[Index].Font, FontStyle.Underline);
                    }
                    if (IsForeignKey(ColumnName) && isNotView)
                    {
                        DataTable aData = new DataTable();
                        //SQLReader = GetForeignKeyData(ColumnName);
                        aData.Load(GetForeignKeyData(ColumnName));
                        //SQLReader.Close();
                        string cboValue = string.Empty;
                        for (int i = 0; i < aData.Rows.Count; i++)
                            cboValue += aData.Rows[i][0] + ((i < aData.Rows.Count - 1) ? "\n" : "");
                        textBox[Index].Text = cboValue;
                        textBox[Index].cboText = aText;
                    }
                    else
                    {
                        switch (tableDataType.Rows[Index]["Data_Type"].ToString().ToLower())
                        {
                            case "datetime":
                            case "datetime2":
                                if (aText != string.Empty)
                                {
                                    string[] str1 = aText.Split(new char[] { '/', ' ' });
                                    aText = str1[0] + "/" + str1[1] + "/" + str1[2];
                                }
                                break;
                            case "image":
                            case "varbinary":
                            case "binary":
                            case "timestamp":
                                byte[] BinaryValue = (byte[])tableData.Rows[RowIndex][Index];
                                BinaryData[Index] = BinaryValue;
                                aText = tableDataType.Rows[Index]["Data_Type"].ToString().ToUpper();
                                break;
                        }
                        textBox[Index].Text = aText;
                    }
                }
            isUserEvents = true;
        }
        /// <summary>
        /// Cập nhật lại dữ liệu và hiện lại trên form
        /// </summary>
        private void ReloadData()
        {
            tableData = new DataTable();
            tableData.Load(Table(TableName));
            isUserEvents = false;
            RowIndex = 0;
            ShowData();
            btnDelete.Enabled = true;
        }
        #region Sự kiện của các nút trên hộp thoại
        private void butNext_Click(object sender, EventArgs e)
        {
            if (RowIndex < tableData.Rows.Count - 1)
                RowIndex++;
            ShowData();
            if (isNotView)
                btnDelete.Enabled = true;

        }
        private void butDelete_Click(object sender, EventArgs e)
        {
            string Condition = MakeCondition();
            try
            {
                DeleteData(TableName, Condition);
                ReloadData();
                MessageBox.Show("Xoá thành công");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void butUpdate_Click(object sender, EventArgs e)
        {
            List<string> Values = MakeValues();
            string Condition = MakeCondition();
            bool IsNotPrimaryKeyExisted = true;
            string[] WrongData = new string[2];
            int WrongDataIndex = 0;
            for (int Index = 0; Index < textBox.Length; Index++)
            {
                string aValue = textBox[Index].Text;
                string ColumnName = tableDataType.Rows[Index]["COLUMN_NAME"].ToString();
                if (IsPrimaryKey(ColumnName))
                {
                    bool Checker = true;
                    for (int i = 0; i < tableData.Rows.Count; i++)
                        if (tableData.Rows[i][ColumnName].ToString() == aValue && i != Index)
                        {
                            WrongData[0] = aValue;
                            WrongData[1] = ColumnName;
                            WrongDataIndex = Index;
                            Checker = false;
                        }
                    IsNotPrimaryKeyExisted = IsNotPrimaryKeyExisted || Checker;
                }
            }
            if (!IsNotPrimaryKeyExisted)
            {
                string messString = string.Empty;
                for (int i = 0; i < WrongData.GetUpperBound(0); i++)
                    messString += "Giá trị '" + WrongData[0] + "' của thuộc tính '" + WrongData[1] + "' đã tồn tại!\n";
                MessageBox.Show(messString + "Khoá chính không được trùng nhau!\nĐề nghị sửa lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox[WrongDataIndex].Focus();
                return;
            }
            try
            {
                UpdateData(TableName, Collunms, Values, Condition);
                ReloadData();
                MessageBox.Show("Cập nhật thành công");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGoTo_Click(object sender, EventArgs e)
        {
            decimal NewValue = RowIndex + 1;
            if (InputBox("Nhập giá vị trí cần tới", "Nhập số", 1, tableData.Rows.Count, ref NewValue) == DialogResult.OK)
            {
                RowIndex = (int)NewValue -1;
                ShowData();
                if (isNotView)
                    btnDelete.Enabled = true;
            }
        }
        private void butInsert_Click(object sender, EventArgs e)
        {
            List<string> Values = MakeValues();
            
            string[] WrongData = new string[2];
            int WrongDataIndex = 0;
            int Checker1 = 0, Checker2 = 0;
            for (int Index = 0; Index < textBox.Length; Index++)
            {
                string aValue = textBox[Index].Text;
                string ColumnName = tableDataType.Rows[Index]["COLUMN_NAME"].ToString();
                if (IsPrimaryKey(ColumnName))
                {
                    Checker1++;
                    for (int i = 0; i < tableData.Rows.Count; i++)
                        if (tableData.Rows[i][ColumnName].ToString() == aValue)
                        {
                            WrongData[0] = aValue;
                            WrongData[1] = ColumnName;
                            WrongDataIndex = Index;
                            Checker2++;
                        }                    
                }
            }
            if (Checker2 > 0 && Checker1 == Checker2)
            {
                string messString = string.Empty;
                for (int i = 0; i < WrongData.GetUpperBound(0); i++)
                    messString += "Giá trị '" + WrongData[0] + "' của thuộc tính '" + WrongData[1] + "' đã tồn tại!\n";
                MessageBox.Show(messString + "Khoá chính không được trùng nhau!\nĐề nghị sửa lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox[WrongDataIndex].Focus();
                return;
            }
            try
            {
                InsertData(TableName, Collunms, Values);
                ReloadData();
                MessageBox.Show("Chèn thành công");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void butPrevious_Click(object sender, EventArgs e)
        {
            if (RowIndex > 0)
                RowIndex--;
            ShowData();
            if (isNotView)
                btnDelete.Enabled = true;

        }
        #endregion
    }
}
