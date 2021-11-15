using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.Services.Interfaces
{
    public class ServiceException : ApplicationException
    {
        public ServiceException(string serviceController, string message, Exception innerException) : base(message, innerException)
        {
            this.ServiceController = serviceController;
        }

        public string ServiceController { get; }
    }
}
