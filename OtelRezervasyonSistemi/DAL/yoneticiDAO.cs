using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OtelRezervasyonSistemi.DOMAIN;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace OtelRezervasyonSistemi.dal
{
    internal class yoneticiDAO
    {
        frmAnaEkran fr = new frmAnaEkran();
        internal System.Collections.ArrayList YoneticiBilgiOku()
        {
            ArrayList okunanYonetici = new ArrayList();
            //MySqlCommand komutum=new MySqlCommand("select * from bolum", (new dbBaglanti()).baglantiGetir())
            MySqlDataReader okunan = (new MySqlCommand("select * from yonetici", (new baglanti()).baglantiSinifi())).ExecuteReader();
            while (okunan.Read())
            {
                okunanYonetici.Add(new yöneticiDome(Convert.ToInt32(okunan[0]), okunan[1].ToString(), okunan[2].ToString(), okunan[3].ToString()));
            }
            return okunanYonetici;

        }

        // Yönetici bilgilerini güncelleme fonksiyonu
        public bool UpdateYönetici(string adsoyad, string sifre)
        {
            using (var connection = new baglanti().baglantiSinifi())
            {
                try
                {
                    string query = "UPDATE yonetici SET yoneticiAdSoyad = @p1 WHERE yoneticiSifre = @p2";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@p1", adsoyad);
                        cmd.Parameters.AddWithValue("@p2", sifre);
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();
                        connection.Close();

                        return result > 0;  // Eğer bir satır güncellenmişse true döner

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }

        }

        public void yoneticiGiris(string girisTc, string girisSifre)
        {
            try
            {
                string query = "SELECT * FROM yonetici WHERE yoneticiTc = @p1 AND yoneticiSifre = @p2";
                using (var connection = new baglanti().baglantiSinifi())
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@p1", girisTc);
                    cmd.Parameters.AddWithValue("@p2", girisSifre);

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        

                        if (reader.Read())
                        {
                            if (System.Windows.Forms.Application.OpenForms.OfType<frmAnaEkran>().Count() == 0)
                            {
                                frmAnaEkran fr = new frmAnaEkran();
                                fr.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Giriş başarısız! Kullanıcı adı veya şifre yanlış.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }


        public bool SifreGuncelle(string tc, string yeniSifre)
        {
            try
            {
                using (var connection = new baglanti().baglantiSinifi())
                {
                    connection.Open();
                    string query = "UPDATE yonetici SET yoneticiSifre = @yeniSifre WHERE yoneticiTc = @tc";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tc", tc);
                        command.Parameters.AddWithValue("@yeniSifre", yeniSifre);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Şifre başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true; // Eğer satır etkilendiyse true döner
                        }
                        else
                        {
                            MessageBox.Show("Şifre güncelleme başarısız. Belirtilen TC numarası bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false; // Eğer hiçbir satır etkilenmediyse false döner
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Veritabanı ile ilgili özel bir hata durumunda
                MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // Genel hataları yakalamak için
                MessageBox.Show($"Beklenmeyen bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}


