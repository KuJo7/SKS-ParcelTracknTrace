/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.2
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using Microsoft.Extensions.Logging;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.Services.Attributes;
using TeamJ.SKS.Package.Services.Interfaces;
using TeamJ.SKS.Package.Webhooks;
using TeamJ.SKS.Package.Webhooks.Interfaces;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ParcelWebhookApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebhookManager _webhookManager;
        private readonly ILogger<ParcelWebhookApiController> _logger;

        public ParcelWebhookApiController(IMapper mapper, IWebhookManager webhookManager, ILogger<ParcelWebhookApiController> logger)
        {
            _webhookManager = webhookManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all registered subscriptions for the parcel webhook.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <response code="200">List of webooks for the &#x60;trackingId&#x60;</response>
        /// <response code="404">No parcel found with that tracking ID.</response>
        [HttpGet]
        [Route("/parcel/{trackingId}/webhooks")]
        [ValidateModelState]
        [SwaggerOperation("ListParcelWebhooks")]
        [SwaggerResponse(statusCode: 200, type: typeof(WebhookResponses), description: "List of webooks for the &#x60;trackingId&#x60;")]
        public virtual IActionResult ListParcelWebhooks([FromRoute][Required][RegularExpression("^[A-Z0-9]{9}$")]string trackingId)
        {
            try
            {
                var result = _mapper.Map<List<WebhookResponse>>(_webhookManager.ListParcelWebHooks(trackingId));
                if (result.Count == 0)
                {
                    return StatusCode(404, "No parcel found with that tracking ID.");
                }
                return Ok(result);
            }
            catch (WebhookException ex)
            {
                var msg = "An error occured while trying to use the /parcel/{trackingId}/webhooks get api.";
                _logger.LogError(msg, ex);
                throw new ServiceException(nameof(ListParcelWebhooks), msg, ex);
            }
            catch (Exception ex)
            {
                var msgException = "An unknown error occured while trying to use the /parcel/{trackingId}/webhooks get api.";
                _logger.LogError(msgException, ex);
                throw new ServiceException(nameof(ListParcelWebhooks), msgException, ex);
            }

        }

        /// <summary>
        /// Subscribe to a webhook notification for the specific parcel.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <param name="url"></param>
        /// <response code="200">Successful response</response>
        /// <response code="404">No parcel found with that tracking ID.</response>
        [HttpPost]
        [Route("/parcel/{trackingId}/webhooks")]
        [ValidateModelState]
        [SwaggerOperation("SubscribeParcelWebhook")]
        [SwaggerResponse(statusCode: 200, type: typeof(WebhookResponse), description: "Successful response")]
        public async virtual Task<IActionResult> SubscribeParcelWebhook([FromRoute][Required][RegularExpression("^[A-Z0-9]{9}$")]string trackingId, [FromQuery][Required()]string url)
        {
            try
            {
                _logger.LogInformation("ParcelWebhookApiController SubscribeParcelWebhook started");
                var result = await _webhookManager.SubscribeParcelWebhook(trackingId, url);
                if (result.Url == "404 - Not Found")
                {
                    _webhookManager.UnsubscribeParcelWebhook(result.Id);
                    return StatusCode(404,"No parcel found with that tracking ID.");
                }
                WebhookResponse webhookResponse = _mapper.Map<WebhookResponse>(result);
                return Ok(webhookResponse);
            }
            catch (WebhookException ex)
            {
                var msg = "An error occured while trying to use the /parcel/{trackingId}/webhooks post api.";
                _logger.LogError(msg, ex);
                throw new ServiceException(nameof(SubscribeParcelWebhook), msg, ex);
            }
            catch (Exception ex)
            {
                var msgException = "An unknown error occured while trying to use the /parcel/{trackingId}/webhooks post api.";
                _logger.LogError(msgException, ex);
                throw new ServiceException(nameof(SubscribeParcelWebhook), msgException, ex);
            }
            
        }

        /// <summary>
        /// Remove an existing webhook subscription.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Subscription does not exist.</response>
        [HttpDelete]
        [Route("/parcel/webhooks/{id}")]
        [ValidateModelState]
        [SwaggerOperation("UnsubscribeParcelWebhook")]
        public virtual IActionResult UnsubscribeParcelWebhook([FromRoute][Required]long id)
        {
            try
            {
                _logger.LogInformation("ParcelWebhookApiController UnsubscribeParcelWebhook started");
                if (_webhookManager.UnsubscribeParcelWebhook(id))
                {
                    return Ok("Success");
                }

                return StatusCode(404, "Subscription does not exist.");
            }
            catch (WebhookException ex)
            {
                var msg = "An error occured while trying to use the /parcel/webhooks/id delete api.";
                _logger.LogError(msg, ex);
                throw new ServiceException(nameof(UnsubscribeParcelWebhook), msg, ex);
            }
            catch (Exception ex)
            {
                var msgException = "An unknown error occured while trying to use the /parcel/webhooks/id delete api.";
                _logger.LogError(msgException, ex);
                throw new ServiceException(nameof(UnsubscribeParcelWebhook), msgException, ex);
            }
        }
    }
}
