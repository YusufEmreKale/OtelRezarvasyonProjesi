using OtelRezervasyonSistemi.dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonSistemi.SERVICE
{
    public class RezervasyonService
    {
        private readonly rezarvasyonDAO _rezervasyonDAO;

        public RezervasyonService()
        {
            _rezervasyonDAO = new rezarvasyonDAO();
        }

        public DataTable TumRezervasyonlariGetir()
        {
            try
            {
                return _rezervasyonDAO.rezarvasyonGoruntule();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyonlar getirilirken bir hata oluştu: " + ex.Message);
                return new DataTable();
            }
        }

        public bool RezervasyonOlustur(string rezervasyonSahibi, string tcNo, string telNo,
            string cinsiyet, string baslangicTarihi, string bitisTarihi, string odaNo)
        {
            try
            {
              

                if (tcNo.Length != 11)
                {
                    MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır.");
                    return false;
                }

                // Tarih kontrolü
                if (!DateTime.TryParse(baslangicTarihi, out DateTime baslangic) ||
                    !DateTime.TryParse(bitisTarihi, out DateTime bitis))
                {
                    MessageBox.Show("Geçersiz tarih formatı.");
                    return false;
                }

                if (baslangic > bitis)
                {
                    MessageBox.Show("Başlangıç tarihi bitiş tarihinden sonra olamaz.");
                    return false;
                }

                return _rezervasyonDAO.rezervasyonEkle(rezervasyonSahibi, tcNo, telNo,
                    cinsiyet, baslangicTarihi, bitisTarihi, odaNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon oluşturulurken bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        public bool RezervasyonGuncelle(string rezervasyonSahibi, string odaNo, string telNo,
            string cinsiyet, string baslangicTarihi, string bitisTarihi, string tcNo)
        {
            try
            {
               

                // Tarih kontrolü
                if (!DateTime.TryParse(baslangicTarihi, out DateTime baslangic) ||
                    !DateTime.TryParse(bitisTarihi, out DateTime bitis))
                {
                    MessageBox.Show("Geçersiz tarih formatı.");
                    return false;
                }

                if (baslangic > bitis)
                {
                    MessageBox.Show("Başlangıç tarihi bitiş tarihinden sonra olamaz.");
                    return false;
                }

                return _rezervasyonDAO.rezervasyonGuncelle(rezervasyonSahibi, odaNo, telNo,
                    cinsiyet, baslangicTarihi, bitisTarihi, tcNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon güncellenirken bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        public bool RezervasyonSil(string rezervasyonSahibi, string odaNo, string telNo,
            string cinsiyet, string baslangicTarihi, string bitisTarihi, string tcNo)
        {
            try
            {
                // Silme işlemi öncesi onay
                DialogResult dr = MessageBox.Show("Bu rezervasyonu silmek istediğinizden emin misiniz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    return _rezervasyonDAO.rezervasyonSil(rezervasyonSahibi, odaNo, telNo,
                        cinsiyet, baslangicTarihi, bitisTarihi, tcNo);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon silinirken bir hata oluştu: " + ex.Message);
                return false;
            }
        }
    }
}
