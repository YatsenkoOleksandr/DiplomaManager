using DiplomaManager.Requests;

namespace DiplomaManager.Areas.Teacher.Requests
{
    public class GetProjectsRequest : SearchRequest
    {
        public int? TeacherId { get; set; }
    }
}