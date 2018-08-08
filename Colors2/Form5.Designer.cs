namespace Colors2
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.box = new System.Windows.Forms.TextBox();
            this.IPViewer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // box
            // 
            this.box.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.box.Location = new System.Drawing.Point(17, 108);
            this.box.Multiline = true;
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(398, 274);
            this.box.TabIndex = 0;
            // 
            // IPViewer
            // 
            this.IPViewer.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.IPViewer.Location = new System.Drawing.Point(12, 9);
            this.IPViewer.Name = "IPViewer";
            this.IPViewer.Size = new System.Drawing.Size(414, 80);
            this.IPViewer.TabIndex = 0;
            this.IPViewer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 394);
            this.Controls.Add(this.box);
            this.Controls.Add(this.IPViewer);
            this.DoubleBuffered = true;
            this.Name = "Form5";
            this.Text = "画像受信";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form5_FormClosing);
            this.Load += new System.EventHandler(this.Form5_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form5_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox box;
        private System.Windows.Forms.Label IPViewer;
    }
}