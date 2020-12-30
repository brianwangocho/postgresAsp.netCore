using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class MemoDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public string ContentType { get; set; }

        public string Attachment { get; set; }
    }
}
