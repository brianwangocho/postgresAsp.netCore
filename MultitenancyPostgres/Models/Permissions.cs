using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class Permissions
    {
        public int Id { get; set; }

        public string Module { get; set; }

        public string Permission { get; set; }

        public string Status { get; set; }
    }
}
