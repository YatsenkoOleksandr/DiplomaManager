using System.Collections.Generic;

namespace DiplomaManager.DAL.TeacherEntities
{
    public class Position
    {
        public int ID
        { get; set; }

        public List<PositionName> PositionNames
        { get; set; }
    }
}
