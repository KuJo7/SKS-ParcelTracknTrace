using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    public class BLWarehouse : BLHop
    {
        public int Level { get; set; }
        public List<BLWarehouseNextHops> NextHops { get; set; }
    }
}
