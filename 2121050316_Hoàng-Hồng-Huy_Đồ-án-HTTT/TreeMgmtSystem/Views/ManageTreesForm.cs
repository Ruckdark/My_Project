using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;

namespace TreeMgmtSystem
{
    public partial class ManageTreesForm : Form
    {
        private DatabaseQueries dbQueries;

        public ManageTreesForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            LoadTreeData();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadTreeData()
        {
            try
            {
                DataTable treeData = dbQueries.GetDetailedTrees();

                // Thêm các cột mới cho chiều cao và đường kính đã chuyển đổi
                treeData.Columns.Add("HeightInMeters", typeof(string));
                treeData.Columns.Add("DiameterInMeters", typeof(string));

                // Chuyển đổi chiều cao và đường kính từ cm sang m
                foreach (DataRow row in treeData.Rows)
                {
                    if (row["Height"] != DBNull.Value)
                    {
                        double heightInCm = Convert.ToDouble(row["Height"]);
                        row["HeightInMeters"] = (heightInCm / 100).ToString("0.00") + " m";
                    }
                    if (row["Diameter"] != DBNull.Value)
                    {
                        double diameterInCm = Convert.ToDouble(row["Diameter"]);
                        row["DiameterInMeters"] = (diameterInCm / 100).ToString("0.00") + " m";
                    }
                }

                // Đặt dữ liệu cho DataGridView
                dataGridView1.DataSource = treeData;

                // Lưu trữ treeId vào Tag của mỗi hàng
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Tag = row.Cells["TreeId"].Value;
                }

                // Thiết lập tên cột hiển thị
                dataGridView1.Columns["Species"].HeaderText = "Loại cây";
                dataGridView1.Columns["Age"].HeaderText = "Tuổi";
                dataGridView1.Columns["HeightInMeters"].HeaderText = "Chiều cao";
                dataGridView1.Columns["DiameterInMeters"].HeaderText = "Đường kính";
                dataGridView1.Columns["HealthStatus"].HeaderText = "Tình trạng";
                dataGridView1.Columns["Note"].HeaderText = "Ghi chú";
                dataGridView1.Columns["Location"].HeaderText = "Vị trí";

                dataGridView1.Columns["HeightInMeters"].DisplayIndex = 2;
                dataGridView1.Columns["DiameterInMeters"].DisplayIndex = 3;

                // Ẩn các cột chiều cao và đường kính cũ
                dataGridView1.Columns["Height"].Visible = false;
                dataGridView1.Columns["Diameter"].Visible = false;
                dataGridView1.Columns["TreeId"].Visible = false;

                // Tùy chỉnh kích thước cột
                dataGridView1.Columns["Species"].Width = 150;
                dataGridView1.Columns["Age"].Width = 100;
                dataGridView1.Columns["HeightInMeters"].Width = 100;
                dataGridView1.Columns["DiameterInMeters"].Width = 100;
                dataGridView1.Columns["HealthStatus"].Width = 150;
                dataGridView1.Columns["Note"].Width = 200;
                dataGridView1.Columns["Location"].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddTree_Click(object sender, EventArgs e)
        {
            // Mở form thêm cây mới
            AddTreeForm addTreeForm = new AddTreeForm();
            addTreeForm.ShowDialog();
            LoadTreeData();
        }

        private void btnEditTree_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có ô nào được chọn hay không
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    // Lấy TreeId từ thuộc tính Tag của hàng được chọn
                    int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                    int treeId = Convert.ToInt32(selectedRow.Tag);

                    // Mở form chỉnh sửa cây được chọn
                    EditTreeForm editTreeForm = new EditTreeForm(treeId);
                    editTreeForm.ShowDialog();

                    // Tải lại dữ liệu cây sau khi chỉnh sửa
                    LoadTreeData();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một cây để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTree_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có ô nào được chọn hay không
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    // Lấy TreeId từ thuộc tính Tag của hàng được chọn
                    int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                    int treeId = Convert.ToInt32(selectedRow.Tag);

                    // Hiển thị thông báo xác nhận
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa cây này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        // Xóa cây được chọn
                        dbQueries.DeleteTree(treeId);
                        LoadTreeData();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một cây để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện nhấp đúp vào một dòng trong DataGridView
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                if (selectedRow.Tag != null)
                {
                    int treeId;
                    bool isInteger = int.TryParse(selectedRow.Tag.ToString(), out treeId);

                    if (isInteger)
                    {
                        // Mở form TreeForm để hiển thị lịch sử chăm sóc của cây
                        TreeHistoryForm treeForm = new TreeHistoryForm(dbQueries, treeId);
                        treeForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("TreeId không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể lấy TreeId từ hàng được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim().ToLower();            
            if (txtSearch.Text != null)
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                        $"Species LIKE '%{searchValue}%' OR Location LIKE '%{searchValue}%'";
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                }
            }
            else
            {
                LoadTreeData();
            }
        }
    }
}
