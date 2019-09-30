using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Basic.Models.Response;
using Elasticsearch.Basic.Services;
using Elasticsearch.Basic.Services.Implementations;
using Elasticsearch.Basic.Services.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly IElasticsearchService _elasticsearchService;

        public ElasticController(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseModel>> IndexDocument(User user)
        {
            //var response = _elasticsearchService.IndexDocument(user);
            var response = await _elasticsearchService.CreateIndexAsync();

            return new BaseResponseModel() { Success = response };
        }

        //[HttpPost]
        //public async Task<ActionResult<BaseResponseModel>> IndexDocumentAsync(User user)
        //{
        //    var response = await _elasticsearchService.IndexDocumentAsync(user);

        //    return new BaseResponseModel() { Success = response };
        //}

        [HttpGet]
        public async Task<UserSuggestResponse> Search(string query)
        {
            var response = await _elasticsearchService.SearchAsync(query);

            return response;
        }


    }
}