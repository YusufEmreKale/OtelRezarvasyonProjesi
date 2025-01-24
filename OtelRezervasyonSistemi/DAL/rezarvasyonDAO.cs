using MySql.Data.MySqlClient;
using OtelRezervasyonSistemi.DOMAIN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonSistemi.dal
{
    internal class rezarvasyonDAO
    {

        //internal System.Collections.ArrayList rezarvasyonGoruntule()
        //{
        //    ArrayList rezarvasyonlar = new ArrayList();
        //    //MySqlCommand komutum=new MySqlCommand("select * from bolum", (new dbBaglanti()).baglantiGetir())
        //    MySqlDataReader gelenDeger = (new MySqlCommand("select * from rezervasyonlar", (new baglanti()).baglantiSinifi())).ExecuteReader(); ;
        //    while (gelenDeger.Read())
        //    {
        //        rezarvasyonlar.Add(new rezarvasyonDome(gelenDeger[0].ToString(), gelenDeger[1].ToString(), gelenDeger[2].ToString(), gelenDeger[3].ToString()));
        //    }
        //    return rezarvasyonlar;
        //}
        public System.Data.DataTable rezarvasyonGoruntule()
        {
            DataTable dt = new DataTable();

            // DataTable sütunlarını tanımlıyoruz
           
            dt.Columns.Add("Rezervasyon Sahibi", typeof(string)); //rezervasyon sahibi
            dt.Columns.Add("TC No", typeof(string));
            dt.Columns.Add(" Tel No", typeof(string));
            dt.Columns.Add(" Cinsiyet", typeof(string));
            dt.Columns.Add("Başlangıç Tarihi", typeof(string)); // Rezervasyon Başlangıç Tarihi
            dt.Columns.Add("Bitiş Tarihi", typeof(string)); // Rezervasyon Bitiş Tarihi
            dt.Columns.Add("Oda No", typeof(string)); // Oda Numarası


            try
            {
                // Bağlantıyı oluşturup açıyoruz
                using (var connection = (new baglanti()).baglantiSinifi())
                {
                    connection.Open();

                    // SQL sorgusunu hazırlıyoruz
                    string query = "SELECT * FROM rezervasyonlar";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // DataTable'a satır ekliyoruz
                            dt.Rows.Add(
                                
                                reader[1].ToString(),
                                reader[2].ToString(),
                                 reader[3].ToString(),
                                reader[4].ToString(),
                                 reader[5].ToString(),
                                reader[6].ToString(),
                                reader[7].ToString()


                            );
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

        public bool rezervasyonEkle(string rezervasyonSahibi, string tcNo, string telNo, string cinsiyet, string baslangicTarihi, string bitisTarihi, string odaNo)
        {
            try
            {
                using (var connection = (new baglanti()).baglantiSinifi())
                {
                    connection.Open();

                    // SQL INSERT sorgusu
                    string query = "INSERT INTO rezervasyonlar (rezervasyon_AdSoyad, rezervasyon_Tc, rezervasyon_TelNo, rezervasyon_Cinsiyet, rezervasyon_BaslangıcTarihi	, rezervasyon_BitisTarihi, rezervasyon_OdaNo) " +
                                   "VALUES (@r1, @r2,@r3, @r4,@r5,@r6,@r7)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Parametreleri ekliyoruz
                        cmd.Parameters.AddWithValue("@r1", rezervasyonSahibi);
                        cmd.Parameters.AddWithValue("@r2", tcNo);
                        cmd.Parameters.AddWithValue("@r3", telNo);
                        cmd.Parameters.AddWithValue("@r4", cinsiyet);
                        cmd.Parameters.AddWithValue("@r5", baslangicTarihi);
                        cmd.Parameters.AddWithValue("@r6", bitisTarihi);
                        cmd.Parameters.AddWithValue("@r7", odaNo);

                        // Veriyi ekliyoruz
                        int result = cmd.ExecuteNonQuery();

                        // Eğer veri başarılı bir şekilde eklenmişse, result 1 döner
                        return result > 0;

                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool rezervasyonGuncelle(string rezervasyonSahibi, string odaNo, string telNo, string cinsiyet, string baslangicTarihi, string bitisTarihi, string tcNo)
        {
            try
            {
                using (var connection = (new baglanti()).baglantiSinifi())

                {
                    connection.Open();
                    string query = "UPDATE rezervasyonlar SET " +
               "rezervasyon_AdSoyad = @a1, " +
               "rezervasyon_OdaNo = @a2, " +
               "rezervasyon_TelNo = @a3, " +
               "rezervasyon_Cinsiyet = @a4, " +
               "rezervasyon_BaslangıcTarihi = @a5, " +
               "rezervasyon_BitisTarihi = @a6 " +
               "WHERE rezervasyon_Tc = @a7";



                    ;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@a1", rezervasyonSahibi);
                        command.Parameters.AddWithValue("@a2", odaNo);
                        command.Parameters.AddWithValue("@a3", telNo);
                        command.Parameters.AddWithValue("@a4", cinsiyet);
                        command.Parameters.AddWithValue("@a5", baslangicTarihi);
                        command.Parameters.AddWithValue("@a6", bitisTarihi);
                        command.Parameters.AddWithValue("@a7", tcNo);




                        int rowsAffected = command.ExecuteNonQuery(); // Sorguyu çalıştır

                        return rowsAffected > 0; // Eğer en az bir satır güncellenmişse true döndür
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                MessageBox.Show("Stack Trace: " + ex.StackTrace);
                return false;
            }

        }

        public bool rezervasyonSil(string rezervasyonSahibi, string odaNo, string telNo, string cinsiyet, string baslangicTarihi, string bitisTarihi, string tcNo)
        {
            try
            {
                using (var connection = (new baglanti()).baglantiSinifi())
                {
                    string query = "DELETE FROM rezervasyonlar " +
    "WHERE rezervasyon_AdSoyad = @b1 AND rezervasyon_OdaNo = @b2 AND rezervasyon_TelNo = @b3 " +
    "AND rezervasyon_Cinsiyet = @b4 AND rezervasyon_BaslangıcTarihi = @b5 " +
    "AND rezervasyon_BitisTarihi = @b6 AND rezervasyon_Tc = @b7";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        cmd.Parameters.AddWithValue("@b1", rezervasyonSahibi);
                        cmd.Parameters.AddWithValue("@b2", odaNo);
                        cmd.Parameters.AddWithValue("@b3", telNo);
                        cmd.Parameters.AddWithValue("@b4", cinsiyet);
                        cmd.Parameters.AddWithValue("@b5", baslangicTarihi);
                        cmd.Parameters.AddWithValue("@b6", bitisTarihi);
                        cmd.Parameters.AddWithValue("@b7", tcNo);

                        // Bağlantıyı aç
                        connection.Open();

                        // Sorguyu çalıştır ve etkilenen satır sayısını al
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.");
                            return true; // Silme başarılı
                        }
                        else
                        {
                            MessageBox.Show("Silinecek kayıt bulunamadı.");
                            return false; // Kayıt bulunamadı
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // SQL ile ilgili hataları yakala
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
                return false; // Hata durumunda false döndür
            }
            catch (Exception ex)
            {
                // Diğer hataları yakala
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
                return false; // Genel hata durumunda false döndür
            }
        }

    }
}