using Elasticsearch.Basic.Services.Objects;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Basic.Services
{
    public interface IElasticsearchService
    {
        bool IndexDocument(User user);
        Task<bool> IndexDocumentAsync(User user);
        IReadOnlyCollection<User> Search(string query);
        bool CreateIndex();


    }
}
