using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonSistemi.DOMAIN
{
    //internal class rezarvasyonDome
    //{
    //    //public int id { get; set; }        
    //    //public string adSoyad { get; set; }    
    //    //public string sifre { get; set; }
    //    //public rezarvasyonDome()
    //    //{

    //    //}


    //}
    public class rezarvasyonDome
    {
        public rezarvasyonDome(string rBasTarihi, string rBitTarihi, string rOdaNo, string rKisiSayisi)
        {

            this.BASTARIHI = rBasTarihi;
            this.BİTTARİHİ = rBitTarihi;
            this.ODANO=rOdaNo;
            this.KISISAYISI=rKisiSayisi;
        }
        public rezarvasyonDome(int rId,string rBasTarihi, string rBitTarihi, string rOdaNo, string rKisiSayisi)
        {
            this.ID=rId;
            this.BASTARIHI = rBasTarihi;
            this.BİTTARİHİ = rBitTarihi;
            this.ODANO = rOdaNo;
            this.KISISAYISI = rKisiSayisi;
        }
        int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        string basTarihi;

        public string BASTARIHI
        {
            get { return basTarihi; }
            set { basTarihi = value; }
        }

        string bitTarihi;

        public string BİTTARİHİ
        {
            get { return bitTarihi; }
            set { bitTarihi = value; }
        }
        string odaNo;
        public string ODANO
        {
            get { return odaNo; }
            set { odaNo = value; }
        }
        string kisiSayisi;
        public string KISISAYISI
        {
            get { return kisiSayisi; }
            set { kisiSayisi = value; }
        }

        public override string ToString()
        {
            return $"Rezervasyon ID: {ID}, Oda No: {ODANO}, Başlangıç Tarihi: {BASTARIHI}, Bitiş Tarihi: {BİTTARİHİ}, Kişi Sayısı: {KISISAYISI}";
        }


    }
}
