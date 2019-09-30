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
        Task<bool> IndexDocumentBulkAsync(List<User> users);
        IReadOnlyCollection<User> Search(string query);
        Task<UserSuggestResponse> SearchAsync(string keyword);
        bool CreateIndex();
        Task<bool> CreateIndexAsync();


    }
}
