﻿using System.Drawing;

namespace Client
{
    partial class FormSceltaPersonaggio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSceltaPersonaggio));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.buttonAvanti = new System.Windows.Forms.Button();
            this.labelAvanti = new System.Windows.Forms.Label();
            this.labelPersonaggioSelezionato = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(101, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 147);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(323, 142);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(145, 147);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(552, 142);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(145, 147);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // buttonAvanti
            // 
            this.buttonAvanti.Location = new System.Drawing.Point(349, 378);
            this.buttonAvanti.Name = "buttonAvanti";
            this.buttonAvanti.Size = new System.Drawing.Size(93, 39);
            this.buttonAvanti.TabIndex = 3;
            this.buttonAvanti.Text = "Avanti";
            this.buttonAvanti.UseVisualStyleBackColor = true;
            this.buttonAvanti.Click += new System.EventHandler(this.buttonAvanti_Click);
            // 
            // labelAvanti
            // 
            this.labelAvanti.AutoSize = true;
            this.labelAvanti.Location = new System.Drawing.Point(280, 335);
            this.labelAvanti.Name = "labelAvanti";
            this.labelAvanti.Size = new System.Drawing.Size(128, 13);
            this.labelAvanti.TabIndex = 4;
            this.labelAvanti.Text = "Personaggio selezionato: ";
            // 
            // labelPersonaggioSelezionato
            // 
            this.labelPersonaggioSelezionato.AutoSize = true;
            this.labelPersonaggioSelezionato.Location = new System.Drawing.Point(434, 335);
            this.labelPersonaggioSelezionato.Name = "labelPersonaggioSelezionato";
            this.labelPersonaggioSelezionato.Size = new System.Drawing.Size(0, 13);
            this.labelPersonaggioSelezionato.TabIndex = 5;
            // 
            // FormSceltaPersonaggio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelPersonaggioSelezionato);
            this.Controls.Add(this.labelAvanti);
            this.Controls.Add(this.buttonAvanti);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "FormSceltaPersonaggio";
            this.Text = "FormSceltaPersonaggio";
            this.Load += new System.EventHandler(this.FormSceltaPersonaggio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button buttonAvanti;
        private System.Windows.Forms.Label labelAvanti;
        private System.Windows.Forms.Label labelPersonaggioSelezionato;
    }
}