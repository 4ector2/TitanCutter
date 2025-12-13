namespace TitanCutter
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text = "Titan Cutter";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Size = new System.Drawing.Size(560, 80);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);

            // btnStart
            this.btnStart.Text = "Start Game";
            this.btnStart.Size = new System.Drawing.Size(200, 48);
            this.btnStart.Location = new System.Drawing.Point(200, 120);
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // btnSettings
            this.btnSettings.Text = "Settings";
            this.btnSettings.Size = new System.Drawing.Size(200, 48);
            this.btnSettings.Location = new System.Drawing.Point(200, 188);
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            // btnHighScores
            this.btnHighScores.Text = "High Scores";
            this.btnHighScores.Size = new System.Drawing.Size(200, 48);
            this.btnHighScores.Location = new System.Drawing.Point(200, 256);
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);

            // btnExit
            this.btnExit.Text = "Exit";
            this.btnExit.Size = new System.Drawing.Size(200, 48);
            this.btnExit.Location = new System.Drawing.Point(200, 324);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // MainMenuForm
            this.ClientSize = new System.Drawing.Size(600, 420);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnHighScores);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Titan Cutter - Menu";
            this.ResumeLayout(false);
        }
    }
}
