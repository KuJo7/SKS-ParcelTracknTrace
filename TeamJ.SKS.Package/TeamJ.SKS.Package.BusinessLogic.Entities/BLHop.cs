using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public abstract class BLHop
    {

        public string HopType { get; set; }

        public int ProcessingDelayMins { get; set; }

        public string LocationName { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public double? Lat { get; set; }

        public double? Lon { get; set; }

    }
}
