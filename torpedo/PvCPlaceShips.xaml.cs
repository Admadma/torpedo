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
    /// Interaction logic for PvCPlaceShips.xaml
    /// </summary>
    public partial class PvCPlaceShips : Window
    {
        PvCViewModel vm;

        private int currentPlayerID;

        private int shipLength;

        private int startX = -1;
        private int startY = -1;
        private int endX = -1;
        private int endY = -1;

        private int currentShipsLength;
        private int totalShipsLength;

        private string playerName;

        private Button startButton;
        private Button endButton;

        public PvCPlaceShips(PvCViewModel vm)
        {
            InitializeComponent();

            this.vm = vm;
            playerName = vm.player1Name;
            playerPlaceShip.Text = playerName + " helyezze el a hajóit!";

            for (int i = 0; i < 11; i++)
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

        public void onShip1Button(object sender, RoutedEventArgs e)
        {
            shipLength = 2;
            Button button = sender as Button;
            button.IsEnabled = false;
            playerPlaceShip.Text = playerName + " válasszon ki 2 egymás után köetkező mezőt!";
        }
        public void onShip2Button(object sender, RoutedEventArgs e)
        {
            shipLength = 3;
            Button button = sender as Button;
            button.IsEnabled = false;
            playerPlaceShip.Text = playerName + " válasszon ki 3 egymás után köetkező mezőt!";
        }
        public void onShip3Button(object sender, RoutedEventArgs e)
        {
            shipLength = 3;
            Button button = sender as Button;
            button.IsEnabled = false;
            playerPlaceShip.Text = playerName + " válasszon ki 3 egymás után köetkező mezőt!";
        }
        public void onShip4Button(object sender, RoutedEventArgs e)
        {
            shipLength = 4;
            Button button = sender as Button;
            button.IsEnabled = false;
            playerPlaceShip.Text = playerName + " válasszon ki 4 egymás után köetkező mezőt!";
        }
        public void onShip5Button(object sender, RoutedEventArgs e)
        {
            shipLength = 5;
            Button button = sender as Button;
            button.IsEnabled = false;
            playerPlaceShip.Text = playerName + " válasszon ki 5 egymás után köetkező mezőt!";
        }

        public void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (startX == -1 && startY == -1)
            {
                startX = Grid.GetColumn(button);
                startY = Grid.GetRow(button);
                button.Background = Brushes.Red;
                startButton = button;
            }
            else if (endX == -1 && endY == -1)
            {
                endX = Grid.GetColumn(button);
                endY = Grid.GetRow(button);
                button.Background = Brushes.Red;
                endButton = button;
                //placeShip();
            }
        }/*

        private void placeShip()
        {
            if (canShipPlacedThere())
            {
                int[,] ships = getNewShipCoordinates();

                vm.addShip(getNewShipCoordinates(), currentShipsLength, currentPlayerID);
                totalShipsLength += currentShipsLength;

                updateShipColors();
                startX = -1;
                startY = -1;
                endX = -1;
                endY = -1;

                if (totalShipsLength >= 17)
                {
                    //TODO: kezdés random legyen

                    if (currentPlayerID == 0)
                    {
                        PlaceShips placePlayer2Ships = new PlaceShips(vm, 1);
                        placePlayer2Ships.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        placePlayer2Ships.Show();
                        this.Hide();
                    }
                    else
                    {
                        TempWindow tmpW = new TempWindow();
                        Player1Window p1w = new Player1Window(vm, tmpW);
                        Player2Window p2w = new Player2Window(vm, tmpW);

                        tmpW = new TempWindow(p1w, p2w, vm);

                        p1w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        p2w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        tmpW.WindowStartupLocation = WindowStartupLocation.CenterScreen;


                        tmpW.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                startButton.Background = Brushes.LightGray; ;
                endButton.Background = Brushes.LightGray; ;
                startX = -1;
                startY = -1;
                endX = -1;
                endY = -1;
            }
        }

        private bool canShipPlacedThere()
        {
            if (shipLength != 0)
            {
                if (startX == endX && startY == endY)
                {
                    return false;
                }
                else
                {
                    if (startX == endX || startY == endY)
                    {
                        if (!areShipsColliding())
                        {
                            if (shipLength == currentShipsLength)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"Válassz ki {shipLength} egymás után következő mezőt!");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ez ütközne egy másik hajóval. Tedd máshova!");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hajókat csak függőlegesen vagy vízszintesen lehet elhelyezni!");
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Előbb válassz hajót!");
                return false;
            }
        }

        private int[,] getNewShipCoordinates()
        {
            int[,] shipParts;

            if (startX == endX)
            {
                if (startY > endY)
                {
                    int tmp = endY;
                    endY = startY;
                    startY = tmp;
                }

                currentShipsLength = endY - startY + 1;

                shipParts = new int[currentShipsLength, 2];

                for (int i = 0; i < currentShipsLength; i++)
                {
                    shipParts[i, 0] = startX;
                    shipParts[i, 1] = startY + i;
                }

                return shipParts;
            }
            else  //implicit startY == endY
            {
                if (startX > endX)
                {
                    int tmp = endX;
                    endX = startX;
                    startX = tmp;
                }
                currentShipsLength = endX - startX + 1;

                shipParts = new int[currentShipsLength, 2];

                for (int i = 0; i < currentShipsLength; i++)
                {
                    shipParts[i, 0] = startX + i;
                    shipParts[i, 1] = startY;
                }

                return shipParts;
            }

        }

        private bool areShipsColliding()
        {
            int[,] shipParts = getNewShipCoordinates();

            for (int i = 0; i < currentShipsLength; i++)
            {
                if (vm.isthereShipAtCoordinate(shipParts[i, 0], shipParts[i, 1], currentPlayerID))
                {
                    return true;
                }
            }
            return false;
        }

        private void updateShipColors()
        {
            int[][] shipCoordinates = vm.getShips(currentPlayerID);

            for (int i = 0; i < totalShipsLength; i++)
            {
                Button button = new Button();
                Grid.SetRow(button, shipCoordinates[i][1]);
                Grid.SetColumn(button, shipCoordinates[i][0]);
                button.Background = Brushes.Red;
                button.Click += new RoutedEventHandler(buttonClicked);
                Ships.Children.Add(button);
            }

        }
    }*/
}
}
