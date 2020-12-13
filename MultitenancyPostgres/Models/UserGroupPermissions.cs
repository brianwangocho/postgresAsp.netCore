using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class UserGroupPermissions
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public int PermissionId { get; set; }


        public IList<Permissions> permissions { get; set; } = new List<Permissions>();
    }
}
