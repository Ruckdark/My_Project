// File: HomeControl.cs
using System;
using System.Data.SqlClient; // Cho SqlException khi gọi Repo
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordVaultAppMVC.Data; // Namespace của DatabaseContext và Repositories
using WordVaultAppMVC.Helpers;  // Namespace của DictionaryApiClient, AudioHelper
using WordVaultAppMVC.Models;   // Namespace của Vocabulary, WordDetails models
using WordVaultAppMVC.Views;    // Namespace của AddToTopicForm (Nếu nằm ở đây)

namespace WordVaultAppMVC.Views.Controls
{
    public class HomeControl : UserControl
    {
        // --- UI Controls (Giữ nguyên) ---
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblPronunciation;
        private Label lblMeaning;
        private Button btnPlayAudio;
        private Button btnAddFavorite;
        private Button btnAddTopic;
        private Label lblStatusMessage;
        private System.Windows.Forms.Timer statusTimer;
        private TableLayoutPanel mainLayout;
        private Panel pnlFooter; // Panel mới ở dưới cùng
        private Label lblVocabularyCount; // Label mới hiển thị số từ
        // private FlowLayoutPanel searchPanel; // <<-- Biến này không còn dùng nữa
        private TableLayoutPanel resultLayout;
        private FlowLayoutPanel buttonPanel;

        // --- Logic Fields (Giữ nguyên) ---
        private Vocabulary currentVocabulary; // Lưu từ đang hiển thị
        private readonly VocabularyRepository vocabRepo;

        public HomeControl()
        {
            vocabRepo = new VocabularyRepository();
            InitializeComponent(); // Gọi hàm khởi tạo giao diện (giữ nguyên)
            UpdateActionButtonsState(false); // Vô hiệu hóa nút ban đầu
            LoadVocabularyCount();
        }

