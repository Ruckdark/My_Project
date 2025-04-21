using System;
using System.Windows.Forms;
using WindowShutdown.Services;

namespace WindowShutdown
{
    public partial class NeatModeControl : UserControl
    {
        public event EventHandler RequestFullMode;

        public NeatModeControl()
        {
            InitializeComponent();
            // Set giới hạn cho NumericUpDown (có thể làm trong Designer)
            numericUpDownHour.Maximum = 99;
            numericUpDownMinute.Maximum = 59;
            numericUpDownSecond.Maximum = 59;
            numericUpDownHour.Minimum = 0;
            numericUpDownMinute.Minimum = 0;
            numericUpDownSecond.Minimum = 0;
        }

        private void btnShutDown_Click(object sender, EventArgs e) => ScheduleAction("shutdown");
        private void btnRestart_Click(object sender, EventArgs e) => ScheduleAction("restart");

        private void ScheduleAction(string action)
        {
            int hourValue = (int)numericUpDownHour.Value;
            int minuteValue = (int)numericUpDownMinute.Value;
            int secondValue = (int)numericUpDownSecond.Value;

            if (hourValue == 0 && minuteValue == 0 && secondValue == 0)
            {
                MessageBox.Show("Vui lòng đặt thời gian lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalSeconds = (hourValue * 3600) + (minuteValue * 60) + secondValue;
            if (action == "shutdown") { ShutdownManager.ScheduleShutdown(totalSeconds); }
            else if (action == "restart") { ShutdownManager.ScheduleRestart(totalSeconds); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool cancelled = ShutdownManager.CancelShutdown();
            if (cancelled)
            {
                MessageBox.Show("Đã hủy lệnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có lệnh nào đang chờ để hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            RequestFullMode?.Invoke(this, EventArgs.Empty);
        }
    }
}