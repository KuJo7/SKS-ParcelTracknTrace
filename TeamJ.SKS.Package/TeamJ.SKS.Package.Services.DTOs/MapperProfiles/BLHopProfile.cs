using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.DTOs.MapperProfiles
{
    public class BLHopProfile : Profile
    {
        public BLHopProfile()
        {
            CreateMap<Hop, BLHop>();
            CreateMap<HopArrival, BLHop>();
            CreateMap<Truck, BLHop>();
            CreateMap<Warehouse, BLHop>();
            CreateMap<WarehouseNextHops, BLHop>();
            CreateMap<Transferwarehouse, BLHop>();
            CreateMap<GeoCoordinate, BLHop>();
        }
    }
}
