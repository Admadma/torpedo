using System;
using System.Collections;
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
    /// Interaction logic for PlaceShips.xaml
    /// </summary>
    public partial class PlaceShips : Window
    {

        PvPViewModel vm;

        private int startX = -1;
        private int startY = -1;
        private int endX = -1;
        private int endY = -1;

        private int[][] shipCoordinates;

        private Button startButton;
        private Button endButton;

        public PlaceShips(PvPViewModel vm)
        {
            InitializeComponent();

            this.vm = vm;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button button = new Button();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Background = Brushes.LightGray;
                    button.Click += new RoutedEventHandler(buttonClicked);
                    Ships.Children.Add(button);
                }
            }
        }

        public void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if(startX == -1 && startY == -1)
            {
                startX = Grid.GetColumn(button);
                startY = Grid.GetRow(button);
                button.Background = Brushes.Red;
                startButton = button;
            }
            else if(endX == -1 && endY == -1)
            {
                endX = Grid.GetColumn(button);
                endY = Grid.GetRow(button);
                button.Background = Brushes.Red;
                endButton = button;
                placeShip();
            }

        }

        private void placeShip()
        {
            if (canShipPlacedThere())
            {

            }
            else
            {
                startButton.Background = Brushes.LightGray; ;
                endButton.Background = Brushes.LightGray; ;
                startX = -1;
                startY = -1;
                endX = -1;
                endY = -1;
                MessageBox.Show("Érvénytelen hajó");        //canShipPlacedThere-ben vizsgálni minden hibát (dimenzió, ütközés, hossz) külön hibaüzenet
            }
        }

        private bool canShipPlacedThere()
        {
            if (startX == endX || startY == endY)        //Ha egy tengely egyenlő akkor biztos egy síkban vannak //NEM lehet átlós hajó
            {
                MessageBox.Show("good");
                if (areShipsColliding())
                {

                }
            }
            else
            {
                MessageBox.Show("bad");
                return false;
            }
            return true;
        }

        private void getNewShipCoordinates()
        {
            int realStartX;
            int realStartY;
            int realEndX;
            int realEndY;
            ArrayList shipParts = new ArrayList();

            //x1 -től kezdve xn-ig minden értéket hozzáadni
            //ugyanez y-al
            shipParts.Add(new int[] { 1, 1 });
        }

        private bool areShipsColliding()
        {
            //TODO: vm-ből lekérni az összes hajó koordinátáját, összvetni
            //kezdő és végpont között minden egyes koordinátát vizsgálni

            return true;
        }
    }
}
