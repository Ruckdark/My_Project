// [FULL CODE - FINALIZED v2] FullModeControl.Designer.cs
namespace WindowShutdown
{
    partial class FullModeControl
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
            this.btn5minutes = new System.Windows.Forms.Button();
            this.btn10minutes = new System.Windows.Forms.Button();
            this.btn20minutes = new System.Windows.Forms.Button();
            this.btn30minutes = new System.Windows.Forms.Button();
            this.btn1Hour = new System.Windows.Forms.Button();
            this.btn2Hours = new System.Windows.Forms.Button();
            this.btn3Hours = new System.Windows.Forms.Button();
            this.btn5Hours = new System.Windows.Forms.Button();
            this.numericUpDownHour = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinute = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecond = new System.Windows.Forms.NumericUpDown();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblMinute = new System.Windows.Forms.Label();
            this.lblSecond = new System.Windows.Forms.Label();
            this.btnSelfSetup = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSleep = new System.Windows.Forms.Button();
            this.btnChangeMode = new System.Windows.Forms.Button();
            this.labelMain = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpQuickShutdown = new System.Windows.Forms.GroupBox();
            this.grpCustomShutdown = new System.Windows.Forms.GroupBox();
            this.customTimeLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.topPanelLayout = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpQuickShutdown.SuspendLayout();
            this.grpCustomShutdown.SuspendLayout();
            this.customTimeLayout.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.topPanelLayout.SuspendLayout();
            this.SuspendLayout();
            //
            // btn5minutes
            //
            this.btn5minutes.Location = new System.Drawing.Point(10, 30);
            this.btn5minutes.Name = "btn5minutes";
            this.btn5minutes.Size = new System.Drawing.Size(112, 34);
            this.btn5minutes.TabIndex = 0;
            this.btn5minutes.Text = "5 minutes";
            this.btn5minutes.UseVisualStyleBackColor = true;
            this.btn5minutes.Click += new System.EventHandler(this.btn5minutes_Click);
            //
            // btn10minutes
            //
            this.btn10minutes.Location = new System.Drawing.Point(128, 30);
            this.btn10minutes.Name = "btn10minutes";
            this.btn10minutes.Size = new System.Drawing.Size(112, 34);
            this.btn10minutes.TabIndex = 1;
            this.btn10minutes.Text = "10 minutes";
            this.btn10minutes.UseVisualStyleBackColor = true;
            this.btn10minutes.Click += new System.EventHandler(this.btn10minutes_Click);
            //
            // btn20minutes
            //
            this.btn20minutes.Location = new System.Drawing.Point(246, 30);
            this.btn20minutes.Name = "btn20minutes";
            this.btn20minutes.Size = new System.Drawing.Size(112, 34);
            this.btn20minutes.TabIndex = 2;
            this.btn20minutes.Text = "20 minutes";
            this.btn20minutes.UseVisualStyleBackColor = true;
            this.btn20minutes.Click += new System.EventHandler(this.btn20minutes_Click);
            //
            // btn30minutes
            //
            this.btn30minutes.Location = new System.Drawing.Point(364, 30);
            this.btn30minutes.Name = "btn30minutes";
            this.btn30minutes.Size = new System.Drawing.Size(112, 34);
            this.btn30minutes.TabIndex = 3;
            this.btn30minutes.Text = "30 minutes";
            this.btn30minutes.UseVisualStyleBackColor = true;
            this.btn30minutes.Click += new System.EventHandler(this.btn30minutes_Click);
            //
            // btn1Hour
            //
            this.btn1Hour.Location = new System.Drawing.Point(10, 70);
            this.btn1Hour.Name = "btn1Hour";
            this.btn1Hour.Size = new System.Drawing.Size(112, 34);
            this.btn1Hour.TabIndex = 4;
            this.btn1Hour.Text = "1 hour";
            this.btn1Hour.UseVisualStyleBackColor = true;
            this.btn1Hour.Click += new System.EventHandler(this.btn1Hour_Click);
            //
            // btn2Hours
            //
            this.btn2Hours.Location = new System.Drawing.Point(128, 70);
            this.btn2Hours.Name = "btn2Hours";
            this.btn2Hours.Size = new System.Drawing.Size(112, 34);
            this.btn2Hours.TabIndex = 5;
            this.btn2Hours.Text = "2 hours";
            this.btn2Hours.UseVisualStyleBackColor = true;
            this.btn2Hours.Click += new System.EventHandler(this.btn2Hours_Click);
            //
            // btn3Hours
            //
            this.btn3Hours.Location = new System.Drawing.Point(246, 70);
            this.btn3Hours.Name = "btn3Hours";
            this.btn3Hours.Size = new System.Drawing.Size(112, 34);
            this.btn3Hours.TabIndex = 6;
            this.btn3Hours.Text = "3 hours";
            this.btn3Hours.UseVisualStyleBackColor = true;
            this.btn3Hours.Click += new System.EventHandler(this.btn3Hours_Click);
            //
            // btn5Hours
            //
            this.btn5Hours.Location = new System.Drawing.Point(364, 70);
            this.btn5Hours.Name = "btn5Hours";
            this.btn5Hours.Size = new System.Drawing.Size(112, 34);
            this.btn5Hours.TabIndex = 7;
            this.btn5Hours.Text = "5 hours";
            this.btn5Hours.UseVisualStyleBackColor = true;
            this.btn5Hours.Click += new System.EventHandler(this.btn5Hours_Click);
            //
            // numericUpDownHour
            //
            this.numericUpDownHour.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownHour.Location = new System.Drawing.Point(3, 14);
            this.numericUpDownHour.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.numericUpDownHour.Name = "numericUpDownHour";
            this.numericUpDownHour.Size = new System.Drawing.Size(70, 31);
            this.numericUpDownHour.TabIndex = 0;
            //
            // numericUpDownMinute
            //
            this.numericUpDownMinute.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownMinute.Location = new System.Drawing.Point(123, 14);
            this.numericUpDownMinute.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.numericUpDownMinute.Name = "numericUpDownMinute";
            this.numericUpDownMinute.Size = new System.Drawing.Size(70, 31);
            this.numericUpDownMinute.TabIndex = 2;
            //
            // numericUpDownSecond
            //
            this.numericUpDownSecond.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownSecond.Location = new System.Drawing.Point(258, 14);
            this.numericUpDownSecond.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.numericUpDownSecond.Name = "numericUpDownSecond";
            this.numericUpDownSecond.Size = new System.Drawing.Size(70, 31);
            this.numericUpDownSecond.TabIndex = 4;
            //
            // lblHour
            //
            this.lblHour.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(76, 17);
            this.lblHour.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(34, 25);
            this.lblHour.TabIndex = 1;
            this.lblHour.Text = "giờ";
            //
            // lblMinute
            //
            this.lblMinute.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(196, 17);
            this.lblMinute.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(49, 25);
            this.lblMinute.TabIndex = 3;
            this.lblMinute.Text = "phút";
            //
            // lblSecond
            //
            this.lblSecond.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSecond.AutoSize = true;
            this.lblSecond.Location = new System.Drawing.Point(331, 17); // Vị trí bắt đầu của label trong cột của nó
            this.lblSecond.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblSecond.Name = "lblSecond";
            // Bỏ MinimumSize nếu dùng Absolute column
            // this.lblSecond.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblSecond.Size = new System.Drawing.Size(45, 25); // Kích thước thực tế của chữ "giây"
            this.lblSecond.TabIndex = 5;
            this.lblSecond.Text = "giây";
            //
            // btnSelfSetup
            //
            this.btnSelfSetup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customTimeLayout.SetColumnSpan(this.btnSelfSetup, 3);
            this.btnSelfSetup.Location = new System.Drawing.Point(12, 73);
            this.btnSelfSetup.Name = "btnSelfSetup";
            this.btnSelfSetup.Size = new System.Drawing.Size(180, 34);
            this.btnSelfSetup.TabIndex = 6;
            this.btnSelfSetup.Text = "Hẹn giờ Shutdown";
            this.btnSelfSetup.UseVisualStyleBackColor = true;
            this.btnSelfSetup.Click += new System.EventHandler(this.btnSelfSetup_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(20, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 34);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy lệnh";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // btnReset
            //
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customTimeLayout.SetColumnSpan(this.btnReset, 3);
            this.btnReset.Location = new System.Drawing.Point(207, 73);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(150, 34);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Đặt lại giờ";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // btnSleep
            //
            this.btnSleep.Location = new System.Drawing.Point(20, 70);
            this.btnSleep.Name = "btnSleep";
            this.btnSleep.Size = new System.Drawing.Size(180, 34);
            this.btnSleep.TabIndex = 1;
            this.btnSleep.Text = "Sleep / Hibernate...";
            this.btnSleep.UseVisualStyleBackColor = true;
            this.btnSleep.Click += new System.EventHandler(this.btnSleep_Click);
            //
            // btnChangeMode
            //
            this.btnChangeMode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnChangeMode.AutoSize = true;
            this.btnChangeMode.Location = new System.Drawing.Point(510, 8);
            this.btnChangeMode.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnChangeMode.MinimumSize = new System.Drawing.Size(150, 34);
            this.btnChangeMode.Name = "btnChangeMode";
            this.btnChangeMode.Size = new System.Drawing.Size(150, 34);
            this.btnChangeMode.TabIndex = 1;
            this.btnChangeMode.Text = "Chế độ gọn";
            this.btnChangeMode.UseVisualStyleBackColor = true;
            this.btnChangeMode.Click += new System.EventHandler(this.btnChangeMode_Click);
            //
            // labelMain
            //
            this.labelMain.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMain.AutoSize = true;
            this.labelMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelMain.Location = new System.Drawing.Point(73, 10);
            this.labelMain.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(394, 29);
            this.labelMain.TabIndex = 0;
            this.labelMain.Text = "Hẹn Giờ Tắt Máy Tính - Chế Độ Đầy Đủ";
            this.labelMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pictureBox1
            //
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Location = new System.Drawing.Point(10, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            //
            // grpQuickShutdown
            //
            this.grpQuickShutdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpQuickShutdown.Controls.Add(this.btn5minutes);
            this.grpQuickShutdown.Controls.Add(this.btn10minutes);
            this.grpQuickShutdown.Controls.Add(this.btn20minutes);
            this.grpQuickShutdown.Controls.Add(this.btn30minutes);
            this.grpQuickShutdown.Controls.Add(this.btn1Hour);
            this.grpQuickShutdown.Controls.Add(this.btn2Hours);
            this.grpQuickShutdown.Controls.Add(this.btn3Hours);
            this.grpQuickShutdown.Controls.Add(this.btn5Hours);
            this.grpQuickShutdown.Location = new System.Drawing.Point(15, 60);
            this.grpQuickShutdown.Name = "grpQuickShutdown";
            this.grpQuickShutdown.Size = new System.Drawing.Size(635, 120);
            this.grpQuickShutdown.TabIndex = 1;
            this.grpQuickShutdown.TabStop = false;
            this.grpQuickShutdown.Text = "Hẹn giờ nhanh";
            //
            // grpCustomShutdown
            //
            this.grpCustomShutdown.Controls.Add(this.customTimeLayout);
            this.grpCustomShutdown.Location = new System.Drawing.Point(15, 190);
            this.grpCustomShutdown.Name = "grpCustomShutdown";
            // Tăng nhẹ chiều rộng group để chứa cột absolute
            this.grpCustomShutdown.Size = new System.Drawing.Size(380, 150); // Tăng từ 370 -> 380
            this.grpCustomShutdown.TabIndex = 2;
            this.grpCustomShutdown.TabStop = false;
            this.grpCustomShutdown.Text = "Tùy chỉnh thời gian";
            //
            // customTimeLayout
            //
            this.customTimeLayout.ColumnCount = 6;
            this.customTimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()); // Col 0
            this.customTimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()); // Col 1
            this.customTimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()); // Col 2
            this.customTimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()); // Col 3
            this.customTimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()); // Col 4
            // *** Đặt cột 5 thành Absolute với độ rộng cố định ***
            this.customTimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F)); // Cột 5: LBL Second (Rộng 60px)
            // *** ***
            this.customTimeLayout.Controls.Add(this.numericUpDownHour, 0, 0);
            this.customTimeLayout.Controls.Add(this.lblHour, 1, 0);
            this.customTimeLayout.Controls.Add(this.numericUpDownMinute, 2, 0);
            this.customTimeLayout.Controls.Add(this.lblMinute, 3, 0);
            this.customTimeLayout.Controls.Add(this.numericUpDownSecond, 4, 0);
            this.customTimeLayout.Controls.Add(this.lblSecond, 5, 0);
            this.customTimeLayout.Controls.Add(this.btnSelfSetup, 0, 1);
            this.customTimeLayout.Controls.Add(this.btnReset, 3, 1);
            this.customTimeLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTimeLayout.Location = new System.Drawing.Point(3, 27);
            this.customTimeLayout.Name = "customTimeLayout";
            this.customTimeLayout.RowCount = 2;
            this.customTimeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.customTimeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.customTimeLayout.Size = new System.Drawing.Size(374, 120); // Kích thước bên trong group
            this.customTimeLayout.TabIndex = 0;
            //
            // grpActions
            //
            this.grpActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpActions.Controls.Add(this.btnCancel);
            this.grpActions.Controls.Add(this.btnSleep);
            this.grpActions.Location = new System.Drawing.Point(400, 190); // Giữ nguyên hoặc điều chỉnh X nếu grpCustom rộng ra
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(250, 150);
            this.grpActions.TabIndex = 3;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Hành động khác";
            //
            // topPanelLayout
            //
            this.topPanelLayout.ColumnCount = 3;
            this.topPanelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.topPanelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topPanelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.topPanelLayout.Controls.Add(this.pictureBox1, 0, 0);
            this.topPanelLayout.Controls.Add(this.labelMain, 1, 0);
            this.topPanelLayout.Controls.Add(this.btnChangeMode, 2, 0);
            this.topPanelLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanelLayout.Location = new System.Drawing.Point(0, 0);
            this.topPanelLayout.Name = "topPanelLayout";
            this.topPanelLayout.RowCount = 1;
            this.topPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topPanelLayout.Size = new System.Drawing.Size(670, 50);
            this.topPanelLayout.TabIndex = 0;
            //
            // FullModeControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.topPanelLayout);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.grpCustomShutdown);
            this.Controls.Add(this.grpQuickShutdown);
            this.MinimumSize = new System.Drawing.Size(670, 360);
            this.Name = "FullModeControl";
            this.Size = new System.Drawing.Size(670, 460);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpQuickShutdown.ResumeLayout(false);
            this.customTimeLayout.ResumeLayout(false);
            this.customTimeLayout.PerformLayout();
            this.grpCustomShutdown.ResumeLayout(false);
            this.grpActions.ResumeLayout(false);
            this.topPanelLayout.ResumeLayout(false);
            this.topPanelLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn5minutes;
        private System.Windows.Forms.Button btn10minutes;
        private System.Windows.Forms.Button btn20minutes;
        private System.Windows.Forms.Button btn30minutes;
        private System.Windows.Forms.Button btn1Hour;
        private System.Windows.Forms.Button btn2Hours;
        private System.Windows.Forms.Button btn3Hours;
        private System.Windows.Forms.Button btn5Hours;
        private System.Windows.Forms.NumericUpDown numericUpDownHour;
        private System.Windows.Forms.NumericUpDown numericUpDownMinute;
        private System.Windows.Forms.NumericUpDown numericUpDownSecond;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Button btnSelfSetup;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSleep;
        private System.Windows.Forms.Button btnChangeMode;
        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox grpQuickShutdown;
        private System.Windows.Forms.GroupBox grpCustomShutdown;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.TableLayoutPanel topPanelLayout;
        private System.Windows.Forms.TableLayoutPanel customTimeLayout;
    }
}