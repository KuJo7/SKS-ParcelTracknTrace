using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALWarehouseNextHops
    {
        [Key]
        public string Id { get; set; }

        public int TraveltimeMins { get; set; }

        public DALHop Hop { get; set; }
    }
}
