using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALWarehouse : DALHop
    {
        public int Level { get; set; }
        public List<DALWarehouseNextHops> NextHops { get; set; }
    }
}
