/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using TeamJ.SKS.Package.BusinessLogic;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Attributes;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class RecipientApiController : ControllerBase
    {
        private readonly IParcelLogic _parcelLogic;

        /// <summary>
        /// RecipientApiController default Constructor
        /// </summary>
        public RecipientApiController()
        {
            _parcelLogic = new ParcelLogic();
        }

        /// <summary>
        /// RecipientApiController Constructor with 2 parameters
        /// </summary>
        public RecipientApiController(IParcelLogic parcelLogic)
        {
            _parcelLogic = parcelLogic;
        }

        
        /// <summary>
        /// Find the latest state of a parcel by its tracking ID. 
        /// </summary>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <response code="200">Parcel exists, here&#x27;s the tracking information.</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">Parcel does not exist with this tracking ID.</response>
        [HttpGet]
        [Route("/parcel/{trackingId}")]
        [ValidateModelState]
        [SwaggerOperation("TrackParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(TrackingInformation), description: "Parcel exists, here&#x27;s the tracking information.")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult TrackParcel([FromRoute][Required][RegularExpression("/^[A-Z0-9]{9}$/")]string trackingId)
        {
            
            if (_parcelLogic.TrackParcel(trackingId) != null)
            {
                return Ok(new TrackingInformation());
            }
            else
            {
                return BadRequest(new Error("Error: TrackParcel"));
            }
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(TrackingInformation));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            /*string exampleJson = null;
            exampleJson = "{\n  \"visitedHops\" : [ {\n    \"dateTime\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"code\" : \"code\",\n    \"description\" : \"description\"\n  }, {\n    \"dateTime\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"code\" : \"code\",\n    \"description\" : \"description\"\n  } ],\n  \"futureHops\" : [ null, null ],\n  \"state\" : \"Pickup\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<TrackingInformation>(exampleJson)
                        : default(TrackingInformation);            //TODO: Change the data returned
            return new ObjectResult(example);*/
        }
    }
}
