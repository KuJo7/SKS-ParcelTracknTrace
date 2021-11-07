using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.DTOs.Validators;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class ParcelLogic : IParcelLogic
    {
        readonly IValidator<BLParcel> validator = new BLParcelValidator();
        readonly IValidator<string> codeValidator = new BLCodeValidator();
        private readonly IParcelRepository _repo;
        private readonly IMapper _mapper;

        public ParcelLogic(IParcelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public BLParcel TrackParcel(string trackingID)
        {
            var blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
            var result = validator.Validate(blParcel);
            if (result.IsValid) 
            {
                // return _mapper.Map<BLParcel>(_repo.TrackParcel(parcel.TrackingId));
                return blParcel;
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
                DALParcel dalParcel = _mapper.Map<DALParcel>(blParcel);
                _repo.Create(dalParcel);
                return true;
            }
            return false;
        }

        public BLParcel SubmitParcel(BLParcel blParcel)
        {
            var result = validator.Validate(blParcel);
            if (result.IsValid)
            {
                DALParcel dalParcel = _mapper.Map<DALParcel>(blParcel);
                _repo.Create(dalParcel);
                //dalParcel.TrackingId = "TEST";
                return _mapper.Map<BLParcel>(dalParcel);
            }
                return null;
        }

        public bool ReportParcelDelivery(string trackingID)
        {
            var blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
            blParcel.State = BLParcel.StateEnum.DeliveredEnum;
            var result = validator.Validate(blParcel);
            if (result.IsValid)
            {
                _repo.Update(_mapper.Map<DALParcel>(blParcel));
                return true;
            }
            return false;
        }

        public bool ReportParcelHop(string trackingID, string code)
        {
            var blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
            //move first Hop of futureHops to visitedHops because parcel arrived at next Hop
            blParcel.VisitedHops.Add(blParcel.FutureHops.First());
            blParcel.FutureHops.Remove(blParcel.FutureHops.First());
            //Update parcel position at which hop (relation parcel and hop) ??
            blParcel.State = BLParcel.StateEnum.InTransportEnum;
            var resultTrackingID = validator.Validate(blParcel);
            var resultCode = codeValidator.Validate(code);
            if (resultTrackingID.IsValid && resultCode.IsValid)
            {
                _repo.Update(_mapper.Map<DALParcel>(blParcel));
                return true;
            }
            return false;
        }
    }
}
