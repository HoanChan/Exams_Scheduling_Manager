namespace Exams_Scheduling_Manager
{
    partial class frmMain
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
            this.btnDatabaseEditor = new System.Windows.Forms.Button();
            this.btnSheduling = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDatabaseEditor
            // 
            this.btnDatabaseEditor.Location = new System.Drawing.Point(19, 60);
            this.btnDatabaseEditor.Name = "btnDatabaseEditor";
            this.btnDatabaseEditor.Size = new System.Drawing.Size(114, 142);
            this.btnDatabaseEditor.TabIndex = 0;
            this.btnDatabaseEditor.Text = "CSDL";
            this.btnDatabaseEditor.UseVisualStyleBackColor = true;
            this.btnDatabaseEditor.Click += new System.EventHandler(this.btnDatabaseEditor_Click);
            // 
            // btnSheduling
            // 
            this.btnSheduling.Location = new System.Drawing.Point(151, 60);
            this.btnSheduling.Name = "btnSheduling";
            this.btnSheduling.Size = new System.Drawing.Size(114, 142);
            this.btnSheduling.TabIndex = 1;
            this.btnSheduling.Text = "Xếp lịch";
            this.btnSheduling.UseVisualStyleBackColor = true;
            this.btnSheduling.Click += new System.EventHandler(this.btnSheduling_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnSheduling);
            this.Controls.Add(this.btnDatabaseEditor);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDatabaseEditor;
        private System.Windows.Forms.Button btnSheduling;
    }
}