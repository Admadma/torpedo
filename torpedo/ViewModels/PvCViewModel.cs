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
        public int numberOfTurns;

        Random r = new Random();

        private int _p1Hits { get; set; }
        private int _p2Hits { get; set; }
        private int _p1Misses { get; set; }
        private int _p2Misses { get; set; }

        private bool[,] wasCoordinateAlreadyAttackedByPlayer1 = new bool[10, 11];
        private bool[,] wasCoordinateAlreadyAttackedByPlayer2 = new bool[10, 11];

        private static int _maxNumberOfShipCoordinates = 17;
        private int[][] shipCoordinatesPlayer1 = new int[_maxNumberOfShipCoordinates][];
        private int[][] shipCoordinatesPlayer2 = new int[_maxNumberOfShipCoordinates][];

        public int numberOfP1ShipCoordinates;
        public int numberOfP2ShipCoordinates;

        private int currentPlayer = 0;      //0 player1  1 player2


        //AI
        private int[] foundShipCoordinate = new int[2] { -1, -1 };
        private int[] nextFoundShipCoordinate = new int[2] { -1, -1 };

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
                        return true;
                    }
                }
                _p2Misses++;
                return false;
            }
        }

        public bool endPlayerTurn()     //return true if AI won in this turn
        {
            //TODO: AI attack
            aiAttack();

            return false;
        }

        public void aiAttack()  //majd bool true- értéket ad vissza, ha megnyerte a játékot
        {
            if(foundShipCoordinate[0] == -1 && foundShipCoordinate[1] == -1)
            {
                seekRandomCoordinate();
            }
            else
            {
                findShipAroundCoordinate();
                //TODO: if(isGameWon())
                //TODO: else foundShipCoordinate[0] = -1; foundShipCoordinate[1] = -1;
            }
        }

        public void seekRandomCoordinate()
        {
            int x;
            int y;
            do
            {
                x = r.Next(0, 10);
                y = r.Next(0, 11);
                //addig keresek amíg nem találok érintetlen mezőt
            } while (!isUntouchedCoordinate(x, y));      //currentPlayer alapján nézi melyik játékos támadta-e meg

            if(isThereAShip(x, y))
            {
                foundShipCoordinate[0] = x;
                foundShipCoordinate[1] = y;
            }
        }

        public void findShipAroundCoordinate()
        {
            if(foundShipCoordinate[0] == 0)
            {
                if(foundShipCoordinate[1] == 0)
                {
                    if(isUntouchedCoordinate(foundShipCoordinate[0] + 1, foundShipCoordinate[1]))
                    {
                        if(isThereAShip(foundShipCoordinate[0] + 1, foundShipCoordinate[1]))
                        {
                            nextFoundShipCoordinate[0] = foundShipCoordinate[0] + 1;
                            nextFoundShipCoordinate[1] = foundShipCoordinate[1];
                        }
                    }
                    //TODO:vizsgálom jobbra
                    //ha azt a mezőt már egyszer néztem akkor vizsgálom lefelé
                }
            }
        }

    }
}
