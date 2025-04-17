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
        private System.Windows.Forms.ToolStripButton btnFavorite;
        private System.Windows.Forms.ToolStripButton btnDailyReview;
        private System.Windows.Forms.ToolStripButton btnQuiz;
        private System.Windows.Forms.ToolStripButton btnShuffle;

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
            this.btnFavorite = new System.Windows.Forms.ToolStripButton();
            this.btnDailyReview = new System.Windows.Forms.ToolStripButton();
            this.btnQuiz = new System.Windows.Forms.ToolStripButton();
            this.btnShuffle = new System.Windows.Forms.ToolStripButton();

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

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.toolStrip);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Size = new System.Drawing.Size(519, 90);

            // lblAppTitle
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(20, 50);
            this.lblAppTitle.Text = "📘 WordVault - Từ điển cá nhân";

            // toolStrip
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Size = new System.Drawing.Size(519, 38);

            // ToolStrip Buttons
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHome.Text = "Home";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);

            this.btnTopicVocabulary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnTopicVocabulary.Text = "Topic Vocabulary";
            this.btnTopicVocabulary.Click += new System.EventHandler(this.btnTopicVocabulary_Click);

            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSettings.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            this.btnFavorite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFavorite.Text = "⭐ Yêu thích";
            this.btnFavorite.Click += new System.EventHandler(this.btnFavorite_Click);

            this.btnDailyReview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDailyReview.Text = "📅 Học từ";
            this.btnDailyReview.Click += new System.EventHandler(this.btnDailyReview_Click);

            this.btnQuiz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnQuiz.Text = "🧠 Quiz";
            this.btnQuiz.Click += new System.EventHandler(this.btnQuiz_Click);

            this.btnShuffle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnShuffle.Text = "🔀 Xáo từ";
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);

            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnHome,
                this.btnTopicVocabulary,
                this.btnSettings,
                this.btnFavorite,
                this.btnDailyReview,
                this.btnQuiz,
                this.btnShuffle
            });

            // pnlSearchArea
            this.pnlSearchArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchArea.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlSearchArea.Size = new System.Drawing.Size(519, 60);
            this.pnlSearchArea.Controls.Add(this.txtSearch);
            this.pnlSearchArea.Controls.Add(this.btnSearch);

            // txtSearch
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Size = new System.Drawing.Size(300, 37);
            this.txtSearch.Location = new System.Drawing.Point(0, 15);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);

            // btnSearch
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSearch.Size = new System.Drawing.Size(106, 37);
            this.btnSearch.Location = new System.Drawing.Point(320, 15);
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // pnlResultArea
            this.pnlResultArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultArea.Padding = new System.Windows.Forms.Padding(20);
            this.pnlResultArea.Controls.Add(this.lblPronunciation);
            this.pnlResultArea.Controls.Add(this.lblMeaning);
            this.pnlResultArea.Controls.Add(this.btnPlayAudio);

            // lblPronunciation
            this.lblPronunciation.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPronunciation.Location = new System.Drawing.Point(0, 10);
            this.lblPronunciation.Size = new System.Drawing.Size(400, 25);
            this.lblPronunciation.Text = "Phát âm: ";

            // lblMeaning
            this.lblMeaning.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMeaning.Location = new System.Drawing.Point(0, 50);
            this.lblMeaning.Size = new System.Drawing.Size(400, 80);
            this.lblMeaning.Text = "Nghĩa tiếng Việt: ";

            // btnPlayAudio
            this.btnPlayAudio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPlayAudio.Size = new System.Drawing.Size(140, 45);
            this.btnPlayAudio.Location = new System.Drawing.Point(5, 140);
            this.btnPlayAudio.Text = "🔊 Nghe phát âm";
            this.btnPlayAudio.Click += new System.EventHandler(this.btnPlayAudio_Click);

            // pnlFooter
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Size = new System.Drawing.Size(519, 30);

            // MainForm
            this.ClientSize = new System.Drawing.Size(519, 371);
            this.Controls.Add(this.pnlResultArea);
            this.Controls.Add(this.pnlSearchArea);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
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
