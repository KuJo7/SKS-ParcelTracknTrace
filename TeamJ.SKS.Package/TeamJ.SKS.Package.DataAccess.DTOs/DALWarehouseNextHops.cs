using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALWarehouseNextHops
    {
        public int TraveltimeMins { get; set; }

        public DALHop Hop { get; set; }
    }
}
