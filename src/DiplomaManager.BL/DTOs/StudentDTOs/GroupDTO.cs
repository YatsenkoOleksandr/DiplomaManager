namespace DiplomaManager.BLL.DTOs.StudentDTOs
{
    public class GroupDTO
    {
        public int Id
        { get; set; }

        public string Name
        { get; set; }

        public int DegreeId { get; set; }

        public int GraduationYear { get; set; }

        public DegreeDTO Degree { get; set; }
    }
}
