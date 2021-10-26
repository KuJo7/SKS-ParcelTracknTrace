using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALHop
    {
        public string HopType { get; set; }

        public int ProcessingDelayMins { get; set; }

        public string LocationName { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }
    }
}
