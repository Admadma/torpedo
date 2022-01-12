using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torpedo.Models
{
    public class GameModel
    {
        
        private int _gameWidth = 3;
        private int _gameHeight = 3;
        //hit mindkét képernyőn(player/opponent) ugyan azt ábrázolja majd: egy eltalált hajót
        public enum fieldState { Untouched, Hit, Miss };
        public fieldState[,] fieldStates { get; set; }

        public Position[] ship1 { get; set; }
        public Position[] ship2 { get; set; }
        public Position[] ship3 { get; set; }
        public Position[] ship4 { get; set; }
        public Position[] ship5 { get; set; }



        public GameModel()
        {
            //initializing empty field
            fieldStates = new fieldState[_gameWidth, _gameHeight];

            for (int i = 0; i < _gameWidth; i++)
            {
                for(int j = 0; j < _gameHeight; j++)
                {
                    fieldStates[i,j] = fieldState.Untouched;
                }
            }

            fieldStates[0, 0] = fieldState.Hit;

            //initialize ships
            ship1 = new Position[2];
            ship2 = new Position[3];
            ship3 = new Position[3];
            ship4 = new Position[4];
            ship5 = new Position[5];
        }

        public fieldState getMyFieldState(Position position)
        {
            return fieldStates[position.X, position.Y];
        }

        public bool createShipAtCoordinates(Position startPosition, Position endPosition)
        {


            return true;
        }



    }
}
