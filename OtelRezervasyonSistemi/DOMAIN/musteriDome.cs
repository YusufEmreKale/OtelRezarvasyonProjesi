using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonSistemi.DOMAIN
{
    
        public class musteriDome
        {
            public musteriDome(string mAdSoyad, string mTc ,string mDogumTarihi, string mGirisTarihi , string mCinsiyet, string mOdaNo, string mTelNo , string mCikisTarihi , string mOdaTuru)
            {

                this.ADSOYAD = mAdSoyad;
                this.TC= mTc;
                this.DOGUMTARIHI= mDogumTarihi;
                this.GIRISTARIHI=mGirisTarihi;
                this.CINSIYET= mCinsiyet;
                this.ODANO= mOdaNo;
                this.TELNO= mTelNo;
                this.CIKISTARIHI= mCikisTarihi;
                this.ODATURU= mOdaTuru;
            }
            public musteriDome(int mId, string mAdSoyad, string mTc, string mDogumTarihi, string mGirisTarihi, string mCinsiyet, string mOdaNo, string mTelNo, string mCikisTarihi, string mOdaTuru)
            {
                this.ID = mId;
                this.ADSOYAD = mAdSoyad;
                this.TC = mTc;
                this.DOGUMTARIHI = mDogumTarihi;
                this.GIRISTARIHI = mGirisTarihi;
                this.CINSIYET = mCinsiyet;
                this.ODANO = mOdaNo;
                this.TELNO = mTelNo;
                this.CIKISTARIHI = mCikisTarihi;
                this.ODATURU = mOdaTuru;
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

            string tc;

            public string TC
            {
                get { return tc; }
                set { tc = value; }
            }
            string dogumTarihi;

            public string DOGUMTARIHI
            {
                get { return dogumTarihi; }
                set { dogumTarihi = value; }
            }

            string girisTarihi;
            public string GIRISTARIHI
            {
                get { return girisTarihi; }
                set { girisTarihi = value; }
            }


            string cinsiyet;
            public string CINSIYET
            {
                get { return cinsiyet; }
                set { cinsiyet = value; }
            }

            string odaNo;
            public string ODANO
            {
                get { return odaNo; }
                set { odaNo = value; }
            }

            string telNo;
            public string TELNO
            {
                get { return telNo; }
                set { telNo = value; }
            }
            string cikisTarihi;
            public string CIKISTARIHI
            {
                get { return cikisTarihi; }
                set { cikisTarihi = value; }
            }

            string odaTuru;
            public string ODATURU
            {
                get { return odaTuru; }
                set { odaTuru = value; }
            }
            public override string ToString()
            {
                return this.ADSOYAD + "(" + this.TC+ ")";
            }

        }
    }

