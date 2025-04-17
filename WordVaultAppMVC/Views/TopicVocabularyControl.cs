using System.Windows.Forms;
using WordVaultAppMVC.Views;

namespace WordVaultAppMVC.Views.Controls
{
    public class TopicVocabularyControl : UserControl
    {
        public TopicVocabularyControl(string topic)
        {
            this.Dock = DockStyle.Fill;
            var form = new TopicVocabularyForm(topic)
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