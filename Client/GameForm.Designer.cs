namespace Client
{
    partial class GameForm
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
            this.progressBarVitaPlayer1 = new System.Windows.Forms.ProgressBar();
            this.progressBarVitaPlayer2 = new System.Windows.Forms.ProgressBar();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // progressBarVitaPlayer1
            // 
            this.progressBarVitaPlayer1.Location = new System.Drawing.Point(101, 51);
            this.progressBarVitaPlayer1.Name = "progressBarVitaPlayer1";
            this.progressBarVitaPlayer1.Size = new System.Drawing.Size(100, 23);
            this.progressBarVitaPlayer1.TabIndex = 0;
            // 
            // progressBarVitaPlayer2
            // 
            this.progressBarVitaPlayer2.Location = new System.Drawing.Point(666, 41);
            this.progressBarVitaPlayer2.Name = "progressBarVitaPlayer2";
            this.progressBarVitaPlayer2.Size = new System.Drawing.Size(100, 23);
            this.progressBarVitaPlayer2.TabIndex = 1;
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(47, 51);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(48, 13);
            this.labelPlayer1.TabIndex = 2;
            this.labelPlayer1.Text = "Player1: ";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(587, 41);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(48, 13);
            this.labelPlayer2.TabIndex = 3;
            this.labelPlayer2.Text = "Player2: ";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 358);
            this.panel1.TabIndex = 4;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.progressBarVitaPlayer2);
            this.Controls.Add(this.progressBarVitaPlayer1);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarVitaPlayer1;
        private System.Windows.Forms.ProgressBar progressBarVitaPlayer2;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Panel panel1;
    }
}