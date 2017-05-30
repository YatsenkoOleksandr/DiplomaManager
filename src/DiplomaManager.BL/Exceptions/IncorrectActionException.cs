using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Exceptions
{
    /// <summary>
    /// Class for describing situations, when you can't do action with some entity
    /// </summary>
    public class IncorrectActionException: Exception
    {
        public IncorrectActionException(): base()
        { }

        public IncorrectActionException(string message): base(message)
        { }
    }
}
