namespace Monopoly
{
    partial class Form_board
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_board));
            this.button_menu = new System.Windows.Forms.Button();
            this.drawPanel_player = new Monopoly.DrawPanel();
            this.panel_control = new Monopoly.DrawPanel();
            this.button_out_of_jail = new System.Windows.Forms.Button();
            this.textBox_commande = new System.Windows.Forms.TextBox();
            this.button_trade = new System.Windows.Forms.Button();
            this.button_end_turn = new System.Windows.Forms.Button();
            this.button_roll_dice = new System.Windows.Forms.Button();
            this.listBox_console = new System.Windows.Forms.ListBox();
            this.drawPanel_plateau = new Monopoly.DrawPanel();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_menu
            // 
            this.button_menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_menu.BackColor = System.Drawing.Color.Transparent;
            this.button_menu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_menu.BackgroundImage")));
            this.button_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_menu.FlatAppearance.BorderSize = 0;
            this.button_menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_menu.Location = new System.Drawing.Point(766, 0);
            this.button_menu.Margin = new System.Windows.Forms.Padding(0);
            this.button_menu.Name = "button_menu";
            this.button_menu.Size = new System.Drawing.Size(18, 16);
            this.button_menu.TabIndex = 10;
            this.button_menu.UseVisualStyleBackColor = false;
            this.button_menu.Click += new System.EventHandler(this.button_menu_Click);
            // 
            // drawPanel_player
            // 
            this.drawPanel_player.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.drawPanel_player.BackColor = System.Drawing.Color.Transparent;
            this.drawPanel_player.Location = new System.Drawing.Point(13, 13);
            this.drawPanel_player.Margin = new System.Windows.Forms.Padding(0);
            this.drawPanel_player.Name = "drawPanel_player";
            this.drawPanel_player.Size = new System.Drawing.Size(146, 414);
            this.drawPanel_player.TabIndex = 9;
            this.drawPanel_player.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_player_Paint);
            this.drawPanel_player.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawPanel_player_MouseClick);
            this.drawPanel_player.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_player_MouseMove);
            // 
            // panel_control
            // 
            this.panel_control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_control.BackColor = System.Drawing.Color.Transparent;
            this.panel_control.Controls.Add(this.button_roll_dice);
            this.panel_control.Controls.Add(this.button_out_of_jail);
            this.panel_control.Controls.Add(this.textBox_commande);
            this.panel_control.Controls.Add(this.button_trade);
            this.panel_control.Controls.Add(this.button_end_turn);
            this.panel_control.Controls.Add(this.listBox_console);
            this.panel_control.Location = new System.Drawing.Point(613, 12);
            this.panel_control.Name = "panel_control";
            this.panel_control.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel_control.Size = new System.Drawing.Size(159, 418);
            this.panel_control.TabIndex = 8;
            // 
            // button_out_of_jail
            // 
            this.button_out_of_jail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_out_of_jail.BackColor = System.Drawing.SystemColors.Control;
            this.button_out_of_jail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_out_of_jail.Location = new System.Drawing.Point(3, 308);
            this.button_out_of_jail.Name = "button_out_of_jail";
            this.button_out_of_jail.Size = new System.Drawing.Size(148, 23);
            this.button_out_of_jail.TabIndex = 5;
            this.button_out_of_jail.Text = "SORTIR DE PRISON";
            this.button_out_of_jail.UseVisualStyleBackColor = false;
            this.button_out_of_jail.Click += new System.EventHandler(this.button_out_of_jail_Click);
            // 
            // textBox_commande
            // 
            this.textBox_commande.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_commande.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_commande.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_commande.Location = new System.Drawing.Point(3, 283);
            this.textBox_commande.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textBox_commande.MinimumSize = new System.Drawing.Size(0, 20);
            this.textBox_commande.Name = "textBox_commande";
            this.textBox_commande.Size = new System.Drawing.Size(148, 20);
            this.textBox_commande.TabIndex = 7;
            this.textBox_commande.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_commande_KeyPress);
            // 
            // button_trade
            // 
            this.button_trade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_trade.BackColor = System.Drawing.SystemColors.Control;
            this.button_trade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_trade.Location = new System.Drawing.Point(3, 337);
            this.button_trade.Name = "button_trade";
            this.button_trade.Size = new System.Drawing.Size(148, 23);
            this.button_trade.TabIndex = 3;
            this.button_trade.Text = "ECHANGES";
            this.button_trade.UseVisualStyleBackColor = false;
            this.button_trade.Click += new System.EventHandler(this.button_trade_Click);
            // 
            // button_end_turn
            // 
            this.button_end_turn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_end_turn.BackColor = System.Drawing.SystemColors.Control;
            this.button_end_turn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_end_turn.Location = new System.Drawing.Point(3, 395);
            this.button_end_turn.Name = "button_end_turn";
            this.button_end_turn.Size = new System.Drawing.Size(148, 23);
            this.button_end_turn.TabIndex = 4;
            this.button_end_turn.Text = "FIN DU TOUR";
            this.button_end_turn.UseVisualStyleBackColor = false;
            this.button_end_turn.Click += new System.EventHandler(this.button_end_turn_Click);
            // 
            // button_roll_dice
            // 
            this.button_roll_dice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_roll_dice.BackColor = System.Drawing.SystemColors.Control;
            this.button_roll_dice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_roll_dice.Location = new System.Drawing.Point(3, 366);
            this.button_roll_dice.Name = "button_roll_dice";
            this.button_roll_dice.Size = new System.Drawing.Size(148, 23);
            this.button_roll_dice.TabIndex = 1;
            this.button_roll_dice.Text = "JETER LES DES";
            this.button_roll_dice.UseVisualStyleBackColor = false;
            this.button_roll_dice.Click += new System.EventHandler(this.button_roll_dice_Click);
            // 
            // listBox_console
            // 
            this.listBox_console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_console.BackColor = System.Drawing.Color.Salmon;
            this.listBox_console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_console.Location = new System.Drawing.Point(3, 13);
            this.listBox_console.Name = "listBox_console";
            this.listBox_console.Size = new System.Drawing.Size(148, 273);
            this.listBox_console.TabIndex = 6;
            // 
            // drawPanel_plateau
            // 
            this.drawPanel_plateau.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("drawPanel_plateau.BackgroundImage")));
            this.drawPanel_plateau.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.drawPanel_plateau.Location = new System.Drawing.Point(165, 0);
            this.drawPanel_plateau.Name = "drawPanel_plateau";
            this.drawPanel_plateau.Size = new System.Drawing.Size(442, 443);
            this.drawPanel_plateau.TabIndex = 0;
            this.drawPanel_plateau.SizeChanged += new System.EventHandler(this.drawPanel_plateau_SizeChanged);
            this.drawPanel_plateau.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_plateau_Paint);
            // 
            // Form_board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.button_menu);
            this.Controls.Add(this.drawPanel_player);
            this.Controls.Add(this.panel_control);
            this.Controls.Add(this.drawPanel_plateau);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form_board";
            this.Text = "MONOPOLY";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.Form_main_Resize);
            this.panel_control.ResumeLayout(false);
            this.panel_control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DrawPanel drawPanel_plateau;
        private System.Windows.Forms.Button button_roll_dice;
        private System.Windows.Forms.Button button_trade;
        private System.Windows.Forms.Button button_end_turn;
        private System.Windows.Forms.Button button_out_of_jail;
        private System.Windows.Forms.ListBox listBox_console;
        private System.Windows.Forms.TextBox textBox_commande;
        private DrawPanel panel_control;
        private DrawPanel drawPanel_player;
        private System.Windows.Forms.Button button_menu;
    }
}

