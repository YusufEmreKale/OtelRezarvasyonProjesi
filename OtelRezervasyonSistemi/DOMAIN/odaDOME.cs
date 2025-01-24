using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonSistemi.DOMAIN
{
    internal class odaDOME
    {

        public odaDOME(int gOdaNo, string gOdaTıp , string gOdaTuru) 
        {
            this.ODANO = gOdaNo;
            this.ODATİP = gOdaTıp;
            this.ODATURU = gOdaTuru;
        
        }
        public odaDOME(int gId, int gOdaNo, string gOdaTıp, string gOdaTuru)
        {
            this.ID= gId;
            this .ODANO = gOdaNo;
            this. ODATİP = gOdaTıp;
            this. ODATURU = gOdaTuru;

        }
        int id;
             public int ID
        {
            get { return id; }
            set { id = value; }
        }
        int odaNo;
        public int ODANO
        {
            get { return odaNo; }
            set { odaNo = value; }
        }
        string odaTıp;
        public string ODATİP
        {
            get { return odaTıp; }
            set { odaTıp = value; }

        }
        string odaTuru;
        public string ODATURU
        {
            get { return odaTuru; }
            set { odaTuru = value; }

        }
        public override string ToString()
        {
            return $"ID: {ID}, Oda No: {ODANO}, Oda Tipi: {ODATİP}, Oda Türü: {ODATURU}";
        }
    }
}
