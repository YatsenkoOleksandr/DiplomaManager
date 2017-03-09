namespace DiplomaManager.Requests
{
    public class SearchRequest : IRequest
    {
        public string Query { get; set; }
        public int Limit { get; set; }
        public int CurrentPage { get; set; }

        public string GetInformation()
        {
            return $"{GetType().Name}(Query: {Query}, CurrentPage: {CurrentPage})";
        }
    }
}