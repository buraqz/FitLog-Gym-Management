using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FITLOG_Gym_Management.FormGiris;

namespace FITLOG_Gym_Management
{
    public partial class FormAnaSayfa : Form
    {
        SqlDataAdapter da;
        DataTable dt;
        string conString = "Data Source=.;Initial Catalog=FitLogDB;Integrated Security=True";
        public FormAnaSayfa()
        {
            InitializeComponent();
        }

        private void FormAnaSayfa_Load(object sender, EventArgs e)
        {
            labelIsim.Text = FormGiris.AktifKullanici.KullaniciAdi;
            panelHesap.Visible = false;
        }

        private void buttonUyeler_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            dgvListe.Visible = true;
            panelHesap.Visible= false;
            button1.Visible = true;
            buttonKaldir.Visible = true;

            dgvListe.DataSource = null;
            dgvListe.Columns.Clear();
            dgvListe.ReadOnly = false;

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    baglanti.Open();

                    //tabloyu cektik
                    string sorgu = "SELECT * FROM Uyeler WHERE YoneticiID = " + AktifKullanici.YoneticiID;

                    da = new SqlDataAdapter(sorgu, baglanti);

                    //sql komutlarını hazırlama
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);

                    dt = new DataTable();
                    da.Fill(dt);

                    dgvListe.DataSource = dt;
                    RenkDegistir();
                    

                    //tasarım
                    dgvListe.Columns["UyeID"].ReadOnly = true; //id değiştirilemez yapma
                    dgvListe.Columns["AktifMi"].Visible = false; //aktifliği renkler kullanarak ayarlayacağız
                    dgvListe.Columns["YoneticiID"].Visible = false; //yonetcici ID kullanıcı görmez
                    dgvListe.Columns["AdSoyad"].HeaderText = "AD SOYAD"; //diğer düzwnlemeler
                    dgvListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //yeni satır eklemeye izin verme
                    dgvListe.AllowUserToAddRows = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veriler yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void dgvListe_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["YoneticiID"].Value = AktifKullanici.YoneticiID;

            if (dgvListe.Columns.Contains("AktifMi"))
            {
                //yeni satıra tıklandığında Yönetici id ve aktiflik doldıur
                e.Row.Cells["AktifMi"].Value = true;
            }
            else if (dgvListe.Columns.Contains("PaketSuresi"))
            {
                e.Row.Cells["PaketSuresi"].Value = 6; // Varsayılan paket süresi 6 ay
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //düzenleme bitir
                dgvListe.EndEdit();

                if (da != null && dt != null)
                {
                    da.SelectCommand.Connection = new SqlConnection(conString);

                    //değişiklik kaydet
                    da.Update(dt);

                    MessageBox.Show("Tüm değişiklikler başarıyla kaydedildi!", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında bir sorun çıktı: " + ex.Message);
            }
        }

        private void buttonKaldir_Click(object sender, EventArgs e)
        {
            //seçili satır var mı diye kontrol
            if (dgvListe.SelectedRows.Count > 0)
            {
                //hangi tabloda oldugumuzun kontrolu
                if (dgvListe.Columns.Contains("SureAy"))
                {
                    int silinecekPaketID = Convert.ToInt32(dgvListe.SelectedRows[0].Cells["PaketID"].Value);

                    using (SqlConnection baglanti = new SqlConnection(conString))
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Uyeler WHERE PaketID = @p1", baglanti);
                        komut.Parameters.AddWithValue("@p1", silinecekPaketID);
                        int kullananSayisi = Convert.ToInt32(komut.ExecuteScalar());

                        if (kullananSayisi > 0)
                        {
                            MessageBox.Show("Bu paketi aktif olarak kullanan " + kullananSayisi + " üye var. Önce onların paketini değiştirmelisin.", "Silme Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // silme işlemi iptal etme
                        }
                    }
                }
                foreach (DataGridViewRow row in dgvListe.SelectedRows)
                {
                    if (!row.IsNewRow) //Yeni satırı silmez
                    {
                        dgvListe.Rows.Remove(row);
                    }
                }
            }
        }

        private void BitisTarihiHesapla(int satirIndex)
        {
            try
            {
                var paketIdObj = dgvListe.Rows[satirIndex].Cells["PaketID"].Value;
                var baslangicObj = dgvListe.Rows[satirIndex].Cells["BaslangicTarihi"].Value;

                if (paketIdObj != null && baslangicObj != DBNull.Value)
                {
                    int paketID = Convert.ToInt32(paketIdObj);
                    DateTime baslangicTarihi = Convert.ToDateTime(baslangicObj);
                    int paketSuresiAy = 0;

                    //Veritabanından ay bilgisi gelişr
                    using (SqlConnection baglanti = new SqlConnection(conString))
                    {
                        baglanti.Open();
                        string sorgu = "SELECT SureAy FROM Paketler WHERE PaketID = @p1";
                        SqlCommand komut = new SqlCommand(sorgu, baglanti);
                        komut.Parameters.AddWithValue("@p1", paketID);

                        object sonuc = komut.ExecuteScalar();
                        if (sonuc != null)
                        {
                            paketSuresiAy = Convert.ToInt32(sonuc);
                        }
                    }

                    if (paketSuresiAy > 0)
                    {
                        dgvListe.Rows[satirIndex].Cells["BitisTarihi"].Value = baslangicTarihi.AddMonths(paketSuresiAy);
                    }
                }
            }
            catch (Exception ex)
            {
                //hata
                Console.WriteLine("Tarih hesaplama hatası: " + ex.Message);
            }
        }
        private void dgvListe_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //değişen hucreye göre hesaplama yapma
            if (e.RowIndex >= 0 && (dgvListe.Columns[e.ColumnIndex].Name == "PaketID" || dgvListe.Columns[e.ColumnIndex].Name == "BaslangicTarihi"))
            {
                BitisTarihiHesapla(e.RowIndex);
            }
        }
        private void RenkDegistir()
        {

            foreach (DataGridViewRow satir in dgvListe.Rows)
            {
                if (!satir.IsNewRow && satir.Cells["BitisTarihi"].Value != DBNull.Value)
                {
                    DateTime bitisTarihi = Convert.ToDateTime(satir.Cells["BitisTarihi"].Value);

                    //bitiş tarih kontrol
                    if (bitisTarihi < DateTime.Now)
                    {
                        // tasarım değiştirme
                        satir.DefaultCellStyle.BackColor = Color.Crimson;
                        satir.DefaultCellStyle.ForeColor = Color.White;
                        satir.DefaultCellStyle.SelectionBackColor = Color.DarkRed;

                        satir.Cells["AktifMi"].Value = false;
                    }
                    else if ((bitisTarihi - DateTime.Now).TotalDays <= 10)
                    {
                        satir.DefaultCellStyle.BackColor = Color.Orange;
                        satir.DefaultCellStyle.ForeColor = Color.White;
                        satir.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
                    }
                    else
                    {
                        satir.Cells["AktifMi"].Value = true;
                    }
                }
            }
        }
        private void buttonPaketler_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            dgvListe.Visible = true;
            panelHesap.Visible = false;
            button1.Visible = true;
            buttonKaldir.Visible = true;


            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    //paket çekme
                    string sorgu = "SELECT * FROM Paketler WHERE YoneticiID = " + AktifKullanici.YoneticiID;

