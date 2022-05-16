using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;

namespace SMARTLİBRARY
{
    public partial class YoneticiPanel : Form
    {
        
        private Form activeForm;
        public YoneticiPanel()
        {
            InitializeComponent();
        }
        
        private void OpenChildForm(Form childform, object btnSender)
        {
            if(activeForm !=null)
            {
                activeForm.Close();
            }
            activeForm = childform;
            childform.TopLevel = false;
            //ActivateButton(btnSender);
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelDesktopPanel.Controls.Add(childform);
            this.panelDesktopPanel.Tag = childform;
            childform.BringToFront();
            childform.Show();
            lbltitle.Text = childform.Text;
        }
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new YoneticiCalisanlar(), sender);



        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //YoneticiAboneler abone = new YoneticiAboneler();
            //abone.Show();
            OpenChildForm(new YoneticiAboneler(), sender);
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new Kitaplar(), sender);
            
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

            OpenChildForm(new YoneticiKitaplik(), sender);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
           
        }

        private void YoneticiPanel_Load(object sender, EventArgs e)
        {
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new yoneticiKitaplar(), sender);
        }

        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new yoneticidashboard(), sender);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
