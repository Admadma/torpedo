using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using torpedo.Models;


namespace torpedo.ViewModels
{
    public class PvPViewModel
    {

        public string player1Name;
        public string player2Name;
        public string winner;
        public int numberOfTurns;




        private int _p1Hits { get; set; }
        private int _p2Hits { get; set; }
        private int _p1Misses { get; set; }
        private int _p2Misses { get; set; }

        private bool[,] wasCoordinateAlreadyAttackedByPlayer1 = new bool[10, 11];
        private bool[,] wasCoordinateAlreadyAttackedByPlayer2 = new bool[10, 11];

        private static int _maxNumberOfShipCoordinates = 17;
        private int[][] shipCoordinatesPlayer1 = new int[_maxNumberOfShipCoordinates][];
        private int[][] shipCoordinatesPlayer2 = new int[_maxNumberOfShipCoordinates][];

        private int numberOfP1ShipCoordinates;
        private int numberOfP2ShipCoordinates;

        public Ship[] separateShipsP1 = new Ship[5];
        public Ship[] separateShipsP2 = new Ship[5];



        private int currentPlayer = 0;      //0 player1  1 player2


        public PvPViewModel()
        {
            for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
            {
                shipCoordinatesPlayer1[i] = new int[] { -1, -1 };
                shipCoordinatesPlayer2[i] = new int[] { -1, -1 };
            }

            //_p2Hits = 100;

            /*
            shipCoordinatesPlayer1[0] = new int[] { 1, 1 };
            shipCoordinatesPlayer1[1] = new int[] { 1, 2 };
            shipCoordinatesPlayer1[2] = new int[] { 1, 3 };
            shipCoordinatesPlayer1[3] = new int[] { 1, 4 };
            numberOfP1ShipCoordinates += 4;
            */
            /*
            shipCoordinatesPlayer1[0] = new int[] { 1, 1 };
            shipCoordinatesPlayer1[1] = new int[] { 1, 2 };
            shipCoordinatesPlayer1[2] = new int[] { 1, 3 };
            shipCoordinatesPlayer1[3] = new int[] { 1, 4 };
            shipCoordinatesPlayer1[4] = new int[] { 1, 5 };

            shipCoordinatesPlayer2[0] = new int[] { 2, 1 };
            shipCoordinatesPlayer2[1] = new int[] { 2, 2 };
            shipCoordinatesPlayer2[2] = new int[] { 2, 3 };
            shipCoordinatesPlayer2[3] = new int[] { 2, 4 };
            shipCoordinatesPlayer2[4] = new int[] { 2, 5 };
            */

        }

