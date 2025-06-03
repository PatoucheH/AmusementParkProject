using AmusementPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Services
{
    public class EarnMoney
    {
        public static double EarnMoneyByVisitorEntry(int numberVisitor, Park park)
        {
            return numberVisitor * 25d * (park.PlacedBuilding.Count);
        }

        public static int CalculateNumberVisitor()
        {
            return new Random().Next(251);
        }
    }
}
