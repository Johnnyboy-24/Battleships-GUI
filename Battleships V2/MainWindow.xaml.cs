using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Battleships_V2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameMode gameMode = GameMode.tba;
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            switch (radiobutton.Content)
            {
                case "Player vs. Player":
                    gameMode = GameMode.P_v_P;

                    break;
                case "Player vs. Computer (Please select a difficulty fo Player 2!)":
                    gameMode = GameMode.P_v_Ai;

                    break;
                case "Computer vs. Computer (Please selct a difficulty for both players!)":
                    gameMode = GameMode.Ai_v_Ai;

                    break;
                default: MessageBox.Show("Sorry something went wrong!"); break;

            }

        }

        private void Fertig_Click(object sender, RoutedEventArgs e)
        {
            if (gameMode == GameMode.tba)
            {
                MessageBox.Show("Please select a GameMode!");
                return;
            }

            game = new Game(gameMode, Name1.Text, Name2.Text);
            switch (game.gameMode)
            {
                case GameMode.P_v_P:
                    apply_difficulty();
                    var place = new Ship_placement(game.players[0], true, game);
                    place.Show();
                    var rules = new Rules();
                    rules.Show();
                    break;
                case GameMode.P_v_Ai:
                    apply_difficulty();
                    var place_1 = new Ship_placement(game.players[0], false, game);
                    place_1.Show();
                    game.players[1].placeships();
                    var rules_1 = new Rules();
                    rules_1.Show();
                    break;
                case GameMode.Ai_v_Ai:
                    apply_difficulty();
                    game.players[0].placeships();
                    game.players[1].placeships();
                    var fire = new Misslefiring(game);
                    fire.Show();
                    break;
                default: MessageBox.Show("Sorry something wemt wrong!"); break;


            }


        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var box = (TextBox)sender;
            box.Text = "";
        }

        private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var box = (TextBox)sender;
            box.Clear();
        }
        private void apply_difficulty()
        {
            if (!game.players[0].human)
            {
                switch (Difficulty_player_1.SelectedIndex)
                {
                    case 0: ((Computer_Player)game.players[0]).difficulty = Difficulty.easier; break;
                    case 1: ((Computer_Player)game.players[0]).difficulty = Difficulty.harder; break;
                    default: ((Computer_Player)game.players[0]).difficulty = Difficulty.easier; break;
                }
            }
            if (!game.players[1].human)
            {
                switch (Difficulty_player_1.SelectedIndex)
                {
                    case 0: ((Computer_Player)game.players[1]).difficulty = Difficulty.easier; break;
                    case 1: ((Computer_Player)game.players[1]).difficulty = Difficulty.harder; break;
                    default: ((Computer_Player)game.players[1]).difficulty = Difficulty.harder; break;
                }
            }
        }
    }

}
