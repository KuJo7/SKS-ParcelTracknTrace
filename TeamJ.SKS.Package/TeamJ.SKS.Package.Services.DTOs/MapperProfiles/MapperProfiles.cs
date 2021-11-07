using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.DTOs.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Hop, BLHop>().ReverseMap();
            CreateMap<HopArrival, BLHopArrival>().ReverseMap();
            CreateMap<Truck, BLTruck>().ReverseMap();
            CreateMap<Warehouse, BLWarehouse>().ReverseMap();
            CreateMap<WarehouseNextHops, BLWarehouseNextHops>().ReverseMap();
            CreateMap<Transferwarehouse, BLTransferwarehouse>().ReverseMap();
            CreateMap<GeoCoordinate, BLHop>().ReverseMap();
            CreateMap<Parcel, BLParcel>().ReverseMap();
            CreateMap<TrackingInformation, BLParcel>().ReverseMap();
            CreateMap<NewParcelInfo, BLParcel>().ReverseMap();
            CreateMap<Recipient, BLRecipient>().ReverseMap();

            CreateMap<BLHop, DALHop>().ReverseMap();
            CreateMap<BLTruck, DALHop>().ReverseMap();
            CreateMap<BLWarehouse, DALHop>().ReverseMap();
            CreateMap<BLTransferwarehouse, DALHop>().ReverseMap();
            CreateMap<BLHopArrival, DALHopArrival>().ReverseMap();
            CreateMap<BLWarehouseNextHops, DALWarehouseNextHops>().ReverseMap();
            CreateMap<DALParcel, BLParcel>().ReverseMap();
            CreateMap<DALRecipient, BLRecipient>().ReverseMap();
        }
    }
}
