using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WordVaultAppMVC.Data;

namespace WordVaultAppMVC.Views
{
    public partial class AddTopicForm : Form
    {
        private List<string> vocabularyList = new List<string>();
        private TextBox txtNewVocabulary;
        private ListBox lstVocabulary;

        public AddTopicForm()
        {
            InitializeComponent();
        }

        // Sự kiện khi người dùng nhấn nút "Thêm từ vựng"
        private void btnAddVocabulary_Click(object sender, EventArgs e)
        {
            string newVocabulary = txtNewVocabulary.Text.Trim();
            if (!string.IsNullOrEmpty(newVocabulary))
            {
                vocabularyList.Add(newVocabulary);
                lstVocabulary.Items.Add(newVocabulary);
                txtNewVocabulary.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ vựng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện khi người dùng nhấn nút "Lưu chủ đề"
        private void btnSave_Click(object sender, EventArgs e)
        {
            string topicName = txtTopicName.Text.Trim();
            if (string.IsNullOrEmpty(topicName))
            {
                MessageBox.Show("Vui lòng nhập tên chủ đề.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (vocabularyList.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một từ vựng vào chủ đề.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lưu chủ đề và từ vựng vào cơ sở dữ liệu.
            SaveTopicToDatabase(topicName, vocabularyList);

            MessageBox.Show("Chủ đề và từ vựng đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form sau khi lưu thành công.
        }

        // Cài đặt phương thức lưu chủ đề và danh sách từ vựng vào cơ sở dữ liệu
        private void SaveTopicToDatabase(string topicName, List<string> vocabularyList)
        {
            using (var connection = DatabaseContext.GetConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int topicId = 0;
                        // Chèn chủ đề mới và lấy Id chủ đề
                        string insertTopicQuery = "INSERT INTO Topics (Name) OUTPUT INSERTED.Id VALUES (@Name)";
                        using (SqlCommand cmdTopic = new SqlCommand(insertTopicQuery, connection, transaction))
                        {
                            cmdTopic.Parameters.AddWithValue("@Name", topicName);
                            topicId = (int)cmdTopic.ExecuteScalar();
                        }

                        // Với mỗi từ vựng, kiểm tra, chèn (nếu cần), và lưu record vào bảng liên kết VocabularyTopic
                        foreach (var word in vocabularyList)
                        {
                            int vocabularyId = 0;
                            // Kiểm tra xem từ vựng đã tồn tại chưa
                            string checkVocabularyQuery = "SELECT Id FROM Vocabulary WHERE Word = @Word";
                            using (SqlCommand cmdCheck = new SqlCommand(checkVocabularyQuery, connection, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@Word", word);
                                var result = cmdCheck.ExecuteScalar();
                                if (result != null)
                                {
                                    vocabularyId = (int)result;
                                }
                            }

                            // Nếu chưa tồn tại, chèn mới vào bảng Vocabulary (với các thông tin còn lại rỗng hoặc mặc định)
                            if (vocabularyId == 0)
                            {
                                string insertVocabularyQuery = "INSERT INTO Vocabulary (Word, Meaning, Pronunciation, AudioUrl) OUTPUT INSERTED.Id VALUES (@Word, '', '', '')";
                                using (SqlCommand cmdInsertVocab = new SqlCommand(insertVocabularyQuery, connection, transaction))
                                {
                                    cmdInsertVocab.Parameters.AddWithValue("@Word", word);
                                    vocabularyId = (int)cmdInsertVocab.ExecuteScalar();
                                }
                            }

                            // Chèn record vào bảng liên kết VocabularyTopic
                            string insertVocabularyTopicQuery = "INSERT INTO VocabularyTopic (VocabularyId, TopicId) VALUES (@VocabularyId, @TopicId)";
                            using (SqlCommand cmdVT = new SqlCommand(insertVocabularyTopicQuery, connection, transaction))
                            {
                                cmdVT.Parameters.AddWithValue("@VocabularyId", vocabularyId);
                                cmdVT.Parameters.AddWithValue("@TopicId", topicId);
                                cmdVT.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi khi lưu chủ đề và từ vựng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Sự kiện khi người dùng nhấn nút "Hủy"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form mà không lưu.
        }

        // Sự kiện khi người dùng nhấn "Thêm chủ đề mới"
        private void btnAddTopic_Click(object sender, EventArgs e)
        {
            // Reset lại form để thêm chủ đề mới
            string topicName = txtTopicName.Text.Trim();
            if (string.IsNullOrEmpty(topicName))
            {
                MessageBox.Show("Vui lòng nhập tên chủ đề.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lstVocabulary.Items.Clear();
            vocabularyList.Clear();
            MessageBox.Show($"Chủ đề \"{topicName}\" đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
