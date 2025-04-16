namespace WordVaultAppMVC.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlSearchArea;
        private System.Windows.Forms.Panel pnlResultArea;
        private System.Windows.Forms.Panel pnlFooter;

        // Controls
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblPronunciation;
        private System.Windows.Forms.Label lblMeaning;
        private System.Windows.Forms.Button btnPlayAudio;
        private System.Windows.Forms.Label lblAppTitle;

        // ToolStrip
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnHome;
        private System.Windows.Forms.ToolStripButton btnTopicVocabulary;
        private System.Windows.Forms.ToolStripButton btnSettings;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnHome = new System.Windows.Forms.ToolStripButton();
            this.btnTopicVocabulary = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.pnlSearchArea = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlResultArea = new System.Windows.Forms.Panel();
            this.lblPronunciation = new System.Windows.Forms.Label();
            this.lblMeaning = new System.Windows.Forms.Label();
            this.btnPlayAudio = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.pnlSearchArea.SuspendLayout();
            this.pnlResultArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.toolStrip);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(519, 90);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(20, 50);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(496, 45);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "📘 WordVault - Từ điển cá nhân";
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHome,
            this.btnTopicVocabulary,
            this.btnSettings});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(519, 38);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip";
            // 
            // btnHome
            // 
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(65, 29);
            this.btnHome.Text = "Home";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnTopicVocabulary
            // 
            this.btnTopicVocabulary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnTopicVocabulary.Name = "btnTopicVocabulary";
            this.btnTopicVocabulary.Size = new System.Drawing.Size(149, 29);
            this.btnTopicVocabulary.Text = "Topic Vocabulary";
            this.btnTopicVocabulary.Click += new System.EventHandler(this.btnTopicVocabulary_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(80, 29);
            this.btnSettings.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pnlSearchArea
            // 
            this.pnlSearchArea.Controls.Add(this.txtSearch);
            this.pnlSearchArea.Controls.Add(this.btnSearch);
            this.pnlSearchArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchArea.Location = new System.Drawing.Point(0, 90);
            this.pnlSearchArea.Name = "pnlSearchArea";
            this.pnlSearchArea.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlSearchArea.Size = new System.Drawing.Size(519, 60);
            this.pnlSearchArea.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(0, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 37);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSearch.Location = new System.Drawing.Point(320, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(106, 37);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlResultArea
            // 
            this.pnlResultArea.Controls.Add(this.lblPronunciation);
            this.pnlResultArea.Controls.Add(this.lblMeaning);
            this.pnlResultArea.Controls.Add(this.btnPlayAudio);
            this.pnlResultArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultArea.Location = new System.Drawing.Point(0, 150);
            this.pnlResultArea.Name = "pnlResultArea";
            this.pnlResultArea.Padding = new System.Windows.Forms.Padding(20);
            this.pnlResultArea.Size = new System.Drawing.Size(519, 191);
            this.pnlResultArea.TabIndex = 0;
            // 
            // lblPronunciation
            // 
            this.lblPronunciation.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPronunciation.Location = new System.Drawing.Point(0, 10);
            this.lblPronunciation.Name = "lblPronunciation";
            this.lblPronunciation.Size = new System.Drawing.Size(400, 25);
            this.lblPronunciation.TabIndex = 0;
            this.lblPronunciation.Text = "Phát âm: ";
            // 
            // lblMeaning
            // 
            this.lblMeaning.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMeaning.Location = new System.Drawing.Point(0, 50);
            this.lblMeaning.Name = "lblMeaning";
            this.lblMeaning.Size = new System.Drawing.Size(400, 80);
            this.lblMeaning.TabIndex = 1;
            this.lblMeaning.Text = "Nghĩa tiếng Việt: ";
            // 
            // btnPlayAudio
            // 
            this.btnPlayAudio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPlayAudio.Location = new System.Drawing.Point(5, 140);
            this.btnPlayAudio.Name = "btnPlayAudio";
            this.btnPlayAudio.Size = new System.Drawing.Size(140, 45);
            this.btnPlayAudio.TabIndex = 2;
            this.btnPlayAudio.Text = "🔊 Nghe phát âm";
            this.btnPlayAudio.Click += new System.EventHandler(this.btnPlayAudio_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 341);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(519, 30);
            this.pnlFooter.TabIndex = 3;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(519, 371);
            this.Controls.Add(this.pnlResultArea);
            this.Controls.Add(this.pnlSearchArea);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WordVault - English Vocabulary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.pnlSearchArea.ResumeLayout(false);
            this.pnlSearchArea.PerformLayout();
            this.pnlResultArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
