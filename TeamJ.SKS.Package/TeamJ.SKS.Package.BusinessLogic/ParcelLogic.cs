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
                var dalParcel = _repo.GetById(trackingID);

                if (dalParcel != null)
                {
                    var blParcel = _mapper.Map<BLParcel>(dalParcel);
                    var result = validator.Validate(blParcel);

                    if (result.IsValid)
                    {
                        _logger.LogInformation("ParcelLogic TrackParcel ended successful.");
                         //return _mapper.Map<TrackingInformation>(blParcel);
                        return blParcel;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    _logger.LogInformation("ParcelLogic TrackParcel ended unsuccessful.");
                    return null;
                }
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

        public bool SubmitParcel(BLParcel blParcel, out string trackingId)
        {
            try
            {
                _logger.LogInformation("ParcelLogic SubmitParcel started.");
                trackingId = GenerateTrackingId();
                blParcel.TrackingId = trackingId;
                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {
                    //var (lat, lon) = _agent.EncodeAddress(blParcel.Sender.Street + blParcel.Sender.PostalCode + blParcel.Sender.City + blParcel.Sender.Country); //returns tuple <lat, lon>
                    //connection between parcel and hop (lat/lon save to db)
                    //Predict future hops (=route between sender  recipient) 
                    blParcel.VisitedHops = new List<BLHopArrival>() { new BLHopArrival() { Code = "testcode011", DateTime = DateTime.Now, Description = "testdesc011" } };
                    blParcel.FutureHops = new List<BLHopArrival>() { new BLHopArrival() { Code = "testcode121", DateTime = DateTime.Now, Description = "testdesc121" },
                                                                      new BLHopArrival() { Code = "testcode231", DateTime = DateTime.Now, Description = "testdesc231" } };
                    
                    blParcel.State = BLParcel.StateEnum.PickupEnum;
                    DALParcel dalParcel = _mapper.Map<DALParcel>(blParcel);
                    _repo.Create(dalParcel);
                    //dalParcel.TrackingId = "TEST";
                    _logger.LogInformation("ParcelLogic SubmitParcel ended successful.");
                    //return _mapper.Map<BLParcel>(dalParcel);
                    return true;
                }
                _logger.LogInformation("ParcelLogic SubmitParcel ended unsuccessful.");
                trackingId = "";
                return false;
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

        private string GenerateTrackingId()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var id = new string(Enumerable.Repeat(chars, 9).Select(s => s[random.Next(s.Length)]).ToArray());
            
            //check in db if id exists 
            if (_repo.GetById(id) == null) //always not null 
                return id;
            else
                return GenerateTrackingId();
        }

        public bool ReportParcelDelivery(string trackingID)
        {
            try
            {
                _logger.LogInformation("ParcelLogic ReportParcelDelivery started.");
                /*var blParcel = _mapper.Map<BLParcel>(_repo.GetById(trackingID));
                blParcel.State = BLParcel.StateEnum.DeliveredEnum;
                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {*/
                    _logger.LogInformation("ParcelLogic ReportParcelDelivery ended successful.");
                var dalParcel = _repo.GetById(trackingID);
                if (dalParcel == null)
                {
                    return false;
                }
                else
                {
                    dalParcel.State = DALParcel.StateEnum.DeliveredEnum;
                    _repo.Update(dalParcel);
                    return true;
                }
                /*}
                _logger.LogInformation("ParcelLogic ReportParcelDelivery ended unsuccessful.");
                return false;*/
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to report a delivered parcel.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(ReportParcelDelivery), msg, ex);
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
                if (resultTrackingID.IsValid)
                {
                    //_repo.Update(_mapper.Map<DALParcel>(blParcel));
                    _logger.LogInformation("ParcelLogic ReportParcelHop ended successful.");
                    return true;
                }
                _logger.LogInformation("ParcelLogic ReportParcelHop ended unsuccessful.");
                return false;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to report a parcel arriving at a certain hop.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(ReportParcelHop), msg, ex);
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
