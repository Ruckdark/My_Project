// File: SettingsControl.cs (Thiết kế mới)
using System;
using System.Windows.Forms;
using System.Drawing;
using WordVaultAppMVC.Properties; // <<< Quan trọng: Using để truy cập Settings
using WordVaultAppMVC.Data;     // <<< Thêm nếu cần gọi Repository cho Clear History
using System.Diagnostics;
using WordVaultAppMVC.Services;        // <<< Cho Debug

namespace WordVaultAppMVC.Views.Controls
{
    // Đảm bảo là partial class nếu bạn tách file Designer
    public partial class SettingsControl : UserControl
    {
        // --- Khai báo UI Controls ---
        private TableLayoutPanel mainLayout;
        private GroupBox gbLearning;
        private Label lblReviewCount;
        private NumericUpDown nudReviewCount;
        private Label lblQuizCount;
        private NumericUpDown nudQuizCount;
        private CheckBox chkAutoPlayAudio;
        private GroupBox gbData;
        private Button btnBackup;
        private Button btnRestore;
        private Button btnClearHistory;
        private Button btnSaveSettings;

        // Có thể thêm các Repository nếu cần cho Clear History
        // private readonly LearningStatusRepository _learningStatusRepo;

        public SettingsControl()
        {
            // _learningStatusRepo = new LearningStatusRepository(); // Khởi tạo nếu dùng
            InitializeComponent();
            LoadSettings(); // Tải cài đặt khi control được tạo
        }

        private void InitializeComponent()
        {
            // --- Khởi tạo Controls ---
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.gbLearning = new System.Windows.Forms.GroupBox();
            this.lblReviewCount = new System.Windows.Forms.Label();
            this.nudReviewCount = new System.Windows.Forms.NumericUpDown();
            this.lblQuizCount = new System.Windows.Forms.Label();
            this.nudQuizCount = new System.Windows.Forms.NumericUpDown();
            this.chkAutoPlayAudio = new System.Windows.Forms.CheckBox();
            this.gbData = new System.Windows.Forms.GroupBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();

            // --- Suspend Layout ---
            this.mainLayout.SuspendLayout();
            this.gbLearning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReviewCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuizCount)).BeginInit();
            this.gbData.SuspendLayout();
            this.SuspendLayout();

