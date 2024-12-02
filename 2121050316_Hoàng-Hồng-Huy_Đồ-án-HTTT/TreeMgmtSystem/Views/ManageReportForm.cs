using System;
using System.Data;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;

namespace TreeMgmtSystem
{
    public partial class ManageReportForm : Form
    {
        private DatabaseQueries dbQueries;

        public ManageReportForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            LoadReports();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadReports()
        {
            try
            {
                DataTable reportData = dbQueries.GetAllReports();
                dataGridViewReports.DataSource = reportData;

                // Thiết lập tên cột hiển thị
                dataGridViewReports.Columns["ReportId"].HeaderText = "Mã Báo Cáo";
                dataGridViewReports.Columns["Annunciator"].HeaderText = "Mã Người Dùng";
                dataGridViewReports.Columns["Description"].HeaderText = "Nội Dung Báo Cáo";
                dataGridViewReports.Columns["ReportDate"].HeaderText = "Ngày Báo Cáo";

                // Tùy chỉnh kích thước cột
                dataGridViewReports.Columns["ReportId"].Width = 100;
                dataGridViewReports.Columns["Annunciator"].Width = 100;
                dataGridViewReports.Columns["Description"].Width = 400;
                dataGridViewReports.Columns["ReportDate"].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu báo cáo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dataGridViewReports.SelectedRows.Count > 0)
            {
                int reportId = (int)dataGridViewReports.SelectedRows[0].Cells["ReportId"].Value;
                ViewReport(reportId);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một báo cáo để xem.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewReports.SelectedRows.Count > 0)
            {
                int reportId = (int)dataGridViewReports.SelectedRows[0].Cells["ReportId"].Value;
                EditReport(reportId);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một báo cáo để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewReports.SelectedRows.Count > 0)
            {
                int reportId = (int)dataGridViewReports.SelectedRows[0].Cells["ReportId"].Value;

                // Hiển thị thông báo xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa báo cáo này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteReport(reportId);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một báo cáo để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void ViewReport(int reportId)
        {
            // Thêm logic để xem chi tiết báo cáo
            MessageBox.Show($"Xem báo cáo với mã: {reportId}", "Xem Báo Cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditReport(int reportId)
        {
            // Thêm logic để chỉnh sửa báo cáo
            MessageBox.Show($"Chỉnh sửa báo cáo với mã: {reportId}", "Chỉnh Sửa Báo Cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteReport(int reportId)
        {
            try
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa báo cáo này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Kiểm tra lựa chọn của người dùng
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = dbQueries.DeleteReport(reportId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Báo cáo đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReports(); // Tải lại danh sách báo cáo sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Xóa báo cáo thất bại. Hãy thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa báo cáo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
