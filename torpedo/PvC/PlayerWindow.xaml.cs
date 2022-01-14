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
using torpedo.ViewModels;

namespace torpedo.PvC
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        PvCViewModel vm;
        ComputerWindow cw;

        private int _hits;
        private int _misses;

        private int _enemyHits;
        private int _enemyMisses;

        private int _numberOfTurns;

        string playerName;
        public PlayerWindow(PvCViewModel vm)
        {
            InitializeComponent();

            this.vm = vm;

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button button = new Button();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Click += new RoutedEventHandler(buttonClicked);
                    Player1Attacks.Children.Add(button);
                }
            }

            playerName = vm.player1Name;
            PlayerAttacks.Text = playerName + " támad!";

            _hits = vm.getHits(0); ;
            _enemyHits = vm.getHits(1);
            _numberOfTurns = vm.numberOfTurns;
            numberOfTurns.Text = _numberOfTurns.ToString();
            playerHits.Text = _hits.ToString();
            enemyHits.Text = _enemyHits.ToString();
        }

        public void onKeyADown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.A)
            {
                cw = new ComputerWindow(vm);
                cw.Show();
            }
        }

        public void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (vm.isUntouchedCoordinate(Grid.GetColumn(button), Grid.GetRow(button)))
            {
                //_numberOfTurns = vm.getTurns();
                //turns.text = _numberOfTurns;
                if (vm.isThereAShip(Grid.GetColumn(button), Grid.GetRow(button)))
                {
                    
                    button.Background = Brushes.Red;
                    endTurn();
                }
                else
                {
                    button.Background = Brushes.Blue;
                    endTurn();
                }

            }
            else
            {
                MessageBox.Show("Ide már lőttél. Célozz másik mezőt!");
            }

            _hits = vm.getHits(0);
            _enemyHits = vm.getHits(1);
            _numberOfTurns = vm.numberOfTurns;
            playerHits.Text = _hits.ToString();
            enemyHits.Text = _enemyHits.ToString();
            numberOfTurns.Text = _numberOfTurns.ToString();
            
        }

        public void endTurn()
        {
            if (vm.endPlayerTurn())
            {
                MessageBox.Show("game over");
            }
        }
    }
}
