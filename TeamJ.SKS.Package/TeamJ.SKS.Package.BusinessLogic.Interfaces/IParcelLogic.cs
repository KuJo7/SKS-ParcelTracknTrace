using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.BusinessLogic.DTOs;

namespace TeamJ.SKS.Package.BusinessLogic.Interfaces
{
    public interface IParcelLogic
    {
        BLParcel TrackParcel(string trackingID);
        bool TransitionParcel(BLParcel blParcel);
        bool SubmitParcel(BLParcel blParcel);
        bool ReportParcelDelivery(string trackingID);
        bool ReportParcelHop(string trackingID, string code);
    }
}
