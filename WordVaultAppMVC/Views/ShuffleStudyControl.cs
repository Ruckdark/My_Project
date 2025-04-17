using System.Windows.Forms;

namespace WordVaultAppMVC.Views.Controls
{
    public class ShuffleStudyControl : UserControl
    {
        public ShuffleStudyControl()
        {
            this.Dock = DockStyle.Fill;
            var form = new ShuffleStudyForm()
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