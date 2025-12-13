using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TitanCutter
{
    public partial class MainMenuForm : Form
    {
        private Image menuBackground;

        public MainMenuForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            LoadBackground();

            this.Resize += (s, e) => CenterButtons();
            CenterButtons();
        }

        private void LoadBackground()
        {
            string img = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images", "menu_background.png");

            if (File.Exists(img))
                menuBackground = Image.FromFile(img);
        }

        private void CenterButtons()
        {
            int centerX = this.ClientSize.Width / 2;

            btnStart.Left = centerX - btnStart.Width / 2;
            btnSettings.Left = centerX - btnSettings.Width / 2;
            btnHighScores.Left = centerX - btnHighScores.Width / 2;
            btnExit.Left = centerX - btnExit.Width / 2;

            btnStart.Top = 200;
            btnSettings.Top = 260;
            btnHighScores.Top = 320;
            btnExit.Top = 380;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (menuBackground != null)
                e.Graphics.DrawImage(menuBackground, 0, 0, Width, Height);
            else
                e.Graphics.Clear(Color.Black);

            base.OnPaint(e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            using (var g = new GameForm())
            {
                this.Hide();
                g.ShowDialog();
                this.Show();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var s = new SettingsForm())
            {
                s.ShowDialog();
            }
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            using (var h = new HighScoresForm())
            {
                h.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                ToggleFullscreen();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ToggleFullscreen()
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }

            CenterButtons();
        }
    }
}
