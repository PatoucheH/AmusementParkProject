using AmusementPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Services
{
    public static class EarnMoney
    {
        private static readonly object _lock = new();
        public static double EarnMoneyByVisitorEntry(Park park)
        {
            return park.VisitorsEntry * 25d * (park.PlacedBuilding.Count);
        }

        public static void GenerateMoneyAndVisitors(Park park)
        {
                        lock (_lock)
                        {
                            double maintenanceTotal = 0d;
                            foreach (var building in park.PlacedBuilding)
                                maintenanceTotal += building.MaintenancePrice;

                            int visitorsIn = Visitors.CalculateNumberVisitorEntry(park);

                            park.VisitorsEntry = visitorsIn;
                            park.VisitorsOut = Visitors.CalculateNumberVisitorOut(park.VisitorsEntry);
                            park.TotalVisitors += visitorsIn;

                            double moneyEarned = EarnMoney.EarnMoneyByVisitorEntry(park);
                            park.Budget += moneyEarned;
                            park.Budget -= maintenanceTotal;
                        }
        }
        public static object GetLock() => _lock;
    }
}
