using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtelRezervasyonSistemi.dal
{
    internal class odaDAO
    {
        public System.Data.DataTable odaGoruntule(string odaNO)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Oda Numarası", typeof(int));
            dt.Columns.Add("Oda Tipi", typeof(string));
            dt.Columns.Add("Oda Türü", typeof(string));

            try
            {
                // Bağlantıyı oluşturup açıyoruz
                using (var connection = (new baglanti()).baglantiSinifi())
                {
                    connection.Open();

                    // Eğer odaNO boş veya null ise tüm odaları sorgula
                    string query = string.IsNullOrWhiteSpace(odaNO)
                        ? "SELECT * FROM odalar"
                        : "SELECT * FROM odalar WHERE oda_Numara = @p1";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Eğer belirli bir oda seçilmişse parametre ekle
                        if (!string.IsNullOrWhiteSpace(odaNO))
                        {
                            cmd.Parameters.AddWithValue("@p1", odaNO);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString()
                                );
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


        public void odaDurumGuncelle(string odaNO, string yeniDurum)
        {
            try
            {
                // Bağlantıyı oluşturup açıyoruz
                using (var connection = (new baglanti()).baglantiSinifi())
                {
                    connection.Open();

                    // SQL sorgusunu oluşturuyoruz
                    string query = "UPDATE odalar SET oda_Durum = @durum WHERE oda_Numara = @odaNo";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Parametreleri ekliyoruz
                        cmd.Parameters.AddWithValue("@durum", yeniDurum);
                        cmd.Parameters.AddWithValue("@odaNo", odaNO);

                        // Komutu çalıştırıyoruz
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Başarı durumunu kontrol edelim
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Oda durumu başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Oda durumu güncellenemedi. Oda numarasını kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                MessageBox.Show("Oda durumu güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

   

}
