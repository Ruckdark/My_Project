using System;
using System.Windows.Forms;

namespace WindowShutdown
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Chạy MainForm thay vì FormWindowShutdown cũ
            Application.Run(new MainForm());
        }
    }
}