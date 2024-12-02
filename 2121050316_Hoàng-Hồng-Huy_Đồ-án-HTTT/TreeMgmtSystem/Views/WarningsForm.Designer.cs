using System.Windows.Forms;

namespace TreeMgmtSystem
{
    partial class WarningsForm
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
            this.lblWarnings = new System.Windows.Forms.Label();
            this.lblServices = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvWarnings = new System.Windows.Forms.DataGridView();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWarnings
            // 
            this.lblWarnings.AutoSize = true;
            this.lblWarnings.Location = new System.Drawing.Point(12, 9);
            this.lblWarnings.Name = "lblWarnings";
            this.lblWarnings.Size = new System.Drawing.Size(0, 20);
            this.lblWarnings.TabIndex = 0;
            // 
            // lblServices
            // 
            this.lblServices.AutoSize = true;
            this.lblServices.Location = new System.Drawing.Point(12, 200);
            this.lblServices.Name = "lblServices";
            this.lblServices.Size = new System.Drawing.Size(0, 20);
            this.lblServices.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(341, 388);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(107, 50);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgvWarnings
            // 
            this.dgvWarnings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarnings.Location = new System.Drawing.Point(16, 41);
            this.dgvWarnings.Name = "dgvWarnings";
            this.dgvWarnings.RowHeadersWidth = 62;
            this.dgvWarnings.RowTemplate.Height = 28;
            this.dgvWarnings.Size = new System.Drawing.Size(760, 150);
            this.dgvWarnings.TabIndex = 3;
            this.dgvWarnings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // dgvServices
            // 
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServices.Location = new System.Drawing.Point(16, 232);
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.RowHeadersWidth = 62;
            this.dgvServices.RowTemplate.Height = 28;
            this.dgvServices.Size = new System.Drawing.Size(760, 150);
            this.dgvServices.TabIndex = 4;
            this.dgvServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(211, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Các cây có tình trạng không tốt:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(211, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Các dịch vụ chưa hoàn thành:";
            // 
            // WarningsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvServices);
            this.Controls.Add(this.dgvWarnings);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblServices);
            this.Controls.Add(this.lblWarnings);
            this.Name = "WarningsForm";
            this.Text = "WarningsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.Label lblServices;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvWarnings;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
