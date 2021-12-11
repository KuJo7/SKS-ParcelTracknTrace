using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    public interface IWebhookRepository
    {
        public void Create(DALWebhookResponse webhookResponse);
        public bool Delete(long Id);
        public List<DALWebhookResponse> ListParcelWebhooks(string trackingId);
    }
}
