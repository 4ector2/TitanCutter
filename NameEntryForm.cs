using System.Windows.Forms;
using System.Xml.Linq;

namespace TitanCutter
{
    public partial class NameEntryForm : Form
    {
        public string PlayerName => txtName.Text.Trim();

        public NameEntryForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
