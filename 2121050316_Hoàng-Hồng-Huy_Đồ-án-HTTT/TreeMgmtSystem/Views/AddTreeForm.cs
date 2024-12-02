using System;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class AddTreeForm : Form
    {
        private DatabaseQueries dbQueries;

        public AddTreeForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            txtUserId.Text = CurrentSession.UserId.ToString();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                Tree tree = new Tree
                {
                    UserId = int.Parse(txtUserId.Text),
                    Species = int.Parse(txtSpecies.Text),
                    Age = int.Parse(txtAge.Text),
                    Height = int.Parse(txtHeight.Text),
                    Diameter = int.Parse(txtDiameter.Text),
                    HealthStatus = txtHealthStatus.Text,
                    Note = txtNote.Text,
                    Location = txtLocation.Text,
                    ReminderDate = dateTimePickerReminder.Value
                };

                // Thêm cây mới vào cơ sở dữ liệu
                dbQueries.AddTree(tree);

                MessageBox.Show("Cây mới đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm cây: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
