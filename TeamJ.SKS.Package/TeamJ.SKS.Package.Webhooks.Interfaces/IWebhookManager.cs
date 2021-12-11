using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.Webhooks.Interfaces
{
    public interface IWebhookManager
    {
        public Task<DALWebhookResponse> SubscribeParcelWebhook(string trackingId, string url);
        public bool UnsubscribeParcelWebhook(long id);

        public List<DALWebhookResponse> ListParcelWebHooks(string trackingId);

        public void SubscriberNotification(string trackingId, string status);

    }
}
