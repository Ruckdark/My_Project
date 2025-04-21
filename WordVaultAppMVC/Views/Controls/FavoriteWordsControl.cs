using System;
using System.Collections.Generic;
using System.Data.SqlClient; // Cần nếu dùng trực tiếp hoặc cho Exception
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordVaultAppMVC.Data;     // Namespace chứa Repositories
using WordVaultAppMVC.Models;   // Namespace chứa Models
using WordVaultAppMVC.Helpers;  // Namespace của AudioHelper
// using WordVaultAppMVC.Views; // Namespace của VocabularyDetailPanel (nếu khác)

namespace WordVaultAppMVC.Views.Controls
{
    public class FavoriteWordsControl : UserControl
    {
        // --- UI Controls ---
        private ListBox lstFavoriteWords;
        private VocabularyDetailPanel vocabularyDetailPanel; // Tái sử dụng control đã có
        private Button btnRemoveFavorite;
        private Button btnPlayAudio;
        private TableLayoutPanel mainLayout;
        private Label lblTitle;

        // --- Logic Fields ---
        private VocabularyRepository vocabRepo;
        private List<Vocabulary> favoriteList; // Lưu danh sách từ yêu thích hiện tại

        public FavoriteWordsControl()
        {
            vocabRepo = new VocabularyRepository();
            InitializeComponent();

            // Load danh sách yêu thích khi control được hiển thị lần đầu
            // Sử dụng sự kiện Load thay vì gọi trực tiếp trong constructor
            // để đảm bảo handle của control đã được tạo.
            this.Load += FavoriteWordsControl_Load;
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = SystemColors.ControlLightLight; // Nền trắng

            // --- Main Layout ---
            mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2, // Chia 2 cột: Danh sách và Chi tiết
                RowCount = 3,    // Hàng cho Title, Content, Buttons
                Padding = new Padding(15)
            };
            // Cột danh sách chiếm ít hơn, cột chi tiết chiếm nhiều hơn
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Hàng 0: Title
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Hàng 1: Content (List + Detail)
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Hàng 2: Buttons

            // --- Title ---
            lblTitle = new Label
            {
                Text = "⭐ Danh sách từ yêu thích",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.DarkGoldenrod, // Màu khác cho nổi bật
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 15) // Khoảng cách dưới
            };
            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.SetColumnSpan(lblTitle, 2); // Title kéo dài 2 cột

