﻿namespace Exams_Scheduling_Manager
{
    partial class frmSheduling
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
            this.tabStep = new System.Windows.Forms.TabControl();
            this.tabPageStep1 = new System.Windows.Forms.TabPage();
            this.tabPageStep2 = new System.Windows.Forms.TabPage();
            this.tabPageStep3 = new System.Windows.Forms.TabPage();
            this.tabPageStep4 = new System.Windows.Forms.TabPage();
            this.tabStep.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabStep
            // 
            this.tabStep.Controls.Add(this.tabPageStep1);
            this.tabStep.Controls.Add(this.tabPageStep2);
            this.tabStep.Controls.Add(this.tabPageStep3);
            this.tabStep.Controls.Add(this.tabPageStep4);
            this.tabStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStep.Location = new System.Drawing.Point(0, 0);
            this.tabStep.Name = "tabStep";
            this.tabStep.SelectedIndex = 0;
            this.tabStep.Size = new System.Drawing.Size(672, 496);
            this.tabStep.TabIndex = 0;
            // 
            // tabPageStep1
            // 
            this.tabPageStep1.Location = new System.Drawing.Point(4, 22);
            this.tabPageStep1.Name = "tabPageStep1";
            this.tabPageStep1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStep1.Size = new System.Drawing.Size(664, 470);
            this.tabPageStep1.TabIndex = 0;
            this.tabPageStep1.Text = "Bước 1 >";
            this.tabPageStep1.UseVisualStyleBackColor = true;
            // 
            // tabPageStep2
            // 
            this.tabPageStep2.Location = new System.Drawing.Point(4, 22);
            this.tabPageStep2.Name = "tabPageStep2";
            this.tabPageStep2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStep2.Size = new System.Drawing.Size(664, 470);
            this.tabPageStep2.TabIndex = 1;
            this.tabPageStep2.Text = "Bước 2 >";
            this.tabPageStep2.UseVisualStyleBackColor = true;
            // 
            // tabPageStep3
            // 
            this.tabPageStep3.Location = new System.Drawing.Point(4, 22);
            this.tabPageStep3.Name = "tabPageStep3";
            this.tabPageStep3.Size = new System.Drawing.Size(664, 470);
            this.tabPageStep3.TabIndex = 2;
            this.tabPageStep3.Text = "Bước 3 >";
            this.tabPageStep3.UseVisualStyleBackColor = true;
            // 
            // tabPageStep4
            // 
            this.tabPageStep4.Location = new System.Drawing.Point(4, 22);
            this.tabPageStep4.Name = "tabPageStep4";
            this.tabPageStep4.Size = new System.Drawing.Size(664, 470);
            this.tabPageStep4.TabIndex = 3;
            this.tabPageStep4.Text = "Xếp Lịch";
            this.tabPageStep4.UseVisualStyleBackColor = true;
            // 
            // frmSheduling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 496);
            this.Controls.Add(this.tabStep);
            this.Name = "frmSheduling";
            this.Text = "frmSheduling";
            this.tabStep.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabStep;
        private System.Windows.Forms.TabPage tabPageStep1;
        private System.Windows.Forms.TabPage tabPageStep2;
        private System.Windows.Forms.TabPage tabPageStep3;
        private System.Windows.Forms.TabPage tabPageStep4;
    }
}