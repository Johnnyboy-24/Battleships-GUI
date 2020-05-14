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
using System.ComponentModel;

namespace Battleships_V2
{   
    
    /// <summary>
    /// Interaktionslogik für Ship_placement.xaml
    /// </summary>
    public partial class Ship_placement : Window
    {

        
        private Modified_Button[,] buttons;
        private Modified_Button[] ship_buttons;
        private Player player;
        private bool secondplayer;
        private Game game;
        private int boatsplaced = 0;
        private Ship current_ship;
        public Ship_placement(Player p, bool s, Game g)
        {
            game = g;
            player = p;
            secondplayer = s;
            buttons = new Modified_Button[10, 10];
            ship_buttons = new Modified_Button[6];
            

            InitializeComponent();
            
            for (int i= 0; i < 6; i++)
            {
                ship_buttons[i]=new Modified_Button()
                { Content=ToString(player.ships[i].type),
                  ship = player.ships[i],
                  Margin = new Thickness(5) ,
                  Padding = new Thickness(3)                                                     
                };
                ship_buttons[i].Click += ship_buttons_click;
                Shipllist.Children.Add(ship_buttons[i]);

            }

            for (int i = 0; i < 10; i++)
            {
                for (int u = 0; u < 10; u++)
                {
                    buttons[i, u] = new Modified_Button() { Width = 50, Height = 50, x = u, y = i, Background= new SolidColorBrush(Colors.Aqua)};
                    buttons[i, u].Click += ship_placement_click;
                    buttons[i, u].MouseEnter += field_MouseEnter;
                    buttons[i, u].MouseLeave += field_MouseLeave;
                    Grid.SetColumn(buttons[i, u], u + 1);
                    Grid.SetRow(buttons[i, u], i + 1);
                    field.Children.Add(buttons[i, u]);
                }
            }
            player.BoardChanged += boardhaschanged;

        }

        private void field_MouseLeave(object sender, MouseEventArgs e)
        {
            if (current_ship != null)
            {
                var b = (Modified_Button)sender;
                var s = current_ship;
                if ((int)s.orientation == 0)
                {
                    for (int i = 0; i < s.Lives; i++)
                    {
                        if (b.y + i >= 0 && b.y + i <= 9 && b.x >= 0 && b.x <= 9)
                        {
                            buttons[b.y + i, b.x].Background = new SolidColorBrush(Colors.Aqua) ;
                        }


                    }
                }
                if ((int)s.orientation == 1)
                {
                    for (int i = 0; i < s.Lives; i++)
                    {
                        if (b.y >= 0 && b.y <= 9 && b.x + i >= 0 && b.x + i <= 9)
                        {
                            buttons[b.y, b.x + i].Background = new SolidColorBrush(Colors.Aqua) ;
                        }

                    }
                }
            }
        }

        private void boardhaschanged(int X, int Y)
        {
            switch (player.board[Y, X].type)
            {
                case Type.Wasser:
                    buttons[Y, X].Background = new SolidColorBrush(Colors.Aqua);
                    break;
                default:
                    buttons[Y, X].Background = new SolidColorBrush(Colors.DarkGray);
                    break;
            }
        }
        private void field_MouseEnter(object sender, MouseEventArgs e)
        {
            if (current_ship != null)
            {
                var b = (Modified_Button)sender;
                var s = current_ship;
                if ((int)s.orientation == 0)
                {
                    for (int i = 0; i < s.Lives; i++)
                    {
                        if(b.y+i >= 0 && b.y+i <= 9 && b.x >= 0 && b.x <= 9)
                        {
                            buttons[b.y + i, b.x].Background = new SolidColorBrush(Colors.DarkGray) { Opacity = 75 };
                        }                        
                        
                        
                    }
                }
                if ((int)s.orientation == 1)
                {
                    for (int i = 0; i < s.Lives; i++)
                    {
                        if (b.y  >= 0 && b.y <= 9 && b.x+i >= 0 && b.x+i <= 9)
                        {
                            buttons[b.y, b.x + i].Background = new SolidColorBrush(Colors.DarkGray) { Opacity = 75 };
                        }
                            

                    }
                }
            }
            

        }

        private void ship_buttons_click(object sender, RoutedEventArgs e)
        {                       
            var b = (Modified_Button)sender;
            if(b.ship.positions.Count==0)
                current_ship = b.ship;
            if (!(b.ship.positions.Count == 0))
            {
                player.remove(b.ship);
                boatsplaced -= 1;
                current_ship = b.ship;
            }
                
        }

        private void ship_placement_click(object sender, RoutedEventArgs e)
        {
            var f = (Modified_Button)sender;
            if (current_ship == null)
            {
                MessageBox.Show("Please select a Ship first");
                return;
            }
            if (!player.Check_placeable(current_ship, f.x, f.y))
            {
                MessageBox.Show("You can't place this ship here!");
            }
            if (player.Check_placeable(current_ship, f.x, f.y))
            {
                current_ship.X = f.x;
                current_ship.Y = f.y;
                player.place(current_ship);
                boatsplaced += 1;
                current_ship = null;

            }
           

        }

        private string ToString(Type type)
        {
            switch (type)
            {
                case Type.Flugzeugtraeger:
                    return "Aircraft Carrier";
                case Type.UBoot:
                    return "Submarine";
                case Type.Kreuzer:
                    return "Cruiser";
                case Type.Schlachtschiff:
                    return "Battleship";
                case Type.Zerstörer:
                    return "Destroyer";
                default:
                    throw (new ArgumentException("That is not a valid ship-type"));
            }
        }

        private void Finish_click(object sender, RoutedEventArgs e)
        {
           if(boatsplaced== 6)
           {
                switch (game.gameMode)
                {
                    case GameMode.P_v_P:
                        if (secondplayer)
                        {
                            var place = new Ship_placement(game.players[1], false, game);
                            place.Show();
                        }
                        else
                        {
                            var fire = new Misslefiring(game);
                            fire.Show();
                        }
                        this.Close();
                        break;

                    case GameMode.P_v_Ai:
                        var fire_1 = new Misslefiring(game);
                        fire_1.Show();
                        this.Close();
                        break;

                    default: MessageBox.Show("Sorry something wemt wrong!"); break;


                }
            }
            else { MessageBox.Show("Please place all ships first!"); }
        }

        private void Rotate_click(object sender, RoutedEventArgs e)
        {
            if (current_ship == null)
                MessageBox.Show("Please select a Ship first");
            else
            {
                if (current_ship.orientation == Orientation.horizontal)
                    current_ship.orientation = Orientation.vertical;
                else { current_ship.orientation = Orientation.horizontal; }
            }
        }
    }
}
