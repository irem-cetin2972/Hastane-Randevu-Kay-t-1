using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastaneRandevuSistemi2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }
        SqlConnection bag = new SqlConnection("server=DESKTOP-9LA6BAG\\SQLEXPRESS;database=kayit;Integrated Security=true");
        SqlCommand komut = new SqlCommand();
        private bool KullaniciDogrula(int tckimlik, string sifre)
        {
            try
            {
                bag.Open();
                string sorgu = "SELECT COUNT(*) FROM tblKullaniciBilgileri WHERE tcKimlik=@tcKimlik AND sifre=@sifre";
                using (SqlCommand komut = new SqlCommand(sorgu, bag))
                {
                    komut.Parameters.AddWithValue("@tckimlik", tckimlik);
                    komut.Parameters.AddWithValue("@sifre", sifre);
                    int sonuc = Convert.ToInt32(komut.ExecuteScalar());
                    return sonuc > 0;
                }
            }
            finally
            {
                bag.Close();
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tckimlik;
            if (int.TryParse(textBox1.Text, out tckimlik))
            {
                string sifre = textBox2.Text;

                if (KullaniciDogrula(tckimlik, sifre))
                {
                    MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Giriş başarılıysa ikinci forma geçiş yapılabilir
                    IkinciFormuAc();
                }
                else
                {
                    MessageBox.Show("TC Kimlik veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir TC Kimlik numarası giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IkinciFormuAc()
        {
            Form3 form3 = new Form3(); // Form1'yi göster
            form3.Show();
            this.Hide();
        }
    }
}
