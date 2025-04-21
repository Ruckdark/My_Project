using System;
using System.Collections.Generic;
// using System.Data.SqlClient; // Cần nếu dùng trực tiếp
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;
using WordVaultAppMVC.Controllers;

namespace WordVaultAppMVC.Views.Controls
{
    public class QuizControl : UserControl
    {
        // --- Fields UI ---
        private ComboBox cboTopic;
        private NumericUpDown numQuestionCount;
        private Button btnStart, btnBack, btnNext, btnSubmitQuiz, btnRetryWrong;
        private Label lblQuestion, lblProgress;
        private Label lblScore;
        private Label lblAnsweredCount; // Mới: Hiển thị số câu đã trả lời
        private Label lblTimer;         // Mới: Hiển thị thời gian
        private RadioButton[] rdoOptions;
        private FlowLayoutPanel pnlBottomJumpButtons;
        private TableLayoutPanel mainLayoutPanel;
        private TableLayoutPanel leftPanelLayout;
        private TableLayoutPanel rightPanelLayout;
        private Timer quizTimer;        // Mới: Timer để đếm giờ

        // --- Fields logic ---
        private List<Vocabulary> questions;
        private Dictionary<int, string> userAnswers = new Dictionary<int, string>();
        private Dictionary<int, List<string>> questionOptions = new Dictionary<int, List<string>>();
        private List<Vocabulary> wrongWords = new List<Vocabulary>();
        private int currentIndex = 0;
        private bool topicsLoadedSuccessfully = false;
        private int elapsedSeconds = 0; // Mới: Biến đếm giây
        private VocabularyRepository vocabRepo;
        private TopicRepository topicRepo;

        public QuizControl()
        {
            vocabRepo = new VocabularyRepository();
            topicRepo = new TopicRepository();
            InitializeComponent();
            if (!this.DesignMode) { LoadTopics(); }
        }

