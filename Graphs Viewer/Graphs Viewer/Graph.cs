using System;
using System.Collections.Generic; //for List
using System.Drawing; //for Rectangle
using System.Drawing.Drawing2D; //for matrix
using System.IO; //for read file
using System.Threading;// for draw
using System.Windows.Forms;//for dialog
namespace Graphs_Viewer
{
    public enum VertexStatus
    {
        /// <summary>
        /// 
        /// Đang rảnh ^^
        /// </summary>
        sFree = 0,
        /// <summary>
        /// Đã vẽ xong tất cả các cạnh
        /// </summary>
        sDrewEdges = 1,

        /// <summary>
        /// Đã duyệt qua
        /// </summary>
        sApproved = 3,
        /// <summary>
        /// Đã được kết nối từ 1 cạnh nào đó.
        /// </summary>
        sConnected = 4
    }
    public enum ItemStyle
    {
        /// <summary>
        /// Là đỉnh
        /// </summary>
        aVertex = 1,
        /// <summary>
        /// Là cạnh
        /// </summary>
        aEdge = 2
    }
    class Graph
    {
        #region Variable
        /// <summary>
        /// Danh sách đỉnh
        /// </summary>
        private List<Vertex> Vertices = new List<Vertex>();
        /// <summary>
        /// Danh sách cạnh
        /// </summary>
        private List<Edge> Edges = new List<Edge>();
        /// <summary>
        /// Form để vẽ
        /// </summary>
        private Form ControlerForm;
        /// <summary>
        /// Tiểu trình chạy thao tác vẽ
        /// </summary>
        public Thread GraphThread;
        bool IsDirected;
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Vị trí ban đầu của các đỉnh
        /// </summary>
        private Rectangle[] RectTemp;
        /// <summary>
        /// Vị trí chuột
        /// </summary>
        private Point MouseLocation;
        /// <summary>
        /// Vị trí menu
        /// </summary>
        private Point MenuLocation;
        /// <summary>
        /// Chỉ số của đỉnh đang được di chuyển
        /// </summary>
        private int MovingVertexIndex;
        /// <summary>
        /// Đang ở trạng thái bị chuột kéo
        /// </summary>
        private bool IsOnDrap = false;
        /// <summary>
        /// Chuột đang nằm kéo 1 đỉnh
        /// </summary>
        private bool IsDrapAVertex = false;
        /// <summary>
        /// Chỉ số của cạnh đang được click
        /// </summary>
        private int ClickingEdgeIndex;
        /// <summary>
        /// Chỉ số của đỉnh đang được click
        /// </summary>
        private int ClickingVertexIndex;
        /// <summary>
        /// Các dạng biểu diễn của đồ thị
        /// </summary>
        private enum ViewMatrixStyle
        {
            /// <summary>
            /// Ma trận kề
            /// </summary>
            vAdjacencyMatrix = 1,
            /// <summary>
            /// Ma trận liên thuộc
            /// </summary>
            vIncidenceMatrix = 2,
            /// <summary>
            /// Danh sách kề
            /// </summary>
            vAdjacencyList = 3,
            /// <summary>
            /// Danh sách liên thuộc / danh sách cạnh
            /// </summary>
            vIncidenceList = 4
        }

        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region this
        /// <summary>
        /// Khai báo đồ thị vô hướng
        /// </summary>
        /// <param name="_ControlerForm">Form sẽ vẽ đồ thị</param>
        public Graph(Form _ControlerForm)
        {
            ControlerForm = _ControlerForm;
        }
        /// <summary>
        /// Xem một điểm có nằm trong vùng vẽ của đỉnh hay cạnh ko
        /// </summary>
        /// <param name="Location">Vị trí điểm cần xét</param>
        /// <param name="Style">Là đỉnh hay là cạnh</param>
        /// <param name="Index">Trả về vị trí của đỉnh hoặc cạnh mà điểm đang xét nằm trong</param>
        /// <returns>true nếu có / false nếu ko</returns>
        private bool Contains(Point Location, ItemStyle Style, ref int Index)
        {
            if (Style == ItemStyle.aVertex)
            {
                for (int i = 0; i < Vertices.Count; i++)
                    if (Vertices[i].Rectangle.Contains(Location))
                    {
                        Index = i;
                        return true;
                    }
            }
            else //if (Style == ItemStyle.aEdge)
            {
                for (int i = 0; i < Edges.Count; i++)
                    if (Edges[i].Rectangle.Contains(Location))
                    {
                        Index = i;
                        return true;
                    }
            }
            return false;
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region vertices
        /// <summary>
        /// Kết nối 2 đỉnh lại với nhau / Tạo một cạnh
        /// </summary>
        /// <param name="Source">Đỉnh bắt đầu</param>
        /// <param name="Target">Đỉnh kết thúc</param>
        /// <param name="EdgeWeight">Trọng số</param>
        private void AddConection(Vertex Source, Vertex Target, int EdgeWeight)
        {
            if (!Source.ConnectionVertices.Contains(Target))
            {
                Source.ConnectionVertices.Add(Target);
                if (!IsDirected)
                    Target.ConnectionVertices.Add(Source);
                Edge _Edge = new Edge(Source, Target);
                _Edge.Value = EdgeWeight;
                Edges.Add(_Edge);

            }
        }
        /// <summary>
        /// Xoá kết nối giữa 2 đỉnh / Xoá cạnh
        /// </summary>
        /// <param name="Source">Đỉnh bắt đầu</param>
        /// <param name="Target">Đỉnh kết thúc</param>
        private void RemoveConection(Vertex Source, Vertex Target)
        {
            Edges.Remove(GetEdge(Source, Target));
            Source.ConnectionVertices.Remove(Target);
            if (!IsDirected)
                Target.ConnectionVertices.Remove(Source);
        }
        /// <summary>
        /// Xoá tất cả các cạnh của 1 đỉnh
        /// </summary>
        /// <param name="Source">Đỉnh cần thao tác</param>
        private void RemoveAllConections(Vertex Source)
        {
            if (IsDirected)
            {
                foreach (Vertex Target in Vertices)
                {
                    if (GetEdge(Source, Target) != null)
                    {
                        Edges.Remove(GetEdge(Source, Target));
                    }
                    if (GetEdge(Target, Source) != null)
                    {
                        Target.ConnectionVertices.Remove(Source);
                        Edges.Remove(GetEdge(Target, Source));
                    }
                }
            }
            else
            {
                foreach (Vertex Target in Source.ConnectionVertices)
                {
                    Target.ConnectionVertices.Remove(Source);
                    Edges.Remove(GetEdge(Source, Target));
                    //Edges.Remove(GetEdge(Target, Source));
                }
            }
            Source.ConnectionVertices.Clear();
        }
        /// <summary>
        /// Tìm cạnh nối giữa 2 đỉnh
        /// </summary>
        /// <param name="V1">Đỉnh thứ nhất</param>
        /// <param name="V2">Đỉnh thứ hai</param>
        /// <returns>Cạnh nối 2 đỉnh / null nếu ko có</returns>
        private Edge GetEdge(Vertex V1, Vertex V2)
        {
            foreach (Edge E in Edges)
            {
                if (E.Vertex1.Value == V1.Value && E.Vertex2.Value == V2.Value) return E;
                if (E.Vertex1.Value == V2.Value && E.Vertex2.Value == V1.Value && !IsDirected) return E;
            }
            return null;
        }
        /// <summary>
        /// Tìm đỉnh có giá trị lớn nhất
        /// (Để định kích thước cho ma trận kề cần tạo)
        /// </summary>
        /// <returns>Giá trị lớn nhất</returns>
        private int GetVerticesMaxValue()
        {
            int MatrixSize = 0;
            foreach (Vertex _VTemp in Vertices)
                if (_VTemp.Value > MatrixSize) MatrixSize = _VTemp.Value;
            return MatrixSize;
        }
        /// <summary>
        /// Sử dụng giá trị mặc định (màu sắc, font chữ, ...) cho tất cả các đỉnh
        /// </summary>
        private void ResetVetiesValue()
        {
            foreach (Vertex VE in Vertices)
                VE.UseDefaultValue();
            foreach (Edge ED in Edges)
            {
                ED.UseDefaultValue();
            }
        }
        /// <summary>
        /// Làm tất cả các đỉnh về trạng thái Free
        /// </summary>
        private void ResetVetiesStatus()
        {
            foreach (Vertex VE in Vertices)
                VE.Status = VertexStatus.sFree;
        }
        /// <summary>
        /// Sử dụng toạ độ mặc định cho tất cả các đỉnh
        /// </summary>
        private void UpdateVerticesRectangle()
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i].UseDefaultRectangle(i, ControlerForm.ClientSize.Width, ControlerForm.ClientSize.Height);
            }
        }
        /// <summary>
        /// Cập nhật liên kết giữa các đỉnh 
        /// (Tạo danh sách cạnh)
        /// </summary>
        /// <param name="AdjacencyMatrix">Ma trận kề / ma trận trọng số</param>
        private void UpdateVerticesConnections(int[,] AdjacencyMatrix)
        {
            for (int i = 0; i <= AdjacencyMatrix.GetUpperBound(0); i++)
                for (int j = 0; j <= AdjacencyMatrix.GetUpperBound(1); j++)
                    if (AdjacencyMatrix[i, j] != 0 && IsOnVertices(i + 1) && IsOnVertices(j + 1))
                    {
                        Vertex Ve1 = GetVertex(i + 1);
                        Vertex Ve2 = GetVertex(j + 1);
                        AddConection(Ve1, Ve2, AdjacencyMatrix[i, j]);
                    }
        }
        /// <summary>
        /// Tìm đỉnh từ giá trị của đỉnh
        /// </summary>
        /// <param name="VertexValue">Giá trị của đỉnh cần tìm</param>
        /// <returns>Đỉnh tìm được / null nếu tìm ko thấy</returns>
        private Vertex GetVertex(int VertexValue)
        {
            if (VertexValue < 1)
                return null;

            foreach (Vertex VE in Vertices)
                if (VE.Value == VertexValue) return VE;
            return null;
        }
        /// <summary>
        /// Xem thử một đỉnh có nằm trong đồ thị không
        /// </summary>
        /// <param name="VertexValue">Giá trị của đỉnh cần tìm</param>
        /// <returns>true nếu có / false nếu ko</returns>
        private bool IsOnVertices(int VertexValue)
        {
            if (VertexValue < 1)
                return false;

            foreach (Vertex VE in Vertices)
                if (VE.Value == VertexValue) return true;

            return false;
        }
        /// <summary>
        /// Xoá tất cả các đỉnh của đồ thị
        /// </summary>
        private void CleanVertices()
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i].Dispose();
                Vertices[i] = null;
            }
            Vertices.Clear();
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region InputBox
        /// <summary>
        /// Hộp thoại nhập giá trị
        /// </summary>
        /// <param name="promptText">Nội dung</param>
        /// <param name="title">Tiêu đề</param>
        /// <param name="value">Lưu giá trị người dùng nhập</param>
        /// <returns>Nút người dùng bấm</returns>
        private DialogResult InputBox(string promptText, string title, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            SizeF textSize = label.CreateGraphics().MeasureString(promptText, label.Font);
            if (textSize.Width < 75 * 2 + 30)
                textSize.Width = 75 * 2 + 10;
            label.SetBounds(10, 10, (int)textSize.Width, (int)textSize.Height); //x , y , width , height
            label.AutoSize = false;
            textBox.SetBounds(label.Left, label.Bottom + 5, label.Width, 20);
            buttonOk.SetBounds(label.Right - 75, textBox.Bottom + 5, 75, 23);
            buttonCancel.SetBounds(label.Right - 75*2 - 10, textBox.Bottom + 5, 75, 23);
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(label.Right + 10, buttonOk.Bottom + 10);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            //form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        /// <summary>
        /// Nhập một đỉnh
        /// </summary>
        /// <param name="Messenger">Nội dung hộp thoại</param>
        /// <param name="Caption">Tiêu đề</param>
        /// <param name="IsNotOnVertices">Đỉnh đó có phải không thuộc đồ thị?</param>
        /// <returns>Giá trị của đỉnh mà người dùng nhập</returns>
        private int InputVertex(string Messenger, string Caption, bool IsNotOnVertices)
        {
            bool isOK = true;
            do
            {
                string InputBoxResult = "";
                DialogResult Result = InputBox(Messenger, Caption, ref InputBoxResult);
                if (Result == DialogResult.OK)
                {
                    int VertexValue;
                    bool isNum = Int32.TryParse(InputBoxResult, out VertexValue);
                    if (isNum)
                    {
                        if (IsNotOnVertices)
                        {
                            if (!IsOnVertices(VertexValue))
                            {
                                if (VertexValue > 0)
                                {
                                    isOK = true;
                                    return VertexValue;
                                }
                                else
                                {
                                    MessageBox.Show("Nhập số phải lớn hơn 0! Bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    isOK = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Đã có đỉnh " + VertexValue + " rồi! Bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                isOK = false;
                            }
                        }
                        else
                        {
                            if (IsOnVertices(VertexValue))
                            {
                                isOK = true;
                                return VertexValue;
                            }
                            else
                            {
                                MessageBox.Show("Hix, làm gì có cái đỉnh " + VertexValue + " bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                isOK = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu nhập vào không phải là số! Bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isOK = false;
                    }
                }
                else
                {
                    throw new System.ArgumentException("Thoát nửa chừng!");
                }
            } while (!isOK);
            return 0;
        }
        /// <summary>
        /// Nhập một số
        /// </summary>
        /// <param name="Messenger">Nội dung hộp thoại</param>
        /// <param name="Caption">Tiêu đề</param>
        /// <param name="isGreaterThanZero">Số đó có lớp hơn 0 hay ko</param>
        /// <returns>Số người dùng nhập</returns>
        private int InputNumber(string Messenger, string Caption, bool isGreaterThanZero)
        {
            bool isOK = true;
            do
            {
                string InputBoxResult = "";
                DialogResult Result = InputBox(Messenger, Caption, ref InputBoxResult);
                if (Result == DialogResult.OK)
                {
                    int Int32Value;
                    bool isNum = Int32.TryParse(InputBoxResult, out Int32Value);
                    if (isNum)
                    {
                        if (isGreaterThanZero)
                        {
                            if (Int32Value > 0)
                            {
                                isOK = true;
                                return Int32Value;
                            }
                            else
                            {
                                MessageBox.Show("Nhập số phải lớn hơn 0! Bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                isOK = false;
                            }
                        }
                        else
                        {
                            if (Int32Value >= 0)
                            {
                                isOK = true;
                                return Int32Value;
                            }
                            else
                            {
                                MessageBox.Show("Nhập số phải lớn hơn hoặc bằng 0! Bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                isOK = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu nhập vào không phải là số! Bấm OK để nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    throw new System.ArgumentException("Thoát nửa chừng!");
                }
            } while (!isOK);
            return 0;
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region Adjacency Matrix
        /// <summary>
        /// Tạo ma trận kề (ma trận trọng số) từ đồ thị hiện tại
        /// </summary>
        /// <returns>Ma trận kề</returns>
        private int[,] GetAdjacencyMatrix()
        {
            int[,] Matrix = null;
            int MatrixSize = GetVerticesMaxValue();
            Matrix = new int[MatrixSize, MatrixSize];
            foreach (Vertex Source in Vertices)
                foreach (Vertex Target in Source.ConnectionVertices)
                    Matrix[Source.Value - 1, Target.Value - 1] = GetEdge(Source, Target).Value;
            return Matrix;
        }
        /// <summary>
        /// Xuất ma trận kề của đồ thị hiện tại ra file
        /// </summary>
        /// <param name="DataFilePath">File sẽ lưu</param>
        private void WriteAdjacencyMatrix(string DataFilePath)
        {
            StreamWriter file = new System.IO.StreamWriter(DataFilePath);
            int[,] AdjacencyMatrix = GetAdjacencyMatrix();
            for (int i = 0; i <= AdjacencyMatrix.GetUpperBound(0); i++)
            {
                string Text = "";
                for (int j = 0; j <= AdjacencyMatrix.GetUpperBound(1); j++)
                {
                    Text += AdjacencyMatrix[i, j].ToString();
                    if (j != AdjacencyMatrix.GetUpperBound(1))
                        Text += " ";
                }
                file.WriteLine(Text);
            }
            AdjacencyMatrix = null;
            file.Close();
            file.Dispose();
        }
        /// <summary>
        /// Xuất ma trận kề cùng với toạ độ các đỉnh của đồ thị hiện tại ra file
        /// </summary>
        /// <param name="DataFilePath">File sẽ lưu</param>
        private void WriteAdjacencyMatrixWithRectangle(string DataFilePath)
        {
            StreamWriter file = new System.IO.StreamWriter(DataFilePath);
            int[,] AdjacencyMatrix = GetAdjacencyMatrix();
            for (int i = 0; i <= AdjacencyMatrix.GetUpperBound(0); i++)
            {
                string Text = "";
                for (int j = 0; j <= AdjacencyMatrix.GetUpperBound(1); j++)
                {
                    Text += AdjacencyMatrix[i, j].ToString();
                    if (j != AdjacencyMatrix.GetUpperBound(1))
                        Text += " ";
                }
                file.WriteLine(Text);
            }
            for (int i = 0; i <= AdjacencyMatrix.GetUpperBound(0); i++)
            {
                Vertex _VeTemp = GetVertex(i + 1);
                if (_VeTemp != null)
                {
                    file.WriteLine(_VeTemp.Rectangle.X + " " + _VeTemp.Rectangle.Y + " " + _VeTemp.Rectangle.Width + " " + _VeTemp.Rectangle.Height);
                }
                else
                    file.WriteLine("");
            }
            AdjacencyMatrix = null;
            file.Close();
            file.Dispose();
        }
        /// <summary>
        /// Đọc ma trận kề từ file
        /// </summary>
        /// <param name="DataFilePath">File cần đọc</param>
        private void ReadAdjacencyMatrix(string DataFilePath)
        {
            int[,] AdjacencyMatrix = null;
            string[] Data = File.ReadAllLines(DataFilePath);
            int AdjacencyMatrixSize = Data.Length;
            string[] Split = null;
            AdjacencyMatrix = new int[AdjacencyMatrixSize, AdjacencyMatrixSize];
            CleanVertices();
            Edges.Clear();
            for (int i = 0; i < AdjacencyMatrixSize; i++)
            {
                Split = Data[i].Split(new char[] { ' ' });
                for (int j = 0; j < Split.Length; j++)
                    AdjacencyMatrix[i, j] = Convert.ToInt32(Split[j]);
                if (Split.Length == AdjacencyMatrixSize)
                    Vertices.Add(new Vertex(i + 1));
            }
            IsDirected = false;
            for (int i = 0; i < AdjacencyMatrixSize; ++i)
                for (int j = 0; j < AdjacencyMatrixSize; ++j)
                    if (AdjacencyMatrix[i, j] != AdjacencyMatrix[j, i])
                    {
                        IsDirected = true;
                        break;
                    }
            UpdateVerticesRectangle();
            UpdateVerticesConnections(AdjacencyMatrix);
            AdjacencyMatrix = null;
            Split = null;
            Data = null;
        }
        /// <summary>
        /// Đọc ma trận kề có toạ độ các đỉnh từ file
        /// </summary>
        /// <param name="DataFilePath">File cần đọc</param>
        private void ReadAdjacencyMatrixWithRectangle(string DataFilePath)
        {
            int[,] AdjacencyMatrix = null;
            string[] Data = File.ReadAllLines(DataFilePath);
            int AdjacencyMatrixSize = Data.Length / 2;
            string[] Split = null;
            AdjacencyMatrix = new int[AdjacencyMatrixSize, AdjacencyMatrixSize];
            CleanVertices();
            Edges.Clear();
            for (int i = 0; i < AdjacencyMatrixSize; i++)
            {
                Split = Data[i].Split(new char[] { ' ' });
                for (int j = 0; j < Split.Length; j++)
                    AdjacencyMatrix[i, j] = Convert.ToInt32(Split[j]);
                if (Split.Length == AdjacencyMatrixSize)
                    Vertices.Add(new Vertex(i + 1));
            }
            for (int i = 0; i < AdjacencyMatrixSize; i++)
            {
                if (Data[i + AdjacencyMatrixSize].Length > 7 && IsOnVertices(i + 1))
                {
                    Split = Data[i + AdjacencyMatrixSize].Split(new char[] { ' ' });
                    Rectangle _RTemp = new Rectangle(Convert.ToInt32(Split[0]), Convert.ToInt32(Split[1]), Convert.ToInt32(Split[2]), Convert.ToInt32(Split[3]));
                    GetVertex(i + 1).Rectangle = _RTemp;
                }
            }
            IsDirected = false;
            for (int i = 0; i < AdjacencyMatrixSize; ++i)
                for (int j = 0; j < AdjacencyMatrixSize; ++j)
                    if (AdjacencyMatrix[i, j] != AdjacencyMatrix[j, i])
                    {
                        IsDirected = true;
                        break;
                    }
            UpdateVerticesConnections(AdjacencyMatrix);
            AdjacencyMatrix = null;
            Split = null;
            Data = null;
        }
        /// <summary>
        /// Đọc dữ liệu đồ thị từ file. 
        /// </summary>
        /// <param name="DataFilePath">File cần đọc</param>
        private void Read(string DataFilePath)
        {
            string[] Data = File.ReadAllLines(DataFilePath);
            int FileSize = Data.Length;
            string[] Split = Data[0].Split(new char[] { ' ' });
            int AdjacencyMatrixSize = Split.Length;
            if (AdjacencyMatrixSize == FileSize)
                ReadAdjacencyMatrix(DataFilePath);
            else
                ReadAdjacencyMatrixWithRectangle(DataFilePath);
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region Algorithms
        /// <summary>
        /// Duyệt đồ thị theo chiều sâu với giải thuật đệ quy
        /// </summary>
        /// <param name="AdjacencyMatrix">Ma trận kề</param>
        /// <param name="StartVertex">Đỉnh bắt đầu</param>
        /// <param name="Free">Mảng đánh dấu đã duyệt qua</param>
        private void DepthFirstSearch_Recursion(int[,] A, int StartVertex, ref bool[] Free)
        {
            Free[StartVertex] = false;
            GetVertex(StartVertex + 1).BackgroundColor = Color.Yellow;
            ReDraw();
            for (int i = 0; i <= A.GetUpperBound(1); i++)
                if (A[StartVertex, i] != 0 && Free[i] && StartVertex != i)
                    DepthFirstSearch_Recursion(A, i, ref Free);
        }
        /// <summary>
        /// Duyệt đồ thị theo chiều sâu dùng Stack
        /// </summary>
        /// <param name="AdjacencyMatrix">Ma trận kề</param>
        /// <param name="StartVertex">Đỉnh bắt đầu</param>
        private void DepthFirstSearch_Stack(int[,] A, int StartVertex, ref bool[] Free)
        {
            Stack<int> VerticesStack = new Stack<int>();
            VerticesStack.Push(StartVertex);
            Free[StartVertex] = false;
            while (VerticesStack.Count > 0)
            {
                int VertexIndex = VerticesStack.Pop();
                GetVertex(VertexIndex + 1).BackgroundColor = Color.Yellow;
                ReDraw();
                for (int i = A.GetUpperBound(0); i >= 0; i--)
                    if (Free[i] && A[VertexIndex, i] != 0 && VertexIndex != i)
                    {
                        VerticesStack.Push(i);
                        Free[i] = false;
                    }
            }
            VerticesStack.Clear();
            VerticesStack = null;
        }
        /// <summary>
        /// Duyệt đồ thị theo chiều rộng với giải thuật đệ quy
        /// </summary>
        /// <param name="AdjacencyMatrix">Ma trận kề</param>
        /// <param name="StartVertices">Danh sách các đỉnh sẽ duyệt</param>
        /// <param name="Free">Mảng đánh dấu đã duyệt qua</param>
        private void BreadthFirstSearch_Recursion(int[,] A, List<int> StartVertices, ref bool[] Free)
        {
            List<int> Sub = new List<int>();
            for (int i = 0; i < StartVertices.Count; i++)
            {
                Free[StartVertices[i]] = false;
                GetVertex(StartVertices[i] + 1).BackgroundColor = Color.Yellow;
                ReDraw();
                for (int j = 0; j <= A.GetUpperBound(0); j++)
                    if (A[StartVertices[i], j] != 0 && Free[j] && !Sub.Contains(j))
                        Sub.Add(j);
            }
            if (Sub.Count > 0)
                BreadthFirstSearch_Recursion(A, Sub, ref Free);
            Sub.Clear();
            Sub = null;

        }
        /// <summary>
        /// Duyệt đồ thị theo chiều rộng dùng Queue
        /// </summary>
        /// <param name="AdjacencyMatrix">Ma trận kề</param>
        /// <param name="StartVertex">Đỉnh bắt đầu</param>
        private void BreadthFirstSearch_Queue(int[,] A, int StartVertex, ref bool[] Free)
        {
            Queue<int> QU = new Queue<int>();// hàng đợi
            QU.Enqueue(StartVertex);
            Free[StartVertex] = false;
            while (QU.Count != 0)
            {
                int aVertex = QU.Dequeue();
                GetVertex(aVertex + 1).BackgroundColor = Color.Yellow;
                ReDraw();
                for (int v = 0; v <= A.GetUpperBound(0); v++)
                    if (Free[v] && A[aVertex, v] != 0 && aVertex != v)
                    {
                        QU.Enqueue(v);
                        Free[v] = false;
                    }
            }
            QU.Clear();
            QU = null;
        }
        /// <summary>
        /// Sử dụng giải thuật Loang để đánh dấu thành phần liên thông với 1 đỉnh
        /// </summary>
        /// <param name="Source">Đỉnh bắt đầu</param>
        /// <param name="EdgeColor">Màu sẽ đánh dấu vào cạnh</param>
        private void Loang(Vertex Source, Color EdgeColor) //BookmarkConnectedComponents
        {
            Source.Status = VertexStatus.sApproved;
            //foreach (Vertex Target in Source.ConnectionVertices)
            //{
            //    if (Target.Status != VertexStatus.sApproved)
            //    {
            //        Target.BorderColor = Source.BorderColor;
            //        Target.BackgroundColor = Source.BackgroundColor;
            //        Target.TextColor = Source.TextColor;
            //        GetEdge(Source, Target).Color = EdgeColor;
            //        Target.Status = VertexStatus.sConnected;
            //        Loang(Target, EdgeColor);
            //    }
            //}
            foreach (Vertex Target in Vertices)
            {
                Edge E1 = GetEdge(Source, Target);
                Edge E2 = GetEdge(Target, Source);
                if (Target.Status != VertexStatus.sApproved && (E1 != null || E2 != null))
                {
                    Target.BorderColor = Source.BorderColor;
                    Target.BackgroundColor = Source.BackgroundColor;
                    Target.TextColor = Source.TextColor;
                    if (E1 != null)
                        E1.Color = EdgeColor;
                    else if (E2 != null)
                        E2.Color = EdgeColor;
                    Target.Status = VertexStatus.sConnected;
                    Loang(Target, EdgeColor);
                }
            }
        }
        /// <summary>
        /// Tìm đường đi ngắn nhất giữa 2 đỉnh dùng giải thuật Dijkstra
        /// </summary>
        /// <param name="AdjacencyMatrix">Ma trận kề</param>
        /// <param name="SourceVertexValue">Giá trị của đỉnh bắt đầu</param>
        /// <param name="TargetVertexValue">Giá trị của đỉnh kết thúc</param>
        private void DijkstraAlgorithm(int[,] A, int SourceVertexValue, int TargetVertexValue)
        {
            int n = A.GetUpperBound(0) + 1;
            bool[] isFree = new bool[n];  //Mảng trạng thái
            int[] Trace = new int[n];     // mảng truy vết đường đi
            int[] Distance = new int[n];  // khoảng cách từ đỉnh xuất phát tới các dỉnh còn lại
            for (int i = 0; i < n; i++)
            {
                isFree[i] = true;
                Distance[i] = 9999;
            }
            Distance[SourceVertexValue] = 0; //đánh dấu để vòng lặp đầu tiên đưa điểm bắt đầu vào đường đi
            int MinDistance, MinDistanceVertex, Neighbor;
            bool IsStop = false;
            bool isNotFound = false;
            while (!IsStop)
            {
                // Tìm trong các đỉnh tự do 1 đỉnh i có d(i,MinDistanceVertex) bé nhất
                MinDistance = 9999;
                MinDistanceVertex = -1;
                for (int i = 0; i < n; i++)
                    if (Distance[i] < MinDistance && isFree[i])
                    {
                        MinDistance = Distance[i];
                        MinDistanceVertex = i;
                    }
                //// Đỉnh mới tìm được, nếu = đỉnh kết thúc hoặc tìm không được đỉnh nào nữa thì dừng vòng lặp
                if (MinDistanceVertex == -1 || MinDistanceVertex == TargetVertexValue)
                {
                    if (MinDistanceVertex == TargetVertexValue)
                    {
                        GetVertex(MinDistanceVertex + 1).BackgroundColor = Color.Yellow;
                        ReDraw();
                    }
                    IsStop = true;
                    if (MinDistanceVertex == -1)
                        isNotFound = true;
                }
                else
                {
                    isFree[MinDistanceVertex] = false; //đánh nhãn cho đỉnh đang xét là ko rảnh
                    GetVertex(MinDistanceVertex + 1).BackgroundColor = Color.Yellow;
                    ReDraw();
                    for (Neighbor = 0; Neighbor < n; Neighbor++)
                        if (isFree[Neighbor] && A[MinDistanceVertex, Neighbor] != 0)
                        {
                            bool KT = Distance[Neighbor] > Distance[MinDistanceVertex] + A[MinDistanceVertex, Neighbor];
                            Vertex VeTemp = GetVertex(Neighbor + 1);
                            string VeTempInfor = VeTemp.Infor;
                            VeTemp.BackgroundColor = Color.GreenYellow;
                            if (Distance[Neighbor] != 9999)
                            {
                                VeTemp.Infor = Distance[Neighbor].ToString() + " > " + Distance[MinDistanceVertex].ToString() + " + " + A[MinDistanceVertex, Neighbor].ToString() + " ?";
                                ReDraw();
                                VeTemp.Infor = KT ? "Có" : "Không";
                                ReDraw();
                                VeTemp.Infor = VeTempInfor;
                            }
                            if (KT)
                            {
                                Distance[Neighbor] = Distance[MinDistanceVertex] + A[MinDistanceVertex, Neighbor];
                                Trace[Neighbor] = MinDistanceVertex; //Đỉnh hàng xóm được chọn.
                                VeTemp.Infor = Distance[Neighbor].ToString();
                                VeTemp.BackgroundColor = Color.GreenYellow;
                                ReDraw();
                            }
                        }
                }
            }
            if (isNotFound)
            {
                MessageBox.Show("Không tìm được đường nào để đi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GetVertex(SourceVertexValue + 1).BorderColor = Color.Red;
                GetVertex(SourceVertexValue + 1).BackgroundColor = Color.Black;
                GetVertex(SourceVertexValue + 1).TextColor = Color.White;
                int f = TargetVertexValue;
                while (f != SourceVertexValue)
                {
                    GetVertex(f + 1).BorderColor = Color.Red;
                    GetVertex(f + 1).BackgroundColor = Color.Black;
                    GetVertex(f + 1).TextColor = Color.White;
                    f = Trace[f];
                }
                ReDraw();
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Uỷ quyền thao tác để chạy một hàm trong tiểu trình của ControlerForm
        /// </summary>
        private delegate void RunInControlerFormThread();
        /// <summary>
        /// Vẽ lại form
        /// </summary>
        private void UpdateControlerForm()
        {
            if (ControlerForm.InvokeRequired)
            {
                // Sử dụng
                // CheckForIllegalCrossThreadCalls = false;
                // cho Form thì khỏi xài
                // Hàm UpdateControlerForm sẽ được chạy trong Theard của Form (Thread Main)
                // Vì thế sử dụng sleep ở đây sẽ gây đứng chương trình.
                ControlerForm.Invoke(new RunInControlerFormThread(UpdateControlerForm));
            }
            else
            {
                ControlerForm.Invalidate();
            }
        }
        /// <summary>
        /// Vẽ lại
        /// </summary>
        private void ReDraw()
        {
            UpdateControlerForm();
            Thread.Sleep(1000);
        }
        /// <summary>
        /// Chạy một hàm trong tiểu trình
        /// </summary>
        /// <param name="Function"></param>
        private void RunInThread(ThreadStart Function)
        {
            // Create the thread object, passing in the _ReDraw method
            // via a ThreadStart delegate. This does not start the thread.
            GraphThread = new Thread(Function);
            GraphThread.Start();
            // Spin for a while waiting for the started thread to become
            // alive:
            while (!GraphThread.IsAlive) ;
            // Put the Main thread to sleep for 1 millisecond to allow oThread
            // to do some work:            
            Thread.Sleep(1);
            // Request that oThread be stopped
            //while(GraphThread.)
            //GraphThread.Abort();
            //// Wait until oThread finishes. Join also has overloads
            //// that take a millisecond interval or a TimeSpan object.
            //GraphThread.Join();            
        }
        /// <summary>
        /// Chạy Dijkstra
        /// </summary>
        private void RunDijkstra()
        {
            //try
            //{
                ResetVetiesValue();
                int Vertex1Value = InputVertex("Nhập giá trị của đỉnh bắt đầu:", "Dijkstra [1/2]", false);
                Vertex Vertex1 = GetVertex(Vertex1Value);
                int Vertex2Value = InputVertex("Nhập giá trị của đỉnh kết thúc:", "Dijkstra [2/2]", false);
                Vertex Vertex2 = GetVertex(Vertex2Value);
                MessageBox.Show("Bắt đầu chạy Dijkstra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DijkstraAlgorithm(GetAdjacencyMatrix(), Vertex1Value - 1, Vertex2Value - 1);
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " Sẽ không chạy Dijkstra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        /// <summary>
        /// Chạy duyệt đồ thì theo chiều sâu dùng đệu quy
        /// </summary>
        private void RunDepthFirstSearch_Recursion()
        {
            try
            {
                ResetVetiesValue();
                int[,] AdjacencyMatrix = GetAdjacencyMatrix();
                bool[] Free = new bool[AdjacencyMatrix.GetUpperBound(0) + 1];
                for (int i = 0; i <= Free.GetUpperBound(0); i++) Free[i] = true;
                DepthFirstSearch_Recursion(AdjacencyMatrix, InputVertex("Nhập giá trị của đỉnh bắt đầu:", "Duyệt đồ thị theo chiều sâu bằng đệ quy [1/1]", false) - 1, ref Free);
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (Free[Vertices[i].Value - 1])
                        DepthFirstSearch_Recursion(AdjacencyMatrix, Vertices[i].Value - 1, ref Free);
                }
                MessageBox.Show("Duyệt Xong!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không chạy duyệt đồ thị theo chiều sâu bằng đệ quy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Chạy duyệt đồ thị theo chiều sâu dùng Stack
        /// </summary>
        private void RunDepthFirstSearch_Stack()
        {
            try
            {
                ResetVetiesValue();
                int[,] AdjacencyMatrix = GetAdjacencyMatrix();
                bool[] Free = new bool[AdjacencyMatrix.GetUpperBound(0) + 1];
                for (int i = 0; i <= Free.GetUpperBound(0); i++) Free[i] = true;
                DepthFirstSearch_Stack(AdjacencyMatrix, InputVertex("Nhập giá trị của đỉnh bắt đầu:", "Duyệt đồ thị theo chiều sâu bằng Stack [1/1]", false) - 1, ref Free);
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (Free[Vertices[i].Value - 1])
                        DepthFirstSearch_Stack(AdjacencyMatrix, Vertices[i].Value - 1, ref Free);
                }
                MessageBox.Show("Duyệt Xong!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không chạy duyệt đồ thị theo chiều sâu bằng Stack", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void BreadthFirstSearch_Recursion(int[,] A, ref bool[] Free, int i)
        //{
        //    List<int> X = new List<int>();
        //    X.Add(i);
        //    GetVertex(i + 1).BackgroundColor = Color.Yellow;
        //    ReDraw();
        //    Free[i] = false;
        //    List<int> Y;

        //    while (X.Count != 0)
        //    {
        //        // MessageBox.Show(A.Count.ToString());
        //        Y = new List<int>();
        //        for (int u = 0; u < X.Count; u++)//voi moi dinh u thuoc X
        //            for (int v = 0; v <= A.GetUpperBound(0); v++)
        //                if (Free[v] && A[X[u], v] != 0)
        //                {
        //                    Free[v] = false;
        //                    Y.Add(v);
        //                    GetVertex(v + 1).BackgroundColor = Color.Yellow;
        //                    ReDraw();
        //                }
        //        X = Y;
        //    }
        //}

        /// <summary>
        /// Chạy duyệt đồ thị theo chiều rộng dùng đệ quy
        /// </summary>
        private void RunBreadthFirstSearch_Recursion()
        {
            try
            {
                ResetVetiesValue();
                int[,] AdjacencyMatrix = GetAdjacencyMatrix();
                bool[] Free = new bool[AdjacencyMatrix.GetUpperBound(0) + 1];
                for (int i = 0; i <= Free.GetUpperBound(0); i++) Free[i] = true;
                List<int> Ve = new List<int>();
                Ve.Add(InputVertex("Nhập giá trị của đỉnh bắt đầu:", "Duyệt đồ thị theo chiều rộng bằng đệ quy [1/1]", false) - 1);
                BreadthFirstSearch_Recursion(AdjacencyMatrix, Ve, ref Free);

                //int j = InputVertex("Nhập giá trị của đỉnh bắt đầu:", "Duyệt đồ thị theo chiều rộng bằng đệ quy [1/1]", false) - 1;
                //BreadthFirstSearch_Recursion(AdjacencyMatrix, ref Free, j);
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (Free[Vertices[i].Value - 1])
                    {
                        Ve.Clear();
                        Ve.Add(Vertices[i].Value - 1);
                        BreadthFirstSearch_Recursion(AdjacencyMatrix, Ve, ref Free);
                        //BreadthFirstSearch_Recursion(AdjacencyMatrix, ref Free, i);
                    }
                }
                MessageBox.Show("Duyệt Xong!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không chạy duyệt đồ thị theo chiều rộng bằng đệ quy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Chạy duyệt đồ thị theo chiều rộng dùng đệ quy
        /// </summary>
        private void RunBreadthFirstSearch_Queue()
        {
            try
            {
                ResetVetiesValue();
                int[,] AdjacencyMatrix = GetAdjacencyMatrix();
                bool[] Free = new bool[AdjacencyMatrix.GetUpperBound(0) + 1];
                for (int i = 0; i <= Free.GetUpperBound(0); i++) Free[i] = true;
                BreadthFirstSearch_Queue(AdjacencyMatrix, InputVertex("Nhập giá trị của đỉnh bắt đầu:", "Duyệt đồ thị theo chiều rộng bằng Queue [1/1]", false) - 1, ref Free);
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (Free[Vertices[i].Value - 1])
                        BreadthFirstSearch_Queue(AdjacencyMatrix, Vertices[i].Value - 1, ref Free);
                }
                MessageBox.Show("Duyệt Xong!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không chạy duyệt đồ thị theo chiều rộng bằng Queue", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Vẽ lại đồ thị / dùng trong sự kiện Paint của Form
        /// </summary>
        /// <param name="e"></param>
        public void OnPaint(PaintEventArgs e)
        {
            if (Vertices.Count > 0)
            {
                foreach (Edge _Etemp in Edges)
                {
                    _Etemp.DrawIt(e.Graphics, IsDirected);
                }

                foreach (Vertex _Vtemp in Vertices)
                {
                    _Vtemp.DrawIt(e.Graphics);
                    _Vtemp.Status = VertexStatus.sFree;
                }
            }
        }

        #region Mouse Events
        /// <summary>
        /// Hiện menu khi click vào đồ thị
        /// </summary>
        /// <param name="e"></param>
        /// <param name="contextMenuMain">menu khi click ngoài đồ thị</param>
        /// <param name="contextMenuEdge">menu khi click vào cạnh</param>
        /// <param name="contextMenuVertex">menu khi click vào đỉnh</param>
        public void OnMouseClick(MouseEventArgs e, ContextMenuStrip contextMenuMain, ContextMenuStrip contextMenuEdge, ContextMenuStrip contextMenuVertex)
        {
            if (e.Button == MouseButtons.Right)
            {
                bool IsOnVertexOrEdge = false;
                if (Contains(e.Location, ItemStyle.aEdge, ref ClickingEdgeIndex))
                {
                    contextMenuEdge.Items[2].Enabled = IsDirected;
                    contextMenuEdge.Show(ControlerForm, e.Location);
                    IsOnVertexOrEdge = true;
                }
                if (Contains(e.Location, ItemStyle.aVertex, ref ClickingVertexIndex))
                {
                    contextMenuVertex.Show(ControlerForm, e.Location);
                    IsOnVertexOrEdge = true;
                }
                if (!IsOnVertexOrEdge)
                {
                    contextMenuMain.Show(ControlerForm, e.Location);
                    MenuLocation = e.Location;
                }
            }
        }
        /// <summary>
        /// Bắt đầu sự kiện kéo thả chuột
        /// </summary>
        /// <param name="e"></param>
        public void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsOnDrap = true;
                MouseLocation = e.Location;
                RectTemp = new Rectangle[Vertices.Count];
                for (int i = 0; i < Vertices.Count; i++)
                {
                    RectTemp[i] = Vertices[i].Rectangle;
                    if (Vertices[i].Rectangle.Contains(e.Location))
                    {
                        MovingVertexIndex = i;
                        IsDrapAVertex = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// kết thúc sự kiện kéo thả chuột
        /// </summary>
        /// <param name="e"></param>
        public void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsOnDrap)
                    IsOnDrap = false;
                if (IsDrapAVertex)
                    IsDrapAVertex = false;
            }
        }
        /// <summary>
        /// Khi chuột đang di chuyển
        /// </summary>
        /// <param name="e"></param>
        public void OnMouseMove(MouseEventArgs e)
        {
            if (IsOnDrap)
            {
                if (IsDrapAVertex)
                {
                    Rectangle _Temp = new Rectangle(RectTemp[MovingVertexIndex].Location.X + (e.X - MouseLocation.X), RectTemp[MovingVertexIndex].Location.Y + (e.Y - MouseLocation.Y), Vertices[0].Rectangle.Width, Vertices[0].Rectangle.Height);
                    Vertices[MovingVertexIndex].Rectangle = _Temp;
                }
                else
                {
                    for (int i = 0; i < Vertices.Count; i++)
                    {
                        Rectangle _Temp = new Rectangle(RectTemp[i].Location.X + (e.X - MouseLocation.X), RectTemp[i].Location.Y + (e.Y - MouseLocation.Y), Vertices[i].Rectangle.Width, Vertices[i].Rectangle.Height);
                        Vertices[i].Rectangle = _Temp;
                    }
                }
                ControlerForm.Invalidate();
            }
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region Button events
        /// <summary>
        /// Thêm đỉnh
        /// </summary>
        /// <param name="isHaveLocation">Có biết vị trí đỉnh cần thêm chưa?</param>
        public void RunAddVertex(bool isHaveLocation)
        {
            try
            {
                if (Vertices.Count == 0)
                {
                    bool Checker;
                    do 
                    {
                        Checker = true;
                        int aNumber = InputNumber("Bạn cần tạo loại đồ thị gì?\n\t1. Có hướng\n\t2. Vô hướng", "Loại đồ thị", false);
                        if (aNumber == 1)
                            IsDirected = true;
                        else if (aNumber == 2)
                            IsDirected = false;
                        else
                        {
                            MessageBox.Show("Nhập số tương ứng để chọn câu trả lời! Đề nghị nhập lại một trong 2 số 1 và 2.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Checker = false;
                        }
                    } while (!Checker);
                    
                }
                int NewVertexValue = InputVertex("Nhập giá trị của đỉnh:", "Thêm đỉnh mới [1/3]", true);
                Vertex _Vtemp;
                if (isHaveLocation)
                {
                    _Vtemp = new Vertex(MenuLocation, NewVertexValue);
                }
                else
                {
                    _Vtemp = new Vertex(NewVertexValue);
                    _Vtemp.UseDefaultRectangle(Vertices.Count, ControlerForm.ClientSize.Width, ControlerForm.ClientSize.Height);
                }
                bool aChecker;
                int NumberOfVertexConnected;
                do 
                {
                    aChecker = true;
                    NumberOfVertexConnected = InputNumber("Nhập số lượng đỉnh có kết nối với đỉnh này:", "Thêm đỉnh mới [2/3]", false);
                    if (NumberOfVertexConnected > Vertices.Count)
                    {
                        MessageBox.Show("Đồ thị hiện tại có " + Vertices.Count + " đỉnh. Đề nghị nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        aChecker = false;
                    }

                } while (!aChecker);
                for (int i = 0; i < NumberOfVertexConnected; i++)
                {
                    int ConnectedVertexValue = InputVertex("Cạnh thứ " + (i + 1).ToString() + " nối từ đỉnh " + NewVertexValue + " tới đỉnh: ", "Thêm đỉnh mới [2." + (i + 1).ToString() + "a/2]", false);
                    Vertex ConnectedVertex = GetVertex(ConnectedVertexValue);
                    int EdgeWeight = InputNumber("Cạnh vừa tạo có giá trị: ", "Thêm đỉnh mới [2." + (i + 1).ToString() + "b/2]", true);
                    if (IsDirected)
                    {
                        bool Checker;
                        do
                        {
                            Checker = true;
                            int aNumber = InputNumber("Chọn hướng của cạnh vừa tạo?\n\t1. Có hướng từ đỉnh " + _Vtemp.Value + " tới đỉnh " + ConnectedVertex.Value + "\n\t2. Có hướng từ đỉnh " + ConnectedVertex.Value + " tới đỉnh " + _Vtemp.Value, "Thêm đỉnh mới [2." + (i + 1).ToString() + "c/2]", false);
                            if (aNumber == 1)
                                AddConection(_Vtemp, ConnectedVertex, EdgeWeight);
                            else if (aNumber == 2)
                                AddConection(ConnectedVertex ,_Vtemp, EdgeWeight);
                            else
                            {
                                MessageBox.Show("Nhập số tương ứng để chọn câu trả lời! Đề nghị nhập lại một trong 2 số 1 và 2.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Checker = false;
                            }
                        } while (!Checker);
                    }
                    else
                        AddConection(_Vtemp, ConnectedVertex, EdgeWeight);
                }
                Vertices.Add(_Vtemp);
                ControlerForm.Invalidate();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không có đỉnh nào được thêm vào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Thêm đỉnh
        /// </summary>
        public void AddVertex()
        {
            RunAddVertex(false);
        }
        /// <summary>
        /// Thêm cạnh
        /// </summary>
        public void AddEdge()
        {
            try
            {
                int Vertex1Value = InputVertex("Nhập giá trị của đỉnh mà cạnh bắt đầu:", "Thêm cạnh mới [1/3]", false);
                Vertex Vertex1 = GetVertex(Vertex1Value);
                bool isOK = true;
                do
                {
                    int Vertex2Value = InputVertex("Nhập giá trị của đỉnh mà cạnh kết thúc:", "Thêm cạnh mới [2/3]", false);
                    Vertex Vertex2 = GetVertex(Vertex2Value);
                    if (Vertex2.ConnectionVertices.Contains(Vertex1))
                    {
                        MessageBox.Show("Hix, đỉnh " + Vertex1Value + " và đỉnh " + Vertex2Value + " đã được nối với nhau rồi! Bấm OK để nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isOK = false;
                    }
                    else
                    {
                        int EdgeWeight = InputNumber("Cạnh vừa tạo có giá trị: ", "Thêm cạnh mới [3/3]", true);
                        RemoveConection(Vertex1, Vertex2);
                        AddConection(Vertex1, Vertex2, EdgeWeight);
                        ControlerForm.Invalidate();
                        isOK = true;
                    }
                } while (!isOK);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không có cạnh nào được thêm vào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Đổi trọng số
        /// </summary>
        public void ChangeEdgeWeight()
        {
            try
            {
                bool isOK = true;
                do
                {
                    int Vertex1Value = InputVertex("Nhập giá trị của đỉnh mà cạnh bắt đầu:", "Thay đổi trọng số [1/3]", false);
                    Vertex Vertex1 = GetVertex(Vertex1Value);
                    int Vertex2Value = InputVertex("Nhập giá trị của đỉnh mà cạnh kết thúc:", "Thay đổi trọng số [2/3]", false);
                    Vertex Vertex2 = GetVertex(Vertex2Value);
                    if (!Vertex1.ConnectionVertices.Contains(Vertex2))
                    {
                        MessageBox.Show("Hix, đỉnh " + Vertex1Value + " và đỉnh " + Vertex2Value + " chưa được nối với nhau mà! Bấm OK để nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isOK = false;
                    }
                    else
                    {
                        int EdgeWeight = InputNumber("Cạnh vừa chọn có giá trị: ", "Thay đổi trọng số [3/3]", true);
                        //RemoveConection(Vertex1, Vertex2);
                        //AddConection(Vertex1, Vertex2, EdgeWeight);
                        GetEdge(Vertex1, Vertex2).Value = EdgeWeight;
                        ControlerForm.Invalidate();
                        isOK = true;
                    }

                } while (!isOK);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không có cạnh nào được thêm vào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Xoá đỉnh
        /// </summary>
        public void DeleteVertex()
        {
            try
            {
                int VertexValue = InputVertex("Nhập giá trị của đỉnh:", "Xoá đỉnh [1/1]", false);
                Vertex _Vtemp = GetVertex(VertexValue);
                RemoveAllConections(_Vtemp);
                Vertices.Remove(_Vtemp);
                ControlerForm.Invalidate();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không có đỉnh nào bị xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Xoá cạnh
        /// </summary>
        public void DeleteEdge()
        {
            try
            {
                bool isOK = true;
                do
                {
                    int Vertex1Value = InputVertex("Nhập giá trị của đỉnh mà cạnh bắt đầu:", "Xoá cạnh [1/2]", false);
                    Vertex Vertex1 = GetVertex(Vertex1Value);
                    int Vertex2Value = InputVertex("Nhập giá trị của đỉnh mà cạnh kết thúc:", "Xoá cạnh [2/2]", false);
                    Vertex Vertex2 = GetVertex(Vertex2Value);
                    int[,] AdjacencyMatrix = GetAdjacencyMatrix();
                    // if (AdjacencyMatrix[Vertex1Value, Vertex2Value] != 0)
                    if (!Vertex1.ConnectionVertices.Contains(Vertex2))
                    {
                        MessageBox.Show("Hix, đỉnh " + Vertex1Value + " và đỉnh " + Vertex2Value + " chưa được nối với nhau mà! Bấm OK để nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isOK = false;
                    }
                    else
                    {
                        RemoveConection(Vertex1, Vertex2);
                        ControlerForm.Invalidate();
                        isOK = true;
                    }
                } while (!isOK);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không có cạnh nào bị xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Lưu ma trận kề
        /// </summary>
        public void SaveAdjacencyMatrix()
        {
            System.Windows.Forms.SaveFileDialog Dialog = new System.Windows.Forms.SaveFileDialog();
            //Dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Dialog.Filter = "File ma trận kề (*.TXT)|*.TXT|All Files (*.*)|*.*";
            Dialog.FilterIndex = 1;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                WriteAdjacencyMatrix(Dialog.FileName);
                MessageBox.Show("Đã xuất ma trận kề của đồ thị hiện tại xong", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Lưu ma trận kề có toạ độ của các đỉnh
        /// </summary>
        public void SaveAdjacencyMatrixWithRectangle()
        {
            System.Windows.Forms.SaveFileDialog Dialog = new System.Windows.Forms.SaveFileDialog();
            //Dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Dialog.Filter = "File ma trận kề có vị trí các đỉnh (*.AMR)|*.AMR|All Files (*.*)|*.*";
            Dialog.FilterIndex = 1;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                WriteAdjacencyMatrixWithRectangle(Dialog.FileName);
                MessageBox.Show("Đã xuất ma trận kề cùng với tạo độ các đỉnh của đồ thị hiện tại xong", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Mở ma trận kề
        /// </summary>
        public void Open()
        {
            System.Windows.Forms.OpenFileDialog Dialog = new System.Windows.Forms.OpenFileDialog();
            //Dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Dialog.Filter = "File đồ thị (*.TXT, *.AMR)|*.TXT;*.AMR|File ma trận kề có toạ độ(*.AMR)|*.AMR|File ma trận kề (*.TXT)|*.TXT|All Files (*.*)|*.*";
            Dialog.FilterIndex = 1;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                Read(Dialog.FileName);
                MessageBox.Show("Đã đọc đồ thị từ file xong", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ControlerForm.Invalidate();
            }
        }
        /// <summary>
        /// Tìm màu tương phản của màu
        /// </summary>
        /// <param name="ColorToInvert">Màu nguồn</param>
        /// <returns>Màu tương phản</returns>
        private Color InvertAColor(Color ColorToInvert)
        {
            return Color.FromArgb(255 - ColorToInvert.R, 255 - ColorToInvert.G, 255 - ColorToInvert.B);
        }
        /// <summary>
        /// Đánh dấu thành phần liên thông
        /// </summary>
        public void BookmarkConnectedComponents()
        {
            //if (IsDirected)
            //{
            //    MessageBox.Show("Thuật toán này chỉ chạy trên đồ thị vô hướng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            ResetVetiesValue();
            Color[] Colors = { Color.Blue, Color.Green, Color.Red, Color.Orange, Color.Brown, Color.Black, Color.Magenta, Color.DarkViolet, Color.DarkCyan, Color.DarkSalmon };
            int GroupNumber = 0;
            foreach (Vertex Source in Vertices)
            {
                if (Source.Status == VertexStatus.sFree)
                {
                    Source.BorderColor = Colors[GroupNumber];
                    Source.BackgroundColor = InvertAColor(Colors[GroupNumber]);
                    Source.TextColor = Colors[GroupNumber];
                    Loang(Source, Colors[GroupNumber]);
                    GroupNumber++;
                    if (GroupNumber > Colors.Length - 1)
                        GroupNumber = 0;
                }
            }
            ControlerForm.Invalidate();
        }

        public void Coloring()
        {
            if (IsDirected)
            {
                MessageBox.Show("Thuật toán này chỉ chạy trên đồ thị vô hướng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ResetVetiesValue();
            Color[] Colors = { Color.Blue, Color.Green, Color.Yellow, Color.White, Color.Thistle, Color.Orange, Color.Brown, Color.Black, Color.Magenta, Color.DarkViolet, Color.DarkCyan, Color.DarkSalmon };

            IEnumerable<int> _Colors = Graph_Coloring.GraphColoringAlgorithm.Run(GetAdjacencyMatrix());
            int i = 0;
            foreach (int C in _Colors)
            {
                i++;
                GetVertex(i).BackgroundColor = Colors[C];
            }

            ControlerForm.Invalidate();
        }

        /// <summary>
        /// Cho đồ thị về màu mặc định
        /// </summary>
        public void ResetColor()
        {
            ResetVetiesValue();

            ControlerForm.Invalidate();
        }
        /// <summary>
        /// Tìm đường đi ngằn nhất
        /// </summary>
        public void Dijsktra()
        {
            RunInThread(new ThreadStart(RunDijkstra));
        }
        /// <summary>
        /// Duyệt theo chiều sâu dùng đệ quy
        /// </summary>
        public void DepthFirstSearch_Recursion()
        {
            RunInThread(new ThreadStart(RunDepthFirstSearch_Recursion));
        }
        /// <summary>
        /// Duyệt theo chiều sâu dùng Stack
        /// </summary>
        public void DepthFirstSearch_Stack()
        {
            RunInThread(new ThreadStart(RunDepthFirstSearch_Stack));
        }
        /// <summary>
        /// Duyệt theo chiều rộng dùng đệ quy
        /// </summary>
        public void BreadthFirstSearch_Recursion()
        {
            RunInThread(new ThreadStart(RunBreadthFirstSearch_Recursion));
        }
        /// <summary>
        /// Duyệt theo chiều rộng dùng Queue
        /// </summary>
        public void BreadthFirstSearch_Queue()
        {
            RunInThread(new ThreadStart(RunBreadthFirstSearch_Queue));
        }
        /// <summary>
        /// Tạo mới đồ thị
        /// </summary>
        public void New()
        {
            CleanVertices();
            Edges.Clear();
            ControlerForm.Invalidate();
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region Menu Events
        /// <summary>
        /// Xoá cạnh đang tương tác
        /// </summary>
        public void DeleteEdgeToolStripMenuItem()
        {
            RemoveConection(Edges[ClickingEdgeIndex].Vertex1, Edges[ClickingEdgeIndex].Vertex2);
            ControlerForm.Invalidate();
        }
        /// <summary>
        /// Đổi trọng số của cạnh đang tương tác
        /// </summary>
        public void ChangeEdgeWeightNumberToolStripMenuItem()
        {
            try
            {
                int EdgeWeight = InputNumber("Cạnh vừa chọn có giá trị: ", "Thay đổi trọng số [1/1]", true);
                //RemoveConection(Edges[ClickingEdgeIndex].Vertex1, Edges[ClickingEdgeIndex].Vertex2);
                //AddConection(Edges[ClickingEdgeIndex].Vertex1, Edges[ClickingEdgeIndex].Vertex2, EdgeWeight);
                GetEdge(Edges[ClickingEdgeIndex].Vertex1, Edges[ClickingEdgeIndex].Vertex2).Value = EdgeWeight;
                ControlerForm.Invalidate();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không thay đổi trọng số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Xoá đỉnh đang tương tác
        /// </summary>
        public void DeleteVertexToolStripMenuItem()
        {
            RemoveAllConections(Vertices[ClickingVertexIndex]);
            Vertices.Remove(Vertices[ClickingVertexIndex]);
            ControlerForm.Invalidate();
        }
        /// <summary>
        /// Nối đỉnh đang thao tác tới 1 đỉnh khác
        /// </summary>
        public void MakeAConnectionToolStripMenuItem()
        {
            try
            {
                bool isOK = true;
                do
                {
                    int Vertex2Value = InputVertex("Nhập giá trị của đỉnh mà cạnh kết thúc:", "Thêm cạnh mới [1/2]", false);
                    Vertex Vertex2 = GetVertex(Vertex2Value);
                    if (Vertex2.ConnectionVertices.Contains(Vertices[ClickingVertexIndex]))
                    {
                        MessageBox.Show("Hix, đỉnh " + Vertices[ClickingVertexIndex].Value + " và đỉnh " + Vertex2Value + " đã được nối với nhau rồi! Bấm OK để nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isOK = false;
                    }
                    else
                    {
                        int EdgeWeight = InputNumber("Cạnh vừa tạo có giá trị: ", "Thêm cạnh mới [2/2]", true);
                        RemoveConection(Vertices[ClickingVertexIndex], Vertex2);
                        AddConection(Vertices[ClickingVertexIndex], Vertex2, EdgeWeight);
                        ControlerForm.Invalidate();
                        isOK = true;
                    }
                } while (!isOK);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " Sẽ không có cạnh nào được thêm vào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Thêm đỉnh tại vị trí hiện tại
        /// </summary>
        public void AddAVertexHereToolStripMenuItem()
        {
            RunAddVertex(true);
        }
        public void ReversalToolStripMenuItem()
        {
            Edges[ClickingEdgeIndex].Reversal();
            ControlerForm.Invalidate();
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////
        #region ViewMatrix
        /// <summary>
        /// Hộp thoại hiển thị các cách biểu diễn của đồ thị
        /// </summary>
        /// <param name="title">Tiêu đề</param>
        /// <param name="Style">Kiểu biểu diễn</param>
        private void ViewMatrix(string title, ViewMatrixStyle Style)
        {
            Form form = new Form();
            ListView listViewMatrix = new ListView();
            Button butOK = new Button();
            listViewMatrix.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            listViewMatrix.SetBounds(0, 0, 559, 459);
            butOK.Anchor = AnchorStyles.Bottom;
            //butOK.DialogResult = DialogResult.OK;
            butOK.SetBounds(223, 476, 112, 25);
            butOK.Text = "OK";
            form.Text = title;
            form.ClientSize = new Size(559, 508);
            form.Controls.AddRange(new Control[] { butOK, listViewMatrix });
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.CancelButton = butOK;
            switch (Style)
            {
                case ViewMatrixStyle.vIncidenceList:
                    ViewIncidenceList(GetAdjacencyMatrix(), listViewMatrix);
                    break;
                case ViewMatrixStyle.vAdjacencyList:
                    ViewAdjacencyList(GetAdjacencyMatrix(), listViewMatrix);
                    break;
                case ViewMatrixStyle.vAdjacencyMatrix:
                    ViewAdjacencyMatrix(GetAdjacencyMatrix(), listViewMatrix);
                    break;
                case ViewMatrixStyle.vIncidenceMatrix:
                    ViewIncidenceMatrix(GetAdjacencyMatrix(), listViewMatrix);
                    break;
            }
            DialogResult dialogResult = form.ShowDialog();
            // return dialogResult;
        }
        /// <summary>
        /// Biểu diễn ma trận kề
        /// </summary>
        /// <param name="A">Ma trận kề</param>
        /// <param name="ViewMatrix">ListView sẽ biểu diễn</param>
        private void ViewAdjacencyMatrix(int[,] A, ListView ViewMatrix)
        {
            int n = A.GetUpperBound(0) + 1;
            ViewMatrix.Clear();
            ViewMatrix.Columns.Add("", 30, HorizontalAlignment.Center);//Them mot cot vao listview(ten field,do rong cot,canh vi tri chuoi trong cot)            
            ViewMatrix.View = View.Details;//chon che do trinh bay details
            for (int i = 0; i < n; i++)
            {
                ViewMatrix.Columns.Add((i + 1).ToString(), 25, HorizontalAlignment.Right);//Them cac cot vao Listview
                ListViewItem Item = new ListViewItem((i + 1).ToString());//khoi tao doi tuong Item la kieu listviewitems
                for (int j = 0; j < n; j++)
                {
                    //Item.SubItems.Add(A[i, j].ToString());//gan gia tri vao doi tuong Item
                    if (A[i, j] != 0)
                        Item.SubItems.Add("1");
                    else
                        Item.SubItems.Add("0");
                }
                ViewMatrix.Items.Add(Item);//cho the hien Item tren lvmatran
            }
        }
        /// <summary>
        /// Biểu diễn ma trận liên thuộc
        /// </summary>
        /// <param name="A">Ma trận kề</param>
        /// <param name="ViewMatrix">ListView sẽ biểu diễn</param>
        private void ViewIncidenceMatrix(int[,] A, ListView ViewMatrix)
        {
            ViewMatrix.Clear();
            ViewMatrix.Columns.Add("", 50, HorizontalAlignment.Center);
            ViewMatrix.View = View.Details;
            for (int i = 0; i <= A.GetUpperBound(0); i++)
                ViewMatrix.Columns.Add("Đỉnh " + (i + 1).ToString(), 50, HorizontalAlignment.Right);
            int socanh = 1;
            if (IsDirected)
            {
                for (int i = 0; i <= A.GetUpperBound(0); i++)
                    for (int j = 0; j <= A.GetUpperBound(1); j++)
                        if (A[i, j] != 0)
                        {
                            ListViewItem Item = new ListViewItem("Cạnh " + socanh.ToString());
                            for (int k = 1; k <= A.GetUpperBound(0) + 2; k++)
                            {
                                Item.SubItems.Add("0");
                            }
                            Item.SubItems[i + 1].Text = "1";
                            Item.SubItems[j + 1].Text = "1";
                            socanh++;
                            ViewMatrix.Items.Add(Item);
                        }
            }
            else
            {
                for (int i = 0; i <= A.GetUpperBound(0); i++)
                    for (int j = i; j <= A.GetUpperBound(1); j++)
                        if (A[i, j] != 0)
                        {
                            ListViewItem Item = new ListViewItem("Cạnh " + socanh.ToString());
                            for (int k = 1; k <= A.GetUpperBound(0) + 2; k++)
                            {
                                Item.SubItems.Add("0");
                            }
                            Item.SubItems[i + 1].Text = "1";
                            Item.SubItems[j + 1].Text = "1";
                            socanh++;
                            ViewMatrix.Items.Add(Item);
                        }
            }
        }
        /// <summary>
        /// Biểu diễn sanh sách kề
        /// </summary>
        /// <param name="A">Ma trận kề</param>
        /// <param name="ViewMatrix">ListView sẽ biểu diễn</param>
        private void ViewAdjacencyList(int[,] A, ListView ViewMatrix)
        {
            int n = A.GetUpperBound(0) + 1;
            ViewMatrix.Clear();
            ViewMatrix.Columns.Add("Đỉnh", 50, HorizontalAlignment.Center);
            ViewMatrix.Columns.Add("Các đỉnh kề", 100, HorizontalAlignment.Left);
            ViewMatrix.View = View.Details;
            ListViewItem Item;
            for (int i = 0; i < n; i++)
            {
                string s = "";
                for (int j = 0; j < n; j++)
                    if (A[i, j] != 0)
                        s = s + (j + 1).ToString() + " ";
                Item = new ListViewItem((i + 1).ToString());
                Item.SubItems.Add(s);
                ViewMatrix.Items.Add(Item);
            }
        }
        /// <summary>
        /// Biểu diễn danh sách liên thuộc (Danh sách cạnh)
        /// </summary>
        /// <param name="A">Ma trận kề</param>
        /// <param name="ViewMatrix">ListView sẽ biểu diễn</param>
        private void ViewIncidenceList(int[,] A, ListView ViewMatrix)
        {
            int n = A.GetUpperBound(0) + 1;
            ViewMatrix.Clear();
            ViewMatrix.Columns.Add("STT", 50, HorizontalAlignment.Center);
            ViewMatrix.Columns.Add("Đỉnh 1", 50, HorizontalAlignment.Center);
            ViewMatrix.Columns.Add("Đỉnh 2", 50, HorizontalAlignment.Center);
            ViewMatrix.View = View.Details;
            ListViewItem Item;
            int socanh = 1;
            if (IsDirected)
            {
                for (int i = 0; i < n - 1; i++)
                    for (int j = 0; j < n; j++)
                        if (A[i, j] != 0)
                        {
                            Item = new ListViewItem(socanh.ToString());
                            Item.SubItems.Add((i + 1).ToString());
                            Item.SubItems.Add((j + 1).ToString());
                            ViewMatrix.Items.Add(Item);
                            socanh++;
                        }
            }
            else
            {
                for (int i = 0; i < n - 1; i++)            
                    for (int j = i; j < n; j++)
                        if (A[i, j] != 0)
                        {
                            Item = new ListViewItem(socanh.ToString());
                            Item.SubItems.Add((i + 1).ToString());
                            Item.SubItems.Add((j + 1).ToString());
                            ViewMatrix.Items.Add(Item);
                            socanh++;
                        }
            }
        }
        /// <summary>
        /// Hiện ma trận kề
        /// </summary>
        public void ShowAdjacencyMatrix()
        {
            ViewMatrix("Ma trận kề", ViewMatrixStyle.vAdjacencyMatrix);
        }
        /// <summary>
        /// Hiện ma trận liên thuộc
        /// </summary>
        public void ShowIncidenceMatrix()
        {
            ViewMatrix("Ma trận liên thuộc", ViewMatrixStyle.vIncidenceMatrix);
        }
        /// <summary>
        /// Hiên danh sách kề
        /// </summary>
        public void ShowAdjacencyList()
        {
            ViewMatrix("Danh sách kề", ViewMatrixStyle.vAdjacencyList);
        }
        /// <summary>
        /// Hiện danh sách liên thuộc (Danh sách cạnh)
        /// </summary>
        public void ShowIncidenceList()
        {
            ViewMatrix("Danh sách cạnh", ViewMatrixStyle.vIncidenceList);
        }
        #endregion

    }
}
