using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleships_V2
{
    public class Modified_Button : Button
    {
        public int x { get; set; }
        public int y { get; set; }

        public Ship ship { get; set; }
        public Player player { get; set; }

    }
}
