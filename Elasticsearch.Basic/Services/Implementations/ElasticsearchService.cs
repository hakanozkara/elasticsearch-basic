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

        public async Task<bool> IndexDocumentBulkAsync(List<User> users)
        {
            var indexResponse = await _elasticClient.IndexManyAsync(users);

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

        public async Task<UserSuggestResponse> SearchAsync(string keyword)
        {
            ISearchResponse<User> searchResponse = await _elasticClient.SearchAsync<User>(s => s
                                        .Suggest(su => su
                                             .Completion("username", c => c
                                                  .Field(f => f.Suggest)
                                                  .Prefix(keyword)
                                                  .Fuzzy(f => f
                                                      .Fuzziness(Fuzziness.Auto)
                                                  )
                                                  .Size(10))
                                                ));

            var suggests = from suggest in searchResponse.Suggest["username"]
                           from option in suggest.Options
                           select new UserSuggest
                           {
                               Id = option.Source.Id,
                               Username = option.Source.Username,
                               Score = option.Score
                           };

            return new UserSuggestResponse
            {
                Suggests = suggests
            };
        }

        public bool CreateIndex()
        {
            var response = _elasticClient.Indices.Create("users", u => u
                .Map<User>(m => m.AutoMap())
            );

            return response.ShardsAcknowledged;
        }

        public async Task<bool> CreateIndexAsync()
        {
            var createIndexDescriptor = new CreateIndexDescriptor("users")
                                                    .Map<User>(m => m
                                                        .AutoMap()
                                                        .Properties(ps => ps
                                                            .Completion(c => c
                                                                .Name(p => p.Suggest))));

            var request = new IndexExistsRequest("users");
            var indexExists = _elasticClient.Indices.Exists(request);
            if (indexExists.IsValid)
            {
                _elasticClient.Indices.Delete("users");
            }

            var response = await _elasticClient.Indices.CreateAsync("users");

            return response.IsValid;
        }
    }
}
