using System.Collections.Generic;
using System;

namespace CodenameShctangencircle
{
    public class Cycles
    {
        public List<string> Combinations = new List<string>();


        void k1Cycle(int n)
        {
            double n1;

            for(int k1 = 100; k1 <= 200; k1++)
            {
                n1 = k1 - 99;
                if (n1 > 0 && n1 % 1 == 0)
                {
                    k2Cycle(n, k1, n1); 
                } 
            }
        }

        void k2Cycle(int n, int k1, double n1)
        {
            double n2;
            for (int k2 = 12; k2 <= 62; k2++)
            {
                n2 = Convert.ToDouble(k2 - (0.1 * k1));
                if (n2 > 0 && n2 % 1 == 0)
                {
                    k3Cycle(n, k1, k2, n1 , n2);
                }
            }
        }

        void k3Cycle(int n, int k1, int k2, double n1, double n2)
        {
            double n3;
            for (int k3 = 7; k3 <= 57; k3++)
            {
                n3 = Convert.ToDouble(k3 - (0.5 * k2));
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
                n4 = Convert.ToDouble(k4 - (0.1 * k3));
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
                n5 = Convert.ToDouble(k5 - (0.1 * k4));
                if (n5 > 0 && n5 % 1 == 0)
                {
                    if(n == n1 + n2 + n3 + n4 + n5)
                    {
                        Combinations.Add($"{n},{k1},{k2},{k3},{k4},{k5},{n1},{n2},{n3},{n4},{n5};");
                    }
                }
            }
        }

        public void nCycle(ref List<string> Output)
        {
            for(int n = 47; n <= 50; n++)
            {
                k1Cycle(n);
            }
            System.IO.File.WriteAllLines("file.txt", Combinations);
            Output = Combinations;
        }







    }
}
