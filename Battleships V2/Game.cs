using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_V2
{
    public enum GameMode { P_v_P, P_v_Ai, Ai_v_Ai, tba };
    public class Game
    {
        public GameMode gameMode { get; set; }
        public Player[] players { get; set; }

        public Game(GameMode m, string n1, string n2)
        {
            gameMode = m;
            players = new Player[2];
                switch (m)
                {
                case GameMode.P_v_P:
                    players[0]= new Player(n1, true);
                    players[1] = new Player(n2, true);
                    break;
                case GameMode.P_v_Ai:
                    players[0] = new Player(n1, true);
                    players[1] = new Computer_Player(n2, false);
                    ((Computer_Player)players[1]).opponent = players[0];
                    ((Computer_Player)players[1]).difficulty = Difficulty.easier;
                    break;
                case GameMode.Ai_v_Ai:
                    players[0] = new Computer_Player(n1, false);
                    players[1] = new Computer_Player(n2, false);
                    ((Computer_Player)players[1]).opponent = players[0];
                    ((Computer_Player)players[1]).difficulty = Difficulty.easier;
                    ((Computer_Player)players[0]).opponent = players[1];
                    ((Computer_Player)players[0]).difficulty = Difficulty.harder;
                    break;
                default: throw (new Exception("Something went wrong! please restart."));

                }

        }
    }
}
