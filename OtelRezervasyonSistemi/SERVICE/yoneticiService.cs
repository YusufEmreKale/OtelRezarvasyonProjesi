using OtelRezervasyonSistemi.dal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonSistemi.SERVICE
{
    public class YoneticiService
    {
        private readonly yoneticiDAO _yoneticiDAO;

        public YoneticiService()
        {
            _yoneticiDAO = new yoneticiDAO();
        }

        public ArrayList TumYoneticileriGetir()
        {
            try
            {
                return _yoneticiDAO.YoneticiBilgiOku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yönetici bilgileri getirilirken bir hata oluştu: " + ex.Message);
                return new ArrayList();
            }
        }

        public bool YoneticiGuncelle(string adSoyad, string sifre)
        {
            try
            {
                // Veri doğrulama
                if (string.IsNullOrWhiteSpace(adSoyad))
                {
                    MessageBox.Show("Ad Soyad alanı boş bırakılamaz.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(sifre))
                {
                    MessageBox.Show("Şifre alanı boş bırakılamaz.");
                    return false;
                }

                if (sifre.Length < 6)
                {
                    MessageBox.Show("Şifre en az 6 karakter olmalıdır.");
                    return false;
                }

                return _yoneticiDAO.UpdateYönetici(adSoyad, sifre);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yönetici güncellenirken bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        public bool YoneticiGirisKontrol(string tc, string sifre)
        {
            try
            {
                // TC Kimlik doğrulama
                if (string.IsNullOrWhiteSpace(tc))
                {
                    MessageBox.Show("TC Kimlik alanı boş bırakılamaz.");
                    return false;
                }

                if (tc.Length != 11 || !long.TryParse(tc, out _))
                {
                    MessageBox.Show("Geçerli bir TC Kimlik numarası giriniz.");
                    return false;
                }

                // Şifre doğrulama
                if (string.IsNullOrWhiteSpace(sifre))
                {
                    MessageBox.Show("Şifre alanı boş bırakılamaz.");
                    return false;
                }

                // Giriş işlemini gerçekleştir
                _yoneticiDAO.yoneticiGiris(tc, sifre);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş işlemi sırasında bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        public bool SifreGuncelle(string tc, string yeniSifre)
        {
            try
            {
                // TC Kimlik doğrulama
                if (string.IsNullOrWhiteSpace(tc))
                {
                    MessageBox.Show("TC Kimlik alanı boş bırakılamaz.");
                    return false;
                }

                if (tc.Length != 11 || !long.TryParse(tc, out _))
                {
                    MessageBox.Show("Geçerli bir TC Kimlik numarası giriniz.");
                    return false;
                }

                // Yeni şifre doğrulama
                if (string.IsNullOrWhiteSpace(yeniSifre))
                {
                    MessageBox.Show("Yeni şifre alanı boş bırakılamaz.");
                    return false;
                }

                if (yeniSifre.Length < 6)
                {
                    MessageBox.Show("Yeni şifre en az 6 karakter olmalıdır.");
                    return false;
                }

                // Şifre güvenlik kontrolü
                bool hasNumber = false;
                bool hasLetter = false;

                foreach (char c in yeniSifre)
                {
                    if (char.IsDigit(c)) hasNumber = true;
                    if (char.IsLetter(c)) hasLetter = true;
                }

                if (!hasNumber || !hasLetter)
                {
                    MessageBox.Show("Şifre en az bir harf ve bir rakam içermelidir.");
                    return false;
                }

                return _yoneticiDAO.SifreGuncelle(tc, yeniSifre);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Şifre güncelleme işlemi sırasında bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        // Yardımcı metod: TC Kimlik Numarası doğrulama
        private bool TcKimlikDogrula(string tcKimlik)
        {
            if (tcKimlik.Length != 11) return false;

            try
            {
                long tcNo = long.Parse(tcKimlik);
                long[] haneler = new long[11];
                for (int i = 0; i < 11; i++)
                {
                    haneler[i] = tcNo % 10;
                    tcNo = tcNo / 10;
                }

                // TC Kimlik algoritması kontrolü
                long tek = haneler[0] + haneler[2] + haneler[4] + haneler[6] + haneler[8];
                long cift = haneler[1] + haneler[3] + haneler[5] + haneler[7];

                long kontrol1 = ((tek * 7) - cift) % 10;
                if (kontrol1 != haneler[9]) return false;

                long toplam = 0;
                for (int i = 0; i < 10; i++)
                {
                    toplam += haneler[i];
                }

                if (toplam % 10 != haneler[10]) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
