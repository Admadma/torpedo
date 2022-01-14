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
    /// Interaction logic for ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {
        PvCViewModel vm;
        public ComputerWindow(PvCViewModel vm)
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
                    AIBoard.Children.Add(button);
                }
            }
            int[][] shipCoordinates = vm.getShips(1);
            int totalShipsLength = vm.numberOfP2ShipCoordinates;

            for (int i = 0; i < totalShipsLength; i++)
            {
                Button redButton = new Button();
                Grid.SetRow(redButton, shipCoordinates[i][1]);
                Grid.SetColumn(redButton, shipCoordinates[i][0]);
                redButton.Background = Brushes.Red;
                AIBoard.Children.Add(redButton);
            }
        }
    }
}
