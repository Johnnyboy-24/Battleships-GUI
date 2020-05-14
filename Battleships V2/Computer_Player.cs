using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleships_V2

{
    public enum Difficulty { easier, harder }
    public class Computer_Player : Player
    {
        private GameElement[,] opponents_board { get; set; }
        public Player opponent { get; set; }
        private List<GameElement> hit_ships { get; set; }
        public Difficulty difficulty { get; set; }

        public bool submarine_is_hit { get; set; }
        public int fire_counter { get; set; }



        public Computer_Player(string n, bool h) : base(n, h)
        {
            opponents_board = new GameElement[10, 10];
            for (int i = 0; i <= 9; i++)
            {
                for (int u = 0; u <= 9; u++)
                {
                    opponents_board[u, i] = new GameElement(Type.Wasser, Status.tba) { Y = u, X = i };
                }
            }

            hit_ships = new List<GameElement>();
            fire_counter = 0;
            submarine_is_hit = false;
        }
        public void next_move()
        {

            if (hit_ships.Count > 0)
                sink_boat_routine();

            else
            {
                Random rand = new Random((int)DateTime.Now.Ticks);

                switch (difficulty)
                {
                    case Difficulty.easier: easier_fire_routine(rand); break;
                    case Difficulty.harder: harder_fire_routine(rand); break;
                }
            }

        }

        private void sink_boat_routine()
        {
            int X = 0;
            int Y = 0;
            bool target_found = false;
            switch (hit_ships.Count)
            {

                case 1:


                    for (int u = hit_ships[0].Y - 1; u <= hit_ships[0].Y + 1; u++)
                    {
                        if (!target_found)
                            if (hit_ships[0].X < 10 && hit_ships[0].X >= 0 && u < 10 && u >= 0)
                            {
                                if (opponents_board[u, hit_ships[0].X].status == Status.tba)
                                {
                                    X = hit_ships[0].X;
                                    Y = u;
                                    target_found = true;

                                }
                            }
                    }

                    for (int i = hit_ships[0].X - 1; i <= hit_ships[0].X + 1; i++)
                    {
                        if (!target_found)


                            if (i < 10 && i >= 0 && hit_ships[0].Y < 10 && hit_ships[0].Y >= 0)
                            {
                                if (opponents_board[hit_ships[0].Y, i].status == Status.tba)
                                {
                                    X = i;
                                    Y = hit_ships[0].Y;
                                    target_found = true;

                                }
                            }

                    }
                    break;
                default:
                    if (hit_ships[0].Y == hit_ships[1].Y)
                    {
                        foreach (GameElement g in hit_ships)
                        {
                            if (g.X > 0)
                            {
                                if (opponents_board[g.Y, g.X - 1].status == Status.tba)
                                {
                                    X = g.X - 1;
                                    Y = g.Y;
                                    break;
                                }
                            }
                            if (g.X < 9)
                            {
                                if (opponents_board[g.Y, g.X + 1].status == Status.tba)
                                {
                                    X = g.X + 1;
                                    Y = g.Y;
                                    break;
                                }
                            }
                        }

                    }
                    if (hit_ships[0].X == hit_ships[1].X)
                    {
                        foreach (GameElement g in hit_ships)
                        {
                            if (g.Y > 0)
                            {
                                if (opponents_board[g.Y - 1, g.X].status == Status.tba)
                                {
                                    X = g.X;
                                    Y = g.Y - 1;
                                    break;
                                }
                            }
                            if (g.Y < 9)
                            {
                                if (opponents_board[g.Y + 1, g.X].status == Status.tba)
                                {
                                    X = g.X;
                                    Y = g.Y + 1;
                                    break;
                                }
                            }
                        }

                    }
                    break;



            }
            fire(X, Y);
        }


        private void fire(int X, int Y)
        {

            bool new_move = false;
            if (X > 9 || X < 0 || Y < 0 || Y > 9)
            {
                return;
            }

            switch (opponent.fire_at(X, Y))
            {
                case Status.hit:
                    opponents_board[Y, X].status = Status.hit; hit_ships.Add(opponents_board[Y, X]); new_move = true; break;
                case Status.miss:
                    opponents_board[Y, X].status = Status.miss; break;
                case Status.sunk:
                    opponents_board[Y, X].status = Status.sunk;
                    foreach (GameElement g in hit_ships) { g.status = Status.sunk; };
                    if (hit_ships.Count == 1) { submarine_is_hit = true; }
                    hit_ships.Clear(); excludefields();
                    if (!opponent.check_has_lost()) { new_move = true; }
                    break;
            }
            fire_counter++;
            if (new_move)
                next_move();


        }
        private void excludefields()
        {
            foreach (GameElement g in opponents_board)
            {
                if (g.status == Status.sunk)
                {
                    for (int i = g.Y - 1; i <= g.Y + 1; i++)
                    {
                        for (int u = g.X - 1; u <= g.X + 1; u++)
                        {
                            if (i < 10 && i >= 0 && u < 10 && u >= 0)
                            {
                                if (opponents_board[i, u].status == Status.tba)
                                {
                                    opponents_board[i, u].status = Status.miss;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void easier_fire_routine(Random r)
        {
            int X;
            int Y;
            do
            {
                X = r.Next(0, 10);
                Y = r.Next(0, 10);
            }
            while (opponents_board[Y, X].status != Status.tba);
            fire(X, Y);
        }
        private void harder_fire_routine(Random r)
        {
            if (fire_counter < 40 || (submarine_is_hit = true))
            {
                int X;
                int Y;
                do
                {
                    X = r.Next(0, 10);
                    Y = r.Next(0, 10);
                }
                while ((opponents_board[Y, X].status != Status.tba) || (((X + 1) % 2) != ((Y + 1) % 2)));
                fire(X, Y);
            }
            else { easier_fire_routine(r); }

        }


    }

}
