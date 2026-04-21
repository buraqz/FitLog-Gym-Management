using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FITLOG_Gym_Management
{
    public partial class FormGiris : Form
    {
        public FormGiris()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //boş kutu ama kod bozulmasın diye silmedim
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //boş kutu ama kod bozulmasın diye silmedim
        }

        public static class AktifKullanici
        {
            //aktif kullanıcı tutuyoruz
            public static int YoneticiID { get; set; }
            public static string KullaniciAdi { get; set; }
        }

        private void buttonGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = kullaniciAdTextbox.Text.Trim();
            string sifre = sifreTextBox.Text.Trim();


            //bosluk kontrol
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Kullanıcı adı ve şifre alanları boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=.;Initial Catalog=FitLogDB;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    baglanti.Open();
                    //sql sorgusu ile şifre ve ad kontrolü
                    string sorgu = "SELECT YoneticiID, KullaniciAdi FROM Yoneticiler WHERE KullaniciAdi=@p1 AND Sifre=@p2";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@p1", kullaniciAdi);
                    komut.Parameters.AddWithValue("@p2", sifre);

                    SqlDataReader dr = komut.ExecuteReader();

                    if (dr.Read())
                    {
                        AktifKullanici.YoneticiID = Convert.ToInt32(dr["YoneticiID"]);
                        AktifKullanici.KullaniciAdi = dr["KullaniciAdi"].ToString();

                        MessageBox.Show($"Hoş geldin {AktifKullanici.KullaniciAdi}! Sisteme yönlendiriliyorsun.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ana sayfaya geçiş yapma
                        FormAnaSayfa anaSayfa = new FormAnaSayfa();
                        anaSayfa.FormClosed += (s, args) => this.Close();
                        anaSayfa.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }
        }

        private void buttonKayit_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = kullaniciAdTextbox.Text.Trim();
            string sifre = sifreTextBox.Text.Trim();


            //bosluk kontrol
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Kullanıcı adı ve şifre alanları boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //uzunluk kontrol
            if (sifre.Length < 4)
            {
                MessageBox.Show("Şifreniz en az 4 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=.;Initial Catalog=FitLogDB;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    baglanti.Open();

                    //iki aynı kullanıcı adı olmasın diye kontrol
                    string kontrolSorgu = "SELECT COUNT(*) FROM Yoneticiler WHERE KullaniciAdi=@p1";
                    SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, baglanti);
                    kontrolKomut.Parameters.AddWithValue("@p1", kullaniciAdi);

                    int adSayisi = (int)kontrolKomut.ExecuteScalar();

                    if (adSayisi > 0)
                    {
                        MessageBox.Show("bu kullanıcı adı zaten alınmış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    //sorun yoksa kullanıcıyı database'e ekleme
                    string kayitSorgu = "INSERT INTO Yoneticiler (KullaniciAdi, Sifre) VALUES (@p1, @p2)";
                    SqlCommand kayitKomut = new SqlCommand(kayitSorgu, baglanti);
                    kayitKomut.Parameters.AddWithValue("@p1", kullaniciAdi);
                    kayitKomut.Parameters.AddWithValue("@p2", sifre);

                    kayitKomut.ExecuteNonQuery();

                    //başarı mesajı
                    MessageBox.Show("Hesabın başarıyla oluşturuldu! Şimdi giriş yapabilirsin.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sifreTextBox.Clear();
                    kullaniciAdTextbox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message);
                }
            }

        }
    }
}
