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
        BLParcel trackPackage(int trackingID);
        void TransferPackage(int trackingID);
        void SubmitParcel(int trackingID);
        void ParcelDelivered(int trackingID);
        void ParcelHopArrival(int trackingID, string code);
    }
}
