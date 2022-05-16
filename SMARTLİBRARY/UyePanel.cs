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

namespace SMARTLİBRARY
{
    public partial class UyePanel : Form
    {
        private Form activeForm;
        public UyePanel()
        {
            InitializeComponent();
        }
        private void OpenChildForm(Form childform, object btnSender)
        {
            if (activeForm != null)
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
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new İsteklerim(), sender);
        }

        private void bunifuButton3_Click(object sender, EventArgs e) //kitaplık
        {
            OpenChildForm(new UyeKitaplık(), sender);
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UyeKitaplarim(), sender);
        }

        private void UyePanel_Load(object sender, EventArgs e)
        {
             
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelDesktopPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
