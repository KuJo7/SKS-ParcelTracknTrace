using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    [ExcludeFromCodeCoverage]
    public class DataAccessException : ApplicationException
    {
        public DataAccessException(string repository, string operation, string message, Exception innerException) : base(message, innerException)
        {
            this.Repository = repository;
            this.Operation = operation;
        }

        public string Repository { get; }
        public string Operation { get; }
    }
}
