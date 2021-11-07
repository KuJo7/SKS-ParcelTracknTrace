using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALHopArrival
    {
        [ForeignKey(nameof(DALParcel))]
        public string ParcelId { get; set; }

        public DALParcel Parcel { get; set; }

        [ForeignKey(nameof(DALHop))]
        public string HopCode { get; set; }

        public DALHop Hop { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}
