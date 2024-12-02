using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class UserForm : Form
    {
        private DatabaseQueries dbQueries;
        private AddServiceForm addServiceForm;
        private AddReportForm addReportForm;
        private AddTreeForm addTreeForm;
        public UserForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            LoadUserData();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadUserData()
        {
            try
            {
                DataTable treeData = dbQueries.GetTreesForUser(CurrentSession.UserId); // Truyền UserId từ CurrentSession

                // Đặt dữ liệu cho DataGridView
                dataGridView1.DataSource = treeData;

                // Thiết lập tên cột hiển thị
                dataGridView1.Columns["TreeId"].HeaderText = "Tree ID";
                dataGridView1.Columns["Species"].HeaderText = "Loại cây";
                dataGridView1.Columns["Location"].HeaderText = "Vị trí";
                dataGridView1.Columns["HealthStatus"].HeaderText = "Tình trạng";
                dataGridView1.Columns["ReminderDate"].HeaderText = "Ngày nhắc nhở";

                // Tùy chỉnh kích thước cột
                dataGridView1.Columns["TreeId"].Width = 100;
                dataGridView1.Columns["Species"].Width = 200;
                dataGridView1.Columns["Location"].Width = 200;
                dataGridView1.Columns["HealthStatus"].Width = 150;
                dataGridView1.Columns["ReminderDate"].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức để người dùng yêu cầu dịch vụ
        private void btnRequestService_Click(object sender, EventArgs e)
        {
            // Mở form AddServiceForm
            if (addServiceForm == null)
                addServiceForm = new AddServiceForm();
            addServiceForm.ShowDialog();
        }

        // Phương thức để người dùng gửi báo cáo
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (addReportForm == null)
                addReportForm = new AddReportForm();
            addReportForm.ShowDialog();
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentSession.ResetSession();
            Application.Exit();
        }

        private void btnAddTree_Click(object sender, EventArgs e)
        {
            if (addTreeForm == null)
                addTreeForm = new AddTreeForm();
            addTreeForm.ShowDialog();
            LoadUserData();
        }

        private void UserForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
