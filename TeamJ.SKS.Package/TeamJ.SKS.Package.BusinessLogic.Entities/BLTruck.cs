using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class BLTruck : BLHop
    {
        public string RegionGeoJson { get; set; }

        public string NumberPlate { get; set; }
    }
}
