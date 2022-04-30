using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemicalPropertiesApp.Models
{
    public partial class UsersInfo
    {
        public UsersInfo()
        {
            Groups = new HashSet<GroupsInfo>();
        }

        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string LastName { get; set; } = null!;
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? IndexCode { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public int Category { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime? DateRecord { get; set; }

        public virtual ICollection<GroupsInfo> Groups { get; set; }
    }
}
