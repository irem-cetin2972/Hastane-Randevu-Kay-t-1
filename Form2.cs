using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastaneRandevuSistemi2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection("server=DESKTOP-9LA6BAG\\SQLEXPRESS;database=kayit;Integrated Security=true");
        SqlCommand komut = new SqlCommand();
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Form1'yi göster
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sorgu = "INSERT INTO tblKullaniciBilgileri (tcKimlik,sifre,ad,soyadi,cinsiyet,telefon,dogumTarihi) VALUES (@tckimlik,@sifre,@ad,@soyadi,@cinsiyet,@telefon,@dogumtarihi)";
            komut = new SqlCommand(sorgu, bag);

            komut.Parameters.AddWithValue("@tckimlik", textBox1.Text);
            komut.Parameters.AddWithValue("@sifre",textBox2.Text);
            komut.Parameters.AddWithValue("@ad", textBox3.Text);
            komut.Parameters.AddWithValue("@soyadi", textBox4.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@telefon", maskedTextBox1.Text);

            DateTime secilenTarihSaat = dateTimePicker1.Value;
            komut.Parameters.Add("@dogumtarihi", SqlDbType.DateTime).Value = secilenTarihSaat;

            try
            {
                bag.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Kaydınız Başarıyla Yapıldı Şifreniz:" + textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bag.Close();
            }
        }
    }
}
