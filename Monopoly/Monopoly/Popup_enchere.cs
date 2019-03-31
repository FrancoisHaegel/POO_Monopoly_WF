using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public partial class Popup_enchere : Form
    {
        private List<Tuple<String, int>> offres = new List < Tuple<String, int> >();
        private List<String> joueurs;
        private int indexEnchere = 0;
        Popup_carte popupCarte;
        public Popup_enchere(List<String> j, Popup_carte carte)
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
                String winner = offres[0].Item1; 
                foreach (Tuple<String, int> entry in offres)
                {
                    if(entry.Item2 > max)
                    {
                        max = entry.Item2;
                        winner = entry.Item1;
                    }
                }
                MessageBox.Show(winner + " remporte l'enchère pour $ " + max.ToString());
                this.Close();
                popupCarte.Close();
            }
            else
            {
                this.label_joueur.Text = joueurs[indexEnchere++];
            }
        }

        private void button_encherir_Click(object sender, EventArgs e)
        {
            int offre = 0;
            Int32.TryParse(this.textBox_offre.Text, out offre);
            offres.Add(Tuple.Create(joueurs[indexEnchere-1], offre));
            nextIndex();
        }

        private void button_pas_encherir_Click(object sender, EventArgs e)
        {
            offres.Add(Tuple.Create(joueurs[indexEnchere - 1], 0));
            nextIndex();
        }
    }
}
