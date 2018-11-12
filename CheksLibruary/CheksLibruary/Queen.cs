using System.Collections.Generic;

namespace CheksLibruary
{
    class Queen : Chess//Королева
    {
        public Queen(int a, int b) : base(a, b)
        {
            Type = 5;
        }
        public override List<int> FindPozitions()
        {
            var list = new List<int>();
            int Poz = ReturnPozition();
            //как слон
            int a = Poz / 10 + 1;
            int b = Poz % 10 + 1;
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
            while ((a < 9) && (b > 0))
            {
                list.Add(ReturnPozition(a++, b--));
            }
            a = Poz / 10 - 1;
            b = Poz % 10 + 1;
            while ((a > 0) && (b < 9))
            {
                list.Add(ReturnPozition(a--, b++));
            }
             a = Poz / 10 + 1;
             b = Poz % 10;
            //как ладья
            while (a < 9)
            {
                list.Add(ReturnPozition(a++, b));
            }
            a = Poz / 10 - 1;
            while (a > 0)
            {
                list.Add(ReturnPozition(a--, b));
            }
            a = Poz / 10;
            b = Poz % 10 + 1;
            while (b < 9)
            {
                list.Add(ReturnPozition(a, b++));
            }
            b = Poz % 10 - 1;
            while (b > 0)
            {
                list.Add(ReturnPozition(a, b--));
            }
            return list;
        }
    }
}