        // --- InitializeComponent (Đã sửa đổi khu vực tìm kiếm) ---
        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = SystemColors.ControlLightLight;
            mainLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(10), AutoScroll = true };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 0: Khu vực tìm kiếm
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Hàng 1: Khu vực kết quả
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng 2: Thông báo trạng thái

            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom, // Dock xuống dưới cùng
                Height = 25,            // Chiều cao vừa đủ
                BackColor = SystemColors.Control, // Màu nền giống status bar
                Padding = new Padding(5, 0, 5, 0) // Padding hai bên
            };
            lblVocabularyCount = new Label
            {
                Name = "lblVocabularyCount",
                Text = "Số từ vựng: ...",
                Dock = DockStyle.Right, // Dock sang phải của pnlFooter
                Font = new Font("Segoe UI", 8.5F), // Font nhỏ hơn
                ForeColor = Color.DimGray,      // Màu xám
                TextAlign = ContentAlignment.MiddleRight, // Căn lề phải
                AutoSize = false, // Tắt AutoSize để Dock hoạt động
                Width = 200 // Đặt chiều rộng cố định hoặc để Dock tự quyết định
            };
            pnlFooter.Controls.Add(lblVocabularyCount); // Thêm label vào footer panel

            // --- BẮT ĐẦU THAY ĐỔI ---

            // ***** Phần FlowLayoutPanel cũ đã được thay thế *****
            /*
            // Search Panel
            searchPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, AutoSize = true, Padding = new Padding(5, 5, 5, 15) };
            txtSearch = new TextBox { Name = "txtSearch", Font = new Font("Segoe UI", 11F), Width = 350, Margin = new Padding(3) };
            txtSearch.KeyDown += TxtSearch_KeyDown;
            btnSearch = new Button { Name = "btnSearch", Text = "🔍 Tìm kiếm", Font = new Font("Segoe UI", 10F, FontStyle.Bold), AutoSize = true, BackColor = Color.SteelBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(5, 3, 3, 3) };
            btnSearch.FlatAppearance.BorderSize = 0; btnSearch.Click += async (s, e) => await SearchAndDisplayWordAsync();
            searchPanel.Controls.AddRange(new Control[] { txtSearch, btnSearch });
            mainLayout.Controls.Add(searchPanel, 0, 0);
            */

            // ***** Code mới sử dụng TableLayoutPanel cho khu vực tìm kiếm *****
            var searchTableLayout = new TableLayoutPanel
            {
                Name = "searchTableLayout",
                Dock = DockStyle.Fill,      // Lấp đầy chiều ngang của hàng 0 trong mainLayout
                ColumnCount = 2,           // Chia làm 2 cột
                RowCount = 1,              // Chỉ có 1 hàng
                AutoSize = true,           // Chiều cao tự động theo nội dung
                Padding = new Padding(5, 5, 5, 15) // Giữ Padding tương tự panel cũ
            };
            // Cấu hình cột: Cột 0 (TextBox) chiếm hết phần rộng còn lại, Cột 1 (Button) tự động kích thước
            searchTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            searchTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // Khởi tạo TextBox - Bỏ Width cố định, thêm Dock = DockStyle.Fill
            txtSearch = new TextBox { Name = "txtSearch", Font = new Font("Segoe UI", 11F), Margin = new Padding(3), Dock = DockStyle.Fill }; // <<< Đặt Dock = Fill
            txtSearch.KeyDown += TxtSearch_KeyDown; // Giữ lại sự kiện KeyDown

            // Khởi tạo Button - Giữ nguyên các thuộc tính cũ
            btnSearch = new Button { Name = "btnSearch", Text = "🔍 Tìm kiếm", Font = new Font("Segoe UI", 10F, FontStyle.Bold), AutoSize = true, BackColor = Color.SteelBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(5, 3, 3, 3), Anchor = AnchorStyles.Left };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += async (s, e) => await SearchAndDisplayWordAsync(); // Giữ lại sự kiện Click

            // Thêm TextBox và Button vào TableLayoutPanel mới
            searchTableLayout.Controls.Add(txtSearch, 0, 0); // txtSearch vào cột 0
            searchTableLayout.Controls.Add(btnSearch, 1, 0); // btnSearch vào cột 1

            // Thêm TableLayoutPanel mới vào mainLayout (ở hàng 0, cột 0)
            mainLayout.Controls.Add(searchTableLayout, 0, 0);

            // --- KẾT THÚC THAY ĐỔI ---


            // --- Các phần còn lại của InitializeComponent giữ nguyên ---
            // Result Area
            resultLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Padding = new Padding(5) };
            resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F)); resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lblPronunciation = new Label { Name = "lblPronunciation", Text = "Phát âm:", Font = new Font("Segoe UI", 11F, FontStyle.Italic), ForeColor = Color.DarkSlateGray, AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 10) };
            resultLayout.Controls.Add(lblPronunciation, 0, 0); resultLayout.SetColumnSpan(lblPronunciation, 2);
            lblMeaning = new Label { Name = "lblMeaning", Text = "Nghĩa tiếng Việt:", Font = new Font("Segoe UI", 12F), AutoSize = false, Dock = DockStyle.Fill, Padding = new Padding(0, 5, 10, 5), AutoEllipsis = true };
            lblMeaning.MaximumSize = new Size(500, 0); lblMeaning.AutoSize = true;
            resultLayout.Controls.Add(lblMeaning, 0, 1);
            buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(10, 0, 0, 0), AutoSize = true };
            btnPlayAudio = new Button { Name = "btnPlayAudio", Text = "🔊 Nghe", Width = 150, Height = 35, Enabled = false, Font = new Font("Segoe UI", 9F), FlatStyle = FlatStyle.System };
            btnPlayAudio.Click += BtnPlayAudio_Click;
            btnAddFavorite = new Button { Name = "btnAddFavorite", Text = "⭐ Yêu thích", Width = 150, Height = 35, Enabled = false, Font = new Font("Segoe UI", 9F), FlatStyle = FlatStyle.System };
            btnAddFavorite.Click += BtnAddFavorite_Click;
            btnAddTopic = new Button { Name = "btnAddTopic", Text = "📚 Thêm vào chủ đề", Width = 150, Height = 35, Enabled = false, Font = new Font("Segoe UI", 9F), FlatStyle = FlatStyle.System };
            btnAddTopic.Click += BtnAddTopic_Click;
            foreach (var btn in new Button[] { btnPlayAudio, btnAddFavorite, btnAddTopic }) { btn.Margin = new Padding(0, 5, 0, 5); }
            buttonPanel.Controls.AddRange(new Control[] { btnPlayAudio, btnAddFavorite, btnAddTopic });
            resultLayout.Controls.Add(buttonPanel, 1, 1);
            mainLayout.Controls.Add(resultLayout, 0, 1); // Thêm resultLayout vào hàng 1

            // Status Message
            lblStatusMessage = new Label { Name = "lblStatusMessage", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Green, Font = new Font("Segoe UI", 9F, FontStyle.Italic), Visible = false, Height = 25 };
            mainLayout.Controls.Add(lblStatusMessage, 0, 2); // Thêm lblStatusMessage vào hàng 2

            // Status Timer
            statusTimer = new System.Windows.Forms.Timer { Interval = 4000 };
            statusTimer.Tick += StatusTimer_Tick;

            // Add Main Layout vào UserControl
            //this.Controls.Add(mainLayout);

            // --- Add Controls to UserControl (Thứ tự quan trọng cho Docking) ---
            this.Controls.Add(mainLayout); // Add main layout trước
            this.Controls.Add(pnlFooter);  // Add footer panel sau (nó sẽ dock xuống dưới)
            mainLayout.BringToFront();     // Đảm bảo mainLayout nằm trên footer
        }


        // --- Xử lý nhấn Enter (Giữ nguyên) ---
        private async void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await SearchAndDisplayWordAsync(); }
        }

        // --- Tìm kiếm và hiển thị (Giữ nguyên luồng gọi API -> Dịch -> Lưu/Lấy DB) ---
        private async Task SearchAndDisplayWordAsync()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm)) { ShowStatusMessage("Vui lòng nhập từ cần tìm.", true); return; }

            // Reset UI và trạng thái
            currentVocabulary = null;
            lblPronunciation.Text = "Phát âm:";
            lblMeaning.Text = "Nghĩa tiếng Việt:";
            UpdateActionButtonsState(false);
            ShowStatusMessage("Đang tìm kiếm...", false);
            this.Cursor = Cursors.WaitCursor;

            WordDetails apiResult = null; // API Client trả về WordDetails
            try
            {
                // 1. Gọi API trước tiên
                apiResult = await DictionaryApiClient.GetWordDetailsAsync(searchTerm);

                if (apiResult != null)
                {
                    // 2. Dịch nghĩa (nếu có)
                    if (!string.IsNullOrEmpty(apiResult.Meaning))
                    {
                        ShowStatusMessage("Đang dịch nghĩa...", false);
                        apiResult.Meaning = await DictionaryApiClient.TranslateToVietnamese(apiResult.Meaning);
                    }

                    // 3. Tạo đối tượng Vocabulary từ kết quả API (có thể chứa audio hoặc không)
                    Vocabulary vocabFromApi = new Vocabulary
                    {
                        Word = apiResult.Word ?? searchTerm,
                        Meaning = apiResult.Meaning,
                        Pronunciation = apiResult.Pronunciation,
                        AudioUrl = apiResult.AudioUrl
                    };

                    // 4. Gọi hàm SaveOrGetWordFromDatabase để xử lý logic DB (bao gồm cả cập nhật audio nếu cần)
                    currentVocabulary = SaveOrGetWordFromDatabase(vocabFromApi);

                    // 5. Cập nhật UI dựa trên kết quả trả về từ SaveOrGetWordFromDatabase
                    if (currentVocabulary != null)
                    {
                        lblPronunciation.Text = "Phát âm: " + (currentVocabulary.Pronunciation ?? "N/A");
                        lblMeaning.Text = "Nghĩa tiếng Việt: " + (currentVocabulary.Meaning ?? "N/A");
                        // Hiển thị thông báo thành công (hoặc đã tồn tại)
                        ShowStatusMessage($"Tìm thấy: {currentVocabulary.Word}", false);
                        UpdateActionButtonsState(true); // Cập nhật trạng thái nút (có check favorite)
                    }
                    else
                    {
                        // Trường hợp SaveOrGetWordFromDatabase trả về null (lỗi thêm mới)
                        ShowStatusMessage("Lỗi khi lưu từ vào cơ sở dữ liệu.", true);
                        UpdateActionButtonsState(false);
                    }
                }
                else // API không tìm thấy từ
                {
                    ShowStatusMessage($"Không tìm thấy từ '{searchTerm}'.", true);
                    UpdateActionButtonsState(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi SearchAndDisplayWordAsync: {ex}");
                ShowStatusMessage($"Lỗi khi tìm kiếm: {ex.Message}", true);
                UpdateActionButtonsState(false);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // --- Lưu hoặc lấy từ trong DB (Giữ nguyên) ---
        private Vocabulary SaveOrGetWordFromDatabase(Vocabulary vocabFromApi)
        {
            if (vocabFromApi == null || string.IsNullOrWhiteSpace(vocabFromApi.Word)) return null;

            try
            {
                // 1. Kiểm tra từ tồn tại bằng Word
                Vocabulary existingVocab = vocabRepo.GetVocabularyByWord(vocabFromApi.Word);

                if (existingVocab != null) // Từ đã tồn tại trong DB
                {
                    Debug.WriteLine($"[HomeControl] Word '{vocabFromApi.Word}' exists (ID: {existingVocab.Id}). Checking for updates...");

                    bool needsUpdate = false;
                    // 2. Kiểm tra và cập nhật AudioUrl nếu DB thiếu và API có
                    if (string.IsNullOrEmpty(existingVocab.AudioUrl) && !string.IsNullOrEmpty(vocabFromApi.AudioUrl))
                    {
                        Debug.WriteLine($"[HomeControl] Updating missing Audio URL from API result: {vocabFromApi.AudioUrl}");
                        existingVocab.AudioUrl = vocabFromApi.AudioUrl;
                        needsUpdate = true;
                    }

                    // 3. (Tùy chọn) Kiểm tra và cập nhật Pronunciation nếu DB thiếu và API có
                    if (string.IsNullOrEmpty(existingVocab.Pronunciation) && !string.IsNullOrEmpty(vocabFromApi.Pronunciation))
                    {
                        Debug.WriteLine($"[HomeControl] Updating missing Pronunciation from API result: {vocabFromApi.Pronunciation}");
                        existingVocab.Pronunciation = vocabFromApi.Pronunciation;
                        needsUpdate = true;
                    }

                    // 4. (Tùy chọn) Cập nhật Meaning - Cân nhắc kỹ
                    // if (existingVocab.Meaning != vocabFromApi.Meaning && !string.IsNullOrEmpty(vocabFromApi.Meaning))
                    // {
                    //      Debug.WriteLine($"[HomeControl] Updating Meaning from API result.");
                    //      existingVocab.Meaning = vocabFromApi.Meaning;
                    //      needsUpdate = true;
                    // }

                    // 5. Thực hiện Update trong DB nếu có thay đổi
                    if (needsUpdate)
                    {
                        Debug.WriteLine($"[HomeControl] Calling UpdateVocabulary for ID: {existingVocab.Id}");
                        bool updateSuccess = vocabRepo.UpdateVocabulary(existingVocab);
                        if (!updateSuccess)
                        {
                            Debug.WriteLine($"[HomeControl] Failed to update vocabulary in DB for ID: {existingVocab.Id}.");
                        }
                        else
                        {
                            Debug.WriteLine($"[HomeControl] Vocabulary updated successfully in DB for ID: {existingVocab.Id}.");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"[HomeControl] No updates needed for existing vocabulary ID: {existingVocab.Id}.");
                    }

                    // 6. Trả về object từ DB (đã được cập nhật nếu cần)
                    return existingVocab;
                }
                else // Từ chưa tồn tại trong DB
                {
                    // 7. Thêm mới từ thông tin lấy từ API (đã dịch nghĩa)
                    Debug.WriteLine($"[HomeControl] Word '{vocabFromApi.Word}' does not exist. Adding new vocabulary.");
                    Vocabulary addedVocab = vocabRepo.AddVocabulary(vocabFromApi); // Dùng AddVocabulary(Vocabulary) trả về object có Id

                    if (addedVocab != null)
                    {
                        Debug.WriteLine($"[HomeControl] Successfully added new vocabulary '{addedVocab.Word}' with ID: {addedVocab.Id}.");
                        LoadVocabularyCount();
                        return addedVocab; // Trả về object vừa thêm
                    }
                    else
                    {
                        Debug.WriteLine($"[HomeControl] Failed to add vocabulary '{vocabFromApi.Word}' to database.");
                        return null; // Trả về null nếu thêm thất bại
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HomeControl] Error in SaveOrGetWordFromDatabase for '{vocabFromApi.Word}': {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }
        }

        // --- Phát âm (Giữ nguyên) ---
        private void BtnPlayAudio_Click(object sender, EventArgs e)
        {
            if (currentVocabulary == null || string.IsNullOrEmpty(currentVocabulary.AudioUrl)) { ShowStatusMessage("Không có âm thanh hoặc chưa tìm từ.", true); return; }
            try { AudioHelper.PlayAudio(currentVocabulary.AudioUrl); }
            catch (Exception ex) { Debug.WriteLine($"Lỗi phát âm thanh: {ex}"); ShowStatusMessage("Lỗi khi phát âm thanh.", true); }
        }

        // --- Thêm Yêu thích (Giữ nguyên) ---
        private void BtnAddFavorite_Click(object sender, EventArgs e)
        {
            if (currentVocabulary == null || currentVocabulary.Id <= 0) { ShowStatusMessage("Chưa có từ hợp lệ để thêm vào yêu thích.", true); return; }
            try
            {
                bool success = vocabRepo.AddFavorite(currentVocabulary.Id);
                if (success) { ShowStatusMessage("⭐ Đã thêm vào Yêu thích / Đã có!", false); UpdateActionButtonsState(true); }
                else { ShowStatusMessage("Lỗi khi thêm từ vào yêu thích.", true); }
            }
            catch (Exception ex) { Debug.WriteLine($"Lỗi BtnAddFavorite_Click: {ex}"); ShowStatusMessage("Lỗi không xác định khi thêm yêu thích.", true); }
        }

        // --- Thêm vào Chủ đề (Giữ nguyên) ---
        private void BtnAddTopic_Click(object sender, EventArgs e)
        {
            if (currentVocabulary == null || string.IsNullOrWhiteSpace(currentVocabulary.Word)) { ShowStatusMessage("Chưa có từ hợp lệ để thêm vào chủ đề.", true); return; }
            var form = new AddToTopicForm(currentVocabulary.Word);
            form.ShowDialog(this.FindForm());
        }

        // --- Cập nhật trạng thái nút (Giữ nguyên) ---
        private void UpdateActionButtonsState(bool enable)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => UpdateActionButtonsState(enable))); return; }

            if (enable && currentVocabulary != null && currentVocabulary.Id > 0)
            {
                btnPlayAudio.Enabled = !string.IsNullOrEmpty(currentVocabulary.AudioUrl); // Bật nếu có AudioUrl
                btnAddTopic.Enabled = true;
                try
                {
                    bool isFav = vocabRepo.IsFavorite(currentVocabulary.Id);
                    if (isFav) { btnAddFavorite.Text = "❤️ Đã thích"; btnAddFavorite.Enabled = false; btnAddFavorite.BackColor = Color.LightPink; }
                    else { btnAddFavorite.Text = "⭐ Yêu thích"; btnAddFavorite.Enabled = true; btnAddFavorite.BackColor = SystemColors.Control; }
                }
                catch (Exception ex) { Debug.WriteLine($"Lỗi kiểm tra IsFavorite: {ex.Message}"); btnAddFavorite.Text = "⭐ Yêu thích"; btnAddFavorite.Enabled = true; btnAddFavorite.BackColor = SystemColors.Control; }
            }
            else { btnPlayAudio.Enabled = false; btnAddFavorite.Enabled = false; btnAddTopic.Enabled = false; btnAddFavorite.Text = "⭐ Yêu thích"; btnAddFavorite.BackColor = SystemColors.Control; }
        }

        // --- Hiển thị thông báo trạng thái (Giữ nguyên) ---
        private void ShowStatusMessage(string message, bool isError)
        {
            if (lblStatusMessage.InvokeRequired) { lblStatusMessage.Invoke(new Action(() => ShowStatusMessage(message, isError))); return; }
            lblStatusMessage.Text = message; lblStatusMessage.ForeColor = isError ? Color.Red : Color.DarkGreen; lblStatusMessage.Visible = true;
            statusTimer.Stop(); statusTimer.Start();
        }

        // --- Sự kiện Tick của Timer để ẩn thông báo (Giữ nguyên) ---
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            statusTimer.Stop();
            if (lblStatusMessage.InvokeRequired) { lblStatusMessage.Invoke(new Action(() => { lblStatusMessage.Visible = false; lblStatusMessage.Text = ""; })); }
            else { lblStatusMessage.Visible = false; lblStatusMessage.Text = ""; }
        }
        /// <summary>
        /// Tải tổng số từ vựng và cập nhật Label.
        /// </summary>
        private void LoadVocabularyCount()
        {
            try
            {
                int count = vocabRepo.GetVocabularyCount();

                // Cập nhật UI trên UI thread
                if (lblVocabularyCount.InvokeRequired)
                {
                    lblVocabularyCount.Invoke(new Action(() =>
                    {
                        lblVocabularyCount.Text = $"Số từ vựng có sẵn: {count} từ";
                        lblVocabularyCount.ForeColor = Color.DimGray; // Đặt lại màu nếu trước đó là lỗi
                    }));
                }
                else
                {
                    lblVocabularyCount.Text = $"Số từ vựng có sẵn: {count} từ";
                    lblVocabularyCount.ForeColor = Color.DimGray; // Đặt lại màu nếu trước đó là lỗi
                }
                Debug.WriteLine($"[HomeControl] Vocabulary count updated: {count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HomeControl] Error loading vocabulary count: {ex.Message}");
                // Hiển thị lỗi trên UI
                if (lblVocabularyCount.InvokeRequired)
                {
                    lblVocabularyCount.Invoke(new Action(() =>
                    {
                        lblVocabularyCount.Text = "Lỗi tải số lượng từ!";
                        lblVocabularyCount.ForeColor = Color.Red;
                    }));
                }
                else
                {
                    lblVocabularyCount.Text = "Lỗi tải số lượng từ!";
                    lblVocabularyCount.ForeColor = Color.Red;
                }
            }
        }
    }
}