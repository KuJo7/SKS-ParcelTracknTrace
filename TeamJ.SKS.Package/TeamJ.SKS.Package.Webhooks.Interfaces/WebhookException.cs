﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.Webhooks.Interfaces
{
    public class WebhookException : ApplicationException
    {
        public WebhookException(string webhookService, string message, Exception innerException) : base(message, innerException)
        {
            this.WebhookService = webhookService;
        }

            public string WebhookService { get; }
    }
}
