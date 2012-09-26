using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Graphs_Viewer
{
    public class Edge
    {
        /// <summary>
        /// Đỉnh 1
        /// </summary>
        public Vertex Vertex1;
        /// <summary>
        /// Đỉnh 2
        /// </summary>
        public Vertex Vertex2;
        /// <summary>
        /// Vị trí và kích cỡ
        /// </summary>
        public Rectangle Rectangle = new Rectangle();
        /// <summary>
        /// Giá trị
        /// </summary>
        public int Value;
        /// <summary>
        /// Kiểu chữ
        /// </summary>
        public Font Font;
        /// <summary>
        /// Màu chữ
        /// </summary>
        public Color TextColor;
        /// <summary>
        /// Màu nền
        /// </summary>
        public Color BackgroundColor;
        /// <summary>
        /// Màu viền
        /// </summary>
        public Color BorderColor;
        /// <summary>
        /// Kích cỡ viễn
        /// </summary>
        public int BorderSize;
        /// <summary>
        /// Màu cạnh
        /// </summary>
        public Color Color;
        /// <summary>
        /// Kích thước cạnh
        /// </summary>
        public int Size;
        /// <summary>
        /// Khởi tạo từ 2 đỉnh
        /// </summary>
        /// <param name="_Vertex1">Đỉnh 1</param>
        /// <param name="_Vertex2">Đỉnh 2</param>
        public Edge(Vertex _Vertex1, Vertex _Vertex2)
        {
            Vertex1 = _Vertex1;
            Vertex2 = _Vertex2;
            UseDefaultValue();
        }
        /// <summary>
        /// Sử dụng các giá trị mặc định
        /// </summary>
        public void UseDefaultValue()
        {
            Font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Point);
            TextColor = Color.Green;
            BackgroundColor = Color.LightYellow;
            BorderColor = Color.Black;
            BorderSize = 1;
            Color = Color.Green;
            Size = 2;
        }
        public void Reversal()
        {
            Vertex1.ConnectionVertices.Remove(Vertex2);
            Vertex2.ConnectionVertices.Add(Vertex1);
            Vertex _Temp = Vertex1;
            Vertex1 = Vertex2;
            Vertex2 = _Temp;
        }
        /// <summary>
        /// Vẽ nó
        /// </summary>
        /// <param name="e">Nơi cần vẽ</param>
        /// <param name="isDirected">Có hướng không</param>
        public void DrawIt(Graphics e, bool isDirected)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.SmoothingMode = SmoothingMode.AntiAlias;
            e.TextRenderingHint = TextRenderingHint.AntiAlias;
            SizeF TextSize = e.MeasureString(Value.ToString(), Font);
            TextSize.Width += 4;
            TextSize.Height += 4;
            PointF PointOfString;
            if (Vertex2.Value == Vertex1.Value) // là khuyên, vẽ khuyên
            {
                DrawCurve(e, new Pen(Color, Size), Vertex1.MiddlePoint, Vertex1.Size, isDirected);
                PointOfString = new PointF(Vertex1.MiddlePoint.X + Vertex1.Size - (TextSize.Width) / 2, Vertex1.MiddlePoint.Y + Vertex1.Size - (TextSize.Height) / 2);
            }
            else // Vẽ đường.
            {
                DrawLine(e, new Pen(Color, Size), Vertex1.MiddlePoint, Vertex2.MiddlePoint, isDirected);
                PointOfString = new PointF((Vertex1.MiddlePoint.X + Vertex2.MiddlePoint.X - TextSize.Width) / 2, (Vertex1.MiddlePoint.Y + Vertex2.MiddlePoint.Y - TextSize.Height) / 2);
            }
            Rectangle = new Rectangle((Int32)PointOfString.X, (Int32)PointOfString.Y, (Int32)TextSize.Width, (Int32)TextSize.Height);
            e.FillEllipse(new SolidBrush(BackgroundColor), Rectangle);
            e.DrawEllipse(new Pen(BorderColor, BorderSize), Rectangle);
            e.DrawString(Value.ToString(), Font, new SolidBrush(TextColor), Rectangle, stringFormat);
        }
        /// <summary>
        /// Vẽ cạnh dưới dạng đường thẳng
        /// </summary>
        /// <param name="e">Nơi cần vẽ</param>
        /// <param name="aPen">Cọ vẽ</param>
        /// <param name="StartPoint">Điểm bắt đầu</param>
        /// <param name="EndPoint">Điểm kết thúc</param>
        /// <param name="isDirected">Có hướng không</param>
        private void DrawLine(Graphics e,Pen aPen, PointF StartPoint, PointF EndPoint, bool isDirected)
        {
            double DX = Math.Abs(StartPoint.X - EndPoint.X);
            double DY = Math.Abs(StartPoint.Y - EndPoint.Y);
            double L = Math.Sqrt(DX*DX + DY*DY);
            double DsX = DX / L * Vertex1.Size / 2;//Math.Cos(Math.Asin(DX / L)) * Vertex1.Size / 2;
            double DsY = DY / L * Vertex1.Size / 2;//Math.Sin(Math.Acos(DY / L)) * Vertex1.Size / 2;
            double X1, X2, Y1, Y2;
            if (StartPoint.X > EndPoint.X)
            {
                X1 = StartPoint.X - DsX;
                X2 = EndPoint.X + DsX;
            }
            else
            {
                X1 = StartPoint.X + DsX;
                X2 = EndPoint.X - DsX;
            }
            if (StartPoint.Y > EndPoint.Y)
            {
                Y1 = StartPoint.Y - DsY;
                Y2 = EndPoint.Y + DsY;
            }
            else
            {
                Y1 = StartPoint.Y + DsY;
                Y2 = EndPoint.Y - DsY;
            }

            if (isDirected)
            {
                AdjustableArrowCap Cap = new AdjustableArrowCap(Size * 3, Size * 3, true);
                //Cap.BaseCap = LineCap.;
                aPen.CustomEndCap = Cap; // vẽ mũi tên
            }
            e.DrawLine(aPen, (float)X1, (float)Y1, (float)X2, (float)Y2);            
        }
        /// <summary>
        /// Tính điểm trên đường tròn khi biết góc
        /// </summary>
        /// <param name="O">Toạ độ tâm</param>
        /// <param name="R">Bán kính</param>
        /// <param name="Angle">Góc (Đơn vị độ)</param>
        /// <returns>Trả về toạ độ tính được</returns>
        private PointF CalcPoint(PointF O, int R, int Angle)
        {
            double Dx = Math.Cos(Angle * Math.PI / 180F) * R;
            double Dy = Math.Sin(Angle * Math.PI / 180F) * R;
            double X = 0;
            double Y = 0;
            X = O.X + Dx;
            Y = O.Y - Dy;
            return new PointF((float)X, (float)Y);
        }
        /// <summary>
        /// Vẽ cạnh dưới dạng cung tròn
        /// </summary>
        /// <param name="e">Nơi cần vẽ</param>
        /// <param name="aPen">Cọ vẽ</param>
        /// <param name="MiddlePoint">Điểm chính giữa của đỉnh có cung tròn</param>
        /// <param name="VertexSize">Kích thước đỉnh</param>
        /// <param name="isDirected">Có hướng không</param>
        private void DrawCurve(Graphics e,Pen aPen, PointF MiddlePoint, int VertexSize, bool isDirected)
        {
            PointF[] PointArray = new PointF[271];
            for (int i = 90; i >= -180; i--) // tính các điểm trên cung tròn từ góc 90 tới góc -180, thuận chiều kim đồng hồ.
                PointArray[90 - i] = CalcPoint(new PointF(MiddlePoint.X + VertexSize / 2, MiddlePoint.Y + VertexSize / 2), VertexSize / 2, i);
            if (isDirected)
            {
                AdjustableArrowCap Cap = new AdjustableArrowCap(Size * 3, Size * 3, true);
                //Cap.BaseCap = LineCap.;
                aPen.CustomEndCap = Cap; // vẽ mũi tên
            }
            e.DrawLines(aPen, PointArray);
        }
    }
}
