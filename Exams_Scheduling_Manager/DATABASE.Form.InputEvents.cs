using System;
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
		#region Bẫy lỗi và trợ giúp người dùng nhập giá trị
		private void textBoxEnter(int Index)
		{
			if (isUserEvents)
			{
				isUserEvents = false;
				if (IsForeignKey(tableDataType.Rows[Index]["COLUMN_NAME"].ToString()))
				{
					//string aStr = string.Empty;
					//if (InputForeignKey("Chọn giá trị", tableDataType.Rows[Index]["COLUMN_NAME"].ToString(), ref aStr) == DialogResult.OK)
					//    textBox[Index].Text = aStr;
				}
				else
				{
					byte[] aBinaryValue = (byte[])BinaryData[Index];
					string aText = "[ " + tableDataType.Rows[Index]["Data_Type"].ToString().ToUpper() + " ]";
					ImageBinaryArrayType ByteArrType = ImageBinaryArrayType.Normal;
					switch (tableDataType.Rows[Index]["Data_Type"].ToString().ToLower())
					{
						case "datetime":
							DateTime aDate;
							if (textBox[Index].Text.Length > 5)
							{
								string[] str1 = textBox[Index].Text.Split(new char[] { '/', ' ' });
								aDate = new DateTime(Convert.ToInt32(str1[2]), Convert.ToInt32(str1[1]), Convert.ToInt32(str1[0]));
							}
							else
								aDate = DateTime.Now;
							if (InputDateTime("Chọn ngày", ref aDate) == DialogResult.OK)
								textBox[Index].Text = aDate.ToString("dd/MM/yyyy");
							break;
						case "int":
                            string aStr = textBox[Index].Text;
                            if (aStr!=string.Empty)
                            {
                                string bStr = aStr[0] == '-' ? "-" : "";
                                for (int i = 0; i < aStr.Length; i++)
                                    if (aStr[i] <= '9' && aStr[i] >= '0')
                                        bStr += aStr[i];
                                textBox[Index].Text = bStr;
                            }
							break;
						case "bit":
							break;
						case "image":
							if (IsImage(aBinaryValue, ref ByteArrType) || aBinaryValue == null)
							{
								if (InputImage("Chọn ảnh", ByteArrType, ref aBinaryValue) == DialogResult.OK)
									BinaryData[Index] = aBinaryValue;
							}
							else
							{
								if (InputBinary("Chỉnh sửa giá trị", ref aBinaryValue) == DialogResult.OK)
									BinaryData[Index] = aBinaryValue;
							}
							textBox[Index].Text = aText;
							break;
						case "varbinary":
						case "binary":
							if (IsImage(aBinaryValue, ref ByteArrType))
							{
								if (InputImage("Chọn ảnh", ByteArrType, ref aBinaryValue) == DialogResult.OK)
									BinaryData[Index] = aBinaryValue;
							}
							else
							{
								if (InputBinary("Chỉnh sửa giá trị", ref aBinaryValue) == DialogResult.OK)
									BinaryData[Index] = aBinaryValue;
							}
							textBox[Index].Text = aText;
							break;
						case "timestamp":
							if (InputBinary("Chỉnh sửa giá trị", ref aBinaryValue) == DialogResult.OK)
								BinaryData[Index] = aBinaryValue;
							textBox[Index].Text = aText;
							break;
					}

				}
				isUserEvents = true;
			}
		}
		private void textBox_Enter(object sender, EventArgs e)
		{
			MultiTypeInputBox Me = sender as MultiTypeInputBox;
			int Index = 0;
			for (int i = 0; i < textBox.Length; i++)
				if (Me == textBox[i])
				{
					Index = i;
					break;
				}
			textBoxEnter(Index);
		}
		private void textBox_TextChanged(object sender, EventArgs e)
		{
			MultiTypeInputBox Me = sender as MultiTypeInputBox;
			int Index = 0;
			for (int i = 0; i < textBox.Length; i++)
				if (Me == textBox[i])
				{
					Index = i;
					break;
				}
			textBoxEnter(Index);
			btnDelete.Enabled = false;
		}
		private void textBox_Leave(object sender, EventArgs e)
		{
			if (isUserEvents)
			{
				isUserEvents = false;
				MultiTypeInputBox Me = sender as MultiTypeInputBox;
				int Index = 0;
				for (int i = 0; i < textBox.Length; i++)
					if (Me == textBox[i])
					{
						Index = i;
						break;
					}
				string ConditionString = GetCondition(tableDataType.Rows[Index]["COLUMN_NAME"].ToString());
				if (ConditionString.Length > 3)
				{
					string aValue = FixData(textBox[Index].Text, tableDataType.Rows[Index]["Data_Type"].ToString());

					if (!CheckCondition(ConditionString, tableDataType.Rows[Index]["COLUMN_NAME"].ToString(), aValue))
					{
						MessageBox.Show("Giá trị sai!\nĐề nghị nhập lại theo điều kiện : " + ConditionString, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
						textBox[Index].Focus();
						isUserEvents = true;
						return;
					}
				}
				if (tableDataType.Rows[Index]["IS_NULLABLE"].ToString().ToUpper() == "NO" && textBox[Index].Text == "")
				{
					MessageBox.Show("Thuộc tính không thể bằng NULL!\nĐề nghị nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
					textBox[Index].Focus();
					isUserEvents = true;
					return;
				}
				if (tableDataType.Rows[Index]["Data_Type"].ToString().ToUpper() == "CHAR"
					|| tableDataType.Rows[Index]["Data_Type"].ToString().ToUpper() == "NCHAR")
				{
					int MaxLength = Convert.ToInt32(tableDataType.Rows[Index][3].ToString());
					if (MaxLength != textBox[Index].Text.Length)
					{
						MessageBox.Show("Giá trị này cần có độ dài chính xác là " + MaxLength + "!\nĐề nghị nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
						textBox[Index].Focus();
					}
				}
				else
					if (tableDataType.Rows[Index]["Data_Type"].ToString().ToUpper() == "VARCHAR"
						|| tableDataType.Rows[Index]["Data_Type"].ToString().ToUpper() == "NVARCHAR")
					{
						int MaxLength = Convert.ToInt32(tableDataType.Rows[Index]["character_maximum_length"].ToString());
						if (MaxLength < textBox[Index].Text.Length)
						{
							MessageBox.Show("Giá trị này có độ dài lớn hơn " + MaxLength + ", phần dư ra sẽ bị cắt bớt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
							textBox[Index].Text = textBox[Index].Text.Substring(0, MaxLength);
						}
					}
				isUserEvents = true;
			}
		}
		#endregion
	}
}