            //
            // mainLayout
            //
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.gbLearning, 0, 0);
            this.mainLayout.Controls.Add(this.gbData, 0, 1);
            this.mainLayout.Controls.Add(this.btnSaveSettings, 0, 2); // Đặt nút Save ở hàng cuối
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(20); // Tăng Padding
            this.mainLayout.RowCount = 3; // 3 hàng chính
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng 0: gbLearning
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng 1: gbData
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng 2: Co giãn để đẩy nút Save xuống
            this.mainLayout.Size = new System.Drawing.Size(550, 450); // Size ví dụ
            this.mainLayout.TabIndex = 0;

            //
            // gbLearning
            //
            this.gbLearning.AutoSize = true;
            this.gbLearning.Controls.Add(this.lblReviewCount);
            this.gbLearning.Controls.Add(this.nudReviewCount);
            this.gbLearning.Controls.Add(this.lblQuizCount);
            this.gbLearning.Controls.Add(this.nudQuizCount);
            this.gbLearning.Controls.Add(this.chkAutoPlayAudio);
            this.gbLearning.Dock = System.Windows.Forms.DockStyle.Fill; // Fill hàng 0
            this.gbLearning.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbLearning.Location = new System.Drawing.Point(23, 23);
            this.gbLearning.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15); // Tăng Margin dưới
            this.gbLearning.Name = "gbLearning";
            this.gbLearning.Padding = new System.Windows.Forms.Padding(10, 10, 10, 15);
            this.gbLearning.Size = new System.Drawing.Size(504, 160); // Size ví dụ
            this.gbLearning.TabIndex = 0;
            this.gbLearning.TabStop = false;
            this.gbLearning.Text = "Học tập && Ôn tập";

            //
            // lblReviewCount
            //
            this.lblReviewCount.AutoSize = true;
            this.lblReviewCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReviewCount.Location = new System.Drawing.Point(15, 40); // Điều chỉnh vị trí Y
            this.lblReviewCount.Name = "lblReviewCount";
            this.lblReviewCount.Size = new System.Drawing.Size(169, 20);
            this.lblReviewCount.TabIndex = 0;
            this.lblReviewCount.Text = "Số từ ôn tập mặc định:";

            //
            // nudReviewCount
            //
            this.nudReviewCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudReviewCount.Location = new System.Drawing.Point(210, 38); // Điều chỉnh vị trí Y
            this.nudReviewCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.nudReviewCount.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudReviewCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudReviewCount.Name = "nudReviewCount";
            this.nudReviewCount.Size = new System.Drawing.Size(80, 27); // Tăng Width
            this.nudReviewCount.TabIndex = 1;
            this.nudReviewCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudReviewCount.Value = new decimal(new int[] { 10, 0, 0, 0 });

            //
            // lblQuizCount
            //
            this.lblQuizCount.AutoSize = true;
            this.lblQuizCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuizCount.Location = new System.Drawing.Point(15, 80); // Điều chỉnh vị trí Y
            this.lblQuizCount.Name = "lblQuizCount";
            this.lblQuizCount.Size = new System.Drawing.Size(158, 20);
            this.lblQuizCount.TabIndex = 2;
            this.lblQuizCount.Text = "Số câu Quiz mặc định:";

            //
            // nudQuizCount
            //
            this.nudQuizCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudQuizCount.Location = new System.Drawing.Point(210, 78); // Điều chỉnh vị trí Y
            this.nudQuizCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.nudQuizCount.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudQuizCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuizCount.Name = "nudQuizCount";
            this.nudQuizCount.Size = new System.Drawing.Size(80, 27); // Tăng Width
            this.nudQuizCount.TabIndex = 3;
            this.nudQuizCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudQuizCount.Value = new decimal(new int[] { 20, 0, 0, 0 });

            //
            // chkAutoPlayAudio
            //
            this.chkAutoPlayAudio.AutoSize = true;
            this.chkAutoPlayAudio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAutoPlayAudio.Location = new System.Drawing.Point(15, 120); // Điều chỉnh vị trí Y
            this.chkAutoPlayAudio.Name = "chkAutoPlayAudio";
            this.chkAutoPlayAudio.Size = new System.Drawing.Size(250, 24);
            this.chkAutoPlayAudio.TabIndex = 4;
            this.chkAutoPlayAudio.Text = "Tự động phát âm thanh khi xem từ";
            this.chkAutoPlayAudio.UseVisualStyleBackColor = true;

            //
            // gbData
            //
            this.gbData.AutoSize = true;
            this.gbData.Controls.Add(this.btnBackup);
            this.gbData.Controls.Add(this.btnRestore);
            this.gbData.Controls.Add(this.btnClearHistory);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill; // Fill hàng 1
            this.gbData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbData.Location = new System.Drawing.Point(23, 203); // Vị trí Y sẽ tự động
            this.gbData.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15); // Tăng Margin dưới
            this.gbData.Name = "gbData";
            this.gbData.Padding = new System.Windows.Forms.Padding(10);
            this.gbData.Size = new System.Drawing.Size(504, 135); // Size ví dụ
            this.gbData.TabIndex = 1;
            this.gbData.TabStop = false;
            this.gbData.Text = "Quản lý Dữ liệu";

            //
            // btnBackup
            //
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBackup.Location = new System.Drawing.Point(15, 35);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(160, 35); // Tăng Width
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "📁 Sao lưu Dữ liệu...";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; // Căn trái text
            this.btnBackup.TextImageRelation = TextImageRelation.ImageBeforeText; // Icon trước text
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += BtnBackup_Click;

            //
            // btnRestore
            //
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRestore.Location = new System.Drawing.Point(190, 35); // Điều chỉnh X
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(160, 35); // Tăng Width
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "🔄 Phục hồi Dữ liệu...";
            this.btnRestore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestore.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += BtnRestore_Click;

            //
            // btnClearHistory
            //
            this.btnClearHistory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearHistory.ForeColor = System.Drawing.Color.DarkRed;
            this.btnClearHistory.Location = new System.Drawing.Point(15, 80); // Xuống hàng mới
            this.btnClearHistory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(220, 35); // Tăng Width
            this.btnClearHistory.TabIndex = 2;
            this.btnClearHistory.Text = "❌ Xóa Trạng thái Học tập...";
            this.btnClearHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearHistory.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += BtnClearHistory_Click;

            //
            // btnSaveSettings
            //
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))); // Neo góc dưới phải
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveSettings.Location = new System.Drawing.Point(357, 392); // Vị trí sẽ tự điều chỉnh
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(160, 40); // Tăng Width
            this.btnSaveSettings.TabIndex = 2; // TabIndex cho hàng cuối
            this.btnSaveSettings.Text = "💾 Lưu Cài đặt";
            this.btnSaveSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += BtnSaveSettings_Click;

            //
            // SettingsControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight; // Đặt nền trắng
            this.Controls.Add(this.mainLayout);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(550, 450);

            // --- Resume Layout ---
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.gbLearning.ResumeLayout(false);
            this.gbLearning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReviewCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuizCount)).EndInit();
            this.gbData.ResumeLayout(false);
            // PerformLayout không cần cho gbData nếu nút đặt thủ công
            this.ResumeLayout(false);

        }

        #region Load/Save Settings and Event Handlers

        private void LoadSettings()
        {
            try
            {
                // Đọc từ Properties.Settings (cần tạo trước trong Project Properties)
                nudReviewCount.Value = Math.Max(nudReviewCount.Minimum, Math.Min(nudReviewCount.Maximum, Settings.Default.DefaultReviewWordCount));
                nudQuizCount.Value = Math.Max(nudQuizCount.Minimum, Math.Min(nudQuizCount.Maximum, Settings.Default.DefaultQuizQuestionCount));
                chkAutoPlayAudio.Checked = Settings.Default.AutoPlayAudio;
                Debug.WriteLine("[SettingsControl] Settings loaded successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[SettingsControl] Error loading settings: {ex.Message}. Using defaults.");
                MessageBox.Show($"Lỗi khi tải cài đặt: {ex.Message}\nSẽ sử dụng giá trị mặc định.", "Lỗi Cài đặt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Gán giá trị mặc định cứng nếu đọc lỗi
                nudReviewCount.Value = 10;
                nudQuizCount.Value = 20;
                chkAutoPlayAudio.Checked = false;
            }
        }

        private void SaveSettings()
        {
            try
            {
                Debug.WriteLine("[SettingsControl] Attempting to save settings...");
                // Lưu vào Properties.Settings
                Settings.Default.DefaultReviewWordCount = (int)nudReviewCount.Value;
                Settings.Default.DefaultQuizQuestionCount = (int)nudQuizCount.Value;
                Settings.Default.AutoPlayAudio = chkAutoPlayAudio.Checked;

                Settings.Default.Save(); // <<< Quan trọng: Lưu thay đổi vào file config
                Debug.WriteLine("[SettingsControl] Settings saved successfully.");
                MessageBox.Show("Đã lưu cài đặt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[SettingsControl] Error saving settings: {ex.Message}");
                MessageBox.Show($"Lỗi khi lưu cài đặt: {ex.Message}", "Lỗi Cài đặt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "SQL Server Backup files (*.bak)|*.bak";
                sfd.Title = "Chọn vị trí lưu bản Sao lưu";
                sfd.FileName = $"WordVaultBackup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string backupPath = sfd.FileName;
                    this.Cursor = Cursors.WaitCursor; // Hiển thị con trỏ chờ
                    try
                    {
                        // Gọi DataService để backup
                        bool success = DataService.BackupDatabase(backupPath);
                        if (success)
                        {
                            MessageBox.Show($"Đã sao lưu cơ sở dữ liệu thành công vào:\n{backupPath}", "Sao lưu Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // Thông báo lỗi đã được hiển thị bên trong DataService
                    }
                    catch (Exception ex) // Bắt lỗi tổng quát nếu DataService ném ra
                    {
                        MessageBox.Show($"Lỗi không mong muốn trong quá trình sao lưu: {ex.Message}", "Lỗi Sao lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default; // Trả con trỏ về bình thường
                    }
                }
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("CẢNH BÁO: Phục hồi dữ liệu sẽ GHI ĐÈ toàn bộ dữ liệu hiện tại và yêu cầu khởi động lại ứng dụng sau khi hoàn tất.\n\nBạn có chắc chắn muốn tiếp tục?",
                                         "Xác nhận Phục hồi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "SQL Server Backup files (*.bak)|*.bak|All files (*.*)|*.*";
                    ofd.Title = "Chọn file Sao lưu để Phục hồi";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string backupPath = ofd.FileName;
                        this.Cursor = Cursors.WaitCursor;
                        try
                        {
                            // Gọi DataService để restore
                            bool success = DataService.RestoreDatabase(backupPath);
                            if (success)
                            {
                                MessageBox.Show("Phục hồi cơ sở dữ liệu thành công! \nỨng dụng cần được khởi động lại để áp dụng thay đổi.", "Phục hồi Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Có thể gợi ý khởi động lại hoặc tự khởi động lại
                                Application.Restart(); // Tự động khởi động lại
                            }
                            // Thông báo lỗi đã được hiển thị bên trong DataService (bao gồm cả việc cố gắng SET MULTI_USER)
                        }
                        catch (Exception ex) // Bắt lỗi tổng quát
                        {
                            MessageBox.Show($"Lỗi không mong muốn trong quá trình phục hồi: {ex.Message}", "Lỗi Phục hồi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                    }
                }
            }
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Hành động này sẽ XÓA TOÀN BỘ trạng thái học tập ('Đã học', 'Đang học') của tất cả các từ và kết quả làm Quiz.\n\nBẠN CÓ CHẮC CHẮN MUỐN TIẾP TỤC?\n(Hành động này không thể hoàn tác!)",
                                        "XÁC NHẬN XÓA DỮ LIỆU HỌC TẬP", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (confirm == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    // Gọi DataService để xóa
                    bool success = DataService.ClearLearningData();
                    if (success)
                    {
                        MessageBox.Show("Đã xóa thành công dữ liệu trạng thái học tập và kết quả quiz.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Có thể cần load lại dữ liệu ở các màn hình khác nếu chúng đang mở
                    }
                    // Thông báo lỗi đã được hiển thị bên trong DataService
                }
                catch (Exception ex) // Bắt lỗi tổng quát
                {
                    MessageBox.Show($"Lỗi không mong muốn khi xóa dữ liệu học tập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        #endregion

    } // Kết thúc class SettingsControl

    // Partial class nếu bạn tách file Designer
    public partial class SettingsControl { }

} // Kết thúc namespace