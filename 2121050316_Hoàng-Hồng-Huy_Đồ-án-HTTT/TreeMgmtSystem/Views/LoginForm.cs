using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class LoginForm : Form
    {
        private DatabaseQueries dbQueries;
        DashboardForm dashboardForm;
        UserForm userForm;
        public LoginForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Không cho phép phóng to form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBox1.Text;
                string password = textBox2.Text;
                string role;

                // Kiểm tra thông tin đăng nhập và lấy vai trò của người dùng
                bool isAuthenticated = dbQueries.AuthenticateUser(userName, password, out role);

                if (isAuthenticated)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();  // Ẩn form đăng nhập

                    CurrentSession.UserId = dbQueries.GetUserId(userName); // Lưu UserId vào CurrentSession
                    CurrentSession.UserName = userName;

                    if (role == "Admin")
                    {
                        //this.Visible = false;
                        if (dashboardForm == null)
                            dashboardForm = new DashboardForm();
                        dashboardForm.Show();
                    }
                    else if (role == "User")
                    {
                        //this.Visible = false;
                        if (userForm == null)
                            userForm = new UserForm();
                        userForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Tên người dùng hoặc mật khẩu không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form đăng ký
                RegisterForm registerForm = new RegisterForm();
                registerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
