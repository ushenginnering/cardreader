using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace read_and_write_card
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                //INITIALIZE READER
                DLL dL = new DLL();
                byte Flag = 1;
                string start = dL.startCard(Flag);
                if (start != "success")
                {
                    MessageBox.Show("Failed to initialize: plug in an encoder");
                }
                else
                {

                    Random rnd = new Random();
                    Byte[] o = new Byte[10];
                    rnd.NextBytes(o);


                    byte cardNo = o[0];
                    byte door = o[1];
                    byte mk_fg = o[2];
                    string genCard = dL.generateCardId(cardNo, door, mk_fg);

                    Clipboard.SetText(genCard);
                    MessageBox.Show("New Card Id Generated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //INITIALIZE READER
            try
            {
                DLL dL = new DLL();
                byte Flag = 1;
                string start = dL.startCard(Flag);
                if (start != "success")
                {
                    MessageBox.Show("Failed to initialize: plug in an encoder");
                }
                else
                {

                    Random rnd = new Random();
                    Byte[] o = new Byte[10];
                    rnd.NextBytes(o);


                    byte cardNo = o[0];
                    byte door = o[1];
                    byte mk_fg = o[2];
                    string RCard = dL.RCard();


                    Clipboard.SetText(RCard);
                    MessageBox.Show("Card Id Captured");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
