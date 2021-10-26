using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALTruck : DALHop
    {
        public string RegionGeoJson { get; set; }

        public string NumberPlate { get; set; }
    }
}
