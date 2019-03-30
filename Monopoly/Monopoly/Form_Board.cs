using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Monopoly.controller;

namespace Monopoly
{
    public partial class Form_board : Form 
    {
        private static int PLAYER_HEIGHT = 20;
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
            {Color.DarkOrange, new int[] {0, 0, 20} },
            {Color.MediumTurquoise, new int[] { 0, 0, 20 } },
            {Color.MediumSeaGreen, new int[] { 0, 0, 20 } },
            {Color.Gold, new int[] { 0, 0, 20 } },
            {Color.RoyalBlue, new int[] { 0, 0, 20 } },
            {Color.LightPink, new int[] { 0, 0, 20 } },
        };

        Dictionary<String, Color> joueurs = new Dictionary<String, Color>()
        {
            {"Francois", Color.DarkOrange},
            {"Bruno", Color.MediumTurquoise},
            {"Nathan", Color.MediumSeaGreen},
            {"Ching", Color.Gold},
            {"Chang", Color.RoyalBlue},
            {"Chong", Color.LightPink},
        };

        Dictionary<String, int> index_proprietes = new Dictionary<String, int>();

        Dictionary<String, List<Tuple<String, Color>>> proprietes = new Dictionary<String, List<Tuple<String, Color>>>()
        {
            {"Francois", new List<Tuple<String, Color>>{  } },
            {"Bruno", new List<Tuple<String, Color>>{ Tuple.Create("Hautepierre", Color.Red), Tuple.Create("Neuhof", Color.Red) } },
            {"Nathan", new List<Tuple<String, Color>>{ Tuple.Create("Gare", Color.Blue), Tuple.Create("Meinau", Color.Blue) } },
            {"Ching", new List<Tuple<String, Color>>{} },
            {"Chang", new List<Tuple<String, Color>>{} },
            {"Chong", new List<Tuple<String, Color>>{}},
        };


        private void init_test()
        {
            /*
            addJoueur("Francois", Color.DarkOrange);
            addJoueur("Bruno", Color.MediumTurquoise);
            addJoueur("Nathan", Color.MediumSeaGreen);
            addJoueur("Ching", Color.Gold);
            addJoueur("Chang", Color.RoyalBlue);
            addJoueur("Chong", Color.LightPink);
            */

            addPropriete("Francois", "Elsau", Color.Gold, 2);
            //addPropriete("Francois", "Aloutettes", Color.Gold, 1);
            //addPropriete("Francois", "Gare", Color.Blue, 3);
        }

        // Affiche les images des des correspondants au entier donne
        public void showDices(int dice_A, int dice_B)
        {
            Popup_dices dices = new Popup_dices(dice_images[dice_A-1], dice_images[dice_B-1]);
            dices.Show();
        }

        public void showDices(int dice)
        {
            Popup_dices dices = new Popup_dices(dice_images[dice - 1]);
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

        public void addJoueur(String nom, Color color)
        {
            joueurs.Add(nom, color);
            proprietes.Add(nom, new List<Tuple<String, Color>> { });
            drawPanel_player.Refresh();
        }

        public void removeJoueur(String nom)
        {
            joueurs.Remove(nom);
            drawPanel_player.Refresh();
        }

        public void addPropriete(String joueur, String nomPropriete, Color couleur)
        {
            proprietes[joueur].Add(Tuple.Create(nomPropriete, couleur));
            index_proprietes[nomPropriete] = 0;
            drawPanel_player.Refresh();
        }

        public void addPropriete(String joueur, String nomPropriete, Color couleur, int index)
        {
            int i = 0;
            foreach (Tuple<String, Color> p in proprietes[joueur])
            {
                if (index_proprietes[p.Item1] < index)
                    i++;
            }
            proprietes[joueur].Insert(i, Tuple.Create(nomPropriete, couleur));
            index_proprietes[nomPropriete] = index;
            drawPanel_player.Refresh();
        }

        public void removePropriete(String joueur, String nomPropriete)
        {
            for(int i = 0; i < proprietes[joueur].Count; i++)
            {
                if (proprietes[joueur][i].Item1 == nomPropriete)
                    proprietes[joueur].RemoveAt(i);
            }
            drawPanel_player.Refresh();
        }

        // Place un jeton de couleur donne avec un rayon donne a la position donne 
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

        // Place le jeton de la couleur donne sur le plateau à l'index donne 
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
            this.listBox_console.Items.Insert(0, message);

            /*
            // Pour modifier le sens de la console
            this.listBox_console.Items.Add(this.textBox_commande.Text);
            this.listBox_console.SelectedIndex = listBox_console.Items.Count - 1;
            this.listBox_console.SelectedIndex = -1;
            */

        }

