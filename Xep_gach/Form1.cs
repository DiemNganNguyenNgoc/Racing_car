using Racing_Car.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xep_gach
{
    public partial class Form1 : Form
    {
        int roadSpeed = 15;
        int trafficSpeed = 15;
        int playerSpeed = 12;
        int score;
        int carImage;

        Random rand= new Random();
        Random carPosition = new Random();
        bool goleft, goright;
        public Form1()
        {
            InitializeComponent();
            resetgame();
            bntStart.BackColor = Color.White;
            bntStart.ForeColor = Color.SkyBlue;
            
        }

        private void restartGame(object sender, EventArgs e)
        {
           resetgame();
        }

      

        private void keydown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if(e.KeyCode== Keys.Right)
            {
                goright = true;
            }
        }

        private async void GameTimer_Tick(object sender, EventArgs e)
        { 
            label1.Text = "Score: " + score;
            score++;
            
            if (goleft== true && player.Left>25)
            {
                player.Left-= playerSpeed;

            }    

            if(goright == true && player.Left <400)
            {
                player.Left+= playerSpeed;
            }

            road2.Top += roadSpeed;
            road1.Top += roadSpeed;

            if (road2.Top> 510)
            {
                road2.Top = -510;
            }

            if (road1.Top > 510)
            {
                road1.Top = -510;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if(AI1.Top>520)
            {
                ChangeACar(AI1);
                
            }

            if (AI2.Top>520)
            {
                ChangeACar(AI2);
               
            }

            if(player.Bounds.IntersectsWith(AI1.Bounds)|| player.Bounds.IntersectsWith(AI2.Bounds))
            {
               
                gameOver();
                
            }

        }

        private void gameOver()
        {
            playtrash();
            GameTimer.Stop();
            trashpicbox.Visible= true;
            player.Controls.Add(trashpicbox);
            trashpicbox.Location = new Point (10, 3);
            
            award.Visible = true;
            award.BringToFront();

            bntStart.Enabled = true;

            if(score >=500)
            {

                award.Image= Resources.gold;
            }

            else if(score >=300 && score< 500)
            {
                award.Image= Resources.silver;
            }
            else
            {
                award.Image= Resources.bronze;
            }
           


        }


        private void ChangeACar(PictureBox A)
        {
            carImage= rand.Next(1, 10);
            switch(carImage)
            {
                case 1:
                    A.Image = Resources.greentruck;
                    break;
                case 2:
                    A.Image = Resources.greytruck;
                    break;
                case 3:
                    A.Image = Resources.yellowcar;
                    break;
                 case 4:
                    A.Image = Resources.ambulance;
                    break;
                case 5:
                    A.Image = Resources.purplecar;
                    break;
                case 6:
                    A.Image= Resources.bluecar;
                    break;
                case 7:
                    A.Image= Resources.lightgreentruck;
                    break;
                case 8:
                    A.Image= Resources.greycar;
                    break;
                case 9:
                    A.Image= Resources.darkbluetruck;
                    break;
                case 10:
                    A.Image= Resources.cargreen;
                    break;
            }
            A.Top= carPosition.Next(100, 400)*-1;

            if ((string)A.Tag == "carLeft")
            {
                A.Left = carPosition.Next(25, 225);
            }
            if ((string)A.Tag == "carRight")
            {
                A.Left = carPosition.Next(245, 422);
            }
        }

        void resetgame()
        {
            bntStart.Enabled = false;
            award.Visible= false;
            trashpicbox.Visible= false;
            goleft=false;
            goright=false;
            score=0;
            

            roadSpeed=12;
            trafficSpeed=15;

            AI1.Top= carPosition.Next(200, 500)*-1;
            AI1.Left= carPosition.Next(5, 200);

            AI2.Top= carPosition.Next(200, 500)*-1;
            AI1.Top= carPosition.Next(245, 475)*-1;

            GameTimer.Start();
        }

       
        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode== Keys.Right)
            {
                goright = false;
            }
        }



        private void playtrash()
        {
            System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer(Resources.trash);
            soundPlayer.Play();
            
        }

       
    }
}
