namespace TitanCutter
{
    partial class NameEntryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblPrompt;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblPrompt
            this.lblPrompt.Text = "Enter your name:";
            this.lblPrompt.Location = new System.Drawing.Point(12, 12);
            this.lblPrompt.Size = new System.Drawing.Size(260, 20);

            // txtName
            this.txtName.Location = new System.Drawing.Point(12, 36);
            this.txtName.Size = new System.Drawing.Size(260, 23);

            // btnOk
            this.btnOk.Text = "OK";
            this.btnOk.Location = new System.Drawing.Point(100, 72);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            // NameEntryForm
            this.ClientSize = new System.Drawing.Size(284, 112);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "Save Score";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
