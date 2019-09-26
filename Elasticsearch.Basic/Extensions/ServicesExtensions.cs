using Elasticsearch.Basic.Services.Objects;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Nest.JsonNetSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Basic.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var urisFromConfiguration = configuration.GetSection("Elasticsearch").GetValue<string>("Urls");

            //var uris = urisFromConfiguration.Select(p => new Uri(p));
            List<Uri> uris = new List<Uri>()
            {
                new Uri(urisFromConfiguration)
            };
            //var credentials = new BasicAuthenticationCredentials("username", "password");
            //var pool = new CloudConnectionPool(cloudId, credentials);
            var connectionPool = new SingleNodeConnectionPool(new Uri(urisFromConfiguration));
            var settings = new ConnectionSettings(connectionPool,sourceSerializer: JsonNetSerializer.Default)
                                .DefaultMappingFor<User>(i => i
                                    .IndexName("users")
                                    .IdProperty(p => p.Username)
                                )
                                .EnableDebugMode()
                                .PrettyJson()
                                .RequestTimeout(TimeSpan.FromMinutes(2));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
