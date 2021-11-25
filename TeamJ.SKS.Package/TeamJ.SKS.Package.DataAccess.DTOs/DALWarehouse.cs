using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DALWarehouse : DALHop
    {
        public int Level { get; set; }
        [ForeignKey("DALWarehouseNextHops")]
        public List<DALWarehouseNextHops> NextHops { get; set; }
    }
}
