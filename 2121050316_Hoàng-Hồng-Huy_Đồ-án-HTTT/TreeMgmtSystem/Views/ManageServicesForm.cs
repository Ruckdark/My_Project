using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Views;

namespace TreeMgmtSystem
{
    public partial class ManageServicesForm : Form
    {
        private DatabaseQueries dbQueries;

        public ManageServicesForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            LoadServiceData();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadServiceData()
        {
            try
            {
                DataTable serviceData = dbQueries.GetServices();

                // Đặt dữ liệu cho DataGridView
                dataGridViewServices.DataSource = serviceData;

                // Thiết lập tên cột hiển thị
                dataGridViewServices.Columns["ServiceId"].HeaderText = "Service ID";
                dataGridViewServices.Columns["ServiceType"].HeaderText = "Loại dịch vụ";
                dataGridViewServices.Columns["RequestDate"].HeaderText = "Ngày yêu cầu";
                dataGridViewServices.Columns["Status"].HeaderText = "Trạng thái";
                dataGridViewServices.Columns["UserId"].HeaderText = "User ID";

                // Tùy chỉnh kích thước cột
                dataGridViewServices.Columns["ServiceId"].Width = 100;
                dataGridViewServices.Columns["ServiceType"].Width = 150;
                dataGridViewServices.Columns["RequestDate"].Width = 150;
                dataGridViewServices.Columns["Status"].Width = 150;
                dataGridViewServices.Columns["UserId"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            // Mở form thêm dịch vụ mới
            AddServiceForm addServiceForm = new AddServiceForm();
            addServiceForm.ShowDialog();
            LoadServiceData();
        }

        private void btnUpdateService_Click(object sender, EventArgs e)
        {
            // Mở form chỉnh sửa dịch vụ được chọn
            int selectedRowIndex = dataGridViewServices.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridViewServices.Rows[selectedRowIndex];
            int serviceId = Convert.ToInt32(selectedRow.Cells["ServiceId"].Value);

            EditServiceForm editServiceForm = new EditServiceForm(serviceId);
            editServiceForm.ShowDialog();
            LoadServiceData();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có ô nào được chọn hay không
                if (dataGridViewServices.SelectedCells.Count > 0)
                {
                    // Lấy serviceId từ cột ServiceId của hàng được chọn
                    int selectedRowIndex = dataGridViewServices.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridViewServices.Rows[selectedRowIndex];
                    int serviceId = Convert.ToInt32(selectedRow.Cells["ServiceId"].Value);

                    // Hiển thị thông báo xác nhận
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        // Xóa dịch vụ được chọn
                        dbQueries.DeleteService(serviceId);
                        LoadServiceData();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một dịch vụ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Sự kiện nhấp đúp vào một dòng trong DataGridView để hiển thị lịch sử của dịch vụ
        private void DataGridViewServices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedRowIndex = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewServices.Rows[selectedRowIndex];
                int serviceId = Convert.ToInt32(selectedRow.Cells["ServiceId"].Value);

                // Mở form hiển thị lịch sử của dịch vụ
                ServiceHistoryForm serviceHistoryForm = new ServiceHistoryForm(serviceId);
                serviceHistoryForm.ShowDialog();
            }
        }
    }
}
