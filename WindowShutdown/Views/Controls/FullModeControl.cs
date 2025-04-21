using System;
using System.Drawing;
using System.Windows.Forms;
using WindowShutdown.Services;

namespace WindowShutdown
{
    public partial class FullModeControl : UserControl
    {
        public event EventHandler RequestNeatMode;
        private SleepModeDialog _sleepModeDialog;

        public FullModeControl()
        {
            InitializeComponent();
            // Set giới hạn cho NumericUpDown (có thể làm trong Designer)
            numericUpDownHour.Maximum = 99; // Cho phép nhiều giờ hơn nếu muốn
            numericUpDownMinute.Maximum = 59;
            numericUpDownSecond.Maximum = 59;
            numericUpDownHour.Minimum = 0;
            numericUpDownMinute.Minimum = 0;
            numericUpDownSecond.Minimum = 0;
        }

        // Nút chuyển sang Neat Mode
        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            RequestNeatMode?.Invoke(this, EventArgs.Empty);
        }

        // --- Các nút hẹn giờ nhanh ---
        private void btn5minutes_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(300);
        private void btn10minutes_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(600);
        private void btn20minutes_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(1200);
        private void btn30minutes_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(1800);
        private void btn1Hour_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(3600);
        private void btn2Hours_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(7200);
        private void btn3Hours_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(10800);
        private void btn5Hours_Click(object sender, EventArgs e) => ShutdownManager.ScheduleShutdown(18000);


        // Nút hủy lệnh
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool cancelled = ShutdownManager.CancelShutdown();
            if (cancelled)
            {
                MessageBox.Show("Command canceled!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No active command to cancel.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút tự thiết lập thời gian (đọc từ NumericUpDown)
        private void btnSelfSetup_Click(object sender, EventArgs e)
        {
            // Lấy giá trị trực tiếp từ NumericUpDown
            int hourValue = (int)numericUpDownHour.Value;
            int minuteValue = (int)numericUpDownMinute.Value;
            int secondValue = (int)numericUpDownSecond.Value;


            // Kiểm tra tổng thời gian bằng 0
            if (hourValue == 0 && minuteValue == 0 && secondValue == 0)
            {
                MessageBox.Show("Total time is zero. Please set a time greater than zero.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NumericUpDown đã xử lý giá trị âm và không phải số

            int totalSeconds = (hourValue * 3600) + (minuteValue * 60) + secondValue;
            ShutdownManager.ScheduleShutdown(totalSeconds);
        }


        // Nút Reset (đặt NumericUpDown về 0)
        private void btnReset_Click(object sender, EventArgs e)
        {
            numericUpDownHour.Value = 0;
            numericUpDownMinute.Value = 0;
            numericUpDownSecond.Value = 0;
        }

        // Nút mở chế độ Sleep/Hibernate
        private void btnSleep_Click(object sender, EventArgs e)
        {
            // Nên tìm Form cha gần nhất để làm parent cho dialog
            Form parentForm = this.FindForm();

            if (_sleepModeDialog == null || _sleepModeDialog.IsDisposed)
            {
                _sleepModeDialog = new SleepModeDialog();
            }
            // Hiển thị dialog với Form cha (nếu tìm thấy)
            _sleepModeDialog.ShowDialog(parentForm);
        }
    }
}