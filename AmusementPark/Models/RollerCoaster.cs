using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// This class inherits from attractions to create a RollerCoaster class with same properties
    /// </summary>
    public class RollerCoaster : Attractions
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 40_000;
        public override string? Emoji { get; init; } = ":roller_coaster:";
        public override string? Type { get; init; } = "Thrill";
        public override Position Ordinal { get; set; }
        public override double MaintenancePrice { get; init; } = 500;
        public override string? Description { get; init; } = "I don't know what to say about this attractions ..... Try it !!";
        public override int VisitorInAttraction { get; set; }

        public RollerCoaster(string name)
        {
            Name = name;
        }
    }
}
