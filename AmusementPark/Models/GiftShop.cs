using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    public class GiftShop : Shops
    {
        public override string Name { get; set; }
        public override double? Price { get; init; } = 25_000;
        public override string? Emoji { get; init; } = ":wrapped_gift:";
        public Position? Ordinal { get; set; }

        public GiftShop(string name)
        {
            Name = name;
        }
    }
}
