using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Monopoly.model;

namespace Monopoly
{
    public partial class Popup_enchere : Form
    {
        private List<Tuple<Player, int>> offres = new List < Tuple<Player, int> >();
        private List<Player> joueurs;
        private int indexEnchere = 0;
        private Popup_carte popupCarte;

        public Popup_enchere(List<Player> j, Popup_carte carte)
        {
            carte.Show(this);
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            //MessageBox.Show(carte.Location.Y.ToString());
            this.Location = new Point(carte.Location.X + carte.Width, carte.Location.Y);

            popupCarte = carte;

            joueurs = j;

            nextIndex();
        }

        // Change de tour d'enchère 
        private void nextIndex()
        {
            // Si on est passé par tout les joueurs 
            if(indexEnchere + 1 > joueurs.Count)
            {
                int max = 0;
                Player winner = offres[0].Item1; 
                foreach (Tuple<Player, int> entry in offres)
                {
                    if(entry.Item2 > max)
                    {
                        max = entry.Item2;
                        winner = entry.Item1;
                    }
                }
                MessageBox.Show(winner.getName() + " remporte l'enchère pour $ " + max.ToString());
                controller.GameManager.GetInstance.playerManager.giveProperty(winner, controller.GameManager.GetInstance.playerManager.getCurrentPlayer().getLocation().getProperty());
                this.Close();
                popupCarte.Close();
            }
            else
            {
                this.label_joueur.Text = joueurs[indexEnchere++].getName();
            }
        }

        private void button_encherir_Click(object sender, EventArgs e)
        {
            int offre = 0;
            Int32.TryParse(this.textBox_offre.Text, out offre);
            offres.Add(Tuple.Create(joueurs[indexEnchere-1], offre));
            this.textBox_offre.Text = "";
            nextIndex();
        }

        private void button_pas_encherir_Click(object sender, EventArgs e)
        {
            offres.Add(Tuple.Create(joueurs[indexEnchere - 1], 0));
            nextIndex();
        }
    }
}
