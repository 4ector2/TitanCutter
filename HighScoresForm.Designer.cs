namespace TitanCutter
{
    partial class HighScoresForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxScores;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listBoxScores = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // listBoxScores
            this.listBoxScores.Location = new System.Drawing.Point(12, 12);
            this.listBoxScores.Size = new System.Drawing.Size(560, 360);

            // btnClose
            this.btnClose.Text = "Close";
            this.btnClose.Location = new System.Drawing.Point(240, 385);
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // HighScoresForm
            this.ClientSize = new System.Drawing.Size(584, 431);
            this.Controls.Add(this.listBoxScores);
            this.Controls.Add(this.btnClose);
            this.Text = "High Scores";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ResumeLayout(false);
        }
    }
}
