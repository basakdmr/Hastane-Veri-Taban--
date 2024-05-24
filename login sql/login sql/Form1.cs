// Form1.cs

using System;
using System.Windows.Forms;
using KullaniciSifreUygulamasi;






namespace login_sql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string girilenKullaniciAdi = txtKullaniciAdi.Text;
            string girilenSifre = txtSifre.Text;

            // Kullanıcıyı oluştur
            KullaniciGiris kullanici = new KullaniciGiris("admin", "123456"); // Örnek olarak kullanıcı adı ve şifre

            // Kullanıcı adı ve şifreyi kontrol et
            if (kullanici.GirisYap(girilenKullaniciAdi, girilenSifre))
            {
                // Doğrulama başarılı, yeni formu aç
                AnaForm anaForm = new AnaForm();
                anaForm.Show();
                this.Hide(); // Giriş ekranını gizle
            }
            else
            {
                // Doğrulama başarısız, kullanıcıya uyarı ver
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
