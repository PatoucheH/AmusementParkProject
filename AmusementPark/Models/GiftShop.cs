using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// This class inherits from shops to create a GiftShop class with same properties
    /// </summary>
    public class GiftShop : Shops
    {
        public override string Name { get; set; }
        public override double Price { get; init; } = 25_000;
        public override string? Emoji { get; init; } = ":wrapped_gift:";
        public Position? Ordinal { get; set; }
        public override double MaintenancePrice { get; init; } = 200;
        public override string? Description { get; init; } = "Come buy some gift for your friends !";
        public GiftShop(string name)
        {
            Name = name;
        }
    }
}
