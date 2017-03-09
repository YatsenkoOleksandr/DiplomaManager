using System.Collections.Generic;

namespace DiplomaManager.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, long total)
        {
            Data = data;
            Total = total;
        }

        public long Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
