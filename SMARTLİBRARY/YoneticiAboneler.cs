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
    public partial class YoneticiAboneler : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public YoneticiAboneler()
        {
            InitializeComponent();
        }

        private void uyeEkle_btn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("insert into Uyeler(Uye_İsim,Uye_Adres,Uye_Telefon,UyelikTarihi,UyeBitisTarihi,TcKimlik, Uye_Yas,Uye_Fotograf)values(@isim,@adres,@telefon,@tarih,@bitistarih,@kimlik,@yas,@foto)", baglantı);
            komut.Parameters.AddWithValue("@isim", uye_adsoyad_txt.Text);
            komut.Parameters.AddWithValue("@adres", uyeAdres_txt.Text);
            komut.Parameters.AddWithValue("@telefon", uyeTelefon_txt.Text);
            komut.Parameters.AddWithValue("@tarih", uyeTarih_txt.Text);
            komut.Parameters.AddWithValue("@bitistarih", uyeBitis_txt.Text);
            komut.Parameters.AddWithValue("@kimlik", uyeKimlik_txt.Text);
            komut.Parameters.AddWithValue("@foto", uyeFotograf_txt.Text);
            komut.Parameters.AddWithValue("@yas", uyeYas_txt.Text);
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Üye Eklendi");
            adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler", baglantı);
            adp.Fill(ds, "Uyeler");
            uyeGridView.DataSource = ds;
            uyeGridView.DataMember = "Uyeler";
            baglantı.Close();
        }

        private void YoneticiAboneler_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            baglantı.Open();
            komut = new SqlCommand("select * from Uyeler", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                uye_adsoyad_txt.Text = okuyucu["Uye_İsim"].ToString();
                uyeAdres_txt.Text = okuyucu["Uye_Adres"].ToString();
                uyeTelefon_txt.Text = okuyucu["Uye_Telefon"].ToString();
                uyeTarih_txt.Text = okuyucu["UyelikTarihi"].ToString();
                uyeFotograf_txt.Text = okuyucu["Uye_Fotograf"].ToString();
                uyeKimlik_txt.Text = okuyucu["TcKimlik"].ToString();
                uyeYas_txt.Text = okuyucu["Uye_Yas"].ToString();
                uyeBitis_txt.Text = okuyucu["UyeBitisTarihi"].ToString();
                uyeİd_txt.Text = okuyucu["Uye_id"].ToString();
                pictureBox1.ImageLocation = Application.StartupPath + okuyucu["Uye_Fotograf"].ToString();


            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler", baglantı);
            adp.Fill(ds, "Uyeler");
            uyeGridView.DataSource = ds;
            uyeGridView.DataMember = "Uyeler";
            baglantı.Close();
        }

        private void uyeGuncelle_btn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("update Uyeler set Uye_İsim=@isim, Uye_Adres=@adres, Uye_Telefon=@telefon, UyelikTarihi=@tarih, UyeBitisTarihi=@bitistarih, TcKimlik=@kimlik, Uye_yas=@yas,Uye_Fotograf=@foto where Uye_id=@id ", baglantı);

            komut.Parameters.AddWithValue("@isim", uye_adsoyad_txt.Text);
            komut.Parameters.AddWithValue("@adres", uyeAdres_txt.Text);
            komut.Parameters.AddWithValue("@telefon", uyeTelefon_txt.Text);
            komut.Parameters.AddWithValue("@tarih", uyeTarih_txt.Text);
            komut.Parameters.AddWithValue("@bitistarih", uyeBitis_txt.Text);
            komut.Parameters.AddWithValue("@kimlik", uyeKimlik_txt.Text);
            komut.Parameters.AddWithValue("@foto", uyeFotograf_txt.Text);
            komut.Parameters.AddWithValue("@yas", uyeYas_txt.Text);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(uyeİd_txt.Text));
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Üye Güncellendi");
            adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler", baglantı);
            adp.Fill(ds, "Uyeler");
            uyeGridView.DataSource = ds;
            uyeGridView.DataMember = "Uyeler";
            baglantı.Close();
        }

        private void uyeSil_btn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("delete from Uyeler where Uye_id=@id ", baglantı);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(uyeİd_txt.Text));
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Üye Silindi");
            adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler", baglantı);
            adp.Fill(ds, "Uyeler");
            uyeGridView.DataSource = ds;
            uyeGridView.DataMember = "Uyeler";

            baglantı.Close();

            uyeİd_txt.Clear();
            uye_adsoyad_txt.Clear();
            uyeAdres_txt.Clear();
            uyeTelefon_txt.Clear();
            uyeTarih_txt.Clear();
            uyeBitis_txt.Clear();
            uyeYas_txt.Clear();
            uyeKimlik_txt.Clear();
            uyeAra_txt.Clear();
            uyeFotograf_txt.Clear();
        }

        private void uyeTemizle_btn_Click(object sender, EventArgs e)
        {
            uyeİd_txt.Clear();
            uye_adsoyad_txt.Clear();
            uyeAdres_txt.Clear();
            uyeTelefon_txt.Clear();
            uyeTarih_txt.Clear();
            uyeBitis_txt.Clear();
            uyeYas_txt.Clear();
            uyeKimlik_txt.Clear();
            uyeAra_txt.Clear();
            uyeFotograf_txt.Clear();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler", baglantı);
            adp.Fill(ds, "Uyeler");
            uyeGridView.DataSource = ds;
            uyeGridView.DataMember = "Uyeler";

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
                uyeFotograf_txt.Text = resimyol;
            }
        }

        private void uyeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            uyeİd_txt.Text = uyeGridView.Rows[x].Cells[0].Value.ToString();
            uye_adsoyad_txt.Text = uyeGridView.Rows[x].Cells[1].Value.ToString();
            uyeAdres_txt.Text = uyeGridView.Rows[x].Cells[2].Value.ToString();
            uyeKimlik_txt.Text = uyeGridView.Rows[x].Cells[6].Value.ToString();
            uyeYas_txt.Text = uyeGridView.Rows[x].Cells[7].Value.ToString();
            uyeFotograf_txt.Text = uyeGridView.Rows[x].Cells[8].Value.ToString();
            uyeTelefon_txt.Text = uyeGridView.Rows[x].Cells[3].Value.ToString();
            uyeTarih_txt.Text = uyeGridView.Rows[x].Cells[4].Value.ToString();
            uyeBitis_txt.Text = uyeGridView.Rows[x].Cells[5].Value.ToString();
            pictureBox1.ImageLocation = Application.StartupPath + uyeGridView.Rows[x].Cells[8].Value.ToString();
        }

        private void araBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            uyeGridView.AutoGenerateColumns = false;
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler where Uye_İsim='"+uyeAra_txt.Text+"'", baglantı);
            adp.Fill(ds, "Uyeler");
            uyeGridView.DataSource = ds;
            uyeGridView.DataMember = "Uyeler";

            komut = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres, Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi', UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler where Uye_İsim='" + uyeAra_txt.Text + "'", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();


            if (okuyucu.Read())
            {
                uyeİd_txt.Text = okuyucu.GetValue(0).ToString();
                uye_adsoyad_txt.Text = okuyucu.GetValue(1).ToString();
                uyeAdres_txt.Text = okuyucu.GetValue(2).ToString();
                uyeTelefon_txt.Text = okuyucu.GetValue(3).ToString();
                uyeTarih_txt.Text = okuyucu.GetValue(4).ToString();
                uyeBitis_txt.Text = okuyucu.GetValue(5).ToString();
                uyeKimlik_txt.Text = okuyucu.GetValue(6).ToString();
                uyeYas_txt.Text = okuyucu.GetValue(7).ToString();
                uyeFotograf_txt.Text = okuyucu.GetValue(8).ToString();

            }
            else
            {
                okuyucu.Close();
                ds.Clear();
                adp.SelectCommand = new SqlCommand("select Uye_id as Numara, Uye_İsim as İsim, Uye_Adres as Adres , Uye_Telefon as Telefon, UyelikTarihi as 'Üyelik Tarihi' , UyeBitisTarihi as 'Bitiş Tarihi', TcKimlik as Kimlik, Uye_Yas as Yaş, Uye_Fotograf as Fotoğraf from Uyeler", baglantı);
                adp.Fill(ds, "Uyeler");
                uyeGridView.DataSource = ds;
                uyeGridView.DataMember = "Uyeler";
                MessageBox.Show("Üye Bulunamadı");

            }



            baglantı.Close();
        }
    }
}
