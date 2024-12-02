using System.Windows.Forms;

namespace TreeMgmtSystem
{
    partial class TreeHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvTreeHistory;
        private System.Windows.Forms.Label lblTreeHistory;
        private System.Windows.Forms.Button btnClose;

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
            this.dgvTreeHistory = new System.Windows.Forms.DataGridView();
            this.lblTreeHistory = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreeHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTreeHistory
            // 
            this.dgvTreeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTreeHistory.Location = new System.Drawing.Point(12, 50);
            this.dgvTreeHistory.Name = "dgvTreeHistory";
            this.dgvTreeHistory.RowHeadersWidth = 62;
            this.dgvTreeHistory.RowTemplate.Height = 28;
            this.dgvTreeHistory.Size = new System.Drawing.Size(760, 400);
            this.dgvTreeHistory.TabIndex = 0;
            this.dgvTreeHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // lblTreeHistory
            // 
            this.lblTreeHistory.AutoSize = true;
            this.lblTreeHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreeHistory.Location = new System.Drawing.Point(300, 10);
            this.lblTreeHistory.Name = "lblTreeHistory";
            this.lblTreeHistory.Size = new System.Drawing.Size(184, 32);
            this.lblTreeHistory.TabIndex = 1;
            this.lblTreeHistory.Text = "Lịch Sử Cây";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(340, 470);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 50);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTreeHistory);
            this.Controls.Add(this.dgvTreeHistory);
            this.Name = "TreeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi Tiết Cây";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreeHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