        // --- InitializeComponent đã bổ sung Label và Timer ---
        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill; this.BackColor = SystemColors.ControlLightLight; this.Padding = new Padding(15);
            mainLayoutPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, BackColor = Color.Transparent };
            mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F)); mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize)); mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));

            // --- Header (Giữ nguyên) ---
            cboTopic = new ComboBox { Name = "cboTopic", Anchor = AnchorStyles.Left, Margin = new Padding(3, 6, 3, 3), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            numQuestionCount = new NumericUpDown { Name = "numQuestionCount", Minimum = 1, Maximum = 100, Value = 20, Width = 60, Anchor = AnchorStyles.Left, Margin = new Padding(3, 6, 3, 3) };
            btnStart = new Button { Name = "btnStart", Text = "Bắt đầu Quiz", Anchor = AnchorStyles.Left, Margin = new Padding(10, 3, 3, 3), AutoSize = true, BackColor = Color.MediumSeaGreen, ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), FlatStyle = FlatStyle.Flat }; btnStart.FlatAppearance.BorderSize = 0; btnStart.Click += BtnStart_Click;
            var headerFlowPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, AutoSize = true, Padding = new Padding(0, 0, 0, 10) };
            headerFlowPanel.Controls.AddRange(new Control[] { new Label { Text = "Chủ đề", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 8, 0, 0), Font = new Font("Segoe UI", 9F, FontStyle.Bold) }, cboTopic, new Label { Text = "Số câu hỏi", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(10, 8, 0, 0), Font = new Font("Segoe UI", 9F, FontStyle.Bold) }, numQuestionCount, btnStart });
            mainLayoutPanel.Controls.Add(headerFlowPanel, 0, 0); mainLayoutPanel.SetColumnSpan(headerFlowPanel, 2);

            // --- Left Panel (Giữ nguyên) ---
            leftPanelLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(5, 0, 20, 0) };
            leftPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); leftPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); leftPanelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); leftPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblQuestion = new Label { Name = "lblQuestion", Font = new Font("Segoe UI", 18F, FontStyle.Bold), AutoSize = true, Dock = DockStyle.Top, Text = "Từ: ..." };
            lblProgress = new Label { Name = "lblProgress", Font = new Font("Segoe UI", 10F), ForeColor = Color.Gray, AutoSize = true, Dock = DockStyle.Top, Text = "Câu 0/0", Padding = new Padding(0, 0, 0, 20) };
            leftPanelLayout.Controls.Add(lblQuestion, 0, 0); leftPanelLayout.Controls.Add(lblProgress, 0, 1);
            var optionsFlowPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Padding = new Padding(0, 5, 0, 5) };
            rdoOptions = new RadioButton[4];
            for (int i = 0; i < 4; i++) { rdoOptions[i] = new RadioButton { Name = $"rdoOption{i + 1}", Font = new Font("Segoe UI", 11F), Padding = new Padding(5), Margin = new Padding(0, 5, 0, 5), AutoSize = true }; rdoOptions[i].CheckedChanged += Option_CheckedChanged; rdoOptions[i].CheckedChanged += RadioButton_StyleOnChange; optionsFlowPanel.Controls.Add(rdoOptions[i]); }
            leftPanelLayout.Controls.Add(optionsFlowPanel, 0, 2);
            btnBack = new Button { Name = "btnBack", Text = "← Quay lại", Width = 100, Anchor = AnchorStyles.Left, AutoSize = true, BackColor = Color.WhiteSmoke, Font = new Font("Segoe UI", 9F), FlatStyle = FlatStyle.Flat }; btnBack.FlatAppearance.BorderColor = Color.LightGray; btnBack.FlatAppearance.BorderSize = 1;
            btnNext = new Button { Name = "btnNext", Text = "Tiếp >", Width = 100, Anchor = AnchorStyles.Left, AutoSize = true, BackColor = Color.WhiteSmoke, Font = new Font("Segoe UI", 9F), FlatStyle = FlatStyle.Flat }; btnNext.FlatAppearance.BorderColor = Color.LightGray; btnNext.FlatAppearance.BorderSize = 1;
            btnSubmitQuiz = new Button { Name = "btnSubmitQuiz", Text = "Nộp bài", Width = 100, Anchor = AnchorStyles.Left, AutoSize = true, BackColor = Color.DodgerBlue, ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), FlatStyle = FlatStyle.Flat }; btnSubmitQuiz.FlatAppearance.BorderSize = 0;
            var navButtonFlowPanel = new FlowLayoutPanel { Dock = DockStyle.Top, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, AutoSize = true, Padding = new Padding(0, 15, 0, 0) };
            foreach (var btn in new Button[] { btnBack, btnNext, btnSubmitQuiz }) { btn.Margin = new Padding(5); }
            navButtonFlowPanel.Controls.AddRange(new Control[] { btnBack, btnNext, btnSubmitQuiz }); leftPanelLayout.Controls.Add(navButtonFlowPanel, 0, 3); mainLayoutPanel.Controls.Add(leftPanelLayout, 0, 1);

            // --- Right Panel (Sidebar) - Bổ sung Labels ---
            rightPanelLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5, BackColor = Color.FromArgb(240, 243, 247), Padding = new Padding(15) };
            rightPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 0: Thống kê Title
            rightPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 1: Score
            rightPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 2: Answered Count (Mới)
            rightPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 3: Timer (Mới)
            rightPanelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Hàng 4: Khoảng trống
            rightPanelLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 5: Nút Luyện lại

            var lblStatsTitle = new Label { Text = "Thống kê", Font = new Font("Segoe UI", 10F, FontStyle.Bold), AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 5) };
            lblScore = new Label { Name = "lblScore", Text = "Điểm: __ / __", Font = new Font("Segoe UI", 9F), AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(5, 0, 0, 10) };
            rightPanelLayout.Controls.Add(lblStatsTitle, 0, 0);
            rightPanelLayout.Controls.Add(lblScore, 0, 1); // Score ở hàng 1

            // Số câu đã trả lời (MỚI)
            lblAnsweredCount = new Label { Name = "lblAnsweredCount", Text = "Đã trả lời: 0 / 0", Font = new Font("Segoe UI", 9F), AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(5, 5, 0, 10) };
            rightPanelLayout.Controls.Add(lblAnsweredCount, 0, 2); // Đặt ở hàng 2

            // Thời gian (MỚI)
            lblTimer = new Label { Name = "lblTimer", Text = "Thời gian: 00:00", Font = new Font("Segoe UI", 9F), AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(5, 5, 0, 10) };
            rightPanelLayout.Controls.Add(lblTimer, 0, 3); // Đặt ở hàng 3

            btnRetryWrong = new Button { Name = "btnRetryWrong", Text = "Luyện lại từ sai", Dock = DockStyle.Bottom, Height = 40, Visible = false, BackColor = Color.RoyalBlue, ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatStyle = FlatStyle.Flat }; btnRetryWrong.FlatAppearance.BorderSize = 0; btnRetryWrong.Click += RetryWrongAnswers;
            rightPanelLayout.Controls.Add(btnRetryWrong, 0, 5); // Đặt vào hàng 5 (hàng cuối)
            mainLayoutPanel.Controls.Add(rightPanelLayout, 1, 1);

            // --- Bottom Navigation Panel (Giữ nguyên) ---
            pnlBottomJumpButtons = new FlowLayoutPanel { Name = "pnlBottomJumpButtons", AutoScroll = true, Dock = DockStyle.Fill, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(5, 10, 5, 5), BackColor = Color.WhiteSmoke };
            mainLayoutPanel.Controls.Add(pnlBottomJumpButtons, 0, 2); mainLayoutPanel.SetColumnSpan(pnlBottomJumpButtons, 2);

            // --- Add main layout ---
            this.Controls.Add(mainLayoutPanel); mainLayoutPanel.BringToFront();

            // --- Thêm Timer component ---
            quizTimer = new Timer { Interval = 1000 }; // Tick mỗi giây
            quizTimer.Tick += QuizTimer_Tick;
            // Không cần Add vào this.components nếu không dùng Designer

            // --- Gán lại sự kiện click nút ---
            btnBack.Click += (s, e) => JumpHandler(-1); btnNext.Click += (s, e) => JumpHandler(1); btnSubmitQuiz.Click += SubmitQuiz;

            // --- Initial State ---
            EnableQuizControls(false);
        }

        // --- Hàm xử lý chung cho nút Back/Next ---
        private void JumpHandler(int direction)
        {
            if (questions == null || questions.Count == 0) return;
            SaveAnswer();
            int newIndex = currentIndex + direction;
            if (newIndex >= 0 && newIndex < questions.Count) { currentIndex = newIndex; LoadQuestion(); UpdateJumpButtonStyles(); }
        }

        // --- Hàm xử lý chung cho các nút Jump ---
        private void JumpButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag is int index)
            {
                if (index >= 0 && index < (questions?.Count ?? 0)) { SaveAnswer(); currentIndex = index; LoadQuestion(); UpdateJumpButtonStyles(); }
            }
        }

        // --- Style RadioButton ---
        private void RadioButton_StyleOnChange(object sender, EventArgs e)
        {
            RadioButton currentRadio = sender as RadioButton; if (currentRadio == null) return;
            foreach (RadioButton rdo in rdoOptions) { if (rdo != null && rdo != currentRadio) { rdo.ForeColor = SystemColors.ControlText; rdo.Font = new Font(rdo.Font, FontStyle.Regular); } }
            if (currentRadio.Checked) { currentRadio.ForeColor = Color.DodgerBlue; currentRadio.Font = new Font(currentRadio.Font, FontStyle.Bold); }
            else { currentRadio.ForeColor = SystemColors.ControlText; currentRadio.Font = new Font(currentRadio.Font, FontStyle.Regular); }
        }

        // --- LoadTopics với Debug Output ---
        private void LoadTopics()
        {
            if (topicsLoadedSuccessfully) return;
            Debug.WriteLine("[QuizControl] Attempting to load topics...");
            if (cboTopic == null) { Debug.WriteLine("[QuizControl] cboTopic is null during LoadTopics call."); return; }
            cboTopic.Items.Clear(); List<Topic> topics = null;
            try
            {
                topics = topicRepo.GetAllTopics();
                if (topics != null)
                {
                    Debug.WriteLine($"[QuizControl] Found {topics.Count} topics in DB."); var validTopics = topics.Where(t => t != null && !string.IsNullOrEmpty(t.Name)).ToList();
                    if (validTopics.Any()) { foreach (var topic in validTopics) { cboTopic.Items.Add(topic.Name); } Debug.WriteLine($"[QuizControl] Added {cboTopic.Items.Count} items to ComboBox."); topicsLoadedSuccessfully = true; }
                    else { Debug.WriteLine("[QuizControl] Topics list from DB is empty or contains no valid names."); }
                }
                else { Debug.WriteLine("[QuizControl] GetAllTopics() returned null."); MessageBox.Show("Không thể lấy danh sách chủ đề (kết quả rỗng).", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { Debug.WriteLine($"[QuizControl] Error loading Topics: {ex.ToString()}"); MessageBox.Show($"Lỗi khi tải chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); topics = new List<Topic>(); }
            if (cboTopic.Items.Count > 0) { cboTopic.SelectedIndex = 0; Debug.WriteLine("[QuizControl] Selected first topic."); } else { Debug.WriteLine("[QuizControl] ComboBox is empty after loading attempt."); }
        }

        // --- BtnStart_Click ĐÃ THAY ĐỔI: Tạo sẵn đáp án và Bật Timer ---
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!topicsLoadedSuccessfully || cboTopic.SelectedItem == null) { MessageBox.Show("Vui lòng chọn một chủ đề hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string selectedTopic = cboTopic.SelectedItem.ToString(); int count = (int)numQuestionCount.Value;
            lblScore.Text = $"Điểm: __ / {count}";
            lblAnsweredCount.Text = $"Đã trả lời: 0 / {count}"; // Cập nhật ban đầu
            lblTimer.Text = "Thời gian: 00:00"; // Reset timer display

            List<Vocabulary> allVocabForTopic = null; List<Vocabulary> allOtherVocab = null;
            try
            {
                allVocabForTopic = vocabRepo.GetVocabularyByTopic(selectedTopic);
                if (allVocabForTopic == null || allVocabForTopic.Count < count) { MessageBox.Show($"Chủ đề '{selectedTopic}' không có đủ {count} từ vựng.", "Không đủ từ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                questions = allVocabForTopic.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
                allOtherVocab = vocabRepo.GetAllVocabulary() ?? new List<Vocabulary>(); var questionIds = new HashSet<int>(questions.Select(q => q.Id)); allOtherVocab = allOtherVocab.Where(v => v != null && !questionIds.Contains(v.Id) && !string.IsNullOrEmpty(v.Meaning)).ToList();
            }
            catch (Exception ex) { Debug.WriteLine($"Lỗi lấy từ vựng hoặc tạo câu hỏi: {ex.Message}"); MessageBox.Show("Lỗi khi tải câu hỏi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            questionOptions.Clear(); var random = new Random();
            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i]; var wrongAnswers = allOtherVocab.OrderBy(_ => random.Next()).Select(v => v.Meaning).Distinct().Take(3).ToList();
                while (wrongAnswers.Count < 3) { wrongAnswers.Add($"Đáp án ngẫu nhiên {random.Next(1000, 9999)}"); }
                var currentQuestionAllAnswers = new List<string> { q.Meaning }; currentQuestionAllAnswers.AddRange(wrongAnswers); currentQuestionAllAnswers = currentQuestionAllAnswers.OrderBy(_ => random.Next()).ToList();
                questionOptions[i] = currentQuestionAllAnswers;
            }

            currentIndex = 0; userAnswers.Clear(); pnlBottomJumpButtons.Controls.Clear(); btnRetryWrong.Visible = false; wrongWords.Clear();
            EnableQuizControls(true);

            // Tạo nút Jump
            for (int i = 0; i < questions.Count; i++) { var btnBottom = new Button { Text = (i + 1).ToString(), Width = 35, Height = 30, Tag = i, Margin = new Padding(2), Font = new Font("Segoe UI", 8F), FlatStyle = FlatStyle.Flat, BackColor = Color.Gainsboro }; btnBottom.FlatAppearance.BorderColor = Color.DarkGray; btnBottom.FlatAppearance.BorderSize = 1; btnBottom.Click += JumpButton_Click; pnlBottomJumpButtons.Controls.Add(btnBottom); }

            // Bắt đầu Timer
            elapsedSeconds = 0; // Reset giây
            quizTimer.Start(); // Bắt đầu đếm giờ

            LoadQuestion(); UpdateJumpButtonStyles();
        }

        // --- LoadQuestion ĐÃ SỬA LỖI ---
        private void LoadQuestion()
        {
            if (questions == null || questions.Count == 0) { EnableQuizControls(false); lblQuestion.Text = "Không có câu hỏi."; return; }
            if (currentIndex < 0 || currentIndex >= questions.Count) currentIndex = 0;

            EnableQuizControls(true);
            var q = questions[currentIndex]; lblQuestion.Text = "Từ: " + q.Word; lblProgress.Text = $"Câu {currentIndex + 1}/{questions.Count}";
            lblAnsweredCount.Text = $"Đã trả lời: {userAnswers.Count} / {questions.Count}"; // Cập nhật số câu đã trả lời

            List<string> currentOptions = null;
            if (questionOptions.ContainsKey(currentIndex)) { currentOptions = questionOptions[currentIndex]; }
            else { Debug.WriteLine($"[LoadQuestion] Error: Options not found for index {currentIndex}"); currentOptions = new List<string> { "Lỗi" }; }

            userAnswers.TryGetValue(currentIndex, out var savedAnswer); savedAnswer = savedAnswer?.Trim();

            for (int i = 0; i < rdoOptions.Length; i++)
            {
                rdoOptions[i].CheckedChanged -= Option_CheckedChanged; rdoOptions[i].CheckedChanged -= RadioButton_StyleOnChange;
                if (i < currentOptions.Count) { rdoOptions[i].Text = currentOptions[i]; rdoOptions[i].Visible = true; bool shouldBeChecked = !string.IsNullOrEmpty(savedAnswer) && string.Equals(rdoOptions[i].Text?.Trim(), savedAnswer, StringComparison.OrdinalIgnoreCase); rdoOptions[i].Checked = shouldBeChecked; }
                else { rdoOptions[i].Text = ""; rdoOptions[i].Visible = false; rdoOptions[i].Checked = false; }
                rdoOptions[i].CheckedChanged += Option_CheckedChanged; rdoOptions[i].CheckedChanged += RadioButton_StyleOnChange;
                RadioButton_StyleOnChange(rdoOptions[i], EventArgs.Empty);
            }
            UpdateJumpButtonStyles();
        }

        // --- Option_CheckedChanged ĐÃ SỬA LỖI ---
        private void Option_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton RdoClick = sender as RadioButton;
            if (RdoClick != null && RdoClick.Checked)
            {
                foreach (RadioButton RdoItem in rdoOptions) { if (RdoItem != RdoClick) { RdoItem.CheckedChanged -= Option_CheckedChanged; RdoItem.CheckedChanged -= RadioButton_StyleOnChange; RdoItem.Checked = false; RdoItem.CheckedChanged += Option_CheckedChanged; RdoItem.CheckedChanged += RadioButton_StyleOnChange; RadioButton_StyleOnChange(RdoItem, EventArgs.Empty); } }
                SaveAnswer(); // SaveAnswer sẽ cập nhật lblAnsweredCount
                UpdateJumpButtonStyles();
            }
        }

        // --- SaveAnswer (Cập nhật lblAnsweredCount)---
        private void SaveAnswer()
        {
            if (questions == null || currentIndex < 0 || currentIndex >= questions.Count) return;
            bool changed = false; // Biến kiểm tra xem từ điển userAnswers có thay đổi không
            var selected = rdoOptions.FirstOrDefault(r => r.Checked);
            string currentAnswer = selected?.Text.Trim();

            if (currentAnswer != null)
            {
                // Kiểm tra xem giá trị mới có khác giá trị cũ không (hoặc key chưa tồn tại)
                if (!userAnswers.TryGetValue(currentIndex, out string previousAnswer) || previousAnswer != currentAnswer)
                {
                    userAnswers[currentIndex] = currentAnswer;
                    changed = true;
                    Debug.WriteLine($"[SaveAnswer] Saved answer for index {currentIndex}: '{userAnswers[currentIndex]}'");
                }
            }
            else
            {
                if (userAnswers.ContainsKey(currentIndex)) // Chỉ remove và đánh dấu changed nếu key tồn tại
                {
                    userAnswers.Remove(currentIndex);
                    changed = true;
                    Debug.WriteLine($"[SaveAnswer] Removed answer for index {currentIndex}");
                }
            }

            // Chỉ cập nhật Label nếu có sự thay đổi
            if (changed && questions != null)
            {
                lblAnsweredCount.Text = $"Đã trả lời: {userAnswers.Count} / {questions.Count}";
            }
        }


        // --- SubmitQuiz (Dừng Timer) ---
        private void SubmitQuiz(object sender, EventArgs e)
        {
            quizTimer.Stop(); // Dừng đếm giờ
            if (questions == null) return;
            if (userAnswers.Count < questions.Count) { if (MessageBox.Show($"Bạn chưa trả lời hết {questions.Count - userAnswers.Count} câu. Vẫn nộp bài?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) { quizTimer.Start(); return; } } // Start lại timer nếu không nộp

            int correct = 0; wrongWords.Clear(); var resultDetails = new List<(Vocabulary vocab, string userAnswer, bool isCorrect)>();
            LearningController learningController = new LearningController();

            for (int i = 0; i < questions.Count; i++)
            {
                var word = questions[i]; userAnswers.TryGetValue(i, out var selectedAnswer); selectedAnswer = selectedAnswer ?? "(Chưa trả lời)";
                bool isCorrect = string.Equals(selectedAnswer, word.Meaning?.Trim(), StringComparison.OrdinalIgnoreCase);
                resultDetails.Add((word, selectedAnswer, isCorrect));
                try { if (isCorrect) { correct++; learningController.UpdateLearningStatus(word.Id.ToString(), "Đã học"); } else { wrongWords.Add(word); learningController.UpdateLearningStatus(word.Id.ToString(), "Đang học"); } }
                catch (Exception ex) { Debug.WriteLine($"SubmitQuiz UpdateLearningStatus Error for Word ID {word.Id}: {ex.Message}"); }
            }

            lblScore.Text = $"Điểm: {correct} / {questions.Count}";
            if (resultDetails.Any()) { using (var summaryForm = new ResultSummaryForm(resultDetails)) { summaryForm.ShowDialog(this.FindForm()); } }
            btnRetryWrong.Visible = wrongWords.Count > 0;
            EnableQuizControls(false);
            UpdateJumpButtonStylesAfterSubmit();
        }

        // --- RetryWrongAnswers (Reset và Bật Timer) ---
        private void RetryWrongAnswers(object sender, EventArgs e)
        {
            if (wrongWords == null || wrongWords.Count == 0) return;
            questions = new List<Vocabulary>(wrongWords); int count = questions.Count;
            lblScore.Text = $"Điểm: __ / {count}";
            lblAnsweredCount.Text = $"Đã trả lời: 0 / {count}"; // Reset answered count
            lblTimer.Text = "Thời gian: 00:00"; // Reset timer display

            // --- TẠO LẠI BỘ ĐÁP ÁN CHO CÁC TỪ SAI ---
            questionOptions.Clear(); var random = new Random(); List<Vocabulary> allOtherVocab = null;
            try { allOtherVocab = vocabRepo.GetAllVocabulary() ?? new List<Vocabulary>(); var currentQuestionIds = new HashSet<int>(questions.Select(q => q.Id)); allOtherVocab = allOtherVocab.Where(v => v != null && !currentQuestionIds.Contains(v.Id) && !string.IsNullOrEmpty(v.Meaning)).ToList(); }
            catch (Exception ex) { Debug.WriteLine($"Lỗi lấy từ vựng khi Retry: {ex.Message}"); allOtherVocab = new List<Vocabulary>(); }
            for (int i = 0; i < questions.Count; i++) { var q = questions[i]; var wrongAnswersRetry = allOtherVocab.OrderBy(_ => random.Next()).Select(v => v.Meaning).Distinct().Take(3).ToList(); while (wrongAnswersRetry.Count < 3) { wrongAnswersRetry.Add($"Đáp án ngẫu nhiên {random.Next(1000, 9999)}"); } var currentQuestionAllAnswersRetry = new List<string> { q.Meaning }; currentQuestionAllAnswersRetry.AddRange(wrongAnswersRetry); currentQuestionAllAnswersRetry = currentQuestionAllAnswersRetry.OrderBy(_ => random.Next()).ToList(); questionOptions[i] = currentQuestionAllAnswersRetry; }

            currentIndex = 0; userAnswers.Clear(); pnlBottomJumpButtons.Controls.Clear(); wrongWords.Clear(); btnRetryWrong.Visible = false;
            EnableQuizControls(true);

            // Tạo lại nút jump
            for (int i = 0; i < questions.Count; i++) { var btnBottom = new Button { Text = (i + 1).ToString(), Width = 35, Height = 30, Tag = i, Margin = new Padding(2), Font = new Font("Segoe UI", 8F), FlatStyle = FlatStyle.Flat, BackColor = Color.Gainsboro }; btnBottom.FlatAppearance.BorderColor = Color.DarkGray; btnBottom.FlatAppearance.BorderSize = 1; btnBottom.Click += JumpButton_Click; pnlBottomJumpButtons.Controls.Add(btnBottom); }

            // Bắt đầu lại Timer
            elapsedSeconds = 0;
            quizTimer.Start();

            LoadQuestion(); UpdateJumpButtonStyles();
        }

        // --- EnableQuizControls (Giữ nguyên) ---
        private void EnableQuizControls(bool enable)
        {
            foreach (var rdo in rdoOptions) { if (rdo != null) rdo.Enabled = enable; }
            btnBack.Enabled = enable && (questions?.Count > 1) && currentIndex > 0;
            btnNext.Enabled = enable && (questions?.Count > 1) && currentIndex < (questions?.Count ?? 0) - 1;
            btnSubmitQuiz.Enabled = enable && (questions?.Count > 0);
            pnlBottomJumpButtons.Enabled = enable && (questions?.Count > 0);
            cboTopic.Enabled = !enable; numQuestionCount.Enabled = !enable; btnStart.Enabled = !enable;
            btnRetryWrong.Enabled = btnRetryWrong.Visible && !enable;
            // Bật/tắt Timer cùng với các control khác
            if (enable && questions?.Count > 0) { if (!quizTimer.Enabled) quizTimer.Start(); } // Start nếu chưa chạy
            else { quizTimer.Stop(); } // Stop nếu không enable hoặc không có câu hỏi
        }

        // --- UpdateJumpButtonStyles (Giữ nguyên) ---
        private void UpdateJumpButtonStyles() { UpdateJumpPanel(pnlBottomJumpButtons); }
        private void UpdateJumpPanel(FlowLayoutPanel panel) { /* ... */ }
        // --- UpdateJumpButtonStylesAfterSubmit (Giữ nguyên) ---
        private void UpdateJumpButtonStylesAfterSubmit() { UpdateJumpPanelAfterSubmit(pnlBottomJumpButtons); }
        private void UpdateJumpPanelAfterSubmit(FlowLayoutPanel panel) { /* ... */ }

        // --- MỚI: Sự kiện Tick của Timer ---
        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            // Định dạng thời gian thành MM:SS
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            lblTimer.Text = $"Thời gian: {time.ToString(@"mm\:ss")}";
        }

    }
}