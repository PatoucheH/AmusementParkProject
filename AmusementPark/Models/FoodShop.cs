using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    public class FoodShop :Shops
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 10_000;
        public override string? Emoji { get; init; } = ":hot_dog:";
        public Position? Ordinal { get; set; }

        public FoodShop(string name)
        {
            Name = name;
        }
    }
}
