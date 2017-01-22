namespace DiplomaManager.DAL.TeacherEntities
{
    public class PositionName
    {
        public int Id
        { get; set; }

        public int LocaleId
        { get; set; }

        public string Name
        { get; set; }

        public int PositionId { get; set; }
        public Position Position
        { get; set; }
    }
}
