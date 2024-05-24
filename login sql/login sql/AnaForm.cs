using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace login_sql
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=LAPTOP-MNQUF8MB\\SQLEXPRESS12;initial catalog=hastane;Trusted_Connection=True;");
        SqlCommand komut;
        SqlDataAdapter da;
        DataSet ds;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();
            komut = new SqlCommand(
                     "SELECT [HastaID], [Ad], [Soyad], [DogumTarihi], [Cinsiyet], [TelefonNumarasi], [Adres] " +
                     "FROM [hastane].[dbo].[Hastalar]", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds, "Hastalar");
            dataGridView_goster.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();

            string query = @"
                        SELECT RaporIcerigi, COUNT(*) AS RaporSayisi
                        FROM tibbiraporlar
                        GROUP BY RaporIcerigi
                        HAVING COUNT(*) > 1;
                    ";
            using (SqlCommand komut = new SqlCommand(query, baglanti))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(komut))
                {
                    using (SqlCommandBuilder cmdb = new SqlCommandBuilder(da))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "TibbiRaporlar");  // Uygun tablo ismini kullanın
                        dataGridView_goster.DataSource = ds.Tables["TibbiRaporlar"];  // Aynı tablo ismini kullanın
                    }
                }
            }

            baglanti.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();

            string query = "SELECT * FROM doktorlar WHERE UzmanlikAlani = @UzmanlikAlani";
            using (SqlCommand komut = new SqlCommand(query, baglanti))
            {
                komut.Parameters.AddWithValue("@UzmanlikAlani", "kardiyoloji");

                using (SqlDataAdapter da = new SqlDataAdapter(komut))
                {
                    using (SqlCommandBuilder cmdb = new SqlCommandBuilder(da))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Doktorlar");
                        dataGridView_goster.DataSource = ds.Tables["Doktorlar"];
                    }
                }
            }

            baglanti.Close();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();

            string query = @"
                        SELECT rv1.RandevuID, rv1.HastaID, h.Ad, h.Soyad, rv1.RandevuTarihi, rv1.RandevuSaati
                        FROM Randevular rv1
                        LEFT JOIN Hastalar h ON rv1.HastaID = h.HastaID
                        WHERE EXISTS (
                            SELECT 1
                            FROM Randevular AS rv2
                            WHERE rv1.RandevuTarihi = rv2.RandevuTarihi
                              AND rv1.RandevuSaati = rv2.RandevuSaati
                            GROUP BY rv2.RandevuTarihi, rv2.RandevuSaati
                            HAVING COUNT(*) > 1
                        )
                    ";

            using (SqlCommand komut = new SqlCommand(query, baglanti))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(komut))
                {
                    using (SqlCommandBuilder cmdb = new SqlCommandBuilder(da))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Randevular");  // Tablo ismini uygun şekilde değiştirin
                        dataGridView_goster.DataSource = ds.Tables["Randevular"];  // Aynı tablo ismini kullanın
                    }
                }
            }

            baglanti.Close();
        }

    }
}





