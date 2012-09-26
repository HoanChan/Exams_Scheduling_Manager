using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
namespace Graphs_Viewer
{
    public class Vertex
    {        
        /// <summary>
        /// Toạ độ + kích thước đỉnh
        /// </summary>
        public Rectangle Rectangle;
        /// <summary>
        /// Giá trị
        /// </summary>
        public int Value;
        /// <summary>
        /// Danh sách các cạnh có liên kết tới
        /// </summary>
        public List<Vertex> ConnectionVertices = new List<Vertex>();
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
        /// Kích thước viền
        /// </summary>
        public int BorderSize;
        /// <summary>
        /// Kích thước
        /// </summary>
        public int Size;
        /// <summary>
        /// Trạng thái
        /// </summary>
        public VertexStatus Status;
        /// <summary>
        /// Thông tin
        /// </summary>
        public string Infor;
        /// <summary>
        /// Kiểu chữ của thông tin
        /// </summary>
        public Font InforFont;
        /// <summary>
        /// Khởi tạo có giá trị
        /// </summary>
        /// <param name="_Value">Giá trị</param>
        public Vertex(int _Value)
        {
            UseDefaultValue();
            Rectangle = new Rectangle(0, 0, Size, Size);
            Value = _Value;
        }
        /// <summary>
        /// Khởi tạo với giá trị và vị trí
        /// </summary>
        /// <param name="_Location">Vị trí</param>
        /// <param name="_Value">Giá trị</param>
        public Vertex(Point _Location, int _Value)
        {
            UseDefaultValue();
            Rectangle = new Rectangle(_Location.X, _Location.Y, Size, Size);
            Value = _Value;
        }
        /// <summary>
        /// Khởi tạo với giá trị, vị trí và kích thước
        /// </summary>
        /// <param name="_Rectangle">Vị trí và kích thước</param>
        /// <param name="_Value">giá trị</param>
        public Vertex(Rectangle _Rectangle, int _Value)
        {
            UseDefaultValue();
            Rectangle = _Rectangle;
            Value = _Value;
        }
        /// <summary>
        /// Điểm trung tâm
        /// </summary>
        public PointF MiddlePoint
        {
            get { return new PointF(Rectangle.Left + Rectangle.Width/2, Rectangle.Top + Rectangle.Height / 2); }
        }

        /// <summary>
        /// Cho đỉnh nhận toạ độ mặc định. 
        /// Là vị trí trên N đường tròn đồng tâm, nằm giữa khung vẽ.
        /// </summary>
        /// <param name="LocationIndex">Thứ tự của đỉnh</param>
        /// <param name="ContainerWidth">Độ rộng khung vẽ</param>
        /// <param name="ContainerHeight">Độ cao khung vẽ</param>
        public void UseDefaultRectangle(int LocationIndex, int ContainerWidth, int ContainerHeight)
        {
            const int VertexPerGroup = 5;
            int CircleSize = 12 * (Int32)(LocationIndex / VertexPerGroup + 1) * VertexPerGroup;
            double BonusAngle = 0.6 / VertexPerGroup * Math.PI * (Int32)(LocationIndex / VertexPerGroup);
            double Angle = (double)LocationIndex / VertexPerGroup * 2 * Math.PI + BonusAngle;
            int X = (int)(ContainerWidth / 2 + CircleSize * Math.Cos(Angle));
            int Y = (int)(ContainerHeight / 2 + CircleSize * Math.Sin(Angle));
            Rectangle = new Rectangle(X, Y, Size, Size);
        }
        /// <summary>
        /// Sử dụng các giá trị mặc định
        /// </summary>
        public void UseDefaultValue()
        {
            Font = new Font(
                  new FontFamily("Arial"),
                  14, FontStyle.Bold,
                  GraphicsUnit.Point);
            TextColor = Color.Red;
            BackgroundColor = Color.LightYellow;
            BorderColor = Color.Blue;
            BorderSize = 2;
            Size = 30;
            Status = VertexStatus.sFree;
            Infor = "";
            InforFont = new Font(
                  new FontFamily("Arial"),
                  12, FontStyle.Regular,
                  GraphicsUnit.Point);
        }
        /// <summary>
        /// Vẽ nó
        /// </summary>
        /// <param name="e">Nơi cần vẽ</param>
        public void DrawIt(Graphics e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.SmoothingMode = SmoothingMode.AntiAlias;
            e.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.FillEllipse(new SolidBrush(BackgroundColor), Rectangle);
            e.DrawEllipse(new Pen(BorderColor, BorderSize), Rectangle);
            e.DrawString(Value.ToString(), Font, new SolidBrush(TextColor), (RectangleF)(Rectangle), stringFormat);
            if (Infor.Length>0)
            {
                SizeF TextSize = e.MeasureString(Infor.ToString(), InforFont);
                Rectangle DistanceRectangle = new Rectangle(Rectangle.X - ((Int32)TextSize.Width + 10 - Rectangle.Width)/2, Rectangle.Y - 15, (Int32)TextSize.Width + 10, (Int32)TextSize.Height);
                e.FillRectangle(new SolidBrush(BackgroundColor), DistanceRectangle);
                e.DrawRectangle(new Pen(BorderColor, BorderSize-1), DistanceRectangle);
                e.DrawString(Infor.ToString(), InforFont, new SolidBrush(TextColor), (RectangleF)(DistanceRectangle), stringFormat);
            }            
        }
        /// <summary>
        /// Xoá nó
        /// </summary>
        public void Dispose()
        {
            ConnectionVertices.Clear();
        }
    }
    
}
