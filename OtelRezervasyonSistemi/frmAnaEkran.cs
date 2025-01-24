using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OtelRezervasyonSistemi.dal;
using OtelRezervasyonSistemi.SERVICE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static OtelRezervasyonSistemi.SERVICE.odaService;

namespace OtelRezervasyonSistemi
{
   
    public partial class frmAnaEkran : Form
    {
        private RezervasyonService _rezervasyonService;

        private readonly OdaService _odaService;

        public frmAnaEkran()
        {
            InitializeComponent();

            _rezervasyonService = new RezervasyonService();
            _odaService = new OdaService();

            // Cinsiyet ComboBox'ını doldur
            cmbCinsiyet.Items.AddRange(new object[] { "Erkek", "Kadın" });

            // Oda numaralarını doldur
            cmbOda2.Items.AddRange(new object[] { "101", "102", "103", "104", "105" });

            // DateTimePicker'ları ayarla
            girisDate.Format = DateTimePickerFormat.Custom;
            girisDate.CustomFormat = "dd MMMM yyyy";
            cıkısDate.Format = DateTimePickerFormat.Custom;
            cıkısDate.CustomFormat = "dd MMMM yyyy";


        }
      
        private void frmAnaEkran_Load(object sender, EventArgs e)
        {
            /*  string colorCode = "#FF5733"; // İstediğiniz HTML renk kodunu buraya yazın
              this.BackColor = ColorTranslator.FromHtml(colorCode);*/
            rezarvasyonDAO rz = new rezarvasyonDAO();

            System.Data.DataTable dt = rz.rezarvasyonGoruntule();
            dtaGrid.DataSource = dt;


        }
        private void FormuTemizle()
        {
            txtAdSoyad.Clear();
            txtTc.Clear();
            txtTelNo.Clear();
            cmbCinsiyet.SelectedIndex = -1;
            girisDate.Value = DateTime.Now;
            cıkısDate.Value = DateTime.Now;
            cmbOda2.SelectedIndex = -1;
        }
        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayısal değer ve kontrol tuşlarına izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayısal değer ve kontrol tuşlarına izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void girisTarihi_ValueChanged(object sender, EventArgs e)
        {
            // Giriş tarihi çıkış tarihinden sonra olamaz
            if (girisDate.Value > cıkısDate.Value)
            {
                cıkısDate.Value = girisDate.Value.AddDays(1);
            }
        }

