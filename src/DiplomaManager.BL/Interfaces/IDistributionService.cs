using DiplomaManager.BLL.Extensions.DistributionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Extensions.DistributionService;
using System.Collections.Generic;


namespace DiplomaManager.BLL.Interfaces
{
    public interface IDistributionService
    {
        IEnumerable<ProjectDTO> GetAcceptedProjects(int degreeId, int graduationYear);

        FormedProjects Distribute(int degreeId, int graduationYear);

        void Save(int degreeId, int graduationYear, FormedProjects formedProjects);
    }
}
