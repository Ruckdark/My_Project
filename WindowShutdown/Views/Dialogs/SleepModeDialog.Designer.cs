namespace WindowShutdown
{
    partial class SleepModeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        // private System.ComponentModel.IContainer components = null; // Đã xóa

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Không cần kiểm tra components nữa
            // if (disposing && (components != null))
            // {
            //    components.Dispose();
            // }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnHibernate = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // btnSuspend
            //
            this.btnSuspend.Location = new System.Drawing.Point(12, 45);
            // *** Gợi ý thêm Icon ***
            // this.btnSuspend.Image = Properties.Resources.SuspendIcon;
            // this.btnSuspend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(135, 67);
            this.btnSuspend.TabIndex = 0;
            this.btnSuspend.Text = "Suspend";
            this.btnSuspend.UseVisualStyleBackColor = true;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            //
            // btnHibernate
            //
            this.btnHibernate.Location = new System.Drawing.Point(153, 45);
            // *** Gợi ý thêm Icon ***
            // this.btnHibernate.Image = Properties.Resources.HibernateIcon;
            // this.btnHibernate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHibernate.Name = "btnHibernate";
            this.btnHibernate.Size = new System.Drawing.Size(135, 67);
            this.btnHibernate.TabIndex = 1;
            this.btnHibernate.Text = "Hibernate";
            this.btnHibernate.UseVisualStyleBackColor = true;
            this.btnHibernate.Click += new System.EventHandler(this.btnHibernate_Click);
            //
            // lblDescription
            //
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(270, 25);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Chọn trạng thái ngủ cho máy tính:";
            //
            // SleepModeDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 125);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnHibernate);
            this.Controls.Add(this.btnSuspend);
            // Các thuộc tính FormBorderStyle, MaximizeBox, MinimizeBox, StartPosition, ShowInTaskbar được set trong code-behind (.cs)
            this.Name = "SleepModeDialog";
            this.Text = "Sleep / Hibernate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.Button btnHibernate;
        private System.Windows.Forms.Label lblDescription;
    }
}