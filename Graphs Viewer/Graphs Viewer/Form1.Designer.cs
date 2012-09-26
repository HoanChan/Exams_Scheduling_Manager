namespace Graphs_Viewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.butShowAdjacencyMatrix = new System.Windows.Forms.Button();
            this.butShowIncidenceMatrix = new System.Windows.Forms.Button();
            this.butShowIncidenceList = new System.Windows.Forms.Button();
            this.butShowAdjacencyList = new System.Windows.Forms.Button();
            this.butNew = new System.Windows.Forms.Button();
            this.butOpen = new System.Windows.Forms.Button();
            this.butSaveAdjacencyMatrixWithRectangle = new System.Windows.Forms.Button();
            this.butSaveAdjacencyMatrix = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.butShowGridLine = new System.Windows.Forms.Button();
            this.butResetColor = new System.Windows.Forms.Button();
            this.butDeleteEdge = new System.Windows.Forms.Button();
            this.butDeleteVertex = new System.Windows.Forms.Button();
            this.butChangeEdgeWeight = new System.Windows.Forms.Button();
            this.butAddEdge = new System.Windows.Forms.Button();
            this.butAddVertex = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.butBreadthFirstSearch_Recursion = new System.Windows.Forms.Button();
            this.butDepthFirstSearch_Recursion = new System.Windows.Forms.Button();
            this.butBreadthFirstSearch_Queue = new System.Windows.Forms.Button();
            this.butDijsktra = new System.Windows.Forms.Button();
            this.butDepthFirstSearch_Stack = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuVertex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeAConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEdge = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeEdgeWeightNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReversalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddAVertexHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.butBookmarkConnectedComponents = new System.Windows.Forms.Button();
            this.butColoring = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.contextMenuVertex.SuspendLayout();
            this.contextMenuEdge.SuspendLayout();
            this.contextMenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1201, 111);
            this.tabControl.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.butShowAdjacencyMatrix);
            this.tabPage1.Controls.Add(this.butShowIncidenceMatrix);
            this.tabPage1.Controls.Add(this.butShowIncidenceList);
            this.tabPage1.Controls.Add(this.butShowAdjacencyList);
            this.tabPage1.Controls.Add(this.butNew);
            this.tabPage1.Controls.Add(this.butOpen);
            this.tabPage1.Controls.Add(this.butSaveAdjacencyMatrixWithRectangle);
            this.tabPage1.Controls.Add(this.butSaveAdjacencyMatrix);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1193, 85);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dữ liệu";
            // 
            // butShowAdjacencyMatrix
            // 
            this.butShowAdjacencyMatrix.Image = ((System.Drawing.Image)(resources.GetObject("butShowAdjacencyMatrix.Image")));
            this.butShowAdjacencyMatrix.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butShowAdjacencyMatrix.Location = new System.Drawing.Point(386, 0);
            this.butShowAdjacencyMatrix.Name = "butShowAdjacencyMatrix";
            this.butShowAdjacencyMatrix.Size = new System.Drawing.Size(89, 85);
            this.butShowAdjacencyMatrix.TabIndex = 5;
            this.butShowAdjacencyMatrix.Text = "Ma trận kề";
            this.butShowAdjacencyMatrix.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butShowAdjacencyMatrix.UseVisualStyleBackColor = true;
            this.butShowAdjacencyMatrix.Click += new System.EventHandler(this.butShowAdjacencyMatrix_Click);
            // 
            // butShowIncidenceMatrix
            // 
            this.butShowIncidenceMatrix.Image = ((System.Drawing.Image)(resources.GetObject("butShowIncidenceMatrix.Image")));
            this.butShowIncidenceMatrix.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butShowIncidenceMatrix.Location = new System.Drawing.Point(481, 0);
            this.butShowIncidenceMatrix.Name = "butShowIncidenceMatrix";
            this.butShowIncidenceMatrix.Size = new System.Drawing.Size(100, 85);
            this.butShowIncidenceMatrix.TabIndex = 6;
            this.butShowIncidenceMatrix.Text = "Ma trận liên thuộc";
            this.butShowIncidenceMatrix.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butShowIncidenceMatrix.UseVisualStyleBackColor = true;
            this.butShowIncidenceMatrix.Click += new System.EventHandler(this.butShowIncidenceMatrix_Click);
            // 
            // butShowIncidenceList
            // 
            this.butShowIncidenceList.Image = ((System.Drawing.Image)(resources.GetObject("butShowIncidenceList.Image")));
            this.butShowIncidenceList.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butShowIncidenceList.Location = new System.Drawing.Point(681, 0);
            this.butShowIncidenceList.Name = "butShowIncidenceList";
            this.butShowIncidenceList.Size = new System.Drawing.Size(98, 85);
            this.butShowIncidenceList.TabIndex = 8;
            this.butShowIncidenceList.Text = "Danh sách cạnh";
            this.butShowIncidenceList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butShowIncidenceList.UseVisualStyleBackColor = true;
            this.butShowIncidenceList.Click += new System.EventHandler(this.butShowIncidenceList_Click);
            // 
            // butShowAdjacencyList
            // 
            this.butShowAdjacencyList.Image = ((System.Drawing.Image)(resources.GetObject("butShowAdjacencyList.Image")));
            this.butShowAdjacencyList.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butShowAdjacencyList.Location = new System.Drawing.Point(587, 0);
            this.butShowAdjacencyList.Name = "butShowAdjacencyList";
            this.butShowAdjacencyList.Size = new System.Drawing.Size(88, 85);
            this.butShowAdjacencyList.TabIndex = 7;
            this.butShowAdjacencyList.Text = "Danh sách kề";
            this.butShowAdjacencyList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butShowAdjacencyList.UseVisualStyleBackColor = true;
            this.butShowAdjacencyList.Click += new System.EventHandler(this.butShowAdjacencyList_Click);
            // 
            // butNew
            // 
            this.butNew.Image = ((System.Drawing.Image)(resources.GetObject("butNew.Image")));
            this.butNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butNew.Location = new System.Drawing.Point(8, 0);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(89, 85);
            this.butNew.TabIndex = 1;
            this.butNew.Text = "Tạo mới";
            this.butNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butNew.UseVisualStyleBackColor = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // butOpen
            // 
            this.butOpen.Image = ((System.Drawing.Image)(resources.GetObject("butOpen.Image")));
            this.butOpen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butOpen.Location = new System.Drawing.Point(103, 0);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(89, 85);
            this.butOpen.TabIndex = 2;
            this.butOpen.Text = "Mở";
            this.butOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butOpen.UseVisualStyleBackColor = true;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // butSaveAdjacencyMatrixWithRectangle
            // 
            this.butSaveAdjacencyMatrixWithRectangle.Image = ((System.Drawing.Image)(resources.GetObject("butSaveAdjacencyMatrixWithRectangle.Image")));
            this.butSaveAdjacencyMatrixWithRectangle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butSaveAdjacencyMatrixWithRectangle.Location = new System.Drawing.Point(198, 0);
            this.butSaveAdjacencyMatrixWithRectangle.Name = "butSaveAdjacencyMatrixWithRectangle";
            this.butSaveAdjacencyMatrixWithRectangle.Size = new System.Drawing.Size(88, 85);
            this.butSaveAdjacencyMatrixWithRectangle.TabIndex = 3;
            this.butSaveAdjacencyMatrixWithRectangle.Text = "Lưu";
            this.butSaveAdjacencyMatrixWithRectangle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butSaveAdjacencyMatrixWithRectangle.UseVisualStyleBackColor = true;
            this.butSaveAdjacencyMatrixWithRectangle.Click += new System.EventHandler(this.butSaveAdjacencyMatrixWithRectangle_Click);
            // 
            // butSaveAdjacencyMatrix
            // 
            this.butSaveAdjacencyMatrix.Image = ((System.Drawing.Image)(resources.GetObject("butSaveAdjacencyMatrix.Image")));
            this.butSaveAdjacencyMatrix.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butSaveAdjacencyMatrix.Location = new System.Drawing.Point(292, 0);
            this.butSaveAdjacencyMatrix.Name = "butSaveAdjacencyMatrix";
            this.butSaveAdjacencyMatrix.Size = new System.Drawing.Size(88, 85);
            this.butSaveAdjacencyMatrix.TabIndex = 4;
            this.butSaveAdjacencyMatrix.Text = "Lưu ma trận kề";
            this.butSaveAdjacencyMatrix.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butSaveAdjacencyMatrix.UseVisualStyleBackColor = true;
            this.butSaveAdjacencyMatrix.Click += new System.EventHandler(this.butSaveAdjacencyMatrix_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.butShowGridLine);
            this.tabPage2.Controls.Add(this.butResetColor);
            this.tabPage2.Controls.Add(this.butDeleteEdge);
            this.tabPage2.Controls.Add(this.butDeleteVertex);
            this.tabPage2.Controls.Add(this.butChangeEdgeWeight);
            this.tabPage2.Controls.Add(this.butAddEdge);
            this.tabPage2.Controls.Add(this.butAddVertex);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1193, 85);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Thao tác với đồ thị";
            // 
            // butShowGridLine
            // 
            this.butShowGridLine.Image = ((System.Drawing.Image)(resources.GetObject("butShowGridLine.Image")));
            this.butShowGridLine.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butShowGridLine.Location = new System.Drawing.Point(562, 0);
            this.butShowGridLine.Name = "butShowGridLine";
            this.butShowGridLine.Size = new System.Drawing.Size(106, 85);
            this.butShowGridLine.TabIndex = 16;
            this.butShowGridLine.Text = "Hiện đường lưới";
            this.butShowGridLine.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butShowGridLine.UseVisualStyleBackColor = true;
            this.butShowGridLine.Click += new System.EventHandler(this.butShowGridLine_Click);
            // 
            // butResetColor
            // 
            this.butResetColor.Image = ((System.Drawing.Image)(resources.GetObject("butResetColor.Image")));
            this.butResetColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butResetColor.Location = new System.Drawing.Point(450, 0);
            this.butResetColor.Name = "butResetColor";
            this.butResetColor.Size = new System.Drawing.Size(106, 85);
            this.butResetColor.TabIndex = 15;
            this.butResetColor.Text = "Về màu mặc định";
            this.butResetColor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butResetColor.UseVisualStyleBackColor = true;
            this.butResetColor.Click += new System.EventHandler(this.butResetColor_Click);
            // 
            // butDeleteEdge
            // 
            this.butDeleteEdge.Image = ((System.Drawing.Image)(resources.GetObject("butDeleteEdge.Image")));
            this.butDeleteEdge.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butDeleteEdge.Location = new System.Drawing.Point(255, 0);
            this.butDeleteEdge.Name = "butDeleteEdge";
            this.butDeleteEdge.Size = new System.Drawing.Size(77, 85);
            this.butDeleteEdge.TabIndex = 13;
            this.butDeleteEdge.Text = "Xoá Cạnh";
            this.butDeleteEdge.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDeleteEdge.UseVisualStyleBackColor = true;
            this.butDeleteEdge.Click += new System.EventHandler(this.butDeleteEdge_Click);
            // 
            // butDeleteVertex
            // 
            this.butDeleteVertex.Image = ((System.Drawing.Image)(resources.GetObject("butDeleteVertex.Image")));
            this.butDeleteVertex.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butDeleteVertex.Location = new System.Drawing.Point(89, 0);
            this.butDeleteVertex.Name = "butDeleteVertex";
            this.butDeleteVertex.Size = new System.Drawing.Size(77, 85);
            this.butDeleteVertex.TabIndex = 11;
            this.butDeleteVertex.Text = "Xoá Đỉnh";
            this.butDeleteVertex.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDeleteVertex.UseVisualStyleBackColor = true;
            this.butDeleteVertex.Click += new System.EventHandler(this.butDeleteVertex_Click);
            // 
            // butChangeEdgeWeight
            // 
            this.butChangeEdgeWeight.Image = ((System.Drawing.Image)(resources.GetObject("butChangeEdgeWeight.Image")));
            this.butChangeEdgeWeight.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butChangeEdgeWeight.Location = new System.Drawing.Point(338, 0);
            this.butChangeEdgeWeight.Name = "butChangeEdgeWeight";
            this.butChangeEdgeWeight.Size = new System.Drawing.Size(106, 85);
            this.butChangeEdgeWeight.TabIndex = 14;
            this.butChangeEdgeWeight.Text = "Thay đổi trọng số";
            this.butChangeEdgeWeight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butChangeEdgeWeight.UseVisualStyleBackColor = true;
            this.butChangeEdgeWeight.Click += new System.EventHandler(this.butChangeEdgeWeight_Click);
            // 
            // butAddEdge
            // 
            this.butAddEdge.Image = ((System.Drawing.Image)(resources.GetObject("butAddEdge.Image")));
            this.butAddEdge.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butAddEdge.Location = new System.Drawing.Point(172, 0);
            this.butAddEdge.Name = "butAddEdge";
            this.butAddEdge.Size = new System.Drawing.Size(77, 85);
            this.butAddEdge.TabIndex = 12;
            this.butAddEdge.Text = "Thêm Cạnh";
            this.butAddEdge.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butAddEdge.UseVisualStyleBackColor = true;
            this.butAddEdge.Click += new System.EventHandler(this.butAddEdge_Click);
            // 
            // butAddVertex
            // 
            this.butAddVertex.Image = ((System.Drawing.Image)(resources.GetObject("butAddVertex.Image")));
            this.butAddVertex.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butAddVertex.Location = new System.Drawing.Point(6, 0);
            this.butAddVertex.Name = "butAddVertex";
            this.butAddVertex.Size = new System.Drawing.Size(77, 85);
            this.butAddVertex.TabIndex = 10;
            this.butAddVertex.Text = "Thêm Đỉnh";
            this.butAddVertex.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butAddVertex.UseVisualStyleBackColor = true;
            this.butAddVertex.Click += new System.EventHandler(this.butAddVertex_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.butColoring);
            this.tabPage3.Controls.Add(this.butBreadthFirstSearch_Recursion);
            this.tabPage3.Controls.Add(this.butDepthFirstSearch_Recursion);
            this.tabPage3.Controls.Add(this.butBreadthFirstSearch_Queue);
            this.tabPage3.Controls.Add(this.butDijsktra);
            this.tabPage3.Controls.Add(this.butBookmarkConnectedComponents);
            this.tabPage3.Controls.Add(this.butDepthFirstSearch_Stack);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1193, 85);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Giải thuật";
            // 
            // butBreadthFirstSearch_Recursion
            // 
            this.butBreadthFirstSearch_Recursion.Image = ((System.Drawing.Image)(resources.GetObject("butBreadthFirstSearch_Recursion.Image")));
            this.butBreadthFirstSearch_Recursion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butBreadthFirstSearch_Recursion.Location = new System.Drawing.Point(617, 0);
            this.butBreadthFirstSearch_Recursion.Name = "butBreadthFirstSearch_Recursion";
            this.butBreadthFirstSearch_Recursion.Size = new System.Drawing.Size(214, 85);
            this.butBreadthFirstSearch_Recursion.TabIndex = 20;
            this.butBreadthFirstSearch_Recursion.Text = "Duyệt đồ thị theo chiều rộng dùng đệ quy";
            this.butBreadthFirstSearch_Recursion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butBreadthFirstSearch_Recursion.UseVisualStyleBackColor = true;
            this.butBreadthFirstSearch_Recursion.Click += new System.EventHandler(this.butBreadthFirstSearch_Recursion_Click);
            // 
            // butDepthFirstSearch_Recursion
            // 
            this.butDepthFirstSearch_Recursion.Image = ((System.Drawing.Image)(resources.GetObject("butDepthFirstSearch_Recursion.Image")));
            this.butDepthFirstSearch_Recursion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butDepthFirstSearch_Recursion.Location = new System.Drawing.Point(189, 0);
            this.butDepthFirstSearch_Recursion.Name = "butDepthFirstSearch_Recursion";
            this.butDepthFirstSearch_Recursion.Size = new System.Drawing.Size(212, 85);
            this.butDepthFirstSearch_Recursion.TabIndex = 18;
            this.butDepthFirstSearch_Recursion.Text = "Duyệt đồ thị theo chiều sâu dùng đệ quy";
            this.butDepthFirstSearch_Recursion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDepthFirstSearch_Recursion.UseVisualStyleBackColor = true;
            this.butDepthFirstSearch_Recursion.Click += new System.EventHandler(this.butDepthFirstSearch_Recursion_Click);
            // 
            // butBreadthFirstSearch_Queue
            // 
            this.butBreadthFirstSearch_Queue.Image = ((System.Drawing.Image)(resources.GetObject("butBreadthFirstSearch_Queue.Image")));
            this.butBreadthFirstSearch_Queue.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butBreadthFirstSearch_Queue.Location = new System.Drawing.Point(837, 0);
            this.butBreadthFirstSearch_Queue.Name = "butBreadthFirstSearch_Queue";
            this.butBreadthFirstSearch_Queue.Size = new System.Drawing.Size(213, 85);
            this.butBreadthFirstSearch_Queue.TabIndex = 38;
            this.butBreadthFirstSearch_Queue.Text = "Duyệt đồ thị theo chiều rộng dùng Queue";
            this.butBreadthFirstSearch_Queue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butBreadthFirstSearch_Queue.UseVisualStyleBackColor = true;
            this.butBreadthFirstSearch_Queue.Click += new System.EventHandler(this.butBreadthFirstSearch_Queue_Click);
            // 
            // butDijsktra
            // 
            this.butDijsktra.Image = ((System.Drawing.Image)(resources.GetObject("butDijsktra.Image")));
            this.butDijsktra.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butDijsktra.Location = new System.Drawing.Point(106, 0);
            this.butDijsktra.Name = "butDijsktra";
            this.butDijsktra.Size = new System.Drawing.Size(77, 85);
            this.butDijsktra.TabIndex = 17;
            this.butDijsktra.Text = "Dijsktra";
            this.butDijsktra.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDijsktra.UseVisualStyleBackColor = true;
            this.butDijsktra.Click += new System.EventHandler(this.butDijsktra_Click);
            // 
            // butDepthFirstSearch_Stack
            // 
            this.butDepthFirstSearch_Stack.Image = ((System.Drawing.Image)(resources.GetObject("butDepthFirstSearch_Stack.Image")));
            this.butDepthFirstSearch_Stack.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butDepthFirstSearch_Stack.Location = new System.Drawing.Point(407, 0);
            this.butDepthFirstSearch_Stack.Name = "butDepthFirstSearch_Stack";
            this.butDepthFirstSearch_Stack.Size = new System.Drawing.Size(204, 85);
            this.butDepthFirstSearch_Stack.TabIndex = 19;
            this.butDepthFirstSearch_Stack.Text = "Duyệt đồ thị theo chiều sâu dùng Stack";
            this.butDepthFirstSearch_Stack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDepthFirstSearch_Stack.UseVisualStyleBackColor = true;
            this.butDepthFirstSearch_Stack.Click += new System.EventHandler(this.butDepthFirstSearch_Stack_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1193, 85);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Thông tin về chương trình";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(675, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 52);
            this.label5.TabIndex = 4;
            this.label5.Text = "Hồ Dư Trọng Lễ - 099100??\r\n      - ??.\r\n      - ??.\r\n      - ??.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(517, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 52);
            this.label4.TabIndex = 3;
            this.label4.Text = "Trần Thị Thu Chín - 099100??\r\n      - Thiết kế giao diện.\r\n      - Test chương tr" +
                "ình.\r\n      - Thiết kế sự kiện.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(353, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 65);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nguyễn Hoàng Duy - 099100??\r\n      - Giải thuật.\r\n      - Nhập xuất.\r\n      - Biể" +
                "u diễn đồ thị.\r\n        (ma trận /  danh sách)\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(214, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 78);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lê Hoàn Chân - 09910015\r\n      - Thiết kế class, obj.\r\n      - Đồ hoạ.\r\n         " +
                "   + Icon.\r\n            + GDI.\r\n      - Xử lý sự kiện.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên chương trình: Graph Viewer.\r\nPhiên bản: 3.0.\r\nChức năng: Biểu diễn đồ thị vô " +
                "hướng \r\n    và có hướng dưới dạng đồ hoạ, minh\r\n    các giải thuật trên đồ thị.";
            // 
            // contextMenuVertex
            // 
            this.contextMenuVertex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteVertexToolStripMenuItem,
            this.MakeAConnectionToolStripMenuItem});
            this.contextMenuVertex.Name = "contextMenuVertex";
            this.contextMenuVertex.Size = new System.Drawing.Size(174, 48);
            // 
            // DeleteVertexToolStripMenuItem
            // 
            this.DeleteVertexToolStripMenuItem.Name = "DeleteVertexToolStripMenuItem";
            this.DeleteVertexToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.DeleteVertexToolStripMenuItem.Text = "Xoá đỉnh này";
            this.DeleteVertexToolStripMenuItem.Click += new System.EventHandler(this.DeleteVertexToolStripMenuItem_Click);
            // 
            // MakeAConnectionToolStripMenuItem
            // 
            this.MakeAConnectionToolStripMenuItem.Name = "MakeAConnectionToolStripMenuItem";
            this.MakeAConnectionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.MakeAConnectionToolStripMenuItem.Text = "Nối đỉnh này với ...";
            this.MakeAConnectionToolStripMenuItem.Click += new System.EventHandler(this.MakeAConnectionToolStripMenuItem_Click);
            // 
            // contextMenuEdge
            // 
            this.contextMenuEdge.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteEdgeToolStripMenuItem,
            this.ChangeEdgeWeightNumberToolStripMenuItem,
            this.ReversalToolStripMenuItem});
            this.contextMenuEdge.Name = "contextMenuEdge";
            this.contextMenuEdge.Size = new System.Drawing.Size(140, 70);
            // 
            // DeleteEdgeToolStripMenuItem
            // 
            this.DeleteEdgeToolStripMenuItem.Name = "DeleteEdgeToolStripMenuItem";
            this.DeleteEdgeToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.DeleteEdgeToolStripMenuItem.Text = "Xoá cạnh";
            this.DeleteEdgeToolStripMenuItem.Click += new System.EventHandler(this.DeleteEdgeToolStripMenuItem_Click);
            // 
            // ChangeEdgeWeightNumberToolStripMenuItem
            // 
            this.ChangeEdgeWeightNumberToolStripMenuItem.Name = "ChangeEdgeWeightNumberToolStripMenuItem";
            this.ChangeEdgeWeightNumberToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.ChangeEdgeWeightNumberToolStripMenuItem.Text = "Đổi trọng số";
            this.ChangeEdgeWeightNumberToolStripMenuItem.Click += new System.EventHandler(this.ChangeEdgeWeightNumberToolStripMenuItem_Click);
            // 
            // ReversalToolStripMenuItem
            // 
            this.ReversalToolStripMenuItem.Name = "ReversalToolStripMenuItem";
            this.ReversalToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.ReversalToolStripMenuItem.Text = "Đảo chiều";
            this.ReversalToolStripMenuItem.Click += new System.EventHandler(this.ReversalToolStripMenuItem_Click);
            // 
            // contextMenuMain
            // 
            this.contextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAVertexHereToolStripMenuItem});
            this.contextMenuMain.Name = "contextMenuMain";
            this.contextMenuMain.Size = new System.Drawing.Size(196, 26);
            // 
            // AddAVertexHereToolStripMenuItem
            // 
            this.AddAVertexHereToolStripMenuItem.Name = "AddAVertexHereToolStripMenuItem";
            this.AddAVertexHereToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.AddAVertexHereToolStripMenuItem.Text = "Thêm một đỉnh tại đây";
            this.AddAVertexHereToolStripMenuItem.Click += new System.EventHandler(this.AddAVertexHereToolStripMenuItem_Click);
            // 
            // butBookmarkConnectedComponents
            // 
            this.butBookmarkConnectedComponents.Image = ((System.Drawing.Image)(resources.GetObject("butBookmarkConnectedComponents.Image")));
            this.butBookmarkConnectedComponents.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butBookmarkConnectedComponents.Location = new System.Drawing.Point(8, 0);
            this.butBookmarkConnectedComponents.Name = "butBookmarkConnectedComponents";
            this.butBookmarkConnectedComponents.Size = new System.Drawing.Size(92, 85);
            this.butBookmarkConnectedComponents.TabIndex = 16;
            this.butBookmarkConnectedComponents.Text = "Tính liên thông";
            this.butBookmarkConnectedComponents.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butBookmarkConnectedComponents.UseVisualStyleBackColor = true;
            this.butBookmarkConnectedComponents.Click += new System.EventHandler(this.butBookmarkConnectedComponents_Click);
            // 
            // butColoring
            // 
            this.butColoring.Image = ((System.Drawing.Image)(resources.GetObject("butColoring.Image")));
            this.butColoring.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butColoring.Location = new System.Drawing.Point(1056, 0);
            this.butColoring.Name = "butColoring";
            this.butColoring.Size = new System.Drawing.Size(92, 85);
            this.butColoring.TabIndex = 39;
            this.butColoring.Text = "Tô màu";
            this.butColoring.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butColoring.UseVisualStyleBackColor = true;
            this.butColoring.Click += new System.EventHandler(this.butColoring_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1201, 381);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "LÝ THUYẾT ĐỒ THỊ - GRAPH VIEWER V3.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.contextMenuVertex.ResumeLayout(false);
            this.contextMenuEdge.ResumeLayout(false);
            this.contextMenuMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button butSaveAdjacencyMatrix;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button butResetColor;
        private System.Windows.Forms.Button butDeleteEdge;
        private System.Windows.Forms.Button butDeleteVertex;
        private System.Windows.Forms.Button butChangeEdgeWeight;
        private System.Windows.Forms.Button butAddEdge;
        private System.Windows.Forms.Button butAddVertex;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button butBreadthFirstSearch_Recursion;
        private System.Windows.Forms.Button butDepthFirstSearch_Recursion;
        private System.Windows.Forms.Button butBreadthFirstSearch_Queue;
        private System.Windows.Forms.Button butDijsktra;
        private System.Windows.Forms.Button butDepthFirstSearch_Stack;
        private System.Windows.Forms.Button butOpen;
        private System.Windows.Forms.Button butSaveAdjacencyMatrixWithRectangle;
        private System.Windows.Forms.Button butNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuVertex;
        private System.Windows.Forms.ToolStripMenuItem DeleteVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuEdge;
        private System.Windows.Forms.ToolStripMenuItem DeleteEdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeEdgeWeightNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeAConnectionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuMain;
        private System.Windows.Forms.ToolStripMenuItem AddAVertexHereToolStripMenuItem;
        private System.Windows.Forms.Button butShowAdjacencyMatrix;
        private System.Windows.Forms.Button butShowIncidenceMatrix;
        private System.Windows.Forms.Button butShowIncidenceList;
        private System.Windows.Forms.Button butShowAdjacencyList;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem ReversalToolStripMenuItem;
        private System.Windows.Forms.Button butShowGridLine;
        private System.Windows.Forms.Button butColoring;
        private System.Windows.Forms.Button butBookmarkConnectedComponents;

    }
}

