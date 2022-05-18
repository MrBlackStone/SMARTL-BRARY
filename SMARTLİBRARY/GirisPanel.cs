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
    public partial class GirisPanel : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=.;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        public static string uyeisimID = "";
        public GirisPanel()
        {
            InitializeComponent();
        }

        private void GirisPanel_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            YoneticiPanel yntcpnl = new YoneticiPanel();
            CalisanPanel clsnpnl = new CalisanPanel();

            UyePanel uyepnl = new UyePanel();
            SqlCommand kontrol = new SqlCommand("select * from Calisanlar where CalisanKullaniciAdi='" + kullaniciAdTxt.Text + "' and CalisanSifre='" + sifreTxt.Text + "'", baglantı);
            SqlDataReader oku = kontrol.ExecuteReader();

            SqlCommand uyekontrol = new SqlCommand("select * from Uyeler where UyeKullaniciAdi='" + kullaniciAdTxt.Text + "' and UyeSifre='" + sifreTxt.Text + "'", baglantı);
            
            oku.Read();
            
            if(oku.HasRows)
            {
                if(oku["Rutbe"].ToString()=="Çalışan")
                {
                    clsnpnl.Show();
                    this.Hide();
                }
                if(oku["Rutbe"].ToString()=="Yönetici")
                {
                    
                    yntcpnl.Show();
                    this.Hide();
                }
                   // oku.Close();
            }
            oku.Close();
            SqlDataReader uyeOku = uyekontrol.ExecuteReader();
            uyeOku.Read();
            if (uyeOku.HasRows)
            {

                uyeisimID = uyeOku["Uye_id"].ToString();
                uyepnl.Show();
                
                this.Hide();


            }
            uyeOku.Close();
            
            
            baglantı.Close();
        }
    }
}
