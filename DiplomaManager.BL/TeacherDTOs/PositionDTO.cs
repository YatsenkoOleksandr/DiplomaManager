using System.Collections.Generic;

namespace DiplomaManager.BL.TeacherDTOs
{
    public class PositionDTO
    {
        public int Id
        { get; set; }

        public List<PositionNameDTO> PositionNames
        { get; set; }
    }
}
