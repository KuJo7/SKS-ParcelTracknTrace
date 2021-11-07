using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALTruck : DALHop
    {
        [Key]
        public string Id { get; set; }

        public string RegionGeoJson { get; set; }
        public string NumberPlate { get; set; }
    }
}
