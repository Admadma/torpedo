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

        public void setParameters(PvPViewModel vm, TempWindow tmpW)//TODO: PvPViewModel helyett egy közös szülő osztály (pl GameViewModel) és ennek lehet majd értékül adni a leszármazottait: PvPViewModel/PvCViewModel
        {
            this.vm = vm;
            this.tmpW = tmpW;
        }

        public Player2Window(PvPViewModel vm, TempWindow tmpW)
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
                    Player2Attacks.Children.Add(button);
                }
            }

            //Score = 0;
        }

        public void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (vm.isMyTurn(1))
            {
                if (vm.isUntouchedCoordinate(Grid.GetColumn(button), Grid.GetRow(button)))
                {
                    if (vm.isThereAShip(Grid.GetColumn(button), Grid.GetRow(button)))
                    {
                        //pontok kiszámítása a model-ben történik, itt csak a model-ből kérem majd le az aktuális értékeket
                        //Score++;
                        //_hits = vm.getHits(true);
                        //player1Score.Text = _hits.ToString();
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
