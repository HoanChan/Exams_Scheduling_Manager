using System;
using System.Windows.Forms;
using System.Drawing;
namespace Graphs_Viewer
{
    public partial class Form1 : Form
    {
        Graph aGraph;
        bool isShowGridLine;
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            aGraph = new Graph(this);
            isShowGridLine = false;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.OnLoad(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if(isShowGridLine)
            {
                int Size = 30;
                for (int i = 0; i <= this.ClientRectangle.Width / Size; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Gray, 1), i * Size, tabControl.Bottom, i * Size, this.ClientRectangle.Height);
                }
                for (int i = 0; i <= this.ClientRectangle.Height / Size; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Gray, 1), 0, i * Size, this.ClientRectangle.Width, i * Size);
                }
            }
            aGraph.OnPaint(e);
            //base.OnPaint(e);
        }
        #region Mouse Events
        protected override void OnMouseClick(MouseEventArgs e)
        {
            aGraph.OnMouseClick(e, contextMenuMain, contextMenuEdge, contextMenuVertex);
            base.OnMouseClick(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            aGraph.OnMouseDown(e);
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            aGraph.OnMouseUp(e);
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            aGraph.OnMouseMove(e);
            base.OnMouseMove(e);
        }
        #endregion
        #region Button events
        private void butAddVertex_Click(object sender, EventArgs e)
        {
            aGraph.AddVertex();
        }
        private void butAddEdge_Click(object sender, EventArgs e)
        {
            aGraph.AddEdge();
        }
        private void butChangeEdgeWeight_Click(object sender, EventArgs e)
        {
            aGraph.ChangeEdgeWeight();
        }
        private void butDeleteVertex_Click(object sender, EventArgs e)
        {
            aGraph.DeleteVertex();
        }
        private void butDeleteEdge_Click(object sender, EventArgs e)
        {
            aGraph.DeleteEdge();
        }
        private void butOpen_Click(object sender, EventArgs e)
        {
            aGraph.Open();
        }
        private void butSaveAdjacencyMatrix_Click(object sender, EventArgs e)
        {
            aGraph.SaveAdjacencyMatrix();
        }
        private void butSaveAdjacencyMatrixWithRectangle_Click(object sender, EventArgs e)
        {
            aGraph.SaveAdjacencyMatrixWithRectangle();
        }
        private void butBookmarkConnectedComponents_Click(object sender, EventArgs e)
        {
            aGraph.BookmarkConnectedComponents();
        }
        private void butDijsktra_Click(object sender, EventArgs e)
        {
            aGraph.Dijsktra();
        }
        private void butResetColor_Click(object sender, EventArgs e)
        {
            aGraph.ResetColor();
        }
        private void butDepthFirstSearch_Recursion_Click(object sender, EventArgs e)
        {
            aGraph.DepthFirstSearch_Recursion();
        }
        private void butDepthFirstSearch_Stack_Click(object sender, EventArgs e)
        {
            aGraph.DepthFirstSearch_Stack();
        }
        private void butBreadthFirstSearch_Recursion_Click(object sender, EventArgs e)
        {
            aGraph.BreadthFirstSearch_Recursion();
        }
        private void butBreadthFirstSearch_Queue_Click(object sender, EventArgs e)
        {
            aGraph.BreadthFirstSearch_Queue();
        }
        private void butNew_Click(object sender, EventArgs e)
        {
            aGraph.New();
        }
        private void butShowAdjacencyMatrix_Click(object sender, EventArgs e)
        {
            aGraph.ShowAdjacencyMatrix();
        }
        private void butShowIncidenceMatrix_Click(object sender, EventArgs e)
        {
            aGraph.ShowIncidenceMatrix();
        }
        private void butShowAdjacencyList_Click(object sender, EventArgs e)
        {
            aGraph.ShowAdjacencyList();
        }
        private void butShowIncidenceList_Click(object sender, EventArgs e)
        {
            aGraph.ShowIncidenceList();
        }
        private void butShowGridLine_Click(object sender, EventArgs e)
        {
            isShowGridLine = !isShowGridLine;
            if (isShowGridLine)
                butShowGridLine.Text = "Ẩn đường lưới";
            else
                butShowGridLine.Text = "Hiện đường lưới";
            this.Invalidate();
        }
        #endregion
        #region Menu Events
        private void DeleteEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aGraph.DeleteEdgeToolStripMenuItem();
        }
        private void ChangeEdgeWeightNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aGraph.ChangeEdgeWeightNumberToolStripMenuItem();
        }
        private void DeleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aGraph.DeleteVertexToolStripMenuItem();
        }
        private void MakeAConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aGraph.MakeAConnectionToolStripMenuItem();
        }
        private void AddAVertexHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aGraph.AddAVertexHereToolStripMenuItem();
        }
        private void ReversalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aGraph.ReversalToolStripMenuItem();
        }
        #endregion

        private void butColoring_Click(object sender, EventArgs e)
        {
            aGraph.Coloring();
        }


    }
}
