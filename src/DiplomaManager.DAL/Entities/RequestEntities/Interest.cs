using DiplomaManager.DAL.Entities.TeacherEntities;

namespace DiplomaManager.DAL.Entities.RequestEntities
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
