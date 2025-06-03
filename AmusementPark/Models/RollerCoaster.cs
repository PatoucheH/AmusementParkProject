using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    public class RollerCoaster : Attractions
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 50_000;
        public override string? Emoji { get; init; } = ":roller_coaster:";
        public override string? Type { get; init; } = "Thrill";
        public Position? Ordinal { get; set; }

        public RollerCoaster(string name)
        {
            Name = name;
        }
    }
}
