using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.MapperProfiles
{
    public class BLToDALMapperProfile : Profile
    {
        public BLToDALMapperProfile()
        {
            CreateMap<BLHop, DALHop>();
            CreateMap<BLHopArrival, DALHopArrival>();
            CreateMap<BLTruck, DALTruck>();
            CreateMap<BLWarehouse, DALWarehouse>();
            CreateMap<BLWarehouseNextHops, DALWarehouseNextHops>();
            CreateMap<BLTransferwarehouse, DALTransferwarehouse>();
            CreateMap<BLParcel, DALParcel>();
            CreateMap<BLRecipient, DALRecipient>();
        }
    }
}
