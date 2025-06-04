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
    public struct Position
    {
        public int X; public int Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
