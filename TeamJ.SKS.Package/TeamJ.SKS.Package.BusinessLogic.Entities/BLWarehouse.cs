using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class BLWarehouse : BLHop
    {
        public int Level { get; set; }
        public List<BLWarehouseNextHops> NextHops { get; set; }
    }
}
