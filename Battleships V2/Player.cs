using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Battleships_V2

{

    public delegate void Boardchangedduetoplace(int X, int Y);
    public delegate void Boardchangedduetofire(int X, int Y, Player p);

    public class Player
    {

        public string name { get; set; }
        public bool human { get; private set; }
        public GameElement[,] board { get; private set; }
        public Ship[] ships { get; private set; }
        public event Boardchangedduetoplace BoardChanged;
        public event Boardchangedduetofire Fired;


        public Player(string n, bool h)
        {
            name = n;
            human = h;
            board = new GameElement[10, 10];
            for (int i = 0; i <= 9; i++)
            {
                for (int u = 0; u <= 9; u++)
                {
                    board[u, i] = new GameElement(Type.Wasser, Status.tba) { X = i, Y = u, ship = null };
                }
            }
            ships = new Ship[6];
            ships[0] = new Ship(Type.UBoot);
            ships[1] = new Ship(Type.Zerstörer);
            ships[2] = new Ship(Type.Zerstörer);
            ships[3] = new Ship(Type.Kreuzer);
            ships[4] = new Ship(Type.Schlachtschiff);
            ships[5] = new Ship(Type.Flugzeugtraeger);
        }


        // nachfolgende Funktionen besser private, aber für Unit-Tests öffentlich
        public void placeships()
        {
            var rand = new Random();

            for (int i = 0; i <= 5; i++)
            {
                int orientation = rand.Next(0, 2);
                ships[i].X = rand.Next(0, 10);
                ships[i].Y = rand.Next(0, 10);
                if (orientation == 0)
                {
                    ships[i].orientation = Orientation.vertical;
                }
                if (orientation == 1)
                {
                    ships[i].orientation = Orientation.horizontal;
                }
                while (!Check_placeable(ships[i], ships[i].X, ships[i].Y))
                {
                    ships[i].X = rand.Next(0, 10);
                    ships[i].Y = rand.Next(0, 10);
                }
                place(ships[i]);
            }
        }
        public bool check_ship(int X, int Y)
        {
            if (X > 9 || Y > 9)
                return false;
            if (board[Y, X].ship != null)
                return false;
            else return true;
        }
        public bool check_surrounding_ships(int X, int Y)
        {
            bool clear = true;
            for (int i = Y - 1; i <= Y + 1; i++)
            {
                for (int u = X - 1; u <= X + 1; u++)
                {
                    if (i < 10 && i >= 0 && u < 10 && u >= 0)
                    {
                        if (board[i, u].ship != null)
                            clear = false;
                    }
                }
            }
            return clear;
        }
        public bool Check_placeable(Ship s, int X, int Y)
        {
            if (X > 9 || Y > 9)
                return false;
            if ((int)s.orientation == 0)
            {
                for (int i = Y; i < Y + s.Lives; i++)
                {
                    if (!(check_surrounding_ships(X, i) && check_ship(X, i)))
                        return false;
                }
            }
            if ((int)s.orientation == 1)
            {
                for (int i = X; i < X + s.Lives; i++)
                {
                    if (!(check_surrounding_ships(i, Y) && check_ship(i, Y)))
                        return false;
                }
            }
            return true;
        }
        public void place(Ship s)
        {
            if (Check_placeable(s, s.X, s.Y))
            {
                if ((int)s.orientation == 0)
                {
                    for (int i = 0; i < s.Lives; i++)
                    {
                        board[s.Y + i, s.X].type = s.type;
                        board[s.Y + i, s.X].ship = s;
                        s.positions.Add(board[s.Y + i, s.X]);
                        BoardChanged?.Invoke(s.X, s.Y + i);
                    }
                }
                if ((int)s.orientation == 1)
                {
                    for (int i = 0; i < s.Lives; i++)
                    {
                        board[s.Y, s.X + i].type = s.type;
                        board[s.Y, s.X + i].ship = s;
                        s.positions.Add(board[s.Y, s.X + i]);
                        BoardChanged?.Invoke(s.X + i, s.Y);
                    }
                }

            }
        }




        public Status fire_at(int X, int Y)
        {
            Status status = Status.tba;
            if (!(board[Y, X].status == Status.tba))
            {
                status = Status.miss;
            }
            if (board[Y, X].type == Type.Wasser)
                board[Y, X].status = Status.miss;
            Fired?.Invoke(X, Y, this);
            status = Status.miss;
            if (board[Y, X].ship != null)
            {
                if (board[Y, X].ship.Lives > 0)
                    board[Y, X].ship.Lives -= 1;

                if (board[Y, X].ship.Lives == 0)
                {
                    foreach (GameElement g in board[Y, X].ship.positions)
                    {
                        g.status = Status.sunk;
                        Fired?.Invoke(g.X, g.Y, this);

                    }
                    status = Status.sunk;
                }
                else
                {
                    board[Y, X].status = Status.hit;
                    status = Status.hit;
                    Fired?.Invoke(X, Y, this);
                }
            }

            return status;
        }

        public void remove(Ship s)
        {
            foreach (GameElement g in s.positions)
            {
                g.type = Type.Wasser;
                g.ship = null;
                BoardChanged(g.X, g.Y);
            }
            s.positions.Clear();

        }
        public bool check_has_lost()
        {
            for (int i = 0; i < 6; i++)
            {
                if (ships[i].Lives != 0)
                    return false;

            }
            return true;
        }


    }
}
