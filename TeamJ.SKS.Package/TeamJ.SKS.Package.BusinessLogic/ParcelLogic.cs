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
        IValidator<BLParcel> validator = new BLParcelValidator();
        IValidator<string> codeValidator = new BLCodeValidator();

        public BLParcel TrackParcel(string trackingID)
        {
            var test = new BLParcel();
            test.TrackingId = trackingID;
            var result = validator.Validate(test);
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
            var test = new BLParcel();
            test.TrackingId = trackingID;
            var result = validator.Validate(test);
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
