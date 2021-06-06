using System;
using System.Collections.Generic;

namespace CodenameShctangencircle
{
    public class FindSteps
    {
        private List<string> stepCombinations = new List<string>();

        public void vFindSteps(List<string> Combinations, double q1, double q2, double q3, double q4, double q5, ref List<string> stComb, double A11)
        {
            int n, n1, n2, n3, n4, n5;
            double KSR, SDM = 0, VGU = 0, NGU = 0, KE;

            int Count = 1;

            foreach (string comb in Combinations)
            {
                int max = 0;
                n = getNumber(comb)[0]; 
                n1 = getNumber(comb)[6]; KSR = n1; if (n1 > max) max = n1;
                n2 = getNumber(comb)[7]; if (n2 > max) max = n2;
                n3 = getNumber(comb)[8]; if (n3 > max) max = n3;
                n4 = getNumber(comb)[9]; if (n4 > max) max = n4;
                n5 = getNumber(comb)[10]; if (n5 > max) max = n5;

                #region не смотри сюда ты меня убьешь я это делал в 5 утра аааааааааааааааааааа
                string res;
                Database.Count.Add(Count.ToString());

                res = $""; 
                double temp = A11;
                res += temp + " ";
                for (int i = 1; i < n1; i++)
                {   
                    temp += q1;
                    if (max == n1 && i == 0) NGU = temp;
      
                    res += temp + " ";
                }
                if (max == n1) VGU = temp;
                SDM += temp;
                res += " ";
                Database.l1.Add(res);

                res = " ";
                for (int i = 0; i < n2; i++)
                {
                    temp += q2;
                    if (max == n2 && i == 0) NGU = temp;
                    res += temp + " ";
                }
                if (max == n2) VGU = temp;
                SDM += temp;
                res += " ";
                Database.l2.Add(res);

                res = " ";
                for (int i = 0; i < n3; i++)
                {
                    temp += q3;
                    if (max == n3 && i == 0) NGU = temp;
                    res += temp + " ";
                }
                if (max == n3) VGU = temp;
                SDM += temp;
                res += " ";
                Database.l3.Add(res);
                
                res = " ";
                for (int i = 0; i < n4; i++)
                {
                    temp += q4;
                    if (max == n4 && i == 0) NGU = temp;
                    res += temp + " ";
                }
                if (max == n4) VGU = temp;
                SDM += temp;
                res += " ";
                Database.l4.Add(res);
                
                res = " ";
                for (int i = 0; i < n5; i++)
                {
                    temp += q5;
                    if (max == n5 && i == 0) NGU = temp;
                    res += temp + " ";
                }
                if (max == n5) VGU = temp;
                SDM += temp;
                res += " ";
                Database.l5.Add(res);

                KE = (KSR / SDM) * (VGU - NGU);
                //res += $" |KE = {KE}| |KSR = {KSR}| |SDM = {SDM}| |VGU = {VGU}| |NGU = {NGU}|";
                Database.KE.Add(KE.ToString());
                Database.KSR.Add(KSR.ToString());
                Database.SDM.Add(SDM.ToString());
                Database.VGU.Add(VGU.ToString());
                Database.NGU.Add(NGU.ToString());
                #endregion
                stepCombinations.Add(res);
                Count++;
            }
            stComb = stepCombinations;
        }

        int[] getNumber(string Comb) //поз - позиция числа (нумерация с единицы)
        {
            int ArrayPos = 0;
            int[] Temp = new int[11];
            string GetNum = String.Empty;
            foreach (char i in Comb)
            {
                if (i != ',' && i != ';')
                {
                    GetNum += i;
                }
                else
                {
                    Temp[ArrayPos] = int.Parse(GetNum);
                    GetNum = String.Empty;
                    ArrayPos++;
                }
                if (ArrayPos == 11)
                {
                    break;
                }
            }
            return Temp;
        }

    }
}
