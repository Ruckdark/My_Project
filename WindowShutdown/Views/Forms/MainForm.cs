using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowShutdown.Services;

namespace WindowShutdown
{
    public partial class MainForm : Form
    {
        private FullModeControl _fullModeControl;
        private NeatModeControl _neatModeControl;
        private Panel _contentPanel;

        public MainForm()
        {
            InitializeComponent();
            InitializeContentPanel();
            InitializeControls();
            ShowFullModeControl(); // Hiển thị mode mặc định

            // Các cấu hình Form khác (Size, BorderStyle, MaximizeBox...) đã được set trong Designer
            this.ShowInTaskbar = true;
        }

        private void InitializeContentPanel()
        {
            var foundPanel = this.Controls.Find("contentPanel", true).FirstOrDefault();
            if (foundPanel is Panel panel) { _contentPanel = panel; }
            else
            {
                _contentPanel = new Panel { Name = "contentPanel", Dock = DockStyle.Fill };
                this.Controls.Add(_contentPanel);
                _contentPanel.BringToFront();
                MessageBox.Show("Warning: Content panel was created programmatically. Please add a Panel named 'contentPanel' to MainForm via the Designer.", "Developer Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeControls()
        {
            if (_contentPanel == null) InitializeContentPanel();

            _fullModeControl = new FullModeControl { Dock = DockStyle.Fill, Visible = false };
            _fullModeControl.RequestNeatMode += SwitchToNeatMode;
            _contentPanel.Controls.Add(_fullModeControl);

            _neatModeControl = new NeatModeControl { Dock = DockStyle.Fill, Visible = false };
            _neatModeControl.RequestFullMode += SwitchToFullMode;
            _contentPanel.Controls.Add(_neatModeControl);
        }

        private void ShowFullModeControl()
        {
            if (_neatModeControl == null || _fullModeControl == null) InitializeControls();
            _neatModeControl.Visible = false;
            _fullModeControl.Visible = true;
            _fullModeControl.BringToFront();
            this.Text = "Window Shutdown - Full Mode";
        }

        private void ShowNeatModeControl()
        {
            if (_neatModeControl == null || _fullModeControl == null) InitializeControls();
            _fullModeControl.Visible = false;
            _neatModeControl.Visible = true;
            _neatModeControl.BringToFront();
            this.Text = "Window Shutdown - Neat Mode";
        }

        private void SwitchToNeatMode(object sender, EventArgs e) => ShowNeatModeControl();
        private void SwitchToFullMode(object sender, EventArgs e) => ShowFullModeControl();

        // --- Logic NotifyIcon ---
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = !this.Visible;
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Activate();
            }
            else { this.ShowInTaskbar = false; }
        }

        private void HideMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            // Thoát trực tiếp từ menu không cần hỏi lại
            ShutdownManager.CancelShutdown();
            Application.Exit();
        }

        // --- Xử lý đóng Form (Nhấn nút X) ---
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Chỉ hỏi khi người dùng chủ động đóng form (nhấn X, Alt+F4,...)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng không?",
                                                      "Xác nhận thoát",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                // Nếu người dùng chọn No, hủy việc đóng Form
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else // Nếu người dùng chọn Yes
                {
                    // Hủy lệnh shutdown/restart đang chờ (nếu có) trước khi thoát
                    ShutdownManager.CancelShutdown();
                    // Cho phép đóng Form và thoát ứng dụng (không cần gọi Application.Exit() ở đây nữa)
                    // base.OnFormClosing(e); // Dòng này không cần thiết khi không cancel
                }
            }
            else // Nếu đóng không phải do người dùng (vd: System Shutdown)
            {
                // Cứ hủy lệnh đang chờ và cho phép đóng
                ShutdownManager.CancelShutdown();
                // base.OnFormClosing(e); // Dòng này không cần thiết khi không cancel
            }

            // Gọi base.OnFormClosing nếu không cancel event
            if (!e.Cancel)
            {
                base.OnFormClosing(e);
            }
        }
    }
}