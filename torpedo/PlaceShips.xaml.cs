﻿using System;
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

        private int currentPlayerID = 0;

        private int shipLength;

        private int startX = -1;
        private int startY = -1;
        private int endX = -1;
        private int endY = -1;

        private int currentShipsLength;
        private int totalShipsLength;       //nem kérem le a model-ből, itt adom hozzá folyamatosan a currentShipsLength értékét 

        private int[][] shipCoordinates;

        private Button startButton;
        private Button endButton;

        public PlaceShips(PvPViewModel vm)
        {
            InitializeComponent();

            this.vm = vm;

            player1PlaceShip.Text = vm.player1Name + " helyezze el a hajóit!";

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
            player1PlaceShip.Text = vm.player1Name + " válasszon ki 2 egymás után köetkező mezőt!";
        }
        public void onShip2Button(object sender, RoutedEventArgs e)
        {
            shipLength = 3;
            Button button = sender as Button;
            button.IsEnabled = false;
        }
        public void onShip3Button(object sender, RoutedEventArgs e)
        {
            shipLength = 3;
            Button button = sender as Button;
            button.IsEnabled = false;
        }
        public void onShip4Button(object sender, RoutedEventArgs e)
        {
            shipLength = 4;
            Button button = sender as Button;
            button.IsEnabled = false;
        }
        public void onShip5Button(object sender, RoutedEventArgs e)
        {
            shipLength = 5;
            Button button = sender as Button;
            button.IsEnabled = false;
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
                int[,] ships = getNewShipCoordinates();

                //MessageBox.Show($"{currentShipsLength}");
                /*for(int i = 0; i < currentShipsLength; i++)
                {
                    MessageBox.Show($"X: {ships[i,0]}  Y: {ships[i, 1]}");
                }*/

                vm.addShip(getNewShipCoordinates(), currentShipsLength, currentPlayerID);
                totalShipsLength += currentShipsLength;

                updateShipColors();
                startX = -1;
                startY = -1;
                endX = -1;
                endY = -1;
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
                            if(shipLength == currentShipsLength)
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
                if(startY > endY)
                {
                    int tmp = endY;
                    endY = startY;
                    startY = tmp;
                }

                currentShipsLength = endY - startY + 1;

                shipParts = new int[currentShipsLength, 2];

                for(int i = 0; i < currentShipsLength; i++)
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

            for(int i = 0; i < currentShipsLength; i++)
            {
                if(vm.isthereShipAtCoordinate(shipParts[i, 0], shipParts[i, 1], currentPlayerID))
                {
                    return true;
                }
            }
            return false;
        }

        private void updateShipColors()
        {
            //TODO: lekérni az összes hajó koordináját -> ezekre rakni egy új, piros gombot
            int[][] shipCoordinates = vm.getShips(currentPlayerID);
            //MessageBox.Show($" x: {shipCoordinates[0][0]}\n y: {shipCoordinates[0][1]}");

            for(int i = 0; i < totalShipsLength; i++)
            {
                //MessageBox.Show($" x: {shipCoordinates[i][0]}\n y: {shipCoordinates[i][1]}");
                
                Button button = new Button();
                Grid.SetRow(button, shipCoordinates[i][1]);
                Grid.SetColumn(button, shipCoordinates[i][0]);
                button.Background = Brushes.Red;
                button.Click += new RoutedEventHandler(buttonClicked);
                Ships.Children.Add(button);
                


            }

        }
    }
}