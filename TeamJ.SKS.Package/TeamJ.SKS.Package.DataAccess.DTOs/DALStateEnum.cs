using System.Runtime.Serialization;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    public enum DALStateEnum
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
}