using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TreeMgmtSystem.Controllers;
using TreeMgmtSystem.Utilities;

namespace TreeMgmtSystem
{
    public partial class DashboardForm : Form
    {
        private DatabaseQueries dbQueries;
        private ManageTreesForm manageTreesForm;
        private ManageServicesForm manageServicesForm;
        private ManageUsersForm manageUsersForm;
        private ManageReportForm manageReportForm;

        public DashboardForm()
        {
            InitializeComponent();
            dbQueries = new DatabaseQueries();
            //warningSystem = new TreeWarningSystem(dbQueries);
            UpdateDashboard();
            LoadTreeData();
            //ShowWarnings();
            ShowWarningsAndUnfinishedServices();
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoadTreeData()
        {
            try
            {
                DataTable treeData = dbQueries.GetTrees();

                // Đặt dữ liệu cho DataGridView
                dataGridView1.DataSource = treeData;

                // Thiết lập tên cột hiển thị
                dataGridView1.Columns["TreeId"].HeaderText = "Tree ID";
                dataGridView1.Columns["Species"].HeaderText = "Loại cây";
                dataGridView1.Columns["Location"].HeaderText = "Vị trí";
                dataGridView1.Columns["HealthStatus"].HeaderText = "Tình trạng";
                dataGridView1.Columns["ReminderDate"].HeaderText = "Ngày nhắc nhở";

                // Tùy chỉnh kích thước cột
                dataGridView1.Columns["TreeId"].Width = 100;
                dataGridView1.Columns["Species"].Width = 200;
                dataGridView1.Columns["Location"].Width = 200;
                dataGridView1.Columns["HealthStatus"].Width = 150;
                dataGridView1.Columns["ReminderDate"].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDashboard()
        {
            try
            {
                // Lấy số liệu từ cơ sở dữ liệu
                int totalTrees = dbQueries.GetTotalTrees();
                int totalServices = dbQueries.GetTotalServices();
                int totalReports = dbQueries.GetTotalReports();
                int totalUsers = dbQueries.GetTotalUsers();

                // Gán giá trị cho các Label
                lblTotalTrees.Text = totalTrees.ToString();
                lblTotalServices.Text = totalServices.ToString();
                lblTotalReports.Text = totalReports.ToString();
                lblTotalUsers.Text = totalUsers.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật số liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnManageTrees_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            if (manageTreesForm == null)
                manageTreesForm = new ManageTreesForm();
            manageTreesForm.ShowDialog();
        }

        private void btnManageServices_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            if (manageServicesForm == null)
                manageServicesForm = new ManageServicesForm();
            manageServicesForm.ShowDialog();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            if (manageUsersForm == null)
                manageUsersForm = new ManageUsersForm();
            manageUsersForm.ShowDialog();
        }

        private void ShowWarningsAndUnfinishedServices()
        {
            DataTable warningTable = new DataTable();
            warningTable.Columns.Add("Tree ID");
            warningTable.Columns.Add("Species");
            warningTable.Columns.Add("Location");
            warningTable.Columns.Add("Health Status");

            DataTable serviceTable = new DataTable();
            serviceTable.Columns.Add("Service ID");
            serviceTable.Columns.Add("Service Type");
            serviceTable.Columns.Add("Status");

            // Lấy danh sách cây có tình trạng không tốt
            DataTable treeWarnings = dbQueries.GetWarnings();
            foreach (DataRow row in treeWarnings.Rows)
            {
                warningTable.Rows.Add(row["TreeId"], row["Species"], row["Location"], row["HealthStatus"]);
            }

            // Lấy danh sách dịch vụ chưa hoàn thành
            DataTable services = dbQueries.GetUnfinishedServices();
            foreach (DataRow row in services.Rows)
            {
                serviceTable.Rows.Add(row["ServiceId"], row["ServiceType"], row["Status"]);
            }

            // Hiển thị form cảnh báo nếu có cảnh báo hoặc dịch vụ chưa hoàn thành
            if (warningTable.Rows.Count > 0 || serviceTable.Rows.Count > 0)
            {
                WarningsForm warningsForm = new WarningsForm(warningTable, serviceTable);
                warningsForm.ShowDialog();
            }
        }

        private void btnWarning_Click(object sender, EventArgs e)
        {
            ShowWarningsAndUnfinishedServices();
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnManageReports_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            if (manageReportForm == null)
                manageReportForm = new ManageReportForm();
            manageReportForm.ShowDialog();
        }
    }
}
