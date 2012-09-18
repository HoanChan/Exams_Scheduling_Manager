namespace Exams_Scheduling_Manager
{
    partial class ucCourseRegistration
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCommands = new System.Windows.Forms.Panel();
            this.lbl1 = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.pnlCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCommands
            // 
            this.pnlCommands.Controls.Add(this.txtStudentID);
            this.pnlCommands.Controls.Add(this.lbl1);
            this.pnlCommands.Controls.Add(this.btnShow);
            this.pnlCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCommands.Location = new System.Drawing.Point(0, 392);
            this.pnlCommands.Name = "pnlCommands";
            this.pnlCommands.Size = new System.Drawing.Size(808, 69);
            this.pnlCommands.TabIndex = 3;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(19, 28);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(81, 13);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "Mã số sinh viên";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(278, 10);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 48);
            this.btnShow.TabIndex = 0;
            this.btnShow.Text = "Hiển thị";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(808, 391);
            this.dataGridView.TabIndex = 2;
            // 
            // txtStudentID
            // 
            this.txtStudentID.Location = new System.Drawing.Point(106, 25);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(155, 20);
            this.txtStudentID.TabIndex = 8;
            // 
            // ucCourseRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCommands);
            this.Controls.Add(this.dataGridView);
            this.Name = "ucCourseRegistration";
            this.Size = new System.Drawing.Size(808, 461);
            this.pnlCommands.ResumeLayout(false);
            this.pnlCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCommands;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}
