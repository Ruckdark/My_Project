using System.Windows.Forms;

namespace WordVaultAppMVC.Views
{
    partial class AddTopicForm
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContentArea;
        private System.Windows.Forms.Panel pnlFooter;

        // Controls
        private System.Windows.Forms.Label lblAddTopicTitle;
        private System.Windows.Forms.TextBox txtTopicName;
        private System.Windows.Forms.Button btnAddTopic;
        private System.Windows.Forms.Label lblTopicName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtNewVocabulary = new TextBox();
            this.lstVocabulary = new ListBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblAddTopicTitle = new System.Windows.Forms.Label();
            this.pnlContentArea = new System.Windows.Forms.Panel();
            this.lblTopicName = new System.Windows.Forms.Label();
            this.txtTopicName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAddTopic = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlContentArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlHeader.Controls.Add(this.lblAddTopicTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(534, 60);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblAddTopicTitle
            // 
            this.lblAddTopicTitle.AutoSize = true;
            this.lblAddTopicTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAddTopicTitle.ForeColor = System.Drawing.Color.White;
            this.lblAddTopicTitle.Location = new System.Drawing.Point(20, 15);
            this.lblAddTopicTitle.Name = "lblAddTopicTitle";
            this.lblAddTopicTitle.Size = new System.Drawing.Size(483, 45);
            this.lblAddTopicTitle.TabIndex = 0;
            this.lblAddTopicTitle.Text = "📝 Thêm Chủ Đề Từ Vựng Mới";
            // 
            // pnlContentArea
            // 
            this.pnlContentArea.Controls.Add(this.lblTopicName);
            this.pnlContentArea.Controls.Add(this.txtTopicName);
            this.pnlContentArea.Controls.Add(this.lblDescription);
            this.pnlContentArea.Controls.Add(this.txtDescription);
            this.pnlContentArea.Controls.Add(this.btnAddTopic);
            this.pnlContentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContentArea.Location = new System.Drawing.Point(0, 60);
            this.pnlContentArea.Name = "pnlContentArea";
            this.pnlContentArea.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContentArea.Size = new System.Drawing.Size(534, 241);
            this.pnlContentArea.TabIndex = 1;
            // 
            // lblTopicName
            // 
            this.lblTopicName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTopicName.Location = new System.Drawing.Point(0, 20);
            this.lblTopicName.Name = "lblTopicName";
            this.lblTopicName.Size = new System.Drawing.Size(400, 25);
            this.lblTopicName.TabIndex = 0;
            this.lblTopicName.Text = "Tên chủ đề:";
            // 
            // txtTopicName
            // 
            this.txtTopicName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTopicName.Location = new System.Drawing.Point(0, 50);
            this.txtTopicName.Name = "txtTopicName";
            this.txtTopicName.Size = new System.Drawing.Size(400, 37);
            this.txtTopicName.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDescription.Location = new System.Drawing.Point(0, 90);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(400, 25);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Mô tả: ";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDescription.Location = new System.Drawing.Point(0, 120);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(400, 50);
            this.txtDescription.TabIndex = 3;
            // 
            // btnAddTopic
            // 
            this.btnAddTopic.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAddTopic.Location = new System.Drawing.Point(0, 180);
            this.btnAddTopic.Name = "btnAddTopic";
            this.btnAddTopic.Size = new System.Drawing.Size(140, 45);
            this.btnAddTopic.TabIndex = 4;
            this.btnAddTopic.Text = "Thêm Chủ Đề";
            this.btnAddTopic.Click += new System.EventHandler(this.btnAddTopic_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 301);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(534, 30);
            this.pnlFooter.TabIndex = 3;
            // 
            // AddTopicForm
            // 
            this.ClientSize = new System.Drawing.Size(534, 331);
            this.Controls.Add(this.pnlContentArea);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "AddTopicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Chủ Đề Từ Vựng";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContentArea.ResumeLayout(false);
            this.pnlContentArea.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
