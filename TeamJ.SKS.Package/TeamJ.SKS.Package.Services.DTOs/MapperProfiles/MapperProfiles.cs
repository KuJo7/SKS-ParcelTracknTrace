using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.DTOs.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Hop, BLHop>();
            CreateMap<HopArrival, BLHopArrival>();
            CreateMap<Truck, BLTruck>();
            CreateMap<Warehouse, BLWarehouse>();
            CreateMap<WarehouseNextHops, BLWarehouseNextHops>();
            CreateMap<Transferwarehouse, BLTransferwarehouse>();
            CreateMap<GeoCoordinate, BLHop>();
            CreateMap<Parcel, BLParcel>();
            CreateMap<TrackingInformation, BLParcel>();
            CreateMap<NewParcelInfo, BLParcel>();
            CreateMap<Recipient, BLRecipient>();
        }
    }
}
