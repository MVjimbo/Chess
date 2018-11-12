using System.Collections.Generic;

namespace CheksLibruary
{
    class Elephant:Chess//Слон
    {
        public Elephant(int a, int b) : base(a, b)
        {
            Type = 4;
        }
        public override List<int> FindPozitions()
        {
            var list = new List<int>();
            int Poz = ReturnPozition();
            int a = Poz / 10 + 1;
            int b = Poz % 10+1;
            while ((a < 9) && (b < 9))
            {
                list.Add(ReturnPozition(a++, b++));
            }
            a = Poz / 10 - 1;
            b = Poz % 10 - 1;
            while ((a > 0) && (b > 0))
            {
                list.Add(ReturnPozition(a--, b--));
            }
            a = Poz / 10 + 1;
            b = Poz % 10 - 1;
            while ((a <9) && (b>0))
            {
                list.Add(ReturnPozition(a++, b--));
            }
            a = Poz / 10-1;
            b = Poz % 10 + 1;
            while ((a>0) && (b < 9))
            {
                list.Add(ReturnPozition(a--, b++));
            }
            return list;
        }
    }
}
