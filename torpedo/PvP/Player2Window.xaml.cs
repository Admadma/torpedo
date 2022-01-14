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

namespace torpedo
{
    /// <summary>
    /// Interaction logic for Player2Window.xaml
    /// </summary>
    public partial class Player2Window : Window
    {

        PvPViewModel vm;
        TempWindow tmpW;

        private int _hits;
        private int _misses;
        private int _numberOfTurns;

        private int _enemyHits;
        private int _enemyMisses;

        public void setParameters(PvPViewModel vm, TempWindow tmpW)
        {
            this.vm = vm;
            this.tmpW = tmpW;
        }

        public Player2Window(PvPViewModel vm, TempWindow tmpW)
        {
            InitializeComponent();

            this.vm = vm;
            this.tmpW = tmpW;

            player2Board.Text = vm.player2Name + " támad.";


            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button button = new Button();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Click += new RoutedEventHandler(buttonClicked);
                    Player2Attacks.Children.Add(button);
                }
            }
        }

        public void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (vm.isMyTurn(1))
            {
                if (vm.isUntouchedCoordinate(Grid.GetColumn(button), Grid.GetRow(button)))
                {
                    _hits = vm.getHits(1);
                    playerHits.Text = _hits.ToString();
                    _enemyHits = vm.getHits(0);
                    enemyHits.Text = _enemyHits.ToString();
                    _numberOfTurns = vm.numberOfTurns;
                    numberOfTurns.Text = _numberOfTurns.ToString();
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
            }
            else
            {
                MessageBox.Show("Most nem a te köröd van!");
            }
        }

        public void endTurn()
        {
            this.Hide();
            tmpW.setParameters(null, this, vm);
            tmpW.isGameOver(1);
        }
    }
}
