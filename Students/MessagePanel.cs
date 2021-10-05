using System.Windows.Forms;

namespace Students
{
    public partial class MessagePanel : UserControl
    {
        public MessagePanel()
        {
            InitializeComponent();
        }

        public string Message
        {
            set => ChangeMessage(value);
        }

        private void ChangeMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Visible = false;
                return;
            }
            
            lblMessage.Text = message;
            lblMessage.Refresh();
        }
    }
}