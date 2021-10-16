using System;
using System.Diagnostics.CodeAnalysis;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class BLHopArrival
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}
