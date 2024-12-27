namespace FightingGameClient.UserControls
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
            this.pictureBoxWarrior_2 = new System.Windows.Forms.PictureBox();
            this.labelPersonaggio = new System.Windows.Forms.Label();
            this.labelGiocatoreSelezionato = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.labelNomeGiocatoreSelezionato = new System.Windows.Forms.Label();
            this.pictureBoxFireWizard = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelVisualizzaPersonaggio = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarrior_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFireWizard)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxWarrior_2
            // 
            this.pictureBoxWarrior_2.AccessibleDescription = "Warrior_2";
            this.pictureBoxWarrior_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxWarrior_2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWarrior_2.Image")));
            this.pictureBoxWarrior_2.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxWarrior_2.Name = "pictureBoxWarrior_2";
            this.pictureBoxWarrior_2.Size = new System.Drawing.Size(162, 154);
            this.pictureBoxWarrior_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxWarrior_2.TabIndex = 0;
            this.pictureBoxWarrior_2.TabStop = false;
            this.pictureBoxWarrior_2.Tag = "";
            this.pictureBoxWarrior_2.Click += new System.EventHandler(this.pictureBoxWarrior_2_Click);
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
            // pictureBoxFireWizard
            // 
            this.pictureBoxFireWizard.AccessibleDescription = "FireWizard";
            this.pictureBoxFireWizard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxFireWizard.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFireWizard.Image")));
            this.pictureBoxFireWizard.Location = new System.Drawing.Point(171, 3);
            this.pictureBoxFireWizard.Name = "pictureBoxFireWizard";
            this.pictureBoxFireWizard.Size = new System.Drawing.Size(163, 154);
            this.pictureBoxFireWizard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFireWizard.TabIndex = 7;
            this.pictureBoxFireWizard.TabStop = false;
            this.pictureBoxFireWizard.Tag = "";
            this.pictureBoxFireWizard.Click += new System.EventHandler(this.pictureBoxWizard_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxWarrior_2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxFireWizard, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(467, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(337, 160);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanelVisualizzaPersonaggio
            // 
            this.tableLayoutPanelVisualizzaPersonaggio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanelVisualizzaPersonaggio.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelVisualizzaPersonaggio.ColumnCount = 1;
            this.tableLayoutPanelVisualizzaPersonaggio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelVisualizzaPersonaggio.Location = new System.Drawing.Point(108, 198);
            this.tableLayoutPanelVisualizzaPersonaggio.Name = "tableLayoutPanelVisualizzaPersonaggio";
            this.tableLayoutPanelVisualizzaPersonaggio.RowCount = 1;
            this.tableLayoutPanelVisualizzaPersonaggio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelVisualizzaPersonaggio.Size = new System.Drawing.Size(207, 200);
            this.tableLayoutPanelVisualizzaPersonaggio.TabIndex = 9;
            // 
            // UserControlSelezionePersonaggio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tableLayoutPanelVisualizzaPersonaggio);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarrior_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFireWizard)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxWarrior_2;
        private System.Windows.Forms.Label labelPersonaggio;
        private System.Windows.Forms.Label labelGiocatoreSelezionato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelNomeGiocatoreSelezionato;
        private System.Windows.Forms.PictureBox pictureBoxFireWizard;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelVisualizzaPersonaggio;
    }
}
