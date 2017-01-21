namespace DiplomaManager.DAL.StudentEntities
{
    public class DegreeName
    {
        public int ID
        { get; set; }

        public int LocaleID
        { get; set; }

        public string Name
        { get; set; }

        public Degree Degree
        { get; set; }
    }
}
