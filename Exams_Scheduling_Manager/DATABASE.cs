using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Globalization;
namespace Exams_Scheduling_Manager
{
    partial class DATABASE
    {
        #region Variable
        private SqlConnection conn;
        private int RowIndex;
        private string TableName;
        private List<String> Collunms;
        public List<String> Tables;
        public List<String> Views;
        private DataTable tableData;
        private DataTable tableDataType;
        private DataTable tableSystemDataTypeInfo;
        private bool isUserEvents;
        private bool isNotView;
        #endregion
        public DATABASE(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            Tables = MakeTablesList();
            Views = MakeViewsList();
            tableSystemDataTypeInfo = new DataTable();
            tableSystemDataTypeInfo.Load(GetSystemDataTypeInfo());
        }
        private List<string> MakeTablesList()
        {
            SqlDataReader SQLReader = RunReader(@"SELECT name FROM SYS.OBJECTS WHERE TYPE = 'U' AND name <> 'sysdiagrams'");
            List<string> aList = new List<string>();
            while (SQLReader.Read())
                aList.Add(SQLReader[0].ToString());
            SQLReader.Close();
            return aList;
        }
        private List<string> MakeViewsList()
        {
            SqlDataReader SQLReader = RunReader(@"SELECT name FROM SYS.OBJECTS WHERE TYPE = 'V'");
            List<string> aList = new List<string>();
            while (SQLReader.Read())
                aList.Add(SQLReader[0].ToString());
            SQLReader.Close();
            return aList;
        }
        /// <summary>
        /// Chèn dữ liệu vào bảng
        /// </summary>
        /// <param name="aTableName">Tên bảng</param>
        /// <param name="aColunmName">Danh sách tên các cột</param>
        /// <param name="aValues">Danh sách giá trị tương ứng với từng cột</param>
        private int InsertData(string aTableName, List<string> aColunmName, List<string> aValues) 
        {
            string Query = "INSERT INTO " + aTableName + "(";
            for (int i = 0; i < aColunmName.Count; i++)
            {
                Query += aColunmName[i];
                if (i < aColunmName.Count - 1)
                    Query += ", ";
            }
            Query += ") VALUES(";
            for (int i = 0; i < aValues.Count; i++)
            {
                Query += aValues[i];
                if (i < aValues.Count - 1)
                    Query += ", ";
            }
            Query += ")";
            return RunNonQuery(Query); 
        }
        /// <summary>
        /// Xoá dữ liệu từ bảng
        /// </summary>
        /// <param name="aTableName">Tên bảng</param>
        /// <param name="aConditions">Điều kiện để xoá</param>
        /// <returns>Dòng được thao tác</returns>
        private int DeleteData(string aTableName, string aConditions) 
        {
            return RunNonQuery("DELETE FROM " + aTableName + " WHERE " + aConditions);         
        }
        /// <summary>
        /// Cập nhật dữ liệu của bảng
        /// </summary>
        /// <param name="aTableName">Tên bảng</param>
        /// <param name="aColunmName">Danh sách tên các cột</param>
        /// <param name="aValues">Danh sách giá trị tương ứng với từng cột</param>
        /// <param name="aConditions">Điều kiện để xoá</param>
        /// <returns>Dòng được thao tác</returns>
        private int UpdateData(string aTableName, List<string> aColunmName, List<string> aValues, string aConditions) 
        {           
            string Query = "UPDATE " + TableName + " SET ";
            for (int i = 0; i < aColunmName.Count; i++)
            {
                Query += aColunmName[i] + " = " + aValues[i];
                if (i < aColunmName.Count - 1)
                    Query += ", ";
            }
            Query += "WHERE " + aConditions;
            return RunNonQuery(Query);         
        }
        /// <summary>
        /// Chạy dòng lệnh không trả về bảng
        /// </summary>
        /// <param name="Query">Lệnh cần chạy</param>
        /// <returns>Số lượng hàng bị ảnh hưởng</returns>
        private int RunNonQuery(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteNonQuery(); 
        }
        /// <summary>
        /// Chạy dòng lệnh trả về một bảng
        /// </summary>
        /// <param name="Query">Lệnh cần chạy</param>
        /// <returns>Kết quả</returns>
        private SqlDataReader RunReader(string Query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.CommandText = Query;
            return sqlCommand.ExecuteReader();
        }
        /// <summary>
        /// Lấy thông tin của các cột trong bảng
        /// </summary>
        /// <param name="TableName">Tên table</param>
        /// <returns>COLUMN_NAME (Tên), Ordinal_position (Thứ tự vị trí), Data_Type (Kiểu dữ liệu), 
        /// character_maximum_length (Độ dài kí tự tối đa),IS_NULLABLE(Nhận giá trị Null được không)</returns>
        private SqlDataReader ColumnList(string aTableName)
        {
            return RunReader("SELECT COLUMN_NAME, Ordinal_position, Data_Type, character_maximum_length, IS_NULLABLE" +
                                " FROM INFORMATION_SCHEMA.COLUMNS" +
                                " WHERE TABLE_NAME='" + aTableName + "'");
        }
        /// <summary>
        /// Lấy thông tin về các kiểu dữ liệu của SQL
        /// </summary>
        /// <returns>
        /// TYPE_NAME	    DATA_TYPE	        PRECISION	    LITERAL_PREFIX	    LITERAL_SUFFIX	
        /// CREATE_PARAMS	NULLABLE	        CASE_SENSITIVE	SEARCHABLE	        UNSIGNED_ATTRIBUTE	
        /// MONEY	        AUTO_INCREMENT	    LOCAL_TYPE_NAME	MINIMUM_SCALE	    MAXIMUM_SCALE	
        /// SQL_DATA_TYPE	SQL_DATETIME_SUB	NUM_PREC_RADIX	INTERVAL_PRECISION	USERTYPE
        /// </returns>
        private SqlDataReader GetSystemDataTypeInfo()
        {
            return RunReader("EXEC sys.sp_datatype_info");
        }
        /// <summary>
        /// Sửa giá trị sang dạng chuẩn để giao tiếp với SQL
        /// (có bao 2 đầu bằng dấu "'" không, có chỉnh sửa gì không)
        /// </summary>
        /// <param name="DataValue">Giá trị cần chỉnh sửa</param>
        /// <param name="DataType">Kiểu của nó</param>
        /// <returns>Trả về giá trị đã được chỉnh sửa</returns>
        private string FixData(string DataValue, string DataType)
        {
            if (DataValue == "") return "NULL";
            string Result = DataValue.Replace("'", "''");
            if (DataType.ToUpper() == "DATETIME" && Result != string.Empty)
            {
                string[] str1 = Result.Split(new char[] { '/', ' ' });
                Result = str1[2] + "/" + str1[1] + "/" + str1[0];
            }
            if (DataType.ToUpper() == "MONEY" || DataType.ToUpper() == "SMALLMONEY")
                Result = Result.Replace(",", ".");
            for (int i = 0; i < tableSystemDataTypeInfo.Rows.Count; i++)
                if (tableSystemDataTypeInfo.Rows[i]["TYPE_NAME"].ToString() == DataType.ToLower())
                {
                    Result = tableSystemDataTypeInfo.Rows[i]["LITERAL_PREFIX"].ToString()
                            + Result
                            + tableSystemDataTypeInfo.Rows[i]["LITERAL_SUFFIX"].ToString();
                    return Result;
                }
            return Result;
        }
        /// <summary>
        /// Lấy bảng dữ liệu
        /// </summary>
        /// <param name="aTableName">Tên bảng cần lấy</param>
        /// <returns>Kết quả</returns>
        private SqlDataReader Table(string aTableName)
        {
            return RunReader("SELECT * FROM [" + aTableName + "]");
        }
        /// <summary>
        /// Kiểm tra có là khoá ngoại hay không
        /// </summary>
        /// <param name="ColumnName">Tên cột cần kiểm tra</param>
        /// <returns>True (Là khoá ngoại) / False (Không phải khoá ngoại)</returns>
        private bool IsForeignKey(string ColumnName)
        {
            SqlDataReader SQLReader = RunReader("SELECT c.COLUMN_NAME" +
                                    " FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk , INFORMATION_SCHEMA.KEY_COLUMN_USAGE c" + 
                                    " WHERE c.TABLE_NAME = '" + TableName + "'" + 
                                            " AND CONSTRAINT_TYPE = 'FOREIGN KEY'" + 
                                            " AND c.TABLE_NAME = pk.TABLE_NAME" + 
                                            " AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME");
            while (SQLReader.Read())
                if (SQLReader[0].ToString().ToUpper() == ColumnName.ToUpper())
                {
                    SQLReader.Close();
                    return true;
                }
            SQLReader.Close();
            return false;
        }
        /// <summary>
        /// Lấy thông tin khoá ngoại
        /// </summary>
        /// <param name="ColumnName">Tên cột cần lấy thông tin</param>
        /// <returns>TABLE_NAME(Tên bảng tham chiếu tới), COLUMN_NAME(Tên cột tham chiếu tới)</returns>
        private SqlDataReader GetForeignKeyData(string ColumnName)
        {
            SqlDataReader SQLReader = RunReader("SELECT kcu.TABLE_NAME, kcu.COLUMN_NAME" +
                                    " FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc , INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu , INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu" +
                                    " WHERE rc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME" +
                                            " AND rc.UNIQUE_CONSTRAINT_NAME = kcu.CONSTRAINT_NAME" +
                                            " AND ccu.TABLE_NAME = '" + TableName + "'" +
                                            " AND ccu.COLUMN_NAME = '" + ColumnName + "'");
            DataTable aData = new DataTable();
            aData.Load(SQLReader);
            SQLReader.Close();
            return RunReader("SELECT " + aData.Rows[0][1].ToString() + " FROM " + aData.Rows[0][0].ToString());
        }
        /// <summary>
        /// Kiểm tra có là khoá chính hay không
        /// </summary>
        /// <param name="ColumnName">Tên cột cần kiểm tra</param>
        /// <returns>True (Là khoá chính) / False (Không phải khoá chính)</returns>
        private bool IsPrimaryKey(string ColumnName)
        {
            SqlDataReader SQLReader = RunReader("SELECT c.COLUMN_NAME" + 
                                    " FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk , INFORMATION_SCHEMA.KEY_COLUMN_USAGE c" + 
                                    " WHERE c.TABLE_NAME = '" + TableName + "'" +
                                            " AND CONSTRAINT_TYPE = 'PRIMARY KEY'" +
                                            " AND c.TABLE_NAME = pk.TABLE_NAME" +
                                            " AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME");
            while (SQLReader.Read())
                if (SQLReader[0].ToString().ToUpper() == ColumnName.ToUpper())
                {
                    SQLReader.Close();
                    return true;
                }
            SQLReader.Close();
            return false;
        }
        /// <summary>
        /// Kiểm tra một điều kiện có đúng không
        /// </summary>
        /// <param name="ConditionString">Chuỗi điều kiện</param>
        /// <param name="ColumnName">Tên cột</param>
        /// <param name="ColumnValue">Giá trị cần kiểm tra</param>
        /// <returns>True (Giá trị cần kiểm tra thoã điều kiện) / False (Giá trị cần kiểm tra không thoã điều kiện)</returns>
        private bool CheckCondition(string ConditionString, string ColumnName, string ColumnValue)
        {
            string ACondition = ConditionString.Replace("[" + ColumnName + "]", (ColumnValue==string.Empty)?"NULL":ColumnValue);
            SqlDataReader SQLReader = RunReader("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE " + ACondition);
            if (SQLReader.Read())
            {
                SQLReader.Close();
                return true;
            }
            SQLReader.Close();
            return false;
        }
        /// <summary>
        /// Lấy chuỗi kiểm tra điều kiện của một cột có ràng buộc kiểm tra dữ liệu
        /// </summary>
        /// <param name="ColumnName">Cột cần tìm điều kiện</param>
        /// <returns>Chuỗi kiểm tra điều kiện</returns>
        private string GetCondition(string ColumnName)
        {
            SqlDataReader SQLReader = RunReader("SELECT cc.CHECK_CLAUSE" +
                                    " FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS cc , INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu" +
                                    " WHERE cc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME" +
                                            " AND ccu.TABLE_NAME = '" + TableName + "'" +
                                            " AND ccu.COLUMN_NAME = '" + ColumnName + "'");
            string sStr;
            if(SQLReader.Read())
                sStr = SQLReader[0].ToString();
            else
                sStr = string.Empty;
            SQLReader.Close();
            return sStr;
        }
        /// <summary>
        /// Ngắt kết nối với cơ sở dữ liệu
        /// </summary>
        public void Close()
        {
            conn.Close();
            conn.Dispose();
        }
        ///// <summary>
        ///// Tạo danh sách các giá trị hiện tại người dùng nhập vào
        ///// </summary>
        ///// <returns>Danh sách</returns>
        //private List<string> MakeValues()
        //{
        //    List<string> Values = new List<string>();
        //    for (int Index = 0; Index < textBox.Length; Index++)
        //    {
        //        string aValue = GetData(Index);
        //        Values.Add(aValue);
        //    }
        //    return Values;
        //}
        ///// <summary>
        ///// Tạo điều kiện để nhận diện dòng hiện tại của bảng hiện tại
        ///// Để xoá / cập nhật
        ///// </summary>
        ///// <returns>Chuỗi điều kiện</returns>
        //private string MakeCondition()
        //{
        //    string aCondition = string.Empty;
        //    int Counter = 0;
        //    for (int Index = 0; Index < textBox.Length; Index++)
        //    {
        //        string ColumnName = tableDataType.Rows[Index]["COLUMN_NAME"].ToString();
        //        if (IsForeignKey(ColumnName) || IsPrimaryKey(ColumnName))
        //        {
        //            string aValue = GetConditionData(Index);
        //            if (Counter > 0)
        //                aCondition += " AND ";
        //            aCondition += "[" + tableDataType.Rows[Index]["COLUMN_NAME"].ToString() + "] = " + aValue;
        //            Counter++;
        //        }
        //    }
        //    return aCondition;
        //}
        //private string GetConditionData(int Index)
        //{
        //    string aValue;
        //    string aDataType = tableDataType.Rows[Index]["Data_Type"].ToString();
        //    switch (aDataType)
        //    {
        //        case "image":
        //        case "varbinary":
        //        case "binary":
        //        case "timestamp":
        //            aValue = FixData(ByteArrayToHexString((byte[])BinaryData[Index]), aDataType);
        //            break;
        //        default:
        //            aValue = FixData(tableData.Rows[RowIndex][Index].ToString(), aDataType);
        //            break;
        //    }
        //    return aValue;
        //}
        //private string GetData(int Index)
        //{
        //    string aValue;
        //    string aDataType = tableDataType.Rows[Index]["Data_Type"].ToString();
        //    switch (aDataType)
        //    {
        //        case "image":
        //        case "varbinary":
        //        case "binary":
        //        case "timestamp":
        //            aValue = FixData(ByteArrayToHexString((byte[])BinaryData[Index]), aDataType);
        //            break;
        //        default:
        //            aValue = FixData(textBox[Index].Text, aDataType);
        //            break;
        //    }
        //    return aValue;
        //}
        //private string ByteArrayToHexString(byte[] ba)
        //{
        //    if (ba.Length == 0) return string.Empty;
        //    StringBuilder hex = new StringBuilder(ba.Length * 2);

        //    for (int i = 0; i < ba.Length; i++)       // <-- use for loop is faster than foreach   
        //        hex.Append(ba[i].ToString("X2"));   // <-- ToString is faster than AppendFormat   

        //    return hex.ToString();
        //}
        //private byte[] HexStringToByteArray(string hexString)
        //{
        //    //if (hexString.Length % 2 != 0)
        //    //{
        //    //    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        //    //}
        //    if (hexString.Length == 0) return new byte[1];
        //    byte[] HexAsBytes = new byte[hexString.Length / 2];
        //    for (int index = 0; index < HexAsBytes.Length; index++)
        //    {
        //        string byteValue = hexString.Substring(index * 2, 2);
        //        HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        //    }

        //    return HexAsBytes;
        //}
    }
}
