using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Basic.Models.Response;
using Elasticsearch.Basic.Services;
using Elasticsearch.Basic.Services.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IElasticsearchService _elasticsearchService;

        public UsersController(IElasticsearchService elasticsearchService)
        {
            this._elasticsearchService = elasticsearchService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseModel>> IndexBulk(List<User> users)
        {
            var response = await _elasticsearchService.IndexDocumentBulkAsync(users);

            return new BaseResponseModel() { Success = response };
        }
    }
}