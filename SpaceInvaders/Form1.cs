using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        bool idiLevo;
        bool idiDesno;
        int brzina = 5;
        int rezultat = 0;
        bool jePritisnuto;
        int ukupnoNeprijatelja=12;
        int brzinaIgraca = 4;
        
        public Form1()
        {
            InitializeComponent();
            this.Text = "Space Invaders";
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        //Kada je dugme pritisnuto
        private void dugmejedole(object sender, KeyEventArgs e)
        {
         if(e.KeyCode==Keys.Left)
            { idiLevo = true; }
            if (e.KeyCode == Keys.Right)
            { idiDesno = true; }
            //Kada je space pritisnut 
            if (e.KeyCode==Keys.Space && !jePritisnuto)
            {
                jePritisnuto = true;
                napraviMetak();  }
        }
        //Kada se dugme vratilo u prvobitni polozaj
        private void dugmejegore(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            { idiLevo = false; }
            if (e.KeyCode == Keys.Right)
            { idiDesno = false; }
            if(jePritisnuto)
            {
                jePritisnuto = false;
            }
        }
        int Brojac = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Kretanje tenka
            if (idiLevo)
            {
                player.Left -= brzinaIgraca;
                //Ne da mu da predje levu granicu
                if (player.Left <= 2)
                    player.Left = 2;
            }
            if (idiDesno)
            {
                player.Left += brzinaIgraca;
                //Ne da mu da predje desnu granicu
                if (player.Left >= 630)
                    player.Left = 630;
            }
            //Kretanje invadera
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "invader")
                {
                    //Ako se igrac i invader udare gotova igra
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        gotovaIgra();
                    }
                    //Prelazak invadera u novi red
                    ((PictureBox)x).Left += brzina;
                    if (((PictureBox)x).Left > 720)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;
                    }
                }
            }
            //Kretanje metka
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "metak")
                {
                    y.Top -= 20;

                    if (((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
            //Kada metak udari u invadera
            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag == "invader")
                    {
                        if (j is PictureBox && j.Tag == "metak")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                rezultat++;
                                i.Visible = false;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);

                            }
                        }

                    }
                }
            }
            label1.Text = "Rezultat: " + rezultat;
            //Kada tenk unisti svakog invadera
            if (rezultat > ukupnoNeprijatelja - 1)
            {
                timer1.Stop();
                MessageBox.Show("Pobedili ste! ");
                this.Close();
                
            }
            //Random funkcija za biranje invadera
            Random nasumicniInvader = new Random();
            int brojPictureBoxa = nasumicniInvader.Next(1, 12);
            Brojac++;
            //niz pictureBox-ova
            PictureBox[] boxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12 };
            //Ispaljuje bombu nasumicni invader
            if (Brojac % 33 == 0 && boxes[brojPictureBoxa].Visible == true)
                    {
                    PictureBox bomba = new PictureBox();
                    bomba.Image = Properties.Resources.invadermissile;
                    bomba.Size = new Size(5, 20);
                    bomba.Tag = "bomba";
                    bomba.Left = boxes[brojPictureBoxa].Left + boxes[brojPictureBoxa].Width / 2;
                    bomba.Top = boxes[brojPictureBoxa].Top + 20;
                    this.Controls.Add(bomba);
                    bomba.BringToFront();
            }
            //letenje metka
            foreach (Control ll in this.Controls)
            {
                if (ll is PictureBox && ll.Tag == "bomba")
                {
                    ll.Top += 10;
                }
            }
            //Sudaranje bombe sa igracem
            foreach (Control ll in this.Controls)
            {
                    if (ll is PictureBox && ll.Tag == "bomba")
                    {
                    if (ll.Bounds.IntersectsWith(player.Bounds))
                            { 
                            this.Controls.Remove(ll);
                            gotovaIgra();
                            break;
                            } 
                    }
            }

        }
        //Pravljenje metka
        private void napraviMetak()
        {
            PictureBox metak = new PictureBox();
            metak.Image = Properties.Resources.bullet;
            metak.Size = new Size(5, 20);
            metak.Tag = "metak";
            metak.Left = player.Left + player.Width / 2;
            metak.Top = player.Top - 20;
            this.Controls.Add(metak);
            metak.BringToFront();
        }
        //Funkcija koja se poziva kada se izgubi
        private void gotovaIgra()
        {
            timer1.Stop();
            MessageBox.Show("Izgubili ste!\n" + "Vas rezultat je " + rezultat);
            this.Close();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
    }
}
