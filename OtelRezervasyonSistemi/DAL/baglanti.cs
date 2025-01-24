using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtelRezervasyonSistemi.dal
{
    internal class baglanti
    {

        public MySqlConnection baglantiSinifi()
        {
            MySqlConnection bgl = new MySqlConnection("Server=172.21.54.253;Database=25_132330010;User=25_132330010;Password=!nif.ogr10KA");
           
            
            return bgl;

        }
    }//try
     //{
     //    bgl.Open();
     //    MessageBox.Show("Bağlantı başarılı!");
     //}
     //catch (Exception ex)
     //{
     //    MessageBox.Show(ex.ToString());
     //    throw;
     //}
}
