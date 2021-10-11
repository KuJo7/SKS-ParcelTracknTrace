using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class BLParcel
    {

        public float Weight { get; set; }

        public BLRecipient Recipient { get; set; }

        public BLRecipient Sender { get; set; }

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

        public List<BLHopArrival> VisitedHops { get; set; }

        public List<BLHopArrival> FutureHops { get; set; }

        public string TrackingId { get; set; }

    }
}
