namespace Elasticsearch.Basic.Services.Objects
{
    public class UserSuggest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public double Score { get; set; }
    }
}