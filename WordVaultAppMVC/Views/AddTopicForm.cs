using System.Collections.Generic;
using System.Windows.Forms;
using System;

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

        // Sự kiện này sẽ được gọi khi người dùng nhấn nút "Thêm từ vựng"
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

        // Sự kiện này sẽ được gọi khi người dùng nhấn nút "Lưu chủ đề"
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
            // Giả sử bạn sử dụng một phương thức như SaveTopicToDatabase(topicName, vocabularyList) để lưu.
            SaveTopicToDatabase(topicName, vocabularyList);

            MessageBox.Show("Chủ đề và từ vựng đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form sau khi lưu thành công.
        }

        // Phương thức lưu chủ đề và từ vựng vào cơ sở dữ liệu
        private void SaveTopicToDatabase(string topicName, List<string> vocabularyList)
        {
            // Thêm mã lưu trữ vào cơ sở dữ liệu ở đây.
            // Ví dụ: Gửi chủ đề và danh sách từ vựng đến cơ sở dữ liệu.
        }

        // Sự kiện này sẽ được gọi khi người dùng nhấn nút "Hủy"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form mà không lưu.
        }

        // Sự kiện khi người dùng nhấn "Thêm chủ đề mới"
        private void btnAddTopic_Click(object sender, EventArgs e)
        {
            // Tạo mới một chủ đề và từ vựng
            string topicName = txtTopicName.Text.Trim();

            if (string.IsNullOrEmpty(topicName))
            {
                MessageBox.Show("Vui lòng nhập tên chủ đề.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Thêm chủ đề vào danh sách chủ đề
            lstVocabulary.Items.Clear(); // Làm sạch danh sách từ vựng cũ trước khi thêm chủ đề mới
            vocabularyList.Clear(); // Xóa danh sách từ vựng hiện tại

            MessageBox.Show($"Chủ đề \"{topicName}\" đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Bạn có thể thêm một đoạn mã lưu dữ liệu vào cơ sở dữ liệu hoặc xử lý theo cách khác
        }
    }
}
