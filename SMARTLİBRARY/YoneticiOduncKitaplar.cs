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
    public partial class Kitaplar : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        PictureBox pic;
        Label lbl;
        public Kitaplar()
        {
            InitializeComponent();
        }

        private void GetData(string tablo1)
        {

            baglantı.Open();
            komut = new SqlCommand("SELECT  Uyeler.Uye_isim as İsim, Uyeler.Uye_Adres as Adres, Uyeler.Uye_Telefon as Telefon, Uyeler.Uye_Fotograf as Fotoğraf, Uyeler.TcKimlik as Kimlik, Kitaplar.Kitap_İsim as 'Kitap İsim', Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotograf FROM OduncKitaplar INNER JOIN Uyeler ON OduncKitaplar.FK_UyeID= Uyeler.Uye_id INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where Kitaplar.Kitap_İsim ='"+tablo1+"'", baglantı);
            SqlDataReader oku = komut.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();
            while(oku.Read())
            {
                pic = new PictureBox();
                pic.Width = 150;
                pic.Height = 200;
                //pic.BackgroundImageLayout = ImageLayout.Zoom;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.BorderStyle = BorderStyle.None;
                pic.ImageLocation = Application.StartupPath + oku.GetValue(9).ToString();
                lbl = new Label();
                lbl.Text = oku.GetValue(5).ToString();
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.BackColor = Color.FromArgb(46, 134, 222);
                lbl.Dock = DockStyle.Bottom;
                pic.Controls.Add(lbl);
                flowLayoutPanel1.Controls.Add(pic);

            }
            oku.Close();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("SELECT  Uyeler.Uye_isim as İsim, Uyeler.Uye_Adres as Adres, Uyeler.Uye_Telefon as Telefon, Uyeler.Uye_Fotograf as Fotoğraf, Uyeler.TcKimlik as Kimlik, Kitaplar.Kitap_İsim as 'Kitap İsim', Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotograf FROM OduncKitaplar INNER JOIN Uyeler ON OduncKitaplar.FK_UyeID= Uyeler.Uye_id INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where Kitaplar.Kitap_İsim ='" + tablo1 + "'", baglantı);

            adp.Fill(ds, "OduncKitaplar");
            oduncDataGridView.DataSource = ds;
            oduncDataGridView.DataMember = "OduncKitaplar";
            baglantı.Close();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Kitaplar_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            UyeResimBox.SizeMode = PictureBoxSizeMode.Zoom;
            pic = new PictureBox();
            komut = new SqlCommand("SELECT  Uyeler.Uye_isim as İsim, Uyeler.Uye_Adres as Adres, Uyeler.Uye_Telefon as Telefon, Uyeler.Uye_Fotograf as Fotoğraf, Uyeler.TcKimlik as Kimlik, Kitaplar.Kitap_İsim as 'Kitap İsim', Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotograf FROM OduncKitaplar INNER JOIN Uyeler ON OduncKitaplar.FK_UyeID= Uyeler.Uye_id INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                adsoyad_txt.Text = okuyucu.GetValue(0).ToString();
                adres_txt.Text = okuyucu.GetValue(1).ToString();
                telefon_txt.Text = okuyucu.GetValue(2).ToString();
                kimlik_txt.Text = okuyucu.GetValue(4).ToString();
                kitapİsim_txt.Text = okuyucu.GetValue(5).ToString();
                yazar_txt.Text = okuyucu.GetValue(6).ToString();
                tur_txt.Text = okuyucu.GetValue(7).ToString();
                ozet_txt.Text = okuyucu.GetValue(8).ToString();
                UyeResimBox.ImageLocation = Application.StartupPath + okuyucu.GetValue(3).ToString();
                
                
                pic = new PictureBox();
                pic.Width = 150;
                pic.Height = 200;
                //pic.BackgroundImageLayout = ImageLayout.Zoom;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.BorderStyle = BorderStyle.None;
                pic.ImageLocation = Application.StartupPath + okuyucu.GetValue(9).ToString();
                lbl = new Label();
                lbl.Text = okuyucu.GetValue(5).ToString();
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.BackColor = Color.FromArgb(46, 134, 222);
                lbl.Dock = DockStyle.Bottom;
                pic.Controls.Add(lbl);
                flowLayoutPanel1.Controls.Add(pic);

            }
            okuyucu.Close();
            
            adp.SelectCommand = new SqlCommand("SELECT  Uyeler.Uye_isim as İsim, Uyeler.Uye_Adres as Adres, Uyeler.Uye_Telefon as Telefon, Uyeler.Uye_Fotograf as Fotoğraf, Uyeler.TcKimlik as Kimlik, Kitaplar.Kitap_İsim as 'Kitap İsim', Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotograf FROM OduncKitaplar INNER JOIN Uyeler ON OduncKitaplar.FK_UyeID= Uyeler.Uye_id INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id", baglantı);
            adp.Fill(ds, "OduncKitaplar");
            oduncDataGridView.DataSource = ds;
            oduncDataGridView.DataMember = "OduncKitaplar";

            

            baglantı.Close();


        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void araBtn_Click(object sender, EventArgs e)
        {
            GetData(ara_txt.Text.ToString());
        }

        private void temizle_btn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            adsoyad_txt.Clear();
            adres_txt.Clear();
            telefon_txt.Clear();
            kimlik_txt.Clear();
            kitapİsim_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            ozet_txt.Clear();
            ara_txt.Clear();
            adp.SelectCommand = new SqlCommand("SELECT  Uyeler.Uye_isim as İsim, Uyeler.Uye_Adres as Adres, Uyeler.Uye_Telefon as Telefon, Uyeler.Uye_Fotograf as Fotoğraf, Uyeler.TcKimlik as Kimlik, Kitaplar.Kitap_İsim as 'Kitap İsim', Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotograf FROM OduncKitaplar INNER JOIN Uyeler ON OduncKitaplar.FK_UyeID= Uyeler.Uye_id INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id", baglantı);
            adp.Fill(ds, "OduncKitaplar");
            oduncDataGridView.DataSource = ds;
            oduncDataGridView.DataMember = "OduncKitaplar";
            flowLayoutPanel1.Controls.Clear();
            UyeResimBox.Image = null;
            baglantı.Close();
        }

        private void oduncDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;

            kitapİsim_txt.Text = oduncDataGridView.Rows[x].Cells[5].Value.ToString();
            yazar_txt.Text = oduncDataGridView.Rows[x].Cells[6].Value.ToString();
            tur_txt.Text = oduncDataGridView.Rows[x].Cells[7].Value.ToString();
            adsoyad_txt.Text = oduncDataGridView.Rows[x].Cells[0].Value.ToString();
            adres_txt.Text = oduncDataGridView.Rows[x].Cells[1].Value.ToString();
            kimlik_txt.Text = oduncDataGridView.Rows[x].Cells[4].Value.ToString();
            ozet_txt.Text = oduncDataGridView.Rows[x].Cells[8].Value.ToString();
            telefon_txt.Text = oduncDataGridView.Rows[x].Cells[2].Value.ToString();

            UyeResimBox.ImageLocation = Application.StartupPath + oduncDataGridView.Rows[x].Cells[3].Value.ToString();

        }
    }
}
