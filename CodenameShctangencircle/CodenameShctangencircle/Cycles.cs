using System.Collections.Generic;
using System;

namespace CodenameShctangencircle
{
    public class Cycles
    {
        public List<string> Combinations = new List<string>();
        public List<string> Combination2 = new List<string>();

        int Count = 1;
        public int o, o1, o2, o3, o4, p1, p2, p3, p4, p5, GridCount;

        void k1Cycle(int n)
        {
            double n1;

            for (int k1 = 100; k1 <= 200; k1++)
            {
                n1 = k1 - _a11 / _koef1 + 1;
                //if (n1 > 0 && n1 % 1 == 0)
                //{
                k2Cycle(n, k1, n1);
                //} 
            }
        }

        void k2Cycle(int n, int k1, double n1)
        {
            double n2;
            for (int k2 = 12; k2 <= 62; k2++)
            {
                n2 = Convert.ToDouble(k2 - k1 * _koef1 / _koef2);
                if (n2 > 0 && n2 % 1 == 0)
                {
                    k3Cycle(n, k1, k2, n1, n2);
                }
            }
        }

        void k3Cycle(int n, int k1, int k2, double n1, double n2)
        {
            double n3;
            for (int k3 = 7; k3 <= 57; k3++)
            {
                n3 = Convert.ToDouble(k3 - k2 * _koef2 / _koef3);
                if (n3 > 0 && n3 % 1 == 0)
                {
                    k4Cycle(n, k1, k2, k3, n1, n2, n3);
                }
            }
        }

        void k4Cycle(int n, int k1, int k2, int k3, double n1, double n2, double n3)
        {
            double n4;
            for (int k4 = 2; k4 <= 52; k4++)
            {
                n4 = Convert.ToDouble(k4 - k3 * _koef3 / _koef4);
                if (n4 > 0 && n4 % 1 == 0)
                {
                    k5Cycle(n, k1, k2, k3, k4, n1, n2, n3, n4);
                }
            }
        }

        void k5Cycle(int n, int k1, int k2, int k3, int k4, double n1, double n2, double n3, double n4)
        {
            double n5;
            for (int k5 = 1; k5 <= 10; k5++)
            {
                n5 = Convert.ToDouble(k5 - k4 * _koef4 / _koef5);
                if (n5 > 0 && n5 % 1 == 0)
                {
                    if (n == n1 + n2 + n3 + n4 + n5) //условие суммы
                    {
                        this.gotResult = true;
                        Combinations.Add($"{n},{k1},{k2},{k3},{k4},{k5},{n1},{n2},{n3},{n4},{n5};"); //добавление результата в базу удачных комбинаций
                        Combination2.Add($"({Count}) {n},{k1},{k2},{k3},{k4},{k5},{n1},{n2},{n3},{n4},{n5};");
                        Database.o.Add(n); Database.p1.Add(n1);
                        Database.o1.Add(k1); Database.p2.Add(n2);
                        Database.o2.Add(k2); Database.p3.Add(n3);
                        Database.o3.Add(k3); Database.p4.Add(n4);
                        Database.o4.Add(k4); Database.p5.Add(n5);
                        Database.o5.Add(k5); Database.GridCount.Add(Count);
                        Count++;
                    }
                }
            }
        }

        private bool gotResult = false;

        public void nCycle(ref List<string> VisualOutput, ref List<string> Output, int higher, int lower, double koef1, double koef2, double koef3, double koef4, double koef5, double a11)
        {
            _koef1 = koef1; _koef2 = koef2; _koef3 = koef3; _koef4 = koef4; _koef5 = koef5; _a11 = a11;
            for (int n = higher; n <= lower; n++)
            {
                k1Cycle(n);
            }
            if (!gotResult) Database.noResultList.Add((_koef1 + " " + koef2 + " " + koef3 + " " + koef4 + " " + koef5).ToString());
            System.IO.File.WriteAllLines("file.txt", Combinations);
            Output = Combinations;
            VisualOutput = Combination2;
        }

        double _koef1, _koef2, _koef3, _koef4, _koef5, _a11;

    }
}
