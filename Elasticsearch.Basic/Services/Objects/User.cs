using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Basic.Services.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public CompletionField Suggest { get; set; }
    }
}
