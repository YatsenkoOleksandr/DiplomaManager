using DiplomaManager.DAL.Entities.TeacherEntities;

namespace DiplomaManager.DAL.Entities.PredefenseEntities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int PredefenseDateId { get; set; }
        public PredefenseDate PredefenseDate { get; set; }
    }
}
