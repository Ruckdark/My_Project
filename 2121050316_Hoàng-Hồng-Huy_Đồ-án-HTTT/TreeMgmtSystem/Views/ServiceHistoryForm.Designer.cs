using System.Windows.Forms;

namespace TreeMgmtSystem.Views
{
    partial class ServiceHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvServiceHistory;
        private System.Windows.Forms.Label lblServiceHistory;
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
            this.dgvServiceHistory = new System.Windows.Forms.DataGridView();
            this.lblServiceHistory = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvServiceHistory
            // 
            this.dgvServiceHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceHistory.Location = new System.Drawing.Point(12, 50);
            this.dgvServiceHistory.Name = "dgvServiceHistory";
            this.dgvServiceHistory.RowHeadersWidth = 62;
            this.dgvServiceHistory.RowTemplate.Height = 28;
            this.dgvServiceHistory.Size = new System.Drawing.Size(760, 400);
            this.dgvServiceHistory.TabIndex = 0;
            this.dgvServiceHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // 
            // lblServiceHistory
            // 
            this.lblServiceHistory.AutoSize = true;
            this.lblServiceHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceHistory.Location = new System.Drawing.Point(300, 10);
            this.lblServiceHistory.Name = "lblServiceHistory";
            this.lblServiceHistory.Size = new System.Drawing.Size(211, 32);
            this.lblServiceHistory.TabIndex = 1;
            this.lblServiceHistory.Text = "Lịch Sử Dịch Vụ";
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
            // ServiceHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblServiceHistory);
            this.Controls.Add(this.dgvServiceHistory);
            this.Name = "ServiceHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch Sử Dịch Vụ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
