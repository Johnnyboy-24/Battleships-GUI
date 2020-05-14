using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_V2
{
    public enum Status { tba, hit, miss, sunk };

    public class GameElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Type type { get; set; }
        public Ship ship { get; set; }
        public Status status { get; set; }

        public GameElement(Type t, Status s)
        {
            type = t;
            status = s;
            ship = null;
        }
    }
}
