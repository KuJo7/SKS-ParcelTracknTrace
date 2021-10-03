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
        public BLParcel TrackParcel(string trackingID)
        {
            if (trackingID == "123456789")
            {
                return new BLParcel();
            }
            else
            {
                return null;
            }
        }

        public bool TransitionParcel(BLParcel blParcel)
        {
            //Check blParcel not null eigentlich jetzt nur zum Testen von Mapping
            if (String.IsNullOrWhiteSpace(blParcel.Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SubmitParcel(BLParcel blParcel)
        {
            if (blParcel != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReportParcelDelivery(string trackingID)
        {
            if (trackingID == "123456789")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReportParcelHop(string trackingID, string code)
        {
            if (trackingID == "123456789" && code == "test")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
