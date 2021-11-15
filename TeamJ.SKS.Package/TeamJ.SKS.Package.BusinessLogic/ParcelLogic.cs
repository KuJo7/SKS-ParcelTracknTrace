using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
        private readonly ILogger<ParcelLogic> _logger;
        private string msg;
        private string msgException;
        private string msgMapper = "An error occured while trying to map parcels.";

        public ParcelLogic(IParcelRepository repo, IMapper mapper, ILogger<ParcelLogic> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public BLParcel TrackParcel(string trackingID)
        {
            try
            {
                _logger.LogInformation("ParcelLogic TrackParcel started.");
                var blParcel = new BLParcel();
                blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {
                    _logger.LogInformation("ParcelLogic TrackParcel ended successful.");
                    // return _mapper.Map<BLParcel>(_repo.TrackParcel(parcel.TrackingId));
                    return blParcel;
                }
                _logger.LogInformation("ParcelLogic TrackParcel ended unsuccessful.");
                return null;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to track a parcel.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(TrackParcel), msg, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(TrackParcel), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to track a parcel.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(TrackParcel), msgException, ex);
            }

            
        }

        public bool TransitionParcel(BLParcel blParcel)
        {
            try
            {
                _logger.LogInformation("ParcelLogic TransitionParcel started.");
                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {
                    DALParcel dalParcel = _mapper.Map<DALParcel>(blParcel);
                    _repo.Create(dalParcel);
                    _logger.LogInformation("ParcelLogic TransitionParcel ended successful.");
                    return true;
                }
                _logger.LogInformation("ParcelLogic TransitionParcel ended unsuccessful.");
                return false;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to transition a parcel.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(TransitionParcel), msg, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(TransitionParcel), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to transition a parcel.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(TransitionParcel), msgException, ex);
            }
            
        }

        public BLParcel SubmitParcel(BLParcel blParcel)
        {
            try
            {
                _logger.LogInformation("ParcelLogic SubmitParcel started.");
                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {
                    DALParcel dalParcel = _mapper.Map<DALParcel>(blParcel);
                    _repo.Create(dalParcel);
                    //dalParcel.TrackingId = "TEST";
                    _logger.LogInformation("ParcelLogic SubmitParcel ended successful.");
                    return _mapper.Map<BLParcel>(dalParcel);
                }
                _logger.LogInformation("ParcelLogic SubmitParcel ended unsuccessful.");
                return null;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to submit a parcel.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(SubmitParcel), msg, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(SubmitParcel), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to submit a parcel.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(SubmitParcel), msgException, ex);
            }
            
        }

        public bool ReportParcelDelivery(string trackingID)
        {
            try
            {
                _logger.LogInformation("ParcelLogic ReportParcelDelivery started.");
                var blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
                //blParcel.State = BLParcel.StateEnum.DeliveredEnum;
                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {
                    _logger.LogInformation("ParcelLogic ReportParcelDelivery ended successful.");
                    _repo.Update(_mapper.Map<DALParcel>(blParcel));
                    return true;
                }
                _logger.LogInformation("ParcelLogic ReportParcelDelivery ended unsuccessful.");
                return false;
            }
            catch (DataAccessException ex)
            {
                msgSQL = "An error occured while trying to report a delivered parcel.";
                _logger.LogError(msgSQL, ex);
                throw new BusinessLogicException(nameof(ReportParcelDelivery), msgSQL, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(ReportParcelDelivery), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to report a delivered parcel.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(ReportParcelDelivery), msgException, ex);
            }
            
        }

        public bool ReportParcelHop(string trackingID, string code)
        {
            try
            {
                _logger.LogInformation("ParcelLogic ReportParcelHop started.");
                var blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
                //move first Hop of futureHops to visitedHops because parcel arrived at next Hop
                //blParcel.VisitedHops.Add(blParcel.FutureHops.First());
                //blParcel.FutureHops.Remove(blParcel.FutureHops.First());
                //Update parcel position at which hop (relation parcel and hop) ??
                //blParcel.State = BLParcel.StateEnum.InTransportEnum;
                var resultTrackingID = validator.Validate(blParcel);
                var resultCode = codeValidator.Validate(code);
                if (resultTrackingID.IsValid && resultCode.IsValid)
                {
                    _repo.Update(_mapper.Map<DALParcel>(blParcel));
                    _logger.LogInformation("ParcelLogic ReportParcelHop ended successful.");
                    return true;
                }
                _logger.LogInformation("ParcelLogic ReportParcelHop ended unsuccessful.");
                return false;
            }
            catch (DataAccessException ex)
            {
                msgSQL = "An error occured while trying to report a parcel arriving at a certain hop.";
                _logger.LogError(msgSQL, ex);
                throw new BusinessLogicException(nameof(ReportParcelHop), msgSQL, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(ReportParcelHop), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to report a parcel arriving at a certain hop.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(ReportParcelHop), msgException, ex);
            }
            
        }
    }
}
