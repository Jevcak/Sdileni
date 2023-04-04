namespace Malovani
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            trackBar1 = new TrackBar();
            menuStrip1 = new MenuStrip();
            souborToolStripMenuItem = new ToolStripMenuItem();
            novýToolStripMenuItem = new ToolStripMenuItem();
            otevřítToolStripMenuItem = new ToolStripMenuItem();
            uložitToolStripMenuItem = new ToolStripMenuItem();
            kouzloToolStripMenuItem = new ToolStripMenuItem();
            konecToolStripMenuItem = new ToolStripMenuItem();
            toolTip1 = new ToolTip(components);
            saveFileDialog1 = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(303, 99);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(12, 136);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(776, 302);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            pictureBox2.MouseMove += pictureBox2_MouseMove;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(321, 31);
            trackBar1.Maximum = 300;
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(479, 56);
            trackBar1.TabIndex = 1;
            trackBar1.Value = 1;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { souborToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // souborToolStripMenuItem
            // 
            souborToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { novýToolStripMenuItem, otevřítToolStripMenuItem, uložitToolStripMenuItem, kouzloToolStripMenuItem, konecToolStripMenuItem });
            souborToolStripMenuItem.Name = "souborToolStripMenuItem";
            souborToolStripMenuItem.Size = new Size(71, 24);
            souborToolStripMenuItem.Text = "Soubor";
            // 
            // novýToolStripMenuItem
            // 
            novýToolStripMenuItem.Name = "novýToolStripMenuItem";
            novýToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            novýToolStripMenuItem.Size = new Size(224, 26);
            novýToolStripMenuItem.Text = "Nový";
            // 
            // otevřítToolStripMenuItem
            // 
            otevřítToolStripMenuItem.Name = "otevřítToolStripMenuItem";
            otevřítToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            otevřítToolStripMenuItem.Size = new Size(224, 26);
            otevřítToolStripMenuItem.Text = "Otevřít";
            // 
            // uložitToolStripMenuItem
            // 
            uložitToolStripMenuItem.Name = "uložitToolStripMenuItem";
            uložitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Right;
            uložitToolStripMenuItem.Size = new Size(224, 26);
            uložitToolStripMenuItem.Text = "Uložit";
            uložitToolStripMenuItem.Click += uložitToolStripMenuItem_Click;
            // 
            // kouzloToolStripMenuItem
            // 
            kouzloToolStripMenuItem.Name = "kouzloToolStripMenuItem";
            kouzloToolStripMenuItem.Size = new Size(224, 26);
            kouzloToolStripMenuItem.Text = "Kouzlo";
            // 
            // konecToolStripMenuItem
            // 
            konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            konecToolStripMenuItem.Size = new Size(224, 26);
            konecToolStripMenuItem.Text = "Konec";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(800, 450);
            Controls.Add(trackBar1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Controls.Add(pictureBox2);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            MouseMove += Form1_MouseMove;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TrackBar trackBar1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem souborToolStripMenuItem;
        private ToolStripMenuItem novýToolStripMenuItem;
        private ToolStripMenuItem otevřítToolStripMenuItem;
        private ToolStripMenuItem uložitToolStripMenuItem;
        private ToolStripMenuItem kouzloToolStripMenuItem;
        private ToolStripMenuItem konecToolStripMenuItem;
        private ToolTip toolTip1;
        private SaveFileDialog saveFileDialog1;
    }
}