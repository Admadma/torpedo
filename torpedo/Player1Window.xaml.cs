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
    /// Interaction logic for Player1Window.xaml
    /// </summary>
    public partial class Player1Window : Window
    {

        PvPViewModel vm;
        TempWindow tmpW;

        public void setParameters(PvPViewModel vm, TempWindow tmpW)//TODO: PvPViewModel helyett egy közös szülő osztály (pl GameViewModel) és ennek lehet majd értékül adni a leszármazottait: PvPViewModel/PvCViewModel
        {
            this.vm = vm;
            this.tmpW = tmpW;
        }

        private int _hits;
        private int _misses;
        private int _numberOfTurns;


        public Player1Window(PvPViewModel vm, TempWindow tmpW)
        {
            InitializeComponent();

            this.vm = vm;
            this.tmpW = tmpW;
            
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
        }

        public void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (vm.isMyTurn(0))
            {
                if (vm.isUntouchedCoordinate(Grid.GetColumn(button), Grid.GetRow(button)))
                {
                    //_numberOfTurns = vm.getTurns();
                    //turns.text = _numberOfTurns;
                    if (vm.isThereAShip(Grid.GetColumn(button), Grid.GetRow(button)))
                    {
                        _hits = vm.getHits(0);
                        player1Score.Text = _hits.ToString();
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
            tmpW.setParameters(this, null, vm);
            tmpW.isGameOver(0);
        }

        public void endTurnClicked(object sender, RoutedEventArgs e)
        {
            //tmpW.setParameters(this, p2w, vm);
            //tmpW.Show();
            //this.Hide();
            //tmpW = new TempWindow(this, p2w, vm);
            //tmpW.Show();
            //this.Close();
        }

        public void endGame()
        {
            MessageBox.Show("Player1 won!");
            this.Close();
        }

    }
}
