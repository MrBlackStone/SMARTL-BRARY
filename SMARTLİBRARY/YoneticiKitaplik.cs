using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace SMARTLİBRARY
{
    public partial class YoneticiKitaplik : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public YoneticiKitaplik()
        {
            InitializeComponent();
        }

        

        private void YoneticiKitaplik_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            baglantı.Open();
            komut = new SqlCommand("select * from Kitaplar", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                İsim_txt.Text = okuyucu["Kitap_isim"].ToString();
                yazar_txt.Text = okuyucu["Yazar"].ToString();
                tur_txt.Text = okuyucu["Tur"].ToString();
                ozet_txt.Text = okuyucu["Ozet"].ToString();
                sayfa_txt.Text = okuyucu["Sayfa"].ToString();
                 dil_txt.Text = okuyucu["dil"].ToString();
                kitap_id_txt.Text = okuyucu["Kitap_id"].ToString();
                pictureBox1.ImageLocation = Application.StartupPath + okuyucu["Fotograf"].ToString();


            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf from Kitaplar ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";

            komut = new SqlCommand("select * from Uyeler", baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                oduncCombo.Items.Add(dr["Uye_İsim"]);
            }
            baglantı.Close();


            
        }

        private void oduncBtn_Click(object sender, EventArgs e)
        {
            
            if (oduncCombo.SelectedItem == null|| basla_txt.Text=="" || bitis_txt.Text == "" )
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            
           DialogResult dr =   MessageBox.Show(İsim_txt.Text.ToUpper() + " adlı kitabı " + oduncCombo.SelectedItem .ToString().ToUpper()+ " adli kişiye " + basla_txt.Text + " - " + bitis_txt.Text + " tarihleri arasında ödünç olarak vermektesiniz. Onaylıyor musunuz ?","Ödünç Kitap",MessageBoxButtons.YesNo);
            
            if (dr == DialogResult.Yes)
            {
                string uyeİd = "";
                baglantı.Open();
                komut = new SqlCommand("select Uye_id from Uyeler where Uye_İsim=@uyeİsim", baglantı);
                komut.Parameters.AddWithValue("@uyeİsim", oduncCombo.SelectedItem.ToString());
                komut.ExecuteNonQuery();
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    uyeİd = oku["Uye_id"].ToString();
                }
                oku.Close();

                komut = new SqlCommand("insert into OduncKitaplar(FK_kitapID,FK_uyeID,Odunc_baslangicTarihi,Odunc_bitisTarihi)values(@kitapİd,@uyeİd,@basla,@bitis)", baglantı);
                komut.Parameters.AddWithValue("@kitapİd", Convert.ToInt32(kitap_id_txt.Text));
                komut.Parameters.AddWithValue("@uyeİd", Convert.ToInt32(uyeİd));
                komut.Parameters.AddWithValue("@basla", basla_txt.Text);
                komut.Parameters.AddWithValue("@bitis", bitis_txt.Text);
                komut.ExecuteNonQuery();
                

                baglantı.Close();

            }
            else
                return;
        }

        private void PolisiyeButton_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Polisiye' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void fantastikButton_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Fantastik' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();

        }

        private void BilimKurguBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='BilimKurgu' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void EgitimBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Egitim' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void AksiyonBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Aksiyon' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void AskBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Ask' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void SanatBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Sanat' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void FelsefeBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Felsefe' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void TeknolojiBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Teknoloji' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void TarihBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Tarih' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void BilimBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Bilim' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void EdebiyatBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Tur='Edebiyat' ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void TemizleBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            İsim_txt.Clear();
            kitap_id_txt.Clear();
            yazar_txt.Clear();
            dil_txt.Clear();
            basla_txt.Clear();
            bitis_txt.Clear();
            ozet_txt.Clear();
            kitapAra_txt.Clear();
            tur_txt.Clear();
            sayfa_txt.Clear();
            pictureBox1.Image = null;
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar  ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void AraBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            kitaplikDataGridView.AutoGenerateColumns = false;
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa ,Baski as Baskı,  dil as Dil,Fotograf from Kitaplar where Kitap_isim='" + kitapAra_txt.Text + "'", baglantı);

            adp.Fill(ds, "Kitaplar");
            kitaplikDataGridView.DataSource = ds;
            kitaplikDataGridView.DataMember = "Kitaplar";

            komut = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar where Kitap_İsim='" + kitapAra_txt.Text + "'", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();


            if (okuyucu.Read())
            {
                kitap_id_txt.Text = okuyucu.GetValue(0).ToString();
                İsim_txt.Text = okuyucu.GetValue(1).ToString();
                yazar_txt.Text = okuyucu.GetValue(2).ToString();
                tur_txt.Text = okuyucu.GetValue(3).ToString();
                ozet_txt.Text = okuyucu.GetValue(4).ToString();
                sayfa_txt.Text = okuyucu.GetValue(5).ToString();
                dil_txt.Text = okuyucu.GetValue(6).ToString();
                pictureBox1.ImageLocation = Application.StartupPath + okuyucu.GetValue(7).ToString();
                

            }
            else
            {
                okuyucu.Close();
                ds.Clear();
                adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil,Fotograf from Kitaplar ", baglantı);
                adp.Fill(ds, "Kitaplar");
                kitaplikDataGridView.DataSource = ds;
                kitaplikDataGridView.DataMember = "Kitaplar";
                MessageBox.Show("Kitap Bulunamadı");
            }
            baglantı.Close();
        }

        private void kitaplikDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            kitap_id_txt.Text = kitaplikDataGridView.Rows[x].Cells[0].Value.ToString();
            İsim_txt.Text = kitaplikDataGridView.Rows[x].Cells[1].Value.ToString();
            yazar_txt.Text = kitaplikDataGridView.Rows[x].Cells[2].Value.ToString();
            tur_txt.Text = kitaplikDataGridView.Rows[x].Cells[3].Value.ToString();
            sayfa_txt.Text = kitaplikDataGridView.Rows[x].Cells[5].Value.ToString();
            dil_txt.Text = kitaplikDataGridView.Rows[x].Cells[6].Value.ToString();
            ozet_txt.Text = kitaplikDataGridView.Rows[x].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = Application.StartupPath + kitaplikDataGridView.Rows[x].Cells[8].Value.ToString();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void İsim_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
