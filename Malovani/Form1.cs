namespace Malovani
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pero = new Pen(Brushes.Red, 10);
            pero.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pero.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            bmp = new Bitmap(2000, 2000); //TODO rozmery
            pictureBox2.Image = bmp;
        }
        int minx;
        int miny;
        Pen pero;
        Bitmap bmp; //1a
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            Graphics g = CreateGraphics();
            if (e.Button == MouseButtons.Left)
                g.DrawLine(pero, minx, miny, e.X, e.Y);
            minx = e.X;
            miny = e.Y;
            */
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pero.Color = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            if (e.Button == MouseButtons.Left)
            {
                g.DrawLine(pero, minx, miny, e.X, e.Y);
                pictureBox2.Refresh();
            }
            minx = e.X;
            miny = e.Y;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pero.Width = trackBar1.Value;
        }

        private void uložitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                bmp.Save(saveFileDialog1.FileName);
        }
    }
}