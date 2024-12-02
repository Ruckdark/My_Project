using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Models;

namespace TreeMgmtSystem
{
    public partial class EditTreeForm : Form
    {
        private DatabaseQueries dbQueries;
        private int treeId;

        public EditTreeForm(int treeId)
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            this.treeId = treeId;
            LoadTreeData();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadTreeData()
        {
            try
            {
                Tree tree = dbQueries.GetTreeById(treeId);
                if (tree != null)
                {
                    txtUserId.Text = tree.UserId.ToString();
                    txtSpecies.Text = tree.Species.ToString();
                    txtAge.Text = tree.Age.ToString();
                    txtHeight.Text = tree.Height.ToString();
                    txtDiameter.Text = tree.Diameter.ToString();
                    txtHealthStatus.Text = tree.HealthStatus;
                    txtNote.Text = tree.Note;
                    txtLocation.Text = tree.Location;
                    dateTimePickerReminder.Value = tree.ReminderDate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu cây: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                Tree tree = new Tree
                {
                    TreeId = treeId,
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

                // Cập nhật cây vào cơ sở dữ liệu
                dbQueries.UpdateTree(tree);

                MessageBox.Show("Thông tin cây đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật cây: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
