using System;
using System.Collections.Generic;
using System.Data.SqlClient; // Needed for SQL Exception
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;
using WordVaultAppMVC.Controllers; // Assuming LearningController is here
using WordVaultAppMVC.Helpers;     // Assuming AudioHelper is here

namespace WordVaultAppMVC.Views.Controls
{
    public class DailyReviewControl : UserControl
    {
        // --- UI Controls ---
        private ComboBox cboTopics;
        private NumericUpDown numWordCount;
        private Button btnStart;
        private Label lblWord;
        private Label lblPronunciation;
        private Label lblMeaning;
        private Button btnShowDetails; // Thay thế btnNext ban đầu
        private Button btnPlayAudio;
        private Button btnRemembered;
        private Button btnNotRemembered;
        private Label lblProgress; // Thêm Label tiến độ
        private TableLayoutPanel mainLayout;
        private Panel cardPanel; // Panel mô phỏng flashcard

        // --- Logic Fields ---
        private List<Vocabulary> currentWordList = new List<Vocabulary>(); // Sử dụng Vocabulary model
        private int currentIndex = -1;
        private VocabularyRepository vocabRepo;
        private TopicRepository topicRepo;
        private LearningController learningController;
        private bool topicsLoadedSuccessfully = false;

        public DailyReviewControl()
        {
            // Khởi tạo các thành phần phụ thuộc
            vocabRepo = new VocabularyRepository();
            topicRepo = new TopicRepository();
            learningController = new LearningController();

            InitializeComponent();

            if (!this.DesignMode)
            {
                LoadTopics();
            }
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = SystemColors.Control; // Nền xám nhạt

            // --- Main Layout ---
            mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4, // Header, Card Area, Buttons, Progress
                Padding = new Padding(20)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 0: Header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 1: Card Area
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 2: Buttons
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 3: Progress

            // --- Row 0: Header ---
            cboTopics = new ComboBox { Name = "cboTopics", Anchor = AnchorStyles.Left, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 3, 3) };
            numWordCount = new NumericUpDown { Name = "numWordCount", Width = 80, Minimum = 1, Maximum = 100, Value = 10, Anchor = AnchorStyles.Left, Margin = new Padding(3, 6, 3, 3) }; // Tăng default lên 10
            btnStart = new Button { Name = "btnStart", Text = "Bắt đầu học", Anchor = AnchorStyles.Left, AutoSize = true, BackColor = Color.MediumSeaGreen, ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Margin = new Padding(10, 3, 3, 3) };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += BtnStart_Click;

            var headerPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, AutoSize = true, Padding = new Padding(0, 0, 0, 15) };
            headerPanel.Controls.AddRange(new Control[] { new Label { Text = "Chủ đề:", AutoSize = true, Margin = new Padding(0, 8, 3, 0) }, cboTopics, new Label { Text = "Số từ:", AutoSize = true, Margin = new Padding(10, 8, 3, 0) }, numWordCount, btnStart });
            mainLayout.Controls.Add(headerPanel, 0, 0);

