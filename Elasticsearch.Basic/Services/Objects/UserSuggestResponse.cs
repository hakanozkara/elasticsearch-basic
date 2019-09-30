using System.Collections.Generic;

namespace Elasticsearch.Basic.Services.Objects
{
    public class UserSuggestResponse
    {
        public IEnumerable<UserSuggest> Suggests { get; set; }
    }
}
