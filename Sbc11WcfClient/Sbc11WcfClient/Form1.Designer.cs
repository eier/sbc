﻿namespace Sbc11WcfClient
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lblUnbemalteEier = new System.Windows.Forms.Label();
            this.lblBemalteEier = new System.Windows.Forms.Label();
            this.lblSchokoHasen = new System.Windows.Forms.Label();
            this.lblNester = new System.Windows.Forms.Label();
            this.lblAusgeliefert = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Eier produzieren";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(142, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "3";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(142, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "3";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(248, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Schoko produzieren";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(367, 12);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(273, 291);
            this.textBox3.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Henne1",
            "Henne2",
            "Henne3"});
            this.comboBox1.Location = new System.Drawing.Point(15, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Henne1";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Hase1",
            "Hase2",
            "Hase3"});
            this.comboBox2.Location = new System.Drawing.Point(15, 48);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.Text = "Hase1";
            // 
            // lblUnbemalteEier
            // 
            this.lblUnbemalteEier.AutoSize = true;
            this.lblUnbemalteEier.Location = new System.Drawing.Point(248, 118);
            this.lblUnbemalteEier.Name = "lblUnbemalteEier";
            this.lblUnbemalteEier.Size = new System.Drawing.Size(13, 13);
            this.lblUnbemalteEier.TabIndex = 9;
            this.lblUnbemalteEier.Text = "0";
            // 
            // lblBemalteEier
            // 
            this.lblBemalteEier.AutoSize = true;
            this.lblBemalteEier.Location = new System.Drawing.Point(248, 151);
            this.lblBemalteEier.Name = "lblBemalteEier";
            this.lblBemalteEier.Size = new System.Drawing.Size(13, 13);
            this.lblBemalteEier.TabIndex = 10;
            this.lblBemalteEier.Text = "0";
            // 
            // lblSchokoHasen
            // 
            this.lblSchokoHasen.AutoSize = true;
            this.lblSchokoHasen.Location = new System.Drawing.Point(248, 185);
            this.lblSchokoHasen.Name = "lblSchokoHasen";
            this.lblSchokoHasen.Size = new System.Drawing.Size(13, 13);
            this.lblSchokoHasen.TabIndex = 11;
            this.lblSchokoHasen.Text = "0";
            // 
            // lblNester
            // 
            this.lblNester.AutoSize = true;
            this.lblNester.Location = new System.Drawing.Point(248, 217);
            this.lblNester.Name = "lblNester";
            this.lblNester.Size = new System.Drawing.Size(13, 13);
            this.lblNester.TabIndex = 12;
            this.lblNester.Text = "0";
            // 
            // lblAusgeliefert
            // 
            this.lblAusgeliefert.AutoSize = true;
            this.lblAusgeliefert.Location = new System.Drawing.Point(248, 245);
            this.lblAusgeliefert.Name = "lblAusgeliefert";
            this.lblAusgeliefert.Size = new System.Drawing.Size(13, 13);
            this.lblAusgeliefert.TabIndex = 13;
            this.lblAusgeliefert.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Unbemalte Eier";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Bemalte Eier";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "SchokoHasen";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Nester";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(100, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Ausgeliefert";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 315);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAusgeliefert);
            this.Controls.Add(this.lblNester);
            this.Controls.Add(this.lblSchokoHasen);
            this.Controls.Add(this.lblBemalteEier);
            this.Controls.Add(this.lblUnbemalteEier);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblUnbemalteEier;
        private System.Windows.Forms.Label lblBemalteEier;
        private System.Windows.Forms.Label lblSchokoHasen;
        private System.Windows.Forms.Label lblNester;
        private System.Windows.Forms.Label lblAusgeliefert;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

