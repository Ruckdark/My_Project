using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class AddServiceForm : Form
    {
        private DatabaseQueries dbQueries;

        public AddServiceForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();

            // Điền UserId hiện tại vào trường txtUserId và ẩn nó
            txtUserId.Text = CurrentSession.UserId.ToString();
            txtUserId.Visible = false;

            // Thêm các dịch vụ vào ComboBox
            PopulateServiceTypeComboBox();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void PopulateServiceTypeComboBox()
        {
            comboBoxServiceType.Items.AddRange(new string[]
            {
                "Trồng mới",
                "Tưới nước",
                "Bón phân",
                "Phun thuốc",
                "Cắt tỉa",
                "Kiểm tra sức khỏe",
                "Chặt cây",
                "Trồng lại"
            });
            comboBoxServiceType.SelectedIndex = 0; // Chọn giá trị mặc định
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                Service service = new Service
                {
                    ServiceType = comboBoxServiceType.SelectedItem.ToString(),
                    RequestDate = dateTimePickerRequestDate.Value,
                    UserId = CurrentSession.UserId
                };

                // Thêm dịch vụ mới vào cơ sở dữ liệu
                bool isServiceAdded = dbQueries.AddService(service);

                if (isServiceAdded)
                {
                    MessageBox.Show("Dịch vụ mới đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm dịch vụ thất bại. Hãy thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dịch vụ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
