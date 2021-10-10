/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using TeamJ.SKS.Package.BusinessLogic;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
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
    public class SenderApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParcelLogic _parcelLogic;

        public SenderApiController()
        {
            _parcelLogic = new ParcelLogic();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            _mapper = new Mapper(config);
        }
        public SenderApiController(IMapper mapper, IParcelLogic parcelLogic)
        {
            _parcelLogic = parcelLogic;
            _mapper = mapper;
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
        public virtual IActionResult SubmitParcel([FromBody]Parcel body)
        {

            BLParcel blParcel = _mapper.Map<BLParcel>(body);
            if (_parcelLogic.SubmitParcel(blParcel))
            {
                // Mapping back auf SVC Parcel (?)
                // mapping entf�llt, weil nur ein string
                return Ok(StatusCode(200));
            }
            else
            {
                return BadRequest(StatusCode(400, default(Error)));

            }

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
