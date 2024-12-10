namespace corretto.UserControls
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
            this.pictureBoxVisualizzaPersonaggio = new System.Windows.Forms.PictureBox();
            this.labelPersonaggio = new System.Windows.Forms.Label();
            this.labelGiocatoreSelezionato = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelNomeGiocatoreSelezionato = new System.Windows.Forms.Label();
            this.pictureBoxPersonaggio1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStickman)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVisualizzaPersonaggio)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPersonaggio1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxStickman
            // 
            this.pictureBoxStickman.AccessibleDescription = "Stickman";
            this.pictureBoxStickman.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxStickman.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStickman.Image")));
            this.pictureBoxStickman.Location = new System.Drawing.Point(387, 52);
            this.pictureBoxStickman.Name = "pictureBoxStickman";
            this.pictureBoxStickman.Size = new System.Drawing.Size(114, 80);
            this.pictureBoxStickman.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStickman.TabIndex = 0;
            this.pictureBoxStickman.TabStop = false;
            this.pictureBoxStickman.Tag = "";
            this.pictureBoxStickman.Click += new System.EventHandler(this.pictureBoxStickman_Click);
            // 
            // pictureBoxVisualizzaPersonaggio
            // 
            this.pictureBoxVisualizzaPersonaggio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxVisualizzaPersonaggio.Location = new System.Drawing.Point(125, 290);
            this.pictureBoxVisualizzaPersonaggio.MaximumSize = new System.Drawing.Size(100, 100);
            this.pictureBoxVisualizzaPersonaggio.MinimumSize = new System.Drawing.Size(100, 100);
            this.pictureBoxVisualizzaPersonaggio.Name = "pictureBoxVisualizzaPersonaggio";
            this.pictureBoxVisualizzaPersonaggio.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxVisualizzaPersonaggio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxVisualizzaPersonaggio.TabIndex = 1;
            this.pictureBoxVisualizzaPersonaggio.TabStop = false;
            this.pictureBoxVisualizzaPersonaggio.Click += new System.EventHandler(this.pictureBoxVisualizzaPersonaggio_Click);
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
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(387, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "SELEZIONE PERSONAGGIO";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Location = new System.Drawing.Point(586, 490);
            this.buttonPlay.MaximumSize = new System.Drawing.Size(50, 50);
            this.buttonPlay.MinimumSize = new System.Drawing.Size(50, 50);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(50, 50);
            this.buttonPlay.TabIndex = 5;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel1.BackgroundImage")));
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259F));
            this.tableLayoutPanel1.Controls.Add(this.buttonPlay, 6, 7);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxStickman, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxVisualizzaPersonaggio, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelNomeGiocatoreSelezionato, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxPersonaggio1, 5, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.87805F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.12195F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(842, 554);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // labelNomeGiocatoreSelezionato
            // 
            this.labelNomeGiocatoreSelezionato.AutoSize = true;
            this.labelNomeGiocatoreSelezionato.Location = new System.Drawing.Point(125, 182);
            this.labelNomeGiocatoreSelezionato.Name = "labelNomeGiocatoreSelezionato";
            this.labelNomeGiocatoreSelezionato.Size = new System.Drawing.Size(35, 13);
            this.labelNomeGiocatoreSelezionato.TabIndex = 6;
            this.labelNomeGiocatoreSelezionato.Text = "Name";
            // 
            // pictureBoxPersonaggio1
            // 
            this.pictureBoxPersonaggio1.AccessibleDescription = "Personaggio1";
            this.pictureBoxPersonaggio1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPersonaggio1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPersonaggio1.Image")));
            this.pictureBoxPersonaggio1.Location = new System.Drawing.Point(507, 52);
            this.pictureBoxPersonaggio1.Name = "pictureBoxPersonaggio1";
            this.pictureBoxPersonaggio1.Size = new System.Drawing.Size(73, 80);
            this.pictureBoxPersonaggio1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPersonaggio1.TabIndex = 7;
            this.pictureBoxPersonaggio1.TabStop = false;
            this.pictureBoxPersonaggio1.Tag = "";
            this.pictureBoxPersonaggio1.Click += new System.EventHandler(this.pictureBoxPersonaggio1_Click);
            // 
            // UserControlSelezionePersonaggio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelGiocatoreSelezionato);
            this.Controls.Add(this.labelPersonaggio);
            this.DoubleBuffered = true;
            this.Name = "UserControlSelezionePersonaggio";
            this.Size = new System.Drawing.Size(842, 554);
            this.Load += new System.EventHandler(this.UserControlSelezionePersonaggio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStickman)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVisualizzaPersonaggio)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPersonaggio1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxStickman;
        private System.Windows.Forms.PictureBox pictureBoxVisualizzaPersonaggio;
        private System.Windows.Forms.Label labelPersonaggio;
        private System.Windows.Forms.Label labelGiocatoreSelezionato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelNomeGiocatoreSelezionato;
        private System.Windows.Forms.PictureBox pictureBoxPersonaggio1;
    }
}
