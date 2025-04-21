namespace WindowShutdown
{
    partial class NeatModeControl
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
            components = new System.ComponentModel.Container();
            numericUpDownHour = new NumericUpDown();
            numericUpDownMinute = new NumericUpDown();
            numericUpDownSecond = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnRestart = new Button();
            btnShutDown = new Button();
            btnCancel = new Button();
            btnChangeMode = new Button();
            labelMain = new Label();
            pictureBox1 = new PictureBox();
            toolTip = new ToolTip(components);
            grpTimerInput = new GroupBox();
            neatTimeLayout = new TableLayoutPanel();
            grpCommands = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSecond).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            grpTimerInput.SuspendLayout();
            neatTimeLayout.SuspendLayout();
            grpCommands.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDownHour
            // 
            numericUpDownHour.Anchor = AnchorStyles.Left;
            numericUpDownHour.Location = new Point(3, 9);
            numericUpDownHour.Margin = new Padding(3, 3, 0, 3);
            numericUpDownHour.Name = "numericUpDownHour";
            numericUpDownHour.Size = new Size(67, 31);
            numericUpDownHour.TabIndex = 0;
            // 
            // numericUpDownMinute
            // 
            numericUpDownMinute.Anchor = AnchorStyles.Left;
            numericUpDownMinute.Location = new Point(121, 9);
            numericUpDownMinute.Margin = new Padding(3, 3, 0, 3);
            numericUpDownMinute.Name = "numericUpDownMinute";
            numericUpDownMinute.Size = new Size(67, 31);
            numericUpDownMinute.TabIndex = 2;
            // 
            // numericUpDownSecond
            // 
            numericUpDownSecond.Anchor = AnchorStyles.Left;
            numericUpDownSecond.Location = new Point(250, 9);
            numericUpDownSecond.Margin = new Padding(3, 3, 0, 3);
            numericUpDownSecond.Name = "numericUpDownSecond";
            numericUpDownSecond.Size = new Size(67, 31);
            numericUpDownSecond.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(70, 12);
            label1.Margin = new Padding(0, 0, 10, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 25);
            label1.TabIndex = 1;
            label1.Text = "giờ";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(188, 12);
            label2.Margin = new Padding(0, 0, 10, 0);
            label2.Name = "label2";
            label2.Size = new Size(49, 25);
            label2.TabIndex = 3;
            label2.Text = "phút";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(317, 12);
            label3.Margin = new Padding(0, 0, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(45, 25);
            label3.TabIndex = 5;
            label3.Text = "giây";
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(175, 30);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(140, 34);
            btnRestart.TabIndex = 1;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // btnShutDown
            // 
            btnShutDown.Location = new Point(15, 30);
            btnShutDown.Name = "btnShutDown";
            btnShutDown.Size = new Size(150, 34);
            btnShutDown.TabIndex = 0;
            btnShutDown.Text = "Shutdown";
            btnShutDown.UseVisualStyleBackColor = true;
            btnShutDown.Click += btnShutDown_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(325, 30);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 34);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Hủy lệnh";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnChangeMode
            // 
            btnChangeMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChangeMode.Location = new Point(360, 10);
            btnChangeMode.Name = "btnChangeMode";
            btnChangeMode.Size = new Size(150, 34);
            btnChangeMode.TabIndex = 3;
            btnChangeMode.Text = "Chế độ đầy đủ";
            btnChangeMode.UseVisualStyleBackColor = true;
            btnChangeMode.Click += btnChangeMode_Click;
            // 
            // labelMain
            // 
            labelMain.AutoSize = true;
            labelMain.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelMain.Location = new Point(70, 12);
            labelMain.Name = "labelMain";
            labelMain.Size = new Size(422, 32);
            labelMain.TabIndex = 0;
            labelMain.Text = "Hẹn Giờ Tắt Máy Tính - Chế Độ Gọn";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(15, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 29;
            pictureBox1.TabStop = false;
            // 
            // grpTimerInput
            // 
            grpTimerInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTimerInput.Controls.Add(neatTimeLayout);
            grpTimerInput.Location = new Point(15, 60);
            grpTimerInput.Name = "grpTimerInput";
            grpTimerInput.Size = new Size(495, 80);
            grpTimerInput.TabIndex = 1;
            grpTimerInput.TabStop = false;
            grpTimerInput.Text = "Đặt thời gian hẹn giờ";
            // 
            // neatTimeLayout
            // 
            neatTimeLayout.ColumnCount = 6;
            neatTimeLayout.ColumnStyles.Add(new ColumnStyle());
            neatTimeLayout.ColumnStyles.Add(new ColumnStyle());
            neatTimeLayout.ColumnStyles.Add(new ColumnStyle());
            neatTimeLayout.ColumnStyles.Add(new ColumnStyle());
            neatTimeLayout.ColumnStyles.Add(new ColumnStyle());
            neatTimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            neatTimeLayout.Controls.Add(numericUpDownHour, 0, 0);
            neatTimeLayout.Controls.Add(label1, 1, 0);
            neatTimeLayout.Controls.Add(numericUpDownMinute, 2, 0);
            neatTimeLayout.Controls.Add(label2, 3, 0);
            neatTimeLayout.Controls.Add(numericUpDownSecond, 4, 0);
            neatTimeLayout.Controls.Add(label3, 5, 0);
            neatTimeLayout.Dock = DockStyle.Fill;
            neatTimeLayout.Location = new Point(3, 27);
            neatTimeLayout.Name = "neatTimeLayout";
            neatTimeLayout.RowCount = 1;
            neatTimeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            neatTimeLayout.Size = new Size(489, 50);
            neatTimeLayout.TabIndex = 0;
            // 
            // grpCommands
            // 
            grpCommands.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpCommands.Controls.Add(btnShutDown);
            grpCommands.Controls.Add(btnRestart);
            grpCommands.Controls.Add(btnCancel);
            grpCommands.Location = new Point(15, 150);
            grpCommands.Name = "grpCommands";
            grpCommands.Size = new Size(495, 80);
            grpCommands.TabIndex = 2;
            grpCommands.TabStop = false;
            grpCommands.Text = "Hành động";
            // 
            // NeatModeControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpCommands);
            Controls.Add(grpTimerInput);
            Controls.Add(btnChangeMode);
            Controls.Add(labelMain);
            Controls.Add(pictureBox1);
            MinimumSize = new Size(530, 250);
            Name = "NeatModeControl";
            Size = new Size(530, 250);
            ((System.ComponentModel.ISupportInitialize)numericUpDownHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinute).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSecond).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            grpTimerInput.ResumeLayout(false);
            neatTimeLayout.ResumeLayout(false);
            neatTimeLayout.PerformLayout();
            grpCommands.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownHour;
        private System.Windows.Forms.NumericUpDown numericUpDownMinute;
        private System.Windows.Forms.NumericUpDown numericUpDownSecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnShutDown;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChangeMode;
        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox grpTimerInput;
        private System.Windows.Forms.GroupBox grpCommands;
        private System.Windows.Forms.TableLayoutPanel neatTimeLayout;
    }
}