using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OtelRezervasyonSistemi.dal;
using OtelRezervasyonSistemi.SERVICE;

namespace OtelRezervasyonSistemi
{
    public partial class frmYoneticiGiris : Form
    {
       
       
            private readonly YoneticiService _yoneticiService;

            public frmYoneticiGiris()
            {
                InitializeComponent();
                _yoneticiService = new YoneticiService();
            }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtAdSoyad.Text.Trim();
                string password = txtSifre.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // YoneticiGirisKontrol işlemi çağrılıyor
                bool girisBasarili = _yoneticiService.YoneticiGirisKontrol(username, password);

                if (girisBasarili)
                {
                    // Ana formun zaten açık olup olmadığını kontrol et
                    bool isFormOpen = Application.OpenForms["frmAnaEkran"] != null;

                    if (!isFormOpen)
                    {
                        this.Hide();
                        frmAnaEkran anaForm = new frmAnaEkran();
                        anaForm.Show();
                    }
                    else
                    {
                       
                    }
                }
                else
                {
                    // Hatalı giriş yapıldığında
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSifre.Clear(); // Şifre alanını temizle
                    txtSifre.Focus(); // Şifre alanına odaklan
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            btnGiris.FlatStyle = FlatStyle.Flat;
            btnGiris.FlatAppearance.BorderSize = 0;
            btnGiris.BackColor = Color.Transparent;
            btnGiris.UseVisualStyleBackColor = false;
            btnGiris.FlatAppearance.MouseOverBackColor = Color.Transparent;
            

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool move ;
        int mouse_x ;
        int mouse_y ;
        private void frmYoneticiGiris_MouseDown(object sender, MouseEventArgs e)
        {
           
            move = true;
            mouse_x=e.X; mouse_y=e.Y;
        }

        private void frmYoneticiGiris_MouseUp(object sender, MouseEventArgs e)
        {
            move= false;
        }

        private void frmYoneticiGiris_MouseMove(object sender, MouseEventArgs e)
        {
          if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSifremiUnuttum fr=new frmSifremiUnuttum();
            fr.Show();
            

        }
    }
}
