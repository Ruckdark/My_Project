using System.Windows.Forms;

namespace WordVaultAppMVC.Views.Controls
{
    public class DailyReviewControl : UserControl
    {
        public DailyReviewControl()
        {
            this.Dock = DockStyle.Fill;
            var form = new DailyReviewForm()
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(form);
            form.Show();
        }
    }
}