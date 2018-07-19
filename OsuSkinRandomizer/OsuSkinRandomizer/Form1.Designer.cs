namespace OsuSkinRandomizer
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chk_interface = new System.Windows.Forms.CheckBox();
            this.chk_Catch = new System.Windows.Forms.CheckBox();
            this.chk_Mania = new System.Windows.Forms.CheckBox();
            this.chk_Sounds = new System.Windows.Forms.CheckBox();
            this.chk_standard = new System.Windows.Forms.CheckBox();
            this.chk_taiko = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(136, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "<your osu skin folder here>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your osu /skins/ folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name of your creation:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(136, 43);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(283, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "RandomOsuSkin";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_interface
            // 
            this.chk_interface.AutoSize = true;
            this.chk_interface.Location = new System.Drawing.Point(75, 73);
            this.chk_interface.Name = "chk_interface";
            this.chk_interface.Size = new System.Drawing.Size(68, 17);
            this.chk_interface.TabIndex = 5;
            this.chk_interface.Text = "Interface";
            this.chk_interface.UseVisualStyleBackColor = true;
            // 
            // chk_Catch
            // 
            this.chk_Catch.AutoSize = true;
            this.chk_Catch.Location = new System.Drawing.Point(15, 73);
            this.chk_Catch.Name = "chk_Catch";
            this.chk_Catch.Size = new System.Drawing.Size(54, 17);
            this.chk_Catch.TabIndex = 6;
            this.chk_Catch.Text = "Catch";
            this.chk_Catch.UseVisualStyleBackColor = true;
            // 
            // chk_Mania
            // 
            this.chk_Mania.AutoSize = true;
            this.chk_Mania.Location = new System.Drawing.Point(149, 73);
            this.chk_Mania.Name = "chk_Mania";
            this.chk_Mania.Size = new System.Drawing.Size(55, 17);
            this.chk_Mania.TabIndex = 7;
            this.chk_Mania.Text = "Mania";
            this.chk_Mania.UseVisualStyleBackColor = true;
            // 
            // chk_Sounds
            // 
            this.chk_Sounds.AutoSize = true;
            this.chk_Sounds.Location = new System.Drawing.Point(15, 96);
            this.chk_Sounds.Name = "chk_Sounds";
            this.chk_Sounds.Size = new System.Drawing.Size(62, 17);
            this.chk_Sounds.TabIndex = 8;
            this.chk_Sounds.Text = "Sounds";
            this.chk_Sounds.UseVisualStyleBackColor = true;
            // 
            // chk_standard
            // 
            this.chk_standard.AutoSize = true;
            this.chk_standard.Location = new System.Drawing.Point(75, 96);
            this.chk_standard.Name = "chk_standard";
            this.chk_standard.Size = new System.Drawing.Size(95, 17);
            this.chk_standard.TabIndex = 9;
            this.chk_standard.Text = "Standard (osu)";
            this.chk_standard.UseVisualStyleBackColor = true;
            // 
            // chk_taiko
            // 
            this.chk_taiko.AutoSize = true;
            this.chk_taiko.Location = new System.Drawing.Point(176, 96);
            this.chk_taiko.Name = "chk_taiko";
            this.chk_taiko.Size = new System.Drawing.Size(53, 17);
            this.chk_taiko.TabIndex = 10;
            this.chk_taiko.Text = "Taiko";
            this.chk_taiko.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 119);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(404, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(379, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 254);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chk_taiko);
            this.Controls.Add(this.chk_standard);
            this.Controls.Add(this.chk_Sounds);
            this.Controls.Add(this.chk_Mania);
            this.Controls.Add(this.chk_Catch);
            this.Controls.Add(this.chk_interface);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "OSU SKIN RANDOMIZER by Sillus";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chk_interface;
        private System.Windows.Forms.CheckBox chk_Catch;
        private System.Windows.Forms.CheckBox chk_Mania;
        private System.Windows.Forms.CheckBox chk_Sounds;
        private System.Windows.Forms.CheckBox chk_standard;
        private System.Windows.Forms.CheckBox chk_taiko;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
    }
}

