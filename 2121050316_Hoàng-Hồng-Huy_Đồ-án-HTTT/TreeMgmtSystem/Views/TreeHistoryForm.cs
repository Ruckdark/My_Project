using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;

namespace TreeMgmtSystem
{
    public partial class TreeHistoryForm : Form
    {
        private DatabaseQueries dbQueries;
        private int selectedTreeId;

        public TreeHistoryForm(DatabaseQueries dbQueries, int treeId)
        {
            InitializeComponent();
            this.dbQueries = dbQueries;
            this.selectedTreeId = treeId;
            LoadTreeHistory();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadTreeHistory()
        {
            try
            {
                DataTable treeHistory = dbQueries.GetTreeHistory(selectedTreeId);
                dgvTreeHistory.DataSource = treeHistory;

                // Đăng ký sự kiện DataBindingComplete để thiết lập thuộc tính cột sau khi hoàn thành liên kết dữ liệu
                dgvTreeHistory.DataBindingComplete += DgvTreeHistory_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử cây: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTreeHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Thiết lập tên cột hiển thị
            if (dgvTreeHistory.Columns["EventType"] != null)
            {
                dgvTreeHistory.Columns["EventType"].HeaderText = "Loại sự kiện";
                dgvTreeHistory.Columns["EventType"].Width = 150;
            }

            if (dgvTreeHistory.Columns["EventDescription"] != null)
            {
                dgvTreeHistory.Columns["EventDescription"].HeaderText = "Mô tả sự kiện";
                dgvTreeHistory.Columns["EventDescription"].Width = 200;
            }

            if (dgvTreeHistory.Columns["Date"] != null)
            {
                dgvTreeHistory.Columns["Date"].HeaderText = "Ngày";
                dgvTreeHistory.Columns["Date"].Width = 100;
            }

            if (dgvTreeHistory.Columns["UserId"] != null)
            {
                dgvTreeHistory.Columns["UserId"].HeaderText = "ID người dùng";
                dgvTreeHistory.Columns["UserId"].Width = 100;
            }
            // Thiết lập tự động điều chỉnh kích thước các cột
            dgvTreeHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Hủy đăng ký sự kiện sau khi hoàn thành để tránh lặp lại
            dgvTreeHistory.DataBindingComplete -= DgvTreeHistory_DataBindingComplete;
        }



    }
}
