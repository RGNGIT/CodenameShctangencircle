using System;
using System.Collections.Generic;

namespace CodenameShctangencircle
{
    public class CheckDiafant
    {
        public void check(List<string> Combinations, int nKoef, int k1Koef, int k2Koef, int k3Koef, int k4Koef, int k5Koef, ref List<string> DiafantOutput)
        {
            int n, k1, k2, k3, k4, k5;
            foreach(string comb in Combinations)
            {
                n = Convert.ToInt32(getNumber(comb, 1)); //коэфициенты диафанта
                k1 = Convert.ToInt32(getNumber(comb, 2));
                k2 = Convert.ToInt32(getNumber(comb, 3));
                k3 = Convert.ToInt32(getNumber(comb, 4));
                k4 = Convert.ToInt32(getNumber(comb, 5));
                k5 = Convert.ToInt32(getNumber(comb, 6));
                
                if(k1 * k1Koef + k2 * k2Koef + k3 * k3Koef + k4 * k4Koef + k5 * k5Koef == nKoef * n + 990)
                {
                    DiafantResults.Add($"{n},{k1},{k2},{k3},{k4},{k5}");
                }
            }
            DiafantOutput = DiafantResults;
        }

        public List<string> DiafantResults = new List<string>();


        string getNumber(string comb, int pos) //поз - позиция числа (нумерация с единицы)
        {
            int commasCounter = 0, stringPos = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            while(commasCounter != pos)
            {
                if (comb[stringPos] == ',') commasCounter++;
                if (commasCounter == pos) break;
                stringPos++;
            }
            while(comb[stringPos] != ',') //типа в обратном порядке от закрывающей запятой
            {
                sb.Insert(0, comb[stringPos]); stringPos--; 
            }

            return sb.ToString();
        }

    }
}
