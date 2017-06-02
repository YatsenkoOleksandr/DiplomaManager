using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Exceptions
{
    public class IncorrectParameterException: Exception
    {
        public IncorrectParameterException():base()
        { }

        public IncorrectParameterException(string message):base(message)
        { }
    }
}
