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
        public bool TransitionParcel(BLParcel blParcel);
        public bool SubmitParcel(BLParcel blParcel);
        public bool ReportParcelDelivery(string trackingID);
        public bool ReportParcelHop(string trackingID, string code);
    }
}
