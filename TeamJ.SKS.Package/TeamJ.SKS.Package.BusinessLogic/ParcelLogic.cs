using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.DTOs.Validators;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class ParcelLogic : IParcelLogic
    {
        readonly IValidator<BLParcel> validator = new BLParcelValidator();
        readonly IValidator<string> codeValidator = new BLCodeValidator();

        public BLParcel TrackParcel(string trackingID)
        {
            var parcel = new BLParcel();
            parcel.TrackingId = trackingID;
            parcel.FutureHops = new List<BLHopArrival>();
            parcel.VisitedHops = new List<BLHopArrival>();
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new BLRecipient();
            parcel.Recipient = new BLRecipient();
            var result = validator.Validate(parcel);
            if (result.IsValid)
            {
                return new BLParcel();
            }
            return null;
        }

        public bool TransitionParcel(BLParcel blParcel)
        {
            var result = validator.Validate(blParcel);
            //Check blParcel not null eigentlich jetzt nur zum Testen von Mapping
            //if (String.IsNullOrWhiteSpace(blParcel.Name))
            if(result.IsValid)
            {
                return true;
            }
            return false;
        }

        public bool SubmitParcel(BLParcel blParcel)
        {
            var result = validator.Validate(blParcel);
            if (result.IsValid)
            {
                return true;
            }
            return false;
        }

        public bool ReportParcelDelivery(string trackingID)
        {
            var parcel = new BLParcel();
            parcel.TrackingId = trackingID;
            parcel.FutureHops = new List<BLHopArrival>();
            parcel.VisitedHops = new List<BLHopArrival>();
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new BLRecipient();
            parcel.Recipient = new BLRecipient();
            var result = validator.Validate(parcel);
            if (result.IsValid)
            {
                return true;
            }
            return false;
            
        }

        public bool ReportParcelHop(string trackingID, string code)
        {
            var parcel = new BLParcel();
            parcel.TrackingId = trackingID;
            parcel.FutureHops = new List<BLHopArrival>();
            parcel.VisitedHops = new List<BLHopArrival>();
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new BLRecipient();
            parcel.Recipient = new BLRecipient();
            var resultTrackingID = validator.Validate(parcel);
            var resultCode = codeValidator.Validate(code);
            if (resultTrackingID.IsValid && resultCode.IsValid)
            {
                return true;
            }
            return false;
        }
    }
}
