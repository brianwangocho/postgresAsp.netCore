using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class Tenant
    {
        public int Id { get; set; }


        public string Status { get; set; }

        public bool DatabaseCreated { get; set; }


        public string Name { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }

    public class TenantRequest
    {


        public string Name { get; set; }
    }




}
