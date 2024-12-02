using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;

namespace TreeMgmtSystem
{
    public partial class EditServiceForm : Form
    {
        private DatabaseQueries dbQueries;
        private int serviceId;

        public EditServiceForm(int serviceId)
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            this.serviceId = serviceId;
            LoadServiceData();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadServiceData()
        {
            try
            {
                Service service = dbQueries.GetServiceById(serviceId);
                if (service != null)
                {
                    txtServiceType.Text = service.ServiceType;
                    dateTimePickerRequestDate.Value = service.RequestDate;
                    txtStatus.Text = service.Status;
                    txtUserId.Text = service.UserId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu dịch vụ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                Service service = new Service
                {
                    ServiceId = serviceId,
                    ServiceType = txtServiceType.Text,
                    RequestDate = dateTimePickerRequestDate.Value,
                    Status = txtStatus.Text,
                    UserId = int.Parse(txtUserId.Text)
                };

                // Cập nhật dịch vụ vào cơ sở dữ liệu
                dbQueries.UpdateService(service);

                MessageBox.Show("Thông tin dịch vụ đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dịch vụ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
