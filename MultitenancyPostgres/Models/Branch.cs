using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class Branch
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Department> Departments { get; set; }



    }

    ////https://www.learndapper.com/relationships///
    ///tutorials on mappings
}
