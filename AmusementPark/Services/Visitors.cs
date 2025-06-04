using AmusementPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Services
{
    public class Visitors
    {
        public static int CalculateNumberVisitorEntry(Park park)
        {
            return new Random().Next(51) * park.PlacedBuilding.Count;
        }

        public static int CalculateNumberVisitorOut(int visitors)
        {
            return new Random().Next(visitors);
        }


    }
}
