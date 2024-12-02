namespace TreeMgmtSystem
{
    partial class DashboardForm
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
        //private void InitializeComponent()
        //{
        //    this.btnManageUsers = new System.Windows.Forms.Button();
        //    this.btnManageServices = new System.Windows.Forms.Button();
        //    this.btnManageTrees = new System.Windows.Forms.Button();
        //    this.lblTitle = new System.Windows.Forms.Label();
        //    this.lblTotalTreesTitle = new System.Windows.Forms.Label();
        //    this.lblTotalReportsTitle = new System.Windows.Forms.Label();
        //    this.lblTotalUsersTitle = new System.Windows.Forms.Label();
        //    this.lblTotalServicesTitle = new System.Windows.Forms.Label();
        //    this.lblTreeStatusTitle = new System.Windows.Forms.Label();
        //    this.lblTotalTrees = new System.Windows.Forms.Label();
        //    this.lblTreeStatus = new System.Windows.Forms.Label();
        //    this.dataGridView1 = new System.Windows.Forms.DataGridView();
        //    this.lblTotalServices = new System.Windows.Forms.Label();
        //    this.lblTotalReports = new System.Windows.Forms.Label();
        //    this.lblTotalUsers = new System.Windows.Forms.Label();
        //    this.btnManageReports = new System.Windows.Forms.Button();
        //    this.btnWarning = new System.Windows.Forms.Button();
        //    ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        //    this.SuspendLayout();
        //    // 
        //    // btnManageUsers
        //    // 
        //    this.btnManageUsers.Location = new System.Drawing.Point(532, 456);
        //    this.btnManageUsers.Name = "btnManageUsers";
        //    this.btnManageUsers.Size = new System.Drawing.Size(124, 58);
        //    this.btnManageUsers.TabIndex = 30;
        //    this.btnManageUsers.Text = "Quản lý người dùng";
        //    this.btnManageUsers.UseVisualStyleBackColor = true;
        //    this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
        //    // 
        //    // btnManageServices
        //    // 
        //    this.btnManageServices.Location = new System.Drawing.Point(306, 456);
        //    this.btnManageServices.Name = "btnManageServices";
        //    this.btnManageServices.Size = new System.Drawing.Size(124, 58);
        //    this.btnManageServices.TabIndex = 29;
        //    this.btnManageServices.Text = "Quản lý dịch vụ";
        //    this.btnManageServices.UseVisualStyleBackColor = true;
        //    this.btnManageServices.Click += new System.EventHandler(this.btnManageServices_Click);
        //    // 
        //    // btnManageTrees
        //    // 
        //    this.btnManageTrees.Location = new System.Drawing.Point(83, 456);
        //    this.btnManageTrees.Name = "btnManageTrees";
        //    this.btnManageTrees.Size = new System.Drawing.Size(124, 58);
        //    this.btnManageTrees.TabIndex = 28;
        //    this.btnManageTrees.Text = "Quản lý cây xanh";
        //    this.btnManageTrees.UseVisualStyleBackColor = true;
        //    this.btnManageTrees.Click += new System.EventHandler(this.btnManageTrees_Click);
        //    // 
        //    // lblTitle
        //    // 
        //    this.lblTitle.AutoSize = true;
        //    this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.lblTitle.Location = new System.Drawing.Point(346, 9);
        //    this.lblTitle.Name = "lblTitle";
        //    this.lblTitle.Size = new System.Drawing.Size(138, 29);
        //    this.lblTitle.TabIndex = 26;
        //    this.lblTitle.Text = "Tổng quan";
        //    // 
        //    // lblTotalTreesTitle
        //    // 
        //    this.lblTotalTreesTitle.AutoSize = true;
        //    this.lblTotalTreesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.lblTotalTreesTitle.Location = new System.Drawing.Point(79, 62);
        //    this.lblTotalTreesTitle.Name = "lblTotalTreesTitle";
        //    this.lblTotalTreesTitle.Size = new System.Drawing.Size(158, 20);
        //    this.lblTotalTreesTitle.TabIndex = 31;
        //    this.lblTotalTreesTitle.Text = "Tổng số cây xanh: ";
        //    // 
        //    // lblTotalReportsTitle
        //    // 
        //    this.lblTotalReportsTitle.AutoSize = true;
        //    this.lblTotalReportsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.lblTotalReportsTitle.Location = new System.Drawing.Point(89, 322);
        //    this.lblTotalReportsTitle.Name = "lblTotalReportsTitle";
        //    this.lblTotalReportsTitle.Size = new System.Drawing.Size(147, 20);
        //    this.lblTotalReportsTitle.TabIndex = 32;
        //    this.lblTotalReportsTitle.Text = "Tổng số báo cáo:";
        //    // 
        //    // lblTotalUsersTitle
        //    // 
        //    this.lblTotalUsersTitle.AutoSize = true;
        //    this.lblTotalUsersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.lblTotalUsersTitle.Location = new System.Drawing.Point(64, 372);
        //    this.lblTotalUsersTitle.Name = "lblTotalUsersTitle";
        //    this.lblTotalUsersTitle.Size = new System.Drawing.Size(172, 20);
        //    this.lblTotalUsersTitle.TabIndex = 33;
        //    this.lblTotalUsersTitle.Text = "Tổng số người dùng:";
        //    // 
        //    // lblTotalServicesTitle
        //    // 
        //    this.lblTotalServicesTitle.AutoSize = true;
        //    this.lblTotalServicesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.lblTotalServicesTitle.Location = new System.Drawing.Point(31, 274);
        //    this.lblTotalServicesTitle.Name = "lblTotalServicesTitle";
        //    this.lblTotalServicesTitle.Size = new System.Drawing.Size(206, 20);
        //    this.lblTotalServicesTitle.TabIndex = 34;
        //    this.lblTotalServicesTitle.Text = "Tổng số dịch vụ yêu cầu:";
        //    // 
        //    // lblTreeStatusTitle
        //    // 
        //    this.lblTreeStatusTitle.AutoSize = true;
        //    this.lblTreeStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.lblTreeStatusTitle.Location = new System.Drawing.Point(67, 127);
        //    this.lblTreeStatusTitle.Name = "lblTreeStatusTitle";
        //    this.lblTreeStatusTitle.Size = new System.Drawing.Size(170, 20);
        //    this.lblTreeStatusTitle.TabIndex = 35;
        //    this.lblTreeStatusTitle.Text = "Tình trạng cây xanh:";
        //    // 
        //    // lblTotalTrees
        //    // 
        //    this.lblTotalTrees.AutoSize = true;
        //    this.lblTotalTrees.Location = new System.Drawing.Point(243, 62);
        //    this.lblTotalTrees.Name = "lblTotalTrees";
        //    this.lblTotalTrees.Size = new System.Drawing.Size(19, 20);
        //    this.lblTotalTrees.TabIndex = 36;
        //    this.lblTotalTrees.Text = "0";
        //    // 
        //    // lblTreeStatus
        //    // 
        //    this.lblTreeStatus.AutoSize = true;
        //    this.lblTreeStatus.Location = new System.Drawing.Point(334, 127);
        //    this.lblTreeStatus.Name = "lblTreeStatus";
        //    this.lblTreeStatus.Size = new System.Drawing.Size(0, 20);
        //    this.lblTreeStatus.TabIndex = 37;
        //    // 
        //    // dataGridView1
        //    // 
        //    this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        //    this.dataGridView1.Location = new System.Drawing.Point(68, 159);
        //    this.dataGridView1.Name = "dataGridView1";
        //    this.dataGridView1.RowHeadersWidth = 62;
        //    this.dataGridView1.RowTemplate.Height = 28;
        //    this.dataGridView1.Size = new System.Drawing.Size(707, 99);
        //    this.dataGridView1.TabIndex = 38;
        //    // 
        //    // lblTotalServices
        //    // 
        //    this.lblTotalServices.AutoSize = true;
        //    this.lblTotalServices.Location = new System.Drawing.Point(243, 274);
        //    this.lblTotalServices.Name = "lblTotalServices";
        //    this.lblTotalServices.Size = new System.Drawing.Size(19, 20);
        //    this.lblTotalServices.TabIndex = 39;
        //    this.lblTotalServices.Text = "0";
        //    // 
        //    // lblTotalReports
        //    // 
        //    this.lblTotalReports.AutoSize = true;
        //    this.lblTotalReports.Location = new System.Drawing.Point(243, 322);
        //    this.lblTotalReports.Name = "lblTotalReports";
        //    this.lblTotalReports.Size = new System.Drawing.Size(19, 20);
        //    this.lblTotalReports.TabIndex = 40;
        //    this.lblTotalReports.Text = "0";
        //    // 
        //    // lblTotalUsers
        //    // 
        //    this.lblTotalUsers.AutoSize = true;
        //    this.lblTotalUsers.Location = new System.Drawing.Point(243, 372);
        //    this.lblTotalUsers.Name = "lblTotalUsers";
        //    this.lblTotalUsers.Size = new System.Drawing.Size(19, 20);
        //    this.lblTotalUsers.TabIndex = 41;
        //    this.lblTotalUsers.Text = "0";
        //    // 
        //    // btnManageReports
        //    // 
        //    this.btnManageReports.Location = new System.Drawing.Point(735, 456);
        //    this.btnManageReports.Name = "btnManageReports";
        //    this.btnManageReports.Size = new System.Drawing.Size(124, 58);
        //    this.btnManageReports.TabIndex = 42;
        //    this.btnManageReports.Text = "Quản lý báo cáo";
        //    this.btnManageReports.UseVisualStyleBackColor = true;
        //    this.btnManageReports.Click += new System.EventHandler(this.btnManageReports_Click);
        //    // 
        //    // btnWarning
        //    // 
        //    this.btnWarning.Location = new System.Drawing.Point(651, 62);
        //    this.btnWarning.Name = "btnWarning";
        //    this.btnWarning.Size = new System.Drawing.Size(124, 58);
        //    this.btnWarning.TabIndex = 43;
        //    this.btnWarning.Text = "Cảnh báo!";
        //    this.btnWarning.UseVisualStyleBackColor = true;
        //    this.btnWarning.Click += new System.EventHandler(this.btnWarning_Click);
        //    // 
        //    // DashboardForm
        //    // 
        //    this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(889, 577);
        //    this.Controls.Add(this.btnWarning);
        //    this.Controls.Add(this.btnManageReports);
        //    this.Controls.Add(this.lblTotalUsers);
        //    this.Controls.Add(this.lblTotalReports);
        //    this.Controls.Add(this.lblTotalServices);
        //    this.Controls.Add(this.dataGridView1);
        //    this.Controls.Add(this.lblTreeStatus);
        //    this.Controls.Add(this.lblTotalTrees);
        //    this.Controls.Add(this.lblTreeStatusTitle);
        //    this.Controls.Add(this.lblTotalServicesTitle);
        //    this.Controls.Add(this.lblTotalUsersTitle);
        //    this.Controls.Add(this.lblTotalReportsTitle);
        //    this.Controls.Add(this.lblTotalTreesTitle);
        //    this.Controls.Add(this.btnManageUsers);
        //    this.Controls.Add(this.btnManageServices);
        //    this.Controls.Add(this.btnManageTrees);
        //    this.Controls.Add(this.lblTitle);
        //    this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.Name = "DashboardForm";
        //    this.Text = "DashboardForm";
        //    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashboardForm_FormClosing);
        //    ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        //    this.ResumeLayout(false);
        //    this.PerformLayout();

        //}

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblTotalUsers = new System.Windows.Forms.Label();
            this.lblTotalReports = new System.Windows.Forms.Label();
            this.lblTotalServices = new System.Windows.Forms.Label();
            this.lblTreeStatus = new System.Windows.Forms.Label();
            this.lblTotalTrees = new System.Windows.Forms.Label();
            this.lblTotalUsersTitle = new System.Windows.Forms.Label();
            this.lblTotalReportsTitle = new System.Windows.Forms.Label();
            this.lblTotalServicesTitle = new System.Windows.Forms.Label();
            this.lblTreeStatusTitle = new System.Windows.Forms.Label();
            this.lblTotalTreesTitle = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnWarning = new System.Windows.Forms.Button();
            this.btnManageReports = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageServices = new System.Windows.Forms.Button();
            this.btnManageTrees = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(889, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(346, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(181, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Tổng quan";
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.Azure;
            this.panelSummary.Controls.Add(this.lblTotalUsers);
            this.panelSummary.Controls.Add(this.lblTotalReports);
            this.panelSummary.Controls.Add(this.lblTotalServices);
            this.panelSummary.Controls.Add(this.lblTreeStatus);
            this.panelSummary.Controls.Add(this.lblTotalTrees);
            this.panelSummary.Controls.Add(this.lblTotalUsersTitle);
            this.panelSummary.Controls.Add(this.lblTotalReportsTitle);
            this.panelSummary.Controls.Add(this.lblTotalServicesTitle);
            this.panelSummary.Controls.Add(this.lblTreeStatusTitle);
            this.panelSummary.Controls.Add(this.lblTotalTreesTitle);
            this.panelSummary.Location = new System.Drawing.Point(12, 66);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(865, 150);
            this.panelSummary.TabIndex = 1;
            // 
            // lblTotalUsers
            // 
            this.lblTotalUsers.AutoSize = true;
            this.lblTotalUsers.Location = new System.Drawing.Point(529, 74);
            this.lblTotalUsers.Name = "lblTotalUsers";
            this.lblTotalUsers.Size = new System.Drawing.Size(19, 20);
            this.lblTotalUsers.TabIndex = 41;
            this.lblTotalUsers.Text = "0";
            // 
            // lblTotalReports
            // 
            this.lblTotalReports.AutoSize = true;
            this.lblTotalReports.Location = new System.Drawing.Point(243, 74);
            this.lblTotalReports.Name = "lblTotalReports";
            this.lblTotalReports.Size = new System.Drawing.Size(19, 20);
            this.lblTotalReports.TabIndex = 40;
            this.lblTotalReports.Text = "0";
            // 
            // lblTotalServices
            // 
            this.lblTotalServices.AutoSize = true;
            this.lblTotalServices.Location = new System.Drawing.Point(243, 38);
            this.lblTotalServices.Name = "lblTotalServices";
            this.lblTotalServices.Size = new System.Drawing.Size(19, 20);
            this.lblTotalServices.TabIndex = 39;
            this.lblTotalServices.Text = "0";
            // 
            // lblTreeStatus
            // 
            this.lblTreeStatus.AutoSize = true;
            this.lblTreeStatus.Location = new System.Drawing.Point(538, 74);
            this.lblTreeStatus.Name = "lblTreeStatus";
            this.lblTreeStatus.Size = new System.Drawing.Size(0, 20);
            this.lblTreeStatus.TabIndex = 37;
            // 
            // lblTotalTrees
            // 
            this.lblTotalTrees.AutoSize = true;
            this.lblTotalTrees.Location = new System.Drawing.Point(529, 38);
            this.lblTotalTrees.Name = "lblTotalTrees";
            this.lblTotalTrees.Size = new System.Drawing.Size(19, 20);
            this.lblTotalTrees.TabIndex = 36;
            this.lblTotalTrees.Text = "0";
            // 
            // lblTotalUsersTitle
            // 
            this.lblTotalUsersTitle.AutoSize = true;
            this.lblTotalUsersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUsersTitle.Location = new System.Drawing.Point(346, 38);
            this.lblTotalUsersTitle.Name = "lblTotalUsersTitle";
            this.lblTotalUsersTitle.Size = new System.Drawing.Size(172, 20);
            this.lblTotalUsersTitle.TabIndex = 33;
            this.lblTotalUsersTitle.Text = "Tổng số người dùng:";
            // 
            // lblTotalReportsTitle
            // 
            this.lblTotalReportsTitle.AutoSize = true;
            this.lblTotalReportsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReportsTitle.Location = new System.Drawing.Point(89, 74);
            this.lblTotalReportsTitle.Name = "lblTotalReportsTitle";
            this.lblTotalReportsTitle.Size = new System.Drawing.Size(147, 20);
            this.lblTotalReportsTitle.TabIndex = 32;
            this.lblTotalReportsTitle.Text = "Tổng số báo cáo:";
            // 
            // lblTotalServicesTitle
            // 
            this.lblTotalServicesTitle.AutoSize = true;
            this.lblTotalServicesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalServicesTitle.Location = new System.Drawing.Point(31, 38);
            this.lblTotalServicesTitle.Name = "lblTotalServicesTitle";
            this.lblTotalServicesTitle.Size = new System.Drawing.Size(206, 20);
            this.lblTotalServicesTitle.TabIndex = 34;
            this.lblTotalServicesTitle.Text = "Tổng số dịch vụ yêu cầu:";
            // 
            // lblTreeStatusTitle
            // 
            this.lblTreeStatusTitle.AutoSize = true;
            this.lblTreeStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreeStatusTitle.Location = new System.Drawing.Point(67, 118);
            this.lblTreeStatusTitle.Name = "lblTreeStatusTitle";
            this.lblTreeStatusTitle.Size = new System.Drawing.Size(170, 20);
            this.lblTreeStatusTitle.TabIndex = 35;
            this.lblTreeStatusTitle.Text = "Tình trạng cây xanh:";
            // 
            // lblTotalTreesTitle
            // 
            this.lblTotalTreesTitle.AutoSize = true;
            this.lblTotalTreesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTreesTitle.Location = new System.Drawing.Point(365, 74);
            this.lblTotalTreesTitle.Name = "lblTotalTreesTitle";
            this.lblTotalTreesTitle.Size = new System.Drawing.Size(153, 20);
            this.lblTotalTreesTitle.TabIndex = 31;
            this.lblTotalTreesTitle.Text = "Tổng số cây xanh:";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Azure;
            this.panelActions.Controls.Add(this.btnWarning);
            this.panelActions.Controls.Add(this.btnManageReports);
            this.panelActions.Controls.Add(this.btnManageUsers);
            this.panelActions.Controls.Add(this.btnManageServices);
            this.panelActions.Controls.Add(this.btnManageTrees);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 350);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(889, 150);
            this.panelActions.TabIndex = 2;
            // 
            // btnWarning
            // 
            this.btnWarning.Location = new System.Drawing.Point(751, 20);
            this.btnWarning.Name = "btnWarning";
            this.btnWarning.Size = new System.Drawing.Size(124, 58);
            this.btnWarning.TabIndex = 43;
            this.btnWarning.Text = "Cảnh báo!";
            this.btnWarning.UseVisualStyleBackColor = true;
            this.btnWarning.Click += new System.EventHandler(this.btnWarning_Click);
            // 
            // btnManageReports
            // 
            this.btnManageReports.Location = new System.Drawing.Point(576, 20);
            this.btnManageReports.Name = "btnManageReports";
            this.btnManageReports.Size = new System.Drawing.Size(124, 58);
            this.btnManageReports.TabIndex = 42;
            this.btnManageReports.Text = "Quản lý báo cáo";
            this.btnManageReports.UseVisualStyleBackColor = true;
            this.btnManageReports.Click += new System.EventHandler(this.btnManageReports_Click);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Location = new System.Drawing.Point(24, 20);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(124, 58);
            this.btnManageUsers.TabIndex = 30;
            this.btnManageUsers.Text = "Quản lý người dùng";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageServices
            // 
            this.btnManageServices.Location = new System.Drawing.Point(215, 20);
            this.btnManageServices.Name = "btnManageServices";
            this.btnManageServices.Size = new System.Drawing.Size(124, 58);
            this.btnManageServices.TabIndex = 29;
            this.btnManageServices.Text = "Quản lý dịch vụ";
            this.btnManageServices.UseVisualStyleBackColor = true;
            this.btnManageServices.Click += new System.EventHandler(this.btnManageServices_Click);
            // 
            // btnManageTrees
            // 
            this.btnManageTrees.Location = new System.Drawing.Point(396, 20);
            this.btnManageTrees.Name = "btnManageTrees";
            this.btnManageTrees.Size = new System.Drawing.Size(124, 58);
            this.btnManageTrees.TabIndex = 28;
            this.btnManageTrees.Text = "Quản lý cây xanh";
            this.btnManageTrees.UseVisualStyleBackColor = true;
            this.btnManageTrees.Click += new System.EventHandler(this.btnManageTrees_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 240);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(865, 100);
            this.dataGridView1.TabIndex = 3;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 500);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashboardForm_FormClosing);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panelActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        // Fields
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblTotalUsers;
        private System.Windows.Forms.Label lblTotalReports;
        private System.Windows.Forms.Label lblTotalServices;
        private System.Windows.Forms.Label lblTreeStatus;
        private System.Windows.Forms.Label lblTotalTrees;
        private System.Windows.Forms.Label lblTotalUsersTitle;
        private System.Windows.Forms.Label lblTotalReportsTitle;
        private System.Windows.Forms.Label lblTotalServicesTitle;
        private System.Windows.Forms.Label lblTreeStatusTitle;
        private System.Windows.Forms.Label lblTotalTreesTitle;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnWarning;
        private System.Windows.Forms.Button btnManageReports;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnManageServices;
        private System.Windows.Forms.Button btnManageTrees;
        private System.Windows.Forms.DataGridView dataGridView1;

        //private System.Windows.Forms.Button btnManageUsers;
        //private System.Windows.Forms.Button btnManageServices;
        //private System.Windows.Forms.Button btnManageTrees;
        //private System.Windows.Forms.Label lblTitle;
        //private System.Windows.Forms.Label lblTotalTreesTitle;
        //private System.Windows.Forms.Label lblTotalReportsTitle;
        //private System.Windows.Forms.Label lblTotalUsersTitle;
        //private System.Windows.Forms.Label lblTotalServicesTitle;
        //private System.Windows.Forms.Label lblTreeStatusTitle;
        //private System.Windows.Forms.Label lblTotalTrees;
        //private System.Windows.Forms.Label lblTreeStatus;
        //private System.Windows.Forms.DataGridView dataGridView1;
        //private System.Windows.Forms.Label lblTotalServices;
        //private System.Windows.Forms.Label lblTotalReports;
        //private System.Windows.Forms.Label lblTotalUsers;
        //private System.Windows.Forms.Button btnManageReports;
        //private System.Windows.Forms.Button btnWarning;
    }
}