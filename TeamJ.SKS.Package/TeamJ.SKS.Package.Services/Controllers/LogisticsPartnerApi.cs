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
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using TeamJ.SKS.Package.BusinessLogic;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Attributes;
using TeamJ.SKS.Package.Services.DTOs.Models;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;


namespace TeamJ.SKS.Package.Services.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class LogisticsPartnerApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParcelLogic _parcelLogic;
        /// <summary>
        /// LogisticsPartnerApiController default Constructor
        /// </summary>
        public LogisticsPartnerApiController()
        {
            _parcelLogic = new ParcelLogic();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            _mapper = new Mapper(config);
        }
        /// <summary>
        /// LogisticsPartnerApiController Constructor with 2 parameters
        /// </summary>
        public LogisticsPartnerApiController(IMapper mapper, IParcelLogic parcelLogic)
        {
            _parcelLogic = parcelLogic;
            _mapper = mapper;
        }
        /// <summary>
        /// Transfer an existing parcel into the system from the service of a logistics partner. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <response code="200">Successfully transitioned the parcel</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/parcel/{trackingId}")]
        [ValidateModelState]
        [SwaggerOperation("TransitionParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(NewParcelInfo), description: "Successfully transitioned the parcel")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult TransitionParcel([FromBody]Parcel body, [FromRoute][Required][RegularExpression("/^[A-Z0-9]{9}$/")]string trackingId)
        {

            BLParcel blParcel = _mapper.Map<BLParcel>(body);
            blParcel.TrackingId = trackingId;
            blParcel.FutureHops = new List<BLHopArrival>();
            blParcel.VisitedHops = new List<BLHopArrival>();
            if (_parcelLogic.TransitionParcel(blParcel))
            {
                // Mapping back auf SVC Parcel (?)
                // mapping entf?llt, weil nur ein string
                return Ok(new NewParcelInfo());
            }
            else
            {
                return BadRequest(new Error("Error: TransitionParcel"));

            }

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(NewParcelInfo));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));
            /*string exampleJson = null;
            exampleJson = "{\n  \"trackingId\" : \"PYJRB4HZ6\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<NewParcelInfo>(exampleJson)
                        : default(NewParcelInfo);            //TODO: Change the data returned
            return new ObjectResult(example);*/
        }
    }
}
