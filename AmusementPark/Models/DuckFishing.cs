using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// This class inherits from attractions to create a DuckFishing class with same properties
    /// </summary>
        internal class DuckFishing :Attractions
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 3_000;
        public override string? Emoji { get; init; } = ":duck:";
        public override string? Type { get; init; } = "Child";
        public Position? Ordinal { get; set; }
        public override double MaintenancePrice { get; init;} = 100;
        public override string? Description { get; init; } = "The best attractions CATCH the little ducks !!!";
        public DuckFishing(string name)
        {
            Name = name;
        }
    }
}
