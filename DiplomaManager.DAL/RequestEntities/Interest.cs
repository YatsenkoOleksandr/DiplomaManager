using DiplomaManager.DAL.TeacherEntities;

namespace DiplomaManager.DAL.RequestEntities
{
    public class Interest
    {
        public int TeacherId { get; set; }
        public Teacher Teacher
        { get; set; }

        public int DevelopmentAreaId { get; set; }
        public DevelopmentArea DevelopmentArea
        { get; set; }
    }
}
