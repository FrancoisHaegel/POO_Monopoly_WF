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
    public partial class Popup_game_param : Form
    {
        private static Popup_game_param instance;
        public static Popup_game_param GetInstance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new Popup_game_param();
                }
                return instance;
            }
        }

        public Popup_game_param()
        {
            InitializeComponent();
        }

        Dictionary<String, Color> couleurs = new Dictionary<string, Color>()
        {
            {"Orange", Color.DarkOrange },
            {"Turquoise", Color.MediumTurquoise },
            {"Vert", Color.MediumSeaGreen },
            {"Jaune", Color.Gold },
            {"Bleu", Color.RoyalBlue },
            {"Rose", Color.LightPink },
            {"Noir", Color.Black}
        };

        List<ComboBox> comboBoxes = new List<ComboBox>();

        List<Control> disabled = new List<Control>();

        private void Popup_game_param_Load(object sender, EventArgs e)
        {
            this.comboBox_nb_joueur.SelectedIndex = 0;
            comboBoxes.Add(comboBox1);
            comboBoxes.Add(comboBox2);
            comboBoxes.Add(comboBox3);
            comboBoxes.Add(comboBox4);
            comboBoxes.Add(comboBox5);
            comboBoxes.Add(comboBox6);

            disabled.Add(label_joueur_3);
            disabled.Add(textBox_joueur_3);
            disabled.Add(comboBox3);
            disabled.Add(label_joueur_4);
            disabled.Add(textBox_joueur_4);
            disabled.Add(comboBox4);
            disabled.Add(label_joueur_5);
            disabled.Add(textBox_joueur_5);
            disabled.Add(comboBox5);
            disabled.Add(label_joueur_6);
            disabled.Add(textBox_joueur_6);
            disabled.Add(comboBox6);
        }

        private void button_quitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_jouer_Click(object sender, EventArgs e)
        {
            Dictionary<string, Color> joueurs = new Dictionary<string, Color>();

            joueurs[textBox_joueur_1.Text] = couleurs[comboBox1.SelectedItem.ToString()];
            joueurs[textBox_joueur_2.Text] = couleurs[comboBox2.SelectedItem.ToString()];
            for (int i = 0; i <= disabled.Count; i++)
            {
                if (i < (comboBox_nb_joueur.SelectedIndex) * 3)
                {
                    if (i % 3 == 1)
                         joueurs[disabled[i].Text] = couleurs[disabled[i + 1].Text];
                }
            }
            controller.GameManager.GetInstance.Init(joueurs);
            this.Close();
        }

        private void closeDropdown(object sender, EventArgs e)
        {
            object temp = ((ComboBox)sender).SelectedItem;
            ((ComboBox)sender).Items.Clear();
            if (temp != null)
            {
                ((ComboBox)sender).Items.Add(temp);
                ((ComboBox)sender).SelectedIndex = 0;
            }
        }

        private void openDropdown(object sender, EventArgs e)
        {
            foreach (KeyValuePair<String, Color> c in couleurs)
            {
                bool draw = true;
                foreach (ComboBox cb in comboBoxes)
                {
                    if (cb.SelectedIndex != -1)
                    {
                        if (couleurs[cb.SelectedItem.ToString()] == c.Value)
                            draw = false;
                    }
                }
                if (draw)
                    ((ComboBox)sender).Items.Add(c.Key);
            }
        }

        private void comboBox_nb_joueur_SelectedValueChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < disabled.Count; i++)
            {
                if(i < (comboBox_nb_joueur.SelectedIndex) * 3)
                {
                    disabled[i].Enabled = true;
                }
                else
                {
                    disabled[i].Enabled = false;
                }
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            bool enabled = true;

            if (string.IsNullOrWhiteSpace(textBox_joueur_1.Text))
                enabled = false;

            if (string.IsNullOrWhiteSpace(textBox_joueur_2.Text))
                enabled = false;

            if (comboBox1.SelectedIndex < 0)
                enabled = false;

            if (comboBox2.SelectedIndex < 0)
                enabled = false;

            for (int i = 0; i <= disabled.Count; i++)
            {
                if (i < (comboBox_nb_joueur.SelectedIndex) * 3)
                {
                    if (i % 3 == 1)
                        if (string.IsNullOrWhiteSpace(((TextBox)disabled[i]).Text))
                            enabled = false;
                    if (i % 3 == 2)
                        if (((ComboBox)disabled[i]).SelectedIndex < 0)
                            enabled = false;

                }
            }

            button_jouer.Enabled = enabled;
        }
    }
}
