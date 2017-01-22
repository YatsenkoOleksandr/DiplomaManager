using System.Collections.Generic;

namespace DiplomaManager.DAL.RequestEntities
{
    public class DevelopmentArea
    {
        public int Id
        { get; set; }

        public string Name
        { get; set; }

        public List<Interest> Interests
        { get; set; }
    }
}
