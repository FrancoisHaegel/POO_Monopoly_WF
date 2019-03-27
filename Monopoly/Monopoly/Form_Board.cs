using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Monopoly
{
    public partial class Form_board : Form 
    {
        private Graphics g;
        DirectoryInfo dice_images_directory = new DirectoryInfo(".\\Resources\\");
        List<Image> dice_images = new List<Image>();


    int index = 0;

        public Form_board()
        {
            // To remove flickering
            this.DoubleBuffered = true;
            foreach (var file in dice_images_directory.GetFiles("*.png"))
            {
                dice_images.Add(Image.FromFile(file.FullName));
            }
            InitializeComponent();
        }

        // Dictionaires des 6 jetons du jeux qui associe à une couleur de jeton une liste d'entier [coordonee_X, coordonee_Y, rayon]
        Dictionary<Color, int[]> jetons = new Dictionary<Color, int[]>()
        {
            {Color.DarkOrange, new int[] {5,5, 20 } },
            {Color.MediumTurquoise, new int[] {50, 5, 20 } },
            {Color.MediumSeaGreen, new int[] {95, 5, 20 } },
            {Color.Gold, new int[] {5, 50, 20 } },
            {Color.RoyalBlue, new int[] {50, 50, 20 } },
            {Color.LightPink, new int[] {95, 50, 20 } },
        };

        public void showDices(int dice_A, int dice_B)
        {
            Popup_dices dices = new Popup_dices(dice_images[dice_A-1], dice_images[dice_B-1]);
            dices.Show();
        }

        public int getJetonPositionX(Color color)
        {
            return jetons[color][0];
        }

        public int getJetonPositionY(Color color)
        {
            return jetons[color][1];
        }

        private void setJetonPosition(Color color, int posX, int posY, int rayon)
        {
            int x = 0;
            int y = 0;
            
            if (color == Color.DarkOrange)
            {
                x = -rayon / 2;
                y = -rayon / 4;
            }
           if (color == Color.MediumTurquoise)
            {
                y = -rayon / 2;
            }
            if (color == Color.MediumSeaGreen)
            {
                x = rayon / 2;
                y = -rayon / 4;
            }
            if (color == Color.Gold)
            {
                x = -rayon / 2;
                y = rayon / 4;
            }
            if (color == Color.RoyalBlue)
            {
                y = rayon / 2;
            }
            if (color == Color.LightPink)
            {
                x = rayon / 2;
                y = rayon / 4;
            }
            

            jetons[color][0] = posX + x;
            jetons[color][1] = posY + y;
            jetons[color][2] = rayon;
            // Invalidate the control and trigger OnPaint event
            Refresh();
        }

        public void setJetonIndex(Color jetonColor, int index)
        {
            if (index == 0)
            {
                int tempX = ((this.ClientSize.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = ((this.ClientSize.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index == 10)
            {
                int tempX = this.ClientSize.Width - ((this.ClientSize.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = ((this.ClientSize.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index == 20)
            {
                int tempX = this.ClientSize.Width - ((this.ClientSize.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = this.ClientSize.Height - ((this.ClientSize.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index == 30)
            {
                int tempX = ((this.ClientSize.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = this.ClientSize.Height - ((this.ClientSize.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index < 10)
            {
                int tempX = ((index + 1) * (this.ClientSize.Width / 12)) + ((this.ClientSize.Width / 12)/2) - (jetons[jetonColor][2]);
                tempX -= 15;
                int tempY = ((this.ClientSize.Height / 12) / 2) - (jetons[jetonColor][2]);
                tempY += 5;
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }else if  (index < 20)
            {
                int tempX = this.ClientSize.Width - ((this.ClientSize.Width / 12) / 2) - (jetons[jetonColor][2]);
                int tempY = (index + 1 - 10) * (this.ClientSize.Height / 12) + ((this.ClientSize.Height / 12) / 2) - (jetons[jetonColor][2]);
                tempY -= 10;
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index < 30)
            {
                int tempX = this.ClientSize.Width - ((index + 1 - 20) * (this.ClientSize.Width / 12) + ((this.ClientSize.Width / 12) / 2) - (jetons[jetonColor][2]));
                int tempY = this.ClientSize.Height - ((this.ClientSize.Height / 12) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index < 40)
            {
                int tempX = ((this.ClientSize.Width / 12) / 2) - jetons[jetonColor][2];
                int tempY = this.ClientSize.Height - ((index + 1 - 30) * (this.ClientSize.Height / 12) + ((this.ClientSize.Height / 13) / 2) - (jetons[jetonColor][2]));
                tempY -= 5;
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
        }

        private void test()
        {

            while (index < 40)
            {
                /*
                setJetonIndex(Color.DarkOrange, index + 1);
                setJetonIndex(Color.MediumTurquoise, index + 2);
                setJetonIndex(Color.MediumSeaGreen, index + 3);
                setJetonIndex(Color.Gold, index + 4);
                setJetonIndex(Color.RoyalBlue, index + 5);
                setJetonIndex(Color.LightPink, index + 6);
                */
                setJetonIndex(Color.DarkOrange, index);
                setJetonIndex(Color.MediumTurquoise, index);
                setJetonIndex(Color.MediumSeaGreen, index);
                setJetonIndex(Color.Gold, index);
                setJetonIndex(Color.RoyalBlue, index);
                setJetonIndex(Color.LightPink, index);
                Thread.Sleep(400);
                index++;
                if (index == 40)
                {
                    index = 0;
                }
            }
        }


        private void Form_main_Click(object sender, EventArgs e)
        {
            //test();
            //showDices(2, 3)
            Form_actions actions = new Form_actions();
            actions.Show();
    }

        //OnPaint event
        private void Form_main_Paint(object sender, PaintEventArgs e)
        {

            foreach (KeyValuePair<Color, int[]> entry in jetons)
            {

                e.Graphics.FillEllipse(new SolidBrush(entry.Key), entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[2]);
            }
        }

        private void Form_main_Resize(object sender, EventArgs e)
        {
            setJetonIndex(Color.Gold, index);
            ((Control)sender).Refresh();
        }
    }
}
