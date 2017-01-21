using System.Collections.Generic;

namespace DiplomaManager.DAL.RequestEntities
{
    public class DevelopmentArea
    {
        public int ID
        { get; set; }

        public string Name
        { get; set; }

        public List<Interest> Interests
        { get; set; }
    }
}
