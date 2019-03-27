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
    public partial class Form_actions : Form
    {
        private static Form_actions instance;
        public Form_actions()
        {
            InitializeComponent();
        }

        public static Form_actions GetInstance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new Form_actions();
                }
                return instance;
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
            if(commande == "q")
            {
                this.Close();
            }else if(int.TryParse(commande, out i))
            {
                Form_board.GetInstance.setJetonIndex(Color.Gold, i);
            }
            insert_console(commande);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                handle_commande(textBox_commande.Text);
                textBox_commande.Text = "";
            }
        }
    }
}
