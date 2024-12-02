namespace TreeMgmtSystem
{
    partial class EditServiceForm
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
            this.panelUserId = new System.Windows.Forms.Panel();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.panelServiceType = new System.Windows.Forms.Panel();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.txtServiceType = new System.Windows.Forms.TextBox();
            this.panelRequestDate = new System.Windows.Forms.Panel();
            this.lblRequestDate = new System.Windows.Forms.Label();
            this.dateTimePickerRequestDate = new System.Windows.Forms.DateTimePicker();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelUserId.SuspendLayout();
            this.panelServiceType.SuspendLayout();
            this.panelRequestDate.SuspendLayout();
            this.panelStatus.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(800, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(300, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(188, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chỉnh Sửa Dịch Vụ";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelUserId);
            this.panelMain.Controls.Add(this.panelServiceType);
            this.panelMain.Controls.Add(this.panelRequestDate);
            this.panelMain.Controls.Add(this.panelStatus);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 340);
            this.panelMain.TabIndex = 1;
            // 
            // panelUserId
            // 
            this.panelUserId.Controls.Add(this.lblUserId);
            this.panelUserId.Controls.Add(this.txtUserId);
            this.panelUserId.Location = new System.Drawing.Point(12, 12);
            this.panelUserId.Name = "panelUserId";
            this.panelUserId.Size = new System.Drawing.Size(776, 50);
            this.panelUserId.TabIndex = 0;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(3, 15);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(91, 20);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "Mã Người Dùng";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(125, 12);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(648, 26);
            this.txtUserId.TabIndex = 1;
            // 
            // panelServiceType
            // 
            this.panelServiceType.Controls.Add(this.lblServiceType);
            this.panelServiceType.Controls.Add(this.txtServiceType);
            this.panelServiceType.Location = new System.Drawing.Point(12, 68);
            this.panelServiceType.Name = "panelServiceType";
            this.panelServiceType.Size = new System.Drawing.Size(776, 50);
            this.panelServiceType.TabIndex = 1;
            // 
            // lblServiceType
            // 
            this.lblServiceType.AutoSize = true;
            this.lblServiceType.Location = new System.Drawing.Point(3, 15);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(91, 20);
            this.lblServiceType.TabIndex = 0;
            this.lblServiceType.Text = "Loại Dịch Vụ";
            // 
            // txtServiceType
            // 
            this.txtServiceType.Location = new System.Drawing.Point(125, 12);
            this.txtServiceType.Name = "txtServiceType";
            this.txtServiceType.Size = new System.Drawing.Size(648, 26);
            this.txtServiceType.TabIndex = 1;
            // 
            // panelRequestDate
            // 
            this.panelRequestDate.Controls.Add(this.lblRequestDate);
            this.panelRequestDate.Controls.Add(this.dateTimePickerRequestDate);
            this.panelRequestDate.Location = new System.Drawing.Point(12, 124);
            this.panelRequestDate.Name = "panelRequestDate";
            this.panelRequestDate.Size = new System.Drawing.Size(776, 50);
            this.panelRequestDate.TabIndex = 2;
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.AutoSize = true;
            this.lblRequestDate.Location = new System.Drawing.Point(3, 15);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(101, 20);
            this.lblRequestDate.TabIndex = 0;
            this.lblRequestDate.Text = "Ngày Yêu Cầu";
            // 
            // dateTimePickerRequestDate
            // 
            this.dateTimePickerRequestDate.Location = new System.Drawing.Point(125, 12);
            this.dateTimePickerRequestDate.Name = "dateTimePickerRequestDate";
            this.dateTimePickerRequestDate.Size = new System.Drawing.Size(648, 26);
            this.dateTimePickerRequestDate.TabIndex = 1;
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.lblStatus);
            this.panelStatus.Controls.Add(this.txtStatus);
            this.panelStatus.Location = new System.Drawing.Point(12, 180);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(776, 50);
            this.panelStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(63, 20);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Trạng Thái";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(125, 12);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(648, 26);
            this.txtStatus.TabIndex = 1;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Azure;
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 400);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(800, 50);
            this.panelActions.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(345, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
            this.Name = "EditServiceForm";
            this.Text = "Chỉnh Sửa Dịch Vụ";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelUserId.ResumeLayout(false);
            this.panelUserId.PerformLayout();
            this.panelServiceType.ResumeLayout(false);
            this.panelServiceType.PerformLayout();
            this.panelRequestDate.ResumeLayout(false);
            this.panelRequestDate.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Panel panelServiceType;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.TextBox txtServiceType;
        private System.Windows.Forms.Panel panelRequestDate;
        private System.Windows.Forms.Label lblRequestDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerRequestDate;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSave;
    }
}

