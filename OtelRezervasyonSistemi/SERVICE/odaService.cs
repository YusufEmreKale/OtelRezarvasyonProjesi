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
    internal class odaService
    {
        public class OdaService
        {
            private readonly odaDAO _odaDAO;

            public OdaService()
            {
                _odaDAO = new odaDAO();
            }

            public DataTable OdaBilgileriniGetir(string odaNo = "")
            {
                try
                {
                    // Oda numarası girilmişse format kontrolü
                    if (!string.IsNullOrWhiteSpace(odaNo))
                    {
                        // Oda numarası sayısal değer kontrolü
                        if (!int.TryParse(odaNo, out _))
                        {
                            MessageBox.Show("Oda numarası sayısal bir değer olmalıdır.");
                            return new DataTable();
                        }

                        // Oda numarası uzunluk kontrolü (örneğin: 101, 102 gibi)
                        if (odaNo.Length < 3 || odaNo.Length > 4)
                        {
                            MessageBox.Show("Geçersiz oda numarası formatı.");
                            return new DataTable();
                        }
                    }

                    // Oda bilgilerini getir
                    DataTable odalar = _odaDAO.odaGoruntule(odaNo);

                    // Veri kontrolü
                    if (odalar.Rows.Count == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(odaNo))
                        {
                            MessageBox.Show("Belirtilen oda numarasına ait kayıt bulunamadı.");
                        }
                        else
                        {
                            MessageBox.Show("Sistemde kayıtlı oda bulunmamaktadır.");
                        }
                    }

                    return odalar;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda bilgileri getirilirken bir hata oluştu: " + ex.Message);
                    return new DataTable();
                }
            }

            public bool OdaDurumuGuncelle(string odaNo, string yeniDurum)
            {
                try
                {
                    // Parametre kontrolleri
                    if (string.IsNullOrWhiteSpace(odaNo))
                    {
                        MessageBox.Show("Oda numarası boş bırakılamaz.");
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(yeniDurum))
                    {
                        MessageBox.Show("Yeni durum seçilmelidir.");
                        return false;
                    }

                    // Oda numarası sayısal değer kontrolü
                    if (!int.TryParse(odaNo, out _))
                    {
                        MessageBox.Show("Oda numarası sayısal bir değer olmalıdır.");
                        return false;
                    }

                    // Oda numarası format kontrolü
                    if (odaNo.Length < 3 || odaNo.Length > 4)
                    {
                        MessageBox.Show("Geçersiz oda numarası formatı.");
                        return false;
                    }

                    // Durum değeri kontrolü
                    string[] gecerliDurumlar = { "Boş", "Dolu", "Temizlik", "Bakım" };
                    if (!Array.Exists(gecerliDurumlar, x => x.Equals(yeniDurum, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Geçersiz oda durumu.");
                        return false;
                    }

                    // Odanın mevcut olup olmadığını kontrol et
                    var odaBilgisi = OdaBilgileriniGetir(odaNo);
                    if (odaBilgisi.Rows.Count == 0)
                    {
                        MessageBox.Show("Belirtilen oda numarası sistemde bulunamadı.");
                        return false;
                    }

                    // Oda durumunu güncelle
                    _odaDAO.odaDurumGuncelle(odaNo, yeniDurum);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda durumu güncellenirken bir hata oluştu: " + ex.Message);
                    return false;
                }
            }

            // Yardımcı metod: Oda tipine göre maksimum kapasite kontrolü
            public int OdaKapasitesiniGetir(string odaTipi)
            {
                switch (odaTipi.ToLower())
                {
                    case "tek kişilik":
                        return 1;
                    case "çift kişilik":
                        return 2;
                    case "suit":
                        return 4;
                    case "aile":
                        return 6;
                    default:
                        return 0;
                }
            }

            // Yardımcı metod: Oda müsaitlik kontrolü
            public bool OdaMusaitMi(string odaNo)
            {
                try
                {
                    var odaBilgisi = OdaBilgileriniGetir(odaNo);
                    if (odaBilgisi.Rows.Count > 0)
                    {
                        // İlgili kolonun indeksini doğru şekilde ayarlayın
                        string odaDurumu = odaBilgisi.Rows[0]["Oda Durumu"].ToString();
                        return odaDurumu.Equals("Boş", StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
