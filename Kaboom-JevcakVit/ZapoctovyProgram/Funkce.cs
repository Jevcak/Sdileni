using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Media;
using System.Resources;
using ZapoctovyProgram;
public class Logika
{
    public static bool JeVRozmezi(Point first, Point second,int range, int polovinaSire)
    {
        if ((second.X <= first.X + polovinaSire) && (first.X + polovinaSire <= second.X + range) && (first.Y + 20 >= second.Y))
        {
            return true;
        }
        return false;
    }
    public static void HudboHrej(bool hraje, SoundPlayer zvukar)
    {
        if (hraje)
        {
            zvukar.PlayLooping();
        }
    }
    public static int SpoctiRychlostBoba(int posunBoba, int celkovyCas,int pozice1,int pozice2)
    {
        int vypocet = (posunBoba * celkovyCas) / VratRozdilAbs(pozice1, pozice2);
        if (vypocet != 0)
        {
            return vypocet;
        }
        else
        {
            return 1;
        }
    }
    public static int VratRozdilAbs(int prvni, int druhy)
    {
        if (prvni>druhy)
        {
            return prvni - druhy;
        }
        else if (prvni == druhy)
        {
            return 1;
        }
        else
        {
            return druhy - prvni;
        }
    }
    public static void ZmenInterval(System.Windows.Forms.Timer casovac, int oKolik)
    {
        if (casovac.Interval > oKolik)
        {
            casovac.Interval -= oKolik;
        }
        else
        {
            casovac.Interval = (casovac.Interval + 1) / 2;
        }
    }
    public static int ZmenaZivotu(int pocet)
    {
        if (pocet < 3)
        {
            return pocet + 1;
        }
        else
        {
            return pocet;
        }
    }
}
