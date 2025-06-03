using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
        internal class DuckFishing :Attractions
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 3_000;
        public override string? Emoji { get; init; } = ":duck:";
        public override string? Type { get; init; } = "Child";
        public Position? Ordinal { get; set; }

        public DuckFishing(string name)
        {
            Name = name;
        }
    }
}
