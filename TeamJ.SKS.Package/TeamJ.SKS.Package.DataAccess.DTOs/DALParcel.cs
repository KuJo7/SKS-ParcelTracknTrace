using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public class DALParcel
    {
        [Key]
        public string TrackingId { get; set; }

        public float Weight { get; set; }

        [ForeignKey(nameof(Recipient))]
        public Guid RecipientId { get; set; }

        public DALRecipient Recipient { get; set; }

        [ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }

        public DALRecipient Sender { get; set; }

        public DALStateEnum State { get; set; }

        public List<DALHopArrival> Hops { get; set; }

        //public List<DALHopArrival> VisitedHops { get; set; }

        //public List<DALHopArrival> FutureHops { get; set; }

    }
}
