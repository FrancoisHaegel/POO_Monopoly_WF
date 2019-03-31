namespace Monopoly
{
    partial class Popup_enchere
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
            this.button_encherir = new System.Windows.Forms.Button();
            this.button_pas_encherir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_joueur = new System.Windows.Forms.Label();
            this.label_offre = new System.Windows.Forms.Label();
            this.textBox_offre = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_encherir
            // 
            this.button_encherir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_encherir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_encherir.Location = new System.Drawing.Point(182, 200);
            this.button_encherir.Name = "button_encherir";
            this.button_encherir.Size = new System.Drawing.Size(111, 46);
            this.button_encherir.TabIndex = 0;
            this.button_encherir.Text = "ENCHERIR";
            this.button_encherir.UseVisualStyleBackColor = true;
            this.button_encherir.Click += new System.EventHandler(this.button_encherir_Click);
            // 
            // button_pas_encherir
            // 
            this.button_pas_encherir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_pas_encherir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_pas_encherir.Location = new System.Drawing.Point(12, 200);
            this.button_pas_encherir.Name = "button_pas_encherir";
            this.button_pas_encherir.Size = new System.Drawing.Size(162, 46);
            this.button_pas_encherir.TabIndex = 1;
            this.button_pas_encherir.Text = "NE PAS ENCHERIR";
            this.button_pas_encherir.UseVisualStyleBackColor = true;
            this.button_pas_encherir.Click += new System.EventHandler(this.button_pas_encherir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "ENCHERE";
            // 
            // label_joueur
            // 
            this.label_joueur.AutoSize = true;
            this.label_joueur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_joueur.Location = new System.Drawing.Point(24, 50);
            this.label_joueur.Name = "label_joueur";
            this.label_joueur.Size = new System.Drawing.Size(95, 20);
            this.label_joueur.TabIndex = 3;
            this.label_joueur.Text = "label_joueur";
            // 
            // label_offre
            // 
            this.label_offre.AutoSize = true;
            this.label_offre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_offre.Location = new System.Drawing.Point(25, 87);
            this.label_offre.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label_offre.Name = "label_offre";
            this.label_offre.Size = new System.Drawing.Size(49, 16);
            this.label_offre.TabIndex = 4;
            this.label_offre.Text = "Offre: $";
            // 
            // textBox_offre
            // 
            this.textBox_offre.Location = new System.Drawing.Point(74, 86);
            this.textBox_offre.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textBox_offre.Name = "textBox_offre";
            this.textBox_offre.Size = new System.Drawing.Size(100, 20);
            this.textBox_offre.TabIndex = 5;
            // 
            // Popup_enchere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 258);
            this.Controls.Add(this.textBox_offre);
            this.Controls.Add(this.label_offre);
            this.Controls.Add(this.label_joueur);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_pas_encherir);
            this.Controls.Add(this.button_encherir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Popup_enchere";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Popup_enchere";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_encherir;
        private System.Windows.Forms.Button button_pas_encherir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_joueur;
        private System.Windows.Forms.Label label_offre;
        private System.Windows.Forms.TextBox textBox_offre;
    }
}