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
            this.progressBarVitaGiocatore1.Location = new System.Drawing.Point(192, 93);
            this.progressBarVitaGiocatore1.Name = "progressBarVitaGiocatore1";
            this.progressBarVitaGiocatore1.Size = new System.Drawing.Size(100, 23);
            this.progressBarVitaGiocatore1.TabIndex = 0;
            // 
            // progressBarVitaGiocatore2
            // 
            this.progressBarVitaGiocatore2.Location = new System.Drawing.Point(926, 93);
            this.progressBarVitaGiocatore2.Name = "progressBarVitaGiocatore2";
            this.progressBarVitaGiocatore2.Size = new System.Drawing.Size(100, 23);
            this.progressBarVitaGiocatore2.TabIndex = 1;
            // 
            // canvasPanel
            // 
            this.canvasPanel.Location = new System.Drawing.Point(111, 213);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(951, 428);
            this.canvasPanel.TabIndex = 2;
            this.canvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPanel_Paint);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 16; //60 FPS
            this.gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // UserControlGioco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.progressBarVitaGiocatore2);
            this.Controls.Add(this.progressBarVitaGiocatore1);
            this.Name = "UserControlGioco";
            this.Size = new System.Drawing.Size(1276, 692);
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
