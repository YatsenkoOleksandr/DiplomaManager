namespace DiplomaManager.ViewModels
{
    public class RequestViewModel
    {
        public int TeacherId { get; set; }

        public int DaId { get; set; }

        public string Title { get; set; }

        public StudentViewModel Student { get; set; }
    }
}
