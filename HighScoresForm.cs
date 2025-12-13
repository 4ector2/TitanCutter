using System;
using System.IO;
using System.Windows.Forms;

namespace TitanCutter
{
    public partial class HighScoresForm : Form
    {
        public HighScoresForm()
        {
            InitializeComponent();
            LoadScores();
        }

        private void LoadScores()
        {
            listBoxScores.Items.Clear();
            var list = HighScoresManager.LoadEntries();
            foreach (var s in list)
            {
                listBoxScores.Items.Add($"{s.Name} - {s.Score} - {s.Date:g}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
