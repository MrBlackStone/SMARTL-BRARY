using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SMARTLİBRARY
{
    public partial class Yoneticiİstekler : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        PictureBox pic;
        Label lbl;
        public Yoneticiİstekler()
        {
            InitializeComponent();
        }

        private void Yoneticiİstekler_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pic = new PictureBox();
            komut = new SqlCommand("SELECT Uyeler.Uye_İsim, İstek_İsim as 'Kitap İsim', İstek_Yazar as Yazar,İstek_tur as Tür, Aciklama as Açıklama, ozet as Özet,İstek_Sayfa as Sayfa, istek_fotograf as Fotoğraf from Uyeİstekler INNER JOIN Uyeler ON Uyeİstekler.Fk_Uye_id = Uyeler.Uye_id", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                uyeİsim.Text = okuyucu.GetValue(0).ToString();
                isim_txt.Text = okuyucu.GetValue(1).ToString();
                yazar_txt.Text = okuyucu.GetValue(2).ToString();
                tur_txt.Text = okuyucu.GetValue(3).ToString();
                aciklama_txt.Text = okuyucu.GetValue(4).ToString();
                ozet_txt.Text = okuyucu.GetValue(5).ToString();
                sayfa_txt.Text = okuyucu.GetValue(6).ToString();
                pictureBox2.ImageLocation = Application.StartupPath + okuyucu.GetValue(7).ToString();




            }
            okuyucu.Close();

            SqlDataReader resimoku = komut.ExecuteReader();
            while (resimoku.Read())
            {
                pic = new PictureBox();
                pic.Width = 150;
                pic.Height = 200;
                //pic.BackgroundImageLayout = ImageLayout.Zoom;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.BorderStyle = BorderStyle.None;
                pic.ImageLocation = Application.StartupPath + resimoku.GetValue(7).ToString();
                lbl = new Label();
                lbl.Text = resimoku.GetValue(1).ToString();
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.BackColor = Color.FromArgb(46, 134, 222);
                lbl.Dock = DockStyle.Bottom;
                pic.Controls.Add(lbl);
                flowLayoutPanel1.Controls.Add(pic);
            }
            resimoku.Close();

            adp.SelectCommand = new SqlCommand("SELECT Uyeler.Uye_İsim as 'Üye İsim', İstek_İsim as 'Kitap İsim', İstek_Yazar as Yazar,İstek_tur as Tür, Aciklama as Açıklama,  ozet as Özet,İstek_Sayfa as Sayfa, istek_fotograf as Fotoğraf from Uyeİstekler INNER JOIN Uyeler ON Uyeİstekler.Fk_Uye_id = Uyeler.Uye_id", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            kitapDataGridView.DataSource = ds;
            kitapDataGridView.DataMember = "Uyeİstekler";



            baglantı.Close();
        }

        private void Temizle_Btn_Click(object sender, EventArgs e)
        {
            uyeİsim.Clear();
            isim_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            aciklama_txt.Clear();
            ozet_txt.Clear();
            sayfa_txt.Clear();
            pictureBox2.Image = null;
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("SELECT Uyeler.Uye_İsim as 'Üye İsim', İstek_İsim as 'Kitap İsim', İstek_Yazar as Yazar,İstek_tur as Tür, Aciklama as Açıklama,İstek_Sayfa as Sayfa, istek_fotograf as Fotoğraf from Uyeİstekler INNER JOIN Uyeler ON Uyeİstekler.Fk_Uye_id = Uyeler.Uye_id", baglantı);
            adp.Fill(ds, "OduncKitaplar");
            kitapDataGridView.DataSource = ds;
            kitapDataGridView.DataMember = "OduncKitaplar";
            baglantı.Close();
        }

        private void ara_txt_TextChanged(object sender, EventArgs e)
        {

        }
        private void GetData()
        {

            baglantı.Open();
            komut  = new SqlCommand("SELECT Uyeler.Uye_İsim as 'Üye İsim', İstek_İsim as 'Kitap İsim', İstek_Yazar as Yazar,İstek_tur as Tür, Aciklama as Açıklama, ozet as Özet,İstek_Sayfa as Sayfa, istek_fotograf as Fotoğraf from Uyeİstekler INNER JOIN Uyeler ON Uyeİstekler.Fk_Uye_id = Uyeler.Uye_id where Uyeİstekler.İstek_İsim='" + ara_txt.Text + "'", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            
            
            if (okuyucu.Read())
            {
                uyeİsim.Text = okuyucu.GetValue(0).ToString();
                isim_txt.Text = okuyucu.GetValue(1).ToString();
                yazar_txt.Text = okuyucu.GetValue(2).ToString();
                tur_txt.Text = okuyucu.GetValue(3).ToString();
                aciklama_txt.Text = okuyucu.GetValue(4).ToString();
                ozet_txt.Text = okuyucu.GetValue(5).ToString();
                sayfa_txt.Text = okuyucu.GetValue(6).ToString();
                pictureBox2.ImageLocation = Application.StartupPath + okuyucu.GetValue(7).ToString();




            }
            okuyucu.Close();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("SELECT Uyeler.Uye_İsim as 'Üye İsim', İstek_İsim as 'Kitap İsim', İstek_Yazar as Yazar,İstek_tur as Tür, Aciklama as Açıklama, ozet as Özet,İstek_Sayfa as Sayfa, istek_fotograf as Fotoğraf from Uyeİstekler INNER JOIN Uyeler ON Uyeİstekler.Fk_Uye_id = Uyeler.Uye_id where Uyeİstekler.İstek_İsim='" + ara_txt.Text + "'", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            kitapDataGridView.DataSource = ds;
            kitapDataGridView.DataMember = "Uyeİstekler";
            baglantı.Close();

        }
        private void AraBtn_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void kitapDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            uyeİsim.Text = kitapDataGridView.Rows[x].Cells[0].Value.ToString();
            isim_txt.Text = kitapDataGridView.Rows[x].Cells[1].Value.ToString();
            yazar_txt.Text = kitapDataGridView.Rows[x].Cells[2].Value.ToString();
            tur_txt.Text = kitapDataGridView.Rows[x].Cells[3].Value.ToString();
            aciklama_txt.Text = kitapDataGridView.Rows[x].Cells[4].Value.ToString();
            ozet_txt.Text = kitapDataGridView.Rows[x].Cells[5].Value.ToString();
            sayfa_txt.Text = kitapDataGridView.Rows[x].Cells[6].Value.ToString();
            pictureBox2.ImageLocation = Application.StartupPath + kitapDataGridView.Rows[x].Cells[7].Value.ToString();
        }
    }
}
