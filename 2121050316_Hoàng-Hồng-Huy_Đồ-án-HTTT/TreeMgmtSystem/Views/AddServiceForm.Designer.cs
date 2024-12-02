namespace TreeMgmtSystem
{
    partial class AddServiceForm
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

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelServiceType = new System.Windows.Forms.Panel();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.comboBoxServiceType = new System.Windows.Forms.ComboBox();
            this.panelRequestDate = new System.Windows.Forms.Panel();
            this.lblRequestDate = new System.Windows.Forms.Label();
            this.dateTimePickerRequestDate = new System.Windows.Forms.DateTimePicker();
            this.panelUserId = new System.Windows.Forms.Panel();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelServiceType.SuspendLayout();
            this.panelRequestDate.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(233, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Dịch Vụ";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelServiceType);
            this.panelMain.Controls.Add(this.panelRequestDate);
            this.panelMain.Controls.Add(this.panelUserId);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 170);
            this.panelMain.TabIndex = 1;
            // 
            // panelServiceType
            // 
            this.panelServiceType.Controls.Add(this.lblServiceType);
            this.panelServiceType.Controls.Add(this.comboBoxServiceType);
            this.panelServiceType.Location = new System.Drawing.Point(12, 12);
            this.panelServiceType.Name = "panelServiceType";
            this.panelServiceType.Size = new System.Drawing.Size(576, 50);
            this.panelServiceType.TabIndex = 0;
            // 
            // lblServiceType
            // 
            this.lblServiceType.AutoSize = true;
            this.lblServiceType.Location = new System.Drawing.Point(3, 15);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(99, 20);
            this.lblServiceType.TabIndex = 0;
            this.lblServiceType.Text = "Loại Dịch Vụ";
            // 
            // comboBoxServiceType
            // 
            this.comboBoxServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServiceType.FormattingEnabled = true;
            this.comboBoxServiceType.Location = new System.Drawing.Point(125, 7);
            this.comboBoxServiceType.Name = "comboBoxServiceType";
            this.comboBoxServiceType.Size = new System.Drawing.Size(437, 28);
            this.comboBoxServiceType.TabIndex = 1;
            // 
            // panelRequestDate
            // 
            this.panelRequestDate.Controls.Add(this.lblRequestDate);
            this.panelRequestDate.Controls.Add(this.dateTimePickerRequestDate);
            this.panelRequestDate.Location = new System.Drawing.Point(12, 68);
            this.panelRequestDate.Name = "panelRequestDate";
            this.panelRequestDate.Size = new System.Drawing.Size(576, 50);
            this.panelRequestDate.TabIndex = 1;
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.AutoSize = true;
            this.lblRequestDate.Location = new System.Drawing.Point(3, 15);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(111, 20);
            this.lblRequestDate.TabIndex = 0;
            this.lblRequestDate.Text = "Ngày Yêu Cầu";
            // 
            // dateTimePickerRequestDate
            // 
            this.dateTimePickerRequestDate.Location = new System.Drawing.Point(125, 9);
            this.dateTimePickerRequestDate.Name = "dateTimePickerRequestDate";
            this.dateTimePickerRequestDate.Size = new System.Drawing.Size(437, 26);
            this.dateTimePickerRequestDate.TabIndex = 1;
            // 
            // panelUserId
            // 
            this.panelUserId.Controls.Add(this.txtUserId);
            this.panelUserId.Location = new System.Drawing.Point(12, 124);
            this.panelUserId.Name = "panelUserId";
            this.panelUserId.Size = new System.Drawing.Size(576, 50);
            this.panelUserId.TabIndex = 2;
            this.panelUserId.Visible = false;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(125, 12);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(437, 26);
            this.txtUserId.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Azure;
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 170);
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
            // AddServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 230);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Name = "AddServiceForm";
            this.Text = "Thêm Dịch Vụ";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelServiceType.ResumeLayout(false);
            this.panelServiceType.PerformLayout();
            this.panelRequestDate.ResumeLayout(false);
            this.panelRequestDate.PerformLayout();
            this.panelUserId.ResumeLayout(false);
            this.panelUserId.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelServiceType;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.ComboBox comboBoxServiceType;
        private System.Windows.Forms.Panel panelRequestDate;
        private System.Windows.Forms.Label lblRequestDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerRequestDate;
        private System.Windows.Forms.Panel panelUserId;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSave;
    }
}
