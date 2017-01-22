﻿using System.Collections.Generic;
using DiplomaManager.DAL.RequestEntities;

namespace DiplomaManager.DAL.StudentEntities
{
    public class Degree
    {
        public int Id
        { get; set; }

        public List<DegreeName> DegreeNames
        { get; set; }

        public List<Group> Groups
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }
    }
}