            // --- Row 1: Card Area ---
            cardPanel = new Panel { Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, Padding = new Padding(15) };
            var cardLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 4, AutoSize = true }; // Thêm 1 hàng cho nút Play
            cardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F)); // Word (chiếm nhiều không gian)
            cardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Play Button
            cardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Pronunciation
            cardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Meaning
            cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Cột chính
            cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));      // Cột cho nút Play (nếu đặt cạnh)

            lblWord = new Label { Name = "lblWord", Text = "...", Font = new Font("Segoe UI", 24F, FontStyle.Bold), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            cardLayout.Controls.Add(lblWord, 0, 0); cardLayout.SetColumnSpan(lblWord, 2);

            btnPlayAudio = new Button { Name = "btnPlayAudio", Text = "🔊 Phát âm", FlatStyle = FlatStyle.Flat, AutoSize = true, Visible = false, Anchor = AnchorStyles.None, Margin = new Padding(5) };
            btnPlayAudio.FlatAppearance.BorderSize = 0; btnPlayAudio.Click += BtnPlayAudio_Click;
            cardLayout.Controls.Add(btnPlayAudio, 0, 1); cardLayout.SetColumnSpan(btnPlayAudio, 2);

            lblPronunciation = new Label { Name = "lblPronunciation", Text = "", Font = new Font("Segoe UI", 11F, FontStyle.Italic), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Visible = false, ForeColor = Color.Gray, Padding = new Padding(0, 5, 0, 5) };
            cardLayout.Controls.Add(lblPronunciation, 0, 2); cardLayout.SetColumnSpan(lblPronunciation, 2);

            lblMeaning = new Label { Name = "lblMeaning", Text = "", Font = new Font("Segoe UI", 12F), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Visible = false };
            cardLayout.Controls.Add(lblMeaning, 0, 3); cardLayout.SetColumnSpan(lblMeaning, 2);

            cardPanel.Controls.Add(cardLayout);
            mainLayout.Controls.Add(cardPanel, 0, 1);

            // --- Row 2: Button Area ---
            btnShowDetails = new Button { Name = "btnShowDetails", Text = "🔍 Hiện chi tiết", Width = 120, AutoSize = true, BackColor = Color.LightSkyBlue, Visible = false, Font = new Font("Segoe UI", 9F) };
            btnShowDetails.Click += BtnShowDetails_Click;

            btnRemembered = new Button { Name = "btnRemembered", Text = "✅ Đã nhớ", Width = 100, AutoSize = true, BackColor = Color.PaleGreen, Visible = false, Font = new Font("Segoe UI", 9F) };
            btnRemembered.Click += BtnRemembered_Click;

            btnNotRemembered = new Button { Name = "btnNotRemembered", Text = "❌ Chưa nhớ", Width = 100, AutoSize = true, BackColor = Color.MistyRose, Visible = false, Font = new Font("Segoe UI", 9F) };
            btnNotRemembered.Click += BtnNotRemembered_Click;

            var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, AutoSize = true, Padding = new Padding(0, 15, 0, 5), Anchor = AnchorStyles.Top, Height = 50 }; // Anchor Top để không bị giãn ra
                                                                                                                                                                                                                                           // Set khoảng cách giữa các nút
            foreach (var btn in new Button[] { btnShowDetails, btnRemembered, btnNotRemembered }) { btn.Margin = new Padding(10, 0, 10, 0); }
            buttonPanel.Controls.AddRange(new Control[] { btnShowDetails, btnRemembered, btnNotRemembered });
            // Căn giữa các nút trong FlowLayoutPanel
            buttonPanel.Resize += (sender, args) => {
                int totalWidth = buttonPanel.Controls.Cast<Control>().Sum(c => c.Width + c.Margin.Horizontal);
                if (buttonPanel.Width > totalWidth)
                {
                    buttonPanel.Padding = new Padding((buttonPanel.Width - totalWidth) / 2, buttonPanel.Padding.Top, 0, buttonPanel.Padding.Bottom);
                }
                else
                {
                    buttonPanel.Padding = new Padding(0, buttonPanel.Padding.Top, 0, buttonPanel.Padding.Bottom);
                }
            };


            mainLayout.Controls.Add(buttonPanel, 0, 2);

            // --- Row 3: Progress Area ---
            lblProgress = new Label { Name = "lblProgress", Text = "Tiến độ: - / -", Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            mainLayout.Controls.Add(lblProgress, 0, 3);

            // --- Add Main Layout to Control ---
            this.Controls.Add(mainLayout);
        }

        // --- LoadTopics (Sử dụng topicRepo) ---
        private void LoadTopics()
        {
            if (topicsLoadedSuccessfully) return;
            Debug.WriteLine("[DailyReviewControl] Attempting to load topics...");
            if (cboTopics == null) { Debug.WriteLine("[DailyReviewControl] cboTopics is null during LoadTopics call."); return; }
            cboTopics.Items.Clear(); List<Topic> topics = null;
            try
            {
                topics = topicRepo.GetAllTopics(); // Sử dụng biến topicRepo
                if (topics != null)
                {
                    Debug.WriteLine($"[DailyReviewControl] Found {topics.Count} topics in DB."); var validTopics = topics.Where(t => t != null && !string.IsNullOrEmpty(t.Name)).ToList();
                    if (validTopics.Any()) { foreach (var topic in validTopics) { cboTopics.Items.Add(topic.Name); } Debug.WriteLine($"[DailyReviewControl] Added {cboTopics.Items.Count} items to ComboBox."); topicsLoadedSuccessfully = true; }
                    else { Debug.WriteLine("[DailyReviewControl] Topics list from DB is empty or contains no valid names."); }
                }
                else { Debug.WriteLine("[DailyReviewControl] GetAllTopics() returned null."); MessageBox.Show("Không thể lấy danh sách chủ đề (kết quả rỗng).", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { Debug.WriteLine($"[DailyReviewControl] Error loading Topics: {ex.ToString()}"); MessageBox.Show($"Lỗi khi tải chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); topics = new List<Topic>(); }
            if (cboTopics.Items.Count > 0) { cboTopics.SelectedIndex = 0; Debug.WriteLine("[DailyReviewControl] Selected first topic."); } else { Debug.WriteLine("[DailyReviewControl] ComboBox is empty after loading attempt."); }
        }

        // --- BtnStart_Click: Bắt đầu phiên ôn tập ---
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!topicsLoadedSuccessfully || cboTopics.SelectedItem == null) { MessageBox.Show("Vui lòng chọn chủ đề.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string selectedTopic = cboTopics.SelectedItem.ToString();
            int count = (int)numWordCount.Value;

            try
            {
                currentWordList = LoadWordsForReview(selectedTopic, count); // Gọi hàm tải từ mới
                if (currentWordList == null || currentWordList.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy từ vựng nào cho chủ đề '{selectedTopic}' hoặc không đủ số lượng yêu cầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetUI(); // Reset giao diện về trạng thái ban đầu
                    return;
                }
                else if (currentWordList.Count < count)
                {
                    MessageBox.Show($"Chỉ tìm thấy {currentWordList.Count} từ cho chủ đề '{selectedTopic}'. Bắt đầu ôn tập với số lượng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi tải từ vựng ôn tập: {ex.Message}");
                MessageBox.Show("Đã xảy ra lỗi khi tải từ vựng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetUI();
                return;
            }

            currentIndex = -1; // Bắt đầu từ -1 để ShowNextWord tăng lên 0
            EnableReviewControls(true); // Bật các nút điều khiển ôn tập
            ShowNextWord(); // Hiển thị từ đầu tiên
        }

        // --- LoadWordsForReview: Tải danh sách từ để ôn tập ---
        private List<Vocabulary> LoadWordsForReview(string topic, int count)
        {
            // **QUAN TRỌNG:** Câu lệnh SQL này cần được điều chỉnh
            // 1. SELECT thêm Id và AudioUrl
            // 2. (Nâng cao) Có thể thay ORDER BY NEWID() bằng logic ưu tiên từ "Chưa học", "Đang học"
            //    dựa trên bảng LearningStatuses. Hiện tại vẫn giữ NEWID() cho đơn giản.
            Debug.WriteLine($"[DailyReviewControl] Loading {count} words for topic '{topic}'");
            var words = new List<Vocabulary>();
            try
            {
                using (var conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    // Sửa câu SQL để lấy Id và AudioUrl
                    string sql = @"SELECT TOP (@Count) V.Id, V.Word, V.Pronunciation, V.Meaning, V.AudioUrl
                                       FROM Vocabulary V
                                       JOIN VocabularyTopic VT ON V.Id = VT.VocabularyId
                                       JOIN Topics T ON T.Id = VT.TopicId
                                       WHERE T.Name = @Topic
                                       ORDER BY NEWID()"; // Vẫn dùng random để đơn giản hóa
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Count", count);
                        cmd.Parameters.AddWithValue("@Topic", topic ?? (object)DBNull.Value); // Xử lý null cho topic
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                words.Add(new Vocabulary
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Word = reader.GetString(reader.GetOrdinal("Word")),
                                    Pronunciation = reader.IsDBNull(reader.GetOrdinal("Pronunciation")) ? "" : reader.GetString(reader.GetOrdinal("Pronunciation")),
                                    Meaning = reader.IsDBNull(reader.GetOrdinal("Meaning")) ? "" : reader.GetString(reader.GetOrdinal("Meaning")),
                                    AudioUrl = reader.IsDBNull(reader.GetOrdinal("AudioUrl")) ? "" : reader.GetString(reader.GetOrdinal("AudioUrl"))
                                });
                            }
                        }
                    }
                }
                Debug.WriteLine($"[DailyReviewControl] Loaded {words.Count} words.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DailyReviewControl] Error in LoadWordsForReview: {ex.Message}");
                throw; // Ném lại lỗi để BtnStart_Click xử lý
            }

            return words;
        }

        // --- ShowNextWord: Hiển thị từ tiếp theo ---
        private void ShowNextWord()
        {
            currentIndex++;
            if (currentWordList == null || currentIndex >= currentWordList.Count)
            {
                MessageBox.Show("🎉 Bạn đã hoàn thành ôn tập!");
                ResetUI();
                return;
            }

            var currentWord = currentWordList[currentIndex];

            // Cập nhật UI
            lblWord.Text = currentWord.Word;
            lblPronunciation.Text = currentWord.Pronunciation;
            lblMeaning.Text = currentWord.Meaning;
            lblProgress.Text = $"Tiến độ: {currentIndex + 1} / {currentWordList.Count}";

            // Lưu thông tin vào Tag của các nút để dùng trong event handlers
            btnPlayAudio.Tag = currentWord.AudioUrl;
            btnRemembered.Tag = currentWord.Id;
            btnNotRemembered.Tag = currentWord.Id;

            // Thiết lập trạng thái hiển thị ban đầu cho "flashcard"
            lblPronunciation.Visible = false;
            lblMeaning.Visible = false;
            btnRemembered.Visible = false;
            btnNotRemembered.Visible = false;
            btnShowDetails.Visible = true;
            btnPlayAudio.Visible = !string.IsNullOrEmpty(currentWord.AudioUrl); // Chỉ hiện nút Play nếu có URL
        }

        // --- BtnShowDetails_Click: Hiển thị chi tiết (lật thẻ) ---
        private void BtnShowDetails_Click(object sender, EventArgs e)
        {
            lblPronunciation.Visible = true;
            lblMeaning.Visible = true;
            btnRemembered.Visible = true;
            btnNotRemembered.Visible = true;
            btnShowDetails.Visible = false;
        }

        // --- BtnPlayAudio_Click: Phát âm thanh ---
        private void BtnPlayAudio_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string audioUrl = btn?.Tag as string;

            if (!string.IsNullOrEmpty(audioUrl))
            {
                try
                {
                    AudioHelper.PlayAudio(audioUrl); // Gọi hàm helper
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[DailyReviewControl] Error playing audio: {ex.Message}");
                    MessageBox.Show("Không thể phát âm thanh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có file âm thanh cho từ này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- BtnRemembered_Click: Đánh dấu đã nhớ ---
        private void BtnRemembered_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is int wordId)
            {
                UpdateLearningStatus(wordId, "Đã học");
                ShowNextWord(); // Chuyển sang từ tiếp theo
            }
        }

        // --- BtnNotRemembered_Click: Đánh dấu chưa nhớ ---
        private void BtnNotRemembered_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is int wordId)
            {
                UpdateLearningStatus(wordId, "Đang học"); // Hoặc trạng thái khác nếu muốn
                ShowNextWord(); // Chuyển sang từ tiếp theo
            }
        }

        // --- UpdateLearningStatus: Gọi Controller để cập nhật DB ---
        private void UpdateLearningStatus(int wordId, string status)
        {
            try
            {
                // Chuyển ID sang string vì LearningController hiện tại dùng string
                learningController.UpdateLearningStatus(wordId.ToString(), status);
                Debug.WriteLine($"[DailyReviewControl] Updated status for word ID {wordId} to '{status}'");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DailyReviewControl] Error updating learning status for word ID {wordId}: {ex.Message}");
                // Có thể hiển thị thông báo lỗi nếu cần, nhưng thường thì không cần làm gián đoạn luồng ôn tập
            }
        }

        // --- EnableReviewControls: Bật/tắt các nút điều khiển ôn tập ---
        private void EnableReviewControls(bool enable)
        {
            cardPanel.Visible = enable;
            lblProgress.Visible = enable;
            btnShowDetails.Visible = enable; // Sẽ bị ẩn/hiện lại trong ShowWord
            btnPlayAudio.Visible = enable;   // Sẽ bị ẩn/hiện lại trong ShowWord
            btnRemembered.Visible = false; // Luôn ẩn ban đầu khi bắt đầu/chuyển từ
            btnNotRemembered.Visible = false; // Luôn ẩn ban đầu

            // Header controls thì ngược lại
            cboTopics.Enabled = !enable;
            numWordCount.Enabled = !enable;
            btnStart.Enabled = !enable;
        }

        // --- ResetUI: Đưa giao diện về trạng thái ban đầu ---
        private void ResetUI()
        {
            EnableReviewControls(false); // Tắt các control ôn tập
            lblWord.Text = "...";
            lblPronunciation.Text = "";
            lblMeaning.Text = "";
            lblProgress.Text = "Tiến độ: - / -";
            currentWordList.Clear();
            currentIndex = -1;
        }

    }
}