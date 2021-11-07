using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALHop
    {
        [Key]
        public string HopCode { get; set; }

        public string HopType { get; set; }

        public int ProcessingDelayMins { get; set; }

        public string LocationName { get; set; }

        public string Description { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public int Level { get; set; }

        public List<DALWarehouseNextHops> NextHops { get; set; }

        public string RegionGeoJson { get; set; }

        public string NumberPlate { get; set; }

        public string LogisticsPartner { get; set; }

        public string LogisticsPartnerUrl { get; set; }
    }
}
