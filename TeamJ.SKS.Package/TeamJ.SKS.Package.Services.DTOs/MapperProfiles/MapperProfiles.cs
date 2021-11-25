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
            CreateMap<GeoCoordinate, BLHop>(MemberList.Source).ReverseMap();
            CreateMap<Hop, BLHop>(MemberList.Source)
                //.ForSourceMember(x => x.LocationCoordinates, opt => opt.DoNotValidate())
                .IncludeMembers(s=>s.LocationCoordinates)
                .IncludeAllDerived();
            CreateMap<HopArrival, BLHopArrival>().ReverseMap();
            CreateMap<Truck, BLTruck>(MemberList.Source).ReverseMap();
            CreateMap<Warehouse, BLWarehouse>(MemberList.Source).ReverseMap();
            CreateMap<WarehouseNextHops, BLWarehouseNextHops>().ReverseMap();
            CreateMap<Transferwarehouse, BLTransferwarehouse>(MemberList.Source).ReverseMap();
            CreateMap<Parcel, BLParcel>(MemberList.Source).ReverseMap();
            CreateMap<TrackingInformation, BLParcel>(MemberList.Source).ReverseMap();
            CreateMap<NewParcelInfo, BLParcel>(MemberList.Source).ReverseMap();
            CreateMap<Recipient, BLRecipient>().ReverseMap();


            CreateMap<BLHop, DALHop>(MemberList.Source).ReverseMap().IncludeAllDerived();
            CreateMap<BLTruck, DALTruck>(MemberList.Source).ReverseMap();
            CreateMap<BLWarehouse, DALWarehouse>(MemberList.Source).ReverseMap();
            CreateMap<BLTransferwarehouse, DALTransferwarehouse>(MemberList.Source).ReverseMap();
            CreateMap<BLHopArrival, DALHopArrival>(MemberList.Source).ReverseMap();
            CreateMap<BLWarehouseNextHops, DALWarehouseNextHops>(MemberList.Source).ReverseMap();
            CreateMap<DALParcel, BLParcel>().ReverseMap();
            CreateMap<DALRecipient, BLRecipient>().ReverseMap();
        }
    }
}
