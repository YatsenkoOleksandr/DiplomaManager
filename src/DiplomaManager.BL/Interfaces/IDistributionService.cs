using DiplomaManager.BLL.Extensions.DistributionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IDistributionService
    {
        FormedProjects Distribute(int degreeId, int graduationYear);

        void Save(FormedProjects formedProjects);
    }
}
