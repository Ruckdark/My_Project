using System;
using WMPLib;

namespace WordVaultAppMVC.Helpers
{
    public static class AudioHelper
    {
        private static WindowsMediaPlayer _player = new WindowsMediaPlayer();

        public static void PlayAudio(string audioUrl)
        {
            if (string.IsNullOrEmpty(audioUrl))
            {
                System.Windows.Forms.MessageBox.Show("URL âm thanh không hợp lệ.");
                return;
            }

            try
            {
                _player.URL = audioUrl;
                _player.controls.play();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi khi phát âm thanh: " + ex.Message);
            }
        }
    }
}
