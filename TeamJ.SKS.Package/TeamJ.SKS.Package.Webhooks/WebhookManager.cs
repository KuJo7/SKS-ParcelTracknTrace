using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.Webhooks.Interfaces;

namespace TeamJ.SKS.Package.Webhooks
{
    public class WebhookManager : IWebhookManager
    {
        private readonly IMapper _mapper;
        private readonly IWebhookRepository _webhookRepo;
        private readonly ILogger<WebhookManager> _logger;
        private static readonly HttpClient client = new HttpClient();
        private string msg;
        private string msgException;

        public WebhookManager(IWebhookRepository repo, IMapper mapper, ILogger<WebhookManager> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _webhookRepo = repo;
        }

        public async Task<DALWebhookResponse> SubscribeParcelWebhook(string trackingId, string url)
        {
            try
            {
                _logger.LogInformation("WebhookManager SubscribeParcelWebhook started.");
                DALWebhookResponse dalWebhook = new DALWebhookResponse();
                dalWebhook.TrackingId = trackingId;
                dalWebhook.Url = url;
                dalWebhook.CreatedAt = DateTime.Now;
                _webhookRepo.Create(dalWebhook);

                var values = new Dictionary<string, string> { };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString == "404 - Not Found\n")
                {
                    dalWebhook.Url = "404 - Not Found";
                    return dalWebhook;
                }

                return dalWebhook;

            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to use SubscribeParcelWebhook.";
                _logger.LogError(msg, ex);
                throw new WebhookException(nameof(SubscribeParcelWebhook), msg, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to use SubscribeParcelWebhook.";
                _logger.LogError(msgException, ex);
                throw new WebhookException(nameof(SubscribeParcelWebhook), msgException, ex);
            }

        }

        public bool UnsubscribeParcelWebhook(long id)
        {
            try
            {
                _logger.LogInformation("WebhookManager UnsubscribeParcelWebhook started.");
                if (!_webhookRepo.Delete(id))
                {
                    return true;
                }

                return false;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to use UnsubscribeParcelWebhook.";
                _logger.LogError(msg, ex);
                throw new WebhookException(nameof(UnsubscribeParcelWebhook), msg, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to use UnsubscribeParcelWebhook.";
                _logger.LogError(msgException, ex);
                throw new WebhookException(nameof(UnsubscribeParcelWebhook), msgException, ex);
            }
            
        }

        public List<DALWebhookResponse> ListParcelWebHooks(string trackingId)
        {
            try
            {
                _logger.LogInformation("WebhookManager ListParcelWebHooks started.");
                return _webhookRepo.ListParcelWebhooks(trackingId);

            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to use ListParcelWebhooks.";
                _logger.LogError(msg, ex);
                throw new WebhookException(nameof(ListParcelWebHooks), msg, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to use ListParcelWebhooks.";
                _logger.LogError(msgException, ex);
                throw new WebhookException(nameof(ListParcelWebHooks), msgException, ex);
            }

        }

        public void SubscriberNotification(string trackingId, string status)
        {
            try
            {
                _logger.LogInformation("WebhookManager SubscriberNotification started.");
                var subscriberList = _webhookRepo.ListParcelWebhooks(trackingId);
                foreach (var subscriber in subscriberList)
                {
                    var values = new Dictionary<string, string> { { "message", $"{status}" } };

                    var content = new FormUrlEncodedContent(values);

                    var response = client.PostAsync(subscriber.Url, content);
                }

            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to use SubscriberNotification.";
                _logger.LogError(msg, ex);
                throw new WebhookException(nameof(SubscriberNotification), msg, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to use SubscriberNotification.";
                _logger.LogError(msgException, ex);
                throw new WebhookException(nameof(SubscriberNotification), msgException, ex);
            }

            

        }
    }
}
