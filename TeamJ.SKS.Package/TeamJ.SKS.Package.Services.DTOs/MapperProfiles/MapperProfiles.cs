using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.Services.DTOs.Models;
using TeamJ.SKS.Package.Services.DTOs.Converter;
using NetTopologySuite.Geometries;

namespace TeamJ.SKS.Package.Services.DTOs.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<GeoCoordinate, Point>().ConvertUsing(g => new Point(g.Lon, g.Lat) { SRID = 4326 });
            CreateMap<Point, GeoCoordinate>().ConvertUsing(p => new GeoCoordinate { Lon = p.X, Lat = p.Y });

            CreateMap<string, Geometry>().ConvertUsing(new GeometrySVCBL());
            CreateMap<Geometry, string>().ConvertUsing(new GeometryBLSVC());

            CreateMap<GeoCoordinate, BLHop>(MemberList.Source).ReverseMap();

            CreateMap<Hop, BLHop>()
                .IncludeMembers(s => s.LocationCoordinates)
                .Include<Warehouse, BLWarehouse>()
                .Include<Truck, BLTruck>()
                .Include<Transferwarehouse, BLTransferwarehouse>();
            CreateMap<Transferwarehouse, BLTransferwarehouse>().ReverseMap();
            CreateMap<Truck, BLTruck>().ReverseMap();
            CreateMap<Warehouse, BLWarehouse>().ReverseMap();


            CreateMap<BLHop, Hop>(MemberList.Source)
                .ForPath(d => d.LocationCoordinates, opt => opt.MapFrom(s => s.LocationCoordinates))
                .Include<BLWarehouse, Warehouse>()
                .Include<BLTruck, Truck>()
                .Include<BLTransferwarehouse, Transferwarehouse>();

            CreateMap<HopArrival, BLHopArrival>().ReverseMap();
            CreateMap<WarehouseNextHops, BLWarehouseNextHops>().ReverseMap();

            CreateMap<Parcel, BLParcel>(MemberList.Source).ReverseMap();
            CreateMap<TrackingInformation, BLParcel>(MemberList.Source).ReverseMap();
            CreateMap<NewParcelInfo, BLParcel>(MemberList.Source).ReverseMap();
            CreateMap<Recipient, BLRecipient>().ReverseMap();

            CreateMap<BLHop, DALHop>(MemberList.Source)
                .Include<BLWarehouse, DALWarehouse>()
                .Include<BLTruck, DALTruck>()
                .Include<BLTransferwarehouse, DALTransferwarehouse>();
            CreateMap<DALHop, BLHop>(MemberList.Source)
                .Include<DALWarehouse, BLWarehouse>()
                .Include<DALTruck, BLTruck>()
                .Include<DALTransferwarehouse, BLTransferwarehouse>();
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
