namespace Monopoly
{
    partial class Form_actions
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
            this.textBox_commande = new System.Windows.Forms.TextBox();
            this.listBox_console = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBox_commande
            // 
            this.textBox_commande.Location = new System.Drawing.Point(12, 180);
            this.textBox_commande.Name = "textBox_commande";
            this.textBox_commande.Size = new System.Drawing.Size(337, 20);
            this.textBox_commande.TabIndex = 0;
            this.textBox_commande.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // listBox_console
            // 
            this.listBox_console.BackColor = System.Drawing.SystemColors.Control;
            this.listBox_console.FormattingEnabled = true;
            this.listBox_console.Location = new System.Drawing.Point(12, 12);
            this.listBox_console.Name = "listBox_console";
            this.listBox_console.Size = new System.Drawing.Size(337, 160);
            this.listBox_console.TabIndex = 1;
            // 
            // Form_actions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 450);
            this.Controls.Add(this.listBox_console);
            this.Controls.Add(this.textBox_commande);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form_actions";
            this.Text = "Form_actions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_commande;
        private System.Windows.Forms.ListBox listBox_console;
    }
}