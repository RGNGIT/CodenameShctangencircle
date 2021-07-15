using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameShctangencircle
{
    [Serializable]
    public class DataBlock
    {
        public string Entry;
        public List<int> o = new List<int>();
        public List<int> o1 = new List<int>();
        public List<int> o2 = new List<int>();
        public List<int> o3 = new List<int>();
        public List<int> o4 = new List<int>();
        public List<int> o5 = new List<int>();
        public List<double> p1 = new List<double>();
        public List<double> p2 = new List<double>();
        public List<double> p3 = new List<double>();
        public List<double> p4 = new List<double>();
        public List<double> p5 = new List<double>();
        public List<int> GridCount = new List<int>();
        // База результатов подбора
        public List<string> Count = new List<string>();
        public List<string> l1 = new List<string>();
        public List<string> l2 = new List<string>();
        public List<string> l3 = new List<string>();
        public List<string> l4 = new List<string>();
        public List<string> l5 = new List<string>();
        public List<string> KE = new List<string>();
        public List<string> KSR = new List<string>();
        public List<string> SDM = new List<string>();
        public List<string> VGU = new List<string>();
        public List<string> NGU = new List<string>();
    }
}
