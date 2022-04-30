using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class GroupsInfo
    {
        public GroupsInfo()
        {
            Objects = new HashSet<ObjectsInfo>();
            Users = new HashSet<UsersInfo>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public string? Comments { get; set; }

        public virtual ICollection<ObjectsInfo> Objects { get; set; }
        public virtual ICollection<UsersInfo> Users { get; set; }
    }
}
