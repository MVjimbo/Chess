using System.Collections.Generic;

namespace CheksLibruary
{
    class Pawn: Chess//Пешка
    {
        public Pawn(int a,int b): base(a,b)
        {
            Type = 1;
        }
        public override List<int> FindPozitions()
        {
            var list = new List<int>();
            int Poz = ReturnPozition();
            int a = Poz / 10;
            int b = Poz % 10;
            if (a<9)
                list.Add(ReturnPozition(a + 1, b));
            if(a>1)
                list.Add(ReturnPozition(a - 1, b));
            return list;
        }
    }
}
