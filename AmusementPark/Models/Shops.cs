using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// Represents a base class for shops within a building or facility.
    /// </summary>
    /// <remarks>This abstract class provides common properties for shops, such as name, price
    /// and description. Derived classes can extend or override these properties to define specific types of
    /// attractions.</remarks>
    public class Shops : IBuilding
    {
        public virtual string Name { get; set; }
        public virtual double Price { get; init; }
        public virtual string? Emoji { get; init; }
        public virtual Position Ordinal { get; set; }
        public virtual double MaintenancePrice { get; init; }
        public virtual string? Description { get; init; }
    }
}
