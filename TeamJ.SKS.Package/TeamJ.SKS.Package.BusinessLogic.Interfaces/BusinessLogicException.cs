using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.Interfaces
{
    public class BusinessLogicException : ApplicationException
    {
        public BusinessLogicException(string logicModule, string message, Exception innerException) : base(message, innerException)
        {
            this.LogicModule = logicModule;
        }

        public string LogicModule { get; }

    }
}
