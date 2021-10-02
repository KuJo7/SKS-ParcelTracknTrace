using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class ParcelLogic : IParcelLogic
    {
        public BLParcel trackPackage(int trackingID)
        {
            throw new NotImplementedException();
        }

        public void TransferPackage(int trackingID)
        {
            throw new NotImplementedException();
        }

        public void SubmitParcel(int trackingID)
        {
            throw new NotImplementedException();
        }

        public void ParcelDelivered(int trackingID)
        {
            throw new NotImplementedException();
        }

        public void ParcelHopArrival(int trackingID, string code)
        {
            throw new NotImplementedException();
        }
    }
}
