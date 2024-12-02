namespace TreeMgmtSystem
{
    partial class EditTreeForm
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
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.panelSpecies = new System.Windows.Forms.Panel();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.lblSpecies = new System.Windows.Forms.Label();
            this.panelHeight = new System.Windows.Forms.Panel();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.panelDiameter = new System.Windows.Forms.Panel();
            this.txtDiameter = new System.Windows.Forms.TextBox();
            this.lblDiameter = new System.Windows.Forms.Label();
            this.panelHealthStatus = new System.Windows.Forms.Panel();
            this.txtHealthStatus = new System.Windows.Forms.TextBox();
            this.lblHealthStatus = new System.Windows.Forms.Label();
            this.panelLocation = new System.Windows.Forms.Panel();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.panelNote = new System.Windows.Forms.Panel();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.panelReminderDate = new System.Windows.Forms.Panel();
            this.dateTimePickerReminder = new System.Windows.Forms.DateTimePicker();
            this.lblReminderDate = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.panelAge = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelUserId.SuspendLayout();
            this.panelSpecies.SuspendLayout();
            this.panelHeight.SuspendLayout();
            this.panelDiameter.SuspendLayout();
            this.panelHealthStatus.SuspendLayout();
            this.panelLocation.SuspendLayout();
            this.panelNote.SuspendLayout();
            this.panelReminderDate.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelAge.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(709, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(250, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(245, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chỉnh Sửa Cây";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelUserId);
            this.panelMain.Controls.Add(this.panelSpecies);
            this.panelMain.Controls.Add(this.panelAge);
            this.panelMain.Controls.Add(this.panelHeight);
            this.panelMain.Controls.Add(this.panelDiameter);
            this.panelMain.Controls.Add(this.panelHealthStatus);
            this.panelMain.Controls.Add(this.panelLocation);
            this.panelMain.Controls.Add(this.panelNote);
            this.panelMain.Controls.Add(this.panelReminderDate);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(709, 511);
            this.panelMain.TabIndex = 1;
            // 
            // panelUserId
            // 
            this.panelUserId.Controls.Add(this.txtUserId);
            this.panelUserId.Controls.Add(this.lblUserId);
            this.panelUserId.Location = new System.Drawing.Point(12, 12);
            this.panelUserId.Name = "panelUserId";
            this.panelUserId.Size = new System.Drawing.Size(685, 50);
            this.panelUserId.TabIndex = 0;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(128, 12);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(550, 26);
            this.txtUserId.TabIndex = 1;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(3, 15);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(119, 20);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "Mã Người Dùng";
            // 
            // panelSpecies
            // 
            this.panelSpecies.Controls.Add(this.txtSpecies);
            this.panelSpecies.Controls.Add(this.lblSpecies);
            this.panelSpecies.Location = new System.Drawing.Point(12, 68);
            this.panelSpecies.Name = "panelSpecies";
            this.panelSpecies.Size = new System.Drawing.Size(685, 50);
            this.panelSpecies.TabIndex = 1;
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(128, 12);
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(550, 26);
            this.txtSpecies.TabIndex = 1;
            // 
            // lblSpecies
            // 
            this.lblSpecies.AutoSize = true;
            this.lblSpecies.Location = new System.Drawing.Point(3, 15);
            this.lblSpecies.Name = "lblSpecies";
            this.lblSpecies.Size = new System.Drawing.Size(70, 20);
            this.lblSpecies.TabIndex = 0;
            this.lblSpecies.Text = "Loài Cây";
            // 
            // panelHeight
            // 
            this.panelHeight.Controls.Add(this.txtHeight);
            this.panelHeight.Controls.Add(this.lblHeight);
            this.panelHeight.Location = new System.Drawing.Point(12, 180);
            this.panelHeight.Name = "panelHeight";
            this.panelHeight.Size = new System.Drawing.Size(685, 50);
            this.panelHeight.TabIndex = 3;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(128, 12);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(550, 26);
            this.txtHeight.TabIndex = 1;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(3, 15);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(83, 20);
            this.lblHeight.TabIndex = 0;
            this.lblHeight.Text = "Chiều Cao";
            // 
            // panelDiameter
            // 
            this.panelDiameter.Controls.Add(this.txtDiameter);
            this.panelDiameter.Controls.Add(this.lblDiameter);
            this.panelDiameter.Location = new System.Drawing.Point(12, 236);
            this.panelDiameter.Name = "panelDiameter";
            this.panelDiameter.Size = new System.Drawing.Size(685, 50);
            this.panelDiameter.TabIndex = 4;
            // 
            // txtDiameter
            // 
            this.txtDiameter.Location = new System.Drawing.Point(128, 12);
            this.txtDiameter.Name = "txtDiameter";
            this.txtDiameter.Size = new System.Drawing.Size(550, 26);
            this.txtDiameter.TabIndex = 1;
            // 
            // lblDiameter
            // 
            this.lblDiameter.AutoSize = true;
            this.lblDiameter.Location = new System.Drawing.Point(3, 15);
            this.lblDiameter.Name = "lblDiameter";
            this.lblDiameter.Size = new System.Drawing.Size(92, 20);
            this.lblDiameter.TabIndex = 0;
            this.lblDiameter.Text = "Đường Kính";
            // 
            // panelHealthStatus
            // 
            this.panelHealthStatus.Controls.Add(this.txtHealthStatus);
            this.panelHealthStatus.Controls.Add(this.lblHealthStatus);
            this.panelHealthStatus.Location = new System.Drawing.Point(12, 292);
            this.panelHealthStatus.Name = "panelHealthStatus";
            this.panelHealthStatus.Size = new System.Drawing.Size(685, 50);
            this.panelHealthStatus.TabIndex = 5;
            // 
            // txtHealthStatus
            // 
            this.txtHealthStatus.Location = new System.Drawing.Point(128, 12);
            this.txtHealthStatus.Name = "txtHealthStatus";
            this.txtHealthStatus.Size = new System.Drawing.Size(550, 26);
            this.txtHealthStatus.TabIndex = 1;
            // 
            // lblHealthStatus
            // 
            this.lblHealthStatus.AutoSize = true;
            this.lblHealthStatus.Location = new System.Drawing.Point(3, 15);
            this.lblHealthStatus.Name = "lblHealthStatus";
            this.lblHealthStatus.Size = new System.Drawing.Size(84, 20);
            this.lblHealthStatus.TabIndex = 0;
            this.lblHealthStatus.Text = "Tình Trạng";
            // 
            // panelLocation
            // 
            this.panelLocation.Controls.Add(this.txtLocation);
            this.panelLocation.Controls.Add(this.lblLocation);
            this.panelLocation.Location = new System.Drawing.Point(12, 348);
            this.panelLocation.Name = "panelLocation";
            this.panelLocation.Size = new System.Drawing.Size(685, 50);
            this.panelLocation.TabIndex = 6;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(128, 9);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(550, 26);
            this.txtLocation.TabIndex = 1;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(3, 15);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(44, 20);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Vị Trí";
            // 
            // panelNote
            // 
            this.panelNote.Controls.Add(this.txtNote);
            this.panelNote.Controls.Add(this.lblNote);
            this.panelNote.Location = new System.Drawing.Point(12, 404);
            this.panelNote.Name = "panelNote";
            this.panelNote.Size = new System.Drawing.Size(685, 50);
            this.panelNote.TabIndex = 7;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(128, 9);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(550, 26);
            this.txtNote.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(3, 15);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(67, 20);
            this.lblNote.TabIndex = 0;
            this.lblNote.Text = "Ghi Chú";
            // 
            // panelReminderDate
            // 
            this.panelReminderDate.Controls.Add(this.dateTimePickerReminder);
            this.panelReminderDate.Controls.Add(this.lblReminderDate);
            this.panelReminderDate.Location = new System.Drawing.Point(12, 460);
            this.panelReminderDate.Name = "panelReminderDate";
            this.panelReminderDate.Size = new System.Drawing.Size(685, 50);
            this.panelReminderDate.TabIndex = 8;
            // 
            // dateTimePickerReminder
            // 
            this.dateTimePickerReminder.Location = new System.Drawing.Point(128, 10);
            this.dateTimePickerReminder.Name = "dateTimePickerReminder";
            this.dateTimePickerReminder.Size = new System.Drawing.Size(550, 26);
            this.dateTimePickerReminder.TabIndex = 1;
            // 
            // lblReminderDate
            // 
            this.lblReminderDate.AutoSize = true;
            this.lblReminderDate.Location = new System.Drawing.Point(3, 15);
            this.lblReminderDate.Name = "lblReminderDate";
            this.lblReminderDate.Size = new System.Drawing.Size(119, 20);
            this.lblReminderDate.TabIndex = 0;
            this.lblReminderDate.Text = "Ngày Nhắc Nhở";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Azure;
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 571);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(709, 100);
            this.panelActions.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(289, 28);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(3, 15);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(39, 20);
            this.lblAge.TabIndex = 0;
            this.lblAge.Text = "Tuổi";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(128, 12);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(550, 26);
            this.txtAge.TabIndex = 1;
            // 
            // panelAge
            // 
            this.panelAge.Controls.Add(this.txtAge);
            this.panelAge.Controls.Add(this.lblAge);
            this.panelAge.Location = new System.Drawing.Point(12, 124);
            this.panelAge.Name = "panelAge";
            this.panelAge.Size = new System.Drawing.Size(685, 50);
            this.panelAge.TabIndex = 2;
            // 
            // EditTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 671);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
            this.Name = "EditTreeForm";
            this.Text = "Chỉnh Sửa Cây";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelUserId.ResumeLayout(false);
            this.panelUserId.PerformLayout();
            this.panelSpecies.ResumeLayout(false);
            this.panelSpecies.PerformLayout();
            this.panelHeight.ResumeLayout(false);
            this.panelHeight.PerformLayout();
            this.panelDiameter.ResumeLayout(false);
            this.panelDiameter.PerformLayout();
            this.panelHealthStatus.ResumeLayout(false);
            this.panelHealthStatus.PerformLayout();
            this.panelLocation.ResumeLayout(false);
            this.panelLocation.PerformLayout();
            this.panelNote.ResumeLayout(false);
            this.panelNote.PerformLayout();
            this.panelReminderDate.ResumeLayout(false);
            this.panelReminderDate.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelAge.ResumeLayout(false);
            this.panelAge.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Panel panelSpecies;
        private System.Windows.Forms.Label lblSpecies;
        private System.Windows.Forms.TextBox txtSpecies;
        private System.Windows.Forms.Panel panelHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Panel panelDiameter;
        private System.Windows.Forms.Label lblDiameter;
        private System.Windows.Forms.TextBox txtDiameter;
        private System.Windows.Forms.Panel panelHealthStatus;
        private System.Windows.Forms.Label lblHealthStatus;
        private System.Windows.Forms.TextBox txtHealthStatus;
        private System.Windows.Forms.Panel panelLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Panel panelNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Panel panelReminderDate;
        private System.Windows.Forms.Label lblReminderDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerReminder;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblAge;
    }
}

