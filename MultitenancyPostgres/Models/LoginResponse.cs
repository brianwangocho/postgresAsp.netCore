using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public string  Message { get; set; }

        public string Status { get; set; }

        public DateTime ? ExpiresOn { get; set; }
    }
}
