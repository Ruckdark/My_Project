using System;
using System.Data;
using System.Windows.Forms;

namespace TreeMgmtSystem
{
    public partial class WarningsForm : Form
    {
        public WarningsForm(DataTable warningTable, DataTable serviceTable)
        {
            InitializeComponent();
            LoadWarnings(warningTable);
            LoadServices(serviceTable);
            // Cài đặt để form xuất hiện ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Không cho phép phóng to form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void LoadWarnings(DataTable warningTable)
        {
            if (warningTable.Rows.Count > 0)
            {
                dgvWarnings.DataSource = warningTable;
            }
            else
            {
                lblWarnings.Text = "Không có cây nào có tình trạng không tốt.";
            }
        }

        private void LoadServices(DataTable serviceTable)
        {
            if (serviceTable.Rows.Count > 0)
            {
                dgvServices.DataSource = serviceTable;
            }
            else
            {
                lblServices.Text = "Không có dịch vụ nào chưa hoàn thành.";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