                    da = new SqlDataAdapter(sorgu, baglanti);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);

                    dt = new DataTable();
                    da.Fill(dt);

                    dgvListe.DataSource = dt;

                    // tasarım düzenlemeleri
                    dgvListe.Columns["PaketID"].ReadOnly = true;
                    dgvListe.Columns["YoneticiID"].Visible = false;
                    dgvListe.Columns["PaketAdi"].HeaderText = "PAKET ADI";
                    dgvListe.Columns["SureAy"].HeaderText = "SÜRE (AY)";
                    dgvListe.Columns["Fiyat"].HeaderText = "FİYAT (TL)";

                    dgvListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvListe.AllowUserToAddRows = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Paketler yüklenirken hata: " + ex.Message);
                }
            }
        }

        private void dgvListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonHesap_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            dgvListe.Visible = false;
            panelHesap.Visible = true;
            button1.Visible = false;
            buttonKaldir.Visible = false;

            HesabimiYukle();


        }

        private void HesabimiYukle()
        {
            labelKullaniciAdB.Text = "Kullanıcı Adınız: " + AktifKullanici.KullaniciAdi;

            //tablo doldurma
            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    string sorgu = @"SELECT p.PaketAdi AS 'Paket Adı', COUNT(u.UyeID) AS 'Kişi Sayısı' 
                             FROM Uyeler u 
                             JOIN Paketler p ON u.PaketID = p.PaketID 
                             WHERE u.YoneticiID = @id AND u.AktifMi = 1
                             GROUP BY p.PaketAdi";

                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@id", AktifKullanici.YoneticiID);

                    SqlDataAdapter daIstatistik = new SqlDataAdapter(komut);
                    DataTable dtIstatistik = new DataTable();
                    daIstatistik.Fill(dtIstatistik);

                    dgvIstatistik.DataSource = dtIstatistik;

                    //tasarım
                    dgvIstatistik.ReadOnly = true;
                    dgvIstatistik.AllowUserToAddRows = false; 
                    dgvIstatistik.AllowUserToDeleteRows = false;
                    dgvIstatistik.RowHeadersVisible = false; 
                    dgvIstatistik.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İstatistikler çekilirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void buttonCikis_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Hesabınızdan çıkış yapmak istediğinize emin misiniz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (onay == DialogResult.Yes)
            {
                
                Application.Restart();
            }
        }
    }
}
