namespace Monopoly
{
    partial class Popup_dices
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox_dice_A = new System.Windows.Forms.PictureBox();
            this.pictureBox_dice_B = new System.Windows.Forms.PictureBox();
            this.timer_fade = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dice_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dice_B)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_dice_A
            // 
            this.pictureBox_dice_A.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_dice_A.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_dice_A.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_dice_A.Name = "pictureBox_dice_A";
            this.pictureBox_dice_A.Size = new System.Drawing.Size(300, 300);
            this.pictureBox_dice_A.TabIndex = 0;
            this.pictureBox_dice_A.TabStop = false;
            // 
            // pictureBox_dice_B
            // 
            this.pictureBox_dice_B.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_dice_B.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_dice_B.Location = new System.Drawing.Point(318, 12);
            this.pictureBox_dice_B.Name = "pictureBox_dice_B";
            this.pictureBox_dice_B.Size = new System.Drawing.Size(300, 300);
            this.pictureBox_dice_B.TabIndex = 1;
            this.pictureBox_dice_B.TabStop = false;
            // 
            // timer_fade
            // 
            this.timer_fade.Tick += new System.EventHandler(this.timer_fade_Tick);
            // 
            // Popup_dices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(631, 326);
            this.Controls.Add(this.pictureBox_dice_B);
            this.Controls.Add(this.pictureBox_dice_A);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Popup_dices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popup_dices";
            this.Load += new System.EventHandler(this.Popup_dices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dice_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dice_B)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_dice_A;
        private System.Windows.Forms.PictureBox pictureBox_dice_B;
        private System.Windows.Forms.Timer timer_fade;
    }
}