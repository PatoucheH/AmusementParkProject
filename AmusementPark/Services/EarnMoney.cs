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

        public static void GenerateMoney(Park park)
        {
            _ = Task.Run(async () =>
                {
                    while (true)
                    {
                        lock (_lock)
                        {
                            double maintenanceTotal = 0d;
                            foreach (var building in park.PlacedBuilding)
                                maintenanceTotal += building.MaintenancePrice;

                            park.VisitorsEntry = Visitors.CalculateNumberVisitorEntry();
                            park.VisitorsOut = Visitors.CalculateNumberVisitorOut(park.VisitorsEntry);

                            double moneyEarned = EarnMoney.EarnMoneyByVisitorEntry(park);
                            park.Budget += moneyEarned;
                            park.Budget -= maintenanceTotal;
                        }
                        await Task.Delay(5_000);
                    }
                });
        }
        public static object GetLock() => _lock;
    }
}
