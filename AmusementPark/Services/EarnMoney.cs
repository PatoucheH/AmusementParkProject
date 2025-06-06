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
        /// <summary>
        /// create an private object to secure the acces 
        /// </summary>
        private static readonly object _lock = new();

        /// <summary>
        /// calculate the money earn by the park by user entries
        /// </summary>
        /// <param name="park">The park of the user </param>
        /// <returns>A <see cref="double"/>The money make by the user entries</returns>
        public static double EarnMoneyByVisitorEntry(Park park)
        {
            return park.VisitorsEntry * 25d * (park.PlacedBuilding.Count);
        }

        /// <summary>
        /// method to calculate money and visitors in back 
        /// </summary>
        /// <param name="park">The park of the user </param>
        public static void GenerateMoneyAndVisitors(Park park)
        {
                        lock (_lock)
                        {
                            double maintenanceTotal = 0d;
                            foreach (var building in park.PlacedBuilding)
                                maintenanceTotal += building.MaintenancePrice;

                            int visitorsIn = Visitors.CalculateNumberVisitorEntry(park);

                            park.VisitorsEntry = visitorsIn;
                            park.VisitorsOut = Visitors.CalculateNumberVisitorOut(park);
                            park.VisitorInPark += Visitors.CalculateNumberVisitorInPark(park);
                            park.TotalVisitors += visitorsIn;

                            double moneyEarned = EarnMoney.EarnMoneyByVisitorEntry(park);
                            park.Budget += moneyEarned;
                            park.Budget -= maintenanceTotal;
                        }
        }
       /// <summary>
       /// Retrieves a shared synchronization object used for thread-safe operations.
       /// </summary>
       /// <returns>The synchronization object that can be used to coordinate access to shared resources.</returns>
        public static object GetLock() => _lock;
    }
}
