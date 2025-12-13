namespace TitanCutter
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckBox chkMusic;
        private System.Windows.Forms.CheckBox chkSFX;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chkMusic = new System.Windows.Forms.CheckBox();
            this.chkSFX = new System.Windows.Forms.CheckBox();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // chkMusic
            this.chkMusic.Text = "Music (On/Off)";
            this.chkMusic.Location = new System.Drawing.Point(20, 20);
            this.chkMusic.AutoSize = true;

            // chkSFX
            this.chkSFX.Text = "SFX (On/Off)";
            this.chkSFX.Location = new System.Drawing.Point(20, 60);
            this.chkSFX.AutoSize = true;

            // cmbDifficulty
            this.cmbDifficulty.Location = new System.Drawing.Point(20, 100);
            this.cmbDifficulty.Width = 200;
            this.cmbDifficulty.Items.AddRange(new object[] { "Easy", "Normal", "Hard" });
            this.cmbDifficulty.SelectedIndex = 1;

            // btnSave
            this.btnSave.Text = "Save";
            this.btnSave.Location = new System.Drawing.Point(20, 150);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // SettingsForm
            this.ClientSize = new System.Drawing.Size(260, 210);
            this.Controls.Add(this.chkMusic);
            this.Controls.Add(this.chkSFX);
            this.Controls.Add(this.cmbDifficulty);
            this.Controls.Add(this.btnSave);
            this.Text = "Settings";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ResumeLayout(false);
        }
    }
}