        // Gere ce qui est entrée dans la textBox de commande
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

        private void button_out_of_jail_Click(object sender, EventArgs e)
        {
            init_test();
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
            GameManager.GetInstance.clickRollDices();
        }

        // KeyPress event de la textBox des commandes 
        private void textBox_commande_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                handle_commande(textBox_commande.Text);
                textBox_commande.Text = "";
            }
        }

        // Paint event du panel des joueurs 
        private void drawPanel_player_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int posX = drawPanel_player.Location.X - 13;
            int posY = drawPanel_player.Location.Y - 13;
            bool space = true;
            foreach (KeyValuePair<String, List<Tuple<String, Color>>> entry in proprietes)
            {
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(posX, posY, drawPanel_player.Width, PLAYER_HEIGHT));
                g.FillRectangle(new SolidBrush(joueurs[entry.Key]), new Rectangle(posX + 2, posY + 2, drawPanel_player.Width - 4, 16));
                g.DrawString(entry.Key, DefaultFont, new SolidBrush(Color.Black), new PointF(posX + 2, posY + 3));
                foreach (Tuple < String, Color > c in entry.Value)
                {
                    if (space)
                        posY += 0;
                    space = false;
                    g.FillRectangle(new SolidBrush(c.Item2), new Rectangle(posX + 2, posY += PLAYER_HEIGHT, drawPanel_player.Width - 4, PLAYER_HEIGHT));
                    g.DrawString(c.Item1, DefaultFont, new SolidBrush(Color.Black), new PointF(posX + 2, posY + 3));
                }
                space = true;
                posY += 25;
            }
        }

        // Paint event du panel du plateau de jeu
        private void drawPanel_plateau_Paint(object sender, PaintEventArgs e)
        {
            foreach (KeyValuePair<Color, int[]> entry in jetons)
            {
                e.Graphics.FillEllipse(new SolidBrush(entry.Key), entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[2]);
            }
        }

        // SizeChanged event du panel du plateau de jeu
        private void drawPanel_plateau_SizeChanged(object sender, EventArgs e)
        {
            panel_control.Location = new Point(drawPanel_plateau.Location.X + drawPanel_plateau.Size.Width);
            panel_control.Width = (this.ClientSize.Width - drawPanel_plateau.Size.Width) / 2 - 35;
            drawPanel_player.Width = (this.ClientSize.Width - drawPanel_plateau.Size.Width) / 2 - 35;
        }

        // Resize event du form principal
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

        private bool mouseIsOnProperty(MouseEventArgs e)
        {
            bool isOn = false;
            int posY = 0;
            foreach (KeyValuePair<String, List<Tuple<String, Color>>> entry in proprietes)
            {
                posY += PLAYER_HEIGHT;
                if (e.Location.Y > posY && e.Location.Y < posY + PLAYER_HEIGHT * (entry.Value.Count))
                {
                    isOn = true;
                }
                posY += PLAYER_HEIGHT * (entry.Value.Count) + 5;
            }
            return isOn;
        }

        private void drawPanel_player_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (mouseIsOnProperty(e))
            {
                drawPanel_player.Cursor = Cursors.Hand;

            }
            else if(drawPanel_player.Cursor != Cursors.Default)
            {
                drawPanel_player.Cursor = Cursors.Default;
            }
        }

        private void drawPanel_player_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseIsOnProperty(e))
            {
                int posY = 0;
                int temp = 0;
                foreach (KeyValuePair<String, List<Tuple<String, Color>>> entry in proprietes)
                {
                    posY += (entry.Value.Count) * PLAYER_HEIGHT + 25;
                    if (posY < e.Location.Y)
                        temp += 25;
                }
                int index = (e.Location.Y - temp) / 20;
                int i = 0;

                foreach (KeyValuePair<String, List<Tuple<String, Color>>> entry in proprietes)
                {
                    foreach (Tuple<String, Color> c in entry.Value)
                    {
                        i++;
                        if (i == index)
                            insert_console(c.Item1);
                    }
                }
            }
        }
    }
}
