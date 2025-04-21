using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowShutdown.Services
{
    public static class ShutdownManager
    {
        private const string ShutdownCommand = "shutdown";

        // Lên lịch Shutdown
        public static bool ScheduleShutdown(int seconds)
        {
            return ExecuteShutdownCommand($"/s /t {seconds}", $"Shutdown scheduled in {TimeSpan.FromSeconds(seconds)}.");
        }

        // Lên lịch Restart
        public static bool ScheduleRestart(int seconds)
        {
            return ExecuteShutdownCommand($"/r /t {seconds}", $"Restart scheduled in {TimeSpan.FromSeconds(seconds)}.");
        }

        // Hủy lệnh đang chờ
        public static bool CancelShutdown()
        {
            // Không cần hiển thị thông báo thành công/thất bại ở đây
            // Hàm gọi sẽ tự quyết định việc hiển thị thông báo
            return ExecuteShutdownCommand("/a", null, false); // Không hiển thị thông báo từ hàm này
        }

        // Hàm thực thi lệnh shutdown chung
        private static bool ExecuteShutdownCommand(string arguments, string successMessage, bool showSuccessMessage = true)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(ShutdownCommand, arguments)
                {
                    CreateNoWindow = true, // Không hiển thị cửa sổ cmd
                    UseShellExecute = false // Cần thiết cho CreateNoWindow
                };
                Process process = Process.Start(psi);
                // process.WaitForExit(); // Không cần đợi vì shutdown là lệnh hệ thống

                // Chỉ hiển thị thông báo thành công nếu được yêu cầu và có nội dung
                if (showSuccessMessage && !string.IsNullOrEmpty(successMessage))
                {
                    MessageBox.Show(successMessage, "Command Scheduled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true; // Giả định lệnh được gửi thành công
            }
            catch (Win32Exception ex) // Bắt lỗi cụ thể nếu không tìm thấy 'shutdown.exe' hoặc thiếu quyền
            {
                MessageBox.Show($"Failed to execute command: {ex.Message}\nEnsure '{ShutdownCommand}.exe' is in your system path and you have sufficient permissions.",
                                "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}