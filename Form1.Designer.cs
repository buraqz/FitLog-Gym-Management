namespace FITLOG_Gym_Management
{
    partial class FormGiris
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGiris));
            this.labelKullaniciAdi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.kullaniciAdTextbox = new System.Windows.Forms.MaskedTextBox();
            this.sifreTextBox = new System.Windows.Forms.MaskedTextBox();
            this.buttonGiris = new System.Windows.Forms.Button();
            this.buttonKayit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelKullaniciAdi
            // 
            this.labelKullaniciAdi.AutoSize = true;
            this.labelKullaniciAdi.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKullaniciAdi.Location = new System.Drawing.Point(114, 245);
            this.labelKullaniciAdi.Name = "labelKullaniciAdi";
            this.labelKullaniciAdi.Size = new System.Drawing.Size(125, 24);
            this.labelKullaniciAdi.TabIndex = 1;
            this.labelKullaniciAdi.Text = "Kullanıcı Adı:";
            this.labelKullaniciAdi.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(114, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Şifre:";
            // 
            // kullaniciAdTextbox
            // 
            this.kullaniciAdTextbox.Location = new System.Drawing.Point(245, 249);
            this.kullaniciAdTextbox.Name = "kullaniciAdTextbox";
            this.kullaniciAdTextbox.Size = new System.Drawing.Size(147, 22);
            this.kullaniciAdTextbox.TabIndex = 3;
            // 
            // sifreTextBox
            // 
            this.sifreTextBox.Location = new System.Drawing.Point(245, 284);
            this.sifreTextBox.Name = "sifreTextBox";
            this.sifreTextBox.Size = new System.Drawing.Size(147, 22);
            this.sifreTextBox.TabIndex = 4;
            // 
            // buttonGiris
            // 
            this.buttonGiris.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonGiris.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.buttonGiris.Location = new System.Drawing.Point(118, 321);
            this.buttonGiris.Name = "buttonGiris";
            this.buttonGiris.Size = new System.Drawing.Size(274, 32);
            this.buttonGiris.TabIndex = 5;
            this.buttonGiris.Text = "GİRİŞ YAP";
            this.buttonGiris.UseVisualStyleBackColor = true;
            this.buttonGiris.Click += new System.EventHandler(this.buttonGiris_Click);
            // 
            // buttonKayit
            // 
            this.buttonKayit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.buttonKayit.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonKayit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonKayit.Location = new System.Drawing.Point(118, 359);
            this.buttonKayit.Name = "buttonKayit";
            this.buttonKayit.Size = new System.Drawing.Size(274, 32);
            this.buttonKayit.TabIndex = 6;
            this.buttonKayit.Text = "HESAP OLUŞTUR";
            this.buttonKayit.UseVisualStyleBackColor = false;
            this.buttonKayit.Click += new System.EventHandler(this.buttonKayit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FITLOG_Gym_Management.Properties.Resources.fitlog_logo_siyah_png;
            this.pictureBox1.Location = new System.Drawing.Point(99, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 301);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.buttonKayit);
            this.panel1.Controls.Add(this.buttonGiris);
            this.panel1.Controls.Add(this.sifreTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.kullaniciAdTextbox);
            this.panel1.Controls.Add(this.labelKullaniciAdi);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(218, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 452);
            this.panel1.TabIndex = 7;
            // 
            // FormGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 553);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGiris";
            this.Text = "FitLog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelKullaniciAdi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox kullaniciAdTextbox;
        private System.Windows.Forms.MaskedTextBox sifreTextBox;
        private System.Windows.Forms.Button buttonGiris;
        private System.Windows.Forms.Button buttonKayit;
        private System.Windows.Forms.Panel panel1;
    }
}

