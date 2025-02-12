﻿using System;
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
    public partial class yoneticidashboard : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=localhost;Initial Catalog=SmartLibrary;Integrated Security=True;Pooling=False");
        SqlCommand komut = new SqlCommand();
        public yoneticidashboard()
        {
            InitializeComponent();
        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void yoneticidashboard_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new SqlCommand("SELECT COUNT(*) FROM  Uyeler ", baglantı);
            uyelbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar ", baglantı);
            kitaplbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  OduncKitaplar ", baglantı);
            odunclbl.Text = komut.ExecuteScalar().ToString();
            

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Polisiye'", baglantı);
            polisiyelbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Felsefe' ", baglantı);
            felsefelbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Fantastik' ", baglantı);
            fantastiklbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Edebiyat' ", baglantı);
            edebiyatlbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Sanat' ", baglantı);
            sanatlbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='BilimKurgu' ", baglantı);
            bilimkurgulbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Tarih' ", baglantı);
            tarihlbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Egitim' ", baglantı);
            egitimlbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Teknoloji' ", baglantı);
            teknolojilbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Ask' ", baglantı);
            asklbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Aksiyon' ", baglantı);
            aksiyonlbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Bilim' ", baglantı);
            bilimlbl.Text = komut.ExecuteScalar().ToString();

            komut = new SqlCommand("SELECT COUNT(*) FROM  Kitaplar where tur='Tarih' ", baglantı);
            tarihlbl.Text = komut.ExecuteScalar().ToString();
            baglantı.Close();
        }

        private void bunifuLabel30_Click(object sender, EventArgs e)
        {

        }
    }
}
