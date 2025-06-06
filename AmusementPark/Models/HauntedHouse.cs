using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// This class inherits from attractions to create a HauntedHouse class with same properties
    /// </summary>
    public class HauntedHouse : Attractions
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 30_000;
        public override string? Emoji { get; init; } = ":ghost:";
        public override string? Type { get; init; } = "Thrill";
        public Position? Ordinal { get; set; }
        public override double MaintenancePrice { get; init; } = 250;
        public override string? Description { get; init; } = "The best ghost in this world is hide in this hous let's find him !";
        public override int VisitorInAttraction { get; set; }
        public HauntedHouse(string name)
        {
            Name = name;
        }
    }
}
