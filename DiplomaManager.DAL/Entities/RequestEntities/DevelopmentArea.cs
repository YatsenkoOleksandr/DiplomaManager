using System.Collections.Generic;

namespace DiplomaManager.DAL.Entities.RequestEntities
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
