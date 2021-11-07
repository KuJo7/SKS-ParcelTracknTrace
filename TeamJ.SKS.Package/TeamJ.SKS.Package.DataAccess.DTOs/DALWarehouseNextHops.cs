using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALWarehouseNextHops
    {
        public int TraveltimeMins { get; set; }

        [ForeignKey(nameof(FromHop))]
        public string FromHopCode { get; set; }

        public DALHop FromHop { get; set; }

        [ForeignKey(nameof(ToHop))]
        public string ToHopCode { get; set; }

        public DALHop ToHop { get; set; }
    }
}
