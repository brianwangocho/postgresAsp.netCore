using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class UserGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
