using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DALWarehouse : DALHop
    {
        [Key]
        public string Id { get; set; }

        public int Level { get; set; }
        public List<DALWarehouseNextHops> NextHops { get; set; }
    }
}
