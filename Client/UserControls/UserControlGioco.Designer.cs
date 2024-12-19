namespace corretto.UserControls
{
    partial class UserControlGioco
    {
        /// <summary> 
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBarVitaGiocatore1 = new System.Windows.Forms.ProgressBar();
            this.progressBarVitaGiocatore2 = new System.Windows.Forms.ProgressBar();
            this.canvasPanel = new System.Windows.Forms.Panel();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBarVitaGiocatore1
            // 
            this.progressBarVitaGiocatore1.Location = new System.Drawing.Point(12, 21);
            this.progressBarVitaGiocatore1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarVitaGiocatore1.Name = "progressBarVitaGiocatore1";
            this.progressBarVitaGiocatore1.Size = new System.Drawing.Size(133, 28);
            this.progressBarVitaGiocatore1.TabIndex = 0;
            // 
            // progressBarVitaGiocatore2
            // 
            this.progressBarVitaGiocatore2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarVitaGiocatore2.Location = new System.Drawing.Point(1134, 21);
            this.progressBarVitaGiocatore2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarVitaGiocatore2.Name = "progressBarVitaGiocatore2";
            this.progressBarVitaGiocatore2.Size = new System.Drawing.Size(133, 28);
            this.progressBarVitaGiocatore2.TabIndex = 1;
            // 
            // canvasPanel
            // 
            this.canvasPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasPanel.Location = new System.Drawing.Point(0, 57);
            this.canvasPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(1280, 663);
            this.canvasPanel.TabIndex = 2;
            this.canvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPanel_Paint);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 16;
            this.gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // UserControlGioco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.progressBarVitaGiocatore2);
            this.Controls.Add(this.progressBarVitaGiocatore1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserControlGioco";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Load += new System.EventHandler(this.UserControlGioco_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarVitaGiocatore1;
        private System.Windows.Forms.ProgressBar progressBarVitaGiocatore2;
        private System.Windows.Forms.Panel canvasPanel;
        private System.Windows.Forms.Timer gameTimer;
    }
}
