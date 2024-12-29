﻿namespace FightingGameClient.UserControls
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
            this.CanvasPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.CanvasPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarVitaGiocatore1
            // 
            this.progressBarVitaGiocatore1.Location = new System.Drawing.Point(83, 13);
            this.progressBarVitaGiocatore1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarVitaGiocatore1.Name = "progressBarVitaGiocatore1";
            this.progressBarVitaGiocatore1.Size = new System.Drawing.Size(133, 28);
            this.progressBarVitaGiocatore1.TabIndex = 0;
            this.progressBarVitaGiocatore1.Value = 100;
            // 
            // progressBarVitaGiocatore2
            // 
            this.progressBarVitaGiocatore2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarVitaGiocatore2.Location = new System.Drawing.Point(1110, 13);
            this.progressBarVitaGiocatore2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarVitaGiocatore2.Name = "progressBarVitaGiocatore2";
            this.progressBarVitaGiocatore2.Size = new System.Drawing.Size(133, 28);
            this.progressBarVitaGiocatore2.TabIndex = 1;
            this.progressBarVitaGiocatore2.Value = 100;
            // 
            // CanvasPanel
            // 
            this.CanvasPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CanvasPanel.Controls.Add(this.label2);
            this.CanvasPanel.Controls.Add(this.progressBarVitaGiocatore1);
            this.CanvasPanel.Controls.Add(this.label1);
            this.CanvasPanel.Controls.Add(this.progressBarVitaGiocatore2);
            this.CanvasPanel.Location = new System.Drawing.Point(0, 0);
            this.CanvasPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CanvasPanel.Name = "CanvasPanel";
            this.CanvasPanel.Size = new System.Drawing.Size(1280, 720);
            this.CanvasPanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1044, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player 2:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
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
            this.Controls.Add(this.CanvasPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserControlGioco";
            this.Size = new System.Drawing.Size(1280, 720);
            this.CanvasPanel.ResumeLayout(false);
            this.CanvasPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarVitaGiocatore1;
        private System.Windows.Forms.ProgressBar progressBarVitaGiocatore2;
        private System.Windows.Forms.Panel CanvasPanel;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
