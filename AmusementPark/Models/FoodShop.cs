using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// This class inherits from Shops to create a FoodShop class with same properties
    /// </summary>
    public class FoodShop :Shops
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 10_000;
        public override string? Emoji { get; init; } = ":hot_dog:";
        public override Position Ordinal { get; set; }
        public override double MaintenancePrice {get; init;} = 100;
        public override string? Description { get; init; } = "Some food if you're hungry not angry.";
        public override int VisitorInAttraction { get; set; }

        public FoodShop(string name)
        {
            Name = name;
        }
    }
}
