namespace FightingGameClient.UserControls
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBarVitaGiocatore1 = new System.Windows.Forms.ProgressBar();
            this.progressBarVitaGiocatore2 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBarVitaGiocatore1
            // 
            this.progressBarVitaGiocatore1.Location = new System.Drawing.Point(31, 52);
            this.progressBarVitaGiocatore1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarVitaGiocatore1.Name = "progressBarVitaGiocatore1";
            this.progressBarVitaGiocatore1.Size = new System.Drawing.Size(177, 28);
            this.progressBarVitaGiocatore1.TabIndex = 0;
            this.progressBarVitaGiocatore1.Value = 100;
            // 
            // progressBarVitaGiocatore2
            // 
            this.progressBarVitaGiocatore2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarVitaGiocatore2.Location = new System.Drawing.Point(1985, 52);
            this.progressBarVitaGiocatore2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarVitaGiocatore2.Name = "progressBarVitaGiocatore2";
            this.progressBarVitaGiocatore2.Size = new System.Drawing.Size(177, 28);
            this.progressBarVitaGiocatore2.TabIndex = 1;
            this.progressBarVitaGiocatore2.Value = 100;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1981, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player 2:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Player 1:";
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
            this.Controls.Add(this.progressBarVitaGiocatore1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarVitaGiocatore2);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserControlGioco";
            this.Size = new System.Drawing.Size(2189, 953);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ProgressBar progressBarVitaGiocatore1;
        private System.Windows.Forms.ProgressBar progressBarVitaGiocatore2;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
