using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Numerics;

namespace Projet_IMA
{
    public partial class Form1 : Form
    {

      
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Affichage.Init(pictureBox1.Width, pictureBox1.Height);
           
          
        }

        public void PictureBoxInvalidate()  { pictureBox1.Invalidate(); }
        public void PictureBoxRefresh()     { pictureBox1.Refresh();    }

        private void button1_Click(object sender, EventArgs e)
        {
          
            SetofPoints.RandomTest();
                 
        }
 

        private void button2_Click_1(object sender, EventArgs e)
        {
            SetofPoints.ScenarioTest();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GiftWrap.start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GiftWrap.Iteration();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MelkMan.start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MelkMan.Iteration();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void TestOperateur(object sender, EventArgs e)
        {
            // exemple d opérations sur les vecteurs 2D

            V2 t = new V2(1, 0);
            V2 r = new V2(0, 1);
            V2 k = t + r;
            BigInteger p = k * t * 2;
            BigInteger n = t ^ r;
            V2 m = -t;
        }
    }
}
