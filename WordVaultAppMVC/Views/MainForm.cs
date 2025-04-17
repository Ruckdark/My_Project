using System;
using System.Windows.Forms;
using WordVaultAppMVC.Helpers;
using WordVaultAppMVC.Models;
using System.Data.SqlClient;
using WordVaultAppMVC.Data;
using System.Threading.Tasks;

namespace WordVaultAppMVC.Views
{
    public partial class MainForm : Form
    {
        MainForm homeForm;
        public MainForm()
        {
            InitializeComponent();
        }

        // Khi người dùng nhấn tìm kiếm (dùng async/await)
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchAndDisplayWordAsync();
        }

        // Khi người dùng nhấn Enter trong ô tìm kiếm
        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await SearchAndDisplayWordAsync();
            }
        }

        // Tìm kiếm và hiển thị từ
        private async Task SearchAndDisplayWordAsync()
        {
            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập từ cần tìm.");
                return;
            }

            var result = await DictionaryApiClient.GetWordDetailsAsync(searchTerm);

            if (result != null)
            {
                // Gọi API dịch nghĩa sang tiếng Việt
                result.Meaning = await DictionaryApiClient.TranslateToVietnamese(result.Meaning);

                lblPronunciation.Text = "Phát âm: " + result.Pronunciation;
                lblMeaning.Text = "Nghĩa tiếng Việt: " + result.Meaning;

                SaveWordToDatabase(result);
            }
            else
            {
                MessageBox.Show("Không tìm thấy từ này trong từ điển.");
            }
        }


        // Lưu từ vào cơ sở dữ liệu
        private void SaveWordToDatabase(WordDetails wordDetails)
        {
            using (var connection = DatabaseContext.GetConnection())
            {
                connection.Open();

                // Kiểm tra trùng: chỉ định schema "dbo" nếu cần
                var checkQuery = "SELECT COUNT(*) FROM dbo.Vocabulary WHERE Word = @Word";
                using (var checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Word", wordDetails.Word);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        //MessageBox.Show("Từ này đã tồn tại trong cơ sở dữ liệu.");
                        return;
                    }
                }

                // Insert nếu chưa có: chỉ định schema "dbo" nếu bảng nằm trong dbo
                var insertQuery = "INSERT INTO dbo.Vocabulary (Word, Pronunciation, AudioUrl, Meaning) VALUES (@Word, @Pronunciation, @AudioUrl, @Meaning)";
                using (var insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Word", wordDetails.Word);
                    insertCommand.Parameters.AddWithValue("@Pronunciation", wordDetails.Pronunciation);
                    insertCommand.Parameters.AddWithValue("@AudioUrl", wordDetails.AudioUrl);
                    insertCommand.Parameters.AddWithValue("@Meaning", wordDetails.Meaning);

                    insertCommand.ExecuteNonQuery();
                }
            }
            //MessageBox.Show("Từ vựng đã được lưu vào cơ sở dữ liệu.");
        }


        // Khi người dùng nhấn nút phát âm
        private void btnPlayAudio_Click(object sender, EventArgs e)
        {
            string audioUrl = GetAudioUrlFromDb();

            if (!string.IsNullOrEmpty(audioUrl))
            {
                AudioHelper.PlayAudio(audioUrl);
            }
            else
            {
                MessageBox.Show("Không có âm thanh để phát.");
            }
        }

        // Lấy URL âm thanh từ cơ sở dữ liệu
        private string GetAudioUrlFromDb()
        {
            string audioUrl = "";

            using (var connection = DatabaseContext.GetConnection())
            {
                connection.Open();
                var query = "SELECT AudioUrl FROM Vocabulary WHERE Word = @Word";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Word", txtSearch.Text.Trim());

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        audioUrl = result.ToString();
                    }
                }
            }

            return audioUrl;
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            // Làm mới giao diện hiện tại thay vì mở form mới
            txtSearch.Clear();
            lblPronunciation.Text = "Phát âm:";
            lblMeaning.Text = "Nghĩa tiếng Việt:";
        }
        private void btnTopicVocabulary_Click(object sender, EventArgs e)
        {
            var topicForm = new TopicVocabularyForm("Chủ đề mặc định"); // hoặc lấy chủ đề từ DB
            topicForm.ShowDialog();
        }

        private void btnFavorite_Click(object sender, EventArgs e)
        {
            var favoriteForm = new FavoriteWordsForm(); // Form này bạn có thể tự tạo
            favoriteForm.ShowDialog();
        }

        private void btnDailyReview_Click(object sender, EventArgs e)
        {
            var reviewForm = new DailyReviewForm();
            reviewForm.ShowDialog();
        }

        private void btnQuiz_Click(object sender, EventArgs e)
        {
            var quizForm = new QuizForm();
            quizForm.ShowDialog();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            var shuffleForm = new ShuffleStudyForm();
            shuffleForm.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(); // bạn có thể tạo form đơn giản này
            settingsForm.ShowDialog();
        }

        //private void btnFavorite_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("⭐ Tính năng Yêu thích đang được phát triển.");
        //}

        //private void btnQuiz_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("🧠 Quiz sẽ sớm có mặt!");
        //}

        //private void btnShuffle_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("🔀 Chức năng xáo trộn đang được phát triển.");
        //}

        //private void btnDailyReview_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("📅 Học từ hàng ngày sẽ được cập nhật.");
        //}


        //private void btnTopicVocabulary_Click(object sender, EventArgs e)
        //{
        //    // Điều hướng đến danh sách từ vựng (hoặc form khác)
        //    // Ví dụ: mở form TopicVocabularyForm
        //    //this.Hide();
        //    //var topicVocabularyForm = new TopicVocabularyForm();
        //    //topicVocabularyForm.Show();
        //}

        //private void btnSettings_Click(object sender, EventArgs e)
        //{
        //    // Điều hướng đến form Settings
        //    //this.Hide();
        //    //var settingsForm = new SettingsForm();
        //    //settingsForm.Show();
        //}

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    // Xử lý khi form đóng
        //    var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.No)
        //    {
        //        e.Cancel = true; // Hủy thao tác đóng form
        //    }
        //    else
        //    {
        //        Application.Exit(); // Thoát chương trình
        //    }
        //}
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
