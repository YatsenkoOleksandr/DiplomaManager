namespace DiplomaManager.Requests
{
    public class RespondProjectRequest : IRequest
    {
        public int? ProjectId { get; set; }
        public bool? Accepted { get; set; }
        public string Comment { get; set; }

        public string GetInformation()
        {
            return $"{GetType().Name}(ProjectId: {ProjectId}, Accepted: {Accepted}, Comment: {Comment})";
        }
    }
}