namespace TreeMgmtSystem
{
    partial class AddReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelReportContent = new System.Windows.Forms.Panel();
            this.lblReportContent = new System.Windows.Forms.Label();
            this.txtReportContent = new System.Windows.Forms.TextBox();
            this.panelUserId = new System.Windows.Forms.Panel();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelReportContent.SuspendLayout();
            this.panelUserId.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(200, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Báo Cáo";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelReportContent);
            this.panelMain.Controls.Add(this.panelUserId);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 240);
            this.panelMain.TabIndex = 1;
            // 
            // panelReportContent
            // 
            this.panelReportContent.Controls.Add(this.lblReportContent);
            this.panelReportContent.Controls.Add(this.txtReportContent);
            this.panelReportContent.Location = new System.Drawing.Point(12, 12);
            this.panelReportContent.Name = "panelReportContent";
            this.panelReportContent.Size = new System.Drawing.Size(576, 180);
            this.panelReportContent.TabIndex = 0;
            // 
            // lblReportContent
            // 
            this.lblReportContent.AutoSize = true;
            this.lblReportContent.Location = new System.Drawing.Point(3, 15);
            this.lblReportContent.Name = "lblReportContent";
            this.lblReportContent.Size = new System.Drawing.Size(137, 20);
            this.lblReportContent.TabIndex = 0;
            this.lblReportContent.Text = "Nội Dung Báo Cáo";
            // 
            // txtReportContent
            // 
            this.txtReportContent.Location = new System.Drawing.Point(146, 12);
            this.txtReportContent.Multiline = true;
            this.txtReportContent.Name = "txtReportContent";
            this.txtReportContent.Size = new System.Drawing.Size(414, 150);
            this.txtReportContent.TabIndex = 1;
            // 
            // panelUserId
            // 
            this.panelUserId.Controls.Add(this.txtUserId);
            this.panelUserId.Location = new System.Drawing.Point(12, 198);
            this.panelUserId.Name = "panelUserId";
            this.panelUserId.Size = new System.Drawing.Size(576, 30);
            this.panelUserId.TabIndex = 1;
            this.panelUserId.Visible = false;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(146, 3);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(414, 26);
            this.txtUserId.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Azure;
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 240);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(600, 60);
            this.panelActions.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(262, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
            this.Name = "AddReportForm";
            this.Text = "Thêm Báo Cáo";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelReportContent.ResumeLayout(false);
            this.panelReportContent.PerformLayout();
            this.panelUserId.ResumeLayout(false);
            this.panelUserId.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelReportContent;
        private System.Windows.Forms.Label lblReportContent;
        private System.Windows.Forms.TextBox txtReportContent;
        private System.Windows.Forms.Panel panelUserId;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSave;
    }
}
