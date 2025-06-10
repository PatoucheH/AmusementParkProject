using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// Structs to get the position of each buiulding in the park's map
    /// </summary>
    public class Position
    {
        public int X { get; set; } 
        public int Y { get; set; } 

        public Position()
        {
            X = 0;
            Y = 0;
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
