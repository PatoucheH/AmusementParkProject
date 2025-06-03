using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    public class HauntedHouse : Attractions
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 30_000;
        public override string? Emoji { get; init; } = ":ghost:";
        public override string? Type { get; init; } = "Thrill";
        public Position? Ordinal { get; set; }

        public HauntedHouse(string name)
        {
            Name = name;
        }
    }
}
