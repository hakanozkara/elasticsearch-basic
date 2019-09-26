using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Basic.Services.Objects;
using Nest;

namespace Elasticsearch.Basic.Services.Implementations
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public bool IndexDocument(User user)
        {
            var indexResponse = _elasticClient.IndexDocument(user);

            return indexResponse.IsValid;
        }

        public async Task<bool> IndexDocumentAsync(User user)
        {
            var indexResponse = await _elasticClient.IndexDocumentAsync(user);

            return indexResponse.IsValid;
        }

        public IReadOnlyCollection<User> Search(string query)
        {
            var searchResponse = _elasticClient.Search<User>(s => s
                .AllIndices()
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Username)
                        .Query(query))));

            return searchResponse.Documents;
        }

        public bool CreateIndex()
        {
            var response = _elasticClient.Indices.Create("myuserindex", u => u
                .Map<User>(m=> m.AutoMap())
            );

            return response.ShardsAcknowledged;
        }
    }
}
