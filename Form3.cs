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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hastaneRandevuSistemi2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            InitializeComboboxes();


        }
        private void InitializeComboboxes()
        {
            // Combobox'ların başlangıç içeriğini ayarla
            comboBox1.Items.AddRange(new string[] { "İstanbul", "Tekirdağ","Bursa" });
            comboBox2.Items.AddRange(new string[] { "merkez", "şarköy","beşiktaş","kadıköy","osmangazi" });
            comboBox3.Items.AddRange(new string[] { "şehir hastanesi", "devlet hastanesi","sait çiftçi hastanesi","özel aritmi hastanesi" });
           

            // Combobox'ların seçim değişikliği olaylarını bağla
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
           
        }



        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // comboBox1'in seçimi değiştiğinde comboBox2'nin içeriğini güncelle
            if (comboBox1.SelectedItem.ToString() == "İstanbul")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(new string[] { "beşiktaş", "kadıköy" });
            }
            else if (comboBox1.SelectedItem.ToString() == "Tekirdağ")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(new string[] { "merkez", "şarköy" });
            }
            else if (comboBox1.SelectedItem.ToString() == "Bursa")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(new string[] { "osmangazi" });
            }

        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // comboBox2'nin seçimi değiştiğinde comboBox3'ün içeriğini güncelle
            if (comboBox2.SelectedItem.ToString() == "beşiktaş")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "sait çiftçi hastanesi" });
            }
            else if (comboBox2.SelectedItem.ToString() == "kadıköy")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "şehir hastanesi" });
            }
            else if (comboBox2.SelectedItem.ToString() == "merkez")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "şehir hastanesi" });
            }
            else if (comboBox2.SelectedItem.ToString() == "şarköy")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "devlet hastanesi" });
            }
            else if (comboBox2.SelectedItem.ToString() == "osmangazi")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "özel aritmi hastanesi" });
            }
            
        }


        SqlConnection bag = new SqlConnection("server=DESKTOP-9LA6BAG\\SQLEXPRESS;database=kayit;Integrated Security=true");
        SqlCommand komut = new SqlCommand();


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            String sorgu = "INSERT INTO tblRandevuBilgilerii (tcKimlik,il,ilce,hastane,poliklinik,tarih,doktor,saat) VALUES (@tckimlik,@il,@ilce,@hastane,@poliklinik,@tarih,@doktor,@saat)";
            komut = new SqlCommand(sorgu, bag);

            komut.Parameters.AddWithValue("@tckimlik", textBox1.Text);
            komut.Parameters.AddWithValue("@il", comboBox1.Text);
            komut.Parameters.AddWithValue("@ilce", comboBox2.Text);
            komut.Parameters.AddWithValue("@hastane", comboBox3.Text);
            komut.Parameters.AddWithValue("@poliklinik", comboBox4.Text);
         
            komut.Parameters.AddWithValue("@doktor", comboBox5.Text);
            komut.Parameters.AddWithValue("@saat", maskedTextBox2.Text);


            DateTime secilenTarih = dateTimePicker1.Value;
            komut.Parameters.Add("@tarih", SqlDbType.DateTime).Value = secilenTarih;

            try
            {
                bag.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Saat:" + maskedTextBox2.Text + "Dr." + comboBox5.Text + "randevunuz oluşmuştur.");
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
        private void Form3_Load(object sender, EventArgs e)
        {


        }

        
    }
}

