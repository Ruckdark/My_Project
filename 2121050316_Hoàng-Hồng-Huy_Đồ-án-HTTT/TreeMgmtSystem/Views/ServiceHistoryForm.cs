using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;

namespace TreeMgmtSystem.Views
{
    public partial class ServiceHistoryForm : Form
    {
        private int serviceId;
        private DatabaseQueries dbQueries;

        public ServiceHistoryForm(int serviceId)
        {
            InitializeComponent();
            this.serviceId = serviceId;
            this.dbQueries = new DatabaseQueries();
            LoadServiceHistory();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadServiceHistory()
        {
            try
            {
                DataTable serviceHistory = dbQueries.GetServiceHistory(serviceId);
                dgvServiceHistory.DataSource = serviceHistory;

                // Thiết lập tên cột hiển thị
                dgvServiceHistory.Columns["ActionType"].HeaderText = "Loại hành động";
                dgvServiceHistory.Columns["ActionDescription"].HeaderText = "Mô tả hành động";
                dgvServiceHistory.Columns["ActionDate"].HeaderText = "Ngày thực hiện";
                dgvServiceHistory.Columns["UserId"].HeaderText = "ID người dùng";

                // Tự động điều chỉnh kích thước cột
                dgvServiceHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
