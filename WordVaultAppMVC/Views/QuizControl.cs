using System.Windows.Forms;

namespace WordVaultAppMVC.Views.Controls
{
    public class QuizControl : UserControl
    {
        public QuizControl()
        {
            this.Dock = DockStyle.Fill;
            var form = new QuizForm()
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