using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class RegisterForm : Form
    {
        private DatabaseQueries dbQueries;

        public RegisterForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            // Điền UserId hiện tại vào trường txtUserId và ẩn nó
            txtRole.Text = "User";
            txtRole.Visible = false;
            lblRole.Visible = false;
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Không cho phép phóng to form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;
                string fullName = txtFullName.Text;
                string email = txtEmail.Text;
                string phoneNumber = txtPhoneNumber.Text;
                string address = txtAddress.Text;
                string role = txtRole.Text;

                // Gọi phương thức để đăng ký người dùng mới
                bool isRegistered = dbQueries.RegisterUser(userName, fullName, email, phoneNumber, address, role);

                if (isRegistered)
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại. Hãy thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
