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
    public partial class Popup_carte : Form
    {


        private model.Property property;

        public Popup_carte()
        {
            InitializeComponent();
        }

        // Constructeur de carte propriété
        public Popup_carte(model.Property p,  string titre, string nom, bool owned, Color color, String currency, int prix_1, int prix_2, int prix_3, int prix_4, int prix_5, int prix_6, int prix_7, int prix_8, bool achat)
        {
            property = p;
            InitializeComponent();
            label1.Text = titre;
            label2.Text = nom;
            label1.BackColor = color;
            label2.BackColor = color;
            pictureBox_color.BackColor = color;
            label11.Text = currency + prix_7.ToString();
            label12.Text = currency + prix_6.ToString();
            label13.Text = currency + prix_5.ToString();
            label14.Text = currency + prix_4.ToString();
            label15.Text = currency + prix_3.ToString();
            label16.Text = currency + prix_2.ToString();
            label18.Text = currency + prix_1.ToString();


            if (owned)
            {
                if (p.getMortgaged())
                {
                    button_hypotheque.Text = "Racheter hypotèque: +" + currency + (prix_8 * 1.1).ToString();
                }
                else
                {
                    button_hypotheque.Text = "Hypotéquer: +" + currency + prix_8.ToString();
                }
                button_achat_maison.Text = "Acheter une maison: -" + currency + prix_7.ToString();
                button_vente_maison.Visible = true;
                button_vente_maison.Text = "Vendre une maison: +" + currency + (prix_7 / 2).ToString();
            }
            else
            {
                if (achat)
                {
                    button1.Visible = true;
                    button_close.Visible = false;
                    button_acheter.Visible = true;
                }
                button_achat_maison.Visible = false;
                button_hypotheque.Visible = false;
                this.Height -= 60;
            }
        }

        // Constructeur de carte gare
        public Popup_carte(model.Property p, string titre, string nom, bool owned, String currency, int prix_1, int prix_2, int prix_3, int prix_4, Image logo, bool achat)
        {
            property = p;
            InitializeComponent();

            label1.Text = titre;
            label2.Text = nom;

            label6.Text = "Loyer avec 1 gare";
            label7.Text = "Loyer avec 2 gare";
            label8.Text = "Loyer avec 3 gare";
            label9.Text = "Loyer avec 4 gare";

            label15.Text = currency + prix_1.ToString();
            label14.Text = currency + prix_2.ToString();
            label13.Text = currency + prix_3.ToString();
            label12.Text = currency + prix_4.ToString();

            // Cacher les label en trop
            label3.Visible = false;
            label5.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label16.Visible = false;
            label18.Visible = false;
            button_achat_maison.Visible = false;

            if (!owned)
            {
                if (achat)
                {
                    button_acheter.Visible = true;
                    button1.Visible = true;
                    button_close.Visible = false;
                }
                button_hypotheque.Visible = false;
                this.Height -= 40;
            }
            else
            {
                if (p.getMortgaged())
                {
                    button_hypotheque.Text = "Racheter hypotèque: +" + currency + 60;
                }
                else
                {
                    button_hypotheque.Text = "Hypotéquer: +" + currency + 50;
                }
            }

            // Retrecir la carte 
            this.Height -= 55;

            // Mettre l'image de fond
            pictureBox1.BackgroundImage = logo;
            pictureBox1.Visible = true;
        }

        // Constructeur de carte entreprise
        public Popup_carte(model.Property p, string titre, string nom, bool owned, Image logo, bool achat)
        {
            property = p;
            InitializeComponent();

            label1.Text = titre;
            label2.Text = nom;

            label6.Text = "Si 1 entreprise de service est possédé, le loyer est 4 fois le montant des dés.";
            label9.Text = "Si 2 entreprises de service sont possédés, le loyer est 10 fois le montant des dés.";

            // Cacher les label en trop
            label3.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label18.Visible = false;
            button_achat_maison.Visible = false;

            if (!owned)
            {
                if (achat)
                {
                    button_acheter.Visible = true;
                    button1.Visible = true;
                    button_close.Visible = false;
                }
                button_hypotheque.Visible = false;
                this.Height -= 35;
            }
            else
            {
                if (p.getMortgaged())
                {
                    button_hypotheque.Text = "Racheter hypotèque: +" + "$" + 60;
                }
                else
                {
                    button_hypotheque.Text = "Hypotéquer: +" + "$" + 50;
                }
            }
            // Retrecir la carte 
            this.Height -= 55;

            // Mettre l'image de fond
            pictureBox1.BackgroundImage = logo;
            pictureBox1.Visible = true;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        private void button_acheter_Click(object sender, EventArgs e)
        {
            controller.GameManager.GetInstance.purchase(controller.GameManager.GetInstance.playerManager.getCurrentPlayer(), controller.GameManager.GetInstance.playerManager.getCurrentPlayer().getLocation().getProperty(), true);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller.GameManager.GetInstance.purchase(controller.GameManager.GetInstance.playerManager.getCurrentPlayer(), controller.GameManager.GetInstance.playerManager.getCurrentPlayer().getLocation().getProperty(), false);
            this.Close();
        }

        private void button_achat_maison_Click(object sender, EventArgs e)
        {
            controller.GameManager.GetInstance.buyHouse(controller.GameManager.GetInstance.playerManager.getCurrentPlayer(), (model.PrivateProperty)property);
        }

        private void button_vente_maison_Click(object sender, EventArgs e)
        {
            controller.GameManager.GetInstance.sellHouse(controller.GameManager.GetInstance.playerManager.getCurrentPlayer(), (model.PrivateProperty)property);
        }

        private void button_hypotheque_Click(object sender, EventArgs e)
        {
            if(property.getMortgaged())
                controller.GameManager.GetInstance.askUnMortgage(controller.GameManager.GetInstance.playerManager.getCurrentPlayer(), (model.PrivateProperty)property);
            else
                controller.GameManager.GetInstance.askMortgage(controller.GameManager.GetInstance.playerManager.getCurrentPlayer(), (model.PrivateProperty)property);
        }

    }
}
