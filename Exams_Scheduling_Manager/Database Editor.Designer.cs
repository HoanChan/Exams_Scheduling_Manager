namespace Exams_Scheduling_Manager
{
    partial class frmDatabaseEditor
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
            this.tabTable = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucStudents2 = new Exams_Scheduling_Manager.ucStudents();
            this.ucCourseRegistration1 = new Exams_Scheduling_Manager.ucCourseRegistration();
            this.ucRoom1 = new Exams_Scheduling_Manager.ucRoom();
            this.ucSubjects2 = new Exams_Scheduling_Manager.ucSubjects();
            this.tabTable.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.tabPage1);
            this.tabTable.Controls.Add(this.tabPage5);
            this.tabTable.Controls.Add(this.tabPage2);
            this.tabTable.Controls.Add(this.tabPage3);
            this.tabTable.Controls.Add(this.tabPage4);
            this.tabTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTable.Location = new System.Drawing.Point(0, 0);
            this.tabTable.Name = "tabTable";
            this.tabTable.SelectedIndex = 0;
            this.tabTable.Size = new System.Drawing.Size(988, 504);
            this.tabTable.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucStudents2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(980, 478);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sinh viên";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ucCourseRegistration1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(980, 478);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Đăng ký môn học";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(980, 478);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Giáo viên";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucRoom1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(980, 478);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Phòng học";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucSubjects2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(980, 478);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Môn học";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ucStudents2
            // 
            this.ucStudents2.BackColor = System.Drawing.Color.DimGray;
            this.ucStudents2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStudents2.Location = new System.Drawing.Point(3, 3);
            this.ucStudents2.Name = "ucStudents2";
            this.ucStudents2.Size = new System.Drawing.Size(974, 472);
            this.ucStudents2.TabIndex = 0;
            // 
            // ucCourseRegistration1
            // 
            this.ucCourseRegistration1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCourseRegistration1.Location = new System.Drawing.Point(3, 3);
            this.ucCourseRegistration1.Name = "ucCourseRegistration1";
            this.ucCourseRegistration1.Size = new System.Drawing.Size(974, 472);
            this.ucCourseRegistration1.TabIndex = 0;
            // 
            // ucRoom1
            // 
            this.ucRoom1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRoom1.Location = new System.Drawing.Point(3, 3);
            this.ucRoom1.Name = "ucRoom1";
            this.ucRoom1.Size = new System.Drawing.Size(974, 472);
            this.ucRoom1.TabIndex = 0;
            // 
            // ucSubjects2
            // 
            this.ucSubjects2.BackColor = System.Drawing.Color.Transparent;
            this.ucSubjects2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSubjects2.Location = new System.Drawing.Point(3, 3);
            this.ucSubjects2.Name = "ucSubjects2";
            this.ucSubjects2.Size = new System.Drawing.Size(974, 472);
            this.ucSubjects2.TabIndex = 0;
            // 
            // frmDatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 504);
            this.Controls.Add(this.tabTable);
            this.Name = "frmDatabaseEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chỉnh sửa cơ sở dữ liệu";
            this.tabTable.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabTable;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
      //  private ucSubjects ucSubjects1;
        private System.Windows.Forms.TabPage tabPage4;
        private ucSubjects ucSubjects2;
        private ucStudents ucStudents2;
        private System.Windows.Forms.TabPage tabPage5;
        private ucRoom ucRoom1;
        private ucCourseRegistration ucCourseRegistration1;
      //  private ucCourseRegistration ucCourseRegistration1;
    }
}

