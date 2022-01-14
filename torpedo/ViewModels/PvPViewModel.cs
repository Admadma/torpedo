using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using torpedo.Models;
using System.Xml;
using System.Xml.Linq;


namespace torpedo.ViewModels
{
    public class PvPViewModel
    {

        public string player1Name;
        public string player2Name;
        public string winner;






        public string Asd { get; set; }

        public int aasd { get; set; }

        private int _p1Hits { get; set; }
        private int _p2Hits { get; set; }
        private int _p1Misses { get; set; }
        private int _p2Misses { get; set; }

        private bool[,] wasCoordinateAlreadyAttackedByPlayer1 = new bool[10, 11];
        private bool[,] wasCoordinateAlreadyAttackedByPlayer2 = new bool[10, 11];

        private static int _maxNumberOfShipCoordinates = 17;        //TODO: 5 helyére majd 17 kell (összesen annyi hajó koordináta van)
        private int[][] shipCoordinatesPlayer1 = new int[_maxNumberOfShipCoordinates][];
        private int[][] shipCoordinatesPlayer2 = new int[_maxNumberOfShipCoordinates][];

        private int numberOfP1ShipCoordinates;
        private int numberOfP2ShipCoordinates;

        private int currentPlayer = 0;      //0 player1  1 player2


        public PvPViewModel(string player1Name, string player2Name)
        {
            for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
            {
                shipCoordinatesPlayer1[i] = new int[] { -1, -1 };
                shipCoordinatesPlayer2[i] = new int[] { -1, -1 };
            }
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
                for (int i = 0; i < shipLength; i++) //a hajó minden egyes pontján elvégzem a műveletet: hozzáadom  
                {
                    shipCoordinatesPlayer1[numberOfP1ShipCoordinates + i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                    //shipCoordinatesPlayer1[i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                }
                numberOfP1ShipCoordinates += shipLength;
            }
            else
            {
                for (int i = 0; i < shipLength; i++)
                {
                    shipCoordinatesPlayer2[numberOfP2ShipCoordinates + i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                    //shipCoordinatesPlayer2[i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                }
                numberOfP2ShipCoordinates += shipLength;
            }
        }

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

                return tempShips;
            }
        }

        public int getHits(int player)
        {
            if (player == 0)
            {
                return _p1Hits;
            }
            else
            {
                return _p2Hits;
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
                        //checkIfGameIsOver();
                        return true;
                    }
                }

                return false;
            }
            else
            {
                currentPlayer = 0;
                wasCoordinateAlreadyAttackedByPlayer2[x, y] = true;
                for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer1[i][0] == x && shipCoordinatesPlayer1[i][1] == y)
                    {
                        _p2Hits++;
                        //checkIfGameIsOver();
                        return true;
                    }
                }

                return false;
            }
        }

        public bool checkIfGameIsOver()
        {
            if (_p1Hits >= _maxNumberOfShipCoordinates)
            {
                //TODO: game over p1 won
                winner = player1Name;
                //exportScore();
                return true;
            }
            else if (_p2Hits >= _maxNumberOfShipCoordinates)
            {
                //TODO: game over p2 won
                winner = player2Name;
                //exportScore();
                return true;
            }
            return false;
        }
    }
}
