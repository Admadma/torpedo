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
        //hit mindkét képernyő(player/opponent) ugyan azt ábrázolja majd: egy eltalált hajó
        public enum fieldState { Untouched, Hit, Miss };
        //public fieldState[][] fieldStates { get; set; }
        public fieldState[,] fieldStates { get; set; }

        public GameModel()
        {
            fieldStates = new fieldState[_gameWidth, _gameHeight];

            for (int i = 0; i < _gameWidth; i++)
            {
                for(int j = 0; j < _gameHeight; j++)
                {
                    fieldStates[i,j] = fieldState.Untouched;
                }
            }

            fieldStates[0, 0] = fieldState.Hit;
        }

        public fieldState getMyFieldState(Position position)
        {
            return fieldStates[position.X, position.Y];
        }

        public void asd()
        {
            
        }

    }
}
