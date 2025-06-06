using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    /// <summary>
    /// This is the base interface from which all buildings, such as attractions and shops, inherit.
    /// </summary>
    public interface IBuilding
    {
        public string Name { get; set; }
        public double Price {get; init; }
        public string? Emoji { get; init; }
        public Position Ordinal { get; set; }
        public double MaintenancePrice { get; init; }
        public string? Description { get; init; }
        public int VisitorInAttraction { get; set; }
    }
}
