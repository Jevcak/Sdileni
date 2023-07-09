using System.Media;
using System.Security.Cryptography;

namespace ZapoctovyProgram
{
    public enum Stav { HRA, MENU, PAUZA, PROHRA };
    public partial class Form1 : Form
    {
        public static Stav stav = Stav.MENU;
        private int skore = 0;
        private Random rnd = new();
        private Bitmap BombImg = new Bitmap("Resources/Bomb.png");
        private Bitmap ExplosionImg = new Bitmap("Resources/Explosion.png");
        private int zivoty;
        private bool HrajeHudba = true;
        private Point NextBomb;
        /* Vysvetleni obtiznosti
        * obtiznost[x,0] = pocet zivotu na zacatku
        * obtiznost[x,1] = pocet bomb do ztizeni (zmena intervalu,rychlost)
        * obtiznost[x,2] = pocatecni interval mezi padanim bomb v ms
        * obtiznost[x,3] = zmena intervalu mezi bombami v ms
        * obtiznost[x,4] = zmena zrychleni padani pri ztizeni
        */
        private int[,] obtiznost = new int[3, 5] { { 3, 20, 2000, 100, 1 }, { 3, 10, 2000, 100, 1 }, { 1, 10, 2000, 200, 1 } };
        private int BobRychlost = 10;
        private int bombZaObtiznost;
        private SoundPlayer hudebnik = new SoundPlayer("Resources/8bitMusic.wav");
        private SoundPlayer exploze = new SoundPlayer("Resources/Exploze.wav");
        private Bitmap Zvuk = new Bitmap("Resources/SoundOn.png");
        private Bitmap Nezvuk = new Bitmap("Resources/SoundOff.png");
        private List<PictureBox> Bomby = new List<PictureBox> { };
        private List<PictureBox> Zivoty = new List<PictureBox> { };
        private List<PictureBox> Trofeje = new List<PictureBox> { };

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            NastavStav(Stav.MENU);
            timer1.Stop();
            BobPosun.Stop();
            bombovac.Stop();
            //pridani bomb a zivotu do listu + trofeje
            {
                Bomby.Add(bomba0);
                Bomby.Add(bomba1);
                Bomby.Add(bomba2);
                Bomby.Add(bomba3);
                Bomby.Add(bomba4);
                Bomby.Add(bomba5);
                Bomby.Add(bomba6);
                Bomby.Add(bomba7);
                Bomby.Add(bomba8);
                Bomby.Add(bomba9);
                Bomby.Add(bomba10);
                Bomby.Add(bomba11);
                Bomby.Add(bomba12);
                Bomby.Add(bomba13);
                Bomby.Add(bomba14);
                Bomby.Add(bomba15);
                Bomby.Add(bomba16);
                Bomby.Add(bomba17);
                Bomby.Add(bomba18);
                Bomby.Add(bomba19);
                Zivoty.Add(srdce0);
                Zivoty.Add(srdce1);
                Zivoty.Add(srdce2);
                Trofeje.Add(bronzTrofej);
                Trofeje.Add(stribroTrofej);
                Trofeje.Add(zlatoTrofej);
                Trofeje.Add(emeraldTrofej);
                Trofeje.Add(diamantTrofej);
            }
            bombovac.Interval = obtiznost[(int)Obtiznost.Value - 1, 2];
            timer1.Interval = 50;
            ;
            Obtiznost.Location = new Point(ObtiznostB.Location.X + 140, ObtiznostB.Location.Y + 16);
            foreach (PictureBox boom in Bomby)
            {
                boom.Visible = false;
            }
            foreach (PictureBox srdce in Zivoty)
            {
                srdce.Visible = false;
            }
            foreach (PictureBox trofej in Trofeje)
            {
                trofej.Visible = false;
            }
            Logika.HudboHrej(HrajeHudba, hudebnik);
            Paddle.Location = new Point((Width - Paddle.Width) / 2, Height - 100);
            Bob.Location = new Point(Width / 2, 50);

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                switch (stav)
                {
                    case Stav.HRA:
                        NastavStav(Stav.PAUZA);
                        break;
                    case Stav.MENU:
                        Close();
                        break;
                    case Stav.PAUZA:
                        NastavStav(Stav.HRA);
                        Logika.HudboHrej(HrajeHudba, hudebnik);
                        for (int i = 0; i < zivoty - 1; i++)
                        {
                            Zivoty[i].Visible = true;
                        }
                        break;
                    case Stav.PROHRA:
                        NastavStav(Stav.MENU);
                        break;
                    default:
                        break;
                };
            }
            if ((keyData == Keys.Left) && (Paddle.Location.X >= 0) && (stav == Stav.HRA))
            {
                Paddle.Location = new Point(Paddle.Location.X - 30, Height - 100);
            }
            if ((keyData == Keys.Right) && (Paddle.Location.X + 120 <= Width) && (stav == Stav.HRA))
            {
                Paddle.Location = new Point(Paddle.Location.X + 30, Height - 100);
            }
            if ((keyData == Keys.VolumeMute) || (keyData == Keys.S))
            {
                HudbaZmena();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void BReset_Click(object sender, EventArgs e)
        {
            NastavStav(Stav.HRA);
            foreach (PictureBox boom in Bomby)
            {
                boom.Visible = false;
            }
            skore = 0;
            zivoty = obtiznost[(int)Obtiznost.Value - 1, 0];
            for (int i = 0; i < zivoty; i++)
            {
                Zivoty[i].Visible = true;
            }
            foreach (PictureBox trofej in Trofeje)
            {
                trofej.Visible = false;
            }
            Skore.Text = skore.ToString();
            Logika.HudboHrej(HrajeHudba, hudebnik);
        }

        private void BHraci_Click(object sender, EventArgs e)
        {
            NastavStav(Stav.HRA);
            Paddle.Location = new Point((Width - Paddle.Width) / 2, Height - 100);
            zivoty = obtiznost[(int)Obtiznost.Value - 1, 0];
            for (int i = 0; i < zivoty; i++)
            {
                Zivoty[i].Visible = true;
            }
            foreach (PictureBox trofej in Trofeje)
            {
                trofej.Visible = false;
            }
        }

        private void BMenu_Click(object sender, EventArgs e)
        {
            NastavStav(Stav.MENU);
            skore = 0;
            Logika.HudboHrej(HrajeHudba, hudebnik);
            foreach (PictureBox trofej in Trofeje)
            {
                trofej.Visible = false;
            }
        }

        private void BPokracovat_Click(object sender, EventArgs e)
        {
            NastavStav(Stav.HRA);
            Logika.HudboHrej(HrajeHudba, hudebnik);
        }
        public static void EnableButton(Button button, bool OnOff)
        {
            button.Enabled = OnOff;
            button.Visible = OnOff;
            button.BringToFront();
        }
        private void NastavStav(Stav NaJaky)
        {
            switch (NaJaky)
            {
                case Stav.HRA:
                    stav = NaJaky;
                    EnableButton(BHraci, false);
                    EnableButton(BReset, false);
                    EnableButton(BMenu, false);
                    EnableButton(BPokracovat, false);
                    EnableButton(ObtiznostB, false);
                    Obtiznost.Visible = false;
                    bombovac.Interval = obtiznost[(int)Obtiznost.Value - 1, 2];
                    timer1.Interval = 50;
                    Paddle.Visible = true;
                    Paddle.BringToFront();
                    Skore.Visible = true;
                    Bob.Visible = true;
                    Skore.Text = skore.ToString();
                    Cursor.Hide();
                    timer1.Start();
                    bombovac.Start();
                    BobPosun.Start();
                    Ukoncit.Visible = false;
                    break;
                case Stav.MENU:
                    stav = NaJaky;
                    EnableButton(BHraci, true);
                    EnableButton(BReset, false);
                    EnableButton(BMenu, false);
                    EnableButton(BPokracovat, false);
                    EnableButton(ObtiznostB, true);
                    Obtiznost.Visible = true;
                    Obtiznost.BringToFront();
                    Bob.Visible = false;
                    Paddle.Visible = false;
                    Skore.Visible = false;
                    bombovac.Interval = obtiznost[(int)Obtiznost.Value - 1, 2];
                    timer1.Interval = 50;
                    ;
                    foreach (PictureBox boom in Bomby)
                    {
                        boom.Visible = false;
                    }
                    foreach (PictureBox srdce in Zivoty)
                    {
                        srdce.Visible = false;
                    }
                    Ukoncit.Visible = true;
                    break;
                case Stav.PAUZA:
                    stav = NaJaky;
                    timer1.Stop();
                    bombovac.Stop();
                    BobPosun.Stop();
                    EnableButton(BHraci, false);
                    EnableButton(BReset, true);
                    EnableButton(BMenu, true);
                    EnableButton(BPokracovat, true);
                    Cursor.Show();
                    break;
                case Stav.PROHRA:
                    timer1.Stop();
                    bombovac.Stop();
                    BobPosun.Stop();
                    stav = NaJaky;
                    EnableButton(BHraci, false);
                    EnableButton(BReset, true);
                    EnableButton(BMenu, true);
                    EnableButton(BPokracovat, false);
                    foreach (PictureBox boom in Bomby)
                    {
                        boom.Visible = false;
                    }
                    Bob.Visible = false;
                    Paddle.Visible = false; 
                    bombovac.Interval = obtiznost[(int)Obtiznost.Value - 1, 2];
                    timer1.Interval = 50;
                    ;
                    Cursor.Show();
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox boom in Bomby)
            {
                if (boom.Visible == true)
                {
                    boom.Location = new Point(boom.Location.X, boom.Location.Y + 10);
                    if (Logika.JeVRozmezi(boom.Location, Paddle.Location, 120, 20))
                    {
                        skore++;
                        Skore.Text = skore.ToString();
                        KontrolaSkore();
                        boom.Visible = false;
                    }
                    else if ((boom.Location.Y >= Paddle.Location.Y + 10) && (boom.Image != ExplosionImg))
                    {
                        boom.Image = ExplosionImg;
                        if (HrajeHudba)
                        {
                            exploze.Play();
                        }
                        zivoty--;
                        Zivoty[zivoty].Visible = false;
                        if (zivoty == 0)
                        {
                            NastavStav(Stav.PROHRA);
                        }
                        else
                        {
                            NastavStav(Stav.PAUZA);
                        }
                    }
                    else if (boom.Location.Y >= Height - 50)
                    {
                        boom.Image = BombImg;
                        boom.Visible = false;
                    }
                }
            }
        }
        private int kterou = 0;
        private void bombovac_Tick(object sender, EventArgs e)
        {
            Bomby[kterou].Image = BombImg;
            Bomby[kterou].Visible = true;
            Bomby[kterou].Location = new Point(Bob.Location.X + 25, 170);
            NextBomb = new Point(rnd.Next(0, Width - 90), 0);
            BobPosun.Interval = Logika.SpoctiRychlostBoba(BobRychlost, bombovac.Interval, Bob.Location.X, NextBomb.X);
            kterou++;
            kterou %= 20;
            bombZaObtiznost++;
            if (bombZaObtiznost >= obtiznost[(int)Obtiznost.Value - 1, 1])
            {
                if (bombovac.Interval - obtiznost[(int)Obtiznost.Value - 1, 3] > 250)
                {
                    Logika.ZmenInterval(bombovac, obtiznost[(int)Obtiznost.Value - 1, 3]);
                }
                Logika.ZmenInterval(timer1, obtiznost[(int)Obtiznost.Value - 1, 4]);
            }
            bombZaObtiznost %= obtiznost[(int)Obtiznost.Value - 1, 1];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HudbaZmena();
        }
        private void HudbaZmena()
        {
            if (zvoucek.Image != Nezvuk)
            {
                zvoucek.Image = Nezvuk;
                HrajeHudba = false;
                hudebnik.Stop();
                exploze.Stop();
            }
            else
            {
                zvoucek.Image = Zvuk;
                HrajeHudba = true;
                Logika.HudboHrej(HrajeHudba, hudebnik);
            }
        }

        private void BobPosun_Tick(object sender, EventArgs e)
        {
            if (Logika.VratRozdilAbs(Bob.Location.X, NextBomb.X) > BobRychlost)
            {
                if (Bob.Location.X >= NextBomb.X)
                {
                    Bob.Location = new Point(Bob.Location.X - BobRychlost, Bob.Location.Y);
                }
                else
                {
                    Bob.Location = new Point(Bob.Location.X + BobRychlost, Bob.Location.Y);
                }
            }
        }

        private void Ukoncit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void KontrolaSkore()
        {
            if (skore == 50)
            {
                bronzTrofej.Visible = true;
                zivoty = Logika.ZmenaZivotu(zivoty);
            }
            else if (skore == 100)
            {
                stribroTrofej.Visible = true;
                zivoty = Logika.ZmenaZivotu(zivoty);
            }
            else if (skore == 250)
            {
                zlatoTrofej.Visible = true;
                zivoty = Logika.ZmenaZivotu(zivoty);
            }
            else if ((skore == 500) && (Obtiznost.Value> 1))
            {
                emeraldTrofej.Visible = true;
                zivoty = Logika.ZmenaZivotu(zivoty);
            }
            else if ((skore == 1000) && (Obtiznost.Value > 2))
            {
                diamantTrofej.Visible = true;
                zivoty = Logika.ZmenaZivotu(zivoty);
            }
            for (int i = 0; i < zivoty; i++)
            {
                Zivoty[i].Visible = true;
            }
        }
    }
}