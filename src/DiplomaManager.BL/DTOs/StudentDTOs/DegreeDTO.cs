using System.Collections.Generic;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using System.Linq;

namespace DiplomaManager.BLL.DTOs.StudentDTOs
{
    public class DegreeDTO
    {
        public int Id
        { get; set; }

        public List<DegreeName> DegreeNames
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }


        public string GetName(int localeId = 193)
        {
            DegreeName name = DegreeNames.Where(dn => dn.LocaleId == localeId).FirstOrDefault();
            if (name != null && !string.IsNullOrEmpty(name.Name))
            {
                return name.Name;
            }
            return "-";
        }
    }
}