            // --- ListBox (Cột 0, Hàng 1) ---
            lstFavoriteWords = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                IntegralHeight = false // Cho phép scroll tốt hơn
            };
            lstFavoriteWords.SelectedIndexChanged += LstFavoriteWords_SelectedIndexChanged; // Bắt sự kiện chọn
            mainLayout.Controls.Add(lstFavoriteWords, 0, 1);

            // --- Detail Panel (Cột 1, Hàng 1) ---
            vocabularyDetailPanel = new VocabularyDetailPanel // Khởi tạo control có sẵn
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle, // Thêm viền cho rõ
                Padding = new Padding(10),
                Visible = false // Chỉ hiển thị khi có từ được chọn
            };
            mainLayout.Controls.Add(vocabularyDetailPanel, 1, 1);

            // --- Buttons Panel (Hàng 2) ---
            btnRemoveFavorite = new Button
            {
                Text = "🗑️ Xóa khỏi Yêu thích",
                AutoSize = true,
                BackColor = Color.MistyRose,
                ForeColor = Color.DarkRed,
                Font = new Font("Segoe UI", 9F),
                Enabled = false, // Chỉ bật khi có từ được chọn
                FlatStyle = FlatStyle.Flat
            };
            btnRemoveFavorite.FlatAppearance.BorderColor = Color.IndianRed;
            btnRemoveFavorite.FlatAppearance.BorderSize = 1;
            btnRemoveFavorite.Click += BtnRemoveFavorite_Click;

            btnPlayAudio = new Button
            {
                Text = "🔊 Nghe",
                AutoSize = true,
                BackColor = Color.SkyBlue,
                Font = new Font("Segoe UI", 9F),
                Enabled = false, // Chỉ bật khi có từ được chọn và có audio
                FlatStyle = FlatStyle.Flat
            };
            btnPlayAudio.FlatAppearance.BorderColor = Color.SteelBlue;
            btnPlayAudio.FlatAppearance.BorderSize = 1;
            btnPlayAudio.Click += BtnPlayAudio_Click;

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, // Fill hàng 2
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Padding = new Padding(0, 10, 0, 0) // Khoảng cách trên
            };
            buttonPanel.Controls.Add(btnRemoveFavorite);
            buttonPanel.Controls.Add(btnPlayAudio);
            mainLayout.Controls.Add(buttonPanel, 0, 2);
            mainLayout.SetColumnSpan(buttonPanel, 2); // Buttons kéo dài 2 cột

            // --- Add main layout to control ---
            this.Controls.Add(mainLayout);
        }

        // --- Load event: Tải dữ liệu khi control được hiển thị ---
        private void FavoriteWordsControl_Load(object sender, EventArgs e)
        {
            // Chỉ load một lần đầu tiên, hoặc có thể thêm nút Refresh nếu muốn
            if (favoriteList == null)
            {
                LoadFavoriteWords();
            }
        }

        // --- LoadFavoriteWords: Lấy và hiển thị danh sách từ yêu thích ---
        private void LoadFavoriteWords()
        {
            Debug.WriteLine("[FavoriteWordsControl] Loading favorite words...");
            try
            {
                // **QUAN TRỌNG:** Giả định vocabRepo có hàm GetFavoriteVocabularies()
                // Hàm này cần thực hiện truy vấn JOIN giữa Vocabulary và FavoriteWords
                favoriteList = vocabRepo.GetFavoriteVocabularies(); // Lấy danh sách Vocabulary yêu thích

                lstFavoriteWords.Items.Clear(); // Xóa danh sách cũ
                vocabularyDetailPanel.Visible = false; // Ẩn panel chi tiết
                btnRemoveFavorite.Enabled = false; // Tắt nút
                btnPlayAudio.Enabled = false;   // Tắt nút

                if (favoriteList != null && favoriteList.Any())
                {
                    // Thêm các từ vào ListBox
                    // Dùng DisplayMember và ValueMember để lưu trữ cả object Vocabulary
                    lstFavoriteWords.DisplayMember = "Word"; // Hiển thị trường Word
                    lstFavoriteWords.ValueMember = "Id";   // Giá trị có thể là Id (hoặc chính object)
                    foreach (var vocab in favoriteList)
                    {
                        lstFavoriteWords.Items.Add(vocab);
                    }
                    Debug.WriteLine($"[FavoriteWordsControl] Loaded {favoriteList.Count} favorite words.");
                }
                else
                {
                    lstFavoriteWords.Items.Add("(Chưa có từ yêu thích nào)");
                    lstFavoriteWords.Enabled = false; // Vô hiệu hóa list nếu rỗng
                    Debug.WriteLine("[FavoriteWordsControl] No favorite words found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FavoriteWordsControl] Error loading favorites: {ex.Message}");
                MessageBox.Show("Lỗi khi tải danh sách từ yêu thích.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lstFavoriteWords.Items.Add("(Lỗi tải dữ liệu)");
                lstFavoriteWords.Enabled = false;
            }
        }

        // --- LstFavoriteWords_SelectedIndexChanged: Hiển thị chi tiết khi chọn từ ---
        private void LstFavoriteWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy đối tượng Vocabulary được chọn từ ListBox
            Vocabulary selectedVocab = lstFavoriteWords.SelectedItem as Vocabulary;

            if (selectedVocab != null)
            {
                vocabularyDetailPanel.DisplayVocabulary(selectedVocab); // Hiển thị chi tiết
                vocabularyDetailPanel.Visible = true;
                btnRemoveFavorite.Enabled = true; // Bật nút xóa
                btnPlayAudio.Enabled = !string.IsNullOrEmpty(selectedVocab.AudioUrl); // Bật nút nghe nếu có URL
                // Lưu trữ AudioUrl vào Tag của nút Play để dễ truy cập
                btnPlayAudio.Tag = selectedVocab.AudioUrl;
            }
            else
            {
                // Nếu không chọn được (ví dụ: chọn dòng thông báo lỗi/rỗng)
                vocabularyDetailPanel.Visible = false;
                btnRemoveFavorite.Enabled = false;
                btnPlayAudio.Enabled = false;
                btnPlayAudio.Tag = null;
            }
        }

        // --- BtnRemoveFavorite_Click: Xóa từ khỏi danh sách yêu thích ---
        private void BtnRemoveFavorite_Click(object sender, EventArgs e)
        {
            Vocabulary selectedVocab = lstFavoriteWords.SelectedItem as Vocabulary;
            if (selectedVocab == null) return;

            var confirmResult = MessageBox.Show($"Bạn có chắc muốn xóa từ '{selectedVocab.Word}' khỏi danh sách yêu thích?",
                                               "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // **QUAN TRỌNG:** Giả định vocabRepo có hàm RemoveFavorite(int vocabularyId)
                    bool success = vocabRepo.RemoveFavorite(selectedVocab.Id);

                    if (success)
                    {
                        MessageBox.Show("Đã xóa khỏi danh sách yêu thích.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFavoriteWords(); // Tải lại danh sách sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa từ yêu thích. Có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[FavoriteWordsControl] Error removing favorite (ID: {selectedVocab.Id}): {ex.Message}");
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- BtnPlayAudio_Click: Phát âm thanh ---
        private void BtnPlayAudio_Click(object sender, EventArgs e)
        {
            // Lấy AudioUrl từ Tag đã lưu trước đó
            string audioUrl = btnPlayAudio.Tag as string;

            if (!string.IsNullOrEmpty(audioUrl))
            {
                try
                {
                    AudioHelper.PlayAudio(audioUrl);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[FavoriteWordsControl] Error playing audio: {ex.Message}");
                    MessageBox.Show("Không thể phát âm thanh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Trường hợp này ít xảy ra vì nút đã bị disable nếu không có URL
                Debug.WriteLine("[FavoriteWordsControl] Play button clicked but AudioUrl is null/empty.");
            }
        }
    }
}