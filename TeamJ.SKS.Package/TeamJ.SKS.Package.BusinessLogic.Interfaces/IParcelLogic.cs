using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TeamJ.SKS.Package.BusinessLogic.DTOs;

namespace TeamJ.SKS.Package.BusinessLogic.Interfaces
{
    public interface IParcelLogic
    {
        BLParcel TrackParcel(string trackingID);
        public bool TransitionParcel(BLParcel blParcel, string trackingId, bool isTransfer);
        public bool SubmitParcel(BLParcel blParcel, out string trackingId);
        public bool ReportParcelDelivery(string trackingID);
        public bool ReportParcelHop(string trackingID, string code);
    }
}
