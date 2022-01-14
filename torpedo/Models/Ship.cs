using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torpedo.Models
{
    public class Ship
    {
        public int[,] shipPositions { get; set; }
        public int length { get; set; }

        public bool isSunk;

        public Ship(int[,] shipPositions, int shipLength)
        {
            this.shipPositions = shipPositions;
            this.length = shipLength;
            isSunk = false;
        }
    }
}
