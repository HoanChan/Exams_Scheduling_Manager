namespace Exams_Scheduling_Manager
{
    partial class ucSubjects
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel = new System.Windows.Forms.Panel();
            this.lblFaculty = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.cboFaculty = new System.Windows.Forms.ComboBox();
            this.cboSubject = new System.Windows.Forms.ComboBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chbIsExperimentalSubject = new System.Windows.Forms.CheckBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cboManagerSubject = new System.Windows.Forms.ComboBox();
            this.cboSchedulingBy = new System.Windows.Forms.ComboBox();
            this.nudTheoryCredit = new System.Windows.Forms.NumericUpDown();
            this.nudPracticesCredit = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTheoryCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPracticesCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView.Size = new System.Drawing.Size(799, 391);
            this.dataGridView.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnModify);
            this.panel.Controls.Add(this.btnDelete);
            this.panel.Controls.Add(this.btnAdd);
            this.panel.Controls.Add(this.lblFaculty);
            this.panel.Controls.Add(this.lblSubject);
            this.panel.Controls.Add(this.cboFaculty);
            this.panel.Controls.Add(this.cboSubject);
            this.panel.Controls.Add(this.btnShow);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 397);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(799, 69);
            this.panel.TabIndex = 1;
            // 
            // lblFaculty
            // 
            this.lblFaculty.AutoSize = true;
            this.lblFaculty.Location = new System.Drawing.Point(20, 40);
            this.lblFaculty.Name = "lblFaculty";
            this.lblFaculty.Size = new System.Drawing.Size(35, 13);
            this.lblFaculty.TabIndex = 4;
            this.lblFaculty.Text = "Khoa:";
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(20, 13);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 3;
            this.lblSubject.Text = "Bộ môn:";
            // 
            // cboFaculty
            // 
            this.cboFaculty.Enabled = false;
            this.cboFaculty.FormattingEnabled = true;
            this.cboFaculty.Location = new System.Drawing.Point(72, 37);
            this.cboFaculty.Name = "cboFaculty";
            this.cboFaculty.Size = new System.Drawing.Size(189, 21);
            this.cboFaculty.TabIndex = 2;
            // 
            // cboSubject
            // 
            this.cboSubject.FormattingEnabled = true;
            this.cboSubject.Location = new System.Drawing.Point(72, 10);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(189, 21);
            this.cboSubject.TabIndex = 1;
            this.cboSubject.SelectedIndexChanged += new System.EventHandler(this.cboSubject_SelectedIndexChanged);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(267, 10);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 48);
            this.btnShow.TabIndex = 0;
            this.btnShow.Text = "Hiển thị";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(423, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(116, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(545, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(116, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(667, 23);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(116, 23);
            this.btnModify.TabIndex = 7;
            this.btnModify.Text = "Sửa đổi";
            this.btnModify.UseVisualStyleBackColor = true;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.btnCancel);
            this.pnlEdit.Controls.Add(this.btnOK);
            this.pnlEdit.Controls.Add(this.nudPracticesCredit);
            this.pnlEdit.Controls.Add(this.nudTheoryCredit);
            this.pnlEdit.Controls.Add(this.cboSchedulingBy);
            this.pnlEdit.Controls.Add(this.cboManagerSubject);
            this.pnlEdit.Controls.Add(this.txtInfo);
            this.pnlEdit.Controls.Add(this.txtName);
            this.pnlEdit.Controls.Add(this.txtID);
            this.pnlEdit.Controls.Add(this.chbIsExperimentalSubject);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Controls.Add(this.label1);
            this.pnlEdit.Location = new System.Drawing.Point(201, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(397, 263);
            this.pnlEdit.TabIndex = 2;
            this.pnlEdit.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên Môn học:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã môn học:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số chỉ lý thuyết:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Số chỉ thực hành:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Xếp lịch bởi:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Bộ môn quản lý:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Ghi chú:";
            // 
            // chbIsExperimentalSubject
            // 
            this.chbIsExperimentalSubject.AutoSize = true;
            this.chbIsExperimentalSubject.Location = new System.Drawing.Point(116, 123);
            this.chbIsExperimentalSubject.Name = "chbIsExperimentalSubject";
            this.chbIsExperimentalSubject.Size = new System.Drawing.Size(114, 17);
            this.chbIsExperimentalSubject.TabIndex = 7;
            this.chbIsExperimentalSubject.Text = "Là môn thí nghiệm";
            this.chbIsExperimentalSubject.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(116, 9);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(255, 20);
            this.txtID.TabIndex = 8;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(116, 34);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 20);
            this.txtName.TabIndex = 9;
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(116, 204);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(255, 20);
            this.txtInfo.TabIndex = 10;
            // 
            // cboManagerSubject
            // 
            this.cboManagerSubject.FormattingEnabled = true;
            this.cboManagerSubject.Location = new System.Drawing.Point(116, 175);
            this.cboManagerSubject.Name = "cboManagerSubject";
            this.cboManagerSubject.Size = new System.Drawing.Size(255, 21);
            this.cboManagerSubject.TabIndex = 11;
            // 
            // cboSchedulingBy
            // 
            this.cboSchedulingBy.FormattingEnabled = true;
            this.cboSchedulingBy.Location = new System.Drawing.Point(116, 146);
            this.cboSchedulingBy.Name = "cboSchedulingBy";
            this.cboSchedulingBy.Size = new System.Drawing.Size(255, 21);
            this.cboSchedulingBy.TabIndex = 12;
            // 
            // nudTheoryCredit
            // 
            this.nudTheoryCredit.Location = new System.Drawing.Point(116, 68);
            this.nudTheoryCredit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTheoryCredit.Name = "nudTheoryCredit";
            this.nudTheoryCredit.Size = new System.Drawing.Size(142, 20);
            this.nudTheoryCredit.TabIndex = 13;
            // 
            // nudPracticesCredit
            // 
            this.nudPracticesCredit.Location = new System.Drawing.Point(116, 97);
            this.nudPracticesCredit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPracticesCredit.Name = "nudPracticesCredit";
            this.nudPracticesCredit.Size = new System.Drawing.Size(142, 20);
            this.nudPracticesCredit.TabIndex = 14;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(61, 230);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(134, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Thực hiện";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(201, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(134, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            // 
            // ucSubjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.dataGridView);
            this.Name = "ucSubjects";
            this.Size = new System.Drawing.Size(799, 466);
            this.Load += new System.EventHandler(this.ucSubjects_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTheoryCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPracticesCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label lblFaculty;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.ComboBox cboFaculty;
        private System.Windows.Forms.ComboBox cboSubject;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chbIsExperimentalSubject;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown nudPracticesCredit;
        private System.Windows.Forms.NumericUpDown nudTheoryCredit;
        private System.Windows.Forms.ComboBox cboSchedulingBy;
        private System.Windows.Forms.ComboBox cboManagerSubject;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;


    }
}
