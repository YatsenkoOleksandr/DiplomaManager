using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Exceptions
{
    /// <summary>
    /// Exception for situation, when there are not exist entity with required Id
    /// </summary>
    public class NoEntityInDatabaseException: Exception
    {
        public NoEntityInDatabaseException(): base()
        { }

        public NoEntityInDatabaseException(string message): base(message)
        { }
    }
}
