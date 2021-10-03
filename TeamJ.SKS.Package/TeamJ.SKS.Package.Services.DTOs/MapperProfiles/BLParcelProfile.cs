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
    public class BLParcelProfile : Profile
    {
        public BLParcelProfile()
        {
            CreateMap<Parcel, BLParcel>();
            CreateMap<TrackingInformation, BLParcel>();
            CreateMap<NewParcelInfo, BLParcel>();
            CreateMap<Recipient, BLParcel>();
        }
    }
}
