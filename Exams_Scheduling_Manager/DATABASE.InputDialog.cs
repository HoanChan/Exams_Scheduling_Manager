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
        private enum ImageBinaryArrayType
        {
            Normal = 1,
            /// <summary>
            /// Cắt bỏ 78 bit đầu
            /// </summary>
            Northwind = 2
        }
        PictureBox picThumb;
        Form frmInputImage;
        /// <summary>
        /// Hiện hộp thoại nhập ngày tháng năm
        /// </summary>
        /// <param name="title">Tiêu đề</param>
        /// <param name="value">Giá trị người dùng nhập vào</param>
        /// <returns>DialogResult.OK (Người dùng bấn nút OK) / DialogResult.Cancel (Người dùng thoát hộp thoại)</returns>
        private DialogResult InputDateTime(string title, ref DateTime value)
        {
            Form form = new Form();
            Button buttonOk = new Button();
            DateTimePicker aDateTimePicker = new DateTimePicker();
            form.Text = title;
            aDateTimePicker.SetBounds(10, 10, 230, 20);
            aDateTimePicker.Value = value;
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.SetBounds((230 + 20 - 75) / 2, 20 + 15, 75, 23);
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            form.ClientSize = new Size(230 + 20, buttonOk.Bottom + 10);
            form.Controls.AddRange(new Control[] { aDateTimePicker, buttonOk });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();
            value = aDateTimePicker.Value;
            return dialogResult;
        }
        /// <summary>
        /// Hộp thoại chọn giá trị khoá ngoại
        /// </summary>
        /// <param name="title">Tiêu đề hộp thoại</param>
        /// <param name="ColumnName">Cột là khoá ngoại</param>
        /// <param name="value">Giá trị người dùng chọn</param>
        /// <returns>DialogResult.OK (Người dùng bấn nút OK) / DialogResult.Cancel (Người dùng thoát hộp thoại)</returns>
        //private DialogResult InputForeignKey(string title, string ColumnName, ref string value)
        //{
        //    Form form = new Form();
        //    Button buttonOk = new Button();
        //    ComboBox aComboBox = new ComboBox();
        //    form.Text = title;
        //    aComboBox.SetBounds(10, 10, 230, 20);
        //    DataTable aData = new DataTable();
        //    SQLReader = GetForeignKeyData(ColumnName);
        //    aData.Load(SQLReader);
        //    SQLReader.Close();
        //    for (int i = 0; i < aData.Rows.Count; i++)
        //        aComboBox.Items.Add(aData.Rows[i][0]);
        //    aComboBox.Text = aComboBox.Items[0].ToString();
        //    buttonOk.Text = "OK";
        //    buttonOk.DialogResult = DialogResult.OK;
        //    buttonOk.SetBounds((230 + 20 - 75) / 2, 20 + 15, 75, 23);
        //    buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        //    form.ClientSize = new Size(230 + 20, buttonOk.Bottom + 10);
        //    form.Controls.AddRange(new Control[] { aComboBox, buttonOk });
        //    form.FormBorderStyle = FormBorderStyle.FixedDialog;
        //    form.StartPosition = FormStartPosition.CenterScreen;
        //    form.MinimizeBox = false;
        //    form.MaximizeBox = false;
        //    form.AcceptButton = buttonOk;
        //    DialogResult dialogResult = form.ShowDialog();
        //    value = aComboBox.Text;
        //    return dialogResult;
        //}
        private DialogResult InputBinary(string title, ref byte[] value)
        {
            Form frmInputBinary = new Form();
            TextBox txtBinaryInput = new TextBox();
            Button buttonOk = new Button();
            frmInputBinary.Text = title;
            txtBinaryInput.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            //txtBinaryInput.CharacterCasing = CharacterCasing.Upper;
            txtBinaryInput.SetBounds(10, 10, 500, 500);
            txtBinaryInput.Multiline = true;
            txtBinaryInput.ScrollBars = ScrollBars.Both;
            txtBinaryInput.Text = ByteArrayToHexString(value);

            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.SetBounds((txtBinaryInput.Right + 10 - 75) / 2, txtBinaryInput.Bottom + 10, 75, 23);
            buttonOk.Anchor = AnchorStyles.Bottom;

            frmInputBinary.ClientSize = new Size(txtBinaryInput.Right + 10, buttonOk.Bottom + 10);
            frmInputBinary.Controls.AddRange(new Control[] { txtBinaryInput, buttonOk });
            frmInputBinary.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmInputBinary.StartPosition = FormStartPosition.CenterScreen;
            frmInputBinary.MinimizeBox = false;
            frmInputBinary.MaximizeBox = false;
            frmInputBinary.AcceptButton = buttonOk;
            DialogResult dialogResult = frmInputBinary.ShowDialog();
            value = HexStringToByteArray(txtBinaryInput.Text);
            return dialogResult;
        }
        private string ByteArrayToHexString(byte[] ba)
        {
            if (ba.Length == 0) return string.Empty;
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            for (int i = 0; i < ba.Length; i++)       // <-- use for loop is faster than foreach   
                hex.Append(ba[i].ToString("X2"));   // <-- ToString is faster than AppendFormat   

            return hex.ToString();
        }
        private byte[] HexStringToByteArray(string hexString)
        {
            //if (hexString.Length % 2 != 0)
            //{
            //    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            //}
            if (hexString.Length == 0) return new byte[1];
            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return HexAsBytes;
        }
        private DialogResult InputImage(string title, ImageBinaryArrayType ByteArrType, ref byte[] value)
        {
            frmInputImage = new Form();
            Button buttonOk = new Button();
            picThumb = new PictureBox();
            frmInputImage.Text = title;
            picThumb.BackgroundImageLayout = ImageLayout.Stretch;
            picThumb.BorderStyle = BorderStyle.FixedSingle;
            picThumb.SetBounds(10, 10, 500, 500);
            if (value != null)
            {
                MemoryStream mST;
                Image img;
                if(ByteArrType == ImageBinaryArrayType.Normal)
                {
                    mST = new MemoryStream(value);
                    img = Image.FromStream(mST);
                }
                else
                {
                    mST = new MemoryStream();
                    mST.Write(value, 78, value.Length - 78);
                    img = Image.FromStream(mST);
                }
                SetImage(img);
            }
            picThumb.Click += new System.EventHandler(picThumb_Click);
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.SetBounds((picThumb.Right + 10 -75)/ 2, picThumb.Bottom + 10, 75, 23);
            buttonOk.Anchor = AnchorStyles.Bottom;

            frmInputImage.ClientSize = new Size(picThumb.Right + 10, buttonOk.Bottom + 10);
            frmInputImage.Controls.AddRange(new Control[] { picThumb, buttonOk });
            frmInputImage.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmInputImage.StartPosition = FormStartPosition.CenterScreen;
            frmInputImage.MinimizeBox = false;
            frmInputImage.MaximizeBox = false;
            frmInputImage.AcceptButton = buttonOk;
            DialogResult dialogResult = frmInputImage.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                MemoryStream mST = new MemoryStream();
                picThumb.BackgroundImage.Save(mST, System.Drawing.Imaging.ImageFormat.Png);
                value = mST.ToArray();
                //if (ByteArrType == ImageBinaryArrayType.Northenwind)
                //{
                //    mST = new MemoryStream();
                //    mST.Write(value, 78, value.Length - 78);
                //    value = mST.ToArray();
                //}
            }
            return dialogResult;
        }
        private void SetImage(Image aImage)
        {
            if (aImage.Width <= 500)
                picThumb.Width = aImage.Width;
            picThumb.Height = picThumb.Width * aImage.Height / aImage.Width;
            if (picThumb.Height > 500)
            {
                picThumb.Height = 500;
                picThumb.Width = picThumb.Height * aImage.Width / aImage.Height;
            }
            picThumb.BackgroundImage = aImage;
        }
        private void picThumb_Click(object sender, EventArgs e)
        {
            OpenFileDialog opDialog = new OpenFileDialog();
            opDialog.Filter = "ImageFile|*.bmp;*.jpg;*.jpeg;*.gif;*.png";
            if (opDialog.ShowDialog() == DialogResult.OK)
            {
                picThumb.Width = 500;
                SetImage(Image.FromFile(opDialog.FileName));                
            }
            frmInputImage.ClientSize = new Size(picThumb.Right + 10, picThumb.Bottom + 10 + 23 + 10);
        }

        private bool IsImage(byte[] aByteArr, ref ImageBinaryArrayType ByteArrType)
        {
            bool Checker = false;
            MemoryStream mST = new MemoryStream(aByteArr);
            try
            {
                Image img = Image.FromStream(mST);
                Checker = true;
                ByteArrType = ImageBinaryArrayType.Normal;
            }
            catch (ArgumentException)
            {
                mST = new MemoryStream();
                mST.Write(aByteArr, 78, aByteArr.Length - 78);
                try
                {
                    Image img = Image.FromStream(mST);
                    Checker = true;
                    ByteArrType = ImageBinaryArrayType.Northwind;
                }
                catch (ArgumentException)
                {
                    Checker = false;
                }
            }
            return Checker;

        }

        /// <summary>
        /// Hộp thoại nhập giá trị
        /// </summary>
        /// <param name="promptText">Nội dung</param>
        /// <param name="title">Tiêu đề</param>
        /// <param name="value">Lưu giá trị người dùng nhập</param>
        /// <returns>Nút người dùng bấm</returns>
        private DialogResult InputBox(string promptText, string title, int Min, int Max, ref decimal value)
        {
            Form form = new Form();
            Label label = new Label();
            NumericUpDown nudInput = new NumericUpDown();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            nudInput.Minimum = Min;
            nudInput.Maximum = Max;
            nudInput.Value = value;

            buttonOk.Text = "&OK";
            buttonCancel.Text = "&Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            SizeF textSize = label.CreateGraphics().MeasureString(promptText, label.Font);
            if (textSize.Width < 75 * 2 + 30)
                textSize.Width = 75 * 2 + 10;
            label.SetBounds(10, 10, (int)textSize.Width, (int)textSize.Height); //x , y , width , height
            label.AutoSize = false;
            nudInput.SetBounds(label.Left, label.Bottom + 5, label.Width, 20);
            buttonOk.SetBounds(label.Right - 75, nudInput.Bottom + 5, 75, 23);
            buttonCancel.SetBounds(label.Right - 75 * 2 - 10, nudInput.Bottom + 5, 75, 23);
            //nudInput.Anchor = nudInput.Anchor | AnchorStyles.Right;
            //buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(label.Right + 10, buttonOk.Bottom + 10);
            form.Controls.AddRange(new Control[] { label, nudInput, buttonOk, buttonCancel });
            //form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = nudInput.Value;
            return dialogResult;
        }
    }
}
