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
        private static string CURRENCY = "$";
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
            Popup_game_param.GetInstance.ShowDialog(this);
            Popup_game_param.GetInstance.Dispose();
        }

        private void Form_board_Load(object sender, EventArgs e)
        {
            foreach (model.Player player in GameManager.GetInstance.playerManager.getPlayers())
            {
                addJoueur(player.getName(), player.Color);
                jetons[player.Color] = reserveJetons[player.Color];
                setJetonIndex(player.Color, 0);
            }
            GameManager.GetInstance.startGame();
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
        Dictionary<Color, int[]> reserveJetons = new Dictionary<Color, int[]>()
        {
            {Color.DarkOrange, new int[] {0, 0, 20} },
            {Color.MediumTurquoise, new int[] { 0, 0, 20 } },
            {Color.MediumSeaGreen, new int[] { 0, 0, 20 } },
            {Color.Gold, new int[] { 0, 0, 20 } },
            {Color.RoyalBlue, new int[] { 0, 0, 20 } },
            {Color.LightPink, new int[] { 0, 0, 20 } },
            {Color.Beige, new int[] { 0, 0, 20 } },
        };

        // Dictionaires des 6 jetons du jeux qui associe à une couleur de jeton une liste d'entier [coordonee_X, coordonee_Y, rayon]
        Dictionary<Color, int[]> jetons = new Dictionary<Color, int[]>();

        Dictionary<String, int> thune = new Dictionary<String, int>()
        {
        };

        Dictionary<String, Tuple<Color, int>> joueurs = new Dictionary<String, Tuple<Color, int>>()
        {
        };

        Dictionary<String, int> index_proprietes = new Dictionary<String, int>();

        Dictionary<model.Property, int> rent_proprietes = new Dictionary<model.Property, int>();

        Dictionary<String, List<Tuple<String, Color, int, int, int>>> proprietes = new Dictionary<String, List<Tuple<String, Color, int, int, int>>>()
        {
        };
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

        public void setRent(model.Property nomPropritete, int rent)
        {
            rent_proprietes[nomPropritete] = rent;
            drawPanel_player.Refresh();
        }

        public void setThunes(string nomJoueur, int thunes)
        {
            thune[nomJoueur] = thunes;
            drawPanel_player.Refresh();
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
            joueurs.Add(nom, Tuple.Create(color, 0));
            thune.Add(nom, 1500);
            proprietes.Add(nom, new List<Tuple<String, Color, int, int, int>> { });
            drawPanel_player.Refresh();
        }

        public void removeJoueur(String nom)
        {
            joueurs.Remove(nom);
            drawPanel_player.Refresh();
        }

        public void addPropriete(String joueur, model.Property nomPropriete, Color couleur, int index)
        {
            int i = 0;
            foreach (Tuple<String, Color, int, int, int> p in proprietes[joueur])
            {
                if (index_proprietes[p.Item1] < index)
                    i++;
            }
            proprietes[joueur].Insert(i, Tuple.Create(nomPropriete.getName(), couleur, 0, 0, index));
            index_proprietes[nomPropriete.getName()] = index;
            rent_proprietes[nomPropriete] = 100;
            drawPanel_player.Refresh();
        }

        public void addPropriete(String joueur, model.Property nomPropriete, Color couleur, int index, int loyer)
        {
            int i = 0;
            foreach (Tuple<String, Color, int, int, int> p in proprietes[joueur])
            {
                if (index_proprietes[p.Item1] < index)
                    i++;
            }
            proprietes[joueur].Insert(i, Tuple.Create(nomPropriete.getName(), couleur, 0, loyer, index));
            index_proprietes[nomPropriete.getName()] = index;
            rent_proprietes[nomPropriete] = loyer;
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

        }

        private void button_mortage_Click(object sender, EventArgs e)
        {

        }

        private void button_trade_Click(object sender, EventArgs e)
        {

        }

        private void button_end_turn_Click(object sender, EventArgs e)
        {
            GameManager.GetInstance.clickEndTurn();
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
            foreach (KeyValuePair<String, List<Tuple<String, Color, int, int, int>>> entry in proprietes)
            {
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(posX, posY, drawPanel_player.Width, PLAYER_HEIGHT));
                g.FillRectangle(new SolidBrush(joueurs[entry.Key].Item1), new Rectangle(posX + 2, posY + 2, drawPanel_player.Width - 4, 16));
                g.DrawString(entry.Key, new Font(DefaultFont, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(posX + 2, posY + 3));
                g.DrawString(CURRENCY + thune[entry.Key].ToString(), new Font(DefaultFont, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(posX + drawPanel_player.Width - 53, posY + 3));
                foreach (Tuple < String, Color, int, int, int> c in entry.Value)
                {
                    if (space)
                        posY += 0;
                    space = false;
                    g.FillRectangle(new SolidBrush(c.Item2), new Rectangle(posX + 2, posY += PLAYER_HEIGHT, drawPanel_player.Width - 4, PLAYER_HEIGHT));
                    g.DrawString(c.Item1, DefaultFont, new SolidBrush(Color.Black), new PointF(posX + 2, posY + 3));
                    g.DrawString("-" + CURRENCY + controller.GameManager.GetInstance.boardManager.getBoard()[c.Item5].getProperty().getCurrentRent(), DefaultFont, new SolidBrush(Color.Black), new PointF(posX + drawPanel_player.Width - 50, posY + 3));
                    // s'il y a au moins une maison sur la propriété on dessine le nombre de maisons
                    if (c.Item3 > 0)
                    {
                        g.DrawString(c.Item3.ToString(), DefaultFont, new SolidBrush(Color.Black), new PointF(posX + drawPanel_player.Width - 100, posY + 3));
                        g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(posX + drawPanel_player.Width - 86, posY + 10, 10, PLAYER_HEIGHT / 2 - 5 ));
                        g.FillPolygon(new SolidBrush(Color.Black), new PointF[] { new PointF(posX + drawPanel_player.Width - 89, posY + 10), new PointF(posX + drawPanel_player.Width - 81, posY + 2), new PointF(posX + drawPanel_player.Width - 73, posY + 10) });
                    }
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
            foreach (KeyValuePair<String, List<Tuple<String, Color, int, int, int>>> entry in proprietes)
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


        // Affiche le popup de la carte
        public void popupCarte(int index, bool purchase)
        {
            model.Property ppt = GameManager.GetInstance.boardManager.getBoard()[index].getProperty();

            bool owned = (ppt.getOwner() == GameManager.GetInstance.playerManager.getCurrentPlayer());

            Popup_carte popup = null;
            switch (ppt.getType())
            {
                case model.Property.PropType.PRIVATE:
                    popup = new Popup_carte(ppt, "PROPRIETE", ppt.getName(), owned,Color.FromName( ((model.PrivateProperty)ppt).getColor() ), CURRENCY, ppt.getRent()[0], ppt.getRent()[1], ppt.getRent()[2], ppt.getRent()[3], ppt.getRent()[4], ppt.getRent()[5], ((model.PrivateProperty)ppt).getHouseCost(), ppt.getPrice()/2, purchase);
                    popup.ShowDialog(this);
                    popup.Dispose();
                    break;
                case model.Property.PropType.RAILROAD:
                    popup = new Popup_carte(ppt, "GARE", ppt.getName(), owned, CURRENCY, ppt.getRent()[0], ppt.getRent()[1], ppt.getRent()[2], ppt.getRent()[3], new Bitmap(".\\Resources\\train.png"), purchase);
                    popup.ShowDialog(this);
                    popup.Dispose();
                    break;
                case model.Property.PropType.UTILITY:
                    Bitmap btmp;
                    if(index == 12)
                    {
                        btmp = new Bitmap(".\\Resources\\electric.png");
                    }
                    else
                    {
                        btmp = new Bitmap(".\\Resources\\water.png");
                    }
                    popup = new Popup_carte(ppt, "ENTREPRISE", ppt.getName(), owned, btmp, purchase);
                    popup.ShowDialog(this);
                    popup.Dispose();
                    break;
            }            
        }

        public void popupEnchere(int index)
        {
            model.Property ppt = GameManager.GetInstance.boardManager.getBoard()[index].getProperty();

            Popup_carte popup = null;
            switch (ppt.getType())
            {
                case model.Property.PropType.PRIVATE:
                    popup = new Popup_carte(ppt, "PROPRIETE", ppt.getName(), false, Color.FromName(((model.PrivateProperty)ppt).getColor()), CURRENCY, ppt.getRent()[0], ppt.getRent()[1], ppt.getRent()[2], ppt.getRent()[3], ppt.getRent()[4], ppt.getRent()[5], ((model.PrivateProperty)ppt).getHouseCost(), ppt.getPrice() / 2, true);
                    break;
                case model.Property.PropType.RAILROAD:
                    popup = new Popup_carte(ppt, "GARE", ppt.getName(), false, CURRENCY, ppt.getRent()[0], ppt.getRent()[1], ppt.getRent()[2], ppt.getRent()[3], new Bitmap(".\\Resources\\train.png"), true);
                    break;
                case model.Property.PropType.UTILITY:
                    Bitmap btmp;
                    if (index == 12)
                    {
                        btmp = new Bitmap(".\\Resources\\electric.png");
                    }
                    else
                    {
                        btmp = new Bitmap(".\\Resources\\water.png");
                    }
                    popup = new Popup_carte(ppt, "ENTREPRISE", ppt.getName(), false, btmp, true);
                    break;
            }
            Popup_enchere enchere = new Popup_enchere(controller.GameManager.GetInstance.playerManager.getPlayers(), popup);
            enchere.ShowDialog(this);
            popup.Dispose();
            enchere.Dispose();

        }

            // Click sur une propriete dans le panel joueur
            private void drawPanel_player_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseIsOnProperty(e))
            {
                int posY = 0;
                int temp = 0;
                foreach (KeyValuePair<String, List<Tuple<String, Color, int, int, int>>> entry in proprietes)
                {
                    posY += (entry.Value.Count) * PLAYER_HEIGHT + 25;
                    if (posY < e.Location.Y)
                        temp += 25;
                }
                int index = (e.Location.Y - temp) / 20;
                int i = 0;

                foreach (KeyValuePair<String, List<Tuple<String, Color, int ,int, int>>> entry in proprietes)
                {
                    foreach (Tuple<String, Color, int, int, int> c in entry.Value)
                    {
                        i++;
                        if (i == index)
                            popupCarte(c.Item5, false);
                    }
                }
            }
        }

        private void button_menu_Click(object sender, EventArgs e)
        {

        }


    }
}
