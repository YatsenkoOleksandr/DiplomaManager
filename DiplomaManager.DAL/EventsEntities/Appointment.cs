using DiplomaManager.DAL.TeacherEntities;

namespace DiplomaManager.DAL.EventsEntities
{
    public class Appointment
    {
        public int Id
        { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher
        { get; set; }

        public int CommitteeId { get; set; }
        public Committee Committee
        { get; set; }
    }
}
