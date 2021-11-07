/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class WarehouseManagementApiController : ControllerBase 
    {
        private readonly IMapper _mapper;
        private readonly IHopLogic _hopLogic;


        /*public WarehouseManagementApiController()
        {
            _hopLogic = new HopLogic();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            _mapper = new Mapper(config);

        }*/
        /// <summary>
        /// WarehouseManagementApiController Constructor with 2 parameters
        /// </summary>
        public WarehouseManagementApiController(IMapper mapper, IHopLogic hopLogic)
        {
            _hopLogic = hopLogic;
            _mapper = mapper;
        }
        /// <summary>
        /// WarehouseManagementApiController Constructor with 1 parameters
        /// </summary>
        public WarehouseManagementApiController(IHopLogic hopLogic)
        {
            _hopLogic = hopLogic;
        }

        /// <summary>
        /// Exports the hierarchy of Warehouse and Truck objects. 
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">An error occurred loading.</response>
        /// <response code="404">No hierarchy loaded yet.</response>
        [HttpGet]
        [Route("/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ExportWarehouses")]
        [SwaggerResponse(statusCode: 200, type: typeof(Warehouse), description: "Successful response")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "An error occurred loading.")]
        public virtual IActionResult ExportWarehouses()
        {
            
            if (_hopLogic.ExportWarehouses().Any())
            {
                return Ok(new Warehouse());
            }
            else
            {
                return BadRequest(new Error("Error: ExportWarehouses"));
            }
                        
                        
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Warehouse));


            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            //string exampleJson = null;
            //exampleJson = "\"\"";
            
            //            var example = exampleJson != null
            //            ? JsonConvert.DeserializeObject<Warehouse>(exampleJson)
            //            : default(Warehouse);            //TODO: Change the data returned
            //return new ObjectResult(example);
        }

        /// <summary>
        /// Get a certain warehouse or truck by code
        /// </summary>
        /// <param name="code"></param>
        /// <response code="200">Successful response</response>
        /// <response code="400">An error occurred loading.</response>
        /// <response code="404">Warehouse id not found</response>
        [HttpGet]
        [Route("/warehouse/{code}")]
        [ValidateModelState]
        [SwaggerOperation("GetWarehouse")]
        [SwaggerResponse(statusCode: 200, type: typeof(Warehouse), description: "Successful response")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "An error occurred loading.")]
        public virtual IActionResult GetWarehouse([FromRoute][Required]string code)
        {
            
            if (_hopLogic.GetWarehouse(code) != null)
            {
                return Ok(new Warehouse());
            }
            else
            {
                return BadRequest(new Error("Error: GetWarehouse"));
            }

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Warehouse));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            //string exampleJson = null;
            //exampleJson = "\"\"";

            //            var example = exampleJson != null
            //            ? JsonConvert.DeserializeObject<Warehouse>(exampleJson)
            //            : default(Warehouse);            //TODO: Change the data returned
            //return new ObjectResult(example);
        }

        /// <summary>
        /// Imports a hierarchy of Warehouse and Truck objects. 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successfully loaded.</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ImportWarehouses")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ImportWarehouses([FromBody]Warehouse body)
        {
            BLWarehouse blWarehouse = _mapper.Map<BLWarehouse>(body);
            blWarehouse.NextHops = new List<BLWarehouseNextHops>();
            if (_hopLogic.ImportWarehouses(blWarehouse))
            {
                // Mapping back auf SVC Parcel (?)
                // mapping entf?llt nicht aufpassen!
                return Ok(200);
            }
            else
            {
                return BadRequest(new Error("Error: ImportWarehouses"));
            }
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //throw new NotImplementedException();
        }
    }
}
