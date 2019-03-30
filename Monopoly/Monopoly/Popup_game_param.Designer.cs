namespace Monopoly
{
    partial class Popup_game_param
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Popup_game_param));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_nb_joueur = new System.Windows.Forms.ComboBox();
            this.button_jouer = new System.Windows.Forms.Button();
            this.button_quitter = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_joueur_1 = new System.Windows.Forms.Label();
            this.textBox_joueur_1 = new System.Windows.Forms.TextBox();
            this.textBox_joueur_2 = new System.Windows.Forms.TextBox();
            this.label_joueur_2 = new System.Windows.Forms.Label();
            this.textBox_joueur_3 = new System.Windows.Forms.TextBox();
            this.label_joueur_3 = new System.Windows.Forms.Label();
            this.textBox_joueur_4 = new System.Windows.Forms.TextBox();
            this.label_joueur_4 = new System.Windows.Forms.Label();
            this.textBox_joueur_5 = new System.Windows.Forms.TextBox();
            this.label_joueur_5 = new System.Windows.Forms.Label();
            this.textBox_joueur_6 = new System.Windows.Forms.TextBox();
            this.label_joueur_6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de joueurs";
            // 
            // comboBox_nb_joueur
            // 
            this.comboBox_nb_joueur.FormattingEnabled = true;
            this.comboBox_nb_joueur.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBox_nb_joueur.Location = new System.Drawing.Point(111, 198);
            this.comboBox_nb_joueur.Name = "comboBox_nb_joueur";
            this.comboBox_nb_joueur.Size = new System.Drawing.Size(97, 21);
            this.comboBox_nb_joueur.TabIndex = 1;
            this.comboBox_nb_joueur.SelectedValueChanged += new System.EventHandler(this.comboBox_nb_joueur_SelectedValueChanged);
            // 
            // button_jouer
            // 
            this.button_jouer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_jouer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_jouer.Location = new System.Drawing.Point(562, 325);
            this.button_jouer.Name = "button_jouer";
            this.button_jouer.Size = new System.Drawing.Size(88, 53);
            this.button_jouer.TabIndex = 2;
            this.button_jouer.Text = "Jouer";
            this.button_jouer.UseVisualStyleBackColor = true;
            this.button_jouer.Click += new System.EventHandler(this.button_jouer_Click);
            // 
            // button_quitter
            // 
            this.button_quitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_quitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_quitter.Location = new System.Drawing.Point(470, 325);
            this.button_quitter.Name = "button_quitter";
            this.button_quitter.Size = new System.Drawing.Size(86, 53);
            this.button_quitter.TabIndex = 3;
            this.button_quitter.Text = "Quitter";
            this.button_quitter.UseVisualStyleBackColor = true;
            this.button_quitter.Click += new System.EventHandler(this.button_quitter_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(647, 173);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label_joueur_1
            // 
            this.label_joueur_1.AutoSize = true;
            this.label_joueur_1.Location = new System.Drawing.Point(13, 231);
            this.label_joueur_1.Name = "label_joueur_1";
            this.label_joueur_1.Size = new System.Drawing.Size(48, 13);
            this.label_joueur_1.TabIndex = 5;
            this.label_joueur_1.Text = "Joueur 1";
            // 
            // textBox_joueur_1
            // 
            this.textBox_joueur_1.Location = new System.Drawing.Point(67, 228);
            this.textBox_joueur_1.MaxLength = 24;
            this.textBox_joueur_1.Name = "textBox_joueur_1";
            this.textBox_joueur_1.Size = new System.Drawing.Size(141, 20);
            this.textBox_joueur_1.TabIndex = 6;
            // 
            // textBox_joueur_2
            // 
            this.textBox_joueur_2.Location = new System.Drawing.Point(67, 254);
            this.textBox_joueur_2.MaxLength = 24;
            this.textBox_joueur_2.Name = "textBox_joueur_2";
            this.textBox_joueur_2.Size = new System.Drawing.Size(141, 20);
            this.textBox_joueur_2.TabIndex = 8;
            // 
            // label_joueur_2
            // 
            this.label_joueur_2.AutoSize = true;
            this.label_joueur_2.Location = new System.Drawing.Point(13, 257);
            this.label_joueur_2.Name = "label_joueur_2";
            this.label_joueur_2.Size = new System.Drawing.Size(48, 13);
            this.label_joueur_2.TabIndex = 7;
            this.label_joueur_2.Text = "Joueur 2";
            // 
            // textBox_joueur_3
            // 
            this.textBox_joueur_3.Enabled = false;
            this.textBox_joueur_3.Location = new System.Drawing.Point(67, 280);
            this.textBox_joueur_3.MaxLength = 24;
            this.textBox_joueur_3.Name = "textBox_joueur_3";
            this.textBox_joueur_3.Size = new System.Drawing.Size(141, 20);
            this.textBox_joueur_3.TabIndex = 10;
            // 
            // label_joueur_3
            // 
            this.label_joueur_3.AutoSize = true;
            this.label_joueur_3.Enabled = false;
            this.label_joueur_3.Location = new System.Drawing.Point(13, 283);
            this.label_joueur_3.Name = "label_joueur_3";
            this.label_joueur_3.Size = new System.Drawing.Size(48, 13);
            this.label_joueur_3.TabIndex = 9;
            this.label_joueur_3.Text = "Joueur 3";
            // 
            // textBox_joueur_4
            // 
            this.textBox_joueur_4.Enabled = false;
            this.textBox_joueur_4.Location = new System.Drawing.Point(67, 306);
            this.textBox_joueur_4.MaxLength = 24;
            this.textBox_joueur_4.Name = "textBox_joueur_4";
            this.textBox_joueur_4.Size = new System.Drawing.Size(141, 20);
            this.textBox_joueur_4.TabIndex = 12;
            // 
            // label_joueur_4
            // 
            this.label_joueur_4.AutoSize = true;
            this.label_joueur_4.Enabled = false;
            this.label_joueur_4.Location = new System.Drawing.Point(13, 309);
            this.label_joueur_4.Name = "label_joueur_4";
            this.label_joueur_4.Size = new System.Drawing.Size(48, 13);
            this.label_joueur_4.TabIndex = 11;
            this.label_joueur_4.Text = "Joueur 4";
            // 
            // textBox_joueur_5
            // 
            this.textBox_joueur_5.Enabled = false;
            this.textBox_joueur_5.Location = new System.Drawing.Point(67, 332);
            this.textBox_joueur_5.MaxLength = 24;
            this.textBox_joueur_5.Name = "textBox_joueur_5";
            this.textBox_joueur_5.Size = new System.Drawing.Size(141, 20);
            this.textBox_joueur_5.TabIndex = 14;
            // 
            // label_joueur_5
            // 
            this.label_joueur_5.AutoSize = true;
            this.label_joueur_5.Enabled = false;
            this.label_joueur_5.Location = new System.Drawing.Point(13, 335);
            this.label_joueur_5.Name = "label_joueur_5";
            this.label_joueur_5.Size = new System.Drawing.Size(48, 13);
            this.label_joueur_5.TabIndex = 13;
            this.label_joueur_5.Text = "Joueur 5";
            // 
            // textBox_joueur_6
            // 
            this.textBox_joueur_6.Enabled = false;
            this.textBox_joueur_6.Location = new System.Drawing.Point(67, 358);
            this.textBox_joueur_6.MaxLength = 24;
            this.textBox_joueur_6.Name = "textBox_joueur_6";
            this.textBox_joueur_6.Size = new System.Drawing.Size(141, 20);
            this.textBox_joueur_6.TabIndex = 16;
            // 
            // label_joueur_6
            // 
            this.label_joueur_6.AutoSize = true;
            this.label_joueur_6.Enabled = false;
            this.label_joueur_6.Location = new System.Drawing.Point(13, 361);
            this.label_joueur_6.Name = "label_joueur_6";
            this.label_joueur_6.Size = new System.Drawing.Size(48, 13);
            this.label_joueur_6.TabIndex = 15;
            this.label_joueur_6.Text = "Joueur 6";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(214, 228);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.DropDown += new System.EventHandler(this.openDropdown);
            this.comboBox1.DropDownClosed += new System.EventHandler(this.closeDropdown);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Location = new System.Drawing.Point(214, 253);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 18;
            this.comboBox2.DropDown += new System.EventHandler(this.openDropdown);
            this.comboBox2.DropDownClosed += new System.EventHandler(this.closeDropdown);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Enabled = false;
            this.comboBox3.Location = new System.Drawing.Point(214, 279);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 19;
            this.comboBox3.DropDown += new System.EventHandler(this.openDropdown);
            this.comboBox3.DropDownClosed += new System.EventHandler(this.closeDropdown);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Enabled = false;
            this.comboBox4.Location = new System.Drawing.Point(214, 305);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 20;
            this.comboBox4.DropDown += new System.EventHandler(this.openDropdown);
            this.comboBox4.DropDownClosed += new System.EventHandler(this.closeDropdown);
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.Enabled = false;
            this.comboBox5.Location = new System.Drawing.Point(214, 331);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 21);
            this.comboBox5.TabIndex = 21;
            this.comboBox5.DropDown += new System.EventHandler(this.openDropdown);
            this.comboBox5.DropDownClosed += new System.EventHandler(this.closeDropdown);
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.Enabled = false;
            this.comboBox6.Location = new System.Drawing.Point(214, 357);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 22;
            this.comboBox6.DropDown += new System.EventHandler(this.openDropdown);
            this.comboBox6.DropDownClosed += new System.EventHandler(this.closeDropdown);
            // 
            // Popup_game_param
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 392);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox_joueur_6);
            this.Controls.Add(this.label_joueur_6);
            this.Controls.Add(this.textBox_joueur_5);
            this.Controls.Add(this.label_joueur_5);
            this.Controls.Add(this.textBox_joueur_4);
            this.Controls.Add(this.label_joueur_4);
            this.Controls.Add(this.textBox_joueur_3);
            this.Controls.Add(this.label_joueur_3);
            this.Controls.Add(this.textBox_joueur_2);
            this.Controls.Add(this.label_joueur_2);
            this.Controls.Add(this.textBox_joueur_1);
            this.Controls.Add(this.label_joueur_1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_quitter);
            this.Controls.Add(this.button_jouer);
            this.Controls.Add(this.comboBox_nb_joueur);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Popup_game_param";
            this.Text = "Popup_game_param";
            this.Load += new System.EventHandler(this.Popup_game_param_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_nb_joueur;
        private System.Windows.Forms.Button button_jouer;
        private System.Windows.Forms.Button button_quitter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_joueur_1;
        private System.Windows.Forms.TextBox textBox_joueur_1;
        private System.Windows.Forms.TextBox textBox_joueur_2;
        private System.Windows.Forms.Label label_joueur_2;
        private System.Windows.Forms.TextBox textBox_joueur_3;
        private System.Windows.Forms.Label label_joueur_3;
        private System.Windows.Forms.TextBox textBox_joueur_4;
        private System.Windows.Forms.Label label_joueur_4;
        private System.Windows.Forms.TextBox textBox_joueur_5;
        private System.Windows.Forms.Label label_joueur_5;
        private System.Windows.Forms.TextBox textBox_joueur_6;
        private System.Windows.Forms.Label label_joueur_6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox6;
    }
}