using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class ObjectsInfo
    {
        public ObjectsInfo()
        {
            ObjectsReferenceObjects = new HashSet<ObjectsReference>();
            ObjectsReferenceReferencedObjects = new HashSet<ObjectsReference>();
            Groups = new HashSet<GroupsInfo>();
            Rubrics = new HashSet<RubricsInfo>();
        }

        public int ObjectId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int RubricId { get; set; }
        public bool? IsPublished { get; set; }
        public bool ControlAccess { get; set; }
        public int? SortType { get; set; }
        public string ObjectName { get; set; } = null!;
        public string? FilePath { get; set; }
        public string? ObjectDescription { get; set; }
        public string? ObjectContent { get; set; }

        public virtual ThermodynamicDataActivity Object { get; set; } = null!;
        public virtual ThermodynamicDataHeatCapacity Object1 { get; set; } = null!;
        public virtual PhaseEquilibrium Object2 { get; set; } = null!;
        public virtual LitReference Object3 { get; set; } = null!;
        public virtual SolidPhase Object4 { get; set; } = null!;
        public virtual ThermodynamicDataEnthalpy ObjectNavigation { get; set; } = null!;
        public virtual RubricsInfo Rubric { get; set; } = null!;
        public virtual ICollection<ObjectsReference> ObjectsReferenceObjects { get; set; }
        public virtual ICollection<ObjectsReference> ObjectsReferenceReferencedObjects { get; set; }

        public virtual ICollection<GroupsInfo> Groups { get; set; }
        public virtual ICollection<RubricsInfo> Rubrics { get; set; }
    }
}
