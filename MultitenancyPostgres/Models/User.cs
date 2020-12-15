using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Status{ get; set; }


        public string RoleId { get; set; }

        public Roles Roles { get; set; }


        public ICollection<UserGroup> userGroups { get; set; } = new HashSet<UserGroup>();

    }
}
