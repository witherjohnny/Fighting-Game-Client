﻿namespace FightingGameClient.UserControls
{
    partial class UserControlSelezionePersonaggio
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
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
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlSelezionePersonaggio));
            this.pictureBoxStickman = new System.Windows.Forms.PictureBox();
            this.labelPersonaggio = new System.Windows.Forms.Label();
            this.labelGiocatoreSelezionato = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.labelNomeGiocatoreSelezionato = new System.Windows.Forms.Label();
            this.pictureBoxPersonaggio1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStickman)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPersonaggio1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxStickman
            // 
            this.pictureBoxStickman.AccessibleDescription = "Stickman";
            this.pictureBoxStickman.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxStickman.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStickman.Image")));
            this.pictureBoxStickman.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxStickman.Name = "pictureBoxStickman";
            this.pictureBoxStickman.Size = new System.Drawing.Size(162, 154);
            this.pictureBoxStickman.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStickman.TabIndex = 0;
            this.pictureBoxStickman.TabStop = false;
            this.pictureBoxStickman.Tag = "";
            this.pictureBoxStickman.Click += new System.EventHandler(this.pictureBoxStickman_Click);
            // 
            // labelPersonaggio
            // 
            this.labelPersonaggio.AutoSize = true;
            this.labelPersonaggio.Location = new System.Drawing.Point(137, 292);
            this.labelPersonaggio.Name = "labelPersonaggio";
            this.labelPersonaggio.Size = new System.Drawing.Size(0, 13);
            this.labelPersonaggio.TabIndex = 2;
            // 
            // labelGiocatoreSelezionato
            // 
            this.labelGiocatoreSelezionato.AutoSize = true;
            this.labelGiocatoreSelezionato.Location = new System.Drawing.Point(180, 273);
            this.labelGiocatoreSelezionato.Name = "labelGiocatoreSelezionato";
            this.labelGiocatoreSelezionato.Size = new System.Drawing.Size(0, 13);
            this.labelGiocatoreSelezionato.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "SELEZIONE PERSONAGGIO";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Location = new System.Drawing.Point(650, 440);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(154, 74);
            this.buttonPlay.TabIndex = 5;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // labelNomeGiocatoreSelezionato
            // 
            this.labelNomeGiocatoreSelezionato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNomeGiocatoreSelezionato.AutoSize = true;
            this.labelNomeGiocatoreSelezionato.Location = new System.Drawing.Point(109, 174);
            this.labelNomeGiocatoreSelezionato.Name = "labelNomeGiocatoreSelezionato";
            this.labelNomeGiocatoreSelezionato.Size = new System.Drawing.Size(35, 13);
            this.labelNomeGiocatoreSelezionato.TabIndex = 6;
            this.labelNomeGiocatoreSelezionato.Text = "Name";
            // 
            // pictureBoxPersonaggio1
            // 
            this.pictureBoxPersonaggio1.AccessibleDescription = "warrior1";
            this.pictureBoxPersonaggio1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxPersonaggio1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPersonaggio1.Image")));
            this.pictureBoxPersonaggio1.Location = new System.Drawing.Point(171, 3);
            this.pictureBoxPersonaggio1.Name = "pictureBoxPersonaggio1";
            this.pictureBoxPersonaggio1.Size = new System.Drawing.Size(163, 154);
            this.pictureBoxPersonaggio1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPersonaggio1.TabIndex = 7;
            this.pictureBoxPersonaggio1.TabStop = false;
            this.pictureBoxPersonaggio1.Tag = "";
            this.pictureBoxPersonaggio1.Click += new System.EventHandler(this.pictureBoxPersonaggio1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxStickman, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxPersonaggio1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(467, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(337, 160);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(108, 198);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(207, 200);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // UserControlSelezionePersonaggio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.labelNomeGiocatoreSelezionato);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelGiocatoreSelezionato);
            this.Controls.Add(this.labelPersonaggio);
            this.DoubleBuffered = true;
            this.Name = "UserControlSelezionePersonaggio";
            this.Size = new System.Drawing.Size(960, 585);
            this.Load += new System.EventHandler(this.UserControlSelezionePersonaggio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStickman)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPersonaggio1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxStickman;
        private System.Windows.Forms.Label labelPersonaggio;
        private System.Windows.Forms.Label labelGiocatoreSelezionato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelNomeGiocatoreSelezionato;
        private System.Windows.Forms.PictureBox pictureBoxPersonaggio1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
