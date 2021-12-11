/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using TeamJ.SKS.Package.BusinessLogic;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Attributes;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.Services.DTOs.Models;
using TeamJ.SKS.Package.Services.Interfaces;

namespace TeamJ.SKS.Package.Services.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class SenderApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParcelLogic _parcelLogic;
        private readonly ILogger<SenderApiController> _logger;

        /*public SenderApiController()
        {
            _parcelLogic = new ParcelLogic();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            _mapper = new Mapper(config);
        }*/
        /// <summary>
        /// SenderApiController Constructor with 2 parameters
        /// </summary>
        public SenderApiController(IMapper mapper, IParcelLogic parcelLogic, ILogger<SenderApiController> logger)
        {
            _parcelLogic = parcelLogic;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Submit a new parcel to the logistics service. 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successfully submitted the new parcel</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/parcel")]
        [ValidateModelState]
        [SwaggerOperation("SubmitParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(NewParcelInfo), description: "Successfully submitted the new parcel")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult SubmitParcel([FromBody] Parcel body)
        {
            try
            {
                _logger.LogInformation("SenderAPI SubmitParcel started.");
                BLParcel blParcel = _mapper.Map<BLParcel>(body);
                //blParcel.FutureHops = new List<BLHopArrival>();
                //blParcel.VisitedHops = new List<BLHopArrival>();



                // Mapping back auf SVC Parcel (?)
                // mapping entf?llt, weil nur ein string
                var trackingId = "";
                
                if (_parcelLogic.SubmitParcel(blParcel, out trackingId))
                {
                    _logger.LogInformation("SenderAPI SubmitParcel ended successful.");
                    return Ok(new NewParcelInfo() { TrackingId = trackingId });
                }
            }
            catch (BusinessLogicException ex)
            {
                var msg = "An error occured while trying to use the /parcel post api.";
                _logger.LogError(msg, ex);
                throw new ServiceException(nameof(SubmitParcel), msg, ex);
            }
            catch (Exception ex)
            {
                var msgException = "An unknown error occured while trying to use the /parcel post api.";
                _logger.LogError(msgException, ex);
                throw new ServiceException(nameof(SubmitParcel), msgException, ex);
            }
            _logger.LogInformation("SenderAPI SubmitParcel ended unsuccessful.");
            return BadRequest(new Error("Error: SubmitParcel"));


            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(NewParcelInfo));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));
            //string exampleJson = null;
            //exampleJson = "{\n  \"trackingId\" : \"PYJRB4HZ6\"\n}";

            //            var example = exampleJson != null
            //            ? JsonConvert.DeserializeObject<NewParcelInfo>(exampleJson)
            //            : default(NewParcelInfo);            //TODO: Change the data returned
            //return new ObjectResult(example);
        }
    }
}