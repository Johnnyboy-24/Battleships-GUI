using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_V2
{
   
    public enum Type {Wasser = 0, UBoot = 1, Zerstörer = 2, Kreuzer = 3, Schlachtschiff = 4 , Flugzeugtraeger = 5 };
    public enum Orientation { vertical, horizontal };
    public class Ship
    {
        public Orientation orientation { get; set; }
        public Type type { get; private set; }
        private int lives;
        private int x;
        private int y;
        public List<GameElement> positions { get; set; }

       


        public int X
        {
            get { return x; }
            set
            {
                if (value <= 9 && value >= 0)
                {
                    /*switch (orientation)
                    {
                        case Orientation.vertical:
                            x = value;
                            break;
                        case Orientation.horizontal:
                            if (((int)type + value) <= 10)
                                x = value;
                            else { throw new ArgumentException("Hier kannst du das Schiff nicht platzieren!"); }
                            break;
                        default: throw new ArgumentException("Hier kannst du das Schiff nicht platzieren!");
                    }*/
                    x = value;                    
                }

            }
        }  
        public int Y
        {
            get { return y; }
            set
            {
                if (value <= 9 && value >= 0)
                {
                    /* switch (orientation)
                     {
                         case Orientation.horizontal:
                             y = value;
                             break;
                         case Orientation.vertical:
                             if (((int)type + value) <= 10)
                                 y = value;
                             //else { throw new ArgumentException("Hier kannst du das Schiff nicht platzieren!"); }
                             break;
                         default: break; ///throw new ArgumentException("Hier kannst du das Schiff nicht platzieren!");
                     }
                     */
                    y = value;
                }
            }
        }
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
            }
        }

        public Ship(Type t)
        {
            switch (t)
            {
                case Type.Flugzeugtraeger:
                    type = Type.Flugzeugtraeger;
                    break;
                case Type.UBoot:
                    type = Type.UBoot;
                    break;
                case Type.Kreuzer:
                    type = Type.Kreuzer;
                    break;
                case Type.Schlachtschiff:
                    type = Type.Schlachtschiff;
                    break;
                case Type.Zerstörer:
                    type = Type.Zerstörer;
                    break;
                default:
                    throw (new ArgumentException("That is not a valid ship-type"));


            }
            Lives = (int)t;
            orientation = Orientation.horizontal;
            positions = new List<GameElement>();
            
        }
    }
}
