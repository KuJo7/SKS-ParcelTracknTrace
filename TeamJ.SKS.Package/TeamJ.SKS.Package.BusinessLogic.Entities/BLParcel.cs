using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    public class BLParcel
    {

        [Required]

        [DataMember(Name = "weight")]
        public float? Weight { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
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

        [Required]

        [DataMember(Name = "state")]
        public StateEnum? State { get; set; }

        [Required]

        [DataMember(Name = "visitedHops")]
        public List<BLHop> VisitedHops { get; set; }

        [Required]

        [DataMember(Name = "futureHops")]
        public List<BLHop> FutureHops { get; set; }

        [RegularExpression("/^[A-Z0-9]{9}$/")]
        [DataMember(Name = "trackingId")]
        public string TrackingId { get; set; }

        [Required]

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [Required]

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [Required]

        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        [Required]

        [DataMember(Name = "city")]
        public string City { get; set; }

        [Required]

        [DataMember(Name = "country")]
        public string Country { get; set; }
    }
}
