using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheksLibruary
{
    class Help:Chess
    {
        public Help():base(0,0)
        {
            Type = 0;
        }
        public override List<int> FindPozitions()
        {
            List<int> list = new List<int>();
            return list;
        }
    }
}
