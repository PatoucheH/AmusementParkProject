using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    public interface IBuilding
    {
        public string Name { get; set; }
        public double Price {get; init; }
        public string? Emoji { get; init; }
        public Position Ordinal { get; set; }
        public double MaintenancePrice { get; init; }

        public string? Description { get; init; }
    }
}
