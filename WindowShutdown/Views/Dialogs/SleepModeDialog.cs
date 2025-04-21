using System;
using System.Windows.Forms;

namespace WindowShutdown
{
    public partial class SleepModeDialog : Form
    {
        public SleepModeDialog()
        {
            InitializeComponent();
            // Cấu hình Form này như một Dialog
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Kiểu border cố định
            this.MaximizeBox = false;                     // Ẩn nút Maximize
            this.MinimizeBox = false;                     // Ẩn nút Minimize
            this.StartPosition = FormStartPosition.CenterParent; // Hiển thị giữa form cha
            this.ShowInTaskbar = false;                   // Không hiển thị trên taskbar
        }


        private void btnSuspend_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đưa máy tính vào chế độ Ngủ (Suspend)?",
                                                 "Xác nhận Suspend",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Trả về DialogResult.OK để form gọi biết hành động đã được xác nhận
                    bool success = Application.SetSuspendState(PowerState.Suspend, false, false); // force=false, disableWake=false
                    this.DialogResult = success ? DialogResult.OK : DialogResult.Abort;
                    if (!success)
                    {
                        MessageBox.Show("Không thể vào chế độ Suspend.\nViệc này có thể yêu cầu quyền quản trị hoặc cài đặt hệ thống đặc biệt.", "Lỗi Suspend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi khi cố gắng Suspend: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Abort;
                }
                // Luôn đóng dialog sau khi thử
                if (!this.IsDisposed) this.Close();
            }
            // Nếu No thì không làm gì, dialog sẽ tự đóng hoặc chờ người dùng đóng
        }


        private void btnHibernate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đưa máy tính vào chế độ Ngủ đông (Hibernate)?\n(Cần bật chế độ này trong cài đặt hệ thống)",
                                                  "Xác nhận Hibernate",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = Application.SetSuspendState(PowerState.Hibernate, false, false); // force=false, disableWake=false
                    this.DialogResult = success ? DialogResult.OK : DialogResult.Abort;
                    if (!success)
                    {
                        MessageBox.Show("Không thể vào chế độ Hibernate.\nHãy chắc chắn Hibernate đã được bật trong Power Options và ứng dụng có đủ quyền.", "Lỗi Hibernate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi khi cố gắng Hibernate: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Abort;
                }
                // Luôn đóng dialog sau khi thử
                if (!this.IsDisposed) this.Close();
            }
        }
    }
}