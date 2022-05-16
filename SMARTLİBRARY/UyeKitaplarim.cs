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
    public partial class UyeKitaplarim : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=.;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        PictureBox pic;
        Label lbl;
        int uyeid = Convert.ToInt32(GirisPanel.uyeisimID);
        public UyeKitaplarim()
        {
            InitializeComponent();
        }
        private void GetData(int tablo1)
        {

            
            komut = new SqlCommand("SELECT  Uyeler.Uye_isim as İsim, Uyeler.Uye_Adres as Adres, Uyeler.Uye_Telefon as Telefon, Uyeler.Uye_Fotograf as Fotoğraf, Uyeler.TcKimlik as Kimlik, Kitaplar.Kitap_İsim as İsim, Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotoğraf FROM OduncKitaplar INNER JOIN Uyeler ON OduncKitaplar.FK_UyeID= Uyeler.Uye_id INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where OduncKitaplar.FK_uyeID ='" + tablo1 + "'", baglantı);
            SqlDataReader oku = komut.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();
            while (oku.Read())
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
            
            

        }

        private void UyeKitaplarim_Load(object sender, EventArgs e)
        {

            baglantı.Open();
            pic = new PictureBox();
            komut = new SqlCommand("SELECT  Kitaplar.Kitap_İsim as İsim, Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotoğraf,OduncKitaplar.Odunc_baslangicTarihi as Başlangıç, OduncKitaplar.Odunc_bitisTarihi as Bitiş, Kitaplar.Baski as Baskı FROM OduncKitaplar  INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where OduncKitaplar.FK_uyeID='"+uyeid+"'", baglantı);
                SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                isim_txt.Text = okuyucu.GetValue(0).ToString();
                yazar_txt.Text = okuyucu.GetValue(1).ToString();
                tur_txt.Text = okuyucu.GetValue(2).ToString();
                ozet_txt.Text = okuyucu.GetValue(3).ToString();
                basla_txt.Text = okuyucu.GetValue(5).ToString();
                bitis_txt.Text = okuyucu.GetValue(6).ToString();
                baski_txt.Text = okuyucu.GetValue(7).ToString();
                pictureBox2.ImageLocation = Application.StartupPath + okuyucu.GetValue(4).ToString();


               

            }
            okuyucu.Close();
            GetData(uyeid);
            adp.SelectCommand = new SqlCommand("SELECT  Kitaplar.Kitap_İsim as İsim, Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotoğraf,OduncKitaplar.Odunc_baslangicTarihi as Başlangıç, OduncKitaplar.Odunc_bitisTarihi as Bitiş, Kitaplar.Baski as Baskı FROM OduncKitaplar  INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where OduncKitaplar.FK_uyeID='" + uyeid + "'", baglantı);
            adp.Fill(ds, "OduncKitaplar");
            kitapDataGridView.DataSource = ds;
            kitapDataGridView.DataMember = "OduncKitaplar";



            baglantı.Close();
        }

        private void kitapDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int x = e.RowIndex;
            isim_txt.Text = kitapDataGridView.Rows[x].Cells[0].Value.ToString();
            yazar_txt.Text = kitapDataGridView.Rows[x].Cells[1].Value.ToString();
            tur_txt.Text = kitapDataGridView.Rows[x].Cells[2].Value.ToString();
            ozet_txt.Text = kitapDataGridView.Rows[x].Cells[3].Value.ToString();
            basla_txt.Text = kitapDataGridView.Rows[x].Cells[5].Value.ToString();
            bitis_txt.Text = kitapDataGridView.Rows[x].Cells[6].Value.ToString();
            baski_txt.Text = kitapDataGridView.Rows[x].Cells[7].Value.ToString();
            pictureBox2.ImageLocation = Application.StartupPath + kitapDataGridView.Rows[x].Cells[4].Value.ToString();
        }

        private void AraBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            
            komut = new SqlCommand("SELECT  Kitaplar.Kitap_İsim as İsim, Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotoğraf,OduncKitaplar.Odunc_baslangicTarihi as Başlangıç, OduncKitaplar.Odunc_bitisTarihi as Bitiş, Kitaplar.Baski as Baskı FROM OduncKitaplar  INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where OduncKitaplar.FK_uyeID='" + uyeid + "' and Kitaplar.Kitap_İsim='" + ara_txt.Text + "'", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                isim_txt.Text = okuyucu.GetValue(0).ToString();
                yazar_txt.Text = okuyucu.GetValue(1).ToString();
                tur_txt.Text = okuyucu.GetValue(2).ToString();
                ozet_txt.Text = okuyucu.GetValue(3).ToString();
                basla_txt.Text = okuyucu.GetValue(5).ToString();
                bitis_txt.Text = okuyucu.GetValue(6).ToString();
                baski_txt.Text = okuyucu.GetValue(7).ToString();
                pictureBox2.ImageLocation = Application.StartupPath + okuyucu.GetValue(4).ToString();




            }
            okuyucu.Close();
            ds.Clear();

            adp.SelectCommand = new SqlCommand("SELECT  Kitaplar.Kitap_İsim as İsim, Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotoğraf,OduncKitaplar.Odunc_baslangicTarihi as Başlangıç, OduncKitaplar.Odunc_bitisTarihi as Bitiş, Kitaplar.Baski as Baskı FROM OduncKitaplar  INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where OduncKitaplar.FK_uyeID='" + uyeid + "' and Kitaplar.Kitap_İsim='"+ara_txt.Text+"'", baglantı);
            adp.Fill(ds, "OduncKitaplar");
            kitapDataGridView.DataSource = ds;
            kitapDataGridView.DataMember = "OduncKitaplar";
            baglantı.Close();
        }

        private void Temizle_Btn_Click(object sender, EventArgs e)
        {

            isim_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            ozet_txt.Clear();
            baski_txt.Clear();
            basla_txt.Clear();
            bitis_txt.Clear();
            pictureBox2.Image = null;
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("SELECT  Kitaplar.Kitap_İsim as İsim, Kitaplar.Yazar ,Kitaplar.Tur, Kitaplar.Ozet as Özet, Kitaplar.Fotograf as Fotoğraf,OduncKitaplar.Odunc_baslangicTarihi as Başlangıç, OduncKitaplar.Odunc_bitisTarihi as Bitiş, Kitaplar.Baski as Baskı FROM OduncKitaplar  INNER JOIN Kitaplar ON OduncKitaplar.FK_kitapID = Kitaplar.Kitap_id where OduncKitaplar.FK_uyeID='" + uyeid + "'", baglantı);
            adp.Fill(ds, "OduncKitaplar");
            kitapDataGridView.DataSource = ds;
            kitapDataGridView.DataMember = "OduncKitaplar";
            baglantı.Close();
        }
    }
}
