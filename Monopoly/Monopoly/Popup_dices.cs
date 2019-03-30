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
    public partial class Popup_dices : Form
    {

        public Popup_dices()
        {
            InitializeComponent();
        }

        public Popup_dices(Image dice_A, Image dice_B)
        {
            InitializeComponent();
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            this.pictureBox_dice_A.BackgroundImage = dice_A;
            this.pictureBox_dice_B.BackgroundImage = dice_B;
        }

        private void timer_fade_Tick(object sender, EventArgs e)
        {
            if(this.Opacity > 0.0)
            {
                this.Opacity -= 0.04;
            }
            else
            {
                this.Close();
            }
        }

        private void Popup_dices_Load(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
            timer_fade.Start();
        }
    }
}
