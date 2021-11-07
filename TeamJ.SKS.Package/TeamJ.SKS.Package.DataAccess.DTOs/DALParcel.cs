using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DALParcel
    {
        [Key]
        public string TrackingId { get; set; }

        public float Weight { get; set; }

        public DALRecipient Recipient { get; set; }

        public DALRecipient Sender { get; set; }

        public enum StateEnum
        {
            [EnumMember(Value = "Pickup")]
            PickupEnum = 0,
            [EnumMember(Value = "InTransport")]
            InTransportEnum = 1,
            [EnumMember(Value = "InTruckDelivery")]
            InTruckDeliveryEnum = 2,
            [EnumMember(Value = "Transferred")]
            TransferredEnum = 3,
            [EnumMember(Value = "Delivered")]
            DeliveredEnum = 4
        }

        public StateEnum State { get; set; }

        public List<DALHopArrival> VisitedHops { get; set; } = new();

        public List<DALHopArrival> FutureHops { get; set; } = new(); 

    }
}
