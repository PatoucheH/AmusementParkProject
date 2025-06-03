using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Models
{
    public abstract class Attractions :IBuilding
    {
        public virtual string Name { get; set; }
        public virtual double Price { get; init; }
        public virtual string? Emoji { get; init; }
        public virtual string? Type { get; init; }
        public virtual Position Ordinal { get; set; }
        
    }
}
