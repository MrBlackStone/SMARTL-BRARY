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
    public partial class YoneticiCalisanlar : Form
    {

        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public YoneticiCalisanlar()
        {
            InitializeComponent();
        }

        private void YoneticiCalisanlar_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            baglantı.Open();
            komut = new SqlCommand("select * from Calisanlar", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                adsoyad_txt.Text = okuyucu["Calisan_isim"].ToString();
                adres_txt.Text = okuyucu["Calisan_adres"].ToString();
                rutbe_txt.Text = okuyucu["Rutbe"].ToString();
                baslamaTarih_txt.Text = okuyucu["CalisanGirisTarihi"].ToString();
                telefon_txt.Text = okuyucu["Telefon"].ToString();
                maas_txt.Text = okuyucu["Maas"].ToString();
                calisan_id_txt.Text = okuyucu["Calisan_id"].ToString();
                fotograf_txt.Text = okuyucu["CalisanFotograf"].ToString();
                pictureBox1.ImageLocation = Application.StartupPath + okuyucu["CalisanFotograf"].ToString();


            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select Calisan_id as Numara, Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf from Calisanlar", baglantı);
            adp.Fill(ds, "Calisanlar");
            CalisanlarDataGridView.DataSource = ds;
            CalisanlarDataGridView.DataMember = "Calisanlar";
            baglantı.Close();
        }
        private void calisanEkleBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            // komut = new SqlCommand("insert into Calisanlar(Calisan_isim,Calisan_adres,Rutbe,CalisanGirisTarihi,Telefon,Maas)values('"+adsoyad_txt.Text+"','"+adres_txt.Text+"','"+rutbe_txt.Text+"','"+baslamaTarih_txt.Text+"','"+telefon_txt.Text+"','"+maas_txt.Text+"')", baglantı);
            komut = new SqlCommand("insert into Calisanlar(Calisan_isim,Calisan_adres,Rutbe,CalisanGirisTarihi,Telefon,Maas,CalisanFotograf)values(@Calisan_isim,@Calisan_adres,@Rutbe,@CalisanGirisTarihi,@Telefon,@Maas,@Fotograf)", baglantı);
            komut.Parameters.AddWithValue("@Calisan_isim", adsoyad_txt.Text);
            komut.Parameters.AddWithValue("@Calisan_adres", adres_txt.Text);
            komut.Parameters.AddWithValue("@Rutbe", rutbe_txt.Text);
            komut.Parameters.AddWithValue("@CalisanGirisTarihi", baslamaTarih_txt.Text);
            komut.Parameters.AddWithValue("@Telefon", telefon_txt.Text);
            komut.Parameters.AddWithValue("@Maas", Convert.ToInt32(maas_txt.Text));
            komut.Parameters.AddWithValue("@Fotograf", fotograf_txt.Text);
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Çalışan Eklendi");
            adp.SelectCommand = new SqlCommand("select Calisan_id as Numara, Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf  from Calisanlar", baglantı);
            adp.Fill(ds, "Calisanlar");
            CalisanlarDataGridView.DataSource = ds;
            CalisanlarDataGridView.DataMember = "Calisanlar";
            baglantı.Close();


        }

        private void CalisanGuncelleBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("update Calisanlar set Calisan_isim='" + adsoyad_txt.Text + "',Calisan_adres='" + adres_txt.Text + "',Telefon='" + telefon_txt.Text + "',Rutbe='" + rutbe_txt.Text + "',Maas='" + maas_txt.Text + "',CalisanGirisTarihi='" + baslamaTarih_txt.Text + "',CalisanFotograf='" + fotograf_txt.Text + "' where Calisan_id='" + calisan_id_txt.Text + "'", baglantı);


            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Kayıt Güncellendi");
            adp.SelectCommand = new SqlCommand("select  Calisan_id as Numara ,Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf  from Calisanlar", baglantı);

            adp.Fill(ds, "Calisanlar");
            CalisanlarDataGridView.DataSource = ds;
            CalisanlarDataGridView.DataMember = "Calisanlar";
            baglantı.Close();
        }

        private void CalisanSilBtn_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("delete from Calisanlar where Calisan_id='" + calisan_id_txt.Text + "'", baglantı);
            komut.ExecuteNonQuery();
            ds.Clear();
            MessageBox.Show("Kayıt Silindi");
            adp.SelectCommand = new SqlCommand("select  Calisan_id as Numara,Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf  from Calisanlar", baglantı);
            adp.Fill(ds, "Calisanlar");
            CalisanlarDataGridView.DataSource = ds;
            CalisanlarDataGridView.DataMember = "Calisanlar";

            calisan_id_txt.Clear();
            adsoyad_txt.Clear();
            baslamaTarih_txt.Clear();
            maas_txt.Clear();
            telefon_txt.Clear();
            adres_txt.Clear();
            fotograf_txt.Clear();
            rutbe_txt.Clear();

            baglantı.Close();
        }

        private void CalisanTemizleBtn_Click(object sender, EventArgs e)
        {

            calisan_id_txt.Clear();
            adsoyad_txt.Clear();
            baslamaTarih_txt.Clear();
            maas_txt.Clear();
            telefon_txt.Clear();
            adres_txt.Clear();
            fotograf_txt.Clear();
            rutbe_txt.Clear();
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select Calisan_id as Numara, Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf from Calisanlar", baglantı);
            adp.Fill(ds, "Calisanlar");
            CalisanlarDataGridView.DataSource = ds;
            CalisanlarDataGridView.DataMember = "Calisanlar";
        }

        private void CalisanlarDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int x = e.RowIndex;
            calisan_id_txt.Text = CalisanlarDataGridView.Rows[x].Cells[0].Value.ToString();
            adsoyad_txt.Text = CalisanlarDataGridView.Rows[x].Cells[1].Value.ToString();
            baslamaTarih_txt.Text = CalisanlarDataGridView.Rows[x].Cells[6].Value.ToString();
            maas_txt.Text = CalisanlarDataGridView.Rows[x].Cells[4].Value.ToString();
            telefon_txt.Text = CalisanlarDataGridView.Rows[x].Cells[3].Value.ToString();
            adres_txt.Text = CalisanlarDataGridView.Rows[x].Cells[2].Value.ToString();
            fotograf_txt.Text = CalisanlarDataGridView.Rows[x].Cells[7].Value.ToString();
            rutbe_txt.Text = CalisanlarDataGridView.Rows[x].Cells[5].Value.ToString();
            pictureBox1.ImageLocation = Application.StartupPath + CalisanlarDataGridView.Rows[x].Cells[7].Value.ToString();



        }

        private void CalisanAraBtn_Click(object sender, EventArgs e)
        {

            baglantı.Open();
            CalisanlarDataGridView.AutoGenerateColumns = false;
            ds.Clear();
            adp.SelectCommand = new SqlCommand("select  Calisan_id as Numara,Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf  from Calisanlar where Calisan_isim='"+CalisanTXT.Text+"'", baglantı);
            adp.Fill(ds, "Calisanlar");
            CalisanlarDataGridView.DataSource = ds;
            CalisanlarDataGridView.DataMember = "Calisanlar";
            
            komut = new SqlCommand("select  Calisan_id as Numara,Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf  from Calisanlar where Calisan_isim='" + CalisanTXT.Text + "'",baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();


            if(okuyucu.Read())
            {
                calisan_id_txt.Text = okuyucu.GetValue(0).ToString();
                adsoyad_txt.Text = okuyucu.GetValue(1).ToString();
                adres_txt.Text = okuyucu.GetValue(2).ToString();
                telefon_txt.Text = okuyucu.GetValue(3).ToString();
                maas_txt.Text = okuyucu.GetValue(4).ToString();
                rutbe_txt.Text = okuyucu.GetValue(5).ToString();
                baslamaTarih_txt.Text = okuyucu.GetValue(6).ToString();
                fotograf_txt.Text = okuyucu.GetValue(7).ToString();
                
            }
            else
            {
                okuyucu.Close();
                ds.Clear();
                adp.SelectCommand = new SqlCommand("select  Calisan_id as Numara,Calisan_isim as İsim, Calisan_adres as Adres, Telefon, Maas as Maaş, Rutbe as Rütbe, CalisanGirisTarihi as 'Giriş Tarihi', CalisanFotograf as Fotoğraf  from Calisanlar", baglantı);
                adp.Fill(ds, "Calisanlar");
                CalisanlarDataGridView.DataSource = ds;
                CalisanlarDataGridView.DataMember = "Calisanlar";
                MessageBox.Show("Çalışan Bulunamadı");
                
            }
            
            

            baglantı.Close();
        }
        string resimyol;
        private void resimSecBtn_Click(object sender, EventArgs e)
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

        private void rutbe_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