        public bool isthereShipAtCoordinate(int x, int y, int playerID)
        {

            if (playerID == 0)
            {
                for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer1[i][0] == x && shipCoordinatesPlayer1[i][1] == y)
                    {
                        return true;
                    }
                }
            }
            else if (playerID == 1)
            {
                for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer2[i][0] == x && shipCoordinatesPlayer2[i][1] == y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void addShip(int[,] shipPositions, int shipLength, int playerID)
        {
            if (playerID == 0)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    shipCoordinatesPlayer1[numberOfP1ShipCoordinates + i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                }
                numberOfP1ShipCoordinates += shipLength;
                addShipToSeparateShips(shipPositions, shipLength, playerID);
            }
            else
            {
                for (int i = 0; i < shipLength; i++)
                {
                    shipCoordinatesPlayer2[numberOfP2ShipCoordinates + i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                }
                numberOfP2ShipCoordinates += shipLength;
                addShipToSeparateShips(shipPositions, shipLength, playerID);
            }
        }

        public void addShipToSeparateShips(int[,] shipPositions, int shipLength, int playerID)
        {
            if (playerID == 0)
            {
                if (shipLength == 2)
                {
                    separateShipsP1[0] = new Ship(shipPositions, shipLength);
                }
                else if (shipLength == 3)
                {
                    if (separateShipsP1[1] is null)
                    {
                        separateShipsP1[1] = new Ship(shipPositions, shipLength);
                    }
                    else
                    {
                        separateShipsP1[2] = new Ship(shipPositions, shipLength);
                    }

                }
                else if (shipLength == 4)
                {
                    separateShipsP1[3] = new Ship(shipPositions, shipLength);
                }
                else
                {
                    separateShipsP1[4] = new Ship(shipPositions, shipLength);
                }
            }
            else
            {
                if (shipLength == 2)
                {
                    separateShipsP2[0] = new Ship(shipPositions, shipLength);
                }
                else if (shipLength == 3)
                {
                    if (separateShipsP2[1] is null)
                    {
                        separateShipsP2[1] = new Ship(shipPositions, shipLength);
                    }
                    else
                    {
                        separateShipsP2[2] = new Ship(shipPositions, shipLength);
                    }

                }
                else if (shipLength == 4)
                {
                    separateShipsP2[3] = new Ship(shipPositions, shipLength);
                }
                else
                {
                    separateShipsP2[4] = new Ship(shipPositions, shipLength);
                }
            }
        }
        /*
        public void checkIfSeparateShipSunk(int playerID)
        {
            //TODO: adott player minden egyes különálló hajóját megvizsgálni,      isthereShipAtCoordinate -el az aktuális player oldalán nézhetem, hogy a hajói hogy állnak 
            if(playerID == 0)
            {
                for(int i = 0; i < 5; i++)
                {
                    checkSeparateShipCoordinateForCollisions(separateShipsP1[i].shipPositions, separateShipsP1[i].length);
                }
            }
            else
            {

            }
        }

        public bool checkSeparateShipCoordinateForCollisions(int[,] shipPositions, int length)
        {
            for(int i = 0; i < length; i++)
            {
                if(!isthereShipAtCoordinate(shipPositions[i, 0], shipPositions[i, 1], 0))       //rossz: ez a hajók helye, nem a találatoké. találatok koordinátái nincsenek tárolva
                {
                    return false;
                }
            }

            return false;
        }
        */

        public int[][] getShips(int playerID)
        {
            int[][] tempShips = new int[numberOfP1ShipCoordinates][];
            for (int i = 0; i < numberOfP1ShipCoordinates; i++)
            {
                tempShips[i] = new int[] { -1, -1 };
            }

            if (playerID == 0)
            {
                for (int i = 0; i < numberOfP1ShipCoordinates; i++)
                {
                    tempShips[i][0] = shipCoordinatesPlayer1[i][0];
                    tempShips[i][1] = shipCoordinatesPlayer1[i][1];
                }
                return tempShips;
            }
            else
            {
                for (int i = 0; i < numberOfP2ShipCoordinates; i++)
                {
                    tempShips[i][0] = shipCoordinatesPlayer2[i][0];
                    tempShips[i][1] = shipCoordinatesPlayer2[i][1];
                }
                return tempShips;
            }
        }

        public int getHits(int playerID)
        {
            if (playerID == 0)
            {
                return _p1Hits;
            }
            else
            {
                return _p2Hits;
            }
        }
        public int getMisses(int playerID)
        {
            if (playerID == 0)
            {
                return _p1Misses;
            }
            else
            {
                return _p2Misses;
            }
        }

        public bool isMyTurn(int playerNumber)
        {
            return playerNumber == currentPlayer;
        }

        public int getCurrentPlayer()
        {
            return currentPlayer;
        }

        public bool isUntouchedCoordinate(int x, int y)
        {
            if (currentPlayer == 0)
            {
                if (wasCoordinateAlreadyAttackedByPlayer1[x, y])
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (wasCoordinateAlreadyAttackedByPlayer2[x, y])
                {
                    return false;
                }
                return true;
            }
        }

        public bool isThereAShip(int x, int y)
        {
            if (currentPlayer == 0)
            {
                currentPlayer = 1;
                wasCoordinateAlreadyAttackedByPlayer1[x, y] = true;
                for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer2[i][0] == x && shipCoordinatesPlayer2[i][1] == y)
                    {
                        _p1Hits++;
                        //checkIfSeparateShipSunk(0);
                        return true;
                    }
                }
                _p1Misses++;
                return false;
            }
            else
            {
                numberOfTurns++;

                currentPlayer = 0;
                wasCoordinateAlreadyAttackedByPlayer2[x, y] = true;
                for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer1[i][0] == x && shipCoordinatesPlayer1[i][1] == y)
                    {
                        _p2Hits++;
                        //checkIfSeparateShipSunk(1);
                        return true;
                    }
                }
                _p1Misses++;
                return false;
            }
        }

        public bool checkIfGameIsOver()
        {
            if (_p1Hits >= _maxNumberOfShipCoordinates)
            {
                winner = player1Name;
                return true;
            }
            else if (_p2Hits >= _maxNumberOfShipCoordinates)
            {
                winner = player2Name;
                return true;
            }
            return false;
        }
    }
}
