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
using System.IO;


namespace SMARTLİBRARY
{   
    public partial class İsteklerim : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public İsteklerim()
        {
            InitializeComponent();
        }

        private void İsteklerim_Load(object sender, EventArgs e)
        {
            int uye_id = Convert.ToInt32(GirisPanel.uyeisimID);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            baglantı.Open();
            komut = new SqlCommand("select * from Uyeİstekler where Fk_Uye_id='"+uye_id+"'", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                kitap_isimTxt.Text = okuyucu["İstek_İsim"].ToString();
                yazar_txt.Text = okuyucu["İstek_Yazar"].ToString();
                ozet_txt.Text = okuyucu["ozet"].ToString();
                aciklama_txt.Text = okuyucu["Aciklama"].ToString();
                tur_txt.Text = okuyucu["İstek_tur"].ToString();
                sayfa_txt.Text = okuyucu["İstek_Sayfa"].ToString();
                fotograf_txt.Text = okuyucu["istek_Fotograf"].ToString();
                id_txt.Text = okuyucu["İstek_id"].ToString();
                pictureBox1.ImageLocation = Application.StartupPath + okuyucu["istek_Fotograf"].ToString();


            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select İstek_id as Numara, İstek_İsim as İsim,İstek_Yazar as Yazar, İstek_tur as Tür,ozet as Özet, İstek_Sayfa as Sayfa, Aciklama as Açıklama, istek_Fotograf as Fotoğraf from Uyeİstekler where Fk_Uye_id='"+uye_id+"'", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            istekDataGridView.DataSource = ds;
            istekDataGridView.DataMember = "Uyeİstekler";
            baglantı.Close();
        }

        private void istekEkle_btn_Click(object sender, EventArgs e)
        {
            int uye_id = Convert.ToInt32(GirisPanel.uyeisimID);

            baglantı.Open();
            komut = new SqlCommand("insert into Uyeİstekler(İstek_İsim,İstek_Yazar,İstek_tur,Aciklama,İstek_Sayfa,istek_Fotograf, Fk_Uye_id,ozet)values(@isim,@yazar,@tur,@aciklama,@sayfa,@foto,@uyeid,@ozet)", baglantı);
            komut.Parameters.AddWithValue("@isim", kitap_isimTxt.Text);
            komut.Parameters.AddWithValue("@yazar", yazar_txt.Text);
            komut.Parameters.AddWithValue("@tur", tur_txt.Text);
            komut.Parameters.AddWithValue("@sayfa", sayfa_txt.Text);
            komut.Parameters.AddWithValue("@ozet", ozet_txt.Text);
            komut.Parameters.AddWithValue("@aciklama", aciklama_txt.Text);
            komut.Parameters.AddWithValue("@foto", fotograf_txt.Text);
            komut.Parameters.AddWithValue("@uyeid", uye_id);
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("İstek Eklendi");
            adp.SelectCommand = new SqlCommand("select İstek_id as Numara, İstek_İsim as İsim,İstek_Yazar as Yazar, İstek_tur as Tür,ozet as Özet, İstek_Sayfa as Sayfa, Aciklama as Açıklama, istek_Fotograf as Fotoğraf from Uyeİstekler where Fk_Uye_id='" + uye_id + "'", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            istekDataGridView.DataSource = ds;
            istekDataGridView.DataMember = "Uyeİstekler";
            baglantı.Close();
        }

        private void Guncelle_Btn_Click(object sender, EventArgs e)
        {
            int uye_id = Convert.ToInt32(GirisPanel.uyeisimID);

            baglantı.Open();
            komut = new SqlCommand("update Uyeİstekler set İstek_İsim=@isim, İstek_Yazar=@yazar, İstek_tur=@tur, ozet=@ozet, İstek_Sayfa=@sayfa, Aciklama=@aciklama, istek_Fotograf=@foto where İstek_id=@id", baglantı);

            komut.Parameters.AddWithValue("@isim", kitap_isimTxt.Text);
            komut.Parameters.AddWithValue("@yazar", yazar_txt.Text);
            komut.Parameters.AddWithValue("@tur", tur_txt.Text);
            komut.Parameters.AddWithValue("@sayfa", sayfa_txt.Text);
            komut.Parameters.AddWithValue("@ozet", ozet_txt.Text);
            komut.Parameters.AddWithValue("@aciklama", aciklama_txt.Text);
            komut.Parameters.AddWithValue("@foto", fotograf_txt.Text);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(id_txt.Text));
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("İstek Güncellendi");
            adp.SelectCommand = new SqlCommand("select İstek_id as Numara, İstek_İsim as İsim,İstek_Yazar as Yazar, İstek_tur as Tür,ozet as Özet, İstek_Sayfa as Sayfa, Aciklama as Açıklama, istek_Fotograf as Fotoğraf from Uyeİstekler where Fk_Uye_id='" + uye_id + "'", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            istekDataGridView.DataSource = ds;
            istekDataGridView.DataMember = "Uyeİstekler";
            baglantı.Close();
        }

        private void Sil_btn_Click(object sender, EventArgs e)
        {
            int uye_id = Convert.ToInt32(GirisPanel.uyeisimID);

            baglantı.Open();
            komut = new SqlCommand("delete from Uyeİstekler where İstek_id=@id ", baglantı);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(id_txt.Text));
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("İstek Silindi");
            adp.SelectCommand = new SqlCommand("select İstek_id as Numara, İstek_İsim as İsim,İstek_Yazar as Yazar, İstek_tur as Tür,ozet as Özet, İstek_Sayfa as Sayfa, Aciklama as Açıklama, istek_Fotograf as Fotoğraf from Uyeİstekler where Fk_Uye_id='" + uye_id + "'", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            istekDataGridView.DataSource = ds;
            istekDataGridView.DataMember = "Uyeİstekler";
            baglantı.Close();

            kitap_isimTxt.Clear();
            id_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            aciklama_txt.Clear();
            ozet_txt.Clear();
            fotograf_txt.Clear();
            sayfa_txt.Clear();
            pictureBox1.Image = null;
        }
        string resimyol;
        private void resimEkle_Btn_Click(object sender, EventArgs e)
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

        private void Temizle_Btn_Click(object sender, EventArgs e)
        {
            int uye_id = Convert.ToInt32(GirisPanel.uyeisimID);

            kitap_isimTxt.Clear();
            id_txt.Clear();
            yazar_txt.Clear();
            tur_txt.Clear();
            aciklama_txt.Clear();
            ozet_txt.Clear();
            fotograf_txt.Clear();
            sayfa_txt.Clear();
            pictureBox1.Image = null;
            baglantı.Open();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select İstek_id as Numara, İstek_İsim as İsim,İstek_Yazar as Yazar, İstek_tur as Tür,ozet as Özet, İstek_Sayfa as Sayfa, Aciklama as Açıklama, istek_Fotograf as Fotoğraf from Uyeİstekler where Fk_Uye_id='" + uye_id + "'", baglantı);
            adp.Fill(ds, "Uyeİstekler");
            istekDataGridView.DataSource = ds;
            istekDataGridView.DataMember = "Uyeİstekler";
            baglantı.Close();
        }

        private void istekDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            id_txt.Text = istekDataGridView.Rows[x].Cells[0].Value.ToString();
            kitap_isimTxt.Text = istekDataGridView.Rows[x].Cells[1].Value.ToString();
            yazar_txt.Text = istekDataGridView.Rows[x].Cells[2].Value.ToString();
            tur_txt.Text = istekDataGridView.Rows[x].Cells[3].Value.ToString();
            ozet_txt.Text = istekDataGridView.Rows[x].Cells[4].Value.ToString();
            sayfa_txt.Text = istekDataGridView.Rows[x].Cells[5].Value.ToString();
            aciklama_txt.Text = istekDataGridView.Rows[x].Cells[6].Value.ToString();
            fotograf_txt.Text = istekDataGridView.Rows[x].Cells[7].Value.ToString();
            pictureBox1.ImageLocation = Application.StartupPath + istekDataGridView.Rows[x].Cells[7].Value.ToString();
        }
    }
}
