using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// Represents a base class for attractions within a building or facility.
    /// </summary>
    public abstract class Attractions :IBuilding
    {
        public virtual string Name { get; set; }
        public virtual double Price { get; init; }
        public virtual string? Emoji { get; init; }
        public virtual string? Type { get; init; }
        public virtual Position Ordinal { get; set; }
        public virtual double MaintenancePrice { get; init; }
        public virtual string? Description { get; init; }
        public virtual int VisitorInAttraction { get; set; }
    }
}
