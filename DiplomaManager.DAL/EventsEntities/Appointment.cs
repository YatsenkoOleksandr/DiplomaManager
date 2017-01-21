namespace DiplomaManager.DAL.EventsEntities
{
    public class Appointment
    {
        public int ID
        { get; set; }

        public TeacherEntities.Teacher Teacher
        { get; set; }

        public Committee Committee
        { get; set; }
    }
}
