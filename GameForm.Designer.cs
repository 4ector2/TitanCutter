namespace TitanCutter
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "GameForm";
            this.Text = "Titan Cutter - Game";
            this.ResumeLayout(false);
        }
    }
}
