namespace DiplomaManager.DAL.TeacherEntities
{
    public class PositionName
    {
        public int ID
        { get; set; }

        public int LocaleID
        { get; set; }

        public string Name
        { get; set; }

        public Position Position
        { get; set; }
    }
}
