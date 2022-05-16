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
    public partial class yoneticiKitaplar : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=.;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public yoneticiKitaplar()
        {
            InitializeComponent();
        }

        private void yoneticiKitaplar_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            baglantı.Open();
            komut = new SqlCommand("select * from Kitaplar", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                kitapİsim_txt.Text = okuyucu["Kitap_isim"].ToString();
                yazar_txt.Text = okuyucu["Yazar"].ToString();
                tur_txt.Text = okuyucu["Tur"].ToString();
                ozet_txt.Text = okuyucu["Ozet"].ToString();
                sayfa_sayi_txt.Text = okuyucu["Sayfa"].ToString();
                baski_txt.Text = okuyucu["Baski"].ToString();
                dil_txt.Text = okuyucu["dil"].ToString();
                kitap_id_txt.Text = okuyucu["Kitap_id"].ToString();
                fotograf_txt.Text = okuyucu["Fotograf"].ToString();
                pictureBox1.ImageLocation = Application.StartupPath + okuyucu["Fotograf"].ToString();


            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur, Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitapGridView.DataSource = ds;
            kitapGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void kitap_ekleBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("insert into Kitaplar(Kitap_isim,Yazar,Tur,Sayfa,Baski,dil,Fotograf)values(@kitap_isim,@yazar,@tur,@sayfa,@ozet,@dil,@foto)", baglantı);
            komut.Parameters.AddWithValue("@kitap_isim", kitapİsim_txt.Text);
            komut.Parameters.AddWithValue("@yazar", yazar_txt.Text);
            komut.Parameters.AddWithValue("@tur", tur_txt.Text);
            komut.Parameters.AddWithValue("@sayfa", sayfa_sayi_txt.Text);
            komut.Parameters.AddWithValue("@dil", dil_txt.Text);
            komut.Parameters.AddWithValue("@foto", fotograf_txt.Text);
            komut.Parameters.AddWithValue("@ozet", ozet_txt.Text);
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Kitap Eklendi");
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar ", baglantı);
            adp.Fill(ds, "Kitaplar");
            kitapGridView.DataSource = ds;
            kitapGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void kitapGuncelleBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("update Kitaplar set Kitap_isim='" + kitapİsim_txt.Text + "',Yazar='" + yazar_txt.Text + "',Tur='" + tur_txt.Text + "',Ozet='" + ozet_txt.Text + "',Sayfa='" + sayfa_sayi_txt.Text + "',Baski='" + baski_txt.Text + "',dil='" + dil_txt.Text + "',Fotograf='"+fotograf_txt.Text+"' where Kitap_id='" + kitap_id_txt.Text + "'", baglantı);


            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Kitap Güncellendi");
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar ", baglantı);

            adp.Fill(ds, "Kitaplar");
            kitapGridView.DataSource = ds;
            kitapGridView.DataMember = "Kitaplar";
            baglantı.Close();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void yazar_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void kitapSil_Btn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("delete from Kitaplar where Kitap_id='" + kitap_id_txt.Text + "'", baglantı);
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Kitap Silindi");
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar ", baglantı);
            adp.Fill(ds, "Calisanlar");
            kitapGridView.DataSource = ds;
            kitapGridView.DataMember = "Calisanlar";
            baglantı.Close();

            kitapİsim_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            ozet_txt.Clear();
            baski_txt.Clear();
            dil_txt.Clear();
            fotograf_txt.Clear();
            sayfa_sayi_txt.Clear();
            kitap_id_txt.Clear();
        }

        private void temizle_btn_Click(object sender, EventArgs e)
        {
            kitapİsim_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            ozet_txt.Clear();
            baski_txt.Clear();
            dil_txt.Clear();
            fotograf_txt.Clear();
            sayfa_sayi_txt.Clear();
            kitap_id_txt.Clear();
        }

        private void kitapGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            


                int x = e.RowIndex;
                kitap_id_txt.Text = kitapGridView.Rows[x].Cells[0].Value.ToString();
                kitapİsim_txt.Text = kitapGridView.Rows[x].Cells[1].Value.ToString();
                yazar_txt.Text = kitapGridView.Rows[x].Cells[2].Value.ToString();
                tur_txt.Text = kitapGridView.Rows[x].Cells[3].Value.ToString();
                baski_txt.Text = kitapGridView.Rows[x].Cells[6].Value.ToString();
                sayfa_sayi_txt.Text = kitapGridView.Rows[x].Cells[5].Value.ToString();
                dil_txt.Text = kitapGridView.Rows[x].Cells[7].Value.ToString();
                fotograf_txt.Text = kitapGridView.Rows[x].Cells[8].Value.ToString();
                ozet_txt.Text = kitapGridView.Rows[x].Cells[4].Value.ToString();
                pictureBox1.ImageLocation = Application.StartupPath + kitapGridView.Rows[x].Cells[8].Value.ToString();
            
           
        }
        string resimyol;
        private void resimEkle_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Filter = "Resim Dosyası | *.jpeg; *.bmp; *.png | Tüm Dosyalar | *.*";
            if (openfiledialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openfiledialog1.FileName;
                string kaynak = openfiledialog1.FileName;
                string hedef = Application.StartupPath + @"\images\";
                string yeniad = Guid.NewGuid() + ".jpg";
                File.Copy(kaynak, hedef + yeniad);
                resimyol = @"\images\" + yeniad;
                fotograf_txt.Text = resimyol;
            }
        }

        private void kitapAra_Btn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            kitapGridView.AutoGenerateColumns = false;
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar where Kitap_isim='"+kitap_ara_txt.Text+"'", baglantı);

            adp.Fill(ds, "Kitaplar");
            kitapGridView.DataSource = ds;
            kitapGridView.DataMember = "Kitaplar";

            komut = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar where Kitap_İsim='" + kitap_ara_txt.Text + "'", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();


            if (okuyucu.Read())
            {
                kitap_id_txt.Text = okuyucu.GetValue(0).ToString();
                kitapİsim_txt.Text = okuyucu.GetValue(1).ToString();
                yazar_txt.Text = okuyucu.GetValue(2).ToString();
                tur_txt.Text = okuyucu.GetValue(3).ToString();
                ozet_txt.Text = okuyucu.GetValue(4).ToString();
                sayfa_sayi_txt.Text = okuyucu.GetValue(5).ToString();
                baski_txt.Text = okuyucu.GetValue(6).ToString();
                dil_txt.Text = okuyucu.GetValue(7).ToString();
                fotograf_txt.Text = okuyucu.GetValue(8).ToString();

            }
            else
            {
                okuyucu.Close();
                ds.Clear();
                adp.SelectCommand = new SqlCommand("select Kitap_id as Numara, Kitap_isim as 'Kitap Adı', Yazar , Tur,Ozet as Özet, Sayfa , Baski as Baskı, dil as Dil, Fotograf as Fotoğraf from Kitaplar ", baglantı);

                adp.Fill(ds, "Kitaplar");
                kitapGridView.DataSource = ds;
                kitapGridView.DataMember = "Kitaplar";
                MessageBox.Show("Kitap Bulunamadı");

            }
            baglantı.Close();
        }
    }

}

    

