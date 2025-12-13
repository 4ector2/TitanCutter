using System;
using System.IO;
using System.Windows.Forms;

namespace TitanCutter
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            string sfn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data", "settings.txt");
            int diff = 1; bool m = true; bool sfx = true;
            if (File.Exists(sfn))
            {
                foreach (var l in File.ReadAllLines(sfn))
                {
                    var p = l.Split('=');
                    if (p.Length != 2) continue;
                    var k = p[0].Trim(); var v = p[1].Trim();
                    if (k == "Music") m = v == "1";
                    if (k == "SFX") sfx = v == "1";
                    if (k == "Difficulty") int.TryParse(v, out diff);
                }
            }

            chkMusic.Checked = m;
            chkSFX.Checked = sfx;
            cmbDifficulty.SelectedIndex = Math.Max(0, Math.Min(2, diff));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sfn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data", "settings.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(sfn));
            File.WriteAllLines(sfn, new string[] {
                $"Music={(chkMusic.Checked ? "1" : "0")}",
                $"SFX={(chkSFX.Checked ? "1" : "0")}",
                $"Difficulty={cmbDifficulty.SelectedIndex}"
            });

            MessageBox.Show("Settings saved. They will apply on next game start.", "Settings");
            this.Close();
        }
    }
}
