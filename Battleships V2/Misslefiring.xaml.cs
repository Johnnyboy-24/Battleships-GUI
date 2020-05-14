using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Battleships_V2
{
    /// <summary>
    /// Interaktionslogik für Misslefiring.xaml
    /// </summary>
    public partial class Misslefiring : Window
    {
        private Modified_Button[,] buttons_0;
        private Modified_Button[,] buttons_1;
        private Player currentplayer;
        private delegate void Currentplayerchanged();
        private event Currentplayerchanged playerhaschanged;
        private bool game_over = false;
        private DispatcherTimer dispatcherTimer;

        private Game game;

        public Misslefiring(Game g)
        {
            game = g;
            buttons_0 = new Modified_Button[10, 10];
            buttons_1 = new Modified_Button[10, 10];
            playerhaschanged += checkforcomputermove;
            
            InitializeComponent();
            player1.Content = game.players[1].name;
            player2.Content = game.players[0].name;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            for (int i = 0; i < 10; i++)
            {
                for (int u = 0; u < 10; u++)
                {
                    buttons_0[i, u] = new Modified_Button() { Width = 50, Height = 50, x = u, y = i, Background = new SolidColorBrush(Colors.Aqua), player= game.players[0] };
                    buttons_0[i, u].Click += fire_click;                    
                    Grid.SetColumn(buttons_0[i, u], u + 1);
                    Grid.SetRow(buttons_0[i, u], i + 1);
                    field_0.Children.Add(buttons_0[i, u]);
                   
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int u = 0; u < 10; u++)
                {
                    buttons_1[i, u] = new Modified_Button() { Width = 50, Height = 50, x = u, y = i, Background = new SolidColorBrush(Colors.Aqua), player = game.players[1] };
                    buttons_1[i, u].Click += fire_click;
                    Grid.SetColumn(buttons_1[i, u], u + 1);
                    Grid.SetRow(buttons_1[i, u], i + 1);
                    field_1.Children.Add(buttons_1[i, u]);


                }
            }            
            game.players[0].Fired += boardhaschanged;
            game.players[1].Fired += boardhaschanged;           
            Button startbattle = new Button() { Content = "Start battling" };
            startbattle.Click += Startbattle_Click;
            game_window.Children.Add(startbattle);
            
            
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            changeplayer();
        }

        private void Startbattle_Click(object sender, RoutedEventArgs e)
        {
            if (game.gameMode == GameMode.Ai_v_Ai)
            {

                dispatcherTimer.Start();
            }
            else
            {
                playerhaschanged += highlight_currentplayer;
                changeplayer();
            }
            
            
        }

        private void highlight_currentplayer()
        {
            if (currentplayer == game.players[0])
            {
                player1.Background = new SolidColorBrush(Colors.Transparent);
                player2.Background = new SolidColorBrush(Colors.Red) { Opacity=30};
            }
                
            else
            {
                player2.Background = new SolidColorBrush(Colors.Transparent);
                player1.Background = new SolidColorBrush(Colors.Red) { Opacity = 30 };
            }
        }
        private void checkforcomputermove()
        {
            if (!currentplayer.human)
            {
                ((Computer_Player)currentplayer).next_move();
                if (currentplayer == game.players[0])
                {
                    if (game.players[1].check_has_lost())
                    {
                        MessageBox.Show(currentplayer.name + " has won the game!");
                        game_over = true;
                    }
                       
                }
                else
                {
                    if (game.players[0].check_has_lost())
                    {
                        MessageBox.Show(currentplayer.name + " has won the game!");
                        game_over = true;
                    }
                }
                
                if((!game_over) && (game.players[0].human || game.players[1].human))                    
                    changeplayer();
                if (game_over)
                {
                    dispatcherTimer.Stop();
                }
                

            }
                
        }
        private void boardhaschanged(int X, int Y, Player player)
        {
            switch (player.board[Y, X].status)
            {
                case Status.hit:
                    if (player==game.players[0]) buttons_0[Y, X].Background = new SolidColorBrush(Colors.Red);
                    if (player == game.players[1]) buttons_1[Y, X].Background = new SolidColorBrush(Colors.Red);
                    break;
                case Status.miss:
                    if (player == game.players[0]) buttons_0[Y, X].Background = new SolidColorBrush(Colors.Blue);
                    if (player == game.players[1]) buttons_1[Y, X].Background = new SolidColorBrush(Colors.Blue);
                    break;
                case Status.sunk:
                    if (player == game.players[0]) buttons_0[Y, X].Background = new SolidColorBrush(Colors.Yellow);
                    if (player == game.players[1]) buttons_1[Y, X].Background = new SolidColorBrush(Colors.Yellow);
                    break;
                default:
                    if (player == game.players[0]) buttons_0[Y, X].Background = new SolidColorBrush(Colors.Blue);
                    if (player == game.players[1]) buttons_1[Y, X].Background = new SolidColorBrush(Colors.Blue);
                    break;
            }
        }

        private void fire_click(object sender, RoutedEventArgs e)
        {
            if (game.gameMode== GameMode.Ai_v_Ai)
            {
                return;
            }
            var b = (Modified_Button)sender;
            if (currentplayer != b.player)
            {
                b.player.fire_at(b.x, b.y);
                if (b.player.check_has_lost())
                {               
                    MessageBox.Show(currentplayer.name + " has won the game!");
                    this.Close();
                }

                if (b.player.board[b.y, b.x].status == Status.miss)
                    changeplayer();

                
            }
                
            else { MessageBox.Show("This is not your turn"); }

        }
        private void changeplayer()
        {
            if (currentplayer == game.players[0])
                currentplayer = game.players[1];
            else { currentplayer = game.players[0]; }
            playerhaschanged();

        }
        
    }
}