        private void cikisDate_ValueChanged(object sender, EventArgs e)
        {
            // Çıkış tarihi giriş tarihinden önce olamaz
            if (cıkısDate.Value < girisDate.Value)
            {
                cıkısDate.Value = girisDate.Value.AddDays(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string secilenMusteri = cmbMusteri.Text;

            // Eğer ComboBox boşsa veya "Tümü" yazıyorsa, bunu kontrol ediyoruz
            if (string.IsNullOrWhiteSpace(secilenMusteri))
            {
                secilenMusteri = "Tümü"; // Boşsa varsayılan olarak "Tümü" kabul ediyoruz
                cmbMusteri.Text = "Tümü"; // ComboBox'a "Tümü" yazısını ekliyoruz
            }

            musteriDAO oda = new musteriDAO();
            DataTable dt = oda.musteriGoruntule(secilenMusteri);
            dtaGrid.DataSource = dt; // Verileri DataGrid'e aktarıyoruz


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
       
        private void LoadOdaDurumComboBox()
        {
            comboBox1.Items.AddRange(new[] { "Boş", "Dolu" });
        }
        private void button6_Click(object sender, EventArgs e)
        {
            odaDAO oda = new odaDAO();
            oda.odaDurumGuncelle(cmbOda.Text, comboBox1.Text);
            string odaNo = cmbOda.Text.Trim();
            string yeniDurum = comboBox1.Text;

            if (_odaService.OdaDurumuGuncelle(odaNo, yeniDurum))
            {
                MessageBox.Show("Oda durumu başarıyla güncellendi.");
                btnOdaBilgi_Click(sender, e); // Listeyi yenile
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
          

        }

        private void btnSil_Click(object sender, EventArgs e)
        {

        }

        private void btnRandevuGoruntule_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable rezervasyonlar = _rezervasyonService.TumRezervasyonlariGetir();
                // DataGridView'e verileri yükle
                dtaGrid.DataSource = rezervasyonlar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyonlar görüntülenirken hata oluştu: " + ex.Message);
            }

        }

        private void btnOluştur_Click(object sender, EventArgs e)
        {
            try
            {
                bool sonuc = _rezervasyonService.RezervasyonOlustur(
                    txtAdSoyad.Text,
                    txtTc.Text,
                    txtTelNo.Text,
                    cmbCinsiyet.SelectedItem?.ToString(),
                    girisDate.Value.ToString("yyyy-MM-dd"),
                    cıkısDate.Value.ToString("yyyy-MM-dd"),
                    cmbOda2.SelectedItem?.ToString()
                );

                if (sonuc)
                {
                    MessageBox.Show("Rezervasyon başarıyla oluşturuldu.");
                    FormuTemizle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon oluşturulurken hata oluştu: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bool sonuc = _rezervasyonService.RezervasyonSil(
                    txtAdSoyad.Text,
                    cmbOda2.SelectedItem?.ToString(),
                    txtTelNo.Text,
                    cmbCinsiyet.SelectedItem?.ToString(),
                    girisDate.Value.ToString("yyyy-MM-dd"),
                    cıkısDate.Value.ToString("yyyy-MM-dd"),
                    txtTc.Text
                );

                if (sonuc)
                {
                    FormuTemizle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon silinirken hata oluştu: " + ex.Message);
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (_rezervasyonService == null)
            {
                MessageBox.Show("_rezervasyonService nesnesi tanımlı değil!");
                return;
            }
            try
            {
                bool sonuc = _rezervasyonService.RezervasyonGuncelle(
                    txtAdSoyad.Text,
                    cmbOda2.SelectedItem?.ToString(),
                    txtTelNo.Text,
                    cmbCinsiyet.SelectedItem?.ToString(),
                    girisDate.Value.ToString("yyyy-MM-dd"),
                    cıkısDate.Value.ToString("yyyy-MM-dd"),
                    txtTc.Text
                );

                if (sonuc)
                {
                    MessageBox.Show("Rezervasyon başarıyla güncellendi.");
                    FormuTemizle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon güncellenirken hata oluştu: " + ex.Message);
            }
        }

        private void dtaGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;
            if (selectedRowIndex >= 0)
            {
                // Seçilen satırı al
                DataGridViewRow selectedRow = dtaGrid.Rows[selectedRowIndex];

                // TextBox'lara hücre bilgilerini aktar
                txtAdSoyad.Text = selectedRow.Cells[0].Value?.ToString(); // 1. sütun
                cmbOda2.Text = selectedRow.Cells[6].Value?.ToString(); // 2. sütun
                txtTelNo.Text = selectedRow.Cells[2].Value?.ToString(); // 3. sütun
                cmbCinsiyet.Text = selectedRow.Cells[3].Value?.ToString(); // 1. sütun
                girisDate.Text = selectedRow.Cells[4].Value?.ToString(); // 2. sütun
                cıkısDate.Text = selectedRow.Cells[5].Value?.ToString(); // 3. sütun
                txtTc.Text = selectedRow.Cells[1].Value?.ToString();
            }
        }

        private void txtOdaNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbOda_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dtaGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnOdaBilgi_Click(object sender, EventArgs e)
        {
            odaDAO rz = new odaDAO();
            
            string odaNo = cmbOda.Text.Trim();
            DataTable odaBilgileri = _odaService.OdaBilgileriniGetir(odaNo);

            // Gösterilen DataGridView'a verileri bağla
            dtaGrid.DataSource = odaBilgileri;

            // DataGrid'e veriyi bağla
            

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
