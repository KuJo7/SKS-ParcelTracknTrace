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
    public class DALWarehouseNextHops
    {
        [Key]
        public string Id { get; set; }

        public int TraveltimeMins { get; set; }
        [ForeignKey("Hops")]

        public DALHop Hop { get; set; }
    }
}
