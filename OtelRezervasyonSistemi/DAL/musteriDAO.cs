using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonSistemi.dal
{
    internal class musteriDAO
    {
        public System.Data.DataTable musteriGoruntule(string musteriAdSoyad)
        {
            DataTable dt = new DataTable();
            // DataTable sütunlarını tanımlıyoruz
            dt.Columns.Add("Rezervasyon Sahibi", typeof(string));
            dt.Columns.Add("Tc No", typeof(string));
            dt.Columns.Add("Doğum Tarihi", typeof(string));
            dt.Columns.Add("Cinsiyet", typeof(string));
            dt.Columns.Add("Oda No", typeof(string));
            dt.Columns.Add("Tel No", typeof(string));
            dt.Columns.Add("Giriş Tarihi", typeof(string));
            dt.Columns.Add("Çıkış Tarihi", typeof(string));
            dt.Columns.Add("Oda Türü", typeof(string));

            try
            {
                // Bağlantıyı oluşturup açıyoruz
                using (var connection = (new baglanti()).baglantiSinifi())
                {
                    connection.Open();

                    // Eğer müşteri adı boş veya "Tümü" ise tüm müşterileri getir
                    string query = string.IsNullOrWhiteSpace(musteriAdSoyad) || musteriAdSoyad == "Tümü"
                        ? "SELECT * FROM musteri"
                        : "SELECT * FROM musteri WHERE musteri_AdSoyad = @c1";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(musteriAdSoyad) && musteriAdSoyad != "Tümü")
                        {
                            cmd.Parameters.AddWithValue("@c1", musteriAdSoyad); // Müşteri adını parametre olarak ekliyoruz
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // DataTable'a satır ekleniyor
                                dt.Rows.Add(
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString(),
                                    reader[4].ToString(),
                                    reader[5].ToString(),
                                    reader[6].ToString(),
                                    reader[7].ToString(),
                                    reader[8].ToString(),
                                    reader[9].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                MessageBox.Show("Veriler getirilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt; // Verileri DataTable olarak döndürüyoruz
        }

    }
}
