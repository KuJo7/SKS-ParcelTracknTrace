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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TeamJ.SKS.Package.BusinessLogic;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Attributes;
using TeamJ.SKS.Package.Services.DTOs.Models;
using TeamJ.SKS.Package.Services.Interfaces;
using TeamJ.SKS.Package.Webhooks.Interfaces;

namespace TeamJ.SKS.Package.Services.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class StaffApiController : ControllerBase
    {
        private readonly IParcelLogic _parcelLogic;
        private readonly ILogger<StaffApiController> _logger;
        private readonly IWebhookManager _webhookManager;

        /*public StaffApiController()
        {
            _parcelLogic = new ParcelLogic();
        }*/
        /// <summary>
        /// StaffApiController Constructor with 2 parameters
        /// </summary>
        public StaffApiController(IParcelLogic parcelLogic, ILogger<StaffApiController> logger, IWebhookManager webhookManager)
        {
            _parcelLogic = parcelLogic;
            _logger = logger;
            _webhookManager = webhookManager;
        }

        /// <summary>
        /// Report that a Parcel has been delivered at it&#x27;s final destination address. 
        /// </summary>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <response code="200">Successfully reported hop.</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">Parcel does not exist with this tracking ID. </response>
        [HttpPost]
        [Route("/parcel/{trackingId}/reportDelivery")]
        [ValidateModelState]
        [SwaggerOperation("ReportParcelDelivery")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ReportParcelDelivery([FromRoute][Required][RegularExpression("^[A-Z0-9]{9}$")]string trackingId)
        {
            try
            {
                _logger.LogInformation("StaffApi ReportParcelDelivery started.");
                if (_parcelLogic.ReportParcelDelivery(trackingId))
                {
                    _webhookManager.SubscriberNotification(trackingId, "Parcel has been successfuly delivered at its final address");
                    foreach (var subscriber in _webhookManager.ListParcelWebHooks(trackingId))
                    {
                        _webhookManager.UnsubscribeParcelWebhook(subscriber.Id);
                    }
                    _logger.LogInformation("StaffApi ReportParcelDelivery ended successful.");
                    return Ok(StatusCode(200));
                }
            }
            catch (BusinessLogicException ex)
            {
                var msg = "An error occured while trying to use the /parcel/trackingid/reportdelivery post api.";
                _logger.LogError(msg, ex);
                throw new ServiceException(nameof(ReportParcelDelivery), msg, ex);
            }
            catch (Exception ex)
            {
                var msgException = "An unknown error occured while trying to use the /parcel/trackingid/reportdelivery post api.";
                _logger.LogError(msgException, ex);
                throw new ServiceException(nameof(ReportParcelDelivery), msgException, ex);
            }
            _logger.LogInformation("StaffApi ReportParcelDelivery ended unsuccessful.");
            return BadRequest(new Error("Error: ReportParcelDelivery"));
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Report that a Parcel has arrived at a certain hop either Warehouse or Truck. 
        /// </summary>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <param name="code">The Code of the hop (Warehouse or Truck).</param>
        /// <response code="200">Successfully reported hop.</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">Parcel does not exist with this tracking ID or hop with code not found. </response>
        [HttpPost]
        [Route("/parcel/{trackingId}/reportHop/{code}")]
        [ValidateModelState]
        [SwaggerOperation("ReportParcelHop")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ReportParcelHop([FromRoute][Required][RegularExpression("^[A-Z0-9]{9}$")] string trackingId, [FromRoute][Required][RegularExpression("^[A-Z]{4}\\d{1,4}$")] string code)
        {
            try
            {
                _logger.LogInformation("StaffApi ReportParcelHop started.");
                if (_parcelLogic.ReportParcelHop(trackingId, code))
                {
                    _webhookManager.SubscriberNotification(trackingId, $"Parcel has been successfuly arrived at {code}");
                    _logger.LogInformation("StaffApi ReportParcelHop ended successful.");
                    return Ok(StatusCode(200));
                }
            }
            catch (BusinessLogicException ex)
            {
                var msg = "An error occured while trying to use the /parcel/trackingid/reportHop/code post api.";
                _logger.LogError(msg, ex);
                throw new ServiceException(nameof(ReportParcelHop), msg, ex);
            }
            catch (Exception ex)
            {
                var msgException = "An unknown error occured while trying to use the /parcel/trackingid/reportHop/code post api.";
                _logger.LogError(msgException, ex);
                throw new ServiceException(nameof(ReportParcelHop), msgException, ex);
            }
            _logger.LogInformation("StaffApi ReportParcelHop ended unsuccessful.");
            return BadRequest(new Error("Error: ReportParcelHop"));

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            //throw new NotImplementedException();
        }
    }
}
