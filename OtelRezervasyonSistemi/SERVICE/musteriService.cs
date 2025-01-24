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
    internal class musteriService
    {
        private readonly musteriDAO _musteriDAO;

        public musteriService()
        {
            _musteriDAO = new musteriDAO();
        }

        public DataTable MusteriBilgileriniGetir(string musteriAdSoyad = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(musteriAdSoyad) && musteriAdSoyad != "Tümü")
                {
                    if (musteriAdSoyad.Length < 2)
                    {
                        MessageBox.Show("Müşteri adı en az 2 karakter olmalıdır.");
                        return new DataTable();
                    }

                    if (!musteriAdSoyad.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    {
                        MessageBox.Show("Müşteri adı sadece harf içermelidir.");
                        return new DataTable();
                    }
                }

                DataTable musteriler = _musteriDAO.musteriGoruntule(musteriAdSoyad);

                if (musteriler.Rows.Count == 0)
                {
                    string mesaj = string.IsNullOrWhiteSpace(musteriAdSoyad) || musteriAdSoyad == "Tümü"
                        ? "Sistemde kayıtlı müşteri bulunmamaktadır."
                        : "Belirtilen isimde müşteri bulunamadı.";
                    MessageBox.Show(mesaj);
                }

                return musteriler;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri bilgileri getirilirken bir hata oluştu: " + ex.Message);
                return new DataTable();
            }
        }

        public bool TcKimlikKontrol(string tcKimlik)
        {
            return !string.IsNullOrWhiteSpace(tcKimlik)
                && tcKimlik.Length == 11
                && tcKimlik.All(char.IsDigit);
        }

        public bool TelefonNumarasiKontrol(string telefon)
        {
            return !string.IsNullOrWhiteSpace(telefon)
                && telefon.Length == 10
                && telefon.All(char.IsDigit);
        }

        public bool TarihKontrol(DateTime girisTarihi, DateTime cikisTarihi)
        {
            if (girisTarihi > cikisTarihi)
            {
                MessageBox.Show("Giriş tarihi çıkış tarihinden sonra olamaz.");
                return false;
            }

            if (girisTarihi.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Giriş tarihi geçmiş bir tarih olamaz.");
                return false;
            }

            return true;
        }
    }
}
