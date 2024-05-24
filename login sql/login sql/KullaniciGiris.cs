using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KullaniciSifreUygulamasi
{
    public class KullaniciGiris
    {
        private string kullaniciAdi;
        private string sifre;

        public KullaniciGiris(string kullaniciAdi, string sifre)
        {
            this.kullaniciAdi = kullaniciAdi;
            this.sifre = sifre;
        }

        public bool GirisYap(string girilenKullaniciAdi, string girilenSifre)
        {
            string connectionString = "Server=LAPTOP-MNQUF8MB\\SQLEXPRESS12;initial catalog=hastane;Trusted_Connection=True;";

            bool isAuthenticated = AuthenticateUser(girilenKullaniciAdi, girilenSifre, connectionString);

            if (isAuthenticated)
                return true;
         
            return false;
        }
        public bool AuthenticateUser(string username, string password, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciAdi", username);
                        command.Parameters.AddWithValue("@Sifre", password);

                        int userCount = (int)command.ExecuteScalar();

                        return userCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
