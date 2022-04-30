using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class ObjectsReference
    {
        public int ObjectId { get; set; }
        public int ReferencedObjectId { get; set; }
        public DateTime Date { get; set; }

        public virtual ObjectsInfo Object { get; set; } = null!;
        public virtual ObjectsInfo ReferencedObject { get; set; } = null!;
    }
}
