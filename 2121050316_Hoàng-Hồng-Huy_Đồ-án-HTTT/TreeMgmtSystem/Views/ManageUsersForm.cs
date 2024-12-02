using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;

namespace TreeMgmtSystem
{
    public partial class ManageUsersForm : Form
    {
        private DatabaseQueries dbQueries;

        public ManageUsersForm()
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
                DataTable userData = dbQueries.GetUsers();

                // Đặt dữ liệu cho DataGridView
                dataGridViewUsers.DataSource = userData;

                // Thiết lập tên cột hiển thị
                dataGridViewUsers.Columns["UserId"].HeaderText = "User ID";
                dataGridViewUsers.Columns["UserName"].HeaderText = "Tên người dùng";
                dataGridViewUsers.Columns["FullName"].HeaderText = "Họ tên";
                dataGridViewUsers.Columns["Email"].HeaderText = "Email";
                dataGridViewUsers.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
                dataGridViewUsers.Columns["Address"].HeaderText = "Địa chỉ";
                dataGridViewUsers.Columns["Role"].HeaderText = "Vai trò";

                // Tùy chỉnh kích thước cột
                dataGridViewUsers.Columns["UserId"].Width = 100;
                dataGridViewUsers.Columns["UserName"].Width = 150;
                dataGridViewUsers.Columns["FullName"].Width = 150;
                dataGridViewUsers.Columns["Email"].Width = 200;
                dataGridViewUsers.Columns["PhoneNumber"].Width = 150;
                dataGridViewUsers.Columns["Address"].Width = 200;
                dataGridViewUsers.Columns["Role"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            // Mở form chỉnh sửa người dùng được chọn
            int selectedRowIndex = dataGridViewUsers.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridViewUsers.Rows[selectedRowIndex];
            int userId = Convert.ToInt32(selectedRow.Cells["UserId"].Value);

            EditUserForm editUserForm = new EditUserForm(userId);
            editUserForm.ShowDialog();
            LoadUserData();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có ô nào được chọn hay không
                if (dataGridViewUsers.SelectedCells.Count > 0)
                {
                    // Lấy userId từ cột UserId của hàng được chọn
                    int selectedRowIndex = dataGridViewUsers.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridViewUsers.Rows[selectedRowIndex];
                    int userId = Convert.ToInt32(selectedRow.Cells["UserId"].Value);

                    // Hiển thị thông báo xác nhận
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        // Xóa người dùng được chọn
                        dbQueries.DeleteUser(userId);
                        LoadUserData();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
