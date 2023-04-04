using System.Net.NetworkInformation;
using System.Collections;
using System;


namespace King_s_path_on_a_chessboard__length_
{
    class Ctecka
    {
        public static int PrectiInt()
        {
            int znak = Console.Read();
            int minuly_znak = znak;
            int x = 0;
            while ((znak < '0') || (znak > '9'))
            {
                minuly_znak = znak;
                znak = Console.Read();
            }
            while ((znak >= '0') && (znak <= '9'))
            {
                x = x * 10 + (znak - '0');
                znak = Console.Read();

            }
            if (minuly_znak != '-')
            {
                return x;
            }
            else
            {
                return -x;
            }
        }
    }
    internal class Program
    {
        static void Main()
        {
            //initialiaze field with edges
            int[,] hraci_pole = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                hraci_pole[i, 0] = -2;
                hraci_pole[i, 9] = -2;
                hraci_pole[0, i] = -2;
                hraci_pole[9, i] = -2;
            }
            for (int j = 1; j < 9; j++)
            {
                for (int k = 1; k < 9; k++)
                {
                    hraci_pole[j, k] = 0;
                }
            }
            //read No. of obstacles
            
            int count = Convert.ToInt32(Console.ReadLine());
            //read obstacles
            for (int i = 0; i < count; i++)
            {
                hraci_pole[Ctecka.PrectiInt(), Ctecka.PrectiInt()] = -2;
            }
            //set start
            int[] start = new int[2] { Ctecka.PrectiInt(), Ctecka.PrectiInt()} ;
            //set finish
            int[] cil = new int[2] { Ctecka.PrectiInt(), Ctecka.PrectiInt()};
            hraci_pole[cil[0], cil[1]] = -3;
            hraci_pole[start[0], start[1]] = 0;
            Queue moznosti = new Queue();
            //0,0 je zarazka
            moznosti.Enqueue(0);
            moznosti.Enqueue(0);
            //do fronty posilam postupne souradnice po dvou a odebiram znovu po dvou
            moznosti.Enqueue(start[0]);
            moznosti.Enqueue(start[1]);
            int krok = 0;
            int[] current = new int[2];
            while ((hraci_pole[cil[0], cil[1]] == -3) & (moznosti.Count > 0))
             {
                current[0] = Convert.ToInt32(moznosti.Dequeue());
                current[1] = Convert.ToInt32(moznosti.Dequeue());
                if (((current[0] == 0) & (current[1] == 0)) & (moznosti.Count > 0))
                {
                    moznosti.Enqueue(0);
                    moznosti.Enqueue(0);
                    krok++;
                }
                else if (moznosti.Count > 0)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (!((i==0) & (j==0)))
                            {
                                if (hraci_pole[current[0] + i, current[1] + j] == 0)
                                {
                                    hraci_pole[current[0] + i, current[1] + j] = krok;
                                    moznosti.Enqueue(current[0] + i);
                                    moznosti.Enqueue(current[1] + j);
                                }
                                if (hraci_pole[current[0] + i, current[1] + j] == -3)
                                {
                                    hraci_pole[current[0] + i, current[1] + j] = krok;
                                }
                            }
                        }
                    }
                }
            }
            if (hraci_pole[cil[0], cil[1]] == -3)
                Console.WriteLine(-1);
            else
                Console.WriteLine(hraci_pole[cil[0], cil[1]]);
            /*print field in the end
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.Write(hraci_pole[j, k]);
                }
                Console.WriteLine();
            }
            */
        }
    }
}