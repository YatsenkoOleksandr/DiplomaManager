using System.Collections.Generic;

namespace DiplomaManager.DAL.TeacherEntities
{
    public class Position
    {
        public int Id
        { get; set; }

        public List<PositionName> PositionNames
        { get; set; }
    }
}
