using System;
using System.Collections.Generic;

namespace CodenameShctangencircle
{
    public class CheckDiafant //класс-рудимент для проверки диафантового уравнения
    {
        public void check(List<string> Combinations, int nKoef, int k1Koef, int k2Koef, int k3Koef, int k4Koef, int k5Koef, ref List<string> DiafantOutput)
        {
            int n, k1, k2, k3, k4, k5;
            foreach(string comb in Combinations)
            {
                n = getNumber(comb)[0]; //коэфициенты диафанта
                k1 = getNumber(comb)[1];
                k2 = getNumber(comb)[2];
                k3 = getNumber(comb)[3];
                k4 = getNumber(comb)[4];
                k5 = getNumber(comb)[5];

                int a = k1 * k1Koef + k2 * k2Koef + k3 * k3Koef + k4 * k4Koef + k5 * k5Koef, b = nKoef * n + 990;

                if (a == b)
                {
                    DiafantResults.Add($"{n},{k1},{k2},{k3},{k4},{k5}");
                }
            }
            DiafantOutput = DiafantResults;
            System.IO.File.WriteAllLines("file1.txt", DiafantOutput);
        }

        public List<string> DiafantResults = new List<string>();


        int[] getNumber(string Comb) //поз - позиция числа (нумерация с единицы)
        {
            int ArrayPos = 0;
            int[] Temp = new int[6];
            string GetNum = String.Empty;
            foreach (char i in Comb)
            {
                if (i != ',')
                {
                    GetNum += i;
                } 
                else
                {
                    Temp[ArrayPos] = int.Parse(GetNum);
                    GetNum = String.Empty;
                    ArrayPos++;
                }
                if(ArrayPos == 6)
                {
                    break;
                }
            }
            return Temp;
        }

    }
}
