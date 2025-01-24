using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace OtelRezervasyonSistemi.DOMAIN
{
    public class yöneticiDome
    {
        public yöneticiDome(string gAdSoyad, string gSifre,string gTc)
        {
           
            this.ADSOYAD = gAdSoyad;
            this.SİFRE = gSifre;
            this. TC=gTc;
        }
        public yöneticiDome(int gId, string gAdSoyad, string gSifre, string gTc)
        {
            this.ID = gId;
            this.ADSOYAD = gAdSoyad;
            this.SİFRE = gSifre;
            this .TC = gTc;
        }
        int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        string ad;

        public string ADSOYAD
        {
            get { return ad; }
            set { ad = value; }
        }
       
        string sifre;

        public string SİFRE
        {
            get { return sifre; }
            set {  sifre = value; }
        }
        string tc;
            public string TC
        {
            get { return tc; }
            set { tc = value; }
        }
        public override string ToString()
        {
            return this.ADSOYAD + "(" + this.SİFRE + ")";
        }

    }
}

