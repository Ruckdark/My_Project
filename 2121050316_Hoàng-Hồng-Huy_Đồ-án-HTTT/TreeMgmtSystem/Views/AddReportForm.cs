using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class AddReportForm : Form
    {
        private DatabaseQueries dbQueries;

        public AddReportForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();

            // Điền UserId hiện tại vào trường txtUserId và ẩn nó
            txtUserId.Text = CurrentSession.UserId.ToString();
            txtUserId.Visible = false;
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                string reportContent = txtReportContent.Text;
                int userId = CurrentSession.UserId;

                // Thêm báo cáo mới vào cơ sở dữ liệu
                bool isReportAdded = dbQueries.SendReport(userId, reportContent);

                if (isReportAdded)
                {
                    MessageBox.Show("Báo cáo mới đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm báo cáo thất bại. Hãy thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm báo cáo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
