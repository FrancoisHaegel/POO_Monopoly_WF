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
        private static Form_board instance;
        public Form_board()
        {
            foreach (var file in dice_images_directory.GetFiles("*.png"))
            {
                dice_images.Add(Image.FromFile(file.FullName));
            }
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            InitializeComponent();
        }

        public static Form_board GetInstance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new Form_board();
                }
                return instance;
            }
        }

        private Graphics g;
        DirectoryInfo dice_images_directory = new DirectoryInfo(".\\Resources\\");
        List<Image> dice_images = new List<Image>();
        int index = 0;
        
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
            if (index == 20)
            {
                int tempX = ((drawPanel_plateau.Size.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = ((drawPanel_plateau.Size.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index == 30)
            {
                int tempX = drawPanel_plateau.Size.Width - ((drawPanel_plateau.Size.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = ((drawPanel_plateau.Size.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index == 0)
            {
                int tempX = drawPanel_plateau.Size.Width - ((drawPanel_plateau.Size.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = drawPanel_plateau.Size.Height - ((drawPanel_plateau.Size.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index == 10)
            {
                int tempX = ((drawPanel_plateau.Size.Width / 10) / 2) - (jetons[jetonColor][2]);
                int tempY = drawPanel_plateau.Size.Height - ((drawPanel_plateau.Size.Height / 10) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index < 10)
            {
                int tempX = drawPanel_plateau.Size.Width - ((index + 1) * (drawPanel_plateau.Size.Width / 12) + ((drawPanel_plateau.Size.Width / 12) / 2) - (jetons[jetonColor][2]));
                int tempY = drawPanel_plateau.Size.Height - ((drawPanel_plateau.Size.Height / 12) / 2) - (jetons[jetonColor][2]);
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index < 20)
            {
                int tempX = ((drawPanel_plateau.Size.Width / 12) / 2) - jetons[jetonColor][2];
                int tempY = drawPanel_plateau.Size.Height - ((index + 1 - 10) * (drawPanel_plateau.Size.Height / 12) + ((drawPanel_plateau.Size.Height / 13) / 2) - (jetons[jetonColor][2]));
                tempY -= 5;
                tempX += 15;
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
            else if (index < 30)
            {
                int tempX = ((index + 1 - 20) * (drawPanel_plateau.Size.Width / 12)) + ((drawPanel_plateau.Size.Width / 12)/2) - (jetons[jetonColor][2]);
                tempX -= 20;
                int tempY = ((drawPanel_plateau.Size.Height / 12) / 2) - (jetons[jetonColor][2]);
                tempY += 10;
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }else if  (index < 40)
            {
                int tempX = drawPanel_plateau.Size.Width - ((drawPanel_plateau.Size.Width / 12) / 2) - (jetons[jetonColor][2]);
                int tempY = (index + 1 - 30) * (drawPanel_plateau.Size.Height / 12) + ((this.ClientSize.Height / 12) / 2) - (jetons[jetonColor][2]);
                tempY -= 15;
                setJetonPosition(jetonColor, tempX, tempY, jetons[jetonColor][2]);
            }
        }

        private void test()
        {

            while (index < 40)
            {
                setJetonIndex(Color.DarkOrange, index);
                setJetonIndex(Color.MediumTurquoise, index);
                setJetonIndex(Color.MediumSeaGreen, index);
                setJetonIndex(Color.Gold, index);
                setJetonIndex(Color.RoyalBlue, index);
                setJetonIndex(Color.LightPink, index);
                Thread.Sleep(100);
                index++;
                if (index == 40)
                {
                    index = 0;
                }
            }
        }

        public void insert_console(string message)
        {
            this.listBox_console.Items.Add(this.textBox_commande.Text);
            this.listBox_console.SelectedIndex = listBox_console.Items.Count - 1;
            this.listBox_console.SelectedIndex = -1;
        }

        private void handle_commande(string commande)
        {
            int i = 0;
            if (commande == "q")
            {
                this.Close();
            }
            else if (int.TryParse(commande, out i))
            {
                Form_board.GetInstance.setJetonIndex(Color.Gold, i);
            }
            insert_console(commande);
        }

        private void Form_main_Resize(object sender, EventArgs e)
        {
            drawPanel_plateau.Height = this.ClientSize.Height;
            drawPanel_plateau.Width = drawPanel_plateau.Height;
            drawPanel_plateau.Location = new Point(
                this.ClientSize.Width / 2 - drawPanel_plateau.Size.Width / 2,
                this.ClientSize.Height / 2 - drawPanel_plateau.Size.Height / 2);
            foreach (KeyValuePair<Color, int[]> entry in jetons)
            {
                setJetonIndex(entry.Key, 0);
            }
        }

        private void drawPanel_plateau_Paint(object sender, PaintEventArgs e)
        {
            foreach (KeyValuePair<Color, int[]> entry in jetons)
            {
                e.Graphics.FillEllipse(new SolidBrush(entry.Key), entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[2]);
            }
        }

        private void drawPanel_plateau_SizeChanged(object sender, EventArgs e)
        {
            panel_control.Location = new Point(drawPanel_plateau.Location.X + drawPanel_plateau.Size.Width);
            panel_control.Width = (this.ClientSize.Width - drawPanel_plateau.Size.Width) / 2 - 35;
        }

        private void button_out_of_jail_Click(object sender, EventArgs e)
        {

        }

        private void button_mortage_Click(object sender, EventArgs e)
        {

        }

        private void button_trade_Click(object sender, EventArgs e)
        {

        }

        private void button_end_turn_Click(object sender, EventArgs e)
        {
            test();
        }

        private void button_roll_dice_Click(object sender, EventArgs e)
        {
            showDices(2, 3);
        }

        private void textBox_commande_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                handle_commande(textBox_commande.Text);
                textBox_commande.Text = "";
            }
        }
    }
}
