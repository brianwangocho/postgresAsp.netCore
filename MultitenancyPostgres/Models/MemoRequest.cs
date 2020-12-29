using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Models
{
    public class MemoRequest
    {


        public string Title { get; set; }

        public string Content { get; set; }

        public IFormFile Attachment { get; set; }


    }
}
