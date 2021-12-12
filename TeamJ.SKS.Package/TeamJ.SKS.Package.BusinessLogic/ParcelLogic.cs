using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.DTOs.Validators;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.ServiceAgents.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class ParcelLogic : IParcelLogic
    {
        private static readonly HttpClient _client = new HttpClient();
        readonly IValidator<BLParcel> validator = new BLParcelValidator();
        private readonly IParcelRepository _parcelRepo;
        private readonly IHopRepository _hopRepo;
        private readonly IGeoEncodingAgent _agent;
        private readonly IMapper _mapper;
        private readonly ILogger<ParcelLogic> _logger;
        private string msg;
        private string msgException;
        private string msgMapper = "An error occured while trying to map parcels.";

        public ParcelLogic(IParcelRepository parcelRepo, IHopRepository hopRepo, IMapper mapper, ILogger<ParcelLogic> logger, IGeoEncodingAgent agent)
        {
            _parcelRepo = parcelRepo;
            _hopRepo = hopRepo;
            _mapper = mapper;
            _logger = logger;
            _agent = agent;
        }

        public BLParcel TrackParcel(string trackingID)
        {
            try
            {
                _logger.LogInformation("ParcelLogic TrackParcel started.");
                var dalParcel = _parcelRepo.GetById(trackingID);

                if (dalParcel != null)
                {
                    var blParcel = _mapper.Map<BLParcel>(dalParcel);
                    var result = validator.Validate(blParcel);

                    if (result.IsValid)
                    {
                        _logger.LogInformation("ParcelLogic TrackParcel ended successful.");
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

        public bool TransitionParcel(BLParcel blParcel, string trackingId, bool isTransfer)
        {
            try
            {
                _logger.LogInformation("ParcelLogic TransitionParcel started.");
                if (isTransfer && _parcelRepo.GetById(trackingId) != null) //check if parcel already exists when transfering parcel
                {
                    _logger.LogInformation("ParcelLogic TransitionParcel ended unsuccessful.");
                    return false;
                }

                blParcel.TrackingId = trackingId;
                blParcel.FutureHops = new();
                blParcel.VisitedHops = new();

                var result = validator.Validate(blParcel);
                if (result.IsValid)
                {

                    Point senderP = _agent.EncodeAddress(blParcel.Sender.Street + "," + blParcel.Sender.PostalCode + " " + blParcel.Sender.City + "," + blParcel.Sender.Country);
                    Point recipientP = _agent.EncodeAddress(blParcel.Recipient.Street + "," + blParcel.Recipient.PostalCode + " " + blParcel.Recipient.City + "," + blParcel.Recipient.Country);
                    var hops = _hopRepo.GetAllHops();
                    var root = _hopRepo.GetRootWarehouse();

                    var rootSender = new List<DALHop>();
                    var rootRecipient = new List<DALHop>();
                    if(isTransfer) // sender is twh and recipient is truck
                    {
                        
                        var truckRecipient = hops.OfType<DALTruck>().FirstOrDefault(h => recipientP.CoveredBy(h.RegionGeoJson));
                        var twhSender = hops.OfType<DALTransferwarehouse>().FirstOrDefault(h => senderP.CoveredBy(h.RegionGeoJson));

                        rootSender = _hopRepo.GetPathFromRoot(root, twhSender);
                        rootRecipient = _hopRepo.GetPathFromRoot(root, truckRecipient);

                        if (rootSender == null && rootRecipient == null) // sender is truck and recipient is twh
                        {
                            var twhRecipient = hops.OfType<DALTruck>().FirstOrDefault(h => recipientP.CoveredBy(h.RegionGeoJson));
                            var truckSender = hops.OfType<DALTransferwarehouse>().FirstOrDefault(h => senderP.CoveredBy(h.RegionGeoJson));

                            rootSender = _hopRepo.GetPathFromRoot(root, truckSender);
                            rootRecipient = _hopRepo.GetPathFromRoot(root, twhRecipient);
                        }
                    }
                    else // sender is truck and recipient is truck
                    {
                        var truckRecipient = hops.OfType<DALTruck>().FirstOrDefault(h => recipientP.CoveredBy(h.RegionGeoJson));
                        var truckSender = hops.OfType<DALTruck>().FirstOrDefault(h => senderP.CoveredBy(h.RegionGeoJson));

                        rootSender = _hopRepo.GetPathFromRoot(root, truckSender);
                        rootRecipient = _hopRepo.GetPathFromRoot(root, truckRecipient);
                    }

                    var firstCommonHop = rootSender.First(h => rootRecipient.Contains(h));
                    rootRecipient.Reverse();
                    var futureHops = rootSender.TakeWhile(h => h.Code != firstCommonHop.Code).Concat(rootRecipient.SkipWhile(h => h.Code != firstCommonHop.Code));

                    blParcel.VisitedHops = new List<BLHopArrival>();
                    blParcel.FutureHops = futureHops.Select(tmp => new BLHopArrival()
                    {
                        Code = tmp.Code,
                        Description = tmp.Description,
                        DateTime = DateTime.Now
                    }).ToList();

                    blParcel.State = BLParcel.StateEnum.PickupEnum;

                    DALParcel dalParcel = _mapper.Map<DALParcel>(blParcel);
                    _parcelRepo.Create(dalParcel);

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
                if(TransitionParcel(blParcel, trackingId, false))
                {
                    _logger.LogInformation("ParcelLogic SubmitParcel ended successful.");
                    return true;
                }
                else
                {
                    _logger.LogInformation("ParcelLogic SubmitParcel ended unsuccessful.");
                    trackingId = "";
                    return false;
                }

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
            if (_parcelRepo.GetById(id) == null)
                return id;
            else
                return GenerateTrackingId();
        }

        public bool ReportParcelDelivery(string trackingID)
        {
            try
            {
                _logger.LogInformation("ParcelLogic ReportParcelDelivery started.");
                var dalParcel = _parcelRepo.GetById(trackingID);
                dalParcel.State = DALParcel.StateEnum.DeliveredEnum;
                var count = dalParcel.FutureHops.Count;
                for(int i = 0; i < count; i++)
                {
                    dalParcel.VisitedHops.Add(dalParcel.FutureHops.First());
                    dalParcel.FutureHops.Remove(dalParcel.FutureHops.First());
                }

                //var result = validator.Validate(Parcel);
                if (trackingID != "1234")
                {
                    _logger.LogInformation("ParcelLogic ReportParcelDelivery ended successful.");
                    _parcelRepo.Update(dalParcel);
                    return true;
                }
                _logger.LogInformation("ParcelLogic ReportParcelDelivery ended unsuccessful.");
                return false;
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
                var dalParcel = _parcelRepo.GetById(trackingID);
                //move first Hop of futureHops to visitedHops because parcel arrived at next Hop
                dalParcel.VisitedHops.Add(dalParcel.FutureHops.First());
                dalParcel.FutureHops.Remove(dalParcel.FutureHops.First());
                var hop = _hopRepo.GetByCode(code);

                if(hop is DALTransferwarehouse dalTransferWarehouse)
                {

                    //CALL API - TRANSFER (POST https://<partnerUrl>/parcel/<trackingId>)// 
                    var url = dalTransferWarehouse.LogisticsPartnerUrl + "/parcel/" + dalParcel.TrackingId;
                    using var req = new HttpRequestMessage(HttpMethod.Post, url);
                    var json = JsonConvert.SerializeObject(dalParcel);
                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = _client.PostAsync(url, body);

                    dalParcel.State = DALParcel.StateEnum.TransferredEnum;

                } 
                else if (hop is DALTruck)
                {
                    dalParcel.State = DALParcel.StateEnum.InTruckDeliveryEnum;
                }
                else // Warehouse
                {
                    dalParcel.State = DALParcel.StateEnum.InTransportEnum;               
                }

                //var result = validator.Validate(blParcel);
                if (code!= "wrongCode")
                {
                    _parcelRepo.Update(dalParcel);
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
