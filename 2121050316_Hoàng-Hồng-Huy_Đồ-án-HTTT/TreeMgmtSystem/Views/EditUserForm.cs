using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;

namespace TreeMgmtSystem
{
    public partial class EditUserForm : Form
    {
        private DatabaseQueries dbQueries;
        private int userId;

        public EditUserForm(int userId)
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            this.userId = userId;
            LoadUserData();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadUserData()
        {
            try
            {
                User user = dbQueries.GetUserById(userId);
                if (user != null)
                {
                    txtUserName.Text = user.UserName;
                    txtFullName.Text = user.FullName;
                    txtEmail.Text = user.Email;
                    txtPhoneNumber.Text = user.PhoneNumber;
                    txtAddress.Text = user.Address;
                    txtRole.Text = user.Role;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu người dùng: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                User user = new User
                {
                    UserId = userId,
                    UserName = txtUserName.Text,
                    FullName = txtFullName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Address = txtAddress.Text,
                    Role = txtRole.Text
                };

                // Cập nhật người dùng vào cơ sở dữ liệu
                dbQueries.UpdateUser(user);

                MessageBox.Show("Thông tin người dùng đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật người dùng: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
