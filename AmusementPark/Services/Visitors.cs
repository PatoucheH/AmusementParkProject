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
        /// <summary>
        /// calculate the number of visitors who enters in the park
        /// </summary>
        /// <param name="park">The park use to play with</param>
        /// <returns>The number of visitors who enters in the park</returns>
        public static int CalculateNumberVisitorEntry(Park park)
        {
            return new Random().Next(51) * park.PlacedBuilding.Count;
        }


        public static int CalculateNumberVisitorInPark(Park park)
        {
            return park.VisitorsEntry - park.VisitorsOut;
        }


        /// <summary>
        /// calculate the number of visitors who leaves the park
        /// </summary>
        /// <param name="park">The park use to play with</param>
        /// <returns>The number of visitors who leaves in the park</returns>
        public static int CalculateNumberVisitorOut(Park park)
        {
            return new Random().Next(park.VisitorInPark);
        }

        public static void CalculateNumberVisitorInAttraction(Park park)
        {
            for (int i = 0; i < park.VisitorInPark; i++)
            {
                int randomInList = new Random().Next(park.PlacedBuilding.Count);

                park.PlacedBuilding[randomInList].VisitorInAttraction++;
            }
        }
    }
}
