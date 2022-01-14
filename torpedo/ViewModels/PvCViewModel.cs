using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torpedo.ViewModels
{

    public class PvCViewModel
    {
        public string player1Name;
        public string player2Name = "AI";
        public string winner;

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

        private int currentPlayer = 0;      //0 player1  1 player2

        public PvCViewModel()
        {
            for (int i = 0; i < _maxNumberOfShipCoordinates; i++)
            {
                shipCoordinatesPlayer1[i] = new int[] { -1, -1 };
                shipCoordinatesPlayer2[i] = new int[] { -1, -1 };
            }

        }
        //saját pályámat vizsgálom, az AI ne ezzel kérdezze le hogy van-e ott találata
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
            }
            else
            {
                for (int i = 0; i < shipLength; i++)
                {
                    shipCoordinatesPlayer2[numberOfP2ShipCoordinates + i] = new int[] { shipPositions[i, 0], shipPositions[i, 1] };
                }
                numberOfP2ShipCoordinates += shipLength;
            }
        }

    }
}
